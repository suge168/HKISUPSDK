using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISUPDemo.UI
{
    public partial class ServerConfiguration : Form
    {

        public bool m_bEnableISUPDemoXML = true;
        public string m_csMQTTPort = "0";

        public HCEHomeCMS.NET_EHOME_SERVER_INFO_V50 struServInfo = new HCEHomeCMS.NET_EHOME_SERVER_INFO_V50();


        public ServerConfiguration()
        {
            InitializeComponent();

            comboBoxAlarmServerType.Items.Clear();
            comboBoxAlarmServerType.Items.Add("UDP");
            comboBoxAlarmServerType.Items.Add("TCP");
            comboBoxAlarmServerType.Items.Add("MQTT");

            comboBoxAlarmServerType.Text = "MQTT".ToString();
            comboBoxPicServerType.Text = "CloudStorage".ToString();

            try
            {
                string hostName = Dns.GetHostName();
                IPHostEntry iPHostEntry = Dns.GetHostEntry(hostName);
                var addressV = iPHostEntry.AddressList.FirstOrDefault(q => q.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);//ip4地址
                if (addressV != null)
                {
                    textBoxUDPIP.Text = addressV.ToString();
                    textBoxTCPIP.Text = addressV.ToString();
                    textBoxMQTTIP.Text = addressV.ToString();
                    textBoxPicServerIP.Text = addressV.ToString();
                    textBoxNTPIP.Text = addressV.ToString();
                }
                return;  
            }
            catch (Exception ex)
            {
                return;
            }
        }

        void ClearCharArr(ref char[] source)
        {
            for (int i = 0; i < source.Length; ++i)
            {
                source[i] = '\0';
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            m_bEnableISUPDemoXML = false;


            HCEHomeCMS.NET_EHOME_IPADDRESS struTCPAlarmServer = new HCEHomeCMS.NET_EHOME_IPADDRESS();
            struServInfo.Init();
            struTCPAlarmServer.Init();

            ClearCharArr(ref struServInfo.struPictureSever.szIP);
            ClearCharArr(ref struServInfo.struTCPAlarmSever.szIP);
            ClearCharArr(ref struServInfo.struUDPAlarmSever.szIP);

            if (comboBoxAlarmServerType.Text == "UDP")
            {
                struServInfo.dwAlarmServerType = 0;
                char[] szIP = textBoxUDPIP.Text.ToString().ToCharArray();
                Array.Copy(szIP, struServInfo.struUDPAlarmSever.szIP, szIP.Length);     //IP地址后面增加结束符'\0',否则传到底层dll库时会变成“地址00000000000”形式
                struServInfo.struUDPAlarmSever.wPort = Convert.ToInt16(textBoxUdpPort.Text.ToString());
            }
            else if (comboBoxAlarmServerType.Text == "TCP")
            {
                struServInfo.dwAlarmServerType = 1;
                char[] szIP = textBoxTCPIP.Text.ToString().ToCharArray();
                Array.Copy(szIP, struServInfo.struTCPAlarmSever.szIP, szIP.Length); 
                struServInfo.struTCPAlarmSever.wPort = Convert.ToInt16(textBoxTcpPort.Text.ToString());
            }
            else if (comboBoxAlarmServerType.Text == "MQTT")
            {
                struServInfo.dwAlarmServerType = 2;
                char[] szIP = textBoxMQTTIP.Text.ToString().ToCharArray();
                Array.Copy(szIP, struServInfo.struTCPAlarmSever.szIP, szIP.Length); 
                struServInfo.struTCPAlarmSever.wPort = Convert.ToInt16(textBoxMQTTPort.Text.ToString());
               // m_csMQTTPort = textBoxMQTTPort.Text.ToString();
            }

            if (comboBoxPicServerType.Text == "VRB")
            {
                struServInfo.dwPicServerType = 1;
            }
            else if (comboBoxPicServerType.Text == "Tomcat")
            {
                struServInfo.dwPicServerType = 0;
            }
            else if (comboBoxPicServerType.Text == "KMS")
            {
                struServInfo.dwPicServerType = 3;
            }
            else if (comboBoxPicServerType.Text == "CloudStorage")
            {
                struServInfo.dwPicServerType = 2;
            }
            
            char[] szPicServIP = textBoxPicServerIP.Text.ToString().ToCharArray();
            Array.Copy(szPicServIP, struServInfo.struPictureSever.szIP, szPicServIP.Length); 
            struServInfo.struPictureSever.wPort = Convert.ToInt16(textBoxPicServerPort.Text.ToString());
        }

    }
}
