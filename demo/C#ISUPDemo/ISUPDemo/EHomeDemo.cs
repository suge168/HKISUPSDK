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
using System.Threading;
using System.Xml;

using TINYXMLTRANS;
using EHomeDemo.Public;
using System.Globalization;

namespace EHomeDemo
{
    public partial class EHomeDemo : Form
    {

        #region "dll region"
        //消息发送API  
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        #endregion

        #region "member varient region"
        private String m_csLocalIP; //本地IP地址
        private Int16  m_nPort;     //端口号
        public int m_lPort;
        public IntPtr m_hWnd;
        public IntPtr m_hPreviewWnd;   //预览窗口句柄
        public bool m_bPlay;
        public int m_iPlayHandle;
        public int m_iSessionID = -1;
        public int m_iWndIndex;
        public int m_iLoginID = -1;
        public int m_iHandle = -1;
        public int m_iChanelType;
        public int m_iLinkHandle = -1;
        public int m_iRealHandle = -1;

        int m_iCurWndNum;    //screen split mode 1, 4, 9, 16, 25, 36
        int m_iCurWndIndex; //current selected split window index, start from 0

        public bool m_bListLogEnlarge;
        public bool m_bListAlarmEnlarge;
        public int m_iMainType;
        public int m_lAlarmHandle;
        public int m_lCmsAlarm;
        public int m_lUdpAlarmHandle;
        public bool m_bUseAccessList = false;
        public bool m_bAlarmed = false;
        public IntPtr m_treeHandle;
        public int m_count1 = 0;
        //自定义demo 消息
        public const int WM_ADD_LOG = 0x401;                      //add log 
        public const int WM_ADD_DEV = 78;                         //add device
        public const int WM_LISTENED_ALARM = 79; 
        public const int WM_DEL_DEV = 0x403;                      //delete device
        public const int WM_CHANGE_CHANNEL_ITEM_IMAGE = 0x404;    //change channel node icon
        public const int WM_PROC_EXCEPTION = 0x405;               //process exception
        public const int WM_CHANGE_IP = 0x406;                    //ip address changed

        public delegate bool rmMsgCallBack(long lHandle, ref HCEHomeAlarm.NET_EHOME_ALARM_MSG pAlarmMsg, IntPtr pUser);
        public HCEHomePublic.NET_EHOME_AMS_ADDRESS m_struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
        public HCEHomePublic.NET_EHOME_LOCAL_ACCESS_SECURITY m_struAccessSecure = new HCEHomePublic.NET_EHOME_LOCAL_ACCESS_SECURITY();
        public HCEHomePublic.NET_EHOME_SEND_PARAM m_struSendParam = new HCEHomePublic.NET_EHOME_SEND_PARAM();
        public HCEHomeCMS.NET_EHOME_CMS_LISTEN_PARAM m_struCMSListenPara = new HCEHomeCMS.NET_EHOME_CMS_LISTEN_PARAM();
        public HCEHomeCMS.NET_EHOME_SERVER_INFO m_struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO();
        public Public.ConvertModel m_ConvertModel = new Public.ConvertModel();
        public ACCESS_DEVICE_INFO[] m_stAccessDeviceList = new ACCESS_DEVICE_INFO[64];
        private static HCEHomeStream.PREVIEW_DATA_CB fnPREVIEW_DATA_CB_Func = new HCEHomeStream.PREVIEW_DATA_CB(fnPREVIEW_DATA_CB);
        private HCEHomeStream.PREVIEW_NEWLINK_CB fnPREVIEW_NEWLINK_CB_Func = null;
        private HCEHomeAlarm.EHomeMsgCallBack AlarmMsgCallBack_Func = null;
        private HCEHomeCMS.DEVICE_REGISTER_CB EHOME_REGISTER_Func = null;

        private Thread m_hAlarmListenCB;
        private AlarmListenCB m_AlarmListenCB = new AlarmListenCB();

        private int m_count = 0;

        //日志实例
        private DeviceLogList g_formList = DeviceLogList.Instance();
        //设备树实例
        private DeviceTree g_deviceTree = DeviceTree.Instance();

        private AudioTalk g_AudioTalk = new AudioTalk();

        private IntPtr m_logListHandle;

        #endregion

        #region "struct define region"
        //设备可接入信息
        public struct ACCESS_DEVICE_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.U1)]
            public byte[] sSerialNumber; //设备序列号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U1)]
            public byte[] sIdentifyCode; //设备验证码
        }
        public enum NET_EHOME_LOCAL_CFG_TYPE
        {
            UNDEFINE = -1,              //暂时没有具体的定义
            ACTIVE_ACCESS_SECURITY = 0, //设备主动接入的安全性
            AMS_ADDRESS,                //报警服务器本地回环地址
            SEND_PARAM,                 //发送参数配置
        }
        #endregion

        public EHomeDemo()
        {

            SelectIP ipDlg = new SelectIP();
            DialogResult nResponse = ipDlg.ShowDialog();
            if (nResponse != DialogResult.OK)
            {
                ipDlg.Hide();
                ipDlg.Close();
                return;
            }
            InitializeComponent();
            g_formList = DeviceLogList.Instance();
            String csIP;
            String csPort;
            csIP = ipDlg.m_cmbLocalIP.Text.ToString();
            csPort = ipDlg.m_textPort.Text.ToString();
            SetAddr(csIP, Convert.ToInt16(csPort));
            m_iChanelType = HCEHomeCMS.DEMO_CHANNEL_TYPE_ZERO;
            m_hWnd = this.Handle;
            m_hPreviewWnd = m_previewWnd.Handle;
            m_treeHandle = g_deviceTree.Handle;
            m_rdoLocalLog.Select();

            for (int i = 0; i < GlobalDefinition.MAX_DEVICES; i++)
            {
                DeviceTree.g_struDeviceInfo[i].Init();
                for (int j = 0; j < GlobalDefinition.MAX_CHAN_NUM_DEMO; j++)
                {
                    DeviceTree.g_struDeviceInfo[i].struChanInfo[j].Init();
                }
            }

            //日志列表添加到面板
            this.m_panelDeviceLog.Controls.Add(g_formList);
            //把设备树添加到面板
            this.m_panelDeviceTree.Controls.Add(g_deviceTree);
            g_formList.Dock = DockStyle.Fill;
            g_deviceTree.Dock = DockStyle.Fill;
            m_logListHandle = g_formList.Handle;

            this.m_previewPanelOne.BackColor = Color.DarkGray;
            this.m_previewPanelTwo.BackColor = Color.DarkGray;
            this.m_previewPanelThree.BackColor = Color.DarkGray;
            this.m_previewPanelFour.BackColor = Color.DarkGray;
            g_deviceTree.m_CurPreviewPanel[0] = this.m_previewPanelOne;
            g_deviceTree.m_CurPreviewPanel[1] = this.m_previewPanelTwo;
            g_deviceTree.m_CurPreviewPanel[2] = this.m_previewPanelThree;
            g_deviceTree.m_CurPreviewPanel[3] = this.m_previewPanelFour;

            DeviceTree.m_cslocalIP = m_csLocalIP;
            DeviceTree.m_lport = m_nPort;
            AudioTalk.m_csIPAddr = m_csLocalIP;
            AudioTalk.m_lPort = m_nPort;

            m_struServInfo.Init();

            InitParamFromXML();
            InitChildWindow();
            InitLib();
            InitPreviewListenParam();
        }

        #region "member function region"
        public void StopPreviewListen()
        {
            int i = 0;
            for (i = 0; i < GlobalDefinition.MAX_LISTEN_NUM; i++)
            {
                if (g_struPreviewListen[i].lHandle != -1)
                {
                    HCEHomeStream.NET_ESTREAM_StopListenPreview(g_struPreviewListen[i].lHandle);
                    g_struPreviewListen[i].lHandle = -1;
                }
            }
        }

        public void InitParamFromXML()
        {
            String strTemp;
            bool bLoaded = false;

            CTinyXmlTrans xmlLocalInfo = new CTinyXmlTrans();
             bLoaded = xmlLocalInfo.LoadFile("EHomeDemo.xml");
             if (!bLoaded)
             {
                 return;
             }
            if (xmlLocalInfo.FindElem("LocalCfg") && xmlLocalInfo.IntoElem())
            {
                if (xmlLocalInfo.FindElem("Param") && xmlLocalInfo.IntoElem())
                {
                    if (xmlLocalInfo.FindElem("KeepAliveSeconds"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.dwKeepAliveSec = Convert.ToInt32(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("AlarmServerIP"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        strTemp.CopyTo(0, m_struServInfo.struTCPAlarmSever.szIP, 0, strTemp.Length);
                        strTemp.CopyTo(0, m_struServInfo.struUDPAlarmSever.szIP, 0, strTemp.Length);
                    }
                    if (xmlLocalInfo.FindElem("AlarmServerPort"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.struUDPAlarmSever.wPort = Convert.ToInt16(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("AlarmServerType"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.dwAlarmServerType = Convert.ToInt32(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("AlarmServerTcpPort"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.struTCPAlarmSever.wPort = Convert.ToInt16(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("NTPServerIP"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        strTemp.CopyTo(0, m_struServInfo.struNTPSever.szIP, 0, strTemp.Length);
                    }
                    if (xmlLocalInfo.FindElem("NTPServerPort"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.struNTPSever.wPort = Convert.ToInt16(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("NTPInterval"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.dwNTPInterval = Convert.ToInt32(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("PictureServer"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        strTemp.CopyTo(0, m_struServInfo.struPictureSever.szIP, 0, strTemp.Length);
                    }
                    if (xmlLocalInfo.FindElem("PictureServerPort"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_struServInfo.struPictureSever.wPort = Convert.ToInt16(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("CmsAccessSecurity"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_byCmsSecureAccessType = Convert.ToByte(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("AlarmAccessSecurity"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_byAlarmSecureAccessType = Convert.ToByte(strTemp);
                    }
                    if (xmlLocalInfo.FindElem("StreamAccessSecurity"))
                    {

                        strTemp = xmlLocalInfo.GetData();
                        m_byStreamSecureAccessType = Convert.ToByte(strTemp);
                    }
                    xmlLocalInfo.OutOfElem();
                }
                xmlLocalInfo.OutOfElem();
            }
        }
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == GlobalDefinition.WM_PROC_EXCEPTION)
            {
                int iHandle = (int)Marshal.PtrToStructure(m.LParam, typeof(int));
                if (!HCEHomeStream.NET_ESTREAM_StopPreview(iHandle))
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, "OnWMProcException NET_ESTREAM_StopPreview failed");
                }
            }
            base.DefWndProc(ref m);
        }

        public void fnPREVIEW_EXCEPTION_CB(int dwType, int iUserID, int iHandle, IntPtr pUser)
        {
            if (HCEHomeStream.EHOME_PREVIEW_EXCEPTION == dwType)
            {
                String str = string.Format("Preview exception, handle={0}", iHandle);
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
                IntPtr pUserId = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
                IntPtr pHandle = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
                Marshal.StructureToPtr(iUserID, pUserId, false);
                PostMessage(m_hWnd, GlobalDefinition.WM_PROC_EXCEPTION, pUserId, pHandle);
            }
        }
        public void StartPreviewListen()
        {
            HCEHomeStream.NET_ESTREAM_SetExceptionCallBack(0, 0, fnPREVIEW_EXCEPTION_CB, m_hWnd);
        }

        public void InitChildWindow()
        {
            for (int i = 0; i < GlobalDefinition.MAX_OUTPUTS; i++)
            {
                DeviceTree.m_PreviewPanel[i].m_cslocalIP = m_csLocalIP;
                DeviceTree.m_PreviewPanel[i].m_nPort = m_nPort;
            }
            DeviceTree.m_PreviewPanel[0].m_hWnd = this.m_previewPanelOne.Handle;
            DeviceTree.m_PreviewPanel[1].m_hWnd = this.m_previewPanelTwo.Handle;
            DeviceTree.m_PreviewPanel[2].m_hWnd = this.m_previewPanelThree.Handle;
            DeviceTree.m_PreviewPanel[3].m_hWnd = this.m_previewPanelFour.Handle;
        }


        //初始化库
        public void InitLib()
        {
            bool bRet = false;
            int err = 0;
            String csLogPath = "C:/EHomeSdkLog/";
            char[] szLogPath = new char[128];
            csLogPath.CopyTo(0, szLogPath, 0, csLogPath.Length);

            //报警库初始化
            bRet = HCEHomeAlarm.NET_EALARM_Init();
            m_struAccessSecure.dwSize = (UInt32)Marshal.SizeOf(m_struAccessSecure);
            m_struAccessSecure.byAccessSecurity = m_byAlarmSecureAccessType;
            IntPtr ptrAccessSecure = Marshal.AllocHGlobal((int)m_struAccessSecure.dwSize);
            Marshal.StructureToPtr(m_struAccessSecure, ptrAccessSecure, false);

            if (!HCEHomeAlarm.NET_EALARM_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.ACTIVE_ACCESS_SECURITY, ptrAccessSecure))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, "NET_EALARM_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Failed");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_EALARM_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Success");
            }

            HCEHomeAlarm.NET_EHOME_ALARM_LISTEN_PARAM struAlarmListenParam = new HCEHomeAlarm.NET_EHOME_ALARM_LISTEN_PARAM();
            struAlarmListenParam.struAddress.Init();
            m_csLocalIP.CopyTo(0, struAlarmListenParam.struAddress.szIP, 0, m_csLocalIP.Length);
            struAlarmListenParam.struAddress.wPort =  7331;
            AlarmMsgCallBack_Func = new HCEHomeAlarm.EHomeMsgCallBack(AlarmMsgCallBack);
            struAlarmListenParam.fnMsgCb = AlarmMsgCallBack_Func;
            struAlarmListenParam.pUserData = m_AlarmListenCB;
            struAlarmListenParam.byProtocolType = 1;
            struAlarmListenParam.byUseThreadPool = 0;

            m_lUdpAlarmHandle = HCEHomeAlarm.NET_EALARM_StartListen(ref struAlarmListenParam);

            struAlarmListenParam.struAddress.wPort =  7332;
            struAlarmListenParam.fnMsgCb = AlarmMsgCallBack_Func;
            struAlarmListenParam.pUserData = m_AlarmListenCB;// this.Handle;
            struAlarmListenParam.byProtocolType = 0;
            struAlarmListenParam.byUseThreadPool = 1;

            m_lAlarmHandle = HCEHomeAlarm.NET_EALARM_StartListen(ref struAlarmListenParam);

            struAlarmListenParam.struAddress.wPort =  7333;
            struAlarmListenParam.fnMsgCb = AlarmMsgCallBack_Func;
            struAlarmListenParam.pUserData = m_AlarmListenCB;// this.Handle;
            struAlarmListenParam.byUseCmsPort = 1;
            struAlarmListenParam.byUseThreadPool = 1;
            int lCmsAlarm = HCEHomeAlarm.NET_EALARM_StartListen(ref struAlarmListenParam);

            if (-1 == lCmsAlarm)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "NET_EALARM_StartListen Failed, port7333");
            }
            else
            {
                m_lCmsAlarm = lCmsAlarm;
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 3, "NET_EALARM_StartListen succ port7333");
            }

            HCEHomeAlarm.NET_EALARM_SetLogToFile(3, szLogPath, true);
            HCEHomeAlarm.NET_EALARM_GetBuildVersion();


            //初始化库
            bRet = HCEHomeCMS.NET_ECMS_Init();
            if (!bRet)
            {
                String str = string.Format("{0}", (int)HCEHomeCMS.NET_ECMS_GetLastError());
                Console.WriteLine("NET_ECMS_Init failed, err={0}", (int)HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_Init Failed");
            }

            csLogPath.CopyTo(0, szLogPath, 0, csLogPath.Length);
            bRet = HCEHomeCMS.NET_ECMS_SetLogToFile(3, szLogPath, true);

            m_struAccessSecure.byAccessSecurity = m_byCmsSecureAccessType;
            Marshal.StructureToPtr(m_struAccessSecure, ptrAccessSecure, false);
            if (!HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.ACTIVE_ACCESS_SECURITY, ptrAccessSecure))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Failed");
                Marshal.FreeHGlobal(ptrAccessSecure);
                return;
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Succeed");
            }


            m_struAmsAddr.struAddress.Init();
            m_csLocalIP.CopyTo(0, m_struAmsAddr.struAddress.szIP, 0, m_csLocalIP.Length);
            m_struAmsAddr.struAddress.wPort = (Int16)7333;
            m_struAmsAddr.byEnable = 1;
            m_struAmsAddr.dwSize = (Int32)Marshal.SizeOf(m_struAmsAddr);
            IntPtr ptrAmsAddr = Marshal.AllocHGlobal((int)m_struAmsAddr.dwSize);
            Marshal.StructureToPtr(m_struAmsAddr, ptrAmsAddr, false);

            //CMS开始接收
            if (!HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, ptrAmsAddr))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "open cms-alarm Failed 7333");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "open cms-alarm  Succ 7333");
            }
            Marshal.FreeHGlobal(ptrAmsAddr);


            m_struSendParam.dwRecvTimeOut = 0;
            m_struSendParam.bySendTimes = 1;
            m_struSendParam.byRes2 = new byte[127];
            m_struSendParam.dwSize = (Int32)Marshal.SizeOf(m_struSendParam);
            IntPtr ptrSendParam = Marshal.AllocHGlobal(m_struSendParam.dwSize);
            Marshal.StructureToPtr(m_struSendParam, ptrSendParam, false);
            //CMS开始接收
            if (!HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.SEND_PARAM, ptrSendParam))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_SetSDKLocalCfg SEND_PARAM failed");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_SetSDKLocalCfg SEND_PARAM succ");
            }
            Marshal.FreeHGlobal(ptrSendParam);

            //开启监听
            m_struCMSListenPara.struAddress.Init();
            m_csLocalIP.CopyTo(0, m_struCMSListenPara.struAddress.szIP, 0, m_csLocalIP.Length);
            m_struCMSListenPara.struAddress.wPort = m_nPort;
            EHOME_REGISTER_Func = new HCEHomeCMS.DEVICE_REGISTER_CB(EHOME_REGISTER);
            m_struCMSListenPara.fnCB = EHOME_REGISTER_Func;
            m_struCMSListenPara.pUserData = this.Handle;
            m_struCMSListenPara.byRes = new byte[32];
            Int32 dwSize = (Int32)Marshal.SizeOf(m_struCMSListenPara);
            IntPtr ptrCMSListenPara = Marshal.AllocHGlobal(dwSize);
            Marshal.StructureToPtr(m_struCMSListenPara, ptrCMSListenPara, false);
            int iListen = -1;
            iListen = HCEHomeCMS.NET_ECMS_StartListen(ref m_struCMSListenPara);
            Marshal.FreeHGlobal(ptrCMSListenPara);
            if (iListen == -1)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_StartListen Failed");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_StartListen Succ");
            }
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);  //注册成功之后再做后续操作
            //    if (m_iLoginID >= 0)
            //    {
            //        break;
            //    }
            //}
            HCEHomeCMS.NET_ECMS_GetBuildVersion();

            //取流库初始化
            bRet = HCEHomeStream.NET_ESTREAM_Init();
            bRet = HCEHomeStream.NET_ESTREAM_SetLogToFile(3, "c:/EHomeSdkLog/", true);
            m_struAccessSecure.byAccessSecurity = m_byStreamSecureAccessType;
            if (!HCEHomeStream.NET_ESTREAM_SetSDKLocalCfg(HCNetSDK.NET_EHOME_LOCAL_CFG_TYPE.ACTIVE_ACCESS_SECURITY, ptrAccessSecure))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ESTREAM_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Success");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ESTREAM_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Success");
            }
            Marshal.FreeHGlobal(ptrAccessSecure);
            HCEHomeStream.NET_ESTREAM_GetBuildVersion();
        }

        public void SetAddr(String csIP, Int16 nport)
        {
            m_csLocalIP = csIP;
            m_nPort = nport;
        }


        //初始化预览监听
        public void InitPreviewListenParam()
        {
            int i = 0;
            for (i = 0; i < GlobalDefinition.MAX_LISTEN_NUM; i++)
            {
                g_struPreviewListen[i].lHandle = -1;
                g_struPreviewListen[i].struIP.Init();
                m_csLocalIP.CopyTo(0, g_struPreviewListen[i].struIP.szIP, 0, m_csLocalIP.Length);
            }

            g_struPreviewListen[0].struIP.wPort = 8000;
            g_struPreviewListen[0].iLinkType = 0;
            fnPREVIEW_NEWLINK_CB_Func = new HCEHomeStream.PREVIEW_NEWLINK_CB(fnPREVIEW_NEWLINK_CB);
            HCEHomeStream.NET_EHOME_LISTEN_PREVIEW_CFG struListen = new HCEHomeStream.NET_EHOME_LISTEN_PREVIEW_CFG();
            struListen.struIPAdress.Init();
            struListen.struIPAdress = g_struPreviewListen[0].struIP;
            struListen.fnNewLinkCB = fnPREVIEW_NEWLINK_CB_Func;
            struListen.pUser = this.Handle;
            struListen.byLinkMode = 0;
            struListen.byRes = new byte[127];

            Int32 dwSize = (Int32)Marshal.SizeOf(struListen);
            //开启预览监听
            Int32 iHandle = HCEHomeStream.NET_ESTREAM_StartListenPreview(ref struListen);
            if (iHandle > -1)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ESTREAM_StartListenPreview Success");
                g_struPreviewListen[0].lHandle = iHandle;
                m_iHandle = iHandle;
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, "NET_ESTREAM_StartListenPreview Failed");
            }
        }

        #region "call back function"
        public bool EHOME_REGISTER(int iUserID, uint dwDataType, IntPtr pOutBuffer, uint dwOutLen, IntPtr pInBuffer, uint dwInLen, IntPtr pUser)
        {
            int dwSize = 0;
            GlobalDefinition.LOCAL_DEVICE_INFO struTemp = new GlobalDefinition.LOCAL_DEVICE_INFO();
            struTemp.Init();
            dwSize = Marshal.SizeOf(typeof(GlobalDefinition.LOCAL_DEVICE_INFO));
            IntPtr ptrTemp = Marshal.AllocHGlobal(dwSize);

            if (HCEHomeCMS.ENUM_DEV_ON == dwDataType)
            {
                if (pInBuffer == null)
                {
                    return false;
                }
                HCEHomeCMS.NET_EHOME_SERVER_INFO struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO();
                dwSize = Marshal.SizeOf(struServInfo);
                IntPtr ptrServInfo = pInBuffer;
                Marshal.StructureToPtr(m_struServInfo, ptrServInfo, false);
                

                //struServInfo = m_struServInfo;//memcpy(pServInfo, &pDlg->m_struServInfo, sizeof(pDlg->m_struServInfo));
                //pServInfo->dwKeepAliveSec = 15;//PU的保活间隔
                //LOCAL_DEVICE_INFO *lpTemp = new LOCAL_DEVICE_INFO();
                //sprintf(pServInfo->struUDPAlarmSever.szIP, "%s", pDlg->m_sLocalIP);
                //pServInfo->struUDPAlarmSever.wPort = 7331;
                //sprintf(pServInfo->struTCPAlarmSever.szIP, "%s", pDlg->m_sLocalIP);
                //pServInfo->struTCPAlarmSever.wPort = 7332;

                dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_DEV_REG_INFO));
                IntPtr ptrDevInfo = Marshal.AllocHGlobal(dwSize);

                HCEHomeCMS.NET_EHOME_DEV_REG_INFO struDevInfo = new HCEHomeCMS.NET_EHOME_DEV_REG_INFO();
                struDevInfo.Init();
                if (pOutBuffer != null)
                {
                    struDevInfo = (HCEHomeCMS.NET_EHOME_DEV_REG_INFO)Marshal.PtrToStructure(pOutBuffer, typeof(HCEHomeCMS.NET_EHOME_DEV_REG_INFO));
                }
                struDevInfo.byDeviceID.CopyTo(struTemp.byDeviceID, 0);
                struTemp.iLoginID = iUserID;
                struDevInfo.sDeviceSerial.CopyTo(struTemp.sDeviceSerial, 0);

                //先拷贝到一个大的缓冲区，在获取长度
                byte[] szDeviceSerial = new byte[HCEHomePublic.NET_EHOME_SERIAL_LEN + 1];
                struDevInfo.sDeviceSerial.CopyTo(szDeviceSerial, 0);
                if (szDeviceSerial.Length < 0)
                {
                    struTemp.dwVersion = 2;
                }
                else
                {
                    struTemp.dwVersion = 4;
                }


                //UTF-8转GBK
                int iOutLen = 0;
                m_ConvertModel.UTF82A(struDevInfo.byDeviceID, struDevInfo.byDeviceID, HCEHomePublic.MAX_DEVICE_ID_LEN, ref iOutLen);
                m_ConvertModel.UTF82A(struDevInfo.sDeviceSerial, struDevInfo.sDeviceSerial, HCEHomePublic.NET_EHOME_SERIAL_LEN, ref iOutLen);

                //在这里进行设备的验证，只有序列号在m_stAccessDeviceList中的设备才会允许注册
                if (m_bUseAccessList)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (struTemp.dwVersion >= 4)
                        {
                            if (struDevInfo.sDeviceSerial.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                                //允许注册
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                                PostMessage(m_treeHandle, EHomeDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                        else
                        {
                            if (struDevInfo.byDeviceID.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                                //允许注册
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                                PostMessage(m_treeHandle, EHomeDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    //允许注册
                    Marshal.StructureToPtr(struTemp, ptrTemp, false);
                    PostMessage(m_treeHandle, EHomeDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                    return true;
                }

                //不允许注册
                return false;
            }
            else if (HCEHomeCMS.ENUM_DEV_OFF == dwDataType)
            {
                //强制注销
                Marshal.StructureToPtr(iUserID, ptrTemp, false);
                PostMessage(m_treeHandle, EHomeDemo.WM_DEL_DEV, ptrTemp, ptrTemp);
                return false;
            }
            else if(HCEHomeCMS.ENUM_DEV_ADDRESS_CHANGED == dwDataType)
            {
                //删除设备
                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                PostMessage(m_treeHandle, EHomeDemo.WM_CHANGE_IP, ptrTemp, ptrTemp);

                //重新注册
                HCEHomeCMS.NET_EHOME_SERVER_INFO struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO();
                dwSize = Marshal.SizeOf(struServInfo);
                IntPtr ptrServInfo = pInBuffer;
                Marshal.StructureToPtr(m_struServInfo, ptrServInfo, false);


                //struServInfo = m_struServInfo;//memcpy(pServInfo, &pDlg->m_struServInfo, sizeof(pDlg->m_struServInfo));
                //pServInfo->dwKeepAliveSec = 15;//PU的保活间隔
                //LOCAL_DEVICE_INFO *lpTemp = new LOCAL_DEVICE_INFO();
                //sprintf(pServInfo->struUDPAlarmSever.szIP, "%s", pDlg->m_sLocalIP);
                //pServInfo->struUDPAlarmSever.wPort = 7331;
                //sprintf(pServInfo->struTCPAlarmSever.szIP, "%s", pDlg->m_sLocalIP);
                //pServInfo->struTCPAlarmSever.wPort = 7332;

                dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_DEV_REG_INFO));
                IntPtr ptrDevInfo = Marshal.AllocHGlobal(dwSize);

                HCEHomeCMS.NET_EHOME_DEV_REG_INFO struDevInfo = new HCEHomeCMS.NET_EHOME_DEV_REG_INFO();
                struDevInfo.Init();
                if (pOutBuffer != null)
                {
                    struDevInfo = (HCEHomeCMS.NET_EHOME_DEV_REG_INFO)Marshal.PtrToStructure(pOutBuffer, typeof(HCEHomeCMS.NET_EHOME_DEV_REG_INFO));
                }
                struDevInfo.byDeviceID.CopyTo(struTemp.byDeviceID, 0);
                struTemp.iLoginID = iUserID;
                struDevInfo.sDeviceSerial.CopyTo(struTemp.sDeviceSerial, 0);

                //先拷贝到一个大的缓冲区，在获取长度
                byte[] szDeviceSerial = new byte[HCEHomePublic.NET_EHOME_SERIAL_LEN + 1];
                struDevInfo.sDeviceSerial.CopyTo(szDeviceSerial, 0);
                if (szDeviceSerial.Length < 0)
                {
                    struTemp.dwVersion = 2;
                }
                else
                {
                    struTemp.dwVersion = 4;
                }


                //UTF-8转GBK
                int iOutLen = 0;
                m_ConvertModel.UTF82A(struDevInfo.byDeviceID, struDevInfo.byDeviceID, HCEHomePublic.MAX_DEVICE_ID_LEN, ref iOutLen);
                m_ConvertModel.UTF82A(struDevInfo.sDeviceSerial, struDevInfo.sDeviceSerial, HCEHomePublic.NET_EHOME_SERIAL_LEN, ref iOutLen);

                //在这里进行设备的验证，只有序列号在m_stAccessDeviceList中的设备才会允许注册
                if (m_bUseAccessList)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (struTemp.dwVersion >= 4)
                        {
                            if (struDevInfo.sDeviceSerial.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                                //允许注册
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                                PostMessage(m_treeHandle, EHomeDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                        else
                        {
                            if (struDevInfo.byDeviceID.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                                //允许注册
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                                PostMessage(m_treeHandle, EHomeDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    //允许注册
                    Marshal.StructureToPtr(struTemp, ptrTemp, false);
                    PostMessage(m_treeHandle, EHomeDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                    return true;
                }

                //不允许注册
                return false;
            }

            return true;
        }

        //预览连接请求回调函数;
        public bool fnPREVIEW_NEWLINK_CB(int iLinkHandle, ref HCEHomeStream.NET_EHOME_NEWLINK_CB_MSG pNewLinkCBMsg, IntPtr pUserData)
        {

            m_iLinkHandle = iLinkHandle;
            m_iPlayHandle = iLinkHandle;
            //需要将字符串字段转换成GB2312
            Int32 dwConvertLen = 0;
            HCEHomeStream.NET_EHOME_PREVIEW_DATA_CB_PARAM struDataCB = new HCEHomeStream.NET_EHOME_PREVIEW_DATA_CB_PARAM();
            struDataCB.fnPreviewDataCB = fnPREVIEW_DATA_CB_Func;
            struDataCB.pUserData = (IntPtr)m_iCurWndIndex;
            struDataCB.byRes = new byte[128];
            IntPtr ptrDataCB = Marshal.AllocHGlobal(Marshal.SizeOf(struDataCB));
            Marshal.StructureToPtr(struDataCB, ptrDataCB, false);
            if (!HCEHomeStream.NET_ESTREAM_SetPreviewDataCB(iLinkHandle, ptrDataCB))
            {
                Marshal.FreeHGlobal(ptrDataCB);
                return false;
            }
            Marshal.FreeHGlobal(ptrDataCB);
            return true;
        }

        //实时流数据回调函数
        public static void fnPREVIEW_DATA_CB(int iPreviewHandle, ref HCEHomeStream.NET_EHOME_PREVIEW_CB_MSG pPreviewCBMsg, IntPtr pUserData)
        {
            bool bRet = false;
            //m_iRealHandle = iPreviewHandle;
            int iWinIndex = (int)pUserData;

            //码流送到播放库进行播放
            DeviceTree.m_PreviewPanel[iWinIndex].InputStreamData(pPreviewCBMsg.byDataType, pPreviewCBMsg.pRecvdata, pPreviewCBMsg.dwDataLen);
        }

        public bool IsInAccessDeviceList(byte[] pSerialNumber)
        {
            if (pSerialNumber == null)
            {
                return false;
            }
            for (int i = 0; i < 64; i++)
            {
                if (pSerialNumber.Equals(m_stAccessDeviceList[i].sSerialNumber))
                {
                    return true;
                }
            }
            return false;
        }

       public bool AlarmMsgCallBack(int iHandle, IntPtr pAlarmMsg/*ref HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmMsg*/, AlarmListenCB pUser)
        {
            HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmMsg = new HCEHomeAlarm.NET_EHOME_ALARM_MSG();
            struAlarmMsg.sSerialNumber = new byte[HCEHomeAlarm.NET_EHOME_SERIAL_LEN];
            struAlarmMsg.byRes = new byte[20];
            struAlarmMsg = (HCEHomeAlarm.NET_EHOME_ALARM_MSG)Marshal.PtrToStructure(pAlarmMsg, typeof(HCEHomeAlarm.NET_EHOME_ALARM_MSG));

            //在这里根据设备的序列号，进行Token认证
            if (m_bUseAccessList && !m_AlarmListenCB.IsInAccessDeviceList(struAlarmMsg.sSerialNumber))
            {
                return false;
            }

            //暂时通过消息发送的方法进行Alarm日志更新, 后续考虑改成委托实现
            SendMessage(m_logListHandle, EHomeDemo.WM_LISTENED_ALARM, pAlarmMsg, pAlarmMsg);
            return true;
        }
        #endregion

        private void EHomeDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPreviewListen();
            HCEHomePublic.NET_EHOME_AMS_ADDRESS struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
            struAmsAddr.dwSize = Marshal.SizeOf(struAmsAddr);
            struAmsAddr.byEnable = 2;
            IntPtr ptrAmsAddr = Marshal.AllocHGlobal(struAmsAddr.dwSize);
            Marshal.StructureToPtr(struAmsAddr, ptrAmsAddr, false);
            //CMS停止接收
            HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, ptrAmsAddr);
            Marshal.FreeHGlobal(ptrAmsAddr);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lUdpAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lCmsAlarm);
            HCEHomeAlarm.NET_EALARM_Fini();
            HCEHomeCMS.NET_ECMS_StopListen(0);
            HCEHomeCMS.NET_ECMS_Fini();
            HCEHomeStream.NET_ESTREAM_Fini();

        }

        private void m_previewPanelOne_MouseDown(object sender, MouseEventArgs e)
        {
           
            //ControlPaint.DrawBorder(e., ClientRectangle, Color.Ivory, ButtonBorderStyle.Solid);
            m_iCurWndIndex = 0;
            g_deviceTree.m_iCurWndIndex = 0;
        }

        private void m_previewPanelTwo_MouseDown(object sender, MouseEventArgs e)
        {

            m_iCurWndIndex = 1;
            g_deviceTree.m_iCurWndIndex = 1;
        }

        private void m_previewPanelThree_MouseDown(object sender, MouseEventArgs e)
        {

            m_iCurWndIndex = 2;
            g_deviceTree.m_iCurWndIndex = 2;
        }

        private void m_previewPanelFour_MouseDown(object sender, MouseEventArgs e)
        {

            m_iCurWndIndex = 3;
            g_deviceTree.m_iCurWndIndex = 3;
        }

        private void audioTalk_Click(object sender, EventArgs e)
        {
            this.m_previewWnd.Visible = false;
            this.g_AudioTalk.Visible = true;
            this.g_TotalPanel.Controls.Clear();
            this.g_TotalPanel.Controls.Add(g_AudioTalk);
            this.g_TotalPanel.Controls.Add(m_panelDeviceLog);

            g_AudioTalk.CheckInitParam();

        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            this.g_AudioTalk.Visible = false;
            this.m_previewWnd.Visible = true;
            this.m_panelDeviceLog.Visible = true;
            this.g_TotalPanel.Controls.Clear();
            this.g_TotalPanel.Controls.Add(m_previewWnd);
            this.g_TotalPanel.Controls.Add(m_panelDeviceLog);
        }

        private void radioBtnLocalLog_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdoLocalLog.Checked)
            {
                g_formList.showList(0);
            }
            else if (m_rdoAlarmInformation.Checked)
            {
                g_formList.showList(1);
            }
        }

        private void m_rdoAlarmInformation_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdoLocalLog.Checked)
            {
                g_formList.showList(0);
            }
            else if (m_rdoAlarmInformation.Checked)
            {
                g_formList.showList(1);
            }
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {
            this.m_MenuElse.Show(btnOthers, this.btnOthers.PointToClient(Cursor.Position), ToolStripDropDownDirection.BelowRight);
        }

        private void GpsInfoSet_Click(object sender, EventArgs e)
        {
            GPSInfoSet GPSInfoDlg = new GPSInfoSet();
            GPSInfoDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            GPSInfoDlg.ShowDialog();
        }


        private void m_panelDeviceLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.m_previewWnd.Visible = false;
            int x = this.g_TotalPanel.Location.X;
            int y = this.g_TotalPanel.Location.Y;
            this.m_panelDeviceLog.Location = new System.Drawing.Point(x, y);
            int w = this.g_TotalPanel.Width;
            int h = this.g_TotalPanel.Height;
            this.m_panelDeviceLog.Size = new System.Drawing.Size(w, h);

        }

        private void EHomeDemoExit_Click(object sender, EventArgs e)
        {
            //停止预览监听
            StopPreviewListen();
            HCEHomePublic.NET_EHOME_AMS_ADDRESS struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
            struAmsAddr.dwSize = Marshal.SizeOf(struAmsAddr);
            struAmsAddr.byEnable = 2;
            IntPtr ptrAmsAddr = Marshal.AllocHGlobal(struAmsAddr.dwSize);
            Marshal.StructureToPtr(struAmsAddr, ptrAmsAddr, false);
            //CMS停止接收
            HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, ptrAmsAddr);
            Marshal.FreeHGlobal(ptrAmsAddr);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lUdpAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lCmsAlarm);
            HCEHomeAlarm.NET_EALARM_Fini();
            HCEHomeCMS.NET_ECMS_StopListen(0);
            HCEHomeCMS.NET_ECMS_Fini();
            HCEHomeStream.NET_ESTREAM_Fini();
            this.Close();
        }
        #endregion
    }
}
