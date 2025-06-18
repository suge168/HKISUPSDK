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

using ISUPDemo;
using ISUPDemo.Public;
using ISUPDemo.Language;
using System.Threading;
using System.Runtime.InteropServices;

namespace ISUPDemo.UI
{
    public partial class Update : UserControl
    {
        public static int m_iLoginID;

        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String m_strInputXml;
        String m_strOutputXml;
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl = new byte[1024];


        private static DeviceLogList g_formList = DeviceLogList.Instance();
        private static DeviceTree g_deviceTree = DeviceTree.Instance();

        public Update()
        {
            InitializeComponent();
        }

        public void CheckInitParam()
        {
            int m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_iLoginID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

            Control.CheckForIllegalCrossThreadCalls = false;

            this.Refresh();
        }

        private void btnStartFTPUpdate_Click(object sender, EventArgs e)
        {
            HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM struXMLCfg = new HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM();

            string m_strInputXml = "<?xml version = \"1.0\" encoding = \"UTF-8\"?>\r\n<PPVSPMessage>\r\n<Version>4.0</Version>\r\n<Sequence>1</Sequence>\r\n<CommandType>REQUEST</CommandType>\r\n" +
            "<Method>CONTROL</Method>\r\n<Command>UPDATE</Command>\r\n<Params>\r\n<FTPServerIP>%" + csFTPServerIP + "</FTPServerIP>\r\n<FTPServerPort>" + csFTPServerPort + "</FTPServerPort>\r\n" +
            "<Account>" + csAccount + "</Account>\r\n<Password>" + csPassword + "</Password>\r\n<File>" + csUpgradePackage + "</File>\r\n</Params>\r\n</PPVSPMessage>/r/n";

            m_szInputBuffer = Encoding.UTF8.GetBytes(m_strInputXml);
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

            IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struXMLCfg));
            Marshal.StructureToPtr(struXMLCfg, ptrCfg, false);
            int dwSize = Marshal.SizeOf(typeof(HCEHomeCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM));

            string strTemp = "";
            if (!HCEHomeCMS.NET_ECMS_XMLRemoteControl(m_iLoginID, ptrCfg, dwSize))
            {
                strTemp = Marshal.PtrToStringAnsi(struXMLCfg.lpOutBuffer, (int)struXMLCfg.dwOutBufferSize);
            }
            else
            {
                strTemp = Marshal.PtrToStringAnsi(struXMLCfg.lpStatusBuffer, (int)struXMLCfg.dwStatusBufferSize);
            }
            Marshal.FreeHGlobal(struXMLCfg.lpInbuffer);
            Marshal.FreeHGlobal(struXMLCfg.lpStatusBuffer);
            Marshal.FreeHGlobal(ptrCfg);
        }

    }
}

