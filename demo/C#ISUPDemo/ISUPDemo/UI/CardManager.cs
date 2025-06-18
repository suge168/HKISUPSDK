using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ISUPDemo.Public;

namespace ISUPDemo.UI
{
    public partial class CardManager : Form
    {
        public class ResponseStatus
        {
            public string requestURL { get; set; }
            public int statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
        }
        public class CardInfo
        {
            public string employeeNo { get; set; }
            public string cardNo { get; set; }
            public string cardType { get; set; }
            public string leaderCard { get; set; }
        }
        public class CardInfoSearch
        {
            public string searchID { get; set; }
            public string responseStatusStrg { get; set; }
            public string numOfMatches { get; set; }
            public string totalMatches { get; set; }
            public List<CardInfo> CardInfo { get; set; }
        }
        public class CardInfoSearchRoot
        {
            public CardInfoSearch CardInfoSearch { get; set; }
        }
        public class Valid
        {
            public string enable { get; set; }
            public string beginTime { get; set; }
            public string endTime { get; set; }
            public string timeType { get; set; }
        }
        public class RightPlanItem
        {
            public int doorNo { get; set; }
            public string planTemplateNo { get; set; }
        }
        public class UserInfoItem
        {
            public string employeeNo { get; set; }
            public string name { get; set; }
            public string userType { get; set; }
            public string closeDelayEnabled { get; set; }
            public Valid Valid { get; set; }
            public string belongGroup { get; set; }
            public string password { get; set; }
            public string doorRight { get; set; }
            public List<RightPlanItem> RightPlan { get; set; }
            public int maxOpenDoorTime { get; set; }
            public int openDoorTime { get; set; }
            public string userVerifyMode { get; set; }
        }
        public class UserInfoSearch
        {
            public string searchID { get; set; }
            public string responseStatusStrg { get; set; }
            public int numOfMatches { get; set; }
            public int totalMatches { get; set; }
            public List<UserInfoItem> UserInfo { get; set; }
        }
        public class UserInfoSearchRoot
        {
            public UserInfoSearch UserInfoSearch { get; set; }
        }

        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String m_strInputXml;
        String m_strOutputXml;
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];
        private int m_iDeviceIndex;
       

        public CardManager()
        {
            InitializeComponent();
            CardTypeBox.SelectedIndex = 0;
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

        }

        public void ConfigMethod(ref IntPtr ptrCfg,String strTemp, String m_strInputXml, int OutBufferLen)
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

        private void CardSetBtn_Click(object sender, EventArgs e)
        {
            string strTemp = string.Empty;
            string m_strInputXml = string.Empty;
            strTemp = "/ISAPI/AccessControl/CardInfo/SetUp?format=json";
            m_strInputXml = "{\"CardInfo\":{\"employeeNo\":\"" + EmployeeNoBox.Text +
                "\",\"cardNo\":\"" + CardNoBox.Text +
                "\",\"cardType\":\"" + CardTypeBox.SelectedItem.ToString() + "\"}}";
            IntPtr ptrCfg = IntPtr.Zero;
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
        private void CardSearchBtn_Click(object sender, EventArgs e)
        {
            String strTemp = "/ISAPI/AccessControl/CardInfo/Search?format=json";
            String m_strInputXml = "{\"CardInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + EmployeeNoBox.Text + "\"}]}}";
            int OutBufferLen = 10*1024;
            IntPtr ptrCfg = IntPtr.Zero;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
          
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: "+Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = "";
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != "")
            {
                CardInfoSearchRoot cs = JsonConvert.DeserializeObject<CardInfoSearchRoot>(strOutXML);
                if (cs.CardInfoSearch.responseStatusStrg.Equals("OK"))
                {
                   // MessageBox.Show("Search Card Succ!");
                    foreach (CardInfo info in cs.CardInfoSearch.CardInfo)
                    {
                        EmployeeNoBox.Text = info.employeeNo;
                        CardNoBox.Text = info.cardNo;
                        CardTypeBox.Text = info.cardType;
                        break;
                    }
                }
                else if (cs.CardInfoSearch.responseStatusStrg.Equals("NO MATCH"))
                {
                    MessageBox.Show("CardInfo not exist!");
                }
                else
                {
                    MessageBox.Show(cs.CardInfoSearch.responseStatusStrg);
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //Delete Card Info
        private void CardDeleteBtn_Click(object sender, EventArgs e)
        {
            string strTemp = "/ISAPI/AccessControl/CardInfo/Delete?format=json";
            string m_strInputXml = "{\"CardInfoDelCond\":{\"CardNoList\":[{\"cardNo\":\"" + CardNoBox.Text + "\"}]}}";

            int OutBufferLen = 1024;
            IntPtr ptrCfg = IntPtr.Zero;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);


          
            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = "";
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (rs.statusString.Equals("OK"))
                {
                    MessageBox.Show("Delete Card Succ!");
                    EmployeeNoBox.Text = "";
                    CardNoBox.Text = "";
                }
                else
                {
                    MessageBox.Show(rs.statusString);
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //Count Total Num 0f Card or Num of Card that has specificed EmployeeNo
        private void CardCountBtn_Click(object sender, EventArgs e)
        {
            string strTemp = string.Empty;
            string m_strInputXml = string.Empty;
            string EmployeeNo = EmployeeNoBox.Text;
            if (EmployeeNo.Length == 0)
            {
                strTemp = "/ISAPI/AccessControl/CardInfo/Count?format=json";
                m_strInputXml = string.Empty;
            }
            else
            {
                strTemp = "/ISAPI/AccessControl/CardInfo/Count?format=json&employeeNo=" + EmployeeNo;
                m_strInputXml = string.Empty;
            }

            int OutBufferLen = 1024;
            IntPtr ptrCfg = IntPtr.Zero;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);

           
            if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = "";
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            //Find cardNumber from strOutXML，Show on the Window
            string Res = "";
            int index = 0;
            bool flag = false;
            if (strOutXML != string.Empty)
            {
                index = strOutXML.LastIndexOf(":");
                for (int i = index; i < strOutXML.Length; i++)
                {
                    if (strOutXML[i] >= '0' && strOutXML[i] <= '9')
                    {
                        for (int j = i; j < strOutXML.Length; j++)
                        {
                            if (strOutXML[j] == '\n')
                            {
                                Res = strOutXML.Substring(i, j - i);
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        MsgLabel.Text = " ";
                        MsgLabel.Text = "Total Num of Card is " + Res;
                        break;
                    }
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //Modify CardInfo
        private void CardModifyBtn_Click(object sender, EventArgs e)
        {
            String strTemp = "/ISAPI/AccessControl/CardInfo/Modify?format=json";
            String m_strInputXml = "{\"CardInfo\":{\"employeeNo\":\"" + EmployeeNoBox.Text +
                "\",\"cardNo\":\"" + CardNoBox.Text +
                "\",\"cardType\":\"" + CardTypeBox.SelectedItem.ToString() + "\"}}";
            int OutBufferLen = 1024;
            IntPtr ptrCfg = IntPtr.Zero;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            
          

            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = "";
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (rs.statusString.Equals("ok"))
                {
                    MessageBox.Show("Modify Card Succ!");
                }
                else
                {
                    MessageBox.Show(rs.statusString);
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }       
    }
}
