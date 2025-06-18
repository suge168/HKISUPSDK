using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ISUPDemo.Public;
namespace ISUPDemo
{
    public partial class EhomeV50Auth : Form
    {
        public EhomeV50Auth()
        {
            InitializeComponent();
        }
        public EhomeV50Auth(int index)
        {
            InitializeComponent();
            m_iDevID = index;
            if (m_iDevID >= 0)
            {
                temp = DeviceTree.ByteToStr(ref DeviceTree.g_struDeviceInfo[m_iDevID].byDeviceID, 256);
                textBoxDeviceID.Text = temp;
            }
        }
        //Newly Add Content
        public String AuthDevIP;
        public Int16 AuthDevPort;

        public int m_iDevID = -1;
        public string temp = null;
      
        private void button1_Click(object sender, EventArgs e)
        {
            if(""==textBoxDeviceID.Text.ToString()||""==textBoxEhomeKey.Text.ToString())
            {
                MessageBox.Show("Please enter key and ID");
                return;
            }
            if(m_iDevID>=0)
            {
                string strEhomeKey = textBoxEhomeKey.Text.ToString();
                int EhomeKeyLength = strEhomeKey.Length;
                byte[] byEhomeKey = new byte[EhomeKeyLength];
                CleanUpBytearray(ref DeviceTree.g_struDeviceInfo[m_iDevID].byEhomeKey, 32);
                DeviceTree.StrToByteArray(ref byEhomeKey, strEhomeKey);
                Array.Copy(byEhomeKey, 0, DeviceTree.g_struDeviceInfo[m_iDevID].byEhomeKey, 0, EhomeKeyLength);

                if(ISUPDemo.g_bSS_Enable)
                {
                    IntPtr ptrDeviceID = Marshal.AllocHGlobal(256);
                    Marshal.Copy(DeviceTree.g_struDeviceInfo[m_iDevID].byDeviceID, 0, ptrDeviceID, 256);

                    IntPtr ptrbyEhomeKey = Marshal.AllocHGlobal(32);
                    Marshal.Copy(DeviceTree.g_struDeviceInfo[m_iDevID].byEhomeKey, 0, ptrbyEhomeKey, 32);

                    //IntPtr ptrClouldSecretKey = Marshal.AllocHGlobal(64);
                    byte[] ClouldSecretKey = new byte[256];
                    if (HCEHomeSS.NET_ESS_HAMSHA256(ptrDeviceID, ptrbyEhomeKey, ClouldSecretKey, 255))
                    {
                        //Marshal.Copy(ptrClouldSecretKey, DeviceTree.g_struDeviceInfo[m_iDevID].byClouldSecretKey, 0, 64);
                        Array.Copy(ClouldSecretKey, DeviceTree.g_struDeviceInfo[m_iDevID].byClouldSecretKey, 64);
                    }
                    Marshal.FreeHGlobal(ptrDeviceID);
                    Marshal.FreeHGlobal(ptrbyEhomeKey);
                    //Marshal.FreeHGlobal(ptrClouldSecretKey);
                }
            }
            else
            {
                GlobalDefinition.LOCAL_DEVICE_INFO lpTemp = new GlobalDefinition.LOCAL_DEVICE_INFO();
                lpTemp.Init();
                lpTemp.dwVersion = 5;
                byte[] DevID = System.Text.Encoding.Default.GetBytes(textBoxDeviceID.Text.ToString());
                int IDLength = DevID.Length;
                if (IDLength > 256)
                {
                    MessageBox.Show("DevID length is exceeding 256");
                }
                Array.Copy(DevID, lpTemp.byDeviceID, IDLength);

                byte[] EhomeKey = System.Text.Encoding.Default.GetBytes(textBoxEhomeKey.Text.ToString());
                int EhomeLength = EhomeKey.Length;
                if (EhomeLength > 32)
                {
                    MessageBox.Show("EhomeKey length is exceeding 32");
                }
                Array.Copy(EhomeKey, lpTemp.byEhomeKey, EhomeLength);
                int dwSize = Marshal.SizeOf(typeof(GlobalDefinition.LOCAL_DEVICE_INFO));
                IntPtr ptrtemp = Marshal.AllocHGlobal(dwSize);
                Marshal.StructureToPtr(lpTemp, ptrtemp, false);
                Message mes = new Message();
                mes.Msg = ISUPDemo.WM_ADD_DEV;
                mes.LParam = ptrtemp;
                g_deviceTree.ProDevStatu(mes);

                //PostMessage(ISUPDemo.m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrtemp, ptrtemp);
            }
            this.DialogResult = DialogResult.OK;
        }
        private DeviceTree g_deviceTree = DeviceTree.Instance();

        public void CleanUpBytearray(ref byte[] source,int len)
        {
            for (int i = 0; i < len; ++i)
            {
                source[i] = 0;
            }
        }
        //消息发送API  
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }
}
