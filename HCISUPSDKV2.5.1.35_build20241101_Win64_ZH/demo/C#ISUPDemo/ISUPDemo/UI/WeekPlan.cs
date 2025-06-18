using ISUPDemo.Public;
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


namespace ISUPDemo.UI
{
    public partial class WeekPlan : Form
    {
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private int m_iDeviceIndex;

        public EHDemoPublic.NET_CMS_WEEK_PLAN_CFG m_struPlanCfg = new EHDemoPublic.NET_CMS_WEEK_PLAN_CFG();
        public EHDemoPublic.NET_CMS_WEEK_PLAN_COND m_struPlanCond = new EHDemoPublic.NET_CMS_WEEK_PLAN_COND();
        private int iItemIndex = -1;
        public WeekPlan()
        {
            InitializeComponent();
            m_struPlanCfg.Init();
            m_struPlanCond.Init();
            cbDeviceType.SelectedIndex = 0;
            cbVerifyMode.SelectedIndex = 0;
            cbDoorStateMode.SelectedIndex = 0;
            cbVerifyMode.Hide();
            label7.Hide();
            cbDoorStateMode.Hide();
            label8.Hide();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
            UpdateList();     
        }
        public class ResponseStatus
        {
            public string requestURL { get; set; }
            public string statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public string errorCode { get; set; }
            public string errorMsg { get; set; }
        }
        public class WeekPlanCfgItem
        {
            public string week { get; set; }
            public int id { get; set; }
            public string enable { get; set; }
            public string doorStatus { get; set; }
            public TimeSegment TimeSegment { get; set; }
        }
        public class DoorStatusWeekPlanCfg
        {
            public string enable { get; set; }
            public List<WeekPlanCfgItem> WeekPlanCfg { get; set; }
        }
        public class DoorStatusWeekPlanCfgRoot
        {
            public DoorStatusWeekPlanCfg DoorStatusWeekPlanCfg { get; set; }
        }
        public class TimeSegment
        {
            public string beginTime { get; set; }
            public string endTime { get; set; }
        }

        public class VerWeekPlanCfgItem
        {
            public string week { get; set; }
            public int id { get; set; }
            public string enable { get; set; }
            public string verifyMode { get; set; }
            public TimeSegment TimeSegment { get; set; }
        }
        public class VerifyWeekPlanCfg
        {
            public string enable { get; set; }
            public List<VerWeekPlanCfgItem> WeekPlanCfg { get; set; }
        }
        public class VerifyWeekPlanCfgRoot
        {
            public VerifyWeekPlanCfg VerifyWeekPlanCfg { get; set; }
        }

        public class UserWeekPlanCfgItem
        {
            public string week { get; set; }
            public int id { get; set; }
            public string enable { get; set; }
            public TimeSegment TimeSegment { get; set; }
        }

        public class UserRightWeekPlanCfg
        {
            public string enable { get; set; }
            public List<UserWeekPlanCfgItem> WeekPlanCfg { get; set; }

        }
        public class UserRightWeekPlanCfgRoot
        {
            public UserRightWeekPlanCfg UserRightWeekPlanCfg { get; set; }
        }

        public HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String Method = String.Empty;
        public byte[] m_szInputBuffer = new byte[1500];
        public byte[] m_szOutBuffer = new byte[1024 * 10];
        public byte[] m_szUrl = new byte[1024];
        IntPtr ptrCfg = IntPtr.Zero;
        string[] week = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
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


        string GetEndTime(EHDemoPublic.NET_CMS_SIMPLE_DAYTIME time)
        {
            int hour = time.byHour;
            int minute = time.byMinute;
            int second = time.bySecond;
            string res = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            if (res.Equals("00:00:00")) res = "23:59:59";
            return res;
        }
        string GetTime(EHDemoPublic.NET_CMS_SIMPLE_DAYTIME time)
        {
            int hour = time.byHour;
            int minute = time.byMinute;
            int second = time.bySecond;
            string res = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            return res;
        }


        private void GetTimeSegment(ref int hour, ref int minute, ref int second, string time)
        {
            string[] sArray = time.Split(':');
            int.TryParse(sArray[0], out hour);
            int.TryParse(sArray[1], out minute);
            int.TryParse(sArray[2], out second);
        }
        private void UpdateList()
        {
            int iDate = cbDate.SelectedIndex;
            EHDemoPublic.NET_CMS_SINGLE_PLAN_SEGMENT[] struTemp = new EHDemoPublic.NET_CMS_SINGLE_PLAN_SEGMENT[EHDemoPublic.MAX_TIMESEGMENT_V30];
            for (int i = 0; i < EHDemoPublic.MAX_TIMESEGMENT_V30; i++)
            {
                struTemp[i] = m_struPlanCfg.struPlanCfg[iDate * EHDemoPublic.MAX_TIMESEGMENT_V30 + i];
            }
            listViewTimeSegment.BeginUpdate();
            listViewTimeSegment.Items.Clear();
            string strTemp = null;
            for (int i = 0; i < EHDemoPublic.MAX_TIMESEGMENT_V30; i++)
            {
                ListViewItem listItem = new ListViewItem();
                strTemp = string.Format("{0}", i + 1);
                listItem.Text = strTemp;
                if (1 == struTemp[i].byEnable)
                {
                    strTemp = "yes";
                }
                else
                {
                    strTemp = "no";
                }
                listItem.SubItems.Add(strTemp);
                EHDemoPublic.NET_CMS_SIMPLE_DAYTIME strTime = struTemp[i].struTimeSegment.struBeginTime;
                strTemp = string.Format("{0,2}:{1,2}:{2,2}", strTime.byHour, strTime.byMinute, strTime.bySecond);
                listItem.SubItems.Add(strTemp);
                strTime = struTemp[i].struTimeSegment.struEndTime;
                strTemp = string.Format("{0,2}:{1,2}:{2,2}", strTime.byHour, strTime.byMinute, strTime.bySecond);
                listItem.SubItems.Add(strTemp);
                if (struTemp[i].byVerifyMode > 28)
                {
                    strTemp = string.Format("{0}", struTemp[i].byVerifyMode);
                }
                else
                {
                    int iDoorIndex = (int)struTemp[i].byVerifyMode;
                    strTemp = EHDemoPublic.strVerify[iDoorIndex];
                }
                listItem.SubItems.Add(strTemp);
                if (struTemp[i].byDoorStatus > 5)
                {
                    strTemp = string.Format("{0}", struTemp[i].byDoorStatus);
                }
                else
                {
                    int iDoorIndex = (int)struTemp[i].byDoorStatus;
                    strTemp = EHDemoPublic.strDoorStatus[iDoorIndex];
                }
                listItem.SubItems.Add(strTemp);
                listViewTimeSegment.Items.Add(listItem);
            }
            listViewTimeSegment.EndUpdate();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            String m_strOutputXml = String.Empty;
            switch (cbDeviceType.SelectedIndex)
            {
                case 2:
                    {
                        String strTemp = String.Empty;
                        String m_strInputXml = String.Empty;
                        strTemp = "/ISAPI/AccessControl/DoorStatusWeekPlanCfg/" + textBoxWPNumber.Text + "?format=json";
                        m_strInputXml = "";
                        m_szUrl = Encoding.UTF8.GetBytes(strTemp);
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
                            struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                            Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                            struPTXML.dwInSize = (uint)strTemp.Length;
                        }
                        struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                        for (int i = 0; i < 1024 * 10; i++)
                        {
                            Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                        }
                        struPTXML.dwOutSize = (uint)(1024 * 10);
                        struPTXML.byRes = new byte[32];
                        try
                        {
                            ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                            Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Memory Exception");
                            Marshal.FreeHGlobal(ptrCfg);
                            return;
                        }
                        if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                            Marshal.FreeHGlobal(struPTXML.pInBuffer);
                            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                            Marshal.FreeHGlobal(ptrCfg);
                            return;
                        }
                        m_strOutputXml = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        break;
                    }
                //m_strOutputXml = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                //if (m_strOutputXml != string.Empty)
                //{
                //    DoorStatusWeekPlanCfgRoot dr = JsonConvert.DeserializeObject<DoorStatusWeekPlanCfgRoot>(m_strOutputXml);
                //    if (dr.DoorStatusWeekPlanCfg.enable.Equals("True"))
                //    {
                //        m_struPlanCfg.byEnable = 1;
                //    }
                //    else
                //    {
                //        m_struPlanCfg.byEnable = 0;
                //    }
                //    foreach (WeekPlanCfgItem item in dr.DoorStatusWeekPlanCfg.WeekPlanCfg)
                //    {
                //        int index = (weekDict[item.week] - 1) * HCNetSDK.MAX_TIMESEGMENT_V30 + item.id - 1;
                //        if (item.enable.Equals("True"))
                //        {
                //            m_struPlanCfg.struPlanCfg[index].byEnable = 1;
                //        }
                //        else
                //        {
                //            m_struPlanCfg.struPlanCfg[index].byEnable = 0;
                //        }
                //        m_struPlanCfg.struPlanCfg[index].byDoorStatus = (byte)doorDict[item.doorStatus];
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment = new HCNetSDK.NET_DVR_TIME_SEGMENT();
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime = new HCNetSDK.NET_DVR_SIMPLE_DAYTIME();
                //        int hour = 0, minute = 0, second = 0;
                //        string time = item.TimeSegment.beginTime;
                //        GetTimeSegment(ref hour, ref minute, ref second, time);
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime.byHour = (byte)hour;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime.byMinute = (byte)minute;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime.bySecond = (byte)second;

                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime = new HCNetSDK.NET_DVR_SIMPLE_DAYTIME();
                //        time = item.TimeSegment.endTime;
                //        GetTimeSegment(ref hour, ref minute, ref second, time);
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime.byHour = (byte)hour;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime.byMinute = (byte)minute;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime.bySecond = (byte)second;
                //    }
                //    UpdateList();
                //}
                case 1:
                    {
                        String strTemp = String.Empty;
                        String m_strInputXml = String.Empty;
                        strTemp = "/ISAPI/AccessControl/VerifyWeekPlanCfg/" + textBoxWPNumber.Text + "?format=json";
                        m_strInputXml = "";
                        m_szUrl = Encoding.UTF8.GetBytes(strTemp);
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
                            struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                            Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                            struPTXML.dwInSize = (uint)strTemp.Length;
                        }
                        struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                        for (int i = 0; i < 1024 * 10; i++)
                        {
                            Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                        }
                        struPTXML.dwOutSize = (uint)(1024 * 10);
                        struPTXML.byRes = new byte[32];
                        try
                        {
                            ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                            Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Memory Exception");
                            Marshal.FreeHGlobal(ptrCfg);
                            return;
                        }
                        if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                            Marshal.FreeHGlobal(struPTXML.pInBuffer);
                            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                            Marshal.FreeHGlobal(ptrCfg);
                            return;
                        }
                        m_strOutputXml = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        break;
                    }
                //if (m_strOutputXml != string.Empty)
                //{
                //    VerifyWeekPlanCfgRoot vr = JsonConvert.DeserializeObject<VerifyWeekPlanCfgRoot>(m_strOutputXml);
                //    if (vr.VerifyWeekPlanCfg.enable.Equals("True"))
                //    {
                //        m_struPlanCfg.byEnable = 1;
                //    }
                //    else
                //    {
                //        m_struPlanCfg.byEnable = 0;
                //    }
                //    foreach (VerWeekPlanCfgItem item in vr.VerifyWeekPlanCfg.WeekPlanCfg)
                //    {
                //        int index = (weekDict[item.week] - 1) * HCNetSDK.MAX_TIMESEGMENT_V30 + item.id - 1;
                //        if (item.enable.Equals("True"))
                //        {
                //            m_struPlanCfg.struPlanCfg[index].byEnable = 1;
                //        }
                //        else
                //        {
                //            m_struPlanCfg.struPlanCfg[index].byEnable = 0;
                //        }
                //        m_struPlanCfg.struPlanCfg[index].byVerifyMode = (byte)verifyDict[item.verifyMode];
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment = new HCNetSDK.NET_DVR_TIME_SEGMENT();
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime = new HCNetSDK.NET_DVR_SIMPLE_DAYTIME();
                //        int hour = 0, minute = 0, second = 0;
                //        string time = item.TimeSegment.beginTime;
                //        GetTimeSegment(ref hour, ref minute, ref second, time);
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime.byHour = (byte)hour;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime.byMinute = (byte)minute;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime.bySecond = (byte)second;

                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struBeginTime = new HCNetSDK.NET_DVR_SIMPLE_DAYTIME();
                //        time = item.TimeSegment.endTime;
                //        GetTimeSegment(ref hour, ref minute, ref second, time);
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime.byHour = (byte)hour;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime.byMinute = (byte)minute;
                //        m_struPlanCfg.struPlanCfg[index].struTimeSegment.struEndTime.bySecond = (byte)second;
                //    }
                //    UpdateList();
                case 0:
                    {
                        string strTemp = "/ISAPI/AccessControl/UserRightWeekPlanCfg/" + textBoxWPNumber.Text + "?format=json";
                        string m_strInputXml = string.Empty;
                        m_szUrl = Encoding.UTF8.GetBytes(strTemp);
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
                            struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                            Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                            struPTXML.dwInSize = (uint)strTemp.Length;
                        }
                        struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                        for (int i = 0; i < 1024 * 10; i++)
                        {
                            Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                        }
                        struPTXML.dwOutSize = (uint)(1024 * 10);
                        struPTXML.byRes = new byte[32];
                        try
                        {
                            ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                            Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Memory Exception");
                            Marshal.FreeHGlobal(ptrCfg);
                            return;
                        }
                        if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Get Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                            Marshal.FreeHGlobal(struPTXML.pInBuffer);
                            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                            Marshal.FreeHGlobal(ptrCfg);
                            return;
                        }
                        try
                        {
                            m_strOutputXml = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Memory Exception");
                            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                            return;
                        }
                        if (m_strOutputXml != string.Empty)
                        {
                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(m_strOutputXml);
                            if (rs.statusString.Equals("OK"))
                            {
                                MessageBox.Show("Get WeekPlan Success");
                            }
                            else
                            {
                                MessageBox.Show("Get WeekPlan Failed");
                            }
                            //UpdateList();
                        }
                        break;
                    }
                default:
                    MessageBox.Show("unknown command");
                    return;
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }


        private void textBoxWPNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//backspace 
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//1-9 is permitted  
                {
                    e.Handled = true;
                }
            }
        }

        private void textBoxLCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//backspace 
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//0-9 is permitted  
                {
                    e.Handled = true;
                }
            }
        }


        private void cbDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDeviceType.SelectedIndex)
            {
                case 0:
                    // 人员周计划
                    cbVerifyMode.Hide();
                    label7.Hide();
                    cbDoorStateMode.Hide();
                    label8.Hide();
                    break;
                case 1:
                    //读卡器验证方式周计划
                    cbVerifyMode.Show();
                    label7.Show(); ;
                    cbDoorStateMode.Hide();
                    label8.Hide();
                    break;
                case 2:
                    // 门状态周计划
                    cbVerifyMode.Hide();
                    label7.Hide();
                    cbDoorStateMode.Show();
                    label8.Show();
                    break;
                default:
                    foreach (Control c in this.Controls)
                    {
                        c.Enabled = false;
                    }
                    MessageBox.Show("Wrong Device Type!");
                    break;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            String strOutXML = String.Empty;

            if (checkBoxEnableWP.Checked)
            {
                m_struPlanCfg.byEnable = 1;
            }
            else
            {
                m_struPlanCfg.byEnable = 0;
            }
            string TemplateName = "TemplateExample";
            IntPtr ptrCfg = IntPtr.Zero;
            switch (cbDeviceType.SelectedIndex)
            {
                case 2:
                    {
                        String strTemp = String.Empty;
                        String m_strInputXml = String.Empty;
                        strTemp = "/ISAPI/AccessControl/DoorStatusWeekPlanCfg/" + textBoxWPNumber.Text + "?format=json";
                        for (int i = 0; i < EHDemoPublic.MAX_DAYS * EHDemoPublic.MAX_TIMESEGMENT_V30; ++i)
                        {
                            EHDemoPublic.NET_CMS_SINGLE_PLAN_SEGMENT struTemp = m_struPlanCfg.struPlanCfg[i];
                            if (i == 0)
                            {
                                m_strInputXml = "{\"DoorStatusWeekPlanCfg\":{\"enable\":" + checkBoxEnableWP.Checked.ToString().ToLower() +
                                    ",\"WeekPlanCfg\":[{\"week\":\"" + week[i / 8] +
                                "\",\"id\":" + (i % 8 + 1) +
                                ",\"enable\":" + ((struTemp.byEnable != 0) ? "true" : "false") +
                                ",\"doorStatus\":\"" + EHDemoPublic.strDoorStatus[struTemp.byDoorStatus] +
                                "\",\"TimeSegment\":{\"beginTime\":\"" + GetTime(struTemp.struTimeSegment.struBeginTime) +
                                "\",\"endTime\":\"" + GetEndTime(struTemp.struTimeSegment.struEndTime) + "\"}}";
                            }
                            else
                            {
                                m_strInputXml += ",{\"week\":\"" + week[i / 8] +
                                "\",\"id\":" + (i % 8 + 1) +
                                ",\"enable\":" + ((struTemp.byEnable != 0) ? "true" : "false") +
                                ",\"doorStatus\":\"" + EHDemoPublic.strDoorStatus[struTemp.byDoorStatus] +
                                "\",\"TimeSegment\":{\"beginTime\":\"" + GetTime(struTemp.struTimeSegment.struBeginTime) +
                                "\",\"endTime\":\"" + GetEndTime(struTemp.struTimeSegment.struEndTime) + "\"}}";
                            }
                        }
                        m_strInputXml += "]}}";
                        int OutBufferLen = 20* 1024;
                        ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);

                        if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                            Marshal.FreeHGlobal(struPTXML.pInBuffer);
                            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                            Marshal.FreeHGlobal(ptrCfg);
                            break;
                        }
                        strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        if (strOutXML != string.Empty)
                        {
                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                            if (rs.statusCode.Equals("1"))
                            {
                                MessageBox.Show("Set Data Succ!");
                            }
                        }
                        Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        Marshal.FreeHGlobal(ptrCfg);
                         //计划模板配置
                        strTemp = "/ISAPI/AccessControl/DoorStatusPlanTemplate/" + textBoxWPNumber.Text + "?format=json";
                        m_strInputXml = "{\"DoorStatusPlanTemplate\":{\"enable\":true" + "\",\"templateName\":" + TemplateName +
                            ",\"weekPlanNo\":" + textBoxWPNumber.Text +",\"holidayGroupNo\":" + "" + "\"}}";
                        ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
                        if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            break;
                        }
                        Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        Marshal.FreeHGlobal(ptrCfg);
                        //门状态计划参数配置
                        strTemp = "/ISAPI/AccessControl/DoorStatusPlan/1?format=json";
                        m_strInputXml = "{\"DoorStatusPlan\":{\"templateNo\":" + 1 + "\"}}";

                        ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
                        if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            break;
                        }
                        strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        if (strOutXML != string.Empty)
                        {
                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                            if (rs.statusCode.Equals("1"))
                            {
                                MessageBox.Show("Set Data Succ!");
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        String strTemp = String.Empty;
                        String m_strInputXml = String.Empty;
                        strTemp = "/ISAPI/AccessControl/VerifyWeekPlanCfg/" + textBoxWPNumber.Text + "?format=json";
                        for (int i = 0; i < EHDemoPublic.MAX_DAYS * EHDemoPublic.MAX_TIMESEGMENT_V30; ++i)
                        {
                            EHDemoPublic.NET_CMS_SINGLE_PLAN_SEGMENT struTemp = m_struPlanCfg.struPlanCfg[i];
                            if (i == 0)
                            {
                                m_strInputXml = "{\"VerifyWeekPlanCfg\":{\"enable\":" + checkBoxEnableWP.Checked.ToString().ToLower() +
                                    ",\"WeekPlanCfg\":[{\"week\":\"" + week[i / 8] +
                                "\",\"id\":" + (i % 8 + 1) +
                                ",\"enable\":" + ((struTemp.byEnable != 0) ? "true" : "false") +
                                ",\"verifyMode\":\"" + EHDemoPublic.strVerify[struTemp.byVerifyMode] +
                                "\",\"TimeSegment\":{\"beginTime\":\"" + GetTime(struTemp.struTimeSegment.struBeginTime) +
                                "\",\"endTime\":\"" + GetEndTime(struTemp.struTimeSegment.struEndTime) + "\"}}";
                            }
                            else
                            {
                                m_strInputXml += ",{\"week\":\"" + week[i / 8] +
                                "\",\"id\":" + (i % 8 + 1) +
                                ",\"enable\":" + ((struTemp.byEnable != 0) ? "true" : "false") +
                                ",\"verifyMode\":\"" + EHDemoPublic.strVerify[struTemp.byVerifyMode] +
                                "\",\"TimeSegment\":{\"beginTime\":\"" + GetTime(struTemp.struTimeSegment.struBeginTime) +
                                "\",\"endTime\":\"" + GetEndTime(struTemp.struTimeSegment.struEndTime) + "\"}}";
                            }
                        }
                        m_strInputXml += "]}}";
                        m_szUrl = Encoding.UTF8.GetBytes(strTemp);
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
                            struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                            Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                            struPTXML.dwInSize = (uint)strTemp.Length;
                        }
                        struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                        for (int i = 0; i < 1024 * 10; i++)
                        {
                            Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                        }
                        struPTXML.dwOutSize = (uint)(1024 * 10);
                        struPTXML.byRes = new byte[32];
                        ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                        Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                        if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            break;
                        }
                        Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        Marshal.FreeHGlobal(ptrCfg);
                        strTemp = "/ISAPI/AccessControl/VerifyPlanTemplate/" + textBoxWPNumber.Text + "?format=json";
                        m_strInputXml = "{\"VerifyPlanTemplate\":{\"enable\":true" + "\",\"templateName\":" + TemplateName +
                             ",\"weekPlanNo\":" + textBoxWPNumber.Text + ",\"holidayGroupNo\":" + "" + "\"}}";
                        {
                            //   "VerifyPlanTemplate": {
                            //       "enable": true,    //必填，boolean，是否启用：true-启用，false-不启用
                            //       "templateName": "",    //必填，string，模板名称
                            //       "weekPlanNo" : 1,    //必填，integer，周计划编号
                            //       "holidayGroupNo" : "1,3,5"    //必填，string，假日组编号
                            //   }
                            //}
                            m_szUrl = Encoding.UTF8.GetBytes(strTemp);
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
                                struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                                Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                                struPTXML.dwInSize = (uint)strTemp.Length;
                            }
                            struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                            for (int i = 0; i < 1024 * 10; i++)
                            {
                                Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                            }
                            struPTXML.dwOutSize = (uint)(1024 * 10);
                            struPTXML.byRes = new byte[32];
                            ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                            Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                            {
                                MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                                break;
                            }
                            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                            Marshal.FreeHGlobal(struPTXML.pInBuffer);
                            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                            Marshal.FreeHGlobal(ptrCfg);

                            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                            if (strOutXML != string.Empty)
                            {
                                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                                if (rs.statusCode.Equals("1"))
                                {
                                    MessageBox.Show("Set Data Succ!");
                                }
                            }
                            break;
                        }
                    }
                case 0:
                    {
                        //UserRight WeekPlan
                        string strTemp = "/ISAPI/AccessControl/UserRightWeekPlanCfg/" + textBoxWPNumber.Text + "?format=json";
                        string m_strInputXml = string.Empty;
                        for (int i = 0; i < EHDemoPublic.MAX_DAYS * 8; ++i)
                        {
                            EHDemoPublic.NET_CMS_SINGLE_PLAN_SEGMENT struTemp = m_struPlanCfg.struPlanCfg[i];
                            if (i == 0)
                            {
                                m_strInputXml = "{\"UserRightWeekPlanCfg\":{\"enable\":" + checkBoxEnableWP.Checked.ToString().ToLower() +
                                    ",\"WeekPlanCfg\":[{\"week\":\"" + week[i / 8] +
                                "\",\"id\":" + (i % 8 + 1) +
                                ",\"enable\":" + ((struTemp.byEnable != 0) ? "true" : "false") +
                                ",\"TimeSegment\":{\"beginTime\":\"" + GetTime(struTemp.struTimeSegment.struBeginTime) +
                                "\",\"endTime\":\"" + GetEndTime(struTemp.struTimeSegment.struEndTime) + "\"}}";
                            }
                            else
                            {
                                m_strInputXml += ",{\"week\":\"" + week[i / 8] +
                                "\",\"id\":" + (i % 8 + 1) +
                                ",\"enable\":" + ((struTemp.byEnable != 0) ? "true" : "false") +
                                ",\"TimeSegment\":{\"beginTime\":\"" + GetTime(struTemp.struTimeSegment.struBeginTime) +
                                "\",\"endTime\":\"" + GetEndTime(struTemp.struTimeSegment.struEndTime) + "\"}}";
                            }
                        }
                        m_strInputXml += "]}}";

                        
                        int OutBufferLen = 20 * 1024;
                        ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);


                   
                        if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            break;
                        }
                        strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        Marshal.FreeHGlobal(ptrCfg);
                        //假日参数配置
                        //strTemp = "/ISAPI/AccessControl/UserRightHolidayPlanCfg/1?format=json";
                        //string BeginDate = "2020-05-10";
                        //string EndDate = "2020-05-10";
                        //string BeginTime = "10:00:00";
                        //string EndTime = "12:00:00";
                        //string id = "1";
                        //m_strInputXml = "{\"UserRightHolidayPlanCfg\":{\"enable\":\"" + checkBoxEnableWP.Checked.ToString().ToLower() +
                        //"\",\"beginDate\":\"" + BeginDate + "\",\"endDate\":\"" + EndDate +
                        //"\",\"HolidayPlanCfg\":[{\"id\":\"" + 1 + "\",\"enable\":\"" + true +
                        //"\",\"TimeSegment\":{\"beginTime\":\"" + BeginTime + "\",\"endTime\":\"" + EndTime + "\"}}]}}";

                        ////m_strInputXml = "{\"UserRightHolidayPlanCfg\":{\"enable\":" + checkBoxEnableWP.Checked.ToString().ToLower() +
                        ////           ",\"beginDate\":" + BeginDate + ",\"endDate\":" + EndDate +
                        ////            ",\"HolidayPlanCfg\":[{\"id\":" + 1 +
                        ////            ",\"enable\":" + true +
                        ////            ",\"TimeSegment\":{\"beginTime\":\"" + BeginTime +
                        ////            "\",\"endTime\":\"" + EndTime + "\"}}";
                        ////m_strInputXml += "]}}";

                        //m_szUrl = Encoding.UTF8.GetBytes(strTemp);
                        //struPTXML.pRequestUrl = Marshal.AllocHGlobal(300);
                        //Marshal.Copy(m_szUrl, 0, struPTXML.pRequestUrl, m_szUrl.Length);
                        //struPTXML.dwRequestUrlLen = (uint)m_szUrl.Length;
                        //strTemp = m_strInputXml;
                        //if ("" == strTemp)
                        //{
                        //    struPTXML.pInBuffer = IntPtr.Zero;
                        //    struPTXML.dwInSize = 0;
                        //}
                        //else
                        //{
                        //    m_szInputBuffer = Encoding.UTF8.GetBytes(strTemp);
                        //    struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                        //    Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                        //    struPTXML.dwInSize = (uint)strTemp.Length;
                        //}
                        //struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                        //for (int i = 0; i < 1024 * 10; i++)
                        //{
                        //    Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                        //}
                        //struPTXML.dwOutSize = (uint)(1024 * 10);
                        //struPTXML.byRes = new byte[32];
                        //ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                        //Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                        //if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        //{
                        //    MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                        //    break;
                        //}
                        //Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        //Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        //Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        //Marshal.FreeHGlobal(ptrCfg);    
                        ////假日组参数配置
                        //string GroupName = "HolidayGroup";
                        //strTemp = "/ISAPI/AccessControl/UserRightHolidayGroupCfg/1?format=json";
                        //m_strInputXml = "{\"UserRightHolidayGroupCfg\":{\"enable\":true\"" + "\",\"groupName\":\"" + GroupName + "\",\"holidayPlanNo\":\"" + 1 +
                        //    "\"}}";

                        //m_szUrl = Encoding.UTF8.GetBytes(strTemp);
                        //struPTXML.pRequestUrl = Marshal.AllocHGlobal(300);
                        //Marshal.Copy(m_szUrl, 0, struPTXML.pRequestUrl, m_szUrl.Length);
                        //struPTXML.dwRequestUrlLen = (uint)m_szUrl.Length;
                        //strTemp = m_strInputXml;
                        //if ("" == strTemp)
                        //{
                        //    struPTXML.pInBuffer = IntPtr.Zero;
                        //    struPTXML.dwInSize = 0;
                        //}
                        //else
                        //{
                        //    m_szInputBuffer = Encoding.UTF8.GetBytes(strTemp);
                        //    struPTXML.pInBuffer = Marshal.AllocHGlobal(1024 * 10);
                        //    Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                        //    struPTXML.dwInSize = (uint)strTemp.Length;
                        //}
                        //struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
                        //for (int i = 0; i < 1024 * 10; i++)
                        //{
                        //    Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
                        //}
                        //struPTXML.dwOutSize = (uint)(1024 * 10);
                        //struPTXML.byRes = new byte[32];
                        //ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
                        //Marshal.StructureToPtr(struPTXML, ptrCfg, true);
                        //if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        //{
                        //    MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                        //    break;
                        //}
                        //Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        //Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        //Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        //Marshal.FreeHGlobal(ptrCfg);

                        //人员权限计划模板参数配置
                        int TemplateNO = 1;
                        strTemp = "/ISAPI/AccessControl/UserRightPlanTemplate/" + TemplateNO + "?format=json";

                        //                        {
                        //    "UserRightPlanTemplate": {
                        //        "enable": true,    //必填，boolean，是否启用：true-启用，false-不启用
                        //        "templateName": "",    //必填，string，模板名称
                        //        "weekPlanNo" : 1,    //必填，integer，周计划编号
                        //        "holidayGroupNo" : "1,3,5"    //必填，string，假日组编号
                        //    }
                        //}

                       m_strInputXml = "{\"UserRightPlanTemplate\":{\"enable\":true" + ",\"templateName\":" + TemplateName +
                             ",\"weekPlanNo\":" + int.Parse(textBoxWPNumber.Text) + ",\"holidayGroupNo\": \"\" }}";
                        //m_strInputXml = "{\"UserRightPlanTemplate\":{\"enable\":true,\"templateName\":\"TemplateExample\",\"weekPlanNo\":1,\"holidayGroupNo\":\"1\"}}";

                     
                       ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);

                        if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                        {
                            MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                            break;
                        }
                        strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
                        if (strOutXML != string.Empty)
                        {
                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                            if (rs.statusCode.Equals("1"))
                            {
                                MessageBox.Show("Set WeekPlan Succ!");
                            }
                        }
                        break;
                    }
                default:
                    MessageBox.Show("unknow command");
                    break;
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int iDateIndex = cbDate.SelectedIndex;
            int iDate = iDateIndex * EHDemoPublic.MAX_TIMESEGMENT_V30 + iItemIndex;
            if (-1 == iDate)
            {
                MessageBox.Show("Please select the list!!!");
                return;
            }
            if (checkBoxEnableTime.Checked)
            {
                m_struPlanCfg.struPlanCfg[iDate].byEnable = 1;
            }
            else
            {
                m_struPlanCfg.struPlanCfg[iDate].byEnable = 0;
            }
            m_struPlanCfg.struPlanCfg[iDate].byVerifyMode = (byte)cbVerifyMode.SelectedIndex;
            m_struPlanCfg.struPlanCfg[iDate].byDoorStatus = (byte)cbDoorStateMode.SelectedIndex;
            m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byHour = (byte)dTPStartTime.Value.Hour;
            m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byMinute = (byte)dTPStartTime.Value.Minute;
            m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.bySecond = (byte)dTPStartTime.Value.Second;
            if (m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byHour == 23
                && m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byMinute == 59
                && m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.bySecond == 59)
            {
                m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byHour = 24;
                m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byMinute = 0;
                m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.bySecond = 0;
            }
            m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struEndTime.byHour = (byte)dTPEndTime.Value.Hour;
            m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struEndTime.byMinute = (byte)dTPEndTime.Value.Minute;
            m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struEndTime.bySecond = (byte)dTPEndTime.Value.Second;
            if (m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byHour == 23
                && m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byMinute == 59
                && m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.bySecond == 59)
            {
                m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byHour = 24;
                m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.byMinute = 0;
                m_struPlanCfg.struPlanCfg[iDate].struTimeSegment.struBeginTime.bySecond = 0;
            }
            UpdateList();
        }
        private void listViewTimeSegment_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            iItemIndex = e.ItemIndex;
            if (iItemIndex < 0)
            {
                return;
            }
            int iDate = cbDate.SelectedIndex;
            int i = iDate * EHDemoPublic.MAX_TIMESEGMENT_V30 + iItemIndex;

            if (1 == m_struPlanCfg.struPlanCfg[i].byEnable)
            {
                checkBoxEnableTime.Checked = true;
            }
            else
            {
                checkBoxEnableTime.Checked = false;
            }
            cbVerifyMode.SelectedIndex = (int)m_struPlanCfg.struPlanCfg[i].byVerifyMode;
            cbDoorStateMode.SelectedIndex = (int)m_struPlanCfg.struPlanCfg[i].byDoorStatus;
            EHDemoPublic.NET_CMS_SIMPLE_DAYTIME struTime = new EHDemoPublic.NET_CMS_SIMPLE_DAYTIME();
            if (EHDemoPublic.CheckDate(m_struPlanCfg.struPlanCfg[i].struTimeSegment.struBeginTime))
            {
                struTime = m_struPlanCfg.struPlanCfg[i].struTimeSegment.struBeginTime;
                if (struTime.byHour == 24 && struTime.byMinute == 0 && struTime.bySecond == 0)
                {
                    struTime.byHour = 23;
                    struTime.byMinute = 59;
                    struTime.bySecond = 59;
                }
                dTPStartTime.Value = new System.DateTime(dTPStartTime.Value.Year,
                    dTPStartTime.Value.Month, dTPStartTime.Value.Day, struTime.byHour,
                    struTime.byMinute, struTime.bySecond);
            }
            if (EHDemoPublic.CheckDate(m_struPlanCfg.struPlanCfg[i].struTimeSegment.struEndTime))
            {
                struTime = m_struPlanCfg.struPlanCfg[i].struTimeSegment.struEndTime;
                if (struTime.byHour == 24 && struTime.byMinute == 0 && struTime.bySecond == 0)
                {
                    struTime.byHour = 23;
                    struTime.byMinute = 59;
                    struTime.bySecond = 59;
                }
                dTPEndTime.Value = new System.DateTime(dTPEndTime.Value.Year,
                    dTPEndTime.Value.Month, dTPEndTime.Value.Day, struTime.byHour,
                    struTime.byMinute, struTime.bySecond);
            }
        }


        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void WeekPlan_Load(object sender, EventArgs e)
        {

        }
    }
}
