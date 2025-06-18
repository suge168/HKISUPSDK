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
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml;

namespace ISUPDemo.UI
{
    public partial class CMSAlarm : Form
    {
        #region "dll region"
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        #endregion
        public bool m_bUseAccessList = false;
        public HCEHomeAlarm.EHomeMsgCallBack ehomeMsgCallback = null;
        public HCEHomePublic.NET_EHOME_LOCAL_ACCESS_SECURITY m_struAccessSecure = new HCEHomePublic.NET_EHOME_LOCAL_ACCESS_SECURITY();
        private DeviceLogList g_formList = DeviceLogList.Instance();
        
        public HCEHomeCMS.NET_EHOME_SERVER_INFO_V50 m_struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO_V50();

        public HCEHomeAlarm.EHomeMsgCallBack ptrTcpAlarm = null;
        public HCEHomeAlarm.EHomeMsgCallBack ptrMqtt = null;
        public int m_lUdpAlarmHandle;
        public int m_lTcpAlarmHandle;
        public static int m_lEhome50AlarmHandle = -1;

        public HCEHomePublic.NET_EHOME_AMS_ADDRESS m_struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
        private delegate void UpdatePictureBoxControl(List<string> AlarmPath);


        private static CMSAlarm g_AlarmInfoList = new CMSAlarm();

        public String m_csLocalIP = string.Empty;
        public ushort m_wAlarmServerMqttPort = 0;
        public static ACCESS_DEVICE_INFO[] m_stAccessDeviceList = new ACCESS_DEVICE_INFO[64];
        private IntPtr m_logListHandle;
        public struct ACCESS_DEVICE_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.U1)]
            public byte[] sSerialNumber; //设备序列号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U1)]
            public byte[] sIdentifyCode; //设备验证码
        }

     
        public static PictureBox VisibleBox = new PictureBox();
        public static PictureBox ThemalBox = new PictureBox();
        public static RichTextBox AlarmInfoBox = new RichTextBox();
        public static RichTextBox ISAPIAlarmBox = new RichTextBox();



        public CMSAlarm()
        {
            InitializeComponent();
            m_logListHandle = g_formList.Handle;
            VisibleBox = CmsPic;
            AlarmInfoBox = AlarmBox;
            ISAPIAlarmBox = TemperatureInfo;
            ThemalBox = ThemalPicBox;
        }

        public static CMSAlarm Instance()
        {
            return g_AlarmInfoList;
        }

       
        public static void ReadImageFromLocal(string AlarmPath)
        {
            VisibleBox.Image = Image.FromFile(AlarmPath);
        }

         public static void ReadThemalImageFromLocal(string AlarmPath)
        {
            ThemalBox.Image = Image.FromFile(AlarmPath);
        }


        public static void ShowCmsAlarmInfo(ref string AlarmInfo)
        {
            if (AlarmInfoBox.Text != null)
            {
                AlarmInfoBox.Clear();
                AlarmInfoBox.Text = AlarmInfo;
            }
        }
         public static void ShowISAPIAlarmInfo(ref string AlarmInfo)
        {
            if (AlarmInfoBox.Text != null)
            {
                ISAPIAlarmBox.Clear();
                ISAPIAlarmBox.Text = AlarmInfo;
            }
        }
        
        //Close CMs Alarm
        private void CloseAlarmBtn_Click(object sender, EventArgs e)
        {
            if (-1 != m_lEhome50AlarmHandle)
            {
                if (HCEHomeAlarm.NET_EALARM_StopListen(m_lEhome50AlarmHandle) == false)
                {
                    string str = string.Format("NET_EALARM_StopListen err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "NET_EALARM_StopListen Failed");
                }
                else
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_EALARM_StopListen Success");  
                }
                m_lEhome50AlarmHandle = -1;
            }
            m_struAmsAddr.dwSize = Marshal.SizeOf(m_struAmsAddr);
            m_struAmsAddr.byEnable = 0;

            IntPtr struAmsAddrPtr = Marshal.AllocHGlobal(m_struAmsAddr.dwSize);

            if (!HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, struAmsAddrPtr))
            {
                string str = string.Format("NET_ECMS_SetSDKLocalCfg err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, str);
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_SetSDKLocalCfg Succ");
            }
        }
        
    }
}
