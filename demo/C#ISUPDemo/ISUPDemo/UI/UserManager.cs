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
using System.Globalization;
using ISUPDemo.Public;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace ISUPDemo.UI
{
    public partial class UserManager : Form
    {
        public class UserInfoCount
        {
            public string userNumber { get; set; }
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
        private int m_iDeviceIndex;
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private DeviceLogList g_formList = DeviceLogList.Instance();
        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String Method = String.Empty;
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];
        IntPtr ptrCfg = IntPtr.Zero;
        String m_strInputXml = String.Empty;
        String strTemp = String.Empty;
        public UserManager()
        {
            InitializeComponent();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
           
        }
        public void ConfigMethod(String strTemp, String m_strInputXml,int OutBufferLen)
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
                Marshal.StructureToPtr(struPTXML, ptrCfg, true);
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
            string EmployeeNo = textBoxEmployeeNo.Text;
            int OutBufferLen = 0;
            if (EmployeeNo.Length != 0)
            {
                strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + EmployeeNo + "\"}]}}";
                OutBufferLen = 5*1024;
            }
            else
            {
                strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30}}";
                OutBufferLen = 30*1024;
            }
            ConfigMethod(strTemp, m_strInputXml, OutBufferLen);
           
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_PostPTXMLConfig");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_PostPTXMLConfig");
            String strOutXML = "";
            try
            {
                strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Memory Exception");
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                return;
            }
            listViewInfo.GridLines = true;

            if (strOutXML != string.Empty)
            {
                UserInfoSearchRoot us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(strOutXML);
                if (0 == us.UserInfoSearch.totalMatches)
                {
                    labelMsg.Text = "Employee No isn't found!";
                }
                else
                {
                    if (us.UserInfoSearch.totalMatches > 0)
                    {
                        int i = 1;
                        listViewInfo.Items.Clear();
                        foreach (UserInfoItem userInfo in us.UserInfoSearch.UserInfo)
                        {
                            ListViewItem Items = new ListViewItem();
                            Items.Text = Convert.ToString(i++);
                            Items.SubItems.Add(userInfo.employeeNo);
                            Items.SubItems.Add(userInfo.name);
                            Items.SubItems.Add(userInfo.userType);
                            Items.SubItems.Add(userInfo.doorRight);
                            Items.SubItems.Add(userInfo.belongGroup);
                            listViewInfo.Items.Add(Items);
                        }
                        labelMsg.Text = "Search employee data succ!";
                    }
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        private void btnPut_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
            m_strInputXml = "{\"UserInfo\":{\"employeeNo\":\"" + textBoxEmployeeNo.Text +
            "\",\"name\":\"" + textBoxName.Text +
            "\",\"userType\":\"normal\",\"Valid\":{\"enable\":true,\"beginTime\":\"2017-08-01T17:30:08\",\"endTime\":\"2020-08-01T17:30:08\"},\"doorRight\": \"1\",\"RightPlan\":[{\"doorNo\":1,\"planTemplateNo\":\"" + textBoxRightPlan.Text +  
            "\",\"userVerifyMode\":\"fpOrCard\"}]}}";
            int OutBufferLen = 5 * 1024;
            ConfigMethod(strTemp, m_strInputXml, OutBufferLen);

            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_PutPTXMLConfig");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_PutPTXMLConfig");
            String strOutXML = String.Empty;
            try
            {
                strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Memory Exception");
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                return;
            }
            if (strOutXML != string.Empty)
            {
                ListViewItem TempItem = new ListViewItem();
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (1 == rs.statusCode)
                {
                    ListViewItem Items = new ListViewItem();
                    TempItem.SubItems.Add(textBoxEmployeeNo.Text);
                    TempItem.SubItems.Add(textBoxName.Text);
                    TempItem.SubItems.Add("normal");
                    TempItem.SubItems.Add(textBoxRightPlan.Text);
                    int Count = listViewInfo.Items.Count;
                    TempItem.Text = Convert.ToString(Count + 1);
                    listViewInfo.Items.Add(TempItem);
                    labelMsg.Text = "Modify Employee Success";
                }
                else
                {
                    labelMsg.Text = "Set Employee Failed";
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        private void btnPost_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
            m_strInputXml = "{\"UserInfo\":{\"employeeNo\":\"" + textBoxEmployeeNo.Text +
           "\",\"name\":\"" + textBoxName.Text +
           "\",\"userType\":\"normal\",\"Valid\":{\"enable\":true,\"beginTime\":\"2017-08-01T17:30:08\",\"endTime\":\"2020-08-01T17:30:08\"},\"doorRight\": \"1\",\"RightPlan\":[{\"doorNo\":1,\"planTemplateNo\":\"" + textBoxRightPlan.Text + "\"}]}}";

            int OutBufferLen = 5 * 1024;
            ConfigMethod(strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_PutPTXMLConfig");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_PutPTXMLConfig");
            String strOutXML = String.Empty;

            try
            {
                strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Memory Exception");
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                return;
            }
            if (strOutXML != string.Empty)
            {
                string EmpNo = textBoxEmployeeNo.Text;
                string Name = textBoxName.Text;
                string RightPlan = textBoxRightPlan.Text;

                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (1 == rs.statusCode)
                {
                    ListViewItem TempItem = new ListViewItem();
                    ListViewItem Items = new ListViewItem();
                    TempItem.SubItems.Add(textBoxEmployeeNo.Text);
                    TempItem.SubItems.Add(textBoxName.Text);
                    TempItem.SubItems.Add("normal");
                    TempItem.SubItems.Add(textBoxRightPlan.Text);
                    int Count = listViewInfo.Items.Count;
                    if (Count == 0)
                    {
                        TempItem.Text = Convert.ToString(Count);
                        listViewInfo.Items.Add(TempItem);
                    }
                    else
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            if (listViewInfo.Items[i].SubItems[1].Text == EmpNo)
                            {
                                listViewInfo.Items[i].SubItems[1].Text = EmpNo;
                                listViewInfo.Items[i].SubItems[2].Text = Name;
                                listViewInfo.Items[i].SubItems[3].Text = "normal";
                                listViewInfo.Items[i].SubItems[4].Text = RightPlan;
                                listViewInfo.Items[i].SubItems[0].Text = Convert.ToString(i + 1);
                                break;
                            }
                        }
                    }
                    labelMsg.Text = "Modify Employee Success";
                }
                else
                {
                    labelMsg.Text = "Modify Employee Success " + rs.errorMsg;
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/UserInfo/Delete?format=json";
            m_strInputXml = "{\"UserInfoDelCond\":{\"EmployeeNoList\":[{\"employeeNo\":\"" + textBoxEmployeeNo.Text + "\"}]}}";
            int OutBufferLen = 1024;
            ConfigMethod(strTemp, m_strInputXml, OutBufferLen);
            try
            {
                if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_PutPTXMLConfig");
                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Null Pointer Exception");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Null Pointer Exception");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_PutPTXMLConfig");
            String strOutXML = String.Empty;
            try
            {
                strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Memory Exception");
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                return;
            }

            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (1 == rs.statusCode)
                {
                    ListViewItem TempItem = new ListViewItem();
                    string EmpNo = textBoxEmployeeNo.Text;
                    TempItem.SubItems.Add(textBoxEmployeeNo.Text);
                    TempItem.SubItems.Add(textBoxName.Text);
                    TempItem.SubItems.Add("normal");
                    TempItem.SubItems.Add(textBoxRightPlan.Text);
                    ListViewItem Items = new ListViewItem();
                    int Count = listViewInfo.Items.Count;
                    if (Count == 1)
                    {
                        listViewInfo.Items.RemoveAt(0);
                    }
                    else
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            if (listViewInfo.Items[i].SubItems[1].Text == EmpNo)
                            {
                                listViewInfo.Items.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    textBoxName.Clear();
                    textBoxEmployeeNo.Clear();
                    textBoxRightPlan.Clear();
                    labelMsg.Text = "Delete Employee Success";
                }
                else
                {
                    labelMsg.Text = "Delete Employee Falied: " + rs.errorMsg;
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //calculate Num of Employee
        private void btnCount_Click(object sender, EventArgs e)
        {
            strTemp = "/ISAPI/AccessControl/UserInfo/Count?format=json";
            int OutBufferLen = 1024;
            ConfigMethod(strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_GetPTXMLConfig");
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_GetPTXMLConfig");
            String strOutXML = String.Empty;
            try
            {
                strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Memory Exception");
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                return;
            }
            //Find userNumber from strOutXML，Show on the Window
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
                        labelMsg.Text = "Total Num of Employee: " + Res;
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

    }
}
