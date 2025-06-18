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

using TINYXMLTRANS;

using ISUPDemo.Public;
using System.IO;
using System.Xml;

namespace ISUPDemo
{
    public partial class CommonConfigure : Form
    {
        public enum CMD_TYPE
        {
            MOVE_DETECTION=1,
            Alarm=2,
            ACS=3,
        };

        const string m_getPatternString = "<Params>\r\n<ConfigCmd>{0}</ConfigCmd>\r\n"
            + "<ConfigParam1>{1}</ConfigParam1>\r\n"
            + "<ConfigParam2>{2}</ConfigParam2>\r\n"
            + "<ConfigParam3>{3}</ConfigParam3>\r\n"
            + "<ConfigParam4>{4}</ConfigParam4>\r\n"
            +"</Params>\r\n";

        const string m_setPatternString = "<Params>\r\n<ConfigCmd>{0}</ConfigCmd>\r\n"
            + "<ConfigParam1>{1}</ConfigParam1>\r\n"
            + "<ConfigParam2>{2}</ConfigParam2>\r\n"
            + "<ConfigParam3>{3}</ConfigParam3>\r\n"
            + "<ConfigParam4>{4}</ConfigParam4>\r\n"
            + "<ConfigXML>\r\n{5}</ConfigXML>\r\n"
            + "</Params>\r\n";

        public CMD_TYPE m_enumType;
        private int m_iDeviceIndex;
        private int m_lUserID;
        String m_csCmd;
        String m_csDetail;
        String m_strInputXml;
        String m_strOutputXml;
        private byte[] m_szCommand = new byte[1024];
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024*10];
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private DeviceLogList g_formList = DeviceLogList.Instance();
        private HCEHomeCMS.NET_EHOME_XML_CFG m_struCfg = new HCEHomeCMS.NET_EHOME_XML_CFG();
        private HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM m_struControlCfg = new HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM();
        public void InitCmdType()
        {
            comboBoxCommand.Items.Clear();
            comboBoxCommand.Items.Add("MOVE_DETECTION");
            comboBoxCommand.Items.Add("Alarm");
            comboBoxCommand.Items.Add("ACS");
        }
        public void SelectInputDetail()
        {
            //keep relax and normal,just a demo show function.

            m_strInputXml = "";
            m_strOutputXml = "";
            if (comboBoxOperation.Text.Equals("GetMotionPara"))
            {
                m_strInputXml = "<Params>\r\n<ConfigCmd>GetMotionPara</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetMotionArea"))
            {
                m_strInputXml="<Params>\r\n<ConfigCmd>GetMotionArea</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmDeploymentTime"))
            {
                m_strInputXml = "<Params>\r\n<ConfigCmd>GetAlarmDeploymentTime</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n<ConfigParam2>1</ConfigParam2>\r\n<ConfigParam3>1</ConfigParam3>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmTriggerRecord"))
            {
                m_strInputXml="<Params>\r\n<ConfigCmd>GetAlarmTriggerRecord</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n<ConfigParam2>1</ConfigParam2>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmLinkageType"))
            {
                m_strInputXml="<Params>\r\n<ConfigCmd>GetAlarmLinkageType</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n<ConfigParam2>1</ConfigParam2>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmTriggerCapture"))
            {
                m_strInputXml = "<Params>\r\n<ConfigCmd>GetAlarmTriggerCapture</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n<ConfigParam2>1</ConfigParam2>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmInPara"))
            {
                m_strInputXml = "<Params>\r\n<ConfigCmd>GetAlarmInPara</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n<ConfigParam2>1</ConfigParam2>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmOutPara"))
            {
              m_strInputXml="<Params>\r\n<ConfigCmd>GetAlarmOutPara</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("GetAlarmOutStatus"))
            {
                m_strInputXml = "<Params>\r\n<ConfigCmd>GetAlarmOutStatus</ConfigCmd>\r\n<ConfigParam1>1</ConfigParam1>\r\n</Params>\r\n";
            }
            else
            {
                m_strInputXml = m_getPatternString;
                m_csDetail = comboBoxOperation.Text;
                if (m_csDetail.Equals("GetCardWeekPlan")) //Get Card week plan status
                {
                    // ConfigParam1——WeekPlanNo（xs:integer from 1）ConfigParam2-ConfigParam4 invalid
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardWeekPlanCapabilities")) //Get capabilities of Card week plan
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardWeekPlanTime"))
                {
                    //Get Card week plan time
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "1", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardWeekPlanTimeCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardHolidayPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardHolidayPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardHolidayGroup"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardHolidayGroupCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardTemplate"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardTemplateCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusWeekPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusWeekPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusWeekPlanTime"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "1", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusWeekPlanTimeCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusHolidayPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusHolidayPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusHolidayGroup"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusHolidayGroupCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusTemplate"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorStatusTemplateCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyWeekPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyWeekPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyWeekPlanTime"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "1", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyWeekPlanTimeCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyHolidayPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyHolidayPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyHolidayGroup"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyHolidayGroupCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyTemplate"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyTemplateCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "123124", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardRightCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "123124", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardRightCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardPasswdOpenDoorCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "123124", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardPasswdOpenDoorCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetHostWorkStatus"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetHostWorkStatusCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardReaderWorkStatus"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardReaderWorkStatusCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorWorkStatus"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorWorkStatusCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorWorkStatusPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetDoorWorkStatusPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardReaderCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCardReaderCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyPlan"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetReaderVertifyPlanCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetGroupCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetGroupCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiCardCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiCardCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiCardGroupCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiCardGroupCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakReaderCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakReaderCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiDoorInterlockCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiDoorInterlockCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiDoorInterlockGroupCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetMultiDoorInterlockGroupCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakMultiHostCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakMultiHostCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakMultiHostReaderCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetAntiSneakMultiHostCardReaderCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCaseStatus"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCaseStatusCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventLinkCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventLinkCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventLinkOpenDoorCfg"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventLinkOpenDoorCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCaseSensorLinkAlarmOut"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetCaseSensorLinkAlarmOutCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventCardLinkBuzzer"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventCardLinkBuzzerCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventCardLinkDoor"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventCardLinkDoorCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventCardLinkAlarmout"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "1", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetEventCardLinkAlarmoutCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetFingerPrint"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "77", "1", "1", "0");
                }
                else if (m_csDetail.Equals("GetFingerPrintCapabilities"))
                {
                    m_strInputXml = "<Params>\r\n<ConfigCmd>GetFingerPrintCapabilities</ConfigCmd>\r\n</Params>\r\n";
                }
                else if (m_csDetail.Equals("GetFingerPrintCfgCapabilities"))
                {
                    m_strInputXml = "<Params>\r\n<ConfigCmd>GetFingerPrintCfgCapabilities</ConfigCmd>\r\n</Params>\r\n";
                }
                else if (m_csDetail.Equals("GetDelFingerPrintByCardNoCapabilities"))
                {
                    m_strInputXml = "<Params>\r\n<ConfigCmd>GetDelFingerPrintByCardNoCapabilities</ConfigCmd>\r\n</Params>\r\n";
                }
                else if (m_csDetail.Equals("GetDelFingerPrintByReaderIDCapabilities"))
                {
                    m_strInputXml = "<Params>\r\n<ConfigCmd>GetDelFingerPrintByReaderIDCapabilities</ConfigCmd>\r\n</Params>\r\n";
                }
                else if (m_csDetail.Equals("GetACSCfgCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
                else if (m_csDetail.Equals("GetACSFunctionCapabilities"))
                {
                    m_strInputXml = string.Format(m_strInputXml, m_csDetail, "0", "0", "0", "0");
                }
            }

            if (comboBoxOperation.Text.Equals("SetMotionPara"))
            {
                m_strInputXml = "<Params>\r\n"
                +"<ConfigCmd>SetMotionPara</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigXML>\r\n"
                +"<MOTIONCFG>\r\n"
                +"<IsUseMotion>0</IsUseMotion>\r\n"
                +"<Sensitive>0</Sensitive>\r\n"
                +"</MOTIONCFG>\r\n"
                + "</ConfigXML>\r\n"
                +"</Params>";
            }
            else if (comboBoxOperation.Text.Equals("SetMotionArea"))
            {
                m_strInputXml=@"<Params>\r\n"
                +"<ConfigCmd>SetMotionArea</ConfigCmd>\r\n"
                + "<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigXML>\r\n"
                +"<MOTIONAREACFG>\r\n"
                +"<Row>18</Row>\r\n"
                +"<BlockPerRow>22</BlockPerRow>\r\n"
                +"<PictureWidth>0</PictureWidth>\r\n"
                +"<PictureHeight>0</PictureHeight>\r\n"
                +"<AREAS>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"<Mask>0</Mask>\r\n"
                +"</AREAS>\r\n"
                +"</MOTIONAREACFG>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmDeploymentTime"))
            {
                m_strInputXml = "<Params>\r\n"
                + "<ConfigCmd>SetAlarmDeploymentTime</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigParam2>1</ConfigParam2>\r\n"
                +"<ConfigParam3>1</ConfigParam3>\r\n"
                +"<ConfigXML>\r\n"
                +"<ALARMTIME>\r\n"
                +"<TIME>\r\n"
                +"<StartTime>00:00</StartTime>\r\n"
                +"<StopTime>24:00</StopTime>\r\n"
                +"</TIME>\r\n"
                +"<TIME>\r\n"
                +"<StartTime>00:00</StartTime>\r\n"
                +"<StopTime>00:00</StopTime>\r\n"
                +"</TIME>\r\n"
                +"<TIME>\r\n"
                +"<StartTime>00:00</StartTime>\r\n"
                +"<StopTime>00:00</StopTime>\r\n"
                +"</TIME>\r\n"
                +"<TIME>\r\n"
                +"<StartTime>00:00</StartTime>\r\n"
                +"<StopTime>00:00</StopTime>\r\n"
                +"</TIME>\r\n"
                +"</ALARMTIME>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmTriggerRecord"))
            {
                m_strInputXml="<Params>\r\n"
                + "<ConfigCmd>SetAlarmTriggerRecord</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigParam2>1</ConfigParam2>\r\n"
                +"<ConfigXML>\r\n"
                +"<RECORDCHAN>\r\n"
                +"<AnalogChan1>1</AnalogChan1>\r\n"
                +"<AnalogChan2>1</AnalogChan2>\r\n"
                +"<DigitChan1>1</DigitChan1>\r\n"
                +"</RECORDCHAN>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmLinkageType"))
            {
                m_strInputXml="<Params>\r\n"
                + "<ConfigCmd>SetAlarmLinkageType</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigParam2>1</ConfigParam2>\r\n"
                +"<ConfigXML>\r\n"
                +"<LINKAGETYPE>\r\n"
                +"<MonitorAlarm>0</MonitorAlarm>\r\n"
                +"<SoundAlarm>0</SoundAlarm>\r\n"
                +"<Upload>0</Upload>\r\n"
                +"<Alarmout>0</Alarmout>\r\n"
                +"<Email>0</Email>\r\n"
                +"<ALARMOUT>\r\n"
                +"<AnalogAlarmOut1>0</AnalogAlarmOut1>\r\n"
                +"<AnalogAlarmOut2>0</AnalogAlarmOut2>\r\n"
                +"</ALARMOUT>\r\n"
                +"</LINKAGETYPE>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmTriggerCapture"))
            {
                m_strInputXml = "<Params>\r\n"
                + "<ConfigCmd>SetAlarmTriggerCapture</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigParam2>1</ConfigParam2>\r\n"
                +"<ConfigXML>\r\n"
                +"<CAPTURECONFIG>\r\n"
                +"<UseCapture>0</UseCapture>\r\n"
                +"<SendTo>1</SendTo>\r\n"
                +"<Interval>5</Interval>\r\n"
                +"<Resolution>1</Resolution>\r\n"
                +"<Quality>60</Quality>\r\n"
                +"<Frequency>1</Frequency>\r\n"
                +"<CAPTURECHAN>\r\n"
                +"<AnalogChan1>0</AnalogChan1>\r\n"
                +"<AnalogChan2>0</AnalogChan2>\r\n"
                +"<AnalogChan3>0</AnalogChan3>\r\n"
                +"<AnalogChan4>0</AnalogChan4>\r\n"
                +"</CAPTURECHAN>\r\n"
                +"</CAPTURECONFIG>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmInPara"))
            {
                m_strInputXml = "<Params>\r\n"
                + "<ConfigCmd>SetAlarmInPara</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigParam2>1</ConfigParam2>\r\n"
                +"<ConfigXML>\r\n"
                +"<ALARMININFO>\r\n"
                +"<AlarmInName>AlarmTest</AlarmInName>\r\n"
                +"<AlarmInType>0</AlarmInType>\r\n"
                +"<IsUseAlarmIn>1</IsUseAlarmIn>\r\n"
                +"<LINKAGETYPE>\r\n"
                +"<MonitorAlarm>1</MonitorAlarm>\r\n"
                +"<SoundAlarm>1</SoundAlarm>\r\n"
                +"<Upload>1</Upload>\r\n"
                +"<Alarmout>1</Alarmout>\r\n"
                +"<Email>1</Email>\r\n"
                +"<ALARMOUT>\r\n"
                +"<AnalogAlarmOut1>1</AnalogAlarmOut1>\r\n"
                +"<AnalogAlarmOut2>1</AnalogAlarmOut2>\r\n"
                +"</ALARMOUT>\r\n"
                +"<PTZLINKAGE>\r\n"
                +"<IsUsePreset>1</IsUsePreset>\r\n"
                +"<PresetNo>1</PresetNo>\r\n"
                +"<IsUseCurise>1</IsUseCurise>\r\n"
                +"<CuriseNo>1</CuriseNo>\r\n"
                +"<IsUseTrack>1</IsUseTrack>\r\n"
                +"<TrackNo>1</TrackNo>\r\n"
                +"</PTZLINKAGE>\r\n"
                +"</LINKAGETYPE>\r\n"
                +"<RECORDCHAN>\r\n"
                +"<AnalogChan1>1</AnalogChan1>\r\n"
                +"<AnalogChan2>1</AnalogChan2>\r\n"
                +"<DigitChan1>2</DigitChan1>\r\n"
                +"<DigitChan2>2</DigitChan2>\r\n"
                +"</RECORDCHAN>\r\n"
                +"</ALARMININFO>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmOutPara"))
            {
              m_strInputXml="<Params>\r\n"
                +"<ConfigCmd>SetAlarmOutPara</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                + "<ConfigXML>\r\n"
                +"<ALARMOUTINFO>\r\n"
                +"<AlarmOutName>AlarmTest</AlarmOutName>\r\n"
                +"<AlarmOutDelay>0</AlarmOutDelay>\r\n"
                +"</ALARMOUTINFO>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
            else if (comboBoxOperation.Text.Equals("SetAlarmOutStatus"))
            {
                m_strInputXml="<Params>\r\n"
                + "<ConfigCmd>SetAlarmOutStatus</ConfigCmd>\r\n"
                +"<ConfigParam1>1</ConfigParam1>\r\n"
                +"<ConfigXML>\r\n"
                +"<ALARMOUTSTATUSINFO>\r\n"
                +"<AlarmOutStatus>1</AlarmOutStatus>\r\n"
                +"</ALARMOUTSTATUSINFO>\r\n"
                +"</ConfigXML>\r\n"
                +"</Params>\r\n";
            }
             else 
            {
                m_csDetail = comboBoxOperation.Text;
                if (m_csDetail.Equals("SetReaderVertifyPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<ReaderVertifyPlan>\r\n"
                         + "<templateNo>1</templateNo>\r\n"
                         + "</ReaderVertifyPlan>\r\n");
                }
                else if (m_csDetail.Equals("SetCardUserInfo"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1234567890", "0", "0", "0", "<CardUserInfo>\r\n"
                         +"<userName>test</userName>\r\n"
                         +"</CardUserInfo>\r\n");
                }
                else if (m_csDetail.Equals("SetACSCfg"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1234567890",  "0", "0", "0", "0", "<AcsCfg>\r\n"
                        +"<rs485Backup>1</rs485Backup>\r\n"
                        +"<showCapPic>1</showCapPic>\r\n"
                        +"<showCardNo>1</showCardNo>\r\n"
                        +"<showUserInfo>1</showUserInfo>\r\n"
                        +"<overlayUserInfo>1</overlayUserInfo>\r\n"
                        +"<voicePrompt>1</voicePrompt>\r\n"
                        +"<uploadCapPic>1</uploadCapPic>\r\n"
                        +"<saveCapPic>1</saveCapPic>\r\n"
                        +"<inputCardNo>1</inputCardNo>\r\n"
                        + "</AcsCfg>\r\n");
                }
                else if (m_csDetail.Equals("DelFingerPrintByReaderID"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<FingerPrintByCardReader>\r\n"
                        +"<enable>1</enable>\r\n"
                        +"</FingerPrintByCardReader>\r\n");
                }
                else if (m_csDetail.Equals("DelFingerPrintByCardNo"))
                {
                    m_strInputXml = "<Params>\r\n<ConfigCmd>DelFingerPrintByCardNo</ConfigCmd>\r\n"
            + "<ConfigParam1>77</ConfigParam1>\r\n"
            + "</Params>\r\n"
            +"<FingerPrintByCard>\r\n"
                        +"<readerNo>1</readerNo>\r\n"
                        +"<fingerNo>2</fingerNo>\r\n"
                        +"</FingerPrintByCard>\r\n";
                }
                else if (m_csDetail.Equals("SetFingerPrintCfg"))
                {
                    string strFingerData = "";
                    try
                    {
                        string strpath = null;
                        strpath = string.Format("{0}\\fingerprint.dat", Environment.CurrentDirectory);

                        using (FileStream fs = new FileStream(strpath, FileMode.Open))
                        {
                            if (0 == fs.Length)
                            {
                                fs.Close();
                            }
                            int dwFingerPrintLen = (int)fs.Length;
                            byte[] byteFingerData = new byte[dwFingerPrintLen];
                            BinaryReader objBinaryReader = new BinaryReader(fs);

                            for (int i = 0; i < dwFingerPrintLen; i++)
                            {
                                byteFingerData[i] = objBinaryReader.ReadByte();
                            }
                            strFingerData = System.Text.Encoding.Default.GetString(byteFingerData);
                            fs.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("FingerData may be wrong", "Error", MessageBoxButtons.OK);
                    }
                    m_strInputXml = "<Params>\r\n<ConfigCmd>SetFingerPrintCfg</ConfigCmd>\r\n"
            + "<ConfigParam1>77</ConfigParam1>\r\n"
            + "<ConfigParam2>2</ConfigParam2>\r\n"
            + "</Params>\r\n"
            +"<FingerPrintCfg>\r\n"
                        +"<readerNo>1</readerNo>\r\n"
                        +"<fingerType>0</fingerType>\r\n"
                        +"<fingerData>" + strFingerData + "</fingerData>\r\n"
                        +"</FingerPrintCfg>\r\n";
                } 
                else if (m_csDetail.Equals("SetCardReaderCfg"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<CardReaderCfg>\r\n"
                        +"<enabled>1</enabled>\r\n"
                        // 读卡器类型，1-DS-K110XM/MK/C/CK，2-DS-K192AM/AMP，3-DS-K192BM/BMP，4-DS-K182AM/AMP，5-DS-K182BM/BMP, 6-DS-K182AMF/ACF，7-韦根或485不在线，
                        // 8- DS-K1101M/MK，9- DS-K1101C/CK，10- DS-K1102M/MK/M-A，11- DS-K1102C/CK，12- DS-K1103M/MK，13- DS-K1103C/CK，14- DS-K1104M/MK，15- DS-K1104C/CK，
                        // 16- DS-K1102S/SK/S-A，17- DS-K1102G/GK，18- DS-K1100S-B，19- DS-K1102EM/EMK，20- DS-K1102E/EK，21- DS-K1200EF，22- DS-K1200MF，23- DS-K1200CF，
                        // 24- DS-K1300EF，25- DS-K1300MF，26- DS-K1300CF，27- DS-K1105E，28- DS-K1105M，29- DS-K1105C，30- DS-K182AMF，31- DS-K196AMF，32-DS-K194AMP
                        +"<cardReaderType>1</cardReaderType>\r\n"
                        +"<okLedPolarity>1</okLedPolarity>\r\n"
                        +"<errorLedPolarity>1</errorLedPolarity>\r\n"
                        +"<buzzerPolarity>1</buzzerPolarity>\r\n"
                        +"<swipeInterval>10</swipeInterval>\r\n"
                        +"<pressTimeout>5</pressTimeout>\r\n"
                        +"<failAlarmEnabled>1</failAlarmEnabled>\r\n"
                        +"<maxReadCardFailNum>10</maxReadCardFailNum>\r\n"
                        +"<tamperCheckEnabled>1</tamperCheckEnabled>\r\n"
                        +"<offlineCheckTime>30</offlineCheckTime>\r\n"
                        // 指纹识别等级，1 - 1 / 10误认率，2 - 1 / 100误认率，3 - 1 / 1000误认率，4 - 1 / 10000误认率，5 - 1 / 100000误认率，6 - 1 / 1000000误认率，
                        // 7 - 1 / 10000000误认率，8 - 1 / 100000000误认率，9 - 3 / 100误认率，10 - 3 / 1000误认率，11 - 3 / 10000误认率，12 - 3 / 100000误认率，
                        // 13 - 3 / 1000000误认率，14 - 3 / 10000000误认率，15 - 3 / 100000000误认率，16 - Automatic Normal, 17 - Automatic Secure, 18 - Automatic More Secure
                        +"<fingerPrintCheckLevel>6</fingerPrintCheckLevel>\r\n"
                        +"<buzzerTime>10</buzzerTime>\r\n"
                        +"</CardReaderCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetDoorWorkStatusPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<DoorStatusPlan>\r\n"
                        +"<templateNo>1</templateNo>\r\n"
                        +"</DoorStatusPlan>\r\n");
                }
                else if (m_csDetail.Equals("SetDoorCfg"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<DoorCfg>\r\n"
                        + "<doorName>JustTest</doorName>\r\n"
                        + "<magneticType>0</magneticType>\r\n"
                        + "<openButtonType>0</openButtonType>\r\n"
                        + "<openDuration>5</openDuration>\r\n"
                        + "<disabledOpenDuration>20</disabledOpenDuration>\r\n"
                        + "<magneticAlarmTimeout>30</magneticAlarmTimeout>\r\n"
                        + "<doorLockEnabled>1</doorLockEnabled>\r\n"
                        + "<leaderCardEnabled>1</leaderCardEnabled>\r\n"
                        + "<leaderCardMode>1</leaderCardMode>\r\n"
                        + "<leaderCardOpenDuration>60</leaderCardOpenDuration>\r\n"
                        + "<stressPassword>MTIzNDU=</stressPassword>\r\n"
                        + "<superPassword>MTIzNDU=</superPassword>\r\n"
                        + "<unlockPassword>MTIzNDU=</unlockPassword>\r\n"
                        + "</DoorCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetCardPasswdOpenDoorCfg"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "123124", "0", "0", "0", "<CardPasswdOpenDoorCfg>\r\n"
                        + "<valid>1</valid>\r\n"
                        + "<password>MTIzNDU=</password>\r\n"
                        + "</CardPasswdOpenDoorCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetCardRightCfg"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "123124", "0", "0", "0", "<CardRight>\r\n"
                        + "<DoorRightList>\r\n"
                        + "<DoorRight>\r\n"
                        + "<doorNo>1</doorNo>\r\n"
                        + "<planNo>1,2</planNo>\r\n"
                        + "</DoorRight>\r\n"
                        + "</DoorRightList>\r\n"
                        + "</CardRight>\r\n");
                }
                else if (m_csDetail.Equals("SetCardCfg"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "123124", "1", "0", "0", "<CardCfg>\r\n"
                        // xs:boolean 置为0表示删除此卡
                         + "<valid>1</valid>\r\n"
                        // xs:integer,卡类型，1-普通卡，2-残疾人卡，3-黑名单卡，4-巡更卡，5-胁迫卡，6-超级卡，7-来宾卡 8-解除卡
                         + "<cardType>1</cardType>\r\n"
                         + "<employeeNo>2</employeeNo>\r\n"
                         + "<validTimeEnabled>1</validTimeEnabled>\r\n"
                         + "<ValidTime>\r\n"
                         + "<startTime>2017-01-01T00:00:00</startTime>\r\n"
                         + "<endTime>2018-01-01T00:00:00</endTime>\r\n"
                         + "</ValidTime>\r\n"
                         + "<password>MTIzNDU=</password>\r\n"
                         + "<maxSwipeTime>0</maxSwipeTime>\r\n"
                         + "<swipeTime>10</swipeTime>\r\n"
                         + "<groupNo>1</groupNo>\r\n"
                         + "<userType>1</userType>\r\n"
                         + "<verifyMode>22</verifyMode>\r\n"
                         + "</CardCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetReaderVertifyTemplate"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<Template>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<name>JustTest</name>\r\n"
                         + "<weekPlanNo>1</weekPlanNo>\r\n"
                         // 假日组，最多16个
                         + "<HGNo>1,2</HGNo>\r\n"
                         + "</Template>\r\n");
                }
                else if (m_csDetail.Equals("SetReaderVertifyHolidayGroup"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<HolidayGroup>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<groupName>JustTest</groupName>\r\n"
                        // 假日列表，最多16个
                         + "<holidayNo>1,2</holidayNo>\r\n"
                         + "</HolidayGroup>\r\n");
                }
                else if (m_csDetail.Equals("SetReaderVertifyHolidayPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<HolidayPlan>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<startDate>2017-01-01T00:00:00</startDate>\r\n"
                         + "<endDate>2017-01-07T00:00:00</endDate>\r\n"
                        // 时间段，最多8个
                         + "<TimeList>\r\n"
                         + "<Time>\r\n"
                         + "<startTime>2017-01-01T08:00:00</startTime>\r\n"
                         + "<endTime>2017-01-01T12:00:00</endTime>\r\n"
                        // 该时间段下读卡器验证方式 0-无效，1-休眠，2-刷卡+密码，3-刷卡,4-刷卡或密码,5-指纹，6-指纹+密码，7-指纹或刷卡，8-指纹+刷卡，9-指纹+刷卡+密码（无先后顺序）
                         + "<verifyMode>2</verifyMode>>\r\n"
                         + "</Time>\r\n"
                         + "</TimeList>\r\n"
                         + "</HolidayPlan>\r\n");
                }
                else if (m_csDetail.Equals("SetReaderVertifyWeekPlanTime"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "1", "0", "0", "<WeekPlanTimeList>\r\n"
                         + "<WeekPlanTime>\r\n"
                         + "<startTime>2017-01-01T08:00:00</startTime>\r\n"
                         + "<endTime>2017-01-01T09:30:00</endTime>\r\n"
                        // 该时间段下读卡器验证方式 0-无效，1-休眠，2-刷卡+密码，3-刷卡,4-刷卡或密码,5-指纹，6-指纹+密码，7-指纹或刷卡，8-指纹+刷卡，9-指纹+刷卡+密码（无先后顺序）
                         + "<verifyMode>2</verifyMode>>\r\n"
                         + "</WeekPlanTime>\r\n"
                         + "</WeekPlanTimeList>\r\n");
                }
                else if (m_csDetail.Equals("SetReaderVertifyWeekPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "0", "0", "0", "<WeekPlanCfg>\r\n<enabled>1</enabled>\r\n</WeekPlanCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetDoorStatusTemplate"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "0", "0", "0", "<Template>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<name>JustTest</name>\r\n"
                         + "<weekPlanNo>1</weekPlanNo>\r\n"
                        // 假日组，最多16个
                         + "<HGNo>1,2</HGNo>\r\n"
                         + "</Template>\r\n");
                }
                 else if (m_csDetail.Equals("SetDoorStatusHolidayGroup"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "0", "0", "0", "<HolidayGroup>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<groupName>JustTest</groupName>\r\n"
                        // 假日列表，最多16个
                         + "<holidayNo>1,2</holidayNo>\r\n"
                         + "</HolidayGroup>\r\n");
                }
                 else if (m_csDetail.Equals("SetDoorStatusHolidayPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "0", "0", "0", "<HolidayPlan>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<startDate>2017-01-01T00:00:00</startDate>\r\n"
                         + "<endDate>2017-01-07T00:00:00</endDate>\r\n"
                        // 时间段，最多8个
                         + "<TimeList>\r\n"
                         + "<Time>\r\n"
                         + "<startTime>2017-01-01T08:00:00</startTime>\r\n"
                         + "<endTime>2017-01-01T12:00:00</endTime>\r\n"
                        // 该时间段下门状态，0-无效，1-休眠，2-常开状态，3-常闭状态
                         + "<doorStatus>2</doorStatus>\r\n"
                         + "</Time>\r\n"
                         + "</TimeList>\r\n"
                         + "</HolidayPlan>\r\n");
                }
                 else if (m_csDetail.Equals("SetDoorStatusWeekPlanTime"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "1", "0", "0", "<WeekPlanTimeList>\r\n"
                         + "<WeekPlanTime>\r\n"
                         + "<startTime>2017-01-01T08:00:00</startTime>\r\n"
                         + "<endTime>2017-01-01T09:30:00</endTime>\r\n"
                        // 该时间段下门状态，0-无效，1-休眠，2-常开状态，3-常闭状态
                         + "<doorStatus>2</doorStatus>\r\n"
                         + "</WeekPlanTime>\r\n"
                         + "</WeekPlanTimeList>\r\n");
                }
                 else if (m_csDetail.Equals("SetDoorStatusWeekPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail,"1", "0", "0", "0", "<WeekPlanCfg>\r\n<enabled>1</enabled>\r\n</WeekPlanCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetCardTemplate"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<Template>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<name>JustTest</name>\r\n"
                         + "<weekPlanNo>1</weekPlanNo>\r\n"
                        // 假日组，最多16个
                         + "<HGNo>1,2</HGNo>\r\n"
                         + "</Template>\r\n");
                }
                else if (m_csDetail.Equals("SetCardHolidayGroup"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<HolidayGroup>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<groupName>JustTest</groupName>\r\n"
                        // 假日列表，最多16个
                         + "<holidayNo>1,2</holidayNo>\r\n"
                         + "</HolidayGroup>\r\n");
                }
                else if (m_csDetail.Equals("SetCardHolidayPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<HolidayPlan>\r\n"
                         + "<enabled>1</enabled>\r\n"
                         + "<startDate>2017-01-01T00:00:00</startDate>\r\n"
                         + "<endDate>2017-01-07T00:00:00</endDate>\r\n"
                        // 时间段，最多8个
                         + "<TimeList>\r\n"
                         + "<Time>\r\n"
                         + "<startTime>2017-01-01T08:00:00</startTime>\r\n"
                         + "<endTime>2017-01-01T12:00:00</endTime>\r\n"
                         + "</Time>\r\n"
                         + "</TimeList>\r\n"
                         + "</HolidayPlan>\r\n");
                }
                else if (m_csDetail.Equals("SetCardWeekPlanTime"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "1", "0", "0", "<WeekPlanTimeList>\r\n"
                         + "<WeekPlanTime>\r\n"
                         + "<startTime>2017-01-01T08:00:00</startTime>\r\n"
                         + "<endTime>2017-01-01T09:30:00</endTime>\r\n"
                         + "</WeekPlanTime>\r\n"
                         + "</WeekPlanTimeList>\r\n");
                }
                 else if (m_csDetail.Equals("SetCardWeekPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<WeekPlanCfg>\r\n<enabled>1</enabled>\r\n</WeekPlanCfg>\r\n");
                }
                else if (m_csDetail.Equals("SetCardWeekPlan"))
                {
                    m_strInputXml = string.Format(m_setPatternString, m_csDetail, "1", "0", "0", "0", "<WeekPlanCfg>\r\n<enabled>1</enabled>\r\n</WeekPlanCfg>\r\n");
                }
                else if (m_csDetail.Equals("GATECONTROL"))
                {
                    m_strInputXml = "<?xml version=\"1.0\" encoding=\"GB2312\"?>\r\n"
                         + "<PPVSPMessage>\r\n"
                         + "<Version>2.5</Version>\r\n"
                         + "<Sequence>1</Sequence>\r\n"
                         + "<Method>CONTROL</Method>\r\n"
                         + "<CommandType>REQUEST</CommandType>\r\n"
                         + "<Command>GATECONTROL</Command>\r\n"
                         + "<Params>\r\n"
                         + "<DoorNo>1</DoorNo>\r\n"
                         + "<Cmd>OPEN</Cmd>\r\n"
                         + "</Params>\r\n"
                         + "</PPVSPMessage>";
                }
                else if (m_csDetail.Equals("CLEARACSPARAM"))
                {
                    m_strInputXml = "<?xml version=\"1.0\" encoding=\"GB2312\"?>\r\n"
                     + "<PPVSPMessage>\r\n"
                     + "<Version>2.5</Version>\r\n"
                     + "<Sequence>1</Sequence>\r\n"
                     + "<Method>CONTROL</Method>\r\n"
                     + "<CommandType>REQUEST</CommandType>\r\n"
                     + "<Command>CLEARACSPARAM</Command>\r\n"
                     + "<Params>\r\n"
                     + "<paramType>16</paramType>\r\n"
                     + "</Params>\r\n"
                     + "</PPVSPMessage>";
                }

            }
            richTextBoxInput.Text = m_strInputXml;
        }
        public void InitGetParam(CMD_TYPE enumType)
        {
            comboBoxOperation.Items.Clear();
            switch (enumType)
            {
                case CMD_TYPE.MOVE_DETECTION:
                    comboBoxOperation.Items.Add("GetMotionPara");
                    comboBoxOperation.Items.Add("GetMotionArea"); //移动侦测布防
                    comboBoxOperation.Items.Add("GetAlarmDeploymentTime");//布防时间
                    comboBoxOperation.Items.Add("GetAlarmTriggerRecord"); //通道录像参数
                    comboBoxOperation.Items.Add("GetAlarmLinkageType"); //联动参数
                    comboBoxOperation.Items.Add("GetAlarmTriggerCapture");//抓拍参数
                    comboBoxOperation.SelectedIndex=0;
                    SelectInputDetail();
                    break;
                case CMD_TYPE.Alarm:
                    comboBoxOperation.Items.Clear();
                    comboBoxOperation.Items.Add("GetAlarmLinkageType");
                    comboBoxOperation.Items.Add("GetAlarmTriggerRecord");
                    comboBoxOperation.Items.Add("GetAlarmTriggerCapture");
                    comboBoxOperation.Items.Add("GetAlarmInPara");
                    comboBoxOperation.Items.Add("GetAlarmOutPara");
                    comboBoxOperation.Items.Add("GetAlarmDeploymentTime");
                    comboBoxOperation.Items.Add("GetAlarmOutStatus");
                    comboBoxOperation.SelectedIndex = 0;
                    SelectInputDetail();
                    break;
                case CMD_TYPE.ACS:
                    comboBoxOperation.Items.Clear();
                    comboBoxOperation.Items.Add("GetCardWeekPlan");
                    comboBoxOperation.Items.Add("GetCardWeekPlanCapabilities");
                    comboBoxOperation.Items.Add("GetCardWeekPlanTime");
                    comboBoxOperation.Items.Add("GetCardWeekPlanTimeCapabilities");
                    comboBoxOperation.Items.Add("GetCardHolidayPlan");
                    comboBoxOperation.Items.Add("GetCardHolidayPlanCapabilities");
                    comboBoxOperation.Items.Add("GetCardHolidayGroup");
                    comboBoxOperation.Items.Add("GetCardHolidayGroupCapabilities");
                    comboBoxOperation.Items.Add("GetCardTemplate");
                    comboBoxOperation.Items.Add("GetCardTemplateCapabilities");
                    comboBoxOperation.Items.Add("GetDoorStatusWeekPlan");
                    comboBoxOperation.Items.Add("GetDoorStatusWeekPlanCapabilities");
                    comboBoxOperation.Items.Add("GetDoorStatusWeekPlanTime");
                    comboBoxOperation.Items.Add("GetDoorStatusWeekPlanTimeCapabilities");
                    comboBoxOperation.Items.Add("GetDoorStatusHolidayPlan");
                    comboBoxOperation.Items.Add("GetDoorStatusHolidayPlanCapabilities");
                    comboBoxOperation.Items.Add("GetDoorStatusHolidayGroup");
                    comboBoxOperation.Items.Add("GetDoorStatusHolidayGroupCapabilities");
                    comboBoxOperation.Items.Add("GetDoorStatusTemplate");
                    comboBoxOperation.Items.Add("GetDoorStatusTemplateCapabilities");
                    comboBoxOperation.Items.Add("GetReaderVertifyWeekPlan");
                    comboBoxOperation.Items.Add("GetReaderVertifyWeekPlanCapabilities");
                    comboBoxOperation.Items.Add("GetReaderVertifyWeekPlanTime");
                    comboBoxOperation.Items.Add("GetReaderVertifyWeekPlanTimeCapabilities");
                    comboBoxOperation.Items.Add("GetReaderVertifyHolidayPlan");
                    comboBoxOperation.Items.Add("GetReaderVertifyHolidayPlanCapabilities");
                    comboBoxOperation.Items.Add("GetReaderVertifyHolidayGroup");
                    comboBoxOperation.Items.Add("GetReaderVertifyHolidayGroupCapabilities");
                    comboBoxOperation.Items.Add("GetReaderVertifyTemplate");
                    comboBoxOperation.Items.Add("GetReaderVertifyTemplateCapabilities");
                    comboBoxOperation.Items.Add("GetCardCfg");
                    comboBoxOperation.Items.Add("GetCardCfgCapabilities");
                    comboBoxOperation.Items.Add("GetCardRightCfg");
                    comboBoxOperation.Items.Add("GetCardRightCfgCapabilities");
                    comboBoxOperation.Items.Add("GetCardPasswdOpenDoorCfg");
                    comboBoxOperation.Items.Add("GetCardPasswdOpenDoorCfgCapabilities");
                    comboBoxOperation.Items.Add("GetHostWorkStatus");
                    comboBoxOperation.Items.Add("GetHostWorkStatusCapabilities");
                    comboBoxOperation.Items.Add("GetCardReaderWorkStatus");
                    comboBoxOperation.Items.Add("GetCardReaderWorkStatusCapabilities");
                    comboBoxOperation.Items.Add("GetDoorCfg");
                    comboBoxOperation.Items.Add("GetDoorCfgCapabilities");
                    comboBoxOperation.Items.Add("GetDoorWorkStatus");
                    comboBoxOperation.Items.Add("GetDoorWorkStatusCapabilities");
                    comboBoxOperation.Items.Add("GetDoorWorkStatusPlan");
                    comboBoxOperation.Items.Add("GetDoorWorkStatusPlanCapabilities");
                    comboBoxOperation.Items.Add("GetCardReaderCfg");
                    comboBoxOperation.Items.Add("GetCardReaderCfgCapabilities");
                    comboBoxOperation.Items.Add("GetReaderVertifyPlan");
                    comboBoxOperation.Items.Add("GetReaderVertifyPlanCapabilities");
                    comboBoxOperation.Items.Add("GetGroupCfg");
                    comboBoxOperation.Items.Add("GetGroupCfgCapabilities");
                    comboBoxOperation.Items.Add("GetMultiCardCfg");
                    comboBoxOperation.Items.Add("GetMultiCardCfgCapabilities");
                    comboBoxOperation.Items.Add("GetMultiCardGroupCfg");
                    comboBoxOperation.Items.Add("GetMultiCardGroupCfgCapabilities");
                    comboBoxOperation.Items.Add("GetAntiSneakCfg");
                    comboBoxOperation.Items.Add("GetAntiSneakCfgCapabilities");
                    comboBoxOperation.Items.Add("GetAntiSneakReaderCfg");
                    comboBoxOperation.Items.Add("GetAntiSneakReaderCfgCapabilities");
                    comboBoxOperation.Items.Add("GetMultiDoorInterlockCfg");
                    comboBoxOperation.Items.Add("GetMultiDoorInterlockCfgCapabilities");
                    comboBoxOperation.Items.Add("GetMultiDoorInterlockGroupCfg");
                    comboBoxOperation.Items.Add("GetMultiDoorInterlockGroupCfgCapabilities");
                    comboBoxOperation.Items.Add("GetAntiSneakMultiHostCfg");
                    comboBoxOperation.Items.Add("GetAntiSneakMultiHostCfgCapabilities");
                    comboBoxOperation.Items.Add("GetAntiSneakMultiHostReaderCfg");
                    comboBoxOperation.Items.Add("GetAntiSneakMultiHostCardReaderCfgCapabilities");
                    comboBoxOperation.Items.Add("GetCaseStatus");
                    comboBoxOperation.Items.Add("GetCaseStatusCapabilities");
                    comboBoxOperation.Items.Add("GetEventLinkCfg");
                    comboBoxOperation.Items.Add("GetEventLinkCfgCapabilities");
                    comboBoxOperation.Items.Add("GetEventLinkOpenDoorCfg");
                    comboBoxOperation.Items.Add("GetEventLinkOpenDoorCfgCapabilities");
                    comboBoxOperation.Items.Add("GetCaseSensorLinkAlarmOut");
                    comboBoxOperation.Items.Add("GetCaseSensorLinkAlarmOutCapabilities");
                    comboBoxOperation.Items.Add("GetEventCardLinkBuzzer");
                    comboBoxOperation.Items.Add("GetEventCardLinkBuzzerCapabilities");
                    comboBoxOperation.Items.Add("GetEventCardLinkDoor");
                    comboBoxOperation.Items.Add("GetEventCardLinkDoorCapabilities");
                    comboBoxOperation.Items.Add("GetEventCardLinkAlarmout");
                    comboBoxOperation.Items.Add("GetEventCardLinkAlarmoutCapabilities");
                    comboBoxOperation.Items.Add("GetFingerPrint");
                    comboBoxOperation.Items.Add("GetFingerPrintCapabilities");
                    comboBoxOperation.Items.Add("GetFingerPrintCfgCapabilities");
                    comboBoxOperation.Items.Add("GetDelFingerPrintByCardNoCapabilities");
                    comboBoxOperation.Items.Add("GetDelFingerPrintByReaderIDCapabilities");
                    comboBoxOperation.Items.Add("GetACSCfgCapabilities");
                    comboBoxOperation.Items.Add("GetACSFunctionCapabilities");
                    comboBoxOperation.SelectedIndex = 0;
                    SelectInputDetail();
                    break;
                default:
                    break;
            }
        }

         public void InitSetParam(CMD_TYPE enumType)
        {
            comboBoxOperation.Items.Clear();
            switch (enumType)
            {
                case CMD_TYPE.MOVE_DETECTION:
                    comboBoxOperation.Items.Add("SetMotionPara");
                    comboBoxOperation.Items.Add("SetMotionArea"); //移动侦测布防
                    comboBoxOperation.Items.Add("SetAlarmDeploymentTime");//布防时间
                    comboBoxOperation.Items.Add("SetAlarmTriggerRecord"); //通道录像参数
                    comboBoxOperation.Items.Add("SetAlarmLinkageType"); //联动参数
                    comboBoxOperation.Items.Add("SetAlarmTriggerCapture");//抓拍参数
                    comboBoxOperation.SelectedIndex=0;
                    SelectInputDetail();
                    break;
                case CMD_TYPE.Alarm:
                    comboBoxOperation.Items.Clear();
                    comboBoxOperation.Items.Add("SetAlarmLinkageType");
                    comboBoxOperation.Items.Add("SetAlarmTriggerRecord");
                    comboBoxOperation.Items.Add("SetAlarmTriggerCapture");
                    comboBoxOperation.Items.Add("SetAlarmInPara");
                    comboBoxOperation.Items.Add("SetAlarmOutPara");
                    comboBoxOperation.Items.Add("SetAlarmDeploymentTime");
                    comboBoxOperation.Items.Add("SetAlarmOutStatus");
                    comboBoxOperation.SelectedIndex = 0;
                    SelectInputDetail();
                    break;
                case CMD_TYPE.ACS:
                    comboBoxOperation.Items.Clear();
                    comboBoxOperation.Items.Add("SetReaderVertifyPlan");
                    comboBoxOperation.Items.Add("SetCardUserInfo");
                    comboBoxOperation.Items.Add("SetACSCfg");
                    comboBoxOperation.Items.Add("SetFingerPrintCfg");
                    comboBoxOperation.Items.Add("DelFingerPrintByReaderID");
                    comboBoxOperation.Items.Add("DelFingerPrintByCardNo");
                    comboBoxOperation.Items.Add("SetCardReaderCfg");

                    comboBoxOperation.Items.Add("SetDoorWorkStatusPlan");
                    comboBoxOperation.Items.Add("SetDoorCfg");
                    comboBoxOperation.Items.Add("SetCardRightCfg");
                    comboBoxOperation.Items.Add("SetCardCfg");
                    comboBoxOperation.Items.Add("SetReaderVertifyTemplate");
                    comboBoxOperation.Items.Add("SetReaderVertifyHolidayGroup");
                    comboBoxOperation.Items.Add("SetReaderVertifyHolidayPlan");

                    comboBoxOperation.Items.Add("SetReaderVertifyWeekPlanTime");
                    comboBoxOperation.Items.Add("SetReaderVertifyWeekPlan");
                    comboBoxOperation.Items.Add("SetDoorStatusTemplate");
                    comboBoxOperation.Items.Add("SetDoorStatusHolidayGroup");
                    comboBoxOperation.Items.Add("SetDoorStatusHolidayPlan");
                    comboBoxOperation.Items.Add("SetDoorStatusWeekPlanTime");
                    comboBoxOperation.Items.Add("SetDoorStatusWeekPlan");
                    comboBoxOperation.Items.Add("SetCardTemplate");
                    comboBoxOperation.Items.Add("SetCardHolidayGroup");
                    comboBoxOperation.Items.Add("SetCardHolidayPlan");
                    comboBoxOperation.Items.Add("SetCardWeekPlanTime");
                    comboBoxOperation.Items.Add("SetCardWeekPlan");
                    comboBoxOperation.SelectedIndex = 0;
                    SelectInputDetail();
                    break;
                default:
                    break;
            }
        }
        public void InitControlParam( CMD_TYPE enumType)
        {
            comboBoxOperation.Items.Clear();
            switch (enumType)
            {
                case CMD_TYPE.ACS:
                    comboBoxOperation.Items.Clear();
                    comboBoxOperation.Items.Add("GATECONTROL"); //remote control door
                    comboBoxOperation.Items.Add("CLEARACSPARAM"); //clear parameters
                    comboBoxOperation.SelectedIndex = 0;
                    SelectInputDetail();
                    break;
                default:
                    break;
            }
        }

        public CommonConfigure()
        {
            InitializeComponent();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
            InitCmdType();
            richTextBoxInput.Clear();
            richTextBoxOutput.Clear();
            comboBoxConfigCmd.Items.Add("GETDEVICECONFIG");
            comboBoxConfigCmd.Items.Add("SETDEVICECONFIG");
            comboBoxConfigCmd.Items.Add("CONTROL");
            //InitGetParam(CMD_TYPE.ACS);

            comboBoxOperation.Text="";
            comboBoxConfigCmd.Text = "";
            comboBoxCommand.Text = "";
        }
        private bool CheckInitParam()
        {
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            if (m_iDeviceIndex < 0)
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "请先选择一个设备");
                return false;
            }
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
            if (m_iDeviceIndex < 0)
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "设备未注册");
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckInitParam();
            string h = System.Text.Encoding.Default.EncodingName.ToString();
           // richTextBoxInput.Text=h;
            m_strInputXml = richTextBoxInput.Text;
            if ((m_csCmd.Equals("GETDEVICECONFIG") == true || m_csCmd.Equals("SETDEVICECONFIG") == true))
            {
                String strTemp = m_csCmd;
                m_szCommand = Encoding.UTF8.GetBytes(strTemp);
                m_struCfg.pCmdBuf = Marshal.AllocHGlobal(1024);
                Marshal.Copy(m_szCommand, 0, m_struCfg.pCmdBuf,m_szCommand.Length);

                strTemp = m_strInputXml;
                m_szInputBuffer = Encoding.UTF8.GetBytes(strTemp);
                m_struCfg.pInBuf = Marshal.AllocHGlobal(1024 * 10);
                Marshal.Copy(m_szInputBuffer, 0, m_struCfg.pInBuf, m_szInputBuffer.Length);

                m_struCfg.pOutBuf = Marshal.AllocHGlobal(1024 * 10);
                for (int i = 0; i < 1024 * 10; i++)
                {
                    Marshal.WriteByte(m_struCfg.pOutBuf, i, 0);
                }
                //Marshal.Copy(m_szOutBuffer, 0, m_struCfg.pInBuf, m_szOutBuffer.Length);
                //m_struCfg.pOutBuf = Marshal.StringToHGlobalAnsi(m_strOutputXml);

                m_struCfg.dwCmdLen = (uint)m_szCommand.Length;
                m_struCfg.dwInSize = (uint)strTemp.Length;
                m_struCfg.dwOutSize = (uint)(1024*10);
                m_struCfg.byRes = new byte[24];

                int dwSize = 0;

                IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(m_struCfg));
                Marshal.StructureToPtr(m_struCfg, ptrCfg, false);

                dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_XML_CFG));
                if (!HCEHomeCMS.NET_ECMS_XMLConfig(m_lUserID, ptrCfg, dwSize))
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_XMLConfig");
                    Marshal.FreeHGlobal(m_struCfg.pCmdBuf);
                    Marshal.FreeHGlobal(m_struCfg.pInBuf);
                    Marshal.FreeHGlobal(m_struCfg.pOutBuf);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
                else
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_XMLConfig");
                    strTemp = "";
                    strTemp = Marshal.PtrToStringAnsi(m_struCfg.pOutBuf, (int)m_struCfg.dwOutSize);
                    richTextBoxOutput.Text = strTemp;

                    if (m_csDetail.Equals("GetFingerPrint"))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(strTemp);
                        XmlNodeList nodeList = doc.SelectSingleNode("//Params//ConfigXML//FingerPrintCfg").ChildNodes;

                        foreach (XmlNode node in nodeList)
                        {
                            if (node.Name == "fingerData")
                            {
                                string strFingerData = node.InnerText;
                                byte[] byteFingerData = System.Text.Encoding.Default.GetBytes(strFingerData);

                                string strpath = null;
                                strpath = string.Format("{0}\\fingerprint.dat", Environment.CurrentDirectory);
                                try
                                {
                                    using (FileStream fs = new FileStream(strpath, FileMode.Create))
                                    {
                                        if (!File.Exists(strpath))
                                        {
                                            MessageBox.Show("Fingerprint storage file create failed！");
                                        }
                                        BinaryWriter objBinaryWrite = new BinaryWriter(fs);
                                        fs.Write(byteFingerData, 0, byteFingerData.Length);
                                        fs.Close();
                                    }
                                    MessageBox.Show("Fingerprint GET SUCCEED", "SUCCESSFUL", MessageBoxButtons.OK);
                                }
                                catch
                                {
                                    MessageBox.Show("Fingerprint process failed");
                                }
                            }
                        }
                    }

                    Marshal.FreeHGlobal(m_struCfg.pCmdBuf);
                    Marshal.FreeHGlobal(m_struCfg.pInBuf);
                    Marshal.FreeHGlobal(m_struCfg.pOutBuf);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
            }
            else if (m_csCmd.Equals("CONTROL") == true)
            {
                HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM struXMLCfg = new HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM();
                string strTemp = m_strInputXml;
                UTF8Encoding utf8 = new UTF8Encoding();
                Byte[] encodedBytes = utf8.GetBytes(strTemp);
                string decodedString = utf8.GetString(encodedBytes);
                m_szInputBuffer = Encoding.UTF8.GetBytes(decodedString);
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


                int dwSize = 0;
                IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struXMLCfg));
                Marshal.StructureToPtr(struXMLCfg, ptrCfg, false);
                dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM));

                if (!HCEHomeCMS.NET_ECMS_XMLRemoteControl(m_lUserID, ptrCfg, dwSize))
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_XMLRemoteControl");
                }
                else
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_XMLRemoteControl");
                    strTemp = "";
                    strTemp = Marshal.PtrToStringAnsi(struXMLCfg.lpStatusBuffer, (int)struXMLCfg.dwStatusBufferSize);
                    richTextBoxOutput.Text = strTemp;
                    Marshal.FreeHGlobal(struXMLCfg.lpInbuffer);
                    Marshal.FreeHGlobal(struXMLCfg.lpStatusBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                }
            }
        }

        private void comboBoxCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxInput.Clear();
            richTextBoxOutput.Clear();
           if(comboBoxCommand.Text.Equals("MOVE_DETECTION")==true)
           {
              m_enumType=CMD_TYPE.MOVE_DETECTION;
           }
            else if(comboBoxCommand.Text.Equals("Alarm")==true)
           {
              m_enumType=CMD_TYPE.Alarm;
           }
           else if (comboBoxCommand.Text.Equals("ACS") == true)
           {
               m_enumType=CMD_TYPE.ACS;
           }
            comboBoxConfigCmd.Items.Clear();
            comboBoxConfigCmd.Items.Add("GETDEVICECONFIG");
            comboBoxConfigCmd.Items.Add("SETDEVICECONFIG");
            comboBoxConfigCmd.Items.Add("CONTROL");
        }

        private void comboBoxConfigCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  this.Refresh();
          //  string test = comboBoxConfigCmd.Text.ToString();
            richTextBoxInput.Clear();
            richTextBoxOutput.Clear();
             if (comboBoxConfigCmd.Text.Equals("GETDEVICECONFIG") == true) //Get parameters
            {
                 m_csCmd="GETDEVICECONFIG";
                InitGetParam(m_enumType);
            }
             else if (comboBoxConfigCmd.Text.Equals("SETDEVICECONFIG") == true) //SET parameters
            {
                m_csCmd = "SETDEVICECONFIG";
                InitSetParam(m_enumType);
                
            }
             else if (comboBoxConfigCmd.Text.Equals("CONTROL") == true) //control parameters
            {
                m_csCmd = "CONTROL";
                InitControlParam(m_enumType);
            } 
        }

        private void comboBoxOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxInput.Clear();
            richTextBoxOutput.Clear();
            comboBoxOperation.Text = "";
             int nCurSel = comboBoxOperation.SelectedIndex;
            if (nCurSel<0)
            {
                return;
            }
            SelectInputDetail();
        }
        
    }
}
