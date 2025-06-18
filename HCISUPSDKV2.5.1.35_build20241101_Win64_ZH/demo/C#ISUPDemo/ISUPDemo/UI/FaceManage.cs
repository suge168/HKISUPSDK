using ISUPDemo.Public;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ISUPDemo.UI
{
    public partial class FaceManage : Form
    {
     
        public class ResponseStatus
        {
            public string requestURL { get; set; }
            public string statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public string errorCode { get; set; }
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
        public class FaceLib
        {
            public string requestURL { get; set; }
            public int statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
            public string faceLibType { get; set; }
            public string name { get; set; }
            public string customInfo { get; set; }
        }
        public class MatchListItem
        {
            public string FPID { get; set; }
            public string faceURL { get; set; }
            public string name { get; set; }
            public string modelData { get; set; }
        }
        public class Root
        {
            public string requestURL { get; set; }
            public int statusCode { get; set; }
            public string statusString { get; set; }
            public string subStatusCode { get; set; }
            public string responseStatusStrg { get; set; }
            public int numOfMatches { get; set; }
            public int totalMatches { get; set; }
            public List<MatchListItem> MatchList { get; set; }
        }

        public HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        string Method = string.Empty;
        public byte[] m_szInputBuffer = new byte[1500];
        public byte[] m_szOutBuffer = new byte[1024 * 10];
        public byte[] m_szUrl = new byte[1024];
        IntPtr ptrCfg = IntPtr.Zero;
        private int m_iDeviceIndex;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        public int m_lUserID;
        string FacePicUrl = string.Empty;
        String PublicPicPath = String.Empty;
        public static int EqualCount = 0;
        public HCEHomePublic.NET_EHOME_IPADDRESS m_struPictureSever;       //图片服务器地址

        public FaceManage()
        {
            InitializeComponent();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
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
        
        //Get Single Device Face
        private void FaceGetBtn_Click(object sender, EventArgs e)
        {
            String strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            String m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + Face_EmpNoBox.Text + "\"}]}}";

            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (Face_EmpNoBox.Text == string.Empty)
            {
                MessageBox.Show("Please Input Employee NO");
                return;
            }
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
            if (strOutXML != string.Empty)
            {
                UserInfoSearchRoot us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(strOutXML);
                if (0 == us.UserInfoSearch.totalMatches)
                {
                    MessageBox.Show("Employee No doesn't found!");
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
         
            strTemp = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";
            m_strInputXml = "{\"searchResultPosition\":0,\"maxResults\":30,\"faceLibType\":\"" + Face_LibType.SelectedItem.ToString() +
                "\",\"FDID\":\"" + Face_DataBaseNo.Text +
                "\",\"FPID\":\"" + Face_EmpNoBox.Text + "\"}";

            OutBufferLen = 30*1024;
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
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                Root rt = JsonConvert.DeserializeObject<Root>(strOutXML);
                if (rt.statusCode == 1)
                {
                    if (rt.totalMatches == 1)
                    {
                        string picData = string.Empty;
                        foreach (MatchListItem item in rt.MatchList)
                        {
                            if (FacepictureBox.Image != null)
                            {
                                FacepictureBox.Image.Dispose();
                                FacepictureBox.Image = null;
                            }
                            string FacePicUrl = item.faceURL;
                            if (FacePicUrl == string.Empty)
                            {
                                break;
                            }
                            string KMSPath ="/kms/services/rest/dataInfoService/downloadFile?id=";
                            string CloudPath = "/pic";
                            string BaseURL = "C:/StorageServer/Storage/";
                            if (FacePicUrl.Contains(KMSPath))
                            {
                                 BaseURL = "http://" + new string(m_struPictureSever.szIP).TrimEnd('\0') + ":" + m_struPictureSever.wPort + FacePicUrl;
                                 System.Net.WebClient getPicWC = new System.Net.WebClient();
                                 byte[] pictureData = getPicWC.DownloadData(BaseURL);
                                 System.IO.MemoryStream picStream = new System.IO.MemoryStream(pictureData);
                                 FacepictureBox.Image = Image.FromStream(picStream);
                                 picStream.Close();
                                 FaceText.Text = FacePicUrl;
                            }
                            else if (FacePicUrl.Contains(CloudPath))
                            {
                                FacePicUrl = FacePicUrl.Substring(5, FacePicUrl.Length - 5);
                                BaseURL += FacePicUrl;
                                if (File.Exists(BaseURL))
                                {
                                    FacepictureBox.Image = Image.FromFile(BaseURL);
                                    FaceText.Text = FacePicUrl;
                                }
                                else
                                {
                                    MessageBox.Show("Get Face Pic Error");
                                } 
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Face Picture isn't found!");
                    }
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
     
        private void FaceCaptureBtn_Click(object sender, EventArgs e)
        {
            String strTemp = "/ISAPI/AccessControl/CaptureFaceData";
            String m_strInputXml = "<CaptureFaceDataCond version=\"2.0\" xmlns=\"http://www.isapi.org/ver20/XMLSchema\"><captureInfrared>false</captureInfrared><dataType>url</dataType></CaptureFaceDataCond>";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 5* 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Capture Face Failed: "+HCEHomeCMS.NET_ECMS_GetLastError());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            MessageBox.Show("Capture Accomplished!");
            return;
        }

        private void FaceSetBtn_Click(object sender, EventArgs e)
        {
            if (Face_EmpNoBox.Text == string.Empty)
            {
                MessageBox.Show("Please Input Employee NO");
                return;
            }
            String strTemp = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            String m_strInputXml = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + Face_EmpNoBox.Text + "\"}]}}";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 2* 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + Marshal.GetLastWin32Error());
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
                UserInfoSearchRoot us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(strOutXML);
                if (0 == us.UserInfoSearch.totalMatches)
                {
                    MessageBox.Show("Please Create Employee First");
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);

            strTemp = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";
            m_strInputXml = "{\"searchResultPosition\":0,\"maxResults\":30,\"faceLibType\":\"" + Face_LibType.SelectedItem.ToString() +
                "\",\"FDID\":\"" + Face_DataBaseNo.Text +
                "\",\"FPID\":\"" + Face_EmpNoBox.Text + "\"}";
           
            OutBufferLen = 5 * 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);

            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                Root rt = JsonConvert.DeserializeObject<Root>(strOutXML);
                if (rt.statusCode == 1)
                {
                    if (rt.totalMatches != 0)
                    {
                        MessageBox.Show("This Employee has Record Face Info");
                        Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                        Marshal.FreeHGlobal(struPTXML.pInBuffer);
                        Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                        Marshal.FreeHGlobal(ptrCfg);
                        return;
                    }
                }
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
            }
            strTemp = "/ISAPI/Intelligent/FDLib/FaceDataRecord?format=json";
            string FPID = Face_EmpNoBox.Text;
            string bornTime = "2004-05-03";
            string FaceUrl = FaceText.Text;
             m_strInputXml = "{\"faceURL\":\"" + FaceUrl +
                "\",\"faceLibType\":\"" + Face_LibType.SelectedItem.ToString() +
                "\",\"FDID\":\"" + Face_DataBaseNo.Text + "\",\"name\":\"" + TextName.Text + "\",\"FPID\":\"" +
                FPID + "\",\"bornTime\":\"" + bornTime + "\"}";
           
             ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Set Face Error: " + HCEHomeCMS.NET_ECMS_GetLastError());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (rs.statusCode.Equals("1"))
                {
                    MessageBox.Show("Face SetUp Success");
                }
                else {
                    MessageBox.Show("Face SetUp Error: " + rs.statusString);
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //Delete Face From FaceLib
        private void FaceDeleteBtn_Click(object sender, EventArgs e)
        {
            if (Face_EmpNoBox.Text == string.Empty)
            {
                MessageBox.Show("Please Input Employee NO");
                return;
            }
            string strTemp = "/ISAPI/Intelligent/FDLib/FDSearch/Delete?format=json&FDID=" +
                Face_DataBaseNo.Text + "&faceLibType=" + Face_LibType.SelectedItem.ToString();
            string m_strInputXml = "{\"FPID\":[{\"value\":\"" + Face_EmpNoBox.Text + "\"}]}";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
            {
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            string strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            if (strOutXML != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(strOutXML);
                if (rs.statusCode.Equals("1"))
                {
                    MessageBox.Show("Delete Picture Succ!");
                }
                else
                {
                    MessageBox.Show("Delete face data failed! Error code:" + rs.subStatusCode);
                }
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //Clear Face Image
        private void FaceClearBtn_Click(object sender, EventArgs e)
        {
            if (FacepictureBox.Image != null)
            {
                FacepictureBox.Image.Dispose();
                FacepictureBox.Image = null;
                FaceText.Text = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String strTemp = "/ISAPI/AccessControl/CaptureFaceData/Progress";
            String m_strInputXml = "";
            IntPtr ptrCfg = IntPtr.Zero;
            int OutBufferLen = 1024;
            ConfigMethod(ref ptrCfg, strTemp, m_strInputXml, OutBufferLen);
            if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
            {
                MessageBox.Show("Error: " + Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                Marshal.FreeHGlobal(struPTXML.pInBuffer);
                Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            String strOutXML = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            string FirstStr = "<faceDataUrl>";
            string LastStr = "</faceDataUrl";
            int FirstIndex = strOutXML.IndexOf("<faceDataUrl>");
            int LastIndex = strOutXML.IndexOf("</faceDataUrl>");
            int SubLen = LastIndex - FirstIndex - LastStr.Length;
            if (SubLen < 0)
            {
                MessageBox.Show("Capture Invaild Pic");
                return;
            }
            String FaceUrl = strOutXML.Substring(FirstIndex + FirstStr.Length, SubLen);
            FaceText.Text = FaceUrl;
            String DirPath = "C:\\StorageServer\\Storage\\";
            string KMSPath = "/kms/services/rest/dataInfoService/downloadFile?id=";
            string CloudPath = "/pic";
            if (FaceUrl.Contains(KMSPath))
            {
                int Length = KMSPath.Length;
                FaceUrl = FaceUrl.Substring(Length, FaceUrl.Length - Length);
            }
            else if (FaceUrl.Contains(CloudPath))
            {
                FaceUrl = FaceUrl.Substring(5, FaceUrl.Length - 5);
            }
            DirPath += FaceUrl;
            if (File.Exists(DirPath))
            {
                FacepictureBox.Image = Image.FromFile(DirPath);
            }
            else
            {
                MessageBox.Show("PIC Don't Existed");
            }
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }
        //Quick Sort Algorithm
        public int BaseSort(ref FileInfo[] FileArr, int Low, int High)
        {
            FileInfo KeyFile = FileArr[Low];
            DateTime KeyTime = KeyFile.LastWriteTime;
            while (Low < High)
            {
                DateTime T2 = FileArr[High].LastWriteTime;
                DateTime T1 = FileArr[Low].LastWriteTime;
                while ((KeyTime.CompareTo(T2) <= 0) && Low < High)
                {
                    --High;
                } 
                FileArr[Low] = FileArr[High];
                while ((KeyTime.CompareTo(T1) >= 0) && Low < High)
                    ++Low;
                FileArr[High] = FileArr[Low];
            }
            FileArr[Low] = KeyFile;
            return High;
        }
        public void QuickSort(ref FileInfo[] FileArr, int Low, int High)
        {
            if (Low >= High)
                return;
            int Index = BaseSort(ref FileArr, Low, High);
            QuickSort(ref FileArr, Low, Index - 1);
            QuickSort(ref FileArr, Index + 1, High);
        }
    }
}
