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
    public partial class PlanTemplate : Form
    {
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private int m_iDeviceIndex;
        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();

        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];

        public PlanTemplate()
        {
            InitializeComponent();
            cbPlanTemplateType.SelectedIndex = 0;
            textBoxTemplateNo.Text = "1";

            checkEnable.Checked = false;
            textName.Text = "";
            textBoxHoildayGroupNo.Text = "";
            textBoxWeekPlan.Text = "";

            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

        }

        public class DoorStatusPlanTemplateObject
        {
            public DoorStatusPlanTemplate DoorStatusPlanTemplate { get; set; }
        }

        public class UserRightPlanTemplateObject
        {
            public UserRightPlanTemplate UserRightPlanTemplate { get; set; }
        }

        public class VerifyPlanTemplateObject
        {
            public VerifyPlanTemplate VerifyPlanTemplate { get; set; }
        }

        public class DoorStatusPlanTemplate
        {
            public bool enable { get; set; }

            public string templateName { get; set; }

            public int weekPlanNo { get; set; }

            public string holidayGroupNo { get; set; }
        }

        public class UserRightPlanTemplate
        {
            public bool enable { get; set; }

            public string templateName { get; set; }

            public int weekPlanNo { get; set; }

            public string holidayGroupNo { get; set; }
        }

        public class VerifyPlanTemplate
        {
            public bool enable { get; set; }

            public string templateName { get; set; }

            public int weekPlanNo { get; set; }

            public string holidayGroupNo { get; set; }
        }

        public class ResponseStatus
        {
            public string requestURL { get; set; }
            public int statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
        }

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
        private void btnGet_Click(object sender, EventArgs e)
        {
            String strTemp = "";
            String m_strInputXml = "";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 1024;

            switch (cbPlanTemplateType.SelectedIndex)
            {
                case 0:
                    {
                        strTemp = "/ISAPI/AccessControl/DoorStatusPlanTemplate/" + textBoxTemplateNo.Text + "?format=json";
                        break;
                    }
                case 1:
                    {
                        strTemp = "/ISAPI/AccessControl/VerifyPlanTemplate/" + textBoxTemplateNo.Text + "?format=json";
                        break;
                    }
                case 2:
                    {
                        strTemp = "/ISAPI/AccessControl/UserRightPlanTemplate/" + textBoxTemplateNo.Text + "?format=json";
                        break;
                    }
                default:
                    MessageBox.Show("unknown command");
                    return;
            }
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);

            if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String szResponse = "";
            szResponse = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            //parse json data
            if (szResponse != string.Empty)
            {
                switch (cbPlanTemplateType.SelectedIndex)
                {
                    case 0:
                        {
                            DoorStatusPlanTemplateObject rb = JsonConvert.DeserializeObject<DoorStatusPlanTemplateObject>(szResponse);

                            if (rb.DoorStatusPlanTemplate == null)
                            {
                                MessageBox.Show("DoorStatusPlanTemplate is NULL!");
                                break;
                            }
                            checkEnable.Checked = rb.DoorStatusPlanTemplate.enable;
                            textName.Text = rb.DoorStatusPlanTemplate.templateName;
                            textBoxHoildayGroupNo.Text = rb.DoorStatusPlanTemplate.holidayGroupNo;
                            textBoxWeekPlan.Text = rb.DoorStatusPlanTemplate.weekPlanNo.ToString();
                            break;
                        }
                    case 1:
                        {
                            VerifyPlanTemplateObject rb = JsonConvert.DeserializeObject<VerifyPlanTemplateObject>(szResponse);

                            if (rb.VerifyPlanTemplate == null)
                            {
                                MessageBox.Show("VerifyPlanTemplate is NULL!");
                                break;
                            }
                            checkEnable.Checked = rb.VerifyPlanTemplate.enable;
                            textName.Text = rb.VerifyPlanTemplate.templateName;
                            textBoxHoildayGroupNo.Text = rb.VerifyPlanTemplate.holidayGroupNo;
                            textBoxWeekPlan.Text = rb.VerifyPlanTemplate.weekPlanNo.ToString();
                            break;
                        }
                    case 2:
                        {
                            UserRightPlanTemplateObject rb = JsonConvert.DeserializeObject<UserRightPlanTemplateObject>(szResponse);

                            if (rb.UserRightPlanTemplate == null)
                            {
                                MessageBox.Show("UserRightPlanTemplate is NULL!");
                                break;
                            }
                            checkEnable.Checked = rb.UserRightPlanTemplate.enable;
                            textName.Text = rb.UserRightPlanTemplate.templateName;
                            textBoxHoildayGroupNo.Text = rb.UserRightPlanTemplate.holidayGroupNo;
                            textBoxWeekPlan.Text = rb.UserRightPlanTemplate.weekPlanNo.ToString();
                            break;
                        }
                    default:
                        MessageBox.Show("unknown data");
                        return;
                }
            }

            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            IntPtr ptrCfg = IntPtr.Zero;
            string strTemp = string.Empty;
            string m_strInputXml = string.Empty;

            string szEnable = "false";
            if(checkEnable.Checked)
            {
                szEnable = "true";
            }
            switch (cbPlanTemplateType.SelectedIndex)
            {
                case 0:
                    {
                        //DoorStatusPlanTemplate
                        strTemp = "/ISAPI/AccessControl/DoorStatusPlanTemplate/" + textBoxTemplateNo.Text + "?format=json";
                        m_strInputXml = "{\"DoorStatusPlanTemplate\":{\"enable\":" + szEnable +
                        ",\"templateName\":\"" + textName.Text +
                        "\",\"weekPlanNo\":" + textBoxWeekPlan.Text +
                        ",\"holidayGroupNo\":\"" + textBoxHoildayGroupNo.Text + "\"}}";
                        break;
                    }
                case 1:
                    {
                        //VerifyPlanTemplate
                        strTemp = "/ISAPI/AccessControl/VerifyPlanTemplate/" + textBoxTemplateNo.Text + "?format=json";
                        m_strInputXml = "{\"VerifyPlanTemplate\":{\"enable\":" + szEnable +
                        ",\"templateName\":\"" + textName.Text +
                        "\",\"weekPlanNo\":" + textBoxWeekPlan.Text +
                        ",\"holidayGroupNo\":\"" + textBoxHoildayGroupNo.Text + "\"}}";
                        break;
                    }
                case 2:
                    {
                        //UserRightPlanTemplate
                        strTemp = "/ISAPI/AccessControl/UserRightPlanTemplate/" + textBoxTemplateNo.Text + "?format=json";
                        m_strInputXml = "{\"DoorStatusPlanTemplate\":{\"enable\":" + szEnable +
                        ",\"templateName\":\"" + textName.Text +
                        "\",\"weekPlanNo\":" + textBoxWeekPlan.Text +
                        ",\"holidayGroupNo\":\"" + textBoxHoildayGroupNo.Text + "\"}}";
                        break;
                    }
                default:
                    MessageBox.Show("unknow template type");
                    break;
            }

            int OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (1 == rs.statusCode)
                {
                    MessageBox.Show("Set Card Succ!");
                }
                else
                {
                    MessageBox.Show("Please Check Employee is Existed or Not!");
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }

        private void textBoxTemplateNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//backspace 
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//1-9 is permitted  
                {
                    e.Handled = true;
                }
            }
        }
    }
}
