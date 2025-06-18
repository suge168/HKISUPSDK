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

using ISUPDemo;
using ISUPDemo.Public;
using ISUPDemo.Language;
using System.Threading;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Xml;
using TINYXMLTRANS;

namespace ISUPDemo.UI
{
    public partial class Alarm : UserControl
    {

        //日志实例
        private DeviceLogList g_formList = DeviceLogList.Instance();
        private static Alarm g_AlarmInfoList = new Alarm();
        public HCEHomeAlarm.EHomeMsgCallBack ptrAlarmCB = null;
        private HCEHomeCMS.DEVICE_REGISTER_CB EHOME_REGISTER_Func = null;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        public Public.ConvertModel m_ConvertModel = new Public.ConvertModel();
        private static SynchronizationContext m_SyncContext = null;
        public XmlDocument doc = new XmlDocument();

        public static int m_lUdpAlarmHandle = -1;
        public static int m_lTcpAlarmHandle = -1;
        public static int m_lEhome50AlarmHandle = -1;

        public class jsonFaceComparisonResult
        {
            public jsonAlarmInfo m_jsonAlarmInfo { get; set; }
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
            public float similarity { get; set; }
            public jsonFace m_jsonFace { get; set; }
        }jsonFaceComparisonResult m_faceComparisonResult;

        public class jsonFace
        {
            public int age { get; set; }
            public string mask { get; set; }
            public string URL { get; set; }
            public string faceScore { get; set; }
            public string currTemperature { get; set; }

        }jsonFace m_jsonFace;

        public class jsonFaceCaptureResult
        {
            public jsonAlarmInfo m_jsonAlarmInfo { get; set; }
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
            public float similarity { get; set; }
            public jsonFace m_jsonFace { get; set; }
        }jsonFaceCaptureResult m_faceCaptureResult;

        public class jsonAlarmInfo
        {
            public string ipAddress { get; set; }
            public int portNo { get; set; }
            public string protocol { get; set; }
            public string macAddress { get; set; }
            public int channelID { get; set; }
            public string dateTime { get; set; }
            public int activePostCount { get; set; }
            public string eventType { get; set; }
            public string eventState { get; set; }
            public string eventDescription { get; set; }
            public string deviceID { get; set; }
        }jsonAlarmInfo m_jsonAlarmInfo;


        public Alarm()
        {
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
            AlarmInfoBox = csAlarmBox;
            AlarmPicUrl = csPicUrl;
            AlarmInfoList = listViewAlarmInfo;
        }

        public static Alarm Instance()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            return g_AlarmInfoList;
        }


        public void CheckInitParam(Int32 lUdpAlarmHandle, Int32 lTcpAlarmHandle, Int32 lEhome50AlarmHandle)
        {
            m_lUdpAlarmHandle = lUdpAlarmHandle;
            m_lEhome50AlarmHandle = lTcpAlarmHandle;
            m_lEhome50AlarmHandle = lEhome50AlarmHandle;
            this.Refresh();
        }


        public static RichTextBox AlarmInfoBox = new RichTextBox();
        public static ListView AlarmInfoList = new ListView();
        public static Label AlarmPicUrl = new Label();
        public static void ShowAlarmInfo(IntPtr pAlarmMsg)
        {
          
            HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmMsg = new HCEHomeAlarm.NET_EHOME_ALARM_MSG();
            struAlarmMsg.Init();
            struAlarmMsg = (HCEHomeAlarm.NET_EHOME_ALARM_MSG)Marshal.PtrToStructure(pAlarmMsg, typeof(HCEHomeAlarm.NET_EHOME_ALARM_MSG));

            if (struAlarmMsg.pHttpUrl != IntPtr.Zero)
            {
                struAlarmMsg.dwAlarmType = HCEHomeAlarm.EHOME_ISAPI_ALARM;                              
            }


            switch (struAlarmMsg.dwAlarmType)
            {
                case HCEHomeAlarm.EHOME_ALARM_UNKNOWN:
                    break;
                case HCEHomeAlarm.EHOME_ISAPI_ALARM:
                    ProcessHttpAlarmInfo(struAlarmMsg.pAlarmInfo);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_NOTICE_PICURL:
                case HCEHomeAlarm.EHOME_ALARM_FACESNAP_REPORT:
                case HCEHomeAlarm.EHOME_ALARM:
                case HCEHomeAlarm.EHOME_ALARM_GPS:
                case HCEHomeAlarm.EHOME_ALARM_HEATMAP_REPORT:
                case HCEHomeAlarm.EHOME_ALARM_CID_REPORT:
				case HCEHomeAlarm.EHOME_ALARM_ACS:
                    ProcessXmlAlarmInfo(pAlarmMsg);
                    break;
                default:
                    break;
            }
        }

        public static void ProcessGPSAlarmInfo(IntPtr ptrAlarmParam)
        {
            HCEHomeAlarm.NET_EHOME_GPS_INFO struGpsInfo = new HCEHomeAlarm.NET_EHOME_GPS_INFO();
            struGpsInfo = (HCEHomeAlarm.NET_EHOME_GPS_INFO)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_GPS_INFO));

            AlarmInfoList.BeginUpdate();
            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            listItem.SubItems.Add("GPS");
            string strinfo = "GPS info: \r\nDevice ID[" + Encoding.UTF8.GetString(struGpsInfo.byDeviceID) + "],SampleTime[" + Encoding.UTF8.GetString(struGpsInfo.bySampleTime) + "}, Longitude[" + struGpsInfo.dwLongitude + "],Latitude[" + struGpsInfo.dwLatitude + "]";
            listItem.SubItems.Add(strinfo);

            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();
        }

        public static void ProcessFaceSnapAlarmInfo(IntPtr ptrAlarmParam)
        {
            HCEHomeAlarm.NET_EHOME_FACESNAP_REPORT struFaceSnap = new HCEHomeAlarm.NET_EHOME_FACESNAP_REPORT();
            struFaceSnap = (HCEHomeAlarm.NET_EHOME_FACESNAP_REPORT)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_FACESNAP_REPORT));

            AlarmInfoList.BeginUpdate();
            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            listItem.SubItems.Add("FACESNAP");
            string strinfo = "Face Snap Info: \r\nDevice ID[" + struFaceSnap.byDeviceID.ToString() + "],dwFaceScore[" + struFaceSnap.dwFaceScore + "],byAgeGroup[" + struFaceSnap.struHumanFeature.byAgeGroup + "]";
            listItem.SubItems.Add(strinfo);

            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();

            if (struFaceSnap.byFacePicUrl.Length != 0)
            {

            }
        }


        public static void ProcessHeatMapAlarmInfo(IntPtr ptrAlarmParam)
        {
            HCEHomeAlarm.NET_EHOME_HEATMAP_REPORT struHeatmapInfo = new HCEHomeAlarm.NET_EHOME_HEATMAP_REPORT();
            struHeatmapInfo = (HCEHomeAlarm.NET_EHOME_HEATMAP_REPORT)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_HEATMAP_REPORT));
            

            AlarmInfoList.BeginUpdate();
            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            listItem.SubItems.Add("HEATMAP");
            string strinfo = "Heatmap report: Device ID[" + new string(struHeatmapInfo.byDeviceID) + "],dwTimeHeatMapValue[" + struHeatmapInfo.struHeatmapValue.dwTimeHeatMapValue +
                "}, dwLineValue[" + struHeatmapInfo.struPixelArraySize.dwLineValue + "],dwColumnValue[" + struHeatmapInfo.struPixelArraySize.dwColumnValue + "]";
            listItem.SubItems.Add(strinfo);

            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();
        }


        public static void ProcessEhomeAlarmInfo(IntPtr ptrAlarmParam)
        {
            HCEHomeAlarm.NET_EHOME_ALARM_INFO struAlarmInfo = new HCEHomeAlarm.NET_EHOME_ALARM_INFO();
            struAlarmInfo.Init();
            struAlarmInfo = (HCEHomeAlarm.NET_EHOME_ALARM_INFO)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_ALARM_INFO));

            AlarmInfoList.BeginUpdate();
            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            listItem.SubItems.Add("EHOME_ALARM");
            string strinfo = "Basic Alarm: Device ID[" + Encoding.UTF8.GetString(struAlarmInfo.szDeviceID) + "], Alarm Type[" + struAlarmInfo.dwAlarmType + "}, Action[" + struAlarmInfo.dwAlarmAction + "]";
            listItem.SubItems.Add(strinfo);

            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();
        }

        public static void ProcessXmlAlarmInfo(IntPtr ptrAlarmParam)
        {
            HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmMsg = new HCEHomeAlarm.NET_EHOME_ALARM_MSG();
            struAlarmMsg.Init();
            struAlarmMsg = (HCEHomeAlarm.NET_EHOME_ALARM_MSG)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_ALARM_MSG));

            AlarmInfoList.BeginUpdate();
            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            try
            {
                if (struAlarmMsg.pXmlBuf != IntPtr.Zero)
                {
                    try
                    {
                        CTinyXmlTrans xmlDoc = new CTinyXmlTrans();
                        //防止报文信息中包含中文，进行编码格式的转换
                        byte[] byXmlData = new byte[struAlarmMsg.dwXmlBufLen];
                        Marshal.Copy(struAlarmMsg.pXmlBuf, byXmlData, 0, (int)struAlarmMsg.dwXmlBufLen);
                        string XmlAlarmInfo = System.Text.Encoding.UTF8.GetString(byXmlData);
                        xmlDoc.Parse(XmlAlarmInfo);
                        if (xmlDoc.FindElem("PPVSPMessage") && xmlDoc.IntoElem())
                        {
                            if (xmlDoc.FindElem("Command"))
                            {
                                listItem.SubItems.Add(xmlDoc.GetData());
                            }
                            if (xmlDoc.FindElem("Params") && xmlDoc.IntoElem())
                            {
                                if (xmlDoc.FindElem("DeviceID"))
                                {
                                    listItem.SubItems.Add(xmlDoc.GetData());
                                }
                            }
                            listItem.SubItems.Add(Marshal.PtrToStringAnsi(struAlarmMsg.pXmlBuf));
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    listItem.SubItems.Add(Marshal.PtrToStringAnsi(struAlarmMsg.pXmlBuf));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            listItem.SubItems.Add(Marshal.PtrToStringAnsi(struAlarmMsg.pXmlBuf));
            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();

        }

        public static void ProcessPicUrlAlarmInfo(IntPtr ptrAlarmParam)
        {
            HCEHomeAlarm.NET_EHOME_NOTICE_PICURL struNoticePic = new HCEHomeAlarm.NET_EHOME_NOTICE_PICURL();
            struNoticePic = (HCEHomeAlarm.NET_EHOME_NOTICE_PICURL)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_NOTICE_PICURL));

            AlarmInfoList.BeginUpdate();
            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            listItem.SubItems.Add("PICURL");
            string strinfo = "Pic Url Info: \r\nDevice ID[" + new string(struNoticePic.byDeviceID) + "],wPicType[" + struNoticePic.wPicType + "],PicUrl[" + struNoticePic.byPicUrl + "]";
            listItem.SubItems.Add(strinfo);

            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();
        }

        public static void ProcessHttpAlarmInfo(IntPtr ptrAlarmParam)
        {
           
            HCEHomeAlarm.NET_EHOME_ALARM_ISAPI_INFO struISAPIAlarm = new HCEHomeAlarm.NET_EHOME_ALARM_ISAPI_INFO();
            struISAPIAlarm = (HCEHomeAlarm.NET_EHOME_ALARM_ISAPI_INFO)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_ALARM_ISAPI_INFO));

            ListViewItem listItem = new ListViewItem();
            listItem.Text = DateTime.Now.ToLocalTime().ToString();

            if (struISAPIAlarm.pAlarmData != IntPtr.Zero)
            {
                if(struISAPIAlarm.byDataType == 1)
                {
                    try
                    {
                        CTinyXmlTrans xmlDoc = new CTinyXmlTrans();
                        //防止XML报文中出现中文时，进行字符编码格式的转换
                        byte[] byAlarmData = new byte[struISAPIAlarm.dwAlarmDataLen];
                        Marshal.Copy(struISAPIAlarm.pAlarmData, byAlarmData, 0, struISAPIAlarm.dwAlarmDataLen);
                        string AlarmInfo = System.Text.Encoding.UTF8.GetString(byAlarmData);
                        xmlDoc.Parse(AlarmInfo);
                        if (xmlDoc.FindElem("EventNotificationAlert") && xmlDoc.IntoElem())
                        {
                            if(xmlDoc.FindElem("eventType"))
                            {
                                listItem.SubItems.Add("ISAPI_ALARM: " + xmlDoc.GetData());
                            }
                            if (xmlDoc.FindElem("deviceID"))
                            {
                                listItem.SubItems.Add(xmlDoc.GetData());                               
                            }
                            else
                            {
                                listItem.SubItems.Add("null");     
                            }
                            listItem.SubItems.Add(Marshal.PtrToStringAnsi(struISAPIAlarm.pAlarmData));

                            if(xmlDoc.FindElem("ANPR")&& xmlDoc.IntoElem())
                            {
                                do
                                {
                                    if (xmlDoc.FindElem("pictureInfoList")&& xmlDoc.IntoElem())
                                    {
                                        if (xmlDoc.FindElem("pictureInfo")&& xmlDoc.IntoElem())
                                        {
                                             if (xmlDoc.FindElem("pictureURL"))
                                             {
                                                 AlarmPicUrl.Text = xmlDoc.GetData();
                                             }
                                        }

                                    }
                                } while (xmlDoc.NextSibElem());
                            }
                            else if(xmlDoc.GetData() == "TMA")
                            {
                                if(xmlDoc.FindElem("visibleLightURL"))
                                {
                                    AlarmPicUrl.Text = xmlDoc.GetData();
                                }
                                if(xmlDoc.FindElem("thermalURL"))
                                {
                                    AlarmPicUrl.Text = xmlDoc.GetData();
                                }
                                if(xmlDoc.FindElem("thermalInfoURL"))
                                {
                                    AlarmPicUrl.Text = xmlDoc.GetData();
                                }
                            }                            
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }

                }
                else if (struISAPIAlarm.byDataType == 2)
                {
                    byte[] byAlarmData = new byte[struISAPIAlarm.dwAlarmDataLen];
                    Marshal.Copy(struISAPIAlarm.pAlarmData, byAlarmData, 0, struISAPIAlarm.dwAlarmDataLen);
                    string strOutJson = System.Text.Encoding.UTF8.GetString(byAlarmData);
                    
                    jsonAlarmInfo m_jsonAlarmInfo = new jsonAlarmInfo();
                    m_jsonAlarmInfo = JsonConvert.DeserializeObject<jsonAlarmInfo>(strOutJson);
                    listItem.SubItems.Add("ISAPI_ALARM: " + m_jsonAlarmInfo.eventType);

                    if (m_jsonAlarmInfo.deviceID != null)
                    {
                        listItem.SubItems.Add(m_jsonAlarmInfo.deviceID.ToString());
                    }
                    else
                    {
                        listItem.SubItems.Add("null");
                    }

                    listItem.SubItems.Add(strOutJson);

                   
                    if (m_jsonAlarmInfo.eventType == "faceCapture")
                    {
                        jsonFaceCaptureResult m_faceCaptureResult = new jsonFaceCaptureResult();
                        m_faceCaptureResult = JsonConvert.DeserializeObject<jsonFaceCaptureResult>(strOutJson);

                        AlarmPicUrl.Text = m_faceCaptureResult.m_jsonFace.URL;
                    }

                }
                else
                {
                    listItem.SubItems.Add("unknow");
                    listItem.SubItems.Add(Marshal.PtrToStringAnsi(struISAPIAlarm.pAlarmData));
                }
            }

            listItem.SubItems.Add(Marshal.PtrToStringAnsi(struISAPIAlarm.pAlarmData));
            AlarmInfoList.Items.Add(listItem);
            AlarmInfoList.EndUpdate();

        }    


        private void listViewAlarmInfo_Click(object sender, EventArgs e)
        {
            if (listViewAlarmInfo.SelectedItems.Count > 0)
            {
                try
                {
                     AlarmInfoBox.Text = listViewAlarmInfo.SelectedItems[0].SubItems[3].Text.ToString();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
               
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (-1 != m_lUdpAlarmHandle)
            {
                if (HCEHomeAlarm.NET_EALARM_StopListen(m_lUdpAlarmHandle) == false)
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "NET_EALARM_StopListen Failed");
                    StopResultText.Text = "Stop fail!" + "Err:" + HCEHomeAlarm.NET_EALARM_GetLastError().ToString();
                }
                else
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_EALARM_StopListen Success");
                    StopResultText.Text = "Stop Succ!";
                }
                m_lUdpAlarmHandle = -1;
            }
            if (-1 != m_lTcpAlarmHandle)
            {
                if (HCEHomeAlarm.NET_EALARM_StopListen(m_lTcpAlarmHandle) == false)
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "NET_EALARM_StopListen Failed");
                    StopResultText.Text = "Stop fail!" + "Err:" + HCEHomeAlarm.NET_EALARM_GetLastError().ToString();
                }
                else
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_EALARM_StopListen Success");
                    StopResultText.Text = "Stop Succ!";
                }
                m_lTcpAlarmHandle = -1;
            }
            if (-1 != m_lEhome50AlarmHandle)
            {
                if (HCEHomeAlarm.NET_EALARM_StopListen(m_lEhome50AlarmHandle) == false)
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "NET_EALARM_StopListen Failed");
                    StopResultText.Text = "Stop fail!" + "Err:" + HCEHomeAlarm.NET_EALARM_GetLastError().ToString();
                }
                else
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_EALARM_StopListen Success");
                    StopResultText.Text = "Stop Succ!";
                }
                m_lEhome50AlarmHandle = -1;
            }
        }      

    }
}
