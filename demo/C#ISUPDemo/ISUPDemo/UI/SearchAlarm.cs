using EHomeDemo.Public;
using Newtonsoft.Json;
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

namespace EHomeDemo.UI
{
    public partial class SearchAlarm : Form
    {
        private int m_iDeviceIndex;
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        public SearchAlarm()
        {
            InitializeComponent();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
            comboBoxMainType.SelectedIndex = 0;
            comboBoxSecondType.SelectedIndex = 0;
        }

        public class InfoList
        {
            public string major { get; set; }
            public string minor { get; set; }
            public string time { get; set; }
            public string netUser { get; set; }
            public string remoteHostAddr { get; set; }
            public string cardNo { get; set; }
            public string cardType { get; set; }
            public string whiteListNo { get; set; }
            public string reportChannel { get; set; }
            public string cardReaderKind { get; set; }
            public string cardReaderNo { get; set; }
            public string doorNo { get; set; }
            public string verifyNo { get; set; }
            public string alarmInNo { get; set; }
            public string alarmOutNo { get; set; }
            public string caseSensorNo { get; set; }
            public string RS485No { get; set; }
            public string multiCardGroupNo { get; set; }
            public string accessChannel { get; set; }
            public string deviceNo { get; set; }
            public string distractControlNo { get; set; }
            public string employeeNoString { get; set; }
            public string localControllerID { get; set; }
            public string InternetAccess { get; set; }
            public string type { get; set; }
            public string MACAddr { get; set; }
            public string swipeCardType { get; set; }
            public string serialNo { get; set; }
            public string userType { get; set; }
            public string attendanceStatus { get; set; }
            public string statusValue { get; set; }
        }

        public class AcsEvent
        {
            public string searchID { get; set; }
            public string responseStatusStrg { get; set; }
            public string numOfMatches { get; set; }
            public string totalMatches { get; set; }
            public List<InfoList> InfoList { get; set; }
        }

        public class RootObject
        {
            public AcsEvent AcsEvent { get; set; }
        }

        private Dictionary<string, uint> MajorTypeDictionary = new Dictionary<string, uint>()
        {
            {"All",0},
            {"Alarm",1},
            {"Exception",2},
            {"Operation",3},
            {"Event",5}
        };

        private static Dictionary<string, uint> MinorTypeDictionary = new Dictionary<string, uint>() 
        {
            {"All",0},
            {"ALARMIN_SHORT_CIRCUIT",HCEHomePublic.MINOR_ALARMIN_SHORT_CIRCUIT},
            {"ALARMIN_BROKEN_CIRCUIT",HCEHomePublic.MINOR_ALARMIN_BROKEN_CIRCUIT},
            {"ALARMIN_EXCEPTION",HCEHomePublic.MINOR_ALARMIN_EXCEPTION},
            {"ALARMIN_RESUME",HCEHomePublic.MINOR_ALARMIN_RESUME},
            {"HOST_DESMANTLE_ALARM",HCEHomePublic.MINOR_HOST_DESMANTLE_ALARM},
            {"HOST_DESMANTLE_RESUME",HCEHomePublic.MINOR_HOST_DESMANTLE_RESUME},
            {"CARD_READER_DESMANTLE_ALARM",HCEHomePublic.MINOR_CARD_READER_DESMANTLE_ALARM},
            {"CARD_READER_DESMANTLE_RESUME",HCEHomePublic.MINOR_CARD_READER_DESMANTLE_RESUME},
            {"CASE_SENSOR_ALARM",HCEHomePublic.MINOR_CASE_SENSOR_ALARM},
            {"CASE_SENSOR_RESUME",HCEHomePublic.MINOR_CASE_SENSOR_RESUME},
            {"STRESS_ALARM",HCEHomePublic.MINOR_STRESS_ALARM},
            {"OFFLINE_ECENT_NEARLY_FULL",HCEHomePublic.MINOR_OFFLINE_ECENT_NEARLY_FULL},
            {"CARD_MAX_AUTHENTICATE_FAIL",HCEHomePublic.MINOR_CARD_MAX_AUTHENTICATE_FAIL},
            {"SD_CARD_FULL",HCEHomePublic.MINOR_SD_CARD_FULL},
            {"LINKAGE_CAPTURE_PIC",HCEHomePublic.MINOR_LINKAGE_CAPTURE_PIC},
            {"SECURITY_MODULE_DESMANTLE_ALARM",HCEHomePublic.MINOR_SECURITY_MODULE_DESMANTLE_ALARM},
            {"SECURITY_MODULE_DESMANTLE_RESUME",HCEHomePublic.MINOR_SECURITY_MODULE_DESMANTLE_RESUME},
            {"POS_START_ALARM",HCEHomePublic.MINOR_POS_START_ALARM},
            {"POS_END_ALARM",HCEHomePublic.MINOR_POS_END_ALARM},
            {"FACE_IMAGE_QUALITY_LOW",HCEHomePublic.MINOR_FACE_IMAGE_QUALITY_LOW},
            {"FINGE_RPRINT_QUALITY_LOW",HCEHomePublic.MINOR_FINGE_RPRINT_QUALITY_LOW},
            {"FIRE_IMPORT_BROKEN_CIRCUIT",HCEHomePublic.MINOR_FIRE_IMPORT_BROKEN_CIRCUIT},
            {"FIRE_IMPORT_RESUME",HCEHomePublic.MINOR_FIRE_IMPORT_RESUME},
            {"FIRE_BUTTON_TRIGGER",HCEHomePublic.MINOR_FIRE_BUTTON_TRIGGER},
            {"FIRE_BUTTON_RESUME",HCEHomePublic.MINOR_FIRE_BUTTON_RESUME},
            {"MAINTENANCE_BUTTON_TRIGGER",HCEHomePublic.MINOR_MAINTENANCE_BUTTON_TRIGGER},
            {"MAINTENANCE_BUTTON_RESUME",HCEHomePublic.MINOR_MAINTENANCE_BUTTON_RESUME},
            {"EMERGENCY_BUTTON_TRIGGER",HCEHomePublic.MINOR_EMERGENCY_BUTTON_TRIGGER},
            {"EMERGENCY_BUTTON_RESUME",HCEHomePublic.MINOR_EMERGENCY_BUTTON_RESUME},
            {"DISTRACT_CONTROLLER_ALARM",HCEHomePublic.MINOR_DISTRACT_CONTROLLER_ALARM},
            {"DISTRACT_CONTROLLER_RESUME",HCEHomePublic.MINOR_DISTRACT_CONTROLLER_RESUME},
            {"CHANNEL_CONTROLLER_DESMANTLE_ALARM",HCEHomePublic.MINOR_CHANNEL_CONTROLLER_DESMANTLE_ALARM},
            {"CHANNEL_CONTROLLER_DESMANTLE_RESUME",HCEHomePublic.MINOR_CHANNEL_CONTROLLER_DESMANTLE_RESUME},
            {"CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM",HCEHomePublic.MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM},
            {"CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME",HCEHomePublic.MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME},
            {"PRINTER_OUT_OF_PAPER",HCEHomePublic.MINOR_PRINTER_OUT_OF_PAPER},
            {"LEGAL_EVENT_NEARLY_FULL",HCEHomePublic.MINOR_LEGAL_EVENT_NEARLY_FULL},
            {"NET_BROKEN",HCEHomePublic.MINOR_NET_BROKEN},
            {"RS485_DEVICE_ABNORMAL",HCEHomePublic.MINOR_RS485_DEVICE_ABNORMAL},
            {"RS485_DEVICE_REVERT",HCEHomePublic.MINOR_RS485_DEVICE_REVERT},
            {"DEV_POWER_ON",HCEHomePublic.MINOR_DEV_POWER_ON},
            {"DEV_POWER_OFF",HCEHomePublic.MINOR_DEV_POWER_OFF},
            {"WATCH_DOG_RESET",HCEHomePublic.MINOR_WATCH_DOG_RESET},
            {"LOW_BATTERY",HCEHomePublic.MINOR_LOW_BATTERY},
            {"BATTERY_RESUME",HCEHomePublic.MINOR_BATTERY_RESUME},
            {"AC_OFF",HCEHomePublic.MINOR_AC_OFF},
            {"AC_RESUME",HCEHomePublic.MINOR_AC_RESUME},
            {"NET_RESUME",HCEHomePublic.MINOR_NET_RESUME},
            {"FLASH_ABNORMAL",HCEHomePublic.MINOR_FLASH_ABNORMAL},
            {"CARD_READER_OFFLINE",HCEHomePublic.MINOR_CARD_READER_OFFLINE},
            {"CARD_READER_RESUME",HCEHomePublic.MINOR_CARD_READER_RESUME},
            {"INDICATOR_LIGHT_OFF",HCEHomePublic.MINOR_INDICATOR_LIGHT_OFF},
            {"INDICATOR_LIGHT_RESUME",HCEHomePublic.MINOR_INDICATOR_LIGHT_RESUME},
            {"CHANNEL_CONTROLLER_OFF",HCEHomePublic.MINOR_CHANNEL_CONTROLLER_OFF},
            {"CHANNEL_CONTROLLER_RESUME",HCEHomePublic.MINOR_CHANNEL_CONTROLLER_RESUME},
            {"SECURITY_MODULE_OFF",HCEHomePublic.MINOR_SECURITY_MODULE_OFF},
            {"SECURITY_MODULE_RESUME",HCEHomePublic.MINOR_SECURITY_MODULE_RESUME},
            {"BATTERY_ELECTRIC_LOW",HCEHomePublic.MINOR_BATTERY_ELECTRIC_LOW},
            {"BATTERY_ELECTRIC_RESUME",HCEHomePublic.MINOR_BATTERY_ELECTRIC_RESUME},
            {"LOCAL_CONTROL_NET_BROKEN",HCEHomePublic.MINOR_LOCAL_CONTROL_NET_BROKEN},
            {"LOCAL_CONTROL_NET_RSUME",HCEHomePublic.MINOR_LOCAL_CONTROL_NET_RSUME},
            {"MASTER_RS485_LOOPNODE_BROKEN",HCEHomePublic.MINOR_MASTER_RS485_LOOPNODE_BROKEN},
            {"MASTER_RS485_LOOPNODE_RESUME",HCEHomePublic.MINOR_MASTER_RS485_LOOPNODE_RESUME},
            {"LOCAL_CONTROL_OFFLINE",HCEHomePublic.MINOR_LOCAL_CONTROL_OFFLINE},
            {"LOCAL_CONTROL_RESUME",HCEHomePublic.MINOR_LOCAL_CONTROL_RESUME},
            {"LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN",HCEHomePublic.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN},
            {"LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME",HCEHomePublic.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME},
            {"DISTRACT_CONTROLLER_ONLINE",HCEHomePublic.MINOR_DISTRACT_CONTROLLER_ONLINE},
            {"DISTRACT_CONTROLLER_OFFLINE",HCEHomePublic.MINOR_DISTRACT_CONTROLLER_OFFLINE},
            {"ID_CARD_READER_NOT_CONNECT",HCEHomePublic.MINOR_ID_CARD_READER_NOT_CONNECT},
            {"ID_CARD_READER_RESUME",HCEHomePublic.MINOR_ID_CARD_READER_RESUME},
            {"FINGER_PRINT_MODULE_NOT_CONNECT",HCEHomePublic.MINOR_FINGER_PRINT_MODULE_NOT_CONNECT},
            {"FINGER_PRINT_MODULE_RESUME",HCEHomePublic.MINOR_FINGER_PRINT_MODULE_RESUME},
            {"CAMERA_NOT_CONNECT",HCEHomePublic.MINOR_CAMERA_NOT_CONNECT},
            {"CAMERA_RESUME",HCEHomePublic.MINOR_CAMERA_RESUME},
            {"COM_NOT_CONNECT",HCEHomePublic.MINOR_COM_NOT_CONNECT},
            {"COM_RESUME",HCEHomePublic.MINOR_COM_RESUME},
            {"DEVICE_NOT_AUTHORIZE",HCEHomePublic.MINOR_DEVICE_NOT_AUTHORIZE},
            {"PEOPLE_AND_ID_CARD_DEVICE_ONLINE",HCEHomePublic.MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE},
            {"PEOPLE_AND_ID_CARD_DEVICE_OFFLINE",HCEHomePublic.MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE},
            {"LOCAL_LOGIN_LOCK",HCEHomePublic.MINOR_LOCAL_LOGIN_LOCK},
            {"LOCAL_LOGIN_UNLOCK",HCEHomePublic.MINOR_LOCAL_LOGIN_UNLOCK},
            {"SUBMARINEBACK_COMM_BREAK",HCEHomePublic.MINOR_SUBMARINEBACK_COMM_BREAK},
            {"SUBMARINEBACK_COMM_RESUME",HCEHomePublic.MINOR_SUBMARINEBACK_COMM_RESUME},
            {"MOTOR_SENSOR_EXCEPTION",HCEHomePublic.MINOR_MOTOR_SENSOR_EXCEPTION},
            {"CAN_BUS_EXCEPTION",HCEHomePublic.MINOR_CAN_BUS_EXCEPTION},
            {"CAN_BUS_RESUME",HCEHomePublic.MINOR_CAN_BUS_RESUME},
            {"GATE_TEMPERATURE_OVERRUN",HCEHomePublic.MINOR_GATE_TEMPERATURE_OVERRUN},
            {"IR_EMITTER_EXCEPTION",HCEHomePublic.MINOR_IR_EMITTER_EXCEPTION},
            {"IR_EMITTER_RESUME",HCEHomePublic.MINOR_IR_EMITTER_RESUME},
            {"LAMP_BOARD_COMM_EXCEPTION",HCEHomePublic.MINOR_LAMP_BOARD_COMM_EXCEPTION},
            {"LAMP_BOARD_COMM_RESUME",HCEHomePublic.MINOR_LAMP_BOARD_COMM_RESUME},
            {"IR_ADAPTOR_COMM_EXCEPTION",HCEHomePublic.MINOR_IR_ADAPTOR_COMM_EXCEPTION},
            {"IR_ADAPTOR_COMM_RESUME",HCEHomePublic.MINOR_IR_ADAPTOR_COMM_RESUME},
            {"PRINTER_ONLINE",HCEHomePublic.MINOR_PRINTER_ONLINE},
            {"PRINTER_OFFLINE",HCEHomePublic.MINOR_PRINTER_OFFLINE},
            {"4G_MOUDLE_ONLINE",HCEHomePublic.MINOR_4G_MOUDLE_ONLINE},
            {"4G_MOUDLE_OFFLINE",HCEHomePublic.MINOR_4G_MOUDLE_OFFLINE},
            {"LOCAL_UPGRADE",HCEHomePublic.MINOR_LOCAL_UPGRADE},
            {"REMOTE_LOGIN",HCEHomePublic.MINOR_REMOTE_LOGIN},
            {"REMOTE_LOGOUT",HCEHomePublic.MINOR_REMOTE_LOGOUT},
            {"MINOR_REMOTE_ARM",HCEHomePublic.MINOR_REMOTE_ARM},
            {"REMOTE_DISARM",HCEHomePublic.MINOR_REMOTE_DISARM},
            {"REMOTE_REBOOT",HCEHomePublic.MINOR_REMOTE_REBOOT},
            {"REMOTE_UPGRADE",HCEHomePublic.MINOR_REMOTE_UPGRADE},
            {"REMOTE_CFGFILE_OUTPUT",HCEHomePublic.MINOR_REMOTE_CFGFILE_OUTPUT},
            {"REMOTE_CFGFILE_INTPUT",HCEHomePublic.MINOR_REMOTE_CFGFILE_INTPUT},
            {"REMOTE_ALARMOUT_OPEN_MAN",HCEHomePublic.MINOR_REMOTE_ALARMOUT_OPEN_MAN},
            {"REMOTE_ALARMOUT_CLOSE_MAN",HCEHomePublic.MINOR_REMOTE_ALARMOUT_CLOSE_MAN},
            {"REMOTE_OPEN_DOOR",HCEHomePublic.MINOR_REMOTE_OPEN_DOOR},
            {"REMOTE_CLOSE_DOOR",HCEHomePublic.MINOR_REMOTE_CLOSE_DOOR},
            {"REMOTE_ALWAYS_OPEN",HCEHomePublic.MINOR_REMOTE_ALWAYS_OPEN},
            {"REMOTE_ALWAYS_CLOSE",HCEHomePublic.MINOR_REMOTE_ALWAYS_CLOSE},
            {"REMOTE_CHECK_TIME",HCEHomePublic.MINOR_REMOTE_CHECK_TIME},
            {"NTP_CHECK_TIME",HCEHomePublic.MINOR_NTP_CHECK_TIME},
            {"REMOTE_CLEAR_CARD",HCEHomePublic.MINOR_REMOTE_CLEAR_CARD},
            {"REMOTE_RESTORE_CFG",HCEHomePublic.MINOR_REMOTE_RESTORE_CFG},
            {"ALARMIN_ARM",HCEHomePublic.MINOR_ALARMIN_ARM},
            {"ALARMIN_DISARM",HCEHomePublic.MINOR_ALARMIN_DISARM},
            {"LOCAL_RESTORE_CFG",HCEHomePublic.MINOR_LOCAL_RESTORE_CFG},
            {"REMOTE_CAPTURE_PIC",HCEHomePublic.MINOR_REMOTE_CAPTURE_PIC},
            {"MOD_NET_REPORT_CFG",HCEHomePublic.MINOR_MOD_NET_REPORT_CFG},
            {"MOD_GPRS_REPORT_PARAM",HCEHomePublic.MINOR_MOD_GPRS_REPORT_PARAM},
            {"MOD_REPORT_GROUP_PARAM",HCEHomePublic.MINOR_MOD_REPORT_GROUP_PARAM},
            {"UNLOCK_PASSWORD_OPEN_DOOR",HCEHomePublic.MINOR_UNLOCK_PASSWORD_OPEN_DOOR},
            {"AUTO_RENUMBER",HCEHomePublic.MINOR_AUTO_RENUMBER},
            {"AUTO_COMPLEMENT_NUMBER",HCEHomePublic.MINOR_AUTO_COMPLEMENT_NUMBER},
            {"NORMAL_CFGFILE_INPUT",HCEHomePublic.MINOR_NORMAL_CFGFILE_INPUT},
            {"NORMAL_CFGFILE_OUTTPUT",HCEHomePublic.MINOR_NORMAL_CFGFILE_OUTTPUT},
            {"CARD_RIGHT_INPUT",HCEHomePublic.MINOR_CARD_RIGHT_INPUT},
            {"CARD_RIGHT_OUTTPUT",HCEHomePublic.MINOR_CARD_RIGHT_OUTTPUT},
            {"LOCAL_USB_UPGRADE",HCEHomePublic.MINOR_LOCAL_USB_UPGRADE},
            {"REMOTE_VISITOR_CALL_LADDER",HCEHomePublic.MINOR_REMOTE_VISITOR_CALL_LADDER},
            {"REMOTE_HOUSEHOLD_CALL_LADDER",HCEHomePublic.MINOR_REMOTE_HOUSEHOLD_CALL_LADDER},
            {"REMOTE_ACTUAL_GUARD",HCEHomePublic.MINOR_REMOTE_ACTUAL_GUARD},
            {"REMOTE_ACTUAL_UNGUARD",HCEHomePublic.MINOR_REMOTE_ACTUAL_UNGUARD},
            {"REMOTE_CONTROL_NOT_CODE_OPER_FAILED",HCEHomePublic.MINOR_REMOTE_CONTROL_NOT_CODE_OPER_FAILED},
            {"REMOTE_CONTROL_CLOSE_DOOR",HCEHomePublic.MINOR_REMOTE_CONTROL_CLOSE_DOOR},
            {"REMOTE_CONTROL_OPEN_DOOR",HCEHomePublic.MINOR_REMOTE_CONTROL_OPEN_DOOR},
            {"REMOTE_CONTROL_ALWAYS_OPEN_DOOR",HCEHomePublic.MINOR_REMOTE_CONTROL_ALWAYS_OPEN_DOOR},
            {"LEGAL_CARD_PASS",HCEHomePublic.MINOR_LEGAL_CARD_PASS},
            {"CARD_AND_PSW_PASS",HCEHomePublic.MINOR_CARD_AND_PSW_PASS},
            {"CARD_AND_PSW_FAIL",HCEHomePublic.MINOR_CARD_AND_PSW_FAIL},
            {"CARD_AND_PSW_TIMEOUT",HCEHomePublic.MINOR_CARD_AND_PSW_TIMEOUT},
            {"CARD_AND_PSW_OVER_TIME",HCEHomePublic.MINOR_CARD_AND_PSW_OVER_TIME},
            {"CARD_NO_RIGHT",HCEHomePublic.MINOR_CARD_NO_RIGHT},
            {"CARD_INVALID_PERIOD",HCEHomePublic.MINOR_CARD_INVALID_PERIOD},
            {"CARD_OUT_OF_DATE",HCEHomePublic.MINOR_CARD_OUT_OF_DATE},
            {"INVALID_CARD",HCEHomePublic.MINOR_INVALID_CARD},
            {"ANTI_SNEAK_FAIL",HCEHomePublic.MINOR_ANTI_SNEAK_FAIL},
            {"INTERLOCK_DOOR_NOT_CLOSE",HCEHomePublic.MINOR_INTERLOCK_DOOR_NOT_CLOSE},
            {"NOT_BELONG_MULTI_GROUP",HCEHomePublic.MINOR_NOT_BELONG_MULTI_GROUP},
            {"INVALID_MULTI_VERIFY_PERIOD",HCEHomePublic.MINOR_INVALID_MULTI_VERIFY_PERIOD},
            {"MULTI_VERIFY_SUPER_RIGHT_FAIL",HCEHomePublic.MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL},
            {"MULTI_VERIFY_REMOTE_RIGHT_FAIL",HCEHomePublic.MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL},
            {"MULTI_VERIFY_SUCCESS",HCEHomePublic.MINOR_MULTI_VERIFY_SUCCESS},
            {"LEADER_CARD_OPEN_BEGIN",HCEHomePublic.MINOR_LEADER_CARD_OPEN_BEGIN},
            {"LEADER_CARD_OPEN_END",HCEHomePublic.MINOR_LEADER_CARD_OPEN_END},
            {"ALWAYS_OPEN_BEGIN",HCEHomePublic.MINOR_ALWAYS_OPEN_BEGIN},
            {"ALWAYS_OPEN_END",HCEHomePublic.MINOR_ALWAYS_OPEN_END},
            {"LOCK_OPEN",HCEHomePublic.MINOR_LOCK_OPEN},
            {"LOCK_CLOSE",HCEHomePublic.MINOR_LOCK_CLOSE},
            {"DOOR_BUTTON_PRESS",HCEHomePublic.MINOR_DOOR_BUTTON_PRESS},
            {"DOOR_BUTTON_RELEASE",HCEHomePublic.MINOR_DOOR_BUTTON_RELEASE},
            {"DOOR_OPEN_NORMAL",HCEHomePublic.MINOR_DOOR_OPEN_NORMAL},
            {"DOOR_CLOSE_NORMAL",HCEHomePublic.MINOR_DOOR_CLOSE_NORMAL},
            {"DOOR_OPEN_ABNORMAL",HCEHomePublic.MINOR_DOOR_OPEN_ABNORMAL},
            {"DOOR_OPEN_TIMEOUT",HCEHomePublic.MINOR_DOOR_OPEN_TIMEOUT},
            {"ALARMOUT_ON",HCEHomePublic.MINOR_ALARMOUT_ON},
            {"ALARMOUT_OFF",HCEHomePublic.MINOR_ALARMOUT_OFF},
            {"ALWAYS_CLOSE_BEGIN",HCEHomePublic.MINOR_ALWAYS_CLOSE_BEGIN},
            {"ALWAYS_CLOSE_END",HCEHomePublic.MINOR_ALWAYS_CLOSE_END},
            {"MULTI_VERIFY_NEED_REMOTE_OPEN",HCEHomePublic.MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN},
            {"MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS",HCEHomePublic.MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS},
            {"MULTI_VERIFY_REPEAT_VERIFY",HCEHomePublic.MINOR_MULTI_VERIFY_REPEAT_VERIFY},
            {"MULTI_VERIFY_TIMEOUT",HCEHomePublic.MINOR_MULTI_VERIFY_TIMEOUT},
            {"DOORBELL_RINGING",HCEHomePublic.MINOR_DOORBELL_RINGING},
            {"FINGERPRINT_COMPARE_PASS",HCEHomePublic.MINOR_FINGERPRINT_COMPARE_PASS},
            {"FINGERPRINT_COMPARE_FAIL",HCEHomePublic.MINOR_FINGERPRINT_COMPARE_FAIL},
            {"CARD_FINGERPRINT_VERIFY_PASS",HCEHomePublic.MINOR_CARD_FINGERPRINT_VERIFY_PASS},
            {"CARD_FINGERPRINT_VERIFY_FAIL",HCEHomePublic.MINOR_CARD_FINGERPRINT_VERIFY_FAIL},
            {"CARD_FINGERPRINT_VERIFY_TIMEOUT",HCEHomePublic.MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT},
            {"CARD_FINGERPRINT_PASSWD_VERIFY_PASS",HCEHomePublic.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS},
            {"CARD_FINGERPRINT_PASSWD_VERIFY_FAIL",HCEHomePublic.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL},
            {"CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT",HCEHomePublic.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT},
            {"FINGERPRINT_PASSWD_VERIFY_PASS",HCEHomePublic.MINOR_FINGERPRINT_PASSWD_VERIFY_PASS},
            {"FINGERPRINT_PASSWD_VERIFY_FAIL",HCEHomePublic.MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL},
            {"FINGERPRINT_PASSWD_VERIFY_TIMEOUT",HCEHomePublic.MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT},
            {"FINGERPRINT_INEXISTENCE",HCEHomePublic.MINOR_FINGERPRINT_INEXISTENCE},
            {"CARD_PLATFORM_VERIFY",HCEHomePublic.MINOR_CARD_PLATFORM_VERIFY},
            {"MAC_DETECT",HCEHomePublic.MINOR_MAC_DETECT},
            {"LEGAL_MESSAGE",HCEHomePublic.MINOR_LEGAL_MESSAGE},
            {"ILLEGAL_MESSAGE",HCEHomePublic.MINOR_ILLEGAL_MESSAGE},
            {"DOOR_OPEN_OR_DORMANT_FAIL",HCEHomePublic.MINOR_DOOR_OPEN_OR_DORMANT_FAIL},
            {"AUTH_PLAN_DORMANT_FAIL",HCEHomePublic.MINOR_AUTH_PLAN_DORMANT_FAIL},
            {"CARD_ENCRYPT_VERIFY_FAIL",HCEHomePublic.MINOR_CARD_ENCRYPT_VERIFY_FAIL},
            {"SUBMARINEBACK_REPLY_FAIL",HCEHomePublic.MINOR_SUBMARINEBACK_REPLY_FAIL},
            {"DOOR_OPEN_OR_DORMANT_OPEN_FAIL",HCEHomePublic.MINOR_DOOR_OPEN_OR_DORMANT_OPEN_FAIL},
            {"DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL",HCEHomePublic.MINOR_DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL},
            {"TRAILING",HCEHomePublic.MINOR_TRAILING},
            {"REVERSE_ACCESS",HCEHomePublic.MINOR_REVERSE_ACCESS},
            {"FORCE_ACCESS",HCEHomePublic.MINOR_FORCE_ACCESS},
            {"CLIMBING_OVER_GATE",HCEHomePublic.MINOR_CLIMBING_OVER_GATE},
            {"PASSING_TIMEOUT",HCEHomePublic.MINOR_PASSING_TIMEOUT},
            {"INTRUSION_ALARM",HCEHomePublic.MINOR_INTRUSION_ALARM},
            {"FREE_GATE_PASS_NOT_AUTH",HCEHomePublic.MINOR_FREE_GATE_PASS_NOT_AUTH},
            {"DROP_ARM_BLOCK",HCEHomePublic.MINOR_DROP_ARM_BLOCK},
            {"DROP_ARM_BLOCK_RESUME",HCEHomePublic.MINOR_DROP_ARM_BLOCK_RESUME},
            {"LOCAL_FACE_MODELING_FAIL",HCEHomePublic.MINOR_LOCAL_FACE_MODELING_FAIL},
            {"STAY_EVENT",HCEHomePublic.MINOR_STAY_EVENT},
            {"PASSWORD_MISMATCH",HCEHomePublic.MINOR_PASSWORD_MISMATCH},
            {"EMPLOYEE_NO_NOT_EXIST",HCEHomePublic.MINOR_EMPLOYEE_NO_NOT_EXIST},
            {"COMBINED_VERIFY_PASS",HCEHomePublic.MINOR_COMBINED_VERIFY_PASS},
            {"COMBINED_VERIFY_TIMEOUT",HCEHomePublic.MINOR_COMBINED_VERIFY_TIMEOUT},
            {"VERIFY_MODE_MISMATCH",HCEHomePublic.MINOR_VERIFY_MODE_MISMATCH}
        };

        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];
        IntPtr ptrCfg = IntPtr.Zero;
        String m_strInputXml = String.Empty;
        String strTemp = String.Empty;
        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();

        public String getStdTime(int year, int month, int day, int hour, int min, int sec)
        {
            DateTime dt = new DateTime(year, month, day, hour, min, sec);
            return String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dt);　//2017-04-01T13:16:32.108
        }

        //Search Alarm Info according to Date Time
        private void btnSearch_Click(object sender, EventArgs e)
        {
            String strTemp = "/ISAPI/AccessControl/AcsEvent?format=json";

            String majorType = comboBoxMainType.SelectedItem.ToString();
            majorType = ((MajorTypeDictionary.ContainsKey(majorType)) ? MajorTypeDictionary[majorType] : 0).ToString();
            String minorType = comboBoxSecondType.SelectedItem.ToString();
            minorType = ((MinorTypeDictionary.ContainsKey(minorType)) ? MinorTypeDictionary[minorType] : 0).ToString();

            int startYear = dateTimeStart.Value.Year;
            int startMonth = dateTimeStart.Value.Month;
            int startDay = dateTimeStart.Value.Day;
            int startHour = dateTimeStart.Value.Hour;
            int startMinute = dateTimeStart.Value.Minute;
            int startSecond = dateTimeStart.Value.Second;
            String startTime = getStdTime(startYear, startMonth, startDay, startHour, startMinute, startSecond) + "+08:00";
            int endYear = dateTimeEnd.Value.Year;
            int endMonth = dateTimeEnd.Value.Month;
            int endDay = dateTimeEnd.Value.Day;
            int endHour = dateTimeEnd.Value.Hour;
            int endMinute = dateTimeEnd.Value.Minute;
            int endSecond = dateTimeEnd.Value.Second;
            String endTime = getStdTime(endYear, endMonth, endDay, endHour, endMinute, endSecond) + "+08:00";

            String searchID = "1";
            String pos = "0";
            String maxResults = "30";

            listViewEvent.Items.Clear();
            int m_lLogNum = 0;

            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

            m_szUrl = Encoding.UTF8.GetBytes(strTemp);
            struPTXML.pRequestUrl = Marshal.AllocHGlobal(300);
            Marshal.Copy(m_szUrl, 0, struPTXML.pRequestUrl, m_szUrl.Length);
            struPTXML.dwRequestUrlLen = (uint)m_szUrl.Length;
            struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
            struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
            struPTXML.byRes = new byte[32];

            while (true)
            {
                m_strInputXml = "{\"AcsEventCond\":{\"searchID\":\"" + searchID +
                    "\",\"searchResultPosition\":" + pos +
                ",\"maxResults\":" + maxResults +
                ",\"major\":" + majorType +
                ",\"minor\":" + minorType +
                ",\"startTime\":\"" + startTime +
                "\",\"endTime\":\"" + endTime +
                "\"}}";

                strTemp = m_strInputXml;
                if ("" == strTemp)
                {
                    struPTXML.pInBuffer = IntPtr.Zero;
                    struPTXML.dwInSize = 0;
                }
                else
                {
                    m_szInputBuffer = Encoding.UTF8.GetBytes(strTemp);
                    for (int i = 0; i < 1024 * 10; i++)
                    {
                        Marshal.WriteByte(struPTXML.pInBuffer, i, 0);
                    }
                    Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                    struPTXML.dwInSize = (uint)strTemp.Length;
                }

                for (int i = 0; i < 1024 * 10; i++)
                {
                    Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                }

                struPTXML.dwOutSize = (uint)(1024 * 10);

                IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                Marshal.StructureToPtr(struPTXML, ptrCfg, true);

                if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
                {
                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
                string m_strOutputXml = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);

          
                RootObject rb = JsonConvert.DeserializeObject<RootObject>(m_strOutputXml);

                if (rb.AcsEvent == null)
                {
                    MessageBox.Show("AcsEvent is NULL pos = " + pos);
                    break;
                }

                MessageBox.Show("totalMatchs = " + rb.AcsEvent.responseStatusStrg);
                String responseTemp = rb.AcsEvent.responseStatusStrg;
                if (responseTemp.Equals("NO MATCH"))
                {
                    break;
                }

                foreach (InfoList info in rb.AcsEvent.InfoList)
                {
                    listViewEvent.BeginUpdate();
                    ListViewItem item = new ListViewItem();
                    item.Text = (++m_lLogNum).ToString();
                    item.SubItems.Add(info.major);
                    item.SubItems.Add(info.minor);
                    item.SubItems.Add(info.time);
                    item.SubItems.Add(info.netUser);
                    item.SubItems.Add(info.remoteHostAddr);
                    item.SubItems.Add(info.cardNo);
                    item.SubItems.Add(info.cardType);
                    item.SubItems.Add(info.whiteListNo);
                    item.SubItems.Add(info.reportChannel);
                    item.SubItems.Add(info.cardReaderKind);
                    item.SubItems.Add(info.cardReaderNo);
                    item.SubItems.Add(info.doorNo);
                    item.SubItems.Add(info.verifyNo);
                    item.SubItems.Add(info.alarmInNo);
                    item.SubItems.Add(info.alarmOutNo);
                    item.SubItems.Add(info.caseSensorNo);
                    item.SubItems.Add(info.RS485No);
                    item.SubItems.Add(info.multiCardGroupNo);
                    item.SubItems.Add(info.accessChannel);
                    item.SubItems.Add(info.deviceNo);
                    item.SubItems.Add(info.distractControlNo);
                    item.SubItems.Add(info.employeeNoString);
                    item.SubItems.Add(info.localControllerID);
                    item.SubItems.Add(info.InternetAccess);
                    item.SubItems.Add(info.type);
                    item.SubItems.Add(info.MACAddr);
                    item.SubItems.Add(info.swipeCardType);
                    item.SubItems.Add(info.serialNo);
                    item.SubItems.Add(info.attendanceStatus);
                    item.SubItems.Add(info.statusValue);
                    listViewEvent.Items.Add(item);
                    listViewEvent.EndUpdate();
                }
                Marshal.FreeHGlobal(ptrCfg);

                int posTmp = 0;
                int.TryParse(pos, out posTmp);
                int numOfMatches = 0;
                int.TryParse(rb.AcsEvent.numOfMatches, out numOfMatches);
                posTmp += numOfMatches;
                pos = posTmp.ToString();

                if (rb.AcsEvent.responseStatusStrg.Equals("OK"))
                {
                    break;
                }
            }

            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            return;
        }
    }
}
