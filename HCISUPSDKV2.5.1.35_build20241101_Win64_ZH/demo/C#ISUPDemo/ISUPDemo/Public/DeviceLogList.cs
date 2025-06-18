using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ISUPDemo.Language;
using System.IO;
using ISUPDemo.UI;

namespace ISUPDemo.Public
{
    public partial class DeviceLogList : UserControl
    {
        private object locker = new object();
        private HCEHomePublic.LOCAL_LOG_INFO m_struLogInfo = new HCEHomePublic.LOCAL_LOG_INFO();
        private static DeviceLogList         g_LogPanel = new DeviceLogList();
        private bool m_bEnlarged = false;
        private Point m_logPanelPos = new Point();
        private Size m_logPanelSize = new Size();
        private int m_iIndex = 1;
        public List<string> AlarmPic = new List<string>();
        public string SS_PATH = "C:\\StorageServer\\Storage\\";
        public DeviceLogList()
        {
            InitializeComponent();
            listViewAllLog.Visible = true;
            listViewAlarmInfo.Visible = false;
        }

        public static DeviceLogList Instance()
        {
            return g_LogPanel;
        }

        protected override void DefWndProc(ref Message m)
        {
            //Newly Modify Content
            if (m.Msg == ISUPDemo.WM_LISTENED_ALARM)
            {
                ProcessAlarmData(m.LParam,m.WParam);
            }
            base.DefWndProc(ref m);
        }
        public struct AlarmParam
        {
            public uint dwAlarmType;
            public IntPtr pStru;
            public uint dwStruLen;
            public IntPtr pXml;
            public uint dwXmlLen;
        }

        //public void ProcessAlarmData(AlarmParam struAlarmParam)
        //Newly Modify Content
        public void ProcessAlarmData(IntPtr ptrAlarmParam, IntPtr ptrFilterAlarmParam)
        {

            HCEHomeAlarm.NET_EHOME_ALARM_MSG struAlarmParam = new HCEHomeAlarm.NET_EHOME_ALARM_MSG();
            struAlarmParam.Init();
            struAlarmParam = (HCEHomeAlarm.NET_EHOME_ALARM_MSG)Marshal.PtrToStructure(ptrAlarmParam, typeof(HCEHomeAlarm.NET_EHOME_ALARM_MSG));

 //           string FilterAlarmMsg = Marshal.PtrToStringAnsi(ptrFilterAlarmParam);

            // Alarm td = new Alarm();
            Alarm.ShowAlarmInfo(ptrAlarmParam);

//             switch (struAlarmParam.dwAlarmType)
//             {
//                 case HCEHomeAlarm.EHOME_ALARM_ACS:
//                     //Newly Modify Content
//                     ProcessEhomeAlarmACS(struAlarmParam.pXmlBuf, struAlarmParam.dwXmlBufLen, ref FilterAlarmMsg);
//                     break;
//                 case HCEHomeAlarm.EHOME_ALARM_FACETEMP:
//                     ShowFaceTempAlarm(struAlarmParam.pXmlBuf, struAlarmParam.dwXmlBufLen, ref FilterAlarmMsg);
//                     break;
//                 default:                  
//                     AddLog(-1, HCEHomePublic.ALARM_INFO_T, 0, "[Unknown_Alarm]");
//                     break;
//             }

        }

        public void ShowFaceTempAlarm(IntPtr pXml, uint dwXmlLen, ref string FilterAlarmMsg)
        {
            String str;
            String strTemp = Marshal.PtrToStringAnsi(pXml, (int)dwXmlLen);
            if (FilterAlarmMsg.Contains("CurrTemperature: "))
            {
                CMSAlarm.ShowISAPIAlarmInfo(ref FilterAlarmMsg);
            }
            else
            {
                CMSAlarm.ShowCmsAlarmInfo(ref FilterAlarmMsg);
            }
            str = string.Format("[ISAPI ALARM] save to[c:\\ISAPI\\]");
            try
            {
                string directoryPath = @"C:\ISAPI";
                string filePath = m_iIndex.ToString() + ".json";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, filePath));
                sw.WriteLine(strTemp);
                sw.Flush();
                sw.Close();
                m_iIndex++;
            }
            catch (IOException e)
            {
                str = string.Format("[ISAPI ALARM] save file failed![%s]", e.ToString());
            }
            string TempStr = "PicDataUrl:";
            if (FilterAlarmMsg.Contains(TempStr) && !FilterAlarmMsg.Contains("Failed-to"))
            {
                int PicIndex = FilterAlarmMsg.LastIndexOf("PicDataUrl:");
                string PicUrl = FilterAlarmMsg.Substring(PicIndex + TempStr.Length);

                if (PicUrl.Contains("/pic?"))
                {
                    PicUrl = PicUrl.Substring(PicUrl.LastIndexOf("/pic?") + 5, PicUrl.Length - PicUrl.LastIndexOf("/pic?") - 5);
                    AlarmPic.Add(SS_PATH + PicUrl);
                    CMSAlarm.ReadThemalImageFromLocal(AlarmPic[0]);
                    AlarmPic.RemoveRange(0, 1);
                }
                else if (PicUrl.Contains("/kms"))
                {
                    PicUrl = PicUrl.Substring(PicUrl.LastIndexOf("id=") + 3, PicUrl.Length - PicUrl.LastIndexOf("id=") - 3);
                    AlarmPic.Add(SS_PATH + PicUrl);
                    CMSAlarm.ReadThemalImageFromLocal(AlarmPic[0]);
                    AlarmPic.RemoveRange(0, 1);
                }
            }
            AddLog(0, HCEHomePublic.ALARM_INFO_T, 0, str);
            AddLog(0, HCEHomePublic.ALARM_INFO_T, 0, FilterAlarmMsg);
        }
        public void ProcessEhomeAlarmACS(IntPtr pXml,uint dwXmlLen, ref string FilterAlarmMsg)
        {
            String str;
            String strTemp = Marshal.PtrToStringAnsi(pXml, (int)dwXmlLen);
            if (FilterAlarmMsg.Contains("CurrTemperature: "))
            {
                CMSAlarm.ShowISAPIAlarmInfo(ref FilterAlarmMsg);
            }
            else
            {
                CMSAlarm.ShowCmsAlarmInfo(ref FilterAlarmMsg);
            }
            str = string.Format("[ACS ALARM] save to[c:\\ACS\\]");
            try
            {
                string directoryPath = @"C:\ACS";
                string filePath = m_iIndex.ToString() + ".xml";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, filePath));
                sw.WriteLine(strTemp);
                sw.Flush();
                sw.Close();
                m_iIndex++;
            }
            catch (IOException e)
            {
                str = string.Format("[ACS ALARM] save file failed![%s]", e.ToString());
            }
            string TempStr = "PicDataUrl:";
            if (FilterAlarmMsg.Contains(TempStr) && !FilterAlarmMsg.Contains("Failed-to"))
            {
                 int PicIndex = FilterAlarmMsg.LastIndexOf("PicDataUrl:");
                 string PicUrl = FilterAlarmMsg.Substring(PicIndex + TempStr.Length);
                 if (PicUrl.Contains("/pic?"))
                 {
                     PicUrl = PicUrl.Substring(PicUrl.LastIndexOf("/pic?") + 5, PicUrl.Length - PicUrl.LastIndexOf("/pic?") - 5);
                     AlarmPic.Add(SS_PATH + PicUrl);
                     CMSAlarm.ReadImageFromLocal(AlarmPic[0]);
                     AlarmPic.RemoveRange(0, 1);
                 }
                 else if(PicUrl.Contains("/kms")){
                     PicUrl = PicUrl.Substring(PicUrl.LastIndexOf("id=") + 3, PicUrl.Length - PicUrl.LastIndexOf("id=") - 3);
                     //AlarmPic.Add(SS_PATH + PicUrl);
                     AlarmPic.Add(SS_PATH + PicUrl);
                     CMSAlarm.ReadImageFromLocal(AlarmPic[0]);
                     AlarmPic.RemoveRange(0, 1);
                 }
            }
            AddLog(0, HCEHomePublic.ALARM_INFO_T, 0, str);
            AddLog(0, HCEHomePublic.ALARM_INFO_T, 0, FilterAlarmMsg);
        }


        //Change the ListView(Log List or Alarm List)
        public void showList(int iTag)
        {
            switch (iTag)
            {
                case 0:
                    listViewAllLog.Visible = true;
                    listViewAlarmInfo.Visible = false;
                    break;
                case 1:
                    listViewAllLog.Visible = false;
                    listViewAlarmInfo.Visible = true;
                    break;
                default:
                    listViewAllLog.Visible = true;
                    listViewAlarmInfo.Visible = false;
                    break;
            }
        }

        //Change the ListView(Log List or Alarm List)
        public void ClearList(int iTag)
        {
            switch (iTag)
            {
                case 0:
                    listViewAllLog.Items.Clear();
                    break;
                case 1:
                    listViewAlarmInfo.Items.Clear();
                    break;
                default:
                    listViewAllLog.Items.Clear();
                    listViewAlarmInfo.Items.Clear();
                    break;
            }
        }

        private delegate void AddListCallBack(HCEHomePublic.LOCAL_LOG_INFO struLogInfo);

        //Output log to ListView
        private void AddList(HCEHomePublic.LOCAL_LOG_INFO struLogInfo)
        {
            if (this.InvokeRequired)
            {
                AddListCallBack add = new AddListCallBack(AddList);
                this.BeginInvoke(add, new object[] { struLogInfo });
            }
            else
            {
                lock (locker)
                {
                    if (listViewAllLog.Items.Count >= 5000)
                    {
                        listViewAllLog.Items.Clear();
                    }
                    if (listViewAlarmInfo.Items.Count >= 5000)
                    {
                        listViewAlarmInfo.Items.Clear();
                    }
                    string strLogType = "FAIL";
                    switch (struLogInfo.iLogType)
                    {
                        case HCEHomePublic.ALARM_INFO_T:
                            strLogType = "Alarm";
                            ListViewItem listItem1 = new ListViewItem();
                            listItem1.Text = struLogInfo.strTime;
                            listItem1.SubItems.Add(struLogInfo.strDevInfo);
                            listItem1.SubItems.Add(struLogInfo.strLogInfo);
                            listViewAlarmInfo.Items.Insert(0, listItem1);
                            break;
                        case HCEHomePublic.OPERATION_SUCC_T:
                            strLogType = "SUCC";
                            ListViewItem listItem2 = new ListViewItem();
                            listItem2.Text = struLogInfo.strTime;
                            listItem2.SubItems.Add(strLogType);
                            listItem2.SubItems.Add(struLogInfo.strLogInfo);
                            listItem2.SubItems.Add(struLogInfo.strDevInfo);
                            listItem2.SubItems.Add(struLogInfo.strErrInfo);
                            listViewAllLog.Items.Insert(0, listItem2);
                            break;
                        case HCEHomePublic.OPERATION_FAIL_T:
                            strLogType = "FAIL";
                            ListViewItem listItem3 = new ListViewItem();
                            listItem3.Text = struLogInfo.strTime;
                            listItem3.SubItems.Add(strLogType);
                            listItem3.SubItems.Add(struLogInfo.strLogInfo);
                            listItem3.SubItems.Add(struLogInfo.strDevInfo);
                            listItem3.SubItems.Add(struLogInfo.strErrInfo);
                            listViewAllLog.Items.Insert(0, listItem3);
                            break;
                        case HCEHomePublic.PLAY_SUCC_T:
                            strLogType = "SUCC";
                            ListViewItem listItem4 = new ListViewItem();
                            listItem4.Text = struLogInfo.strTime;
                            listItem4.SubItems.Add(strLogType);
                            listItem4.SubItems.Add(struLogInfo.strLogInfo);
                            listItem4.SubItems.Add(struLogInfo.strDevInfo);
                            listItem4.SubItems.Add(struLogInfo.strErrInfo);
                            listViewAllLog.Items.Insert(0, listItem4);
                            break;
                        case HCEHomePublic.PLAY_FAIL_T:
                            strLogType = "FAIL";
                            ListViewItem listItem5 = new ListViewItem();
                            listItem5.Text = struLogInfo.strTime;
                            listItem5.SubItems.Add(strLogType);
                            listItem5.SubItems.Add(struLogInfo.strLogInfo);
                            listItem5.SubItems.Add(struLogInfo.strDevInfo);
                            listItem5.SubItems.Add(struLogInfo.strErrInfo);
                            listViewAllLog.Items.Insert(0, listItem5);
                            break;
                        default:
                            strLogType = "FAIL";
                            ListViewItem listItem6 = new ListViewItem();
                            listItem6.Text = struLogInfo.strTime;
                            listItem6.SubItems.Add(strLogType);
                            listItem6.SubItems.Add(struLogInfo.strLogInfo);
                            listItem6.SubItems.Add(struLogInfo.strDevInfo);
                            listItem6.SubItems.Add(struLogInfo.strErrInfo);
                            listViewAllLog.Items.Insert(0, listItem6);
                            break;
                    }
                }
            }
        }

        /*********************************************************
        Function:    AddLog
        Desc:        add local log
        Input:    iLogType, log type, 0-alam, 1-operate log, 2-debug info; csLogInfo log info
        Output:    
        Return:    
        **********************************************************/
        /** @fn void CEHomeDemoDlg::AddLog(int iDeviceIndex, int iLogType, int iComType, const char* format, ...)
            *  @brief
            *  @param (in)    int iDeviceIndex    
            *  @param (in)    int iLogType    
            *  @param (in)    int iComType 模块类型，0-模块无关，1-CMS模块，2-STREAM模块，3-ALARM模块
            *  @param (in)    const char * format    
            *  @param (in)    ...    
            *  @return void
            */
        public void AddLog(int iDeviceIndex, int iLogType, int iComType, string strFormat)
        {
            DeviceTree g_deviceTree = DeviceTree.Instance();
            DateTime curTime = DateTime.Now;
            string strTime = null;
            string strlogType = "FAIL";
            string strLogInfo = null;
            string strDevInfo = null;
            string strErrInfo = null;
            strTime = curTime.ToString();
            strLogInfo = strFormat;
            if (iDeviceIndex != -1 && iDeviceIndex < 512)
            {
                if (DeviceTree.g_struDeviceInfo[iDeviceIndex].byDeviceID[0] == 0)
                {
                    strDevInfo = string.Format("[]");
                }
                else {
                    strDevInfo = System.Text.Encoding.UTF8.GetString(DeviceTree.g_struDeviceInfo[iDeviceIndex].byDeviceID);
                    int strDevInfoLen = strDevInfo.IndexOf('\0');
                    strDevInfo = string.Format("[{0}]", strDevInfo.Substring(0, strDevInfoLen));
                }
            }
            switch (iLogType)
            {
                case HCEHomePublic.OPERATION_SUCC_T:
                    strErrInfo = "";
                    strlogType = "SUCC";
                    break;
                case HCEHomePublic.PLAY_SUCC_T:
                    strErrInfo = "";
                    strlogType = "SUCC";
                    break;
                case HCEHomePublic.PLAY_FAIL_T:
                    strErrInfo = "PLAY_M4 Error!!!";
                    break;
                case HCEHomePublic.OPERATION_FAIL_T:
                   
                default:
                    switch(iComType)
                    {
                        case 1:
                                strErrInfo = string.Format("CMS_ERR:{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                                break;
                        case 2:
                                strErrInfo = string.Format("STREAM_ERR:{0}", HCEHomeStream.NET_ESTREAM_GetLastError());
                                break;
                        case 3:
                                strErrInfo = string.Format("ALARM_ERR:{0}", HCEHomeAlarm.NET_EALARM_GetLastError());
                                break;
                        default:
                                break;

                    }
                    break;
            }
            m_struLogInfo.iLogType = iLogType;
            m_struLogInfo.strTime = strTime;
            m_struLogInfo.strLogInfo = strLogInfo;
            m_struLogInfo.strDevInfo = strDevInfo;
            m_struLogInfo.strErrInfo = strErrInfo;
            AddList(m_struLogInfo);
        }

      
        private void listViewAllLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Point pos = new Point();

            //TotalPanel的顶点位置
            pos.X = 5;                             
            pos.Y = 4;

            int w = this.Parent.Parent.Size.Width;  
            int h = this.Parent.Parent.Size.Height;

            if(!m_bEnlarged)
            {
                //保存记录日志面板的原始位置
                m_logPanelPos.X = this.Parent.Location.X;
                m_logPanelPos.Y = this.Parent.Location.Y;
                m_logPanelSize.Width = this.Parent.Size.Width;
                m_logPanelSize.Height = this.Parent.Size.Height;

                this.Parent.SetBounds(pos.X, pos.Y, w, h);
                this.Parent.Parent.GetChildAtPoint(pos).Visible = false;
                this.Parent.Visible = true;
                this.Parent.Show();
                m_bEnlarged = true;
            }
            else
            {
                this.Parent.SetBounds(m_logPanelPos.X, m_logPanelPos.Y, m_logPanelSize.Width, m_logPanelSize.Height);
                this.Parent.Parent.GetChildAtPoint(pos).Visible = true;
                this.Parent.Visible = true;
                this.Parent.Show();
                m_bEnlarged = false;
            }
        }

        private void DeviceLogList_Load(object sender, EventArgs e)
        {
            MultiLanguage.LoadLanguage(this);
        }
    }
}
