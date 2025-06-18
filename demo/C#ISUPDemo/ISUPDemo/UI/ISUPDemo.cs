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
using System.IO;

using TINYXMLTRANS;
using ISUPDemo.Public;
using System.Globalization;
using ISUPDemo.UI;
using Newtonsoft.Json;

namespace ISUPDemo
{
    public partial class ISUPDemo : Form
    {

        #region "dll region"
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        #endregion

        #region "member varient region"
        private String m_csLocalIP=null; 
        private Int16  m_nPort;     
        public int m_lPort;
        public IntPtr m_hWnd;
        public IntPtr m_hPreviewWnd;   
        public bool m_bPlay;
        public int m_iPlayHandle;
        public int m_iSessionID = -1;
        public int m_iWndIndex;
        public int m_iLoginID = -1;
        public int m_iHandle = -1;
        public int m_iChanelType;
        public int m_iLinkHandle = -1;
        public int m_iRealHandle = -1;
        public bool m_EnableXmlDoc;   // 是否适用ISUPDemo.Xml进行服务参数配置

        int m_iCurWndNum;    //screen split mode 1, 4, 9, 16, 25, 36
        int m_iCurWndIndex; //current selected split window index, start from 0
        public int m_byUseCmsPort;
        public bool m_bListLogEnlarge;
        public bool m_bListAlarmEnlarge;
        public int m_iMainType;
        public int m_lUdpAlarmHandle;
        public int m_lTcpAlarmHandle;
        public int m_lEhome50AlarmHandle;
        public int m_lCmsAlarm;
        public bool m_bUseAccessList = false;
        public bool m_bAlarmed = false;
        public static IntPtr m_treeHandle;
        public int m_count1 = 0;
        public const int MAX_PATH = 260;
        public string SS_PATH = "C:\\StorageServer";
        public string SS_MESSAGE_PATH = "C:\\StorageServer\\Message";
        public string SS_STORAGE_PATH = "C:\\StorageServer\\Storage";
        public int PicServerPort = 0;

         public HCEHomeAlarm.EHomeMsgCallBack ptrAlarmCB = null;
         public HCEHomeAlarm.EHomeMsgCallBack ptrTcpAlarm = null;
         public HCEHomeAlarm.EHomeMsgCallBack ptrMqtt = null;


        public static bool g_bSS_Enable = true;

        public ushort m_wAlarmServerMqttPort = 0;
        public const int DevOnServerAdd = 0;
        public const int StreamAdd = 1;
        public const int AudioAdd = 2;
        public const int DASAdd = 3;
        public const int PSSAdd = 4;
        public const int PIC_URI_LEN = 128;
        public const int WM_ADD_LOG = 0x401;                      //add log 
        public const int WM_ADD_DEV = 78;                         //add device
        public const int WM_LISTENED_ALARM = 79;
        public const int WM_ACS_ALARM = 80; 
        public const int WM_DEL_DEV = 0x403;                      //delete device
        public const int WM_CHANGE_CHANNEL_ITEM_IMAGE = 0x404;    //change channel node icon
        public const int WM_PROC_EXCEPTION = 0x405;               //process exception
        public const int WM_CHANGE_IP = 0x406;                    //ip address changed
        public const int MAX_DEVICES=1024;

        public delegate bool rmMsgCallBack(long lHandle, ref HCEHomeAlarm.NET_EHOME_ALARM_MSG pAlarmMsg, IntPtr pUser);
        public HCEHomePublic.NET_EHOME_AMS_ADDRESS m_struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
        public HCEHomePublic.NET_EHOME_LOCAL_ACCESS_SECURITY m_struAccessSecure = new HCEHomePublic.NET_EHOME_LOCAL_ACCESS_SECURITY();
        public HCEHomePublic.NET_EHOME_SEND_PARAM m_struSendParam = new HCEHomePublic.NET_EHOME_SEND_PARAM();
        public HCEHomeCMS.NET_EHOME_CMS_LISTEN_PARAM m_struCMSListenPara = new HCEHomeCMS.NET_EHOME_CMS_LISTEN_PARAM();
        //public HCEHomeCMS.NET_EHOME_SERVER_INFO m_struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO();
        public HCEHomeCMS.NET_EHOME_SERVER_INFO_V50 m_struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO_V50();
        public HCEHomeCMS.NET_EHOME_SERVER_INFO_V50 m_struServUIConfig = new HCEHomeCMS.NET_EHOME_SERVER_INFO_V50();
        public Public.ConvertModel m_ConvertModel = new Public.ConvertModel();
        public static ACCESS_DEVICE_INFO[] m_stAccessDeviceList = new ACCESS_DEVICE_INFO[64];
        private static HCEHomeStream.PREVIEW_DATA_CB fnPREVIEW_DATA_CB_Func = new HCEHomeStream.PREVIEW_DATA_CB(fnPREVIEW_DATA_CB);
        private HCEHomeStream.PREVIEW_NEWLINK_CB fnPREVIEW_NEWLINK_CB_Func = null;
        private HCEHomeAlarm.EHomeMsgCallBack AlarmMsgCallBack_Func = null;
        private HCEHomeCMS.DEVICE_REGISTER_CB EHOME_REGISTER_Func = null;

        private  HCEHomeSS.EHomeSSMsgCallBack SSMsgCallBack = null;
        private  HCEHomeSS.EHomeSSStorageCallBack StroageCallBack = null;

        private HCEHomeSS.EHomeSSMsgCallBack ptrSSMsgCallBack = null;
        private HCEHomeSS.EHomeSSStorageCallBack ptrStroageCallBack = null;

        public bool m_bHttps = false;
        public XmlDocument doc = new XmlDocument();
        //public GlobalDefinition.LOCAL_DEVICE_INFO[] g_struDeviceInfo = new GlobalDefinition.LOCAL_DEVICE_INFO[MAX_DEVICES]; 

        private int m_count = 0;
        public int m_lSSHandle = -1;
        private string PicServerip = null;

        private DeviceLogList g_formList = DeviceLogList.Instance();
        private DeviceTree g_deviceTree = DeviceTree.Instance();

//        private CMSAlarm g_alarmInfoList = CMSAlarm.Instance();
        private Alarm g_alarmInfoList = Alarm.Instance();

        private AudioTalk g_AudioTalk = new AudioTalk();
        private PlayBack g_PlayBack = new PlayBack();
        private Alarm g_Alarm = new Alarm();
        private Update g_Update = new Update();

        private IntPtr m_logListHandle;
//        private IntPtr m_alarmListHandle;
        private static string StrTcpAlarmIp = "";
        private static string StrUdpAlarmIp = "";
        private static string StrPicServIp = "";

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


//         public class RegionCoordinates
//         {
//             public int positionX { get; set; }
//             public int positionY { get; set; }
//         }
//         public class FaceTemperatureMeasurementEvent
//         {
//             public string deviceName { get; set; }
//             public int serialNo { get; set; }
//             public string thermometryUnit { get; set; }
//             public float currTemperature { get; set; }
//             public Boolean isAbnomalTemperature { get; set; }
//             public RegionCoordinates RegionCoordinates { get; set; }
//             public string mask { get; set; }
//             public string visibleLightURL { get; set; }
//             public string thermalURL { get; set; }
//         }
//         public class FaceAlarmInfo
//         {
//             public string ipAddress { get; set; }
//             public int portNo { get; set; }
//             public int channelID { get; set; }
//             public string dateTime { get; set; }
//             public int activePostCount { get; set; }
//             public string eventType { get; set; }
//             public string eventState { get; set; }
//             public string eventDescription { get; set; }
//             public FaceTemperatureMeasurementEvent FaceTemperatureMeasurementEvent { get; set; }
//         }
        public ISUPDemo()
        {

            ServerConfiguration configDlg = new ServerConfiguration();
            DialogResult mResponse = configDlg.ShowDialog();
            if (mResponse != DialogResult.OK)
            {
                configDlg.Hide();
                configDlg.Close();
                return;
            }

            m_EnableXmlDoc = configDlg.m_bEnableISUPDemoXML;
            m_struServUIConfig = configDlg.struServInfo;
//            m_wAlarmServerMqttPort = ushort.Parse(configDlg.m_csMQTTPort.ToString()); ; 


            SelectIP ipDlg = new SelectIP();
            DialogResult nResponse = ipDlg.ShowDialog();
            if (nResponse != DialogResult.OK)
            {
                ipDlg.Hide();
                ipDlg.Close();
                return;
            }
            //Initg_struDeviceInfo(ref g_struDeviceInfo, MAX_DEVICES);
            InitializeComponent();
            String csIP;
            String csPort;
            csIP = ipDlg.m_cmbLocalIP.Text.ToString();
            csPort = ipDlg.m_textPort.Text.ToString();
            SetAddr(csIP, Convert.ToInt16(csPort));
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
                doc.Load(@"ISUPDemo.xml");
                try
                {
                    XmlNode root = doc.DocumentElement.FirstChild;

                    m_struServInfo.dwKeepAliveSec = int.Parse(root.SelectSingleNode("//KeepAliveSeconds").InnerText);

                    m_struServInfo.dwTimeOutCount = int.Parse(root.SelectSingleNode("//KeepAliveCount").InnerText);

                    m_struServInfo.dwAlarmServerType = int.Parse(root.SelectSingleNode("//AlarmServerType").InnerText);
                    strTemp = root.SelectSingleNode("//AlarmServerIP").InnerText;
                    StrTcpAlarmIp = strTemp;
                    StrUdpAlarmIp = strTemp;
                    strTemp.CopyTo(0, m_struServInfo.struTCPAlarmSever.szIP, 0, strTemp.Length);
                    strTemp.CopyTo(0, m_struServInfo.struUDPAlarmSever.szIP, 0, strTemp.Length);

                    m_struServInfo.struUDPAlarmSever.wPort = short.Parse(root.SelectSingleNode("//AlarmServerUdpPort").InnerText);
                    
                    m_struServInfo.struTCPAlarmSever.wPort = short.Parse(root.SelectSingleNode("//AlarmServerTcpPort").InnerText);
                    m_wAlarmServerMqttPort = ushort.Parse(root.SelectSingleNode("//AlarmServerMqttPort").InnerText);
                    m_byUseCmsPort = int.Parse(root.SelectSingleNode("//AlarmServerPortUseCms").InnerText);

                    strTemp = root.SelectSingleNode("//NTPServerIP").InnerText;
                    strTemp.CopyTo(0, m_struServInfo.struNTPSever.szIP, 0, strTemp.Length);

                    m_struServInfo.struNTPSever.wPort = short.Parse(root.SelectSingleNode("//NTPServerPort").InnerText);
                    m_struServInfo.dwNTPInterval = int.Parse(root.SelectSingleNode("//NTPInterval").InnerText);

                    m_struServInfo.dwPicServerType = int.Parse(root.SelectSingleNode("//PictureServerType").InnerText);
                    strTemp = root.SelectSingleNode("//PictureServerIP").InnerText;
                    PicServerip = strTemp;
                    StrPicServIp = strTemp;
                    strTemp.CopyTo(0, m_struServInfo.struPictureSever.szIP, 0, strTemp.Length);
                    m_struServInfo.struPictureSever.wPort = short.Parse(root.SelectSingleNode("//PictureServerPort").InnerText);
                    PicServerPort = int.Parse(root.SelectSingleNode("//PictureServerPort").InnerText);                    

                    m_byCmsSecureAccessType = Convert.ToByte(root.SelectSingleNode("//CmsAccessSecurity").InnerText);
                    m_byAlarmSecureAccessType = Convert.ToByte(root.SelectSingleNode("//AlarmAccessSecurity").InnerText);
                    m_byStreamSecureAccessType = Convert.ToByte(root.SelectSingleNode("//StreamAccessSecurity").InnerText);


                    //新增修改的内容，修复设备注册回调时，IP地址异常的bug，IP地址后面增加结束符'\0',否则传到底层dll库时会变成“地址00000000000”形式
                    char[] TCPAlarmIP = StrTcpAlarmIp.ToCharArray();
                    char[] UDPAlarmIP = StrUdpAlarmIp.ToCharArray();
                    char[] PicServIP = StrPicServIp.ToCharArray();
                    ClearCharArr(ref m_struServInfo.struPictureSever.szIP);
                    ClearCharArr(ref m_struServInfo.struTCPAlarmSever.szIP);
                    ClearCharArr(ref m_struServInfo.struUDPAlarmSever.szIP);
                    Array.Copy(TCPAlarmIP, m_struServInfo.struTCPAlarmSever.szIP, TCPAlarmIP.Length);
                    Array.Copy(UDPAlarmIP, m_struServInfo.struUDPAlarmSever.szIP, UDPAlarmIP.Length);
                    Array.Copy(PicServIP, m_struServInfo.struPictureSever.szIP, PicServIP.Length);
                }

            catch(Exception ep)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 0, ep.ToString());
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


        private bool SS_Message_Callback(int iHandle,HCEHomeSS.NET_EHOME_SS_MSG_TYPE enumType,IntPtr pOutBuffer,int dwOutLen,IntPtr pInBuffer,int dwInLen,IntPtr pUser)
        {
            if(enumType==HCEHomeSS.NET_EHOME_SS_MSG_TYPE.NET_EHOME_SS_MSG_TOMCAT)
            {
                HCEHomeSS.NET_EHOME_SS_TOMCAT_MSG pTomcatMsg = (HCEHomeSS.NET_EHOME_SS_TOMCAT_MSG)Marshal.PtrToStructure(pOutBuffer, typeof(HCEHomeSS.NET_EHOME_SS_TOMCAT_MSG));
                char[] szPicUri = new char[4 * PIC_URI_LEN];
                
                for(int i=0;i<pTomcatMsg.dwPicNum;++i)
                {
                    string temp = Marshal.PtrToStringAnsi(pTomcatMsg.pPicURLs);
                    temp += (i * HCEHomeSS.MAX_URL_LEN_SS).ToString();
                    temp.CopyTo(0, szPicUri, i * PIC_URI_LEN, PIC_URI_LEN);
                }
                if(!File.Exists(SS_PATH))
                {
                    File.Create(SS_PATH);
                }
                if(!File.Exists(SS_MESSAGE_PATH))
                {
                    File.Create(SS_MESSAGE_PATH);
                }

                string strFilePath = SS_MESSAGE_PATH + "\\tomcatOutput.txt";
                byte[] byPicUri=new byte[4*PIC_URI_LEN];
                Array.Copy(szPicUri, 0, byPicUri, 0, 4 * PIC_URI_LEN);
                string line= "\n";
                byte[] byline = System.Text.Encoding.Default.GetBytes(line);
                using(FileStream fs=new FileStream(strFilePath,FileMode.Create,FileAccess.Write))
                {
                    try
                    {
                        for(int i=0;i<pTomcatMsg.dwPicNum;++i)
                        {
                            fs.Write(byPicUri, i * PIC_URI_LEN, PIC_URI_LEN);
                            fs.Write(byline, 0, byline.Length);
                        }
                        fs.Flush();
                        fs.Close();
                    }
                    catch(Exception e)
                    {
                        g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 0, e.ToString());
                    }
                }
            }
            else if(enumType==HCEHomeSS.NET_EHOME_SS_MSG_TYPE.NET_EHOME_SS_MSG_KMS_USER_PWD)
            {
                Marshal.WriteByte(pInBuffer, 1);
            }
            else if(enumType==HCEHomeSS.NET_EHOME_SS_MSG_TYPE.NET_EHOME_SS_MSG_CLOUD_AK)
            {
                byte[] temp = new byte[128];
                Marshal.Copy(pOutBuffer, temp, 0, dwOutLen);
                for(int i=0;i<MAX_DEVICES;++i)
                {
                    if(DeviceTree.g_struDeviceInfo[i].iDeviceIndex!=-1)
                    {
                        if(DeviceTree.isByteEqual(ref DeviceTree.g_struDeviceInfo[i].byDeviceID,ref temp,64))
                        {
                            Marshal.Copy(DeviceTree.g_struDeviceInfo[i].byClouldSecretKey, 0, pInBuffer, 64);
                            break;
                        }
                    }
                }
            }
            return true;
        }
        //存储回调
        public bool SS_Storage_Callback(int iHandle,IntPtr pFileName,IntPtr pFileBuf,int dwFileLen,IntPtr pFilepath,IntPtr pUser)
        {
            string strFilename = Marshal.PtrToStringAnsi(pFileName);
            if(""==strFilename||IntPtr.Zero==pFileBuf||0==dwFileLen)
            {
                return false;
            }
            if(false==System.IO.Directory.Exists(SS_PATH))
            {
                System.IO.Directory.CreateDirectory(SS_PATH);
            }
            if(false==System.IO.Directory.Exists(SS_STORAGE_PATH))
            {
                System.IO.Directory.CreateDirectory(SS_STORAGE_PATH);
            }
            strFilename.Replace(':', '_');
            string KMSTemp = "/home/config/pic/enrlFace";
            if (strFilename.Contains("/home/config/pic/enrlFace"))
            {
                int KMSIndex = strFilename.IndexOf(KMSTemp);
                strFilename = strFilename.Substring(0, KMSIndex);
            }
//             else if ((strFilename.Contains(".jpg") || strFilename.Contains(".png")) && !(strFilename.Contains("_00.jpg")))
//             {
//                 //capture Face Through KMS Protocol，Filter .jpg or .png as well as DateTime  in Pic Name
//                 //in case of FileName is too long,
//                 strFilename = strFilename.Substring(0, strFilename.Length - 18);
//             }
//             else if (strFilename.Contains("_00.jpg"))
//             {
//                 strFilename = strFilename.Substring(0, strFilename.Length - 21);
//             }
            string Filepath = SS_STORAGE_PATH + "\\" + strFilename;
            try
            {
                using (FileStream fs = new FileStream(Filepath, FileMode.OpenOrCreate, FileAccess.Read | FileAccess.Write))
                {
                    byte[] DataStream = new byte[dwFileLen];
                    Marshal.Copy(pFileBuf, DataStream, 0, dwFileLen);
                    fs.Write(DataStream, 0, dwFileLen);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch(Exception e)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, e.ToString());
            }
            HCEHomeSS.PicPath pPath = new HCEHomeSS.PicPath();
            pPath.Init();
            //Convert Filepath to byte []
            pPath.Path = System.Text.Encoding.Default.GetBytes(Filepath.PadRight(100, '\0'));
            IntPtr DemoPath = Marshal.AllocHGlobal(Marshal.SizeOf(pPath));
            Marshal.StructureToPtr(pPath, pFilepath, false);
            return true;
        }
        void GetAddressByType(int iType, int dwVersion,ref IntPtr pOutValue, int dwOutLen, ref int pSubValue, int dwSubLen)
        {
            int iMapType = -1;
            try
            {
                XmlNode root = doc.SelectSingleNode("//AddressMap");
                iMapType = int.Parse(root.SelectSingleNode("//Enable").InnerText);
                if (iMapType != 1 && iMapType != -1 && iMapType != 0)
                {
                    iMapType = -1;
                }
                else if (iMapType == 1)
                {
                    iMapType = 1;
                }
                else
                {
                    iMapType = -1;
                }
            }
            catch
            {

            }

            if (DevOnServerAdd == iType)
            {
                FormatRegAddress(ref pOutValue, dwOutLen, dwVersion, iMapType, ref doc);
            }
            else if (StreamAdd == iType)
            {
                GetStreamAddress(ref pOutValue, dwOutLen, ref pSubValue, dwSubLen, iMapType, ref doc);
            }
            else if (AudioAdd == iType)
            {
                GetAudioAddress(ref pOutValue, dwOutLen, ref pSubValue, dwSubLen, iMapType, ref doc);
            }
            else if (DASAdd == iType)
            {
                GetDasAddress(ref pOutValue, dwOutLen, ref pSubValue, dwSubLen, iMapType, ref doc);
            }
            else if (PSSAdd == iType)
            {
                GetPicServerAddress(ref pOutValue, dwOutLen, ref pSubValue, dwSubLen, iMapType, ref doc);
            }
        }

        void ClearCharArr(ref char[] source)
        {
            for(int i=0;i<source.Length;++i)
            {
                source[i] = '\0';
            }
        }


        void FormatRegAddress(ref IntPtr pOutValue, int dwOutLen, int dwVersion, int iMapType, ref XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("//AddressMap");
            HCEHomeCMS.NET_EHOME_SERVER_INFO_V50 struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO_V50();
            struServInfo.Init();
            struServInfo = (HCEHomeCMS.NET_EHOME_SERVER_INFO_V50)Marshal.PtrToStructure(pOutValue, typeof(HCEHomeCMS.NET_EHOME_SERVER_INFO_V50));
            struServInfo = m_struServInfo;
            string strTemp = null;
            if (5 == dwVersion)
            {
                struServInfo.struTCPAlarmSever.wPort = (short)m_wAlarmServerMqttPort;
            }

            if (1 == iMapType)
            {
                strTemp = doc.SelectSingleNode("//AddressMap//AlarmServerIP").InnerText;
                ClearCharArr(ref struServInfo.struTCPAlarmSever.szIP);
                ClearCharArr(ref struServInfo.struUDPAlarmSever.szIP);
                strTemp.CopyTo(0, struServInfo.struTCPAlarmSever.szIP, 0, strTemp.Length);
                strTemp.CopyTo(0, struServInfo.struUDPAlarmSever.szIP, 0, strTemp.Length);
                //System.Buffer.BlockCopy(strTemp.ToCharArray(), 0, struServInfo.struTCPAlarmSever.szIP, 0, strTemp.ToCharArray().Length);
                //System.Buffer.BlockCopy(strTemp.ToCharArray(), 0, struServInfo.struUDPAlarmSever.szIP, 0, strTemp.ToCharArray().Length);

                struServInfo.struUDPAlarmSever.wPort = short.Parse(doc.SelectSingleNode("//AddressMap//AlarmServerUdpPort").InnerText);
                struServInfo.struTCPAlarmSever.wPort = short.Parse(doc.SelectSingleNode("//AddressMap//AlarmServerTcpPort").InnerText);

                if (5 == dwVersion)
                {
                    struServInfo.struTCPAlarmSever.wPort = short.Parse(doc.SelectSingleNode("//AddressMap//AlarmServerMqttPort").InnerText);
                }

                string strPicIp = doc.SelectSingleNode("//AddressMap//PictureServerIP").InnerText.ToString();

                //System.Buffer.BlockCopy(strPicIp.ToCharArray(), 0, struServInfo.struPictureSever.szIP, 0, strPicIp.ToCharArray().Length);
                ClearCharArr(ref struServInfo.struPictureSever.szIP);
                strPicIp.CopyTo(0, struServInfo.struPictureSever.szIP, 0, strPicIp.Length);

                struServInfo.struPictureSever.wPort = short.Parse(doc.SelectSingleNode("//AddressMap//PictureServerPort").InnerText);
            }

            string strptrTest = new string(struServInfo.struTCPAlarmSever.szIP, 0, 128);
            IntPtr ptrTest = (IntPtr)Marshal.StringToHGlobalAnsi(strptrTest);
            if (IsNULLAddress(ptrTest))
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                System.Buffer.BlockCopy(LocalIP.ToCharArray(), 0, struServInfo.struTCPAlarmSever.szIP, 0, LocalIP.Length);

            }
            Marshal.FreeHGlobal(ptrTest);

            strptrTest = new string(struServInfo.struUDPAlarmSever.szIP, 0, 128);
            ptrTest = (IntPtr)Marshal.StringToHGlobalAnsi(strptrTest);
            if (IsNULLAddress(ptrTest))
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                System.Buffer.BlockCopy(LocalIP.ToCharArray(), 0, struServInfo.struUDPAlarmSever.szIP, 0, LocalIP.Length);
            }
            Marshal.FreeHGlobal(ptrTest);

            Marshal.StructureToPtr(struServInfo, pOutValue, false);
        }

        void GetStreamAddress(ref IntPtr pOutValue, int dwOutLen, ref int pSubValue, int dwSubLen, int iMapType, ref XmlDocument doc)
        {
            if (-1 == iMapType || 0 == iMapType)
            {
                
                pOutValue = Marshal.StringToHGlobalAnsi(m_csLocalIP);
            }
            else
            {
                string StreamIP = doc.SelectSingleNode("//StreamServerIP").InnerText.ToString();
                char[] TempByte = new char[StreamIP.Length * sizeof(char)];
                System.Buffer.BlockCopy(StreamIP.ToCharArray(), 0, TempByte, 0, TempByte.Length);
                Marshal.Copy(TempByte, 0, pOutValue, TempByte.Length);

                int ServerPort = int.Parse(doc.SelectSingleNode("//StreamServerPort").InnerText);
                pSubValue = ServerPort;
            }
            if (IsNULLAddress(pOutValue))
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                char[] IPtemp = new char[LocalIP.Length * sizeof(char)];
                System.Buffer.BlockCopy(LocalIP.ToCharArray(), 0, IPtemp, 0, LocalIP.Length);
                Marshal.Copy(IPtemp, 0, pOutValue, IPtemp.Length);
            }
        }

        bool IsNULLAddress(IntPtr pOutValue)
        {
            string temp = Marshal.PtrToStringAnsi(pOutValue);
            if (0 == temp.Length || temp == "0.0.0.0")
            {
                return true;
            }
            return false;
        }

        void GetAudioAddress(ref IntPtr pOutValue, int dwOutLen, ref int pSubValue, int dwSubLen, int iMapType, ref XmlDocument doc)
        {
            if (-1 == iMapType || 0 == iMapType)
            {
                pOutValue = Marshal.StringToHGlobalAnsi(m_csLocalIP);
            }
            else
            {
                string AudioIP = doc.SelectSingleNode("//AudioServerIP").InnerText.ToString();
                char[] TempByte = new char[AudioIP.Length * sizeof(char)];
                System.Buffer.BlockCopy(AudioIP.ToCharArray(), 0, TempByte, 0, TempByte.Length);
                Marshal.Copy(TempByte, 0, pOutValue, TempByte.Length);

                int ServerPort = int.Parse(doc.SelectSingleNode("//AudioServerPort").InnerText);
                pSubValue = ServerPort;
            }

            if (IsNULLAddress(pOutValue))
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                char[] IPtemp = new char[LocalIP.Length * sizeof(char)];
                System.Buffer.BlockCopy(LocalIP.ToCharArray(), 0, IPtemp, 0, LocalIP.Length);
                Marshal.Copy(IPtemp, 0, pOutValue, IPtemp.Length);
            }
        }

        void GetDasAddress(ref IntPtr pOutValue, int dwOutLen, ref int pSubValue, int dwSubLen, int iMapType, ref XmlDocument doc)
        {
            if (-1 == iMapType || 0 == iMapType)
            {
                pOutValue = Marshal.StringToHGlobalAnsi(m_csLocalIP);
            }
            else
            {
                string DASIP = doc.SelectSingleNode("//DASIP").InnerText.ToString();
                //char[] TempByte = new char[DASIP.Length * sizeof(char)];
                //System.Buffer.BlockCopy(DASIP.ToCharArray(), 0, TempByte, 0, TempByte.Length);
                //Marshal.Copy(TempByte, 0, pOutValue, TempByte.Length);

                pOutValue=Marshal.StringToHGlobalAnsi(DASIP);
                int ServerPort = int.Parse(doc.SelectSingleNode("//DASPort").InnerText);
                pSubValue = ServerPort;
            }

            if (IsNULLAddress(pOutValue))
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                char[] IPtemp = new char[LocalIP.Length * sizeof(char)];
                System.Buffer.BlockCopy(LocalIP.ToCharArray(), 0, IPtemp, 0, LocalIP.Length);
                Marshal.Copy(IPtemp, 0, pOutValue, IPtemp.Length);
            }
        }

        void GetPicServerAddress(ref IntPtr pOutValue, int dwOutLen, ref int pSubValue, int dwSubLen, int iMapType, ref XmlDocument doc)
        {
            
            if (-1 == iMapType || 0 == iMapType)
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                byte[] IPtemp = new byte[LocalIP.Length];
                StrTobyte(ref IPtemp, LocalIP);
                Marshal.Copy(IPtemp, 0, pOutValue, IPtemp.Length);
            }
            else
            {
                string strPicIp = doc.SelectSingleNode("//AddressMap//PictureServerIP").InnerText.ToString();
                //char[] TempByte = new char[strPicIp.Length * sizeof(char)];
                //System.Buffer.BlockCopy(strPicIp.ToCharArray(), 0, TempByte, 0, TempByte.Length);
                byte[] TempByte = new byte[strPicIp.Length];
                StrTobyte(ref TempByte, strPicIp);
                //pOutValue = Marshal.StringToHGlobalAnsi(strPicIp);
                Marshal.Copy(TempByte, 0, pOutValue, strPicIp.Length);
                int ServerPort = int.Parse(doc.SelectSingleNode("//AddressMap//PictureServerPort").InnerText);
                pSubValue = ServerPort;
            }
            if (IsNULLAddress(pOutValue))
            {
                string LocalIP = SelectIP.IpAddressList[0].ToString();
                byte[] IPtemp = new byte[LocalIP.Length];
                StrTobyte(ref IPtemp, LocalIP);
                Marshal.Copy(IPtemp, 0, pOutValue, IPtemp.Length);
            }
        }

        private void StrTobyte(ref byte[] des, string source)
        {
            if (source.Length <= 0)
                return;
            for(int i=0;i<source.Length;++i)
            {
                des[i] = (byte)source[i];
            }
        }
        public void InitLib()
        {
            String csLogPath = "C:/EHomeSdkLog/";
            char[] szLogPath = new char[128];
            csLogPath.CopyTo(0, szLogPath, 0, csLogPath.Length);
            
            if(g_bSS_Enable)
            {
                HCEHomeSS.NET_ESS_Init();
                HCEHomeSS.NET_ESS_SetLogToFile(3, szLogPath, true);
                if(m_bHttps)
                {
                    HCEHomeSS.NET_EHOME_SS_LISTEN_HTTPS_PARAM struHttpsParam = new HCEHomeSS.NET_EHOME_SS_LISTEN_HTTPS_PARAM();
                    struHttpsParam.Init();
                    struHttpsParam.byHttps = 1;
                    struHttpsParam.byCertificateFileType = 0;
                    struHttpsParam.byPrivateKeyFileType = 0;

                    struHttpsParam.szUserCertificateFile = "C:\\Certificate\\server.pem";
                    struHttpsParam.szUserPrivateKeyFile = "C:\\Certificate\\server.pem";
                    struHttpsParam.dwSSLVersion = 0;
                    HCEHomeSS.NET_ESS_SetListenHttpsParam(ref struHttpsParam);
                }
               
                HCEHomePublic.NET_EHOME_IPADDRESS struPublicAddress = new HCEHomePublic.NET_EHOME_IPADDRESS();
                struPublicAddress.Init();

                IntPtr PtrSzIp = Marshal.AllocHGlobal(Marshal.SizeOf(struPublicAddress));
                Marshal.StructureToPtr(struPublicAddress,PtrSzIp,false);
                int port=0;
                int size=Marshal.SizeOf(struPublicAddress.wPort);
                GetAddressByType(4, 0, ref PtrSzIp, 128, ref port, size);

                struPublicAddress = (HCEHomePublic.NET_EHOME_IPADDRESS)Marshal.PtrToStructure(PtrSzIp, typeof(HCEHomePublic.NET_EHOME_IPADDRESS));

                struPublicAddress.wPort = short.Parse(port.ToString());
                if(0==struPublicAddress.wPort)
                {
                    struPublicAddress.wPort = m_struServInfo.struPictureSever.wPort;
                }
                Array.Copy(m_struServInfo.struPictureSever.szIP, 0, struPublicAddress.szIP,0,128);
                
                int dwsize = Marshal.SizeOf(struPublicAddress);
                IntPtr ptrstruPublicAddress = Marshal.AllocHGlobal(dwsize);
                Marshal.StructureToPtr(struPublicAddress, ptrstruPublicAddress, false);
                HCEHomeSS.NET_ESS_SetSDKInitCfg(HCEHomeSS.NET_EHOME_SS_INIT_CFG_TYPE.NET_EHOME_SS_INIT_CFG_PUBLIC_IP_PORT, ptrstruPublicAddress);
                Marshal.FreeHGlobal(ptrstruPublicAddress);

                HCEHomeSS.NET_EHOME_SS_LISTEN_PARAM struSSListenParam = new HCEHomeSS.NET_EHOME_SS_LISTEN_PARAM();
                struSSListenParam.Init();
                struSSListenParam.struAddress.Init();
                m_csLocalIP.CopyTo(0, struSSListenParam.struAddress.szIP, 0, m_csLocalIP.Length);
                struSSListenParam.struAddress.wPort = m_struServInfo.struPictureSever.wPort;
                struSSListenParam.szKMS_UserName = "test";
                struSSListenParam.szKMS_Password = "12345";
                struSSListenParam.szAccessKey = "test";
                struSSListenParam.szSecretKey = "12345";
                //将安全模式关闭
                struSSListenParam.bySecurityMode = 1;
               
                SSMsgCallBack = new HCEHomeSS.EHomeSSMsgCallBack(SS_Message_Callback);
                struSSListenParam.fnSSMsgCb = SSMsgCallBack;
                ptrSSMsgCallBack = SSMsgCallBack;

                struSSListenParam.fnSSRWCbEx = null;

                StroageCallBack = new HCEHomeSS.EHomeSSStorageCallBack(SS_Storage_Callback);
                struSSListenParam.fnSStorageCb = StroageCallBack;
                ptrStroageCallBack = StroageCallBack;

                struSSListenParam.pUserData = this.Handle;

                m_lSSHandle = HCEHomeSS.NET_ESS_StartListen(ref struSSListenParam);
                if(-1==m_lSSHandle)
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "NET_ESS_StartListen Failed, port" + struSSListenParam.struAddress.wPort.ToString());
                }
                else
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 3, "NET_ESS_StartListen SUCC, port" + struSSListenParam.struAddress.wPort.ToString());
                }
            }
               bool bRet = false;




            bRet = HCEHomeCMS.NET_ECMS_Init();
            if (!bRet)
            {
                String str = string.Format("{0}", (int)HCEHomeCMS.NET_ECMS_GetLastError());
                Console.WriteLine("NET_ECMS_Init failed, err={0}", (int)HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_Init Failed");
            }

            csLogPath.CopyTo(0, szLogPath, 0, csLogPath.Length);
            bRet = HCEHomeCMS.NET_ECMS_SetLogToFile(3, szLogPath, true);

            m_struAccessSecure.dwSize = (uint)Marshal.SizeOf(m_struAccessSecure);
            m_struAccessSecure.byAccessSecurity = m_byCmsSecureAccessType;
            IntPtr ptrAccessSecure = Marshal.AllocHGlobal((int)m_struAccessSecure.dwSize);
            Marshal.StructureToPtr(m_struAccessSecure, ptrAccessSecure, false);
            if (!HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.ACTIVE_ACCESS_SECURITY, ptrAccessSecure))
            {
                int iRet = HCEHomeCMS.NET_ECMS_GetLastError();
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
            m_struAmsAddr.struAddress.wPort = m_struServInfo.struUDPAlarmSever.wPort;
            m_struAmsAddr.byEnable = (byte)m_byUseCmsPort;
            m_struAmsAddr.dwSize = (Int32)Marshal.SizeOf(m_struAmsAddr);
            IntPtr ptrAmsAddr = Marshal.AllocHGlobal((int)m_struAmsAddr.dwSize);
            Marshal.StructureToPtr(m_struAmsAddr, ptrAmsAddr, false);

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
            m_struSendParam.bySendTimes = 2;
            m_struSendParam.byRes2 = new byte[127];
            m_struSendParam.dwSize = (Int32)Marshal.SizeOf(m_struSendParam);
            IntPtr ptrSendParam = Marshal.AllocHGlobal(m_struSendParam.dwSize);
            Marshal.StructureToPtr(m_struSendParam, ptrSendParam, false);

            if (!HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.SEND_PARAM, ptrSendParam))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_SetSDKLocalCfg SEND_PARAM failed");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_SetSDKLocalCfg SEND_PARAM succ");
            }
            Marshal.FreeHGlobal(ptrSendParam);

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
            HCEHomeCMS.NET_ECMS_GetBuildVersion();

            string sCurPath = System.Windows.Forms.Application.StartupPath;

            if (!HCEHomeCMS.NET_ECMS_SetSDKInitCfg(HCEHomeCMS.NET_EHOME_CMS_INIT_CFG_LIBEAY_PATH, Marshal.StringToHGlobalAnsi(sCurPath + "\\libeay32.dll")))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_SetSDKInitCfg, NET_EHOME_CMS_INIT_CFG_LIBEAY_PATH, Failed");
            }
            if (!HCEHomeCMS.NET_ECMS_SetSDKInitCfg(HCEHomeCMS.NET_EHOME_CMS_INIT_CFG_SSLEAY_PATH, Marshal.StringToHGlobalAnsi(sCurPath + "\\ssleay32.dll")))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_SetSDKInitCfg, NET_EHOME_CMS_INIT_CFG_SSLEAY_PATH, Failed");
            }

            bRet = HCEHomeStream.NET_ESTREAM_Init();
            bRet = HCEHomeStream.NET_ESTREAM_SetLogToFile(3, "c:/EHomeSdkLog/", true);
            m_struAccessSecure.dwSize = (uint)Marshal.SizeOf(m_struAccessSecure);
            m_struAccessSecure.byAccessSecurity = m_byStreamSecureAccessType;
            IntPtr ptrStreamAccessSecure = Marshal.AllocHGlobal((int)m_struAccessSecure.dwSize);
            Marshal.StructureToPtr(m_struAccessSecure, ptrStreamAccessSecure, false);

            if (!HCEHomeStream.NET_ESTREAM_SetSDKLocalCfg(EhomeSDK.NET_EHOME_LOCAL_CFG_TYPE.ACTIVE_ACCESS_SECURITY, ptrStreamAccessSecure))
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ESTREAM_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Success");
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ESTREAM_SetSDKLocalCfg ACTIVE_ACCESS_SECURITY Success");
            }
            Marshal.FreeHGlobal(ptrStreamAccessSecure);
            HCEHomeStream.NET_ESTREAM_GetBuildVersion();


            bRet = HCEHomeAlarm.NET_EALARM_Init();
            bRet = HCEHomeAlarm.NET_EALARM_SetLogToFile(3, "c:/EHomeSdkLog/", true);
            m_struAccessSecure.dwSize = (UInt32)Marshal.SizeOf(m_struAccessSecure);
            m_struAccessSecure.byAccessSecurity = m_byAlarmSecureAccessType;
            ptrAccessSecure = Marshal.AllocHGlobal((int)m_struAccessSecure.dwSize);
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
            struAlarmListenParam.Init();
            string LocalIP = SelectIP.IpAddressList[0].ToString();
            LocalIP.CopyTo(0, struAlarmListenParam.struAddress.szIP, 0, LocalIP.Length);
            struAlarmListenParam.struAddress.wPort = 7661;
            AlarmMsgCallBack_Func = new HCEHomeAlarm.EHomeMsgCallBack(AlarmMsgCallBack);
            struAlarmListenParam.fnMsgCb = AlarmMsgCallBack_Func;
            struAlarmListenParam.pUserData = this.Handle;
            struAlarmListenParam.byProtocolType = 1;
            struAlarmListenParam.byUseThreadPool = 0;
            m_lUdpAlarmHandle = HCEHomeAlarm.NET_EALARM_StartListen(ref struAlarmListenParam);

            struAlarmListenParam.byProtocolType = 0;
            struAlarmListenParam.struAddress.wPort = 7662;
            ptrTcpAlarm = new HCEHomeAlarm.EHomeMsgCallBack(AlarmMsgCallBack);
            struAlarmListenParam.fnMsgCb = ptrTcpAlarm;
            struAlarmListenParam.pUserData = this.Handle;
            m_lTcpAlarmHandle = HCEHomeAlarm.NET_EALARM_StartListen(ref struAlarmListenParam);


            struAlarmListenParam.struAddress.Init();
            m_csLocalIP.CopyTo(0, struAlarmListenParam.struAddress.szIP, 0, m_csLocalIP.Length);
            ptrMqtt = new HCEHomeAlarm.EHomeMsgCallBack(AlarmMsgCallBack);
            struAlarmListenParam.fnMsgCb = ptrMqtt;
            struAlarmListenParam.pUserData = this.Handle;
            struAlarmListenParam.byUseThreadPool = 0;                       
            struAlarmListenParam.byUseCmsPort = 0;
            struAlarmListenParam.byProtocolType = 2;
            struAlarmListenParam.struAddress.wPort = 7663;
            m_lEhome50AlarmHandle = HCEHomeAlarm.NET_EALARM_StartListen(ref struAlarmListenParam);
            if (m_lEhome50AlarmHandle < 0)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_EALARM_StartListen Failed");
            }

           
        }

        public void SetAddr(String csIP, Int16 nport)
        {
            m_csLocalIP = csIP;
            m_nPort = nport;
        }

        
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
        public bool EHOME_REGISTER(int iUserID, int dwDataType, IntPtr pOutBuffer, uint dwOutLen, IntPtr pInBuffer, uint dwInLen, IntPtr pUser)
        {
            int dwSize = 0;
            GlobalDefinition.LOCAL_DEVICE_INFO struTemp = new GlobalDefinition.LOCAL_DEVICE_INFO();
            struTemp.Init();
            dwSize = Marshal.SizeOf(typeof(GlobalDefinition.LOCAL_DEVICE_INFO));
            IntPtr ptrTemp = Marshal.AllocHGlobal(dwSize);

            
            int iOutLen = 0;
            HCEHomeCMS.NET_EHOME_DEV_REG_INFO_V12 struDevInfo = new HCEHomeCMS.NET_EHOME_DEV_REG_INFO_V12();
            struDevInfo.Init();
            if (pOutBuffer != null)
            {
                if (dwDataType == HCEHomeCMS.ENUM_DEV_ON || HCEHomeCMS.ENUM_DEV_AUTH == dwDataType || HCEHomeCMS.ENUM_DEV_SESSIONKEY == dwDataType || HCEHomeCMS.ENUM_DEV_ADDRESS_CHANGED == dwDataType)
                {
                    struDevInfo = (HCEHomeCMS.NET_EHOME_DEV_REG_INFO_V12)Marshal.PtrToStructure(pOutBuffer, typeof(HCEHomeCMS.NET_EHOME_DEV_REG_INFO_V12));
                }
                
            }
            else
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, "pOutBuffer is NULL");
            }


            if (HCEHomeCMS.ENUM_DEV_ON == dwDataType)
            {
                if (pInBuffer == null)
                {
                    return false;
                }


                m_ConvertModel.UTF82A(struDevInfo.struRegInfo.byDeviceID, struDevInfo.struRegInfo.byDeviceID, HCEHomePublic.MAX_DEVICE_ID_LEN, ref iOutLen);
                m_ConvertModel.UTF82A(struDevInfo.struRegInfo.sDeviceSerial, struDevInfo.struRegInfo.sDeviceSerial, HCEHomePublic.NET_EHOME_SERIAL_LEN, ref iOutLen);

                struDevInfo.struRegInfo.byDeviceID.CopyTo(struTemp.byDeviceID, 0);
                struTemp.iLoginID = iUserID;
                struDevInfo.struRegInfo.sDeviceSerial.CopyTo(struTemp.sDeviceSerial, 0);


                byte[] szDeviceSerial = new byte[HCEHomePublic.NET_EHOME_SERIAL_LEN + 1];
                struDevInfo.struRegInfo.sDeviceSerial.CopyTo(szDeviceSerial, 0);
                byte[] szSessionKey = new byte[HCEHomePublic.MAX_MASTER_KEY_LEN + 1];
                struDevInfo.struRegInfo.bySessionKey.CopyTo(szSessionKey, 0);
                if(struDevInfo.struRegInfo.byDevProtocolVersion[0]=='2')
                {
                    struTemp.dwVersion = 2;
                }
                else if ('4'== struDevInfo.struRegInfo.byDevProtocolVersion[0])
                {
                    struTemp.dwVersion = 4;
                }
                else
                {
                    struTemp.dwVersion = 5;
                }

                HCEHomeCMS.NET_EHOME_SERVER_INFO_V50 struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO_V50();
                struServInfo.Init();
                struServInfo = (HCEHomeCMS.NET_EHOME_SERVER_INFO_V50)Marshal.PtrToStructure(pInBuffer, typeof(HCEHomeCMS.NET_EHOME_SERVER_INFO_V50));
                
                if (m_EnableXmlDoc)
                {
                    struServInfo = m_struServInfo;   //use ISUPDemo.xml
                    if (5 == struTemp.dwVersion)
                    {
                        struServInfo.struTCPAlarmSever.wPort = (short)m_wAlarmServerMqttPort;
                    }
                }
                else{
                    struServInfo = m_struServUIConfig;   //use ServerConfig UI
                    struServInfo.dwKeepAliveSec = 15;
                    struServInfo.dwTimeOutCount = 6;
                    struServInfo.dwNTPInterval = 3600;
                }


                
                int TdwSize = Marshal.SizeOf(struServInfo);
                IntPtr ptrStruS = Marshal.AllocHGlobal(TdwSize);
                Marshal.StructureToPtr(struServInfo,ptrStruS,false);

                if (m_EnableXmlDoc)
                {
                    int value = 0;
                    GetAddressByType(0, 0, ref ptrStruS, TdwSize, ref value, 0);
                    struServInfo = (HCEHomeCMS.NET_EHOME_SERVER_INFO_V50)Marshal.PtrToStructure(ptrStruS, typeof(HCEHomeCMS.NET_EHOME_SERVER_INFO_V50));
                }
                

                if(g_bSS_Enable)
                {
                    for(int i=0;i<MAX_DEVICES;++i)
                    {
                        if(DeviceTree.g_struDeviceInfo[i].iDeviceIndex!=-1)
                        {
                            if(true==DeviceTree.isByteEqual(ref DeviceTree.g_struDeviceInfo[i].byDeviceID,ref struDevInfo.struRegInfo.byDeviceID,256))
                            {
                                if(m_bHttps)
                                {
                                    struServInfo.byClouldHttps = 1;
                                }
                                else
                                {
                                    struServInfo.byClouldHttps = 0;
                                }
                                Array.Copy(struDevInfo.struRegInfo.byDeviceID,0, struServInfo.byClouldAccessKey,0, 64);
                                Array.Copy(DeviceTree.g_struDeviceInfo[i].byClouldSecretKey,0, struServInfo.byClouldSecretKey,0, 64);
                                break;
                            }
                        }
                    }
                }

               

                Marshal.StructureToPtr(struServInfo, pInBuffer, false);


                if (m_bUseAccessList)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (struTemp.dwVersion >= 4)
                        {
                            if (struDevInfo.struRegInfo.sDeviceSerial.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                           
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);

                                Message mes = new Message();
                                mes.Msg = ISUPDemo.WM_ADD_DEV;
                                mes.LParam = ptrTemp;
                                g_deviceTree.ProDevStatu(mes);


                                //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                        else
                        {
                            if (struDevInfo.struRegInfo.byDeviceID.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                               
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);

                                    Message mes = new Message();
                                    mes.Msg = ISUPDemo.WM_ADD_DEV;
                                    mes.LParam = ptrTemp;
                                    g_deviceTree.ProDevStatu(mes);

                                //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    
                    Marshal.StructureToPtr(struTemp, ptrTemp, false);

                        Message mes = new Message();
                        mes.Msg = ISUPDemo.WM_ADD_DEV;
                        mes.LParam = ptrTemp;
                        g_deviceTree.ProDevStatu(mes);

                    //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                    return true;
                }
             
                return false;
            }
            else if (HCEHomeCMS.ENUM_DEV_OFF == dwDataType)
            {
            
                Marshal.StructureToPtr(iUserID, ptrTemp, false);

                    Message mes = new Message();
                    mes.Msg = ISUPDemo.WM_DEL_DEV;
                    mes.LParam = ptrTemp;
                    g_deviceTree.ProDevStatu(mes);

                //PostMessage(m_treeHandle, ISUPDemo.WM_DEL_DEV, ptrTemp, ptrTemp);
                return false;
            }
            else if(HCEHomeCMS.ENUM_DEV_AUTH==dwDataType)
            {
                for(int i=0;i<MAX_DEVICES;++i)
                {
                    if(-1==DeviceTree.g_struDeviceInfo[i].iDeviceIndex)
                    {
                        struDevInfo.struRegInfo.byDeviceID.CopyTo(struTemp.byDeviceID, 0);
                        struTemp.dwVersion = 5;
                        //if (struDevInfo.struRegInfo.byDeviceID.Equals(m_stAccessDeviceList[i].sSerialNumber))
                        //{
               
                        Marshal.StructureToPtr(struTemp, ptrTemp, false);
                        Message mes = new Message();
                        mes.Msg = ISUPDemo.WM_ADD_DEV;
                        mes.LParam = ptrTemp;
                        g_deviceTree.ProDevStatu(mes);

                        //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                        //}
                        break;
                    }
                    else
                    {
                        m_ConvertModel.UTF82A(struDevInfo.struRegInfo.byDeviceID, struDevInfo.struRegInfo.byDeviceID, HCEHomePublic.MAX_DEVICE_ID_LEN, ref iOutLen);
                        if (Enumerable.SequenceEqual(DeviceTree.g_struDeviceInfo[i].byDeviceID, struDevInfo.struRegInfo.byDeviceID))
                        {
                            byte[] temp = new byte[32];
                            DeviceTree.g_struDeviceInfo[i].byEhomeKey.CopyTo(temp, 0);
                            Marshal.Copy(temp, 0, pInBuffer, 32);
                            break;
                        }
                    }
                }
            }
            else if(HCEHomeCMS.ENUM_DEV_SESSIONKEY==dwDataType)
            {
                HCEHomePublic.NET_EHOME_DEV_SESSIONKEY devSessionkey = new HCEHomePublic.NET_EHOME_DEV_SESSIONKEY();
                devSessionkey.Init();
                struDevInfo.struRegInfo.byDeviceID.CopyTo(devSessionkey.sDeviceID, 0);
                struDevInfo.struRegInfo.bySessionKey.CopyTo(devSessionkey.sSessionKey, 0);
                HCEHomeCMS.NET_ECMS_SetDeviceSessionKey(ref devSessionkey);
                HCEHomeAlarm.NET_EALARM_SetDeviceSessionKey(ref devSessionkey);
            }
            else if(HCEHomeCMS.ENUM_DEV_DAS_REQ==dwDataType)
            {
                string szLocalIP="";
                //IntPtr ptrLocalIP = Marshal.AllocHGlobal(128);
                //ptrLocalIP=Marshal.StringToHGlobalAnsi(szLocalIP);
                //szLocalIP = SelectIP.IpAddressList[0].ToString();
                IntPtr ptrLocalIP = IntPtr.Zero;
                int dwPort = 0;
                GetAddressByType(3, 0, ref ptrLocalIP, 128, ref dwPort, 4);
                szLocalIP = Marshal.PtrToStringAnsi(ptrLocalIP);
                if(0==dwPort)
                {
                    dwPort = m_nPort;
                }
                string portTemp=dwPort.ToString();
                string strInBuffer="{\"Type\":\"DAS\",\"DasInfo\":{\"Address\":\""+szLocalIP+"\","+
                "\"Domain\":\"test.ys7.com\",\"ServerID\":\"das_"+szLocalIP+"_"+portTemp+"\",\"Port\":"+portTemp+",\"UdpPort\":"+portTemp+"}}";

                byte[] byTemp = System.Text.Encoding.Default.GetBytes(strInBuffer);
                Marshal.Copy(byTemp, 0, pInBuffer, byTemp.Length);

            }
            else if (HCEHomeCMS.ENUM_DEV_ADDRESS_CHANGED == dwDataType)
            {
                byte[] byEhomeKey=new byte[32];
                for(int i=0;i<MAX_DEVICES;++i)
                {
                    if (DeviceTree.g_struDeviceInfo[i].iDeviceIndex != -1)
                    {
                        DeviceTree.g_struDeviceInfo[i].byEhomeKey.CopyTo(byEhomeKey, 0);
                        break;
                    }
                }
                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                Message mes = new Message();
                mes.Msg = ISUPDemo.WM_CHANGE_IP;
                mes.LParam = ptrTemp;
                g_deviceTree.ProDevStatu(mes);

                //PostMessage(m_treeHandle, ISUPDemo.WM_CHANGE_IP, ptrTemp, ptrTemp);
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 0, "IP changed,register again");

         

                m_ConvertModel.UTF82A(struDevInfo.struRegInfo.byDeviceID, struDevInfo.struRegInfo.byDeviceID, HCEHomePublic.MAX_DEVICE_ID_LEN, ref iOutLen);
                m_ConvertModel.UTF82A(struDevInfo.struRegInfo.sDeviceSerial, struDevInfo.struRegInfo.sDeviceSerial, HCEHomePublic.NET_EHOME_SERIAL_LEN, ref iOutLen);

                struDevInfo.struRegInfo.byDeviceID.CopyTo(struTemp.byDeviceID, 0);
                struTemp.iLoginID = iUserID;
                struDevInfo.struRegInfo.sDeviceSerial.CopyTo(struTemp.sDeviceSerial, 0);


                byte[] szDeviceSerial = new byte[HCEHomePublic.NET_EHOME_SERIAL_LEN + 1];
                struDevInfo.struRegInfo.sDeviceSerial.CopyTo(szDeviceSerial, 0);
                if (2==struDevInfo.struRegInfo.byDevProtocolVersion[0])
                {
                    struTemp.dwVersion = 2;
                }
                else if (4 == struDevInfo.struRegInfo.byDevProtocolVersion[0])
                {
                    struTemp.dwVersion = 4;
                }
                else
                {
                    struTemp.dwVersion = 5;
                }

                if (m_bUseAccessList)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (struTemp.dwVersion >= 4)
                        {
                            if (struDevInfo.struRegInfo.sDeviceSerial.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                       
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                                Message mesg = new Message();
                                mesg.Msg = ISUPDemo.WM_ADD_DEV;
                                mesg.LParam = ptrTemp;
                                g_deviceTree.ProDevStatu(mesg);
                                //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                        else
                        {
                            if (struDevInfo.struRegInfo.byDeviceID.Equals(m_stAccessDeviceList[i].sSerialNumber))
                            {
                                Marshal.StructureToPtr(struTemp, ptrTemp, false);
                                Message mesg = new Message();
                                mesg.Msg = ISUPDemo.WM_ADD_DEV;
                                mesg.LParam = ptrTemp;
                                g_deviceTree.ProDevStatu(mesg);
                                //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Marshal.StructureToPtr(struTemp, ptrTemp, false);
                    Message mesa = new Message();
                    mesa.Msg = ISUPDemo.WM_ADD_DEV;
                    mesa.LParam = ptrTemp;
                    g_deviceTree.ProDevStatu(mesa);

                    //PostMessage(m_treeHandle, ISUPDemo.WM_ADD_DEV, ptrTemp, ptrTemp);
                    return true;
                }

                return false;
            }

            return true;
        }

        //3, 0, ref szLocalIP, 128, ref dwPort, 4
      
        public bool fnPREVIEW_NEWLINK_CB(int iLinkHandle, ref HCEHomeStream.NET_EHOME_NEWLINK_CB_MSG pNewLinkCBMsg, IntPtr pUserData)
        {

            m_iLinkHandle = iLinkHandle;
            m_iPlayHandle = iLinkHandle;
            //Int32 dwConvertLen = 0;
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

        public static void fnPREVIEW_DATA_CB(int iPreviewHandle, ref HCEHomeStream.NET_EHOME_PREVIEW_CB_MSG pPreviewCBMsg, IntPtr pUserData)
        {
            //bool bRet = false;
            //m_iRealHandle = iPreviewHandle;
            int iWinIndex = (int)pUserData;
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
       public bool AlarmMsgCallBack(int iHandle, IntPtr pAlarmMsg/*ref HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmMsg*/, IntPtr pUser)
       {
           HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmMsg = new HCEHomeAlarm.NET_EHOME_ALARM_MSG();
           struAlarmMsg.sSerialNumber = new byte[HCEHomeAlarm.NET_EHOME_SERIAL_LEN];
           struAlarmMsg.byRes = new byte[20];
           struAlarmMsg = (HCEHomeAlarm.NET_EHOME_ALARM_MSG)Marshal.PtrToStructure(pAlarmMsg, typeof(HCEHomeAlarm.NET_EHOME_ALARM_MSG));
           if (m_bUseAccessList && !IsInAccessDeviceList(struAlarmMsg.sSerialNumber))
           {
               return false;
           }
//           ProcessAlarmData((int)struAlarmMsg.dwAlarmType, struAlarmMsg.pAlarmInfo, (int)struAlarmMsg.dwAlarmInfoLen,
//              ref struAlarmMsg.pXmlBuf, (int)struAlarmMsg.dwXmlBufLen, struAlarmMsg.pHttpUrl, (int)struAlarmMsg.dwHttpUrlLen);
           SendMessage(m_logListHandle, ISUPDemo.WM_LISTENED_ALARM, struAlarmMsg.pXmlBuf, pAlarmMsg);
           return true;
       }
        #endregion
        //Newly Add Content
//        public void ProcessAlarmData(int dwAlarmType, IntPtr pStru, int dwStruLen, ref IntPtr pXml,
//          int dwXmlLen, IntPtr pUrl, int dwUrlLen)
//        {
// 
//        }



       private void ISUPDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPreviewListen();
            HCEHomePublic.NET_EHOME_AMS_ADDRESS struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
            struAmsAddr.dwSize = Marshal.SizeOf(struAmsAddr);
            struAmsAddr.byEnable = 2;
            IntPtr ptrAmsAddr = Marshal.AllocHGlobal(struAmsAddr.dwSize);
            Marshal.StructureToPtr(struAmsAddr, ptrAmsAddr, false);
            HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, ptrAmsAddr);
            Marshal.FreeHGlobal(ptrAmsAddr);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lUdpAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lTcpAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lEhome50AlarmHandle);
            HCEHomeAlarm.NET_EALARM_Fini();
            HCEHomeCMS.NET_ECMS_StopListen(0);
            HCEHomeCMS.NET_ECMS_Fini();
            HCEHomeStream.NET_ESTREAM_Fini();
            if(g_bSS_Enable)
            {
                HCEHomeSS.NET_ESS_StopListen(m_lSSHandle);
                HCEHomeSS.NET_ESS_Fini();
            }
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

        private void Alarm_Click(object sender, EventArgs e)
        {

            this.m_previewWnd.Visible = false;
            this.g_PlayBack.Visible = false;
            this.g_Alarm.Visible = true;
            this.m_panelDeviceLog.Visible = false;
            this.g_TotalPanel.Controls.Clear();
            this.g_TotalPanel.Controls.Add(g_Alarm);

            g_Alarm.CheckInitParam(m_lUdpAlarmHandle, m_lTcpAlarmHandle, m_lEhome50AlarmHandle);
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

        private void buttonPlayBack_Click(object sender, EventArgs e)
        {
            this.m_previewWnd.Visible = false;
            this.g_PlayBack.Visible = true;
            this.m_panelDeviceLog.Visible = true;
            this.g_TotalPanel.Controls.Clear();
            this.g_TotalPanel.Controls.Add(g_PlayBack);
            this.g_TotalPanel.Controls.Add(m_panelDeviceLog);

            g_PlayBack.CheckInitParam();
        }

        private void radioBtnLocalLog_CheckedChanged(object sender, EventArgs e)
        {
//             if (m_rdoLocalLog.Checked)
//             {
//                 g_formList.showList(0);
//             }
//             else if (m_rdoAlarmInformation.Checked)
//             {
//                 g_formList.showList(1);
//             }
        }

        private void m_rdoAlarmInformation_CheckedChanged(object sender, EventArgs e)
        {
//             if (m_rdoLocalLog.Checked)
//             {
//                 g_formList.showList(0);
//             }
//             else if (m_rdoAlarmInformation.Checked)
//             {
//                 g_formList.showList(1);
//             }
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {
            this.m_MenuElse.Show(btnOthers, this.btnOthers.PointToClient(Cursor.Position), ToolStripDropDownDirection.BelowRight);
        }

        private void BtnManage_Click(object sender, EventArgs e)
        {
            this.m_MenuMoudle.Show(BtnManage, this.BtnManage.PointToClient(Cursor.Position), ToolStripDropDownDirection.BelowRight);
        }



        private void GpsInfoSet_Click(object sender, EventArgs e)
        {
            GPSInfoSet GPSInfoDlg = new GPSInfoSet();
            GPSInfoDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            GPSInfoDlg.ShowDialog();
        }

        private void CommonConfigure_Click(object sender, EventArgs e)
        {
            CommonConfigure CommonConfigureDlg = new CommonConfigure();
            CommonConfigureDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            CommonConfigureDlg.ShowDialog();
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

        //private void ISUPDemoExit_Click(object sender, EventArgs e)
        //{
        //    StopPreviewListen();
        //    HCEHomePublic.NET_EHOME_AMS_ADDRESS struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
        //    struAmsAddr.dwSize = Marshal.SizeOf(struAmsAddr);
        //    struAmsAddr.byEnable = 2;
        //    IntPtr ptrAmsAddr = Marshal.AllocHGlobal(struAmsAddr.dwSize);
        //    Marshal.StructureToPtr(struAmsAddr, ptrAmsAddr, false);
        //    HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, ptrAmsAddr);
        //    Marshal.FreeHGlobal(ptrAmsAddr);
        //    HCEHomeAlarm.NET_EALARM_StopListen(m_lUdpAlarmHandle);
        //    HCEHomeAlarm.NET_EALARM_StopListen(m_lAlarmHandle);
        //    HCEHomeAlarm.NET_EALARM_StopListen(m_lCmsAlarm);
        //    HCEHomeAlarm.NET_EALARM_Fini();
        //    HCEHomeCMS.NET_ECMS_StopListen(0);
        //    HCEHomeCMS.NET_ECMS_Fini();
        //    HCEHomeStream.NET_ESTREAM_Fini();
        //    this.Close();
        //}
        #endregion

        private void ISUPDemo_Load(object sender, EventArgs e)
        {
            g_formList = DeviceLogList.Instance();

           
            m_iChanelType = HCEHomeCMS.DEMO_CHANNEL_TYPE_ZERO;
            m_hWnd = this.Handle;
            m_hPreviewWnd = m_previewWnd.Handle;
            m_treeHandle = g_deviceTree.Handle;
//            m_rdoLocalLog.Select();

            for (int i = 0; i < GlobalDefinition.MAX_DEVICES; i++)
            {
                DeviceTree.g_struDeviceInfo[i].Init();
                for (int j = 0; j < GlobalDefinition.MAX_CHAN_NUM_DEMO; j++)
                {
                    DeviceTree.g_struDeviceInfo[i].struChanInfo[j].Init();
                }
            }

            this.m_panelDeviceLog.Controls.Add(g_formList);
            this.m_panelDeviceTree.Controls.Add(g_deviceTree);
            g_formList.Dock = DockStyle.Fill;
            g_deviceTree.Dock = DockStyle.Fill;
            m_logListHandle = g_formList.Handle;
//            m_alarmListHandle = g_alarmInfoList.Handle;

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
            PlayBack.m_csLocalIPAddr = m_csLocalIP;
            PlayBack.m_lLocalPort = m_nPort;

            m_struServInfo.Init();

            if (m_EnableXmlDoc)
            {
                InitParamFromXML();
            }
            else
            {
                m_struServInfo = m_struServUIConfig;
            }
            InitChildWindow();
            InitLib();
            InitPreviewListenParam();
        }

       

       
        private void BtnConfig_Click(object sender, EventArgs e)
        {
            this.m_ConfigStrip.Show(BtnConfig, this.BtnConfig.PointToClient(Cursor.Position), ToolStripDropDownDirection.BelowRight);
        }
       

        private void storageServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StorageServer SSdialog = new StorageServer();
            SSdialog.m_csListenIP = m_csLocalIP;
            SSdialog.m_nPort = PicServerPort;
            SSdialog.m_csPicServerIP = PicServerip;
            SSdialog.ShowDialog();
        }

        PT_PROXY PT_ProxyDlg = new PT_PROXY();
        private void pTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PT_ProxyDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            PT_ProxyDlg.ShowDialog();
        }

        private void UserManageStrip_Click(object sender, EventArgs e)
        {
            UserManager userManagerDlg = new UserManager();
            userManagerDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            userManagerDlg.ShowDialog();
        }

        private void FingerManageStrip_Click(object sender, EventArgs e)
        {
            FingerManager fingerManageDlg = new FingerManager();
            fingerManageDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            fingerManageDlg.ShowDialog();
        }

        private void CardManageStrip_Click(object sender, EventArgs e)
        {
            CardManager cardManagerDlg = new CardManager();
            cardManagerDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            cardManagerDlg.ShowDialog();
        }

        private void FaceManageStrip_Click(object sender, EventArgs e)
        {
            FaceManage faceManageDlg = new FaceManage();
            faceManageDlg.m_struPictureSever = m_struServInfo.struPictureSever;
            faceManageDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            faceManageDlg.ShowDialog();
        }

        private void WeekPlanStrip_Click(object sender, EventArgs e)
        {
            WeekPlan weekPlanDlg = new WeekPlan();
            weekPlanDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            weekPlanDlg.ShowDialog();
        }

        private void CommonConfigStrip_Click(object sender, EventArgs e)
        {
            CommonConfigure CommonConfigureDlg = new CommonConfigure();
            CommonConfigureDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            CommonConfigureDlg.ShowDialog();
        }

        private void XMLConfigStrip_Click(object sender, EventArgs e)
        {
            XMLConfig xmlConfigDlg = new XMLConfig();
            xmlConfigDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            xmlConfigDlg.ShowDialog();
        }

        private void ExitStrip_Click(object sender, EventArgs e)
        {
            StopPreviewListen();
            HCEHomePublic.NET_EHOME_AMS_ADDRESS struAmsAddr = new HCEHomePublic.NET_EHOME_AMS_ADDRESS();
            struAmsAddr.dwSize = Marshal.SizeOf(struAmsAddr);
            struAmsAddr.byEnable = 2;
            IntPtr ptrAmsAddr = Marshal.AllocHGlobal(struAmsAddr.dwSize);
            Marshal.StructureToPtr(struAmsAddr, ptrAmsAddr, false);
            HCEHomeCMS.NET_ECMS_SetSDKLocalCfg(HCEHomePublic.NET_EHOME_LOCAL_CFG_TYPE.AMS_ADDRESS, ptrAmsAddr);
            Marshal.FreeHGlobal(ptrAmsAddr);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lUdpAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lTcpAlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lEhome50AlarmHandle);
            HCEHomeAlarm.NET_EALARM_StopListen(m_lCmsAlarm);
            HCEHomeAlarm.NET_EALARM_Fini();
            HCEHomeCMS.NET_ECMS_StopListen(0);
            HCEHomeCMS.NET_ECMS_Fini();
            HCEHomeStream.NET_ESTREAM_Fini();
            this.Close();
        }

        private void AcsEventStrip_Click(object sender, EventArgs e)
        {
            GetACSEvent aceEventDlg = new GetACSEvent();
            aceEventDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            aceEventDlg.Show();
        }

        private void VoiceTalkStrip_Click(object sender, EventArgs e)
        {
            this.m_previewWnd.Visible = false;
            this.g_AudioTalk.Visible = true;
            this.g_TotalPanel.Controls.Clear();
            this.g_TotalPanel.Controls.Add(g_AudioTalk);
            this.g_TotalPanel.Controls.Add(m_panelDeviceLog);

            g_AudioTalk.CheckInitParam();
        }


        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String m_strInputXml;
        String m_strOutputXml;
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];

        public class ResponseStatus
        {
            public string requestURL { get; set; }
            public string statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public string errorCode { get; set; }
            public string errorMsg { get; set; }
        }

        private void RebooTooltStrip_Click(object sender, EventArgs e)
        {
            //针对ISUP4.0/5.0设备，使用透传ISAPI方式实现重启功能
            String m_strInputXml = String.Empty;
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 20 * 1024;
            GlobalDefinition tc = new GlobalDefinition();
            tc.ConfigMethod(ref ptrCfg, "/ISAPI/System/reboot", m_strInputXml, OutBufferLen);

            int m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_iLoginID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_iLoginID, ptrCfg))
            {
                MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
            }
            else
            {
                CTinyXmlTrans xmlDoc = new CTinyXmlTrans();
                xmlDoc.Parse(Marshal.PtrToStringAnsi(struPTXML.pOutBuffer));
                if (xmlDoc.FindElem("ResponseStatus") && xmlDoc.IntoElem())
                {
                    if (xmlDoc.FindElem("statusCode"))
                    {
                        if (xmlDoc.GetData() == "1")
                        {
                            MessageBox.Show("Set Data Succ!");
                        }
                    }
                }
            }
            Marshal.FreeHGlobal(ptrCfg);
        }

        private void UpdataToolStrip_Click(object sender, EventArgs e)
        {
            this.m_previewWnd.Visible = false;
            this.g_Update.Visible = true;
            this.g_TotalPanel.Controls.Clear();
            this.g_TotalPanel.Controls.Add(g_Update);
            this.g_TotalPanel.Controls.Add(m_panelDeviceLog);

            g_Update.CheckInitParam();           
        }

        private void TimingToolStrip_Click(object sender, EventArgs e)
        {
            HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM struXMLCfg = new HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM();

            string m_strInputXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" + 
            "<PPVSPMessage>\r\n" + 
            "<Version>4.0</Version>\r\n" + 
            "<Sequence>1</Sequence>\r\n" + 
            "<CommandType>REQUEST</CommandType>\r\n" + 
            "<Method>CONTROL</Method>\r\n" + 
            "<Command>ADJUSTTIME</Command>\r\n" + 
            "<Params>\r\n" + 
            "<TimeZone>0</TimeZone>\r\n" + 
            "<Time>" + DateTime.Now.ToLocalTime().ToString() + "</Time>\r\n" + 
            "</Params>\r\n" + 
            "</PPVSPMessage>";

            m_szInputBuffer = Encoding.UTF8.GetBytes(m_strInputXml);
            struXMLCfg.lpInbuffer = Marshal.AllocHGlobal(1500);
            for (int i = 0; i < 1500; i++)
            {
                Marshal.WriteByte(struXMLCfg.lpInbuffer, i, 0);
            }
            Marshal.Copy(m_szInputBuffer, 0, struXMLCfg.lpInbuffer, m_szInputBuffer.Length);
            struXMLCfg.dwInBufferSize = 1500;
            struXMLCfg.lpOutBuffer = Marshal.AllocHGlobal(1024 * 100);
            for (int i = 0; i < 1024 * 100; i++)
            {
                Marshal.WriteByte(struXMLCfg.lpOutBuffer, i, 0);
            }
            struXMLCfg.dwOutBufferSize = 102400;

            struXMLCfg.lpStatusBuffer = Marshal.AllocHGlobal(1024 * 100);
            for (int i = 0; i < 1024 * 100; i++)
            {
                Marshal.WriteByte(struXMLCfg.lpStatusBuffer, i, 0);
            }
            struXMLCfg.dwStatusBufferSize = (uint)(1024 * 100);
            struXMLCfg.dwSize = (uint)Marshal.SizeOf(struXMLCfg);
            struXMLCfg.dwRecvTimeOut = 30 * 1000;
            
            
            IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struXMLCfg));
            Marshal.StructureToPtr(struXMLCfg, ptrCfg, false);
            int dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM));

            string strTemp = "";
            if (!HCEHomeCMS.NET_ECMS_XMLRemoteControl(DeviceTree.g_struDeviceInfo[g_deviceTree.m_iCurDeviceIndex].iLoginID, ptrCfg, dwSize))
            {
                string str = string.Format("NET_ECMS_XMLRemoteControl err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
            }
            else
            {
                struXMLCfg = (HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM)Marshal.PtrToStructure(ptrCfg, typeof(HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM));
                strTemp = Marshal.PtrToStringAnsi(struXMLCfg.lpStatusBuffer, (int)struXMLCfg.dwStatusBufferSize);
            }
            Marshal.FreeHGlobal(ptrCfg);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PlanTemplate planDlg = new PlanTemplate();
            planDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            planDlg.ShowDialog();
        }
    }
}
