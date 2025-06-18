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

using EHomeDemo.Public;

namespace EHomeDemo
{
    public partial class GPSInfoSet : Form
    {
        public struct NET_EHOME_GPS_PARAM
        {
            public uint iInteralTime;
            public int iMaxSpeed;
            public int iMinSpeed;
        }

        public static String GPS_CONFIG_XML = "<GPSINFOPARA>\r\n<GPSTransInterval>{0}</GPSTransInterval>\r\n</GPSINFOPARA>\r\n";
        private int m_iDeviceIndex;
        private int m_lUserID;
        private byte[] m_szCommand = new byte[32];
        private byte[] m_szInputXml = new byte[HCEHomeCMS.MAX_EHOME_PROTOCOL_LEN];
        private byte[] m_szOutputXml = new byte[HCEHomeCMS.MAX_EHOME_PROTOCOL_LEN];
        private HCEHomeCMS.NET_EHOME_XML_CFG m_struCfg = new HCEHomeCMS.NET_EHOME_XML_CFG();
        private NET_EHOME_GPS_PARAM m_struGPSInfo = new NET_EHOME_GPS_PARAM();
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private DeviceLogList g_formList = DeviceLogList.Instance();



        public GPSInfoSet()
        {
            InitializeComponent();
            m_cmbInteral.SelectedText = "0";
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

        }

        private void btnGETGpsInfo_Click(object sender, EventArgs e)
        {
            String strTemp = "GETDEVICECONFIG";
            m_szCommand = Encoding.UTF8.GetBytes(strTemp); 
            strTemp = Encoding.UTF8.GetString(m_szCommand, 0, m_szCommand.Length);
            m_struCfg.pCmdBuf = Marshal.StringToBSTR(strTemp);                  //命令赋值 

            strTemp = null;
            strTemp = string.Format(HCEHomeCMS.CONFIG_GET_PARAM_XML, "GetGPSInfoPara", 0, 0, 0, 0);
            m_szInputXml = Encoding.UTF8.GetBytes(strTemp);
            strTemp = Encoding.UTF8.GetString(m_szInputXml, 0, m_szInputXml.Length);
            m_struCfg.pInBuf = Marshal.StringToBSTR(strTemp);                //输入指针赋值

            strTemp = "";
            m_szOutputXml = Encoding.UTF8.GetBytes(strTemp);
            strTemp = Encoding.UTF8.GetString(m_szOutputXml, 0, m_szOutputXml.Length);
            m_struCfg.pOutBuf = Marshal.StringToBSTR(strTemp);                //输出指针赋值

            m_struCfg.dwCmdLen = (uint)m_szCommand.Length;
            m_struCfg.dwInSize = (uint)m_szInputXml.Length;
            m_struCfg.dwOutSize = HCEHomeCMS.MAX_EHOME_PROTOCOL_LEN;
            m_struCfg.byRes = new byte[24];
            //需要将字符串字段转换成UTF-8
            // A2UTF8((char*)m_struCfg.pCmdBuf, (char*)m_struCfg.pCmdBuf, sizeof(m_szCommand), &m_struCfg.dwCmdLen);
            // A2UTF8((char*)m_struCfg.pInBuf, (char*)m_struCfg.pInBuf, MAX_EHOME_PROTOCOL_LEN, &m_struCfg.dwInSize);
            int dwSize = 0;

            IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(m_struCfg));
             Marshal.StructureToPtr(m_struCfg, ptrCfg, false);

            dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_XML_CFG));
            if (!HCEHomeCMS.NET_ECMS_XMLConfig(m_lUserID, ptrCfg, dwSize))
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_XMLConfig");
                Marshal.FreeBSTR(m_struCfg.pCmdBuf);
                Marshal.FreeBSTR(m_struCfg.pInBuf);
                Marshal.FreeBSTR(m_struCfg.pOutBuf);
                Marshal.FreeHGlobal(ptrCfg);
                return;
            }
            else
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "Get GPS Info");
            }
            CTinyXmlTrans xmlGPSInfo = new CTinyXmlTrans();
            strTemp = Marshal.PtrToStringAnsi(m_struCfg.pOutBuf);
            xmlGPSInfo.Parse(strTemp);
            if (xmlGPSInfo.FindElem("Params") && xmlGPSInfo.IntoElem())
            {
                if (xmlGPSInfo.FindElem("ConfigXML") && xmlGPSInfo.IntoElem())
                {
                    if (xmlGPSInfo.FindElem("GPSINFOPARA") && xmlGPSInfo.IntoElem())
                    {
                        IntPtr ptrTemp = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(uint)));
                        Marshal.StructureToPtr((uint)m_struGPSInfo.iInteralTime, ptrTemp, false);
                        m_struGPSInfo.iInteralTime = Convert.ToUInt32(xmlGPSInfo.GetAttributeValue("GPSTransInterval"));
                        //ConvertModel.ConvertSingleNodeData(ptrTemp, xmlGPSInfo, "GPSTransInterval", ConvertModel.XML_NODE_TYPE.NODE_STRING_TO_INT);
                        //ConvertSingleNodeData(&m_struGPSInfo.iMaxSpeed, xmlGPSInfo, "MaxSpeed", NODE_STRING_TO_INT);
                        //ConvertSingleNodeData(&m_struGPSInfo.iMinSpeed, xmlGPSInfo, "MinSpeed", NODE_STRING_TO_INT);
                    }
                }
            }

            Marshal.FreeBSTR(m_struCfg.pCmdBuf);
            Marshal.FreeBSTR(m_struCfg.pInBuf);
            Marshal.FreeBSTR(m_struCfg.pOutBuf);
            Marshal.FreeHGlobal(ptrCfg);
            m_cmbInteral.Text = Convert.ToString(m_struGPSInfo.iInteralTime);
           //m_iMaxSpeed.Text = Convert.ToString(m_struGPSInfo.iMaxSpeed);
           //m_iMinSpeed.Text = Convert.ToString(m_struGPSInfo.iMinSpeed);
        }

        private void btnSETGpsInfo_Click(object sender, EventArgs e)
        {

            // TODO:  在此添加控件通知处理程序代码
            m_struGPSInfo.iInteralTime = Convert.ToUInt32(m_cmbInteral.Text);
            //m_struGPSInfo.iMaxSpeed = Convert.ToInt32(m_iMaxSpeed.Text);
            //m_struGPSInfo.iMinSpeed = Convert.ToInt32(m_iMinSpeed.Text);

            String strTemp = "SETDEVICECONFIG";
            m_szCommand = Encoding.UTF8.GetBytes(strTemp);
            strTemp = Encoding.UTF8.GetString(m_szCommand, 0, m_szCommand.Length);
            m_struCfg.pCmdBuf = Marshal.StringToBSTR(strTemp);                       //命令赋值

            strTemp = null;
            String strInput;
            strInput = string.Format(GPS_CONFIG_XML, m_struGPSInfo.iInteralTime);
            strTemp = string.Format(HCEHomeCMS.CONFIG_SET_PARAM_XML, "SetGPSInfoPara", 0, 0, 0, 0, strInput);
            m_szInputXml = Encoding.UTF8.GetBytes(strTemp);
            strTemp = Encoding.UTF8.GetString(m_szInputXml, 0, m_szInputXml.Length);     //输入指针赋值
            m_struCfg.pInBuf = Marshal.StringToBSTR(strTemp);

            strTemp = "";
            m_szOutputXml = Encoding.UTF8.GetBytes(strTemp);
            strTemp = Encoding.UTF8.GetString(m_szOutputXml, 0, m_szOutputXml.Length);
            m_struCfg.pOutBuf = Marshal.StringToBSTR(strTemp);                //输出指针赋值

            m_struCfg.dwCmdLen = (uint)m_szCommand.Length;
            m_struCfg.dwInSize = (uint)m_szInputXml.Length;
            m_struCfg.dwOutSize = HCEHomeCMS.MAX_EHOME_PROTOCOL_LEN;
            m_struCfg.byRes = new byte[24];
            //需要将字符串字段转换成UTF-8
            //A2UTF8((char*)m_struCfg.pCmdBuf, (char*)m_struCfg.pCmdBuf, sizeof(m_szCommand), &m_struCfg.dwCmdLen);
            //A2UTF8((char*)m_struCfg.pInBuf, (char*)m_struCfg.pInBuf, MAX_EHOME_PROTOCOL_LEN, &m_struCfg.dwInSize);

            IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(m_struCfg));
            Marshal.StructureToPtr(m_struCfg, ptrCfg, false);

            if (!HCEHomeCMS.NET_ECMS_XMLConfig(m_lUserID, ptrCfg, Marshal.SizeOf(m_struCfg)))
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_XMLConfig");
                return;
            }
            else
            {
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "Set GPS Info");
            }
        }
    }
}
