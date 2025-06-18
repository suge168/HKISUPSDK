using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISUPDemo.Public;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace ISUPDemo
{
    public partial class StorageServer : Form
    {
        private string FilePath = null;
        public int m_nPort = 0;
        public int m_nDownloadPort = 0;
        public string m_csListenIP = null;
        public string m_csDownloadListenIP = null;
        public string SS_CLIENT_FILE_PATH_PARAM_NAME = "File-Path";
        public string SS_CLIENT_VRB_FILENAME_CODE = "Filename-Code";
        public string SS_CLIENT_KMS_USER_NAME = "KMS-Username";
        public string SS_CLIENT_KMS_PASSWIRD = "KMS-Password";
        public string SS_CLIENT_CLOUD_AK_NAME = "Access-Key";
        public string SS_CLIENT_CLOUD_SK_NAME = "Secret-Key";
        private string SS_CLIENT_CLOUD_POOL_ID = "Pool-Id";
        private string Format = "filename=1.jpg&code=";
        private string PoolID = null;

        public string strAK = null;
        public string strSK = null;
        public string m_csPicServerIP = null;

        public int lSSClientHandle = -1;
        public int m_PicServerType = 0;

        public XmlDocument doc = new XmlDocument();

        public StorageServer()
        {
            InitializeComponent();
            InitCfgXML();

            m_csListenIP = textBoxMonitorIP.Text;
            m_nPort = int.Parse(textBoxMonitorPort.Text);

            m_csDownloadListenIP = textBoxMonitorAddress.Text;
            m_nDownloadPort = int.Parse(textBoxPort.Text);
        }


        public void InitCfgXML()
        {
            String strTemp;
            doc.Load(@"ISUPDemo.xml");
            try
            {
                XmlNode root = doc.DocumentElement.FirstChild;

                textBoxMonitorIP.Text = root.SelectSingleNode("//PictureServerIP").InnerText.ToString();
                textBoxMonitorPort.Text = root.SelectSingleNode("//PictureServerPort").InnerText.ToString();

                textBoxMonitorAddress.Text = root.SelectSingleNode("//PictureServerIP").InnerText.ToString();
                textBoxPort.Text = root.SelectSingleNode("//PictureServerPort").InnerText.ToString();

                m_PicServerType = int.Parse(root.SelectSingleNode("//PictureServerType").InnerText);

                if (0 == m_PicServerType)
                {
                    comboBoxSStype.SelectedIndex = 0;
                    comboBoxDownloadServerType.SelectedIndex = 0;
                }
                else if (1 == m_PicServerType)
                {
                    comboBoxSStype.SelectedIndex = 1;
                    comboBoxDownloadServerType.SelectedIndex = 1;
                }
                else if (2 == m_PicServerType)
                {
                    comboBoxSStype.SelectedIndex = 2;
                    comboBoxDownloadServerType.SelectedIndex = 2;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("InitCfgXML fail", "Error", MessageBoxButtons.OK);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != openFileDialogPath.ShowDialog())
            {
                MessageBox.Show("please select corrct file", "Error", MessageBoxButtons.OK);
            }
            FilePath = System.IO.Path.GetFullPath(openFileDialogPath.FileName);
            textBoxFilePath.Text = FilePath;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            HCEHomeSS.NET_EHOME_SS_CLIENT_PARAM struClientParam = new HCEHomeSS.NET_EHOME_SS_CLIENT_PARAM();
            struClientParam.Init();
            struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_TOMCAT;
            m_csListenIP = textBoxMonitorIP.Text;
            m_nPort = int.Parse(textBoxMonitorPort.Text);
            m_csListenIP.CopyTo(0, struClientParam.struAddress.szIP, 0, m_csListenIP.Length);
            struClientParam.struAddress.wPort = (short)m_nPort;
            struClientParam.byHttps = 0;
//            int lPssClientHandle = -1;
            char[] szUrl = new char[HCEHomePublic.MAX_URL_LEN_SS];
            char[] szPicUrl = new char[HCEHomePublic.MAX_URL_LEN_SS];

            if (0 == comboBoxSStype.SelectedIndex)
            {
                struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_VRB;
            }
            else if (1 == comboBoxSStype.SelectedIndex)
            {
                struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_KMS;
            }
            else
            {
                struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_CLOUD;
            }
            lSSClientHandle = HCEHomeSS.NET_ESS_CreateClient(ref struClientParam);
            if (lSSClientHandle < 0)
            {
                DeviceTree.g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 0, "NET_ESS_CreateClient");
                return;
            }
            HCEHomeSS.NET_ESS_ClientSetTimeout(lSSClientHandle, 60 * 1000, 60 * 1000);
          
            strAK = textBoxAK.Text;
            strSK = textBoxSK.Text;
            if (comboBoxSStype.SelectedIndex == 0)
            {
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_VRB_FILENAME_CODE, "filename=1.jpg&code=");
            }
            else if (comboBoxSStype.SelectedIndex == 1)
            {//KMS
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_KMS_USER_NAME, strAK);
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_KMS_PASSWIRD, strSK);
            }
            else if (comboBoxSStype.SelectedIndex == 2)
            {
                PoolID = PoolIDText.Text;
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_CLOUD_AK_NAME, strAK);
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_CLOUD_SK_NAME, strSK);
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_CLOUD_POOL_ID, PoolID);
            }

            IntPtr ptrszUrl = Marshal.AllocHGlobal(HCEHomePublic.MAX_URL_LEN_SS);
            if (checkBoxBuffer.Checked)
            {               
                if (File.Exists(FilePath))
                {
                    FileStream fs = File.OpenRead(FilePath);
                    int dwFileLen = 0;
                    dwFileLen = (int)fs.Length;
                    byte[] byPicData = new byte[dwFileLen];
                    fs.Read(byPicData, 0, dwFileLen);
                    fs.Close();

                    IntPtr pBuff = Marshal.AllocHGlobal(dwFileLen);
                    try
                    {
                        Marshal.Copy(byPicData, 0, pBuff, dwFileLen);

                        bool Flag = HCEHomeSS.NET_ESS_ClientDoUploadBuffer(lSSClientHandle, ptrszUrl, HCEHomePublic.MAX_URL_LEN_SS - 1, pBuff, dwFileLen);
                        if (!Flag)
                        {
                            MessageBox.Show("Upload Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            HCEHomeSS.NET_ESS_DestroyClient(lSSClientHandle);
                            return;
                        }
                        string strszUrl = Marshal.PtrToStringAnsi(ptrszUrl);
                        textBoxURLresult.Text = strszUrl;
                        textBoxFileUrl.Text = strszUrl;
                        Marshal.FreeHGlobal(ptrszUrl);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(pBuff);
                    }                    
                }
            }
            else
            {
                HCEHomeSS.NET_ESS_ClientSetParam(lSSClientHandle, SS_CLIENT_FILE_PATH_PARAM_NAME, FilePath);
                bool Flag = HCEHomeSS.NET_ESS_ClientDoUpload(lSSClientHandle, ptrszUrl, HCEHomePublic.MAX_URL_LEN_SS - 1);
                if (!Flag)
                {
                    MessageBox.Show("Upload Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                    HCEHomeSS.NET_ESS_DestroyClient(lSSClientHandle);
                    return;
                }
                string strszUrl = Marshal.PtrToStringAnsi(ptrszUrl);
                textBoxURLresult.Text = strszUrl;
                textBoxFileUrl.Text = strszUrl;
                Marshal.FreeHGlobal(ptrszUrl);
            }

            HCEHomeSS.NET_ESS_DestroyClient(lSSClientHandle);
        }

        private void comboBoxSStype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (2 == comboBoxSStype.SelectedIndex)  //cloud
            {
                groupBoxCloud.Visible = true;
                label6.Text = "AK:";
                label7.Text = "SK:";
            }
            else if (1 == comboBoxSStype.SelectedIndex)   //kms
            {
                groupBoxCloud.Visible = true;
                label6.Text = "Kms Admin:";
                label7.Text = "Kms Pass:";
            }
            else
            {
                groupBoxCloud.Visible = false;
            }
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            HCEHomeSS.NET_EHOME_SS_CLIENT_PARAM struClientParam = new HCEHomeSS.NET_EHOME_SS_CLIENT_PARAM();
            struClientParam.Init();
            struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_TOMCAT;
            m_csDownloadListenIP.CopyTo(0, struClientParam.struAddress.szIP, 0, m_csDownloadListenIP.Length);
            struClientParam.struAddress.wPort = (short)m_nDownloadPort;
            struClientParam.byHttps = 0;
            char[] szUrl = new char[HCEHomePublic.MAX_URL_LEN_SS];
            char[] szPicUrl = new char[HCEHomePublic.MAX_URL_LEN_SS];

            if (0 == comboBoxDownloadServerType.SelectedIndex)
            {
                struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_VRB;
            }
            else if (1 == comboBoxDownloadServerType.SelectedIndex)
            {
                struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_KMS;
            }
            else
            {
                struClientParam.enumType = HCEHomeSS.NET_EHOME_SS_CLIENT_TYPE.NET_EHOME_SS_CLIENT_TYPE_CLOUD;
            }
            lSSClientHandle = HCEHomeSS.NET_ESS_CreateClient(ref struClientParam);
            if (lSSClientHandle < 0)
            {
                DeviceTree.g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 0, "NET_ESS_CreateClient");
                return;
            }
            HCEHomeSS.NET_ESS_ClientSetTimeout(lSSClientHandle, 60 * 1000, 60 * 1000);


            Int32 dwContentLen = 0;
            IntPtr pFileContent = Marshal.AllocHGlobal(2*1024*1024);
            bool Flag = HCEHomeSS.NET_ESS_ClientDoDownload(lSSClientHandle, Marshal.StringToHGlobalAnsi(textBoxFileUrl.Text.ToString()), pFileContent, dwContentLen);
            if (!Flag)
            {
                MessageBox.Show("Download Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
            }
            else
            {
                string path = textBoxDownloadFilePath.ToString() + DateTime.Now.ToString() + "download.data";
                FileStream fs = new FileStream(path, FileMode.Create);
                byte[] by = new byte[dwContentLen];
                Marshal.Copy(pFileContent, by, 0, dwContentLen);
                fs.Write(by, 0, dwContentLen);
                fs.Close();
            }
            Marshal.FreeHGlobal(pFileContent);
            HCEHomeSS.NET_ESS_DestroyClient(lSSClientHandle);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (lSSClientHandle < 0)
            {
                MessageBox.Show("NET_ESS_CreateClient first!");
                return;
            }

            bool ret = HCEHomeSS.NET_ESS_ClientDoDelete(lSSClientHandle, Marshal.StringToHGlobalAnsi(textBoxFileUrl.Text.ToString()));
            if (!ret)
            {
                MessageBox.Show("Delete Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileSavePath = new FolderBrowserDialog();
            if (DialogResult.OK != fileSavePath.ShowDialog())
            {
                MessageBox.Show("please select corrct file", "Error", MessageBoxButtons.OK);
            }
            textBoxDownloadFilePath.Text = fileSavePath.SelectedPath;
        }

        private void comboBoxDownloadServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (2 == comboBoxDownloadServerType.SelectedIndex)  //cloud
            {
                groupBox4.Visible = true;
                label19.Text = "AK:";
                label20.Text = "SK:";
            }
            else if (1 == comboBoxDownloadServerType.SelectedIndex)   //kms
            {
                groupBox4.Visible = true;
                label19.Text = "Kms Admin:";
                label20.Text = "Kms Pass:";
            }
            else
            {
                groupBox4.Visible = false;
            }
        }

    }
}
