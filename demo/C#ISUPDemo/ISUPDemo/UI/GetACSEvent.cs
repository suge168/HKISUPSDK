using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;
using ISUPDemo.Public;
using ISUPDemo;
using GetACSEvent;
using System.Reflection;
namespace ISUPDemo
{
    public partial class GetACSEvent : Form
    {
        private int m_lLogNum = 0;
        private string MinorType = null;
        private string MajorType = null;

        private int m_iDeviceIndex;
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private DeviceLogList g_formList = DeviceLogList.Instance();
        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 50];
        private byte[] m_szUrl = new byte[256];
        private object m_csLock = new object();

        public GetACSEvent()
        {
            InitializeComponent();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
        }

        String GetStdTime(int year, int month, int day, int hour, int min, int sec)
        {
            DateTime dt = new DateTime(year, month, day, hour, min, sec);
            return String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dt);　//2017-04-01T13:16:32.108
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lock (m_csLock)
            {
                listViewEvent.Items.Clear();
                m_lLogNum = 0;

                string szUrl = "/ISAPI/AccessControl/AcsEvent?format=json";

                MajorType = comboBoxMainType.SelectedItem.ToString();
                uint dwMajor = GetAcsEventType.ReturnMajorTypeValue(ref MajorType);

                MinorType = comboBoxSecondType.SelectedItem.ToString();
                uint dwMinor = GetAcsEventType.ReturnMinorTypeValue(ref MinorType);

                int startYear = dateTimeStart.Value.Year;
                int startMonth = dateTimeStart.Value.Month;
                int startDay = dateTimeStart.Value.Day;
                int startHour = dateTimeStart.Value.Hour;
                int startMinute = dateTimeStart.Value.Minute;
                int startSecond = dateTimeStart.Value.Second;
                String startTime = GetStdTime(startYear, startMonth, startDay, startHour, startMinute, startSecond) + "+08:00";//UTC时间格式
                int endYear = dateTimeEnd.Value.Year;
                int endMonth = dateTimeEnd.Value.Month;
                int endDay = dateTimeEnd.Value.Day;
                int endHour = dateTimeEnd.Value.Hour;
                int endMinute = dateTimeEnd.Value.Minute;
                int endSecond = dateTimeEnd.Value.Second;
                String endTime = GetStdTime(endYear, endMonth, endDay, endHour, endMinute, endSecond) + "+08:00";
                String szName = textBoxName.Text;
                String szEmployeeNo = textBoxEmployeeNo.Text;
                String szCardNo = textBoxCardNo.Text;
                String pos = "0";

                while (true)
                {
                    string szResponse = string.Empty;
                    string szRequest = "{\"AcsEventCond\":{\"searchID\":\"1\",\"searchResultPosition\":" + pos +
                        ",\"maxResults\":30,\"major\":" + dwMajor +
                    ",\"minor\":" + dwMinor +
                    ",\"startTime\":\"" + startTime +
                    "\",\"endTime\":\"" + endTime;
                    if (szCardNo != "")
                    {
                        szRequest += "\",\"cardNo\":\"" + szCardNo;
                    }
                    if (szName != "")
                    {
                        szRequest += "\",\"name\":\"" + szName;
                    }
                    if (szEmployeeNo != "")
                    {
                        szRequest += "\",\"employeeNoString\":\"" + szEmployeeNo;
                    }
                    szRequest += "\"}}";

                    m_szUrl = Encoding.UTF8.GetBytes(szUrl);
                    struPTXML.pRequestUrl = Marshal.AllocHGlobal(256);
                    Marshal.Copy(m_szUrl, 0, struPTXML.pRequestUrl, m_szUrl.Length);
                    struPTXML.dwRequestUrlLen = (uint)m_szUrl.Length;


                    m_szInputBuffer = Encoding.UTF8.GetBytes(szRequest);
                    struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 50);
                    Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                    struPTXML.dwInSize = (uint)szRequest.Length;


                    struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 50);
                    for (int i = 0; i < 1024 * 50; i++)
                    {
                        Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                    }

                    struPTXML.dwOutSize = (uint)(1024 * 50);
                    struPTXML.byRes = new byte[32];

                    IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                    Marshal.StructureToPtr(struPTXML, ptrCfg, false);

                    if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
                    {
                        g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_PostPTXMLConfig");
                        Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        Marshal.FreeHGlobal(ptrCfg);
                        return;
                    }

                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_XXXPTXMLConfig");
                    szResponse = "";
                    szResponse = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);

                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);

                    if (szResponse != string.Empty)
                    {
                        AcsEventObject rb = JsonConvert.DeserializeObject<AcsEventObject>(szResponse);

                        if (rb.AcsEvent == null)
                        {
                            MessageBox.Show("AcsEvent is NULL!");
                            break;
                        }

                        String responseTemp = rb.AcsEvent.responseStatusStrg;
                        if (responseTemp.Equals("NO MATCH"))
                        {
                            break;
                        }
                        foreach (InfoListItem info in rb.AcsEvent.InfoList)
                        {
                            listViewEvent.BeginUpdate();
                            ListViewItem item = new ListViewItem();
                            item.Text = (++m_lLogNum).ToString();
                            item.SubItems.Add(ProcessMajorType(info.major.ToString()));
                            item.SubItems.Add(ProcessMinorType(info.major.ToString(), info.minor.ToString()));
                            item.SubItems.Add(info.time);
                            item.SubItems.Add(info.netUser);
                            item.SubItems.Add(info.remoteHostAddr);
                            item.SubItems.Add(info.cardNo);
                            item.SubItems.Add(CardTypeMap(info.cardType.ToString()));
                            item.SubItems.Add(info.whiteListNo.ToString());
                            item.SubItems.Add(ProcessReportChannel(info.reportChannel.ToString()));
                            item.SubItems.Add(ProcessCardReader(info.cardReaderKind.ToString()));
                            item.SubItems.Add(info.cardReaderNo.ToString());
                            item.SubItems.Add(info.doorNo.ToString());
                            item.SubItems.Add(info.verifyNo.ToString());
                            item.SubItems.Add(info.alarmInNo.ToString());
                            item.SubItems.Add(info.alarmOutNo.ToString());
                            item.SubItems.Add(info.caseSensorNo.ToString());
                            item.SubItems.Add(info.RS485No.ToString());
                            item.SubItems.Add(info.multiCardGroupNo.ToString());
                            item.SubItems.Add(info.accessChannel.ToString());
                            item.SubItems.Add(info.deviceNo.ToString());
                            item.SubItems.Add(info.distractControlNo.ToString());
                            item.SubItems.Add(info.employeeNoString);
                            item.SubItems.Add(info.localControllerID.ToString());
                            item.SubItems.Add(ProcessInternatAccess(info.InternetAccess.ToString()));
                            item.SubItems.Add(ProcessByType(info.type.ToString()));
                            item.SubItems.Add(info.MACAddr);
                            item.SubItems.Add(ProcessSwipeCard(info.swipeCardType.ToString()));
                            item.SubItems.Add(info.serialNo.ToString());
                            item.SubItems.Add(info.channelControllerID.ToString());
                            item.SubItems.Add(info.channelControllerLampID.ToString());
                            item.SubItems.Add(info.channelControllerIRAdaptorID.ToString());
                            item.SubItems.Add(info.channelControllerIREmitterID.ToString());
                            item.SubItems.Add(info.userType);
                            item.SubItems.Add(info.currentVerifyMode);
                            item.SubItems.Add(info.filename);
                            listViewEvent.Items.Add(item);
                            listViewEvent.EndUpdate();
                        }

                        int posTmp = 0;
                        int.TryParse(pos, out posTmp);
                        int numOfMatches = rb.AcsEvent.numOfMatches;
                        posTmp += numOfMatches;
                        pos = posTmp.ToString();

                        if (rb.AcsEvent.responseStatusStrg.Equals("OK"))
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void GetACSEvent_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void listViewAlarmInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewEvent.SelectedItems.Count > 0)
            {
                textBoxCardNo.Text = listViewEvent.FocusedItem.SubItems[6].Text.ToString();
                textBoxEmployeeNo.Text = listViewEvent.FocusedItem.SubItems[22].Text.ToString();
                textBoxName.Text = "";
            }
        }

        private string ProcessMajorType(string dwMajor)
        {
            string res = null;
            switch (dwMajor)
            {
                case "1":
                    res = "Alarm";
                    break;
                case "2":
                    res = "Exception";
                    break;
                case "3":
                    res = "Operation";
                    break;
                case "5":
                    res = "Event";
                    break;
                default:
                    res = "Unknown";
                    break;
            }
            return res;
        }

        private string AlarmMinorTypeMap(int dwMinor)
        {
            string CsTemp = null;
            switch (dwMinor)
            {
                case EHDemoPublic.MINOR_ALARMIN_SHORT_CIRCUIT:
                    CsTemp = "ALARMIN_SHORT_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_ALARMIN_BROKEN_CIRCUIT:
                    CsTemp = "ALARMIN_BROKEN_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_ALARMIN_EXCEPTION:
                    CsTemp = "ALARMIN_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_ALARMIN_RESUME:
                    CsTemp = "ALARMIN_RESUME";
                    break;
                case EHDemoPublic.MINOR_HOST_DESMANTLE_ALARM:
                    CsTemp = "HOST_DESMANTLE_ALARM";
                    break;
                case EHDemoPublic.MINOR_HOST_DESMANTLE_RESUME:
                    CsTemp = "HOST_DESMANTLE_RESUME";
                    break;
                case EHDemoPublic.MINOR_CARD_READER_DESMANTLE_ALARM:
                    CsTemp = "CARD_READER_DESMANTLE_ALARM";
                    break;
                case EHDemoPublic.MINOR_CARD_READER_DESMANTLE_RESUME:
                    CsTemp = "CARD_READER_DESMANTLE_RESUME";
                    break;
                case EHDemoPublic.MINOR_CASE_SENSOR_ALARM:
                    CsTemp = "CASE_SENSOR_ALARM";
                    break;
                case EHDemoPublic.MINOR_CASE_SENSOR_RESUME:
                    CsTemp = "CASE_SENSOR_RESUME";
                    break;
                case EHDemoPublic.MINOR_STRESS_ALARM:
                    CsTemp = "STRESS_ALARM";
                    break;
                case EHDemoPublic.MINOR_OFFLINE_ECENT_NEARLY_FULL:
                    CsTemp = "OFFLINE_ECENT_NEARLY_FULL";
                    break;
                case EHDemoPublic.MINOR_CARD_MAX_AUTHENTICATE_FAIL:
                    CsTemp = "CARD_MAX_AUTHENTICATE_FAIL";
                    break;
                case EHDemoPublic.MINOR_SD_CARD_FULL:
                    CsTemp = "SD_CARD_FULL";
                    break;
                case EHDemoPublic.MINOR_LINKAGE_CAPTURE_PIC:
                    CsTemp = "LINKAGE_CAPTURE_PIC";
                    break;
                case EHDemoPublic.MINOR_SECURITY_MODULE_DESMANTLE_ALARM:
                    CsTemp = "SECURITY_MODULE_DESMANTLE_ALARM";
                    break;
                case EHDemoPublic.MINOR_SECURITY_MODULE_DESMANTLE_RESUME:
                    CsTemp = "SECURITY_MODULE_DESMANTLE_RESUME";
                    break;
                case EHDemoPublic.MINOR_POS_START_ALARM:
                    CsTemp = "POS_START_ALARM";
                    break;
                case EHDemoPublic.MINOR_POS_END_ALARM:
                    CsTemp = "POS_END_ALARM";
                    break;
                case EHDemoPublic.MINOR_FACE_IMAGE_QUALITY_LOW:
                    CsTemp = "FACE_IMAGE_QUALITY_LOW";
                    break;
                case EHDemoPublic.MINOR_FINGE_RPRINT_QUALITY_LOW:
                    CsTemp = "FINGE_RPRINT_QUALITY_LOW";
                    break;
                case EHDemoPublic.MINOR_FIRE_IMPORT_SHORT_CIRCUIT:
                    CsTemp = "FIRE_IMPORT_SHORT_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_FIRE_IMPORT_BROKEN_CIRCUIT:
                    CsTemp = "FIRE_IMPORT_BROKEN_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_FIRE_IMPORT_RESUME:
                    CsTemp = "FIRE_IMPORT_RESUME";
                    break;
                case EHDemoPublic.MINOR_FIRE_BUTTON_TRIGGER:
                    CsTemp = "FIRE_BUTTON_TRIGGER";
                    break;
                case EHDemoPublic.MINOR_FIRE_BUTTON_RESUME:
                    CsTemp = "FIRE_BUTTON_RESUME";
                    break;
                case EHDemoPublic.MINOR_MAINTENANCE_BUTTON_TRIGGER:
                    CsTemp = "MAINTENANCE_BUTTON_TRIGGER";
                    break;
                case EHDemoPublic.MINOR_MAINTENANCE_BUTTON_RESUME:
                    CsTemp = "MAINTENANCE_BUTTON_RESUME";
                    break;
                case EHDemoPublic.MINOR_EMERGENCY_BUTTON_TRIGGER:
                    CsTemp = "EMERGENCY_BUTTON_TRIGGER";
                    break;
                case EHDemoPublic.MINOR_EMERGENCY_BUTTON_RESUME:
                    CsTemp = "EMERGENCY_BUTTON_RESUME";
                    break;
                case EHDemoPublic.MINOR_DISTRACT_CONTROLLER_ALARM:
                    CsTemp = "DISTRACT_CONTROLLER_ALARM";
                    break;
                case EHDemoPublic.MINOR_DISTRACT_CONTROLLER_RESUME:
                    CsTemp = "DISTRACT_CONTROLLER_RESUME";
                    break;
                case EHDemoPublic.MINOR_CHANNEL_CONTROLLER_DESMANTLE_ALARM:
                    CsTemp = "CHANNEL_CONTROLLER_DESMANTLE_ALARM";
                    break;
                case EHDemoPublic.MINOR_CHANNEL_CONTROLLER_DESMANTLE_RESUME:
                    CsTemp = "CHANNEL_CONTROLLER_DESMANTLE_RESUME";
                    break;
                case EHDemoPublic.MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM:
                    CsTemp = "CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM";
                    break;
                case EHDemoPublic.MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME:
                    CsTemp = "CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME";
                    break;
                case EHDemoPublic.MINOR_PRINTER_OUT_OF_PAPER:
                    CsTemp = "PRINTER_OUT_OF_PAPER";
                    break;
                case EHDemoPublic.MINOR_LEGAL_EVENT_NEARLY_FULL:
                    CsTemp = "LEGAL_EVENT_NEARLY_FULL";
                    break;
                default:
                    CsTemp = Convert.ToString(dwMinor, 16);
                    break;
            }
            return CsTemp;
        }

        private string ExceptionMinorTypeMap(int dwMinor)
        {
            string CsTemp = null;
            switch (dwMinor)
            {
                case EHDemoPublic.MINOR_NET_BROKEN:
                    CsTemp = "NET_BROKEN";
                    break;
                case EHDemoPublic.MINOR_RS485_DEVICE_ABNORMAL:
                    CsTemp = "RS485_DEVICE_ABNORMAL";
                    break;
                case EHDemoPublic.MINOR_RS485_DEVICE_REVERT:
                    CsTemp = "RS485_DEVICE_REVERT";
                    break;
                case EHDemoPublic.MINOR_DEV_POWER_ON:
                    CsTemp = "DEV_POWER_ON";
                    break;
                case EHDemoPublic.MINOR_DEV_POWER_OFF:
                    CsTemp = "DEV_POWER_OFF";
                    break;
                case EHDemoPublic.MINOR_WATCH_DOG_RESET:
                    CsTemp = "WATCH_DOG_RESET";
                    break;
                case EHDemoPublic.MINOR_LOW_BATTERY:
                    CsTemp = "LOW_BATTERY";
                    break;
                case EHDemoPublic.MINOR_BATTERY_RESUME:
                    CsTemp = "BATTERY_RESUME";
                    break;
                case EHDemoPublic.MINOR_AC_OFF:
                    CsTemp = "AC_OFF";
                    break;
                case EHDemoPublic.MINOR_AC_RESUME:
                    CsTemp = "AC_RESUME";
                    break;
                case EHDemoPublic.MINOR_NET_RESUME:
                    CsTemp = "NET_RESUME";
                    break;
                case EHDemoPublic.MINOR_FLASH_ABNORMAL:
                    CsTemp = "FLASH_ABNORMAL";
                    break;
                case EHDemoPublic.MINOR_CARD_READER_OFFLINE:
                    CsTemp = "CARD_READER_OFFLINE";
                    break;
                case EHDemoPublic.MINOR_CARD_READER_RESUME:
                    CsTemp = "CARD_READER_RESUME";
                    break;
                case EHDemoPublic.MINOR_INDICATOR_LIGHT_OFF:
                    CsTemp = "INDICATOR_LIGHT_OFF";
                    break;
                case EHDemoPublic.MINOR_INDICATOR_LIGHT_RESUME:
                    CsTemp = "INDICATOR_LIGHT_RESUME";
                    break;
                case EHDemoPublic.MINOR_CHANNEL_CONTROLLER_OFF:
                    CsTemp = "CHANNEL_CONTROLLER_OFF";
                    break;
                case EHDemoPublic.MINOR_CHANNEL_CONTROLLER_RESUME:
                    CsTemp = "CHANNEL_CONTROLLER_RESUME";
                    break;
                case EHDemoPublic.MINOR_SECURITY_MODULE_OFF:
                    CsTemp = "SECURITY_MODULE_OFF";
                    break;
                case EHDemoPublic.MINOR_SECURITY_MODULE_RESUME:
                    CsTemp = "SECURITY_MODULE_RESUME";
                    break;
                case EHDemoPublic.MINOR_BATTERY_ELECTRIC_LOW:
                    CsTemp = "BATTERY_ELECTRIC_LOW";
                    break;
                case EHDemoPublic.MINOR_BATTERY_ELECTRIC_RESUME:
                    CsTemp = "BATTERY_ELECTRIC_RESUME";
                    break;
                case EHDemoPublic.MINOR_LOCAL_CONTROL_NET_BROKEN:
                    CsTemp = "LOCAL_CONTROL_NET_BROKEN";
                    break;
                case EHDemoPublic.MINOR_LOCAL_CONTROL_NET_RSUME:
                    CsTemp = "LOCAL_CONTROL_NET_RSUME";
                    break;
                case EHDemoPublic.MINOR_MASTER_RS485_LOOPNODE_BROKEN:
                    CsTemp = "MASTER_RS485_LOOPNODE_BROKEN";
                    break;
                case EHDemoPublic.MINOR_MASTER_RS485_LOOPNODE_RESUME:
                    CsTemp = "MASTER_RS485_LOOPNODE_RESUME";
                    break;
                case EHDemoPublic.MINOR_LOCAL_CONTROL_OFFLINE:
                    CsTemp = "LOCAL_CONTROL_OFFLINE";
                    break;
                case EHDemoPublic.MINOR_LOCAL_CONTROL_RESUME:
                    CsTemp = "LOCAL_CONTROL_RESUME";
                    break;
                case EHDemoPublic.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN:
                    CsTemp = "LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN";
                    break;
                case EHDemoPublic.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME:
                    CsTemp = "LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME";
                    break;
                case EHDemoPublic.MINOR_DISTRACT_CONTROLLER_ONLINE:
                    CsTemp = "DISTRACT_CONTROLLER_ONLINE";
                    break;
                case EHDemoPublic.MINOR_DISTRACT_CONTROLLER_OFFLINE:
                    CsTemp = "DISTRACT_CONTROLLER_OFFLINE";
                    break;
                case EHDemoPublic.MINOR_ID_CARD_READER_NOT_CONNECT:
                    CsTemp = "ID_CARD_READER_NOT_CONNECT";
                    break;
                case EHDemoPublic.MINOR_ID_CARD_READER_RESUME:
                    CsTemp = "ID_CARD_READER_RESUME";
                    break;
                case EHDemoPublic.MINOR_FINGER_PRINT_MODULE_NOT_CONNECT:
                    CsTemp = "FINGER_PRINT_MODULE_NOT_CONNECT";
                    break;
                case EHDemoPublic.MINOR_FINGER_PRINT_MODULE_RESUME:
                    CsTemp = "FINGER_PRINT_MODULE_RESUME";
                    break;
                case EHDemoPublic.MINOR_CAMERA_NOT_CONNECT:
                    CsTemp = "CAMERA_NOT_CONNECT";
                    break;
                case EHDemoPublic.MINOR_CAMERA_RESUME:
                    CsTemp = "CAMERA_RESUME";
                    break;
                case EHDemoPublic.MINOR_COM_NOT_CONNECT:
                    CsTemp = "COM_NOT_CONNECT";
                    break;
                case EHDemoPublic.MINOR_COM_RESUME:
                    CsTemp = "COM_RESUME";
                    break;
                case EHDemoPublic.MINOR_DEVICE_NOT_AUTHORIZE:
                    CsTemp = "DEVICE_NOT_AUTHORIZE";
                    break;
                case EHDemoPublic.MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE:
                    CsTemp = "PEOPLE_AND_ID_CARD_DEVICE_ONLINE";
                    break;
                case EHDemoPublic.MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE:
                    CsTemp = "PEOPLE_AND_ID_CARD_DEVICE_OFFLINE";
                    break;
                case EHDemoPublic.MINOR_LOCAL_LOGIN_LOCK:
                    CsTemp = "LOCAL_LOGIN_LOCK";
                    break;
                case EHDemoPublic.MINOR_LOCAL_LOGIN_UNLOCK:
                    CsTemp = "LOCAL_LOGIN_UNLOCK";
                    break;
                case EHDemoPublic.MINOR_SUBMARINEBACK_COMM_BREAK:
                    CsTemp = "SUBMARINEBACK_COMM_BREAK";
                    break;
                case EHDemoPublic.MINOR_SUBMARINEBACK_COMM_RESUME:
                    CsTemp = "SUBMARINEBACK_COMM_RESUME";
                    break;
                case EHDemoPublic.MINOR_MOTOR_SENSOR_EXCEPTION:
                    CsTemp = "MOTOR_SENSOR_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_CAN_BUS_EXCEPTION:
                    CsTemp = "CAN_BUS_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_CAN_BUS_RESUME:
                    CsTemp = "CAN_BUS_RESUME";
                    break;
                case EHDemoPublic.MINOR_GATE_TEMPERATURE_OVERRUN:
                    CsTemp = "GATE_TEMPERATURE_OVERRUN";
                    break;
                case EHDemoPublic.MINOR_IR_EMITTER_EXCEPTION:
                    CsTemp = "IR_EMITTER_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_IR_EMITTER_RESUME:
                    CsTemp = "IR_EMITTER_RESUME";
                    break;
                case EHDemoPublic.MINOR_LAMP_BOARD_COMM_EXCEPTION:
                    CsTemp = "LAMP_BOARD_COMM_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_LAMP_BOARD_COMM_RESUME:
                    CsTemp = "LAMP_BOARD_COMM_RESUME";
                    break;
                case EHDemoPublic.MINOR_IR_ADAPTOR_COMM_EXCEPTION:
                    CsTemp = "IR_ADAPTOR_COMM_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_IR_ADAPTOR_COMM_RESUME:
                    CsTemp = "IR_ADAPTOR_COMM_RESUME";
                    break;
                case EHDemoPublic.MINOR_PRINTER_ONLINE:
                    CsTemp = "PRINTER_ONLINE";
                    break;
                case EHDemoPublic.MINOR_PRINTER_OFFLINE:
                    CsTemp = "PRINTER_OFFLINE";
                    break;
                case EHDemoPublic.MINOR_4G_MOUDLE_ONLINE:
                    CsTemp = "4G_MOUDLE_ONLINE";
                    break;
                case EHDemoPublic.MINOR_4G_MOUDLE_OFFLINE:
                    CsTemp = "4G_MOUDLE_OFFLINE";
                    break;
                default:
                    CsTemp = Convert.ToString(dwMinor, 16);
                    break;
            }
            return CsTemp;
        }

        private string OperationMinorTypeMap(int dwMinor)
        {
            string CsTemp = null;
            switch (dwMinor)
            {
                case EHDemoPublic.MINOR_LOCAL_UPGRADE:
                    CsTemp = "LOCAL_UPGRADE";
                    break;
                case EHDemoPublic.MINOR_REMOTE_LOGIN:
                    CsTemp = "REMOTE_LOGIN";
                    break;
                case EHDemoPublic.MINOR_REMOTE_LOGOUT:
                    CsTemp = "REMOTE_LOGOUT";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ARM:
                    CsTemp = "REMOTE_ARM";
                    break;
                case EHDemoPublic.MINOR_REMOTE_DISARM:
                    CsTemp = "REMOTE_DISARM";
                    break;
                case EHDemoPublic.MINOR_REMOTE_REBOOT:
                    CsTemp = "REMOTE_REBOOT";
                    break;
                case EHDemoPublic.MINOR_REMOTE_UPGRADE:
                    CsTemp = "REMOTE_UPGRADE";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CFGFILE_OUTPUT:
                    CsTemp = "REMOTE_CFGFILE_OUTPUT";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CFGFILE_INTPUT:
                    CsTemp = "REMOTE_CFGFILE_INTPUT";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ALARMOUT_OPEN_MAN:
                    CsTemp = "REMOTE_ALARMOUT_OPEN_MAN";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ALARMOUT_CLOSE_MAN:
                    CsTemp = "REMOTE_ALARMOUT_CLOSE_MAN";
                    break;
                case EHDemoPublic.MINOR_REMOTE_OPEN_DOOR:
                    CsTemp = "REMOTE_OPEN_DOOR";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CLOSE_DOOR:
                    CsTemp = "REMOTE_CLOSE_DOOR";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ALWAYS_OPEN:
                    CsTemp = "REMOTE_ALWAYS_OPEN";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ALWAYS_CLOSE:
                    CsTemp = "REMOTE_ALWAYS_CLOSE";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CHECK_TIME:
                    CsTemp = "REMOTE_CHECK_TIME";
                    break;
                case EHDemoPublic.MINOR_NTP_CHECK_TIME:
                    CsTemp = "NTP_CHECK_TIME";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CLEAR_CARD:
                    CsTemp = "REMOTE_CLEAR_CARD";
                    break;
                case EHDemoPublic.MINOR_REMOTE_RESTORE_CFG:
                    CsTemp = "REMOTE_RESTORE_CFG";
                    break;
                case EHDemoPublic.MINOR_ALARMIN_ARM:
                    CsTemp = "ALARMIN_ARM";
                    break;
                case EHDemoPublic.MINOR_ALARMIN_DISARM:
                    CsTemp = "ALARMIN_DISARM";
                    break;
                case EHDemoPublic.MINOR_LOCAL_RESTORE_CFG:
                    CsTemp = "LOCAL_RESTORE_CFG";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CAPTURE_PIC:
                    CsTemp = "REMOTE_CAPTURE_PIC";
                    break;
                case EHDemoPublic.MINOR_MOD_NET_REPORT_CFG:
                    CsTemp = "MOD_NET_REPORT_CFG";
                    break;
                case EHDemoPublic.MINOR_MOD_GPRS_REPORT_PARAM:
                    CsTemp = "MOD_GPRS_REPORT_PARAM";
                    break;
                case EHDemoPublic.MINOR_MOD_REPORT_GROUP_PARAM:
                    CsTemp = "MOD_REPORT_GROUP_PARAM";
                    break;
                case EHDemoPublic.MINOR_UNLOCK_PASSWORD_OPEN_DOOR:
                    CsTemp = "UNLOCK_PASSWORD_OPEN_DOOR";
                    break;
                case EHDemoPublic.MINOR_AUTO_RENUMBER:
                    CsTemp = "AUTO_RENUMBER";
                    break;
                case EHDemoPublic.MINOR_AUTO_COMPLEMENT_NUMBER:
                    CsTemp = "AUTO_COMPLEMENT_NUMBER";
                    break;
                case EHDemoPublic.MINOR_NORMAL_CFGFILE_INPUT:
                    CsTemp = "NORMAL_CFGFILE_INPUT";
                    break;
                case EHDemoPublic.MINOR_NORMAL_CFGFILE_OUTTPUT:
                    CsTemp = "NORMAL_CFGFILE_OUTTPUT";
                    break;
                case EHDemoPublic.MINOR_CARD_RIGHT_INPUT:
                    CsTemp = "CARD_RIGHT_INPUT";
                    break;
                case EHDemoPublic.MINOR_CARD_RIGHT_OUTTPUT:
                    CsTemp = "CARD_RIGHT_OUTTPUT";
                    break;
                case EHDemoPublic.MINOR_LOCAL_USB_UPGRADE:
                    CsTemp = "LOCAL_USB_UPGRADE";
                    break;
                case EHDemoPublic.MINOR_REMOTE_VISITOR_CALL_LADDER:
                    CsTemp = "REMOTE_VISITOR_CALL_LADDER";
                    break;
                case EHDemoPublic.MINOR_REMOTE_HOUSEHOLD_CALL_LADDER:
                    CsTemp = "REMOTE_HOUSEHOLD_CALL_LADDER";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ACTUAL_GUARD:
                    CsTemp = "REMOTE_ACTUAL_GUARD";
                    break;
                case EHDemoPublic.MINOR_REMOTE_ACTUAL_UNGUARD:
                    CsTemp = "REMOTE_ACTUAL_UNGUARD";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CONTROL_NOT_CODE_OPER_FAILED:
                    CsTemp = "REMOTE_CONTROL_NOT_CODE_OPER_FAILED";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CONTROL_CLOSE_DOOR:
                    CsTemp = "REMOTE_CONTROL_CLOSE_DOOR";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CONTROL_OPEN_DOOR:
                    CsTemp = "REMOTE_CONTROL_OPEN_DOOR";
                    break;
                case EHDemoPublic.MINOR_REMOTE_CONTROL_ALWAYS_OPEN_DOOR:
                    CsTemp = "REMOTE_CONTROL_ALWAYS_OPEN_DOOR";
                    break;
                default:
                    CsTemp = Convert.ToString(dwMinor, 16);
                    break;
            }
            return CsTemp;
        }

        private string EventMinorTypeMap(int dwMinor)
        {
            string CsTemp = null;
            switch (dwMinor)
            {
                case EHDemoPublic.MINOR_LEGAL_CARD_PASS:
                    CsTemp = "LEGAL_CARD_PASS";
                    break;
                case EHDemoPublic.MINOR_CARD_AND_PSW_PASS:
                    CsTemp = "CARD_AND_PSW_PASS";
                    break;
                case EHDemoPublic.MINOR_CARD_AND_PSW_FAIL:
                    CsTemp = "CARD_AND_PSW_FAIL";
                    break;
                case EHDemoPublic.MINOR_CARD_AND_PSW_TIMEOUT:
                    CsTemp = "CARD_AND_PSW_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_CARD_AND_PSW_OVER_TIME:
                    CsTemp = "CARD_AND_PSW_OVER_TIME";
                    break;
                case EHDemoPublic.MINOR_CARD_NO_RIGHT:
                    CsTemp = "CARD_NO_RIGHT";
                    break;
                case EHDemoPublic.MINOR_CARD_INVALID_PERIOD:
                    CsTemp = "CARD_INVALID_PERIOD";
                    break;
                case EHDemoPublic.MINOR_CARD_OUT_OF_DATE:
                    CsTemp = "CARD_OUT_OF_DATE";
                    break;
                case EHDemoPublic.MINOR_INVALID_CARD:
                    CsTemp = "INVALID_CARD";
                    break;
                case EHDemoPublic.MINOR_ANTI_SNEAK_FAIL:
                    CsTemp = "ANTI_SNEAK_FAIL";
                    break;
                case EHDemoPublic.MINOR_INTERLOCK_DOOR_NOT_CLOSE:
                    CsTemp = "INTERLOCK_DOOR_NOT_CLOSE";
                    break;
                case EHDemoPublic.MINOR_NOT_BELONG_MULTI_GROUP:
                    CsTemp = "NOT_BELONG_MULTI_GROUP";
                    break;
                case EHDemoPublic.MINOR_INVALID_MULTI_VERIFY_PERIOD:
                    CsTemp = "INVALID_MULTI_VERIFY_PERIOD";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL:
                    CsTemp = "MULTI_VERIFY_SUPER_RIGHT_FAIL";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL:
                    CsTemp = "MULTI_VERIFY_REMOTE_RIGHT_FAIL";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_SUCCESS:
                    CsTemp = "MULTI_VERIFY_SUCCESS";
                    break;
                case EHDemoPublic.MINOR_LEADER_CARD_OPEN_BEGIN:
                    CsTemp = "LEADER_CARD_OPEN_BEGIN";
                    break;
                case EHDemoPublic.MINOR_LEADER_CARD_OPEN_END:
                    CsTemp = "LEADER_CARD_OPEN_END";
                    break;
                case EHDemoPublic.MINOR_ALWAYS_OPEN_BEGIN:
                    CsTemp = "ALWAYS_OPEN_BEGIN";
                    break;
                case EHDemoPublic.MINOR_ALWAYS_OPEN_END:
                    CsTemp = "ALWAYS_OPEN_END";
                    break;
                case EHDemoPublic.MINOR_LOCK_OPEN:
                    CsTemp = "LOCK_OPEN";
                    break;
                case EHDemoPublic.MINOR_LOCK_CLOSE:
                    CsTemp = "LOCK_CLOSE";
                    break;
                case EHDemoPublic.MINOR_DOOR_BUTTON_PRESS:
                    CsTemp = "DOOR_BUTTON_PRESS";
                    break;
                case EHDemoPublic.MINOR_DOOR_BUTTON_RELEASE:
                    CsTemp = "DOOR_BUTTON_RELEASE";
                    break;
                case EHDemoPublic.MINOR_DOOR_OPEN_NORMAL:
                    CsTemp = "DOOR_OPEN_NORMAL";
                    break;
                case EHDemoPublic.MINOR_DOOR_CLOSE_NORMAL:
                    CsTemp = "DOOR_CLOSE_NORMAL";
                    break;
                case EHDemoPublic.MINOR_DOOR_OPEN_ABNORMAL:
                    CsTemp = "DOOR_OPEN_ABNORMAL";
                    break;
                case EHDemoPublic.MINOR_DOOR_OPEN_TIMEOUT:
                    CsTemp = "DOOR_OPEN_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_ALARMOUT_ON:
                    CsTemp = "ALARMOUT_ON";
                    break;
                case EHDemoPublic.MINOR_ALARMOUT_OFF:
                    CsTemp = "ALARMOUT_OFF";
                    break;
                case EHDemoPublic.MINOR_ALWAYS_CLOSE_BEGIN:
                    CsTemp = "ALWAYS_CLOSE_BEGIN";
                    break;
                case EHDemoPublic.MINOR_ALWAYS_CLOSE_END:
                    CsTemp = "ALWAYS_CLOSE_END";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN:
                    CsTemp = "MULTI_VERIFY_NEED_REMOTE_OPEN";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS:
                    CsTemp = "MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_REPEAT_VERIFY:
                    CsTemp = "MULTI_VERIFY_REPEAT_VERIFY";
                    break;
                case EHDemoPublic.MINOR_MULTI_VERIFY_TIMEOUT:
                    CsTemp = "MULTI_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_DOORBELL_RINGING:
                    CsTemp = "DOORBELL_RINGING";
                    break;
                case EHDemoPublic.MINOR_FINGERPRINT_COMPARE_PASS:
                    CsTemp = "FINGERPRINT_COMPARE_PASS";
                    break;
                case EHDemoPublic.MINOR_FINGERPRINT_COMPARE_FAIL:
                    CsTemp = "FINGERPRINT_COMPARE_FAIL";
                    break;
                case EHDemoPublic.MINOR_CARD_FINGERPRINT_VERIFY_PASS:
                    CsTemp = "CARD_FINGERPRINT_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_CARD_FINGERPRINT_VERIFY_FAIL:
                    CsTemp = "CARD_FINGERPRINT_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT:
                    CsTemp = "CARD_FINGERPRINT_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS:
                    CsTemp = "CARD_FINGERPRINT_PASSWD_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL:
                    CsTemp = "CARD_FINGERPRINT_PASSWD_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT:
                    CsTemp = "CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FINGERPRINT_PASSWD_VERIFY_PASS:
                    CsTemp = "FINGERPRINT_PASSWD_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL:
                    CsTemp = "FINGERPRINT_PASSWD_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT:
                    CsTemp = "FINGERPRINT_PASSWD_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FINGERPRINT_INEXISTENCE:
                    CsTemp = "FINGERPRINT_INEXISTENCE";
                    break;
                case EHDemoPublic.MINOR_CARD_PLATFORM_VERIFY:
                    CsTemp = "CARD_PLATFORM_VERIFY";
                    break;
                case EHDemoPublic.MINOR_CALL_CENTER:
                    CsTemp = "CALL_CENTER";
                    break;
                case EHDemoPublic.MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN:
                    CsTemp = "FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN";
                    break;
                case EHDemoPublic.MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL:
                    CsTemp = "FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_FP_VERIFY_PASS:
                    CsTemp = "FACE_AND_FP_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_FP_VERIFY_FAIL:
                    CsTemp = "FACE_AND_FP_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_FP_VERIFY_TIMEOUT:
                    CsTemp = "FACE_AND_FP_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_PW_VERIFY_PASS:
                    CsTemp = "FACE_AND_PW_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_PW_VERIFY_FAIL:
                    CsTemp = "FACE_AND_PW_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_PW_VERIFY_TIMEOUT:
                    CsTemp = "FACE_AND_PW_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_CARD_VERIFY_PASS:
                    CsTemp = "FACE_AND_CARD_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_CARD_VERIFY_FAIL:
                    CsTemp = "FACE_AND_CARD_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_CARD_VERIFY_TIMEOUT:
                    CsTemp = "FACE_AND_CARD_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS:
                    CsTemp = "FACE_AND_PW_AND_FP_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL:
                    CsTemp = "FACE_AND_PW_AND_FP_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT:
                    CsTemp = "FACE_AND_PW_AND_FP_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FACE_CARD_AND_FP_VERIFY_PASS:
                    CsTemp = "FACE_CARD_AND_FP_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FACE_CARD_AND_FP_VERIFY_FAIL:
                    CsTemp = "FACE_CARD_AND_FP_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT:
                    CsTemp = "FACE_CARD_AND_FP_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS:
                    CsTemp = "EMPLOYEENO_AND_FP_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL:
                    CsTemp = "EMPLOYEENO_AND_FP_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT:
                    CsTemp = "EMPLOYEENO_AND_FP_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS:
                    CsTemp = "EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL:
                    CsTemp = "EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT:
                    CsTemp = "EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FACE_VERIFY_PASS:
                    CsTemp = "FACE_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_FACE_VERIFY_FAIL:
                    CsTemp = "FACE_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS:
                    CsTemp = "EMPLOYEENO_AND_FACE_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL:
                    CsTemp = "EMPLOYEENO_AND_FACE_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT:
                    CsTemp = "EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FACE_RECOGNIZE_FAIL:
                    CsTemp = "FACE_RECOGNIZE_FAIL";
                    break;
                case EHDemoPublic.MINOR_FIRSTCARD_AUTHORIZE_BEGIN:
                    CsTemp = "FIRSTCARD_AUTHORIZE_BEGIN";
                    break;
                case EHDemoPublic.MINOR_FIRSTCARD_AUTHORIZE_END:
                    CsTemp = "FIRSTCARD_AUTHORIZE_END";
                    break;
                case EHDemoPublic.MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT:
                    CsTemp = "DOORLOCK_INPUT_SHORT_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT:
                    CsTemp = "DOORLOCK_INPUT_BROKEN_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_DOORLOCK_INPUT_EXCEPTION:
                    CsTemp = "DOORLOCK_INPUT_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT:
                    CsTemp = "DOORCONTACT_INPUT_SHORT_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT:
                    CsTemp = "DOORCONTACT_INPUT_BROKEN_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_DOORCONTACT_INPUT_EXCEPTION:
                    CsTemp = "DOORCONTACT_INPUT_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT:
                    CsTemp = "OPENBUTTON_INPUT_SHORT_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT:
                    CsTemp = "OPENBUTTON_INPUT_BROKEN_CIRCUIT";
                    break;
                case EHDemoPublic.MINOR_OPENBUTTON_INPUT_EXCEPTION:
                    CsTemp = "OPENBUTTON_INPUT_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_DOORLOCK_OPEN_EXCEPTION:
                    CsTemp = "DOORLOCK_OPEN_EXCEPTION";
                    break;
                case EHDemoPublic.MINOR_DOORLOCK_OPEN_TIMEOUT:
                    CsTemp = "DOORLOCK_OPEN_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE:
                    CsTemp = "FIRSTCARD_OPEN_WITHOUT_AUTHORIZE";
                    break;
                case EHDemoPublic.MINOR_CALL_LADDER_RELAY_BREAK:
                    CsTemp = "CALL_LADDER_RELAY_BREAK";
                    break;
                case EHDemoPublic.MINOR_CALL_LADDER_RELAY_CLOSE:
                    CsTemp = "CALL_LADDER_RELAY_CLOSE";
                    break;
                case EHDemoPublic.MINOR_AUTO_KEY_RELAY_BREAK:
                    CsTemp = "AUTO_KEY_RELAY_BREAK";
                    break;
                case EHDemoPublic.MINOR_AUTO_KEY_RELAY_CLOSE:
                    CsTemp = "AUTO_KEY_RELAY_CLOSE";
                    break;
                case EHDemoPublic.MINOR_KEY_CONTROL_RELAY_BREAK:
                    CsTemp = "KEY_CONTROL_RELAY_BREAK";
                    break;
                case EHDemoPublic.MINOR_KEY_CONTROL_RELAY_CLOSE:
                    CsTemp = "KEY_CONTROL_RELAY_CLOSE";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_PW_PASS:
                    CsTemp = "EMPLOYEENO_AND_PW_PASS";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_PW_FAIL:
                    CsTemp = "EMPLOYEENO_AND_PW_FAIL";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEENO_AND_PW_TIMEOUT:
                    CsTemp = "EMPLOYEENO_AND_PW_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_HUMAN_DETECT_FAIL:
                    CsTemp = "HUMAN_DETECT_FAIL";
                    break;
                case EHDemoPublic.MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS:
                    CsTemp = "PEOPLE_AND_ID_CARD_COMPARE_PASS";
                    break;
                case EHDemoPublic.MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL:
                    CsTemp = "PEOPLE_AND_ID_CARD_COMPARE_FAIL";
                    break;
                case EHDemoPublic.MINOR_CERTIFICATE_BLACK_LIST:
                    CsTemp = "CERTIFICATE_BLACK_LIST";
                    break;
                case EHDemoPublic.MINOR_LEGAL_MESSAGE:
                    CsTemp = "LEGAL_MESSAGE";
                    break;
                case EHDemoPublic.MINOR_ILLEGAL_MESSAGE:
                    CsTemp = "ILLEGAL_MESSAGE";
                    break;
                case EHDemoPublic.MINOR_MAC_DETECT:
                    CsTemp = "MAC_DETECT";
                    break;
                case EHDemoPublic.MINOR_DOOR_OPEN_OR_DORMANT_FAIL:
                    CsTemp = "DOOR_OPEN_OR_DORMANT_FAIL";
                    break;
                case EHDemoPublic.MINOR_AUTH_PLAN_DORMANT_FAIL:
                    CsTemp = "AUTH_PLAN_DORMANT_FAIL";
                    break;
                case EHDemoPublic.MINOR_CARD_ENCRYPT_VERIFY_FAIL:
                    CsTemp = "CARD_ENCRYPT_VERIFY_FAIL";
                    break;
                case EHDemoPublic.MINOR_SUBMARINEBACK_REPLY_FAIL:
                    CsTemp = "SUBMARINEBACK_REPLY_FAIL";
                    break;
                case EHDemoPublic.MINOR_DOOR_OPEN_OR_DORMANT_OPEN_FAIL:
                    CsTemp = "DOOR_OPEN_OR_DORMANT_OPEN_FAIL";
                    break;
                case EHDemoPublic.MINOR_DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL:
                    CsTemp = "DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL";
                    break;
                case EHDemoPublic.MINOR_TRAILING:
                    CsTemp = "TRAILING";
                    break;
                case EHDemoPublic.MINOR_HEART_BEAT:
                    CsTemp = "HEART_BEAT";
                    break;
                case EHDemoPublic.MINOR_REVERSE_ACCESS:
                    CsTemp = "REVERSE_ACCESS";
                    break;
                case EHDemoPublic.MINOR_FORCE_ACCESS:
                    CsTemp = "FORCE_ACCESS";
                    break;
                case EHDemoPublic.MINOR_CLIMBING_OVER_GATE:
                    CsTemp = "CLIMBING_OVER_GATE";
                    break;
                case EHDemoPublic.MINOR_PASSING_TIMEOUT:
                    CsTemp = "PASSING_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_INTRUSION_ALARM:
                    CsTemp = "INTRUSION_ALARM";
                    break;
                case EHDemoPublic.MINOR_FREE_GATE_PASS_NOT_AUTH:
                    CsTemp = "FREE_GATE_PASS_NOT_AUTH";
                    break;
                case EHDemoPublic.MINOR_DROP_ARM_BLOCK:
                    CsTemp = "DROP_ARM_BLOCK";
                    break;
                case EHDemoPublic.MINOR_DROP_ARM_BLOCK_RESUME:
                    CsTemp = "DROP_ARM_BLOCK_RESUME";
                    break;
                case EHDemoPublic.MINOR_LOCAL_FACE_MODELING_FAIL:
                    CsTemp = "LOCAL_FACE_MODELING_FAIL";
                    break;
                case EHDemoPublic.MINOR_STAY_EVENT:
                    CsTemp = "STAY_EVENT";
                    break;
                case EHDemoPublic.MINOR_PASSWORD_MISMATCH:
                    CsTemp = "PASSWORD_MISMATCH";
                    break;
                case EHDemoPublic.MINOR_EMPLOYEE_NO_NOT_EXIST:
                    CsTemp = "EMPLOYEE_NO_NOT_EXIST";
                    break;
                case EHDemoPublic.MINOR_COMBINED_VERIFY_PASS:
                    CsTemp = "COMBINED_VERIFY_PASS";
                    break;
                case EHDemoPublic.MINOR_COMBINED_VERIFY_TIMEOUT:
                    CsTemp = "COMBINED_VERIFY_TIMEOUT";
                    break;
                case EHDemoPublic.MINOR_VERIFY_MODE_MISMATCH:
                    CsTemp = "VERIFY_MODE_MISMATCH";
                    break;

                default:
                    CsTemp = Convert.ToString(dwMinor, 16);
                    break;
            }
            return CsTemp;
        }

        private string ProcessMinorType(string dwMajor, string dwMinor)
        {
            string CsTemp = null;
            int iMajor=0;
            int iMinor = 0;
            int.TryParse(dwMajor,out iMajor);
            int.TryParse(dwMinor,out iMinor);
            switch (iMajor)
            {
                case EHDemoPublic.MAJOR_ALARM:
                    CsTemp = AlarmMinorTypeMap(iMinor);
                    break;
                case EHDemoPublic.MAJOR_EXCEPTION:
                    CsTemp = ExceptionMinorTypeMap(iMinor);
                    break;
                case EHDemoPublic.MAJOR_OPERATION:
                    CsTemp = OperationMinorTypeMap(iMinor);
                    break;
                case EHDemoPublic.MAJOR_EVENT:
                    CsTemp = EventMinorTypeMap(iMinor);
                    break;
                default:
                    CsTemp = "Unknown";
                    break;
            }
            return CsTemp;
        }

        private string CardTypeMap(string byCardType)
        {
            string CsTemp = null;
            int iCardType = 0;
            int.TryParse(byCardType, out iCardType);
            switch (iCardType)
            {
                case 1:
                    CsTemp = "Ordinary Card";
                    break;
                case 2:
                    CsTemp = "Disabled Card";
                    break;
                case 3:
                    CsTemp = "Black List Card";
                    break;
                case 4:
                    CsTemp = "Patrol Card";
                    break;
                case 5:
                    CsTemp = "Stress Card";
                    break;
                case 6:
                    CsTemp = "Super Card";
                    break;
                case 7:
                    CsTemp = "Guest Card";
                    break;
                case 8:
                    CsTemp = "Release Card";
                    break;
                default:
                    CsTemp = "No effect";
                    break;
            }
            return CsTemp;
        }

        private string ProcessReportChannel(string szReportChannel)
        {
            string CsTemp = null;
            int iReportChannel = 0;
            int.TryParse(szReportChannel, out iReportChannel);
            switch (iReportChannel)
            {
                case 1:
                    CsTemp = "Upload";
                    break;
                case 2:
                    CsTemp = "Center 1 Upload";
                    break;
                case 3:
                    CsTemp = "Center 2 Upload";
                    break;
                default:
                    CsTemp = "No effect";
                    break;
            }
            return CsTemp;
        }

        private string ProcessCardReader(string szCardReaderKind)
        {
            string CsTemp = null;
            int iCardReaderKind = 0;
            int.TryParse(szCardReaderKind, out iCardReaderKind);
            switch (iCardReaderKind)
            {
                case 1:
                    CsTemp = "IC Reader";
                    break;
                case 2:
                    CsTemp = "Certificate Reader";
                    break;
                case 3:
                    CsTemp = "Two-dimension Reader";
                    break;
                case 4:
                    CsTemp = "Finger Print Head";
                    break;
                default:
                    CsTemp = "No effect";
                    break;
            }
            return CsTemp;
        }

        private string ProcessByType(string szType)
        {
            string CsTemp = null;
            int iType = 0;
            int.TryParse(szType, out iType);
            switch (iType)
            {
                case 0:
                    CsTemp = "Instant Zone";
                    break;
                case 1:
                    CsTemp = "24 Hour Zone";
                    break;
                case 2:
                    CsTemp = "Delay Zone";
                    break;
                case 3:
                    CsTemp = "Internal Zone";
                    break;
                case 4:
                    CsTemp = "Key Zone";
                    break;
                case 5:
                    CsTemp = "Fire Zone";
                    break;
                case 6:
                    CsTemp = "Perimeter Zone";
                    break;
                case 7:
                    CsTemp = "24 Hour Silent Zone";
                    break;
                case 8:
                    CsTemp = "24 Hour Auxiliary Zone";
                    break;
                case 9:
                    CsTemp = "24 Hour Vibration Zone";
                    break;
                case 10:
                    CsTemp = "Acs Emergency Open Zone";
                    break;
                case 11:
                    CsTemp = "Acs Emergency Close Zone";
                    break;
                default:
                    CsTemp = "No Effect";
                    break;
            }
            return CsTemp;
        }
        private string ProcessInternatAccess(string szInternetAccess)
        {
            string CsTemp = null;
            int iInternetAccess = 0;
            int.TryParse(szInternetAccess, out iInternetAccess);
            switch (iInternetAccess)
            {
                case 1:
                    CsTemp = "Up Network Port 1";
                    break;
                case 2:
                    CsTemp = "Up Network Port 2";
                    break;
                case 3:
                    CsTemp = "Down Network Port 1";
                    break;
                default:
                    CsTemp = "No effect";
                    break;
            }
            return CsTemp;
        }

        private string ProcessSwipeCard(string szSwipeCardType)
        {
            string CsTemp = null;
            int iSwipeCardType = 0;
            int.TryParse(szSwipeCardType, out iSwipeCardType);
            if (iSwipeCardType == 1)
            {
                CsTemp = "QR Code";
            }
            else
            {
                CsTemp = "No effect";
            }
            return CsTemp;
        }
        public void ExportToExecl()
        {
            //保存文件对话框
            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xls";
            sfd.Filter = "Excel文件(*.xls)|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DoExport(this.listViewEvent, sfd.FileName);
            }
        }
        private void DoExport(ListView listView, string strFileName)
        {
            int rowNum = listView.Items.Count;
            int columnNum = listView.Items[0].SubItems.Count;
            int rowIndex = 1;
            int columnIndex = 0;
            if (rowNum == 0 || string.IsNullOrEmpty(strFileName))
            {
                return;
            }
            if (rowNum > 0)
            {

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
                    return;
                }
                xlApp.DefaultFilePath = "";
                //设置是否显示警告窗体
                xlApp.DisplayAlerts = false;
                //设置是否显示Excel
                xlApp.Visible = false;
                //设置工作表sheet1
                xlApp.SheetsInNewWorkbook = 1;
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                //将ListView的列名导入Excel表第一行
                foreach (ColumnHeader dc in listView.Columns)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = dc.Text;
                }
                //将ListView中的数据导入Excel中
                for (int i = 0; i < rowNum; i++)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for (int j = 0; j < columnNum; j++)
                    {
                        columnIndex++;
                        //注意这个在导出的时候加了“\t” 的目的就是避免导出的数据显示为科学计数法。可以放在每行的首尾。
                        xlApp.Cells[rowIndex, columnIndex] = Convert.ToString(listView.Items[i].SubItems[j].Text) + "\t";
                    }
                }
                xlBook.SaveAs(strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlApp = null;
                xlBook = null;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExecl();
        }
    }
}
