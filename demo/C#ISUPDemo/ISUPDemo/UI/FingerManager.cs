using ISUPDemo.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace ISUPDemo.UI
{
    public partial class FingerManager : Form
    {
        public class UserInfoCount
        {
            public string userNumber { get; set; }
        }

        public class ResponseStatus
        {
            public string requestURL { get; set; }
            public int statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
        }
        public class Valid
        {
            public string enable { get; set; }
            public string beginTime { get; set; }
            public string endTime { get; set; }
            public string timeType { get; set; }
        }
        public class RightPlanItem
        {
            public int doorNo { get; set; }
            public string planTemplateNo { get; set; }
        }
        public class UserInfoItem
        {
            public string employeeNo { get; set; }
            public string name { get; set; }
            public string userType { get; set; }
            public string closeDelayEnabled { get; set; }
            public Valid Valid { get; set; }
            public string belongGroup { get; set; }
            public string password { get; set; }
            public string doorRight { get; set; }
            public List<RightPlanItem> RightPlan { get; set; }
            public int maxOpenDoorTime { get; set; }
            public int openDoorTime { get; set; }
            public string userVerifyMode { get; set; }
        }
        public class UserInfoSearch
        {
            public string searchID { get; set; }
            public string responseStatusStrg { get; set; }
            public int numOfMatches { get; set; }
            public int totalMatches { get; set; }
            public List<UserInfoItem> UserInfo { get; set; }
        }
        public class UserInfoSearchRoot
        {
            public UserInfoSearch UserInfoSearch { get; set; }
        }
        public class FingerPrintList
        {
            public int cardReaderNo { get; set; }
            public int fingerPrintID { get; set; }
            public string fingerType { get; set; }
            public string fingerData { get; set; }
            public List<string> leaderFP { get; set; }
        }

        public class FingerPrintInfo
        {
            public string searchID { get; set; }
            public string status { get; set; }
            public List<FingerPrintList> FPList { get; set; }
        }

        public class FPInfo
        {
            public FingerPrintInfo FingerPrintInfo { get; set; }
        }
        public class StatusListItem
        {
            public int id { get; set; }
            public int cardReaderRecvStatus { get; set; }
            public string errorMsg { get; set; }
        }
        public class FingerPrintStatus
        {
            public List<StatusListItem> StatusList { get; set; }
            public int totalStatus { get; set; }
        }
        public class FPRoot
        {
            public FingerPrintStatus FingerPrintStatus { get; set; }
        }

        //Delete Process
        public class FingerPrintDeleteProcess
        {
            public string status { get; set; }
        }
        public class FPDel
        {
            public FingerPrintDeleteProcess FpDeletProcess { get; set; }
        }
        private int m_iDeviceIndex;
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private DeviceLogList g_formList = DeviceLogList.Instance();

        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String Method = String.Empty;
       
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];
        IntPtr ptrCfg = IntPtr.Zero;
        String FingerPath = String.Empty;
        String strTemp = String.Empty;
        String m_strInputXml = String.Empty;

        public FingerManager()
        {
            InitializeComponent();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
        }



        public void ConfigMethod(ref IntPtr ptrCfg, String strTemp, String m_strInputXml, int OutBufferLen)
        {
            byte[] m_szInputBuffer = new byte[strTemp.Length];
            m_szUrl = Encoding.Default.GetBytes(strTemp);
            struPTXML.pRequestUrl = Marshal.AllocHGlobal(300);
            Marshal.Copy(m_szUrl, 0, struPTXML.pRequestUrl, m_szUrl.Length);
            struPTXML.dwRequestUrlLen = (uint)m_szUrl.Length;
            strTemp = m_strInputXml;
            if ("" == strTemp)
            {
                struPTXML.pInBuffer = IntPtr.Zero;
                struPTXML.dwInSize = 0;
            }
            else
            {
                m_szInputBuffer = Encoding.UTF8.GetBytes(strTemp);
                struPTXML.pInBuffer = Marshal.AllocHGlobal(strTemp.Length);
                Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                struPTXML.dwInSize = (uint)m_szInputBuffer.Length;
            }
            struPTXML.pOutBuffer = Marshal.AllocHGlobal(OutBufferLen);
            for (int i = 0; i < OutBufferLen; i++)
            {
                Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
            }
            struPTXML.dwOutSize = (uint)OutBufferLen;
            struPTXML.byRes = new byte[32];
            try
            {
                ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                Marshal.StructureToPtr(struPTXML, ptrCfg, false);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Memory Exception");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
        }

        private void btnFingerGet_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + textBoxEmployeeNo.Text + "\"}]}}";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 5*1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Employee Not Existed");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);

            String searchID = "1";
            strTemp = "/ISAPI/AccessControl/FingerPrintUpload?format=json";
            m_strInputXml = "{\"FingerPrintCond\":{\"searchID\":\"" + searchID + "\",\"employeeNo\":\"" + textBoxEmployeeNo.Text +
                "\",\"cardReaderNo\":" + textBoxCardReadNo.Text +
                ",\"fingerPrintID\":" + textBoxFingerPrintNo.Text + "}}";
            OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            string strOutXML = string.Empty;
            string fingerData = string.Empty;
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            FPInfo fpInfo = JsonConvert.DeserializeObject<FPInfo>(strOutXML);

            if (strOutXML != string.Empty)
            {
                if (fpInfo.FingerPrintInfo.status.Equals("OK"))
                {
                    int FirstIndex = strOutXML.LastIndexOf("Data\"");
                    String FirstStr = "Data\":\t\"";
                    string LastStr = "\"\n\t\t\t}]";
                    int LastIndex = strOutXML.LastIndexOf("\"\n\t\t\t}]");
                    int SubLen = LastIndex - FirstIndex - LastStr.Length - 1;
                    fingerData = strOutXML.Substring(FirstIndex + FirstStr.Length, SubLen);
                    WriteFingerData(fingerData);
                }
                else if (fpInfo.FingerPrintInfo.status.Equals("NoFP"))
                {
                    MessageBox.Show("This Employee doesn't input FingerPrint Info");
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            //Check EmployeeNo Exist or not
            strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + textBoxEmployeeNo.Text + "\"}]}}";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen =  1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);


            strTemp = "/ISAPI/AccessControl/FingerPrintDownload?format=json";
            string FingerData = string.Empty;

            string FPPath = textBoxFingerData.Text;
            if (File.Exists(FPPath))
            {
                FileStream fs = File.OpenRead(FPPath); // OpenRead
                int filelen = 0;
                filelen = (int)fs.Length;
                byte[] byteFp = new byte[filelen];
                fs.Read(byteFp, 0, filelen);
                fs.Close();
                try
                {
                    FingerData = System.Text.Encoding.Default.GetString(byteFp);
                }
                catch
                {
                    FingerData = null;
                }
            }
            string FingerID = textBoxFingerPrintNo.Text;
            m_strInputXml = "{\"FingerPrintCfg\":{\"employeeNo\":\"" + textBoxEmployeeNo.Text +
          "\",\"enableCardReader\":[" + textBoxCardReadNo.Text + "],\"fingerPrintID\":" + int.Parse(FingerID) +
          ",\"fingerType\":\"normalFP\",\"fingerData\":\"" + FingerData + "\"}}";
            OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            string strOutXML = string.Empty;
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (rs.statusCode == 1)
                {
                    MessageBox.Show("FingerPrint SetUP Success");
                }
                else
                {
                    MessageBox.Show("FingerPrint SetUp Error: " + rs.statusString);
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //delete FingerPrint Data
        private void btnDelete_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + textBoxEmployeeNo.Text + "\"}]}}";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);

            strTemp = "/ISAPI/AccessControl/FingerPrint/Delete?format=json";
            m_strInputXml = "{\"FingerPrintDelete\":{\"mode\":\"byEmployeeNo\",\"EmployeeNoDetail\":{\"employeeNo\":\"" + textBoxEmployeeNo.Text +
            "\",\"enableCardReader\":[" + textBoxCardReadNo.Text + "],\"fingerPrintID\":[" + textBoxFingerPrintNo.Text + "]}}}";

            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);

            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = "";
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (rs.statusString.Equals("OK"))
                {
                    MessageBox.Show("Delete FPInfo Success");
                }
                else
                {
                    MessageBox.Show("FingerPrint Delete Process Failed");
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
      
        public void WriteFingerData(string FingerData)
        {
            string szPath = textBoxFingerData.Text;
            byte[] WriteData = System.Text.Encoding.Default.GetBytes(FingerData);
            try
            {
                using (FileStream fs = new FileStream(szPath, FileMode.Truncate, FileAccess.ReadWrite))
                {
                    fs.Write(WriteData, 0, WriteData.Length);
                    fs.Close();
                }
                textBoxFingerData.Text = szPath;
                MessageBox.Show("Store Finger Data Success");
            }
            catch
            {
                MessageBox.Show("Write Finger Print Process Failed");
            }
        }
        //read Finger Print Info From Hard Disk
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Fingerprint file|*.dat|All documents|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFingerData.Text = openFileDialog.FileName;
            }
        }
        //Capture FingerPrint
        private void BtnCapture_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/CaptureFingerPrint";
            m_strInputXml = "<CaptureFingerPrintCond xmlns=\"http://www.isapi.org/ver20/XMLSchema\" version=\"2.0\" ><fingerNo>" + textBoxFingerPrintNo.Text + "</fingerNo></CaptureFingerPrintCond>";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 10*1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            string szFingerData = string.Empty;

            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            string strOutXML = string.Empty;
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            string FirstStr = "<fingerData>";
            string LastStr = "</fingerData";
            int FirstIndex = strOutXML.IndexOf("<fingerData>");
            int LastIndex = strOutXML.IndexOf("</fingerData>");
            int SubLen = LastIndex - FirstIndex - LastStr.Length;
            if (SubLen < 0)
            {
                MessageBox.Show("Capture Invalid FingerPrint");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            string fingerData = strOutXML.Substring(FirstIndex + FirstStr.Length, SubLen);
            if (fingerData != string.Empty)
            {
                CapFingerData(fingerData);
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        private void CapFingerData(string fingerData)
        {
            string szPath = null;

            szPath = textBoxFingerData.Text;

            byte[] byfingerData = System.Text.Encoding.Default.GetBytes(fingerData);
            try
            {
                using (FileStream fs = new FileStream(szPath, FileMode.Truncate, FileAccess.ReadWrite))
                {
                    fs.Write(byfingerData, 0, byfingerData.Length);
                    fs.Close();
                }
                MessageBox.Show("FingerPrint Cap SUCCEED", "SUCCEED", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("CapFingerprint process failed");
            }
        }
        private void BtnScan_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Fingerprint file|*.dat|All documents|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFingerData.Text = openFileDialog.FileName;
            }
        }
    }
}
