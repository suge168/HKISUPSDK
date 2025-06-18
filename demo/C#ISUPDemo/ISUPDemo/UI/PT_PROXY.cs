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
using System.Threading;

using ISUPDemo.Public;

namespace ISUPDemo
{
    public partial class PT_PROXY : Form
    {
        public Int32 m_lListenSDK = -1;

        public PT_PROXY()
        {
            InitializeComponent();
        }

        private void btnStartListen_Click(object sender, EventArgs e)
        {
            if (textBoxListeningIP.Text.Length == 0 || textBoxListeningPort.Text.Length == 0)
            {
                MessageBox.Show("IP and Port can't be empty!");
                return;
            }
            if (m_lListenSDK > -1)
            {
                MessageBox.Show("the proxy has been open!");
                return;
            }

            HCEHomePublic.NET_EHOME_PT_PARAM m_struCfg = new HCEHomePublic.NET_EHOME_PT_PARAM();
            m_struCfg.Init();
            string strIp = textBoxListeningIP.Text;
            strIp.CopyTo(0, m_struCfg.struIP.szIP, 0, strIp.Length);
            Int16.TryParse(textBoxListeningPort.Text, out m_struCfg.struIP.wPort);
            m_struCfg.byProtocolType = 0; //0~TCP
            m_struCfg.byProxyType = (byte)HCEHomePublic.NET_CMS_ENUM_PROXY_TYPE.ENUM_PROXY_TYPE_NETSDK;
            m_lListenSDK = HCEHomeCMS.NET_ECMS_StartListenProxy(ref m_struCfg);
            if (m_lListenSDK < 0)
            {
                MessageBox.Show("Start NetSDK PT proxy failed!");
                return;
            }
            else
            {
                MessageBox.Show("Start NetSDK PT proxy Success!");
            }
        }

        private void btnStopListen_Click(object sender, EventArgs e)
        {
            if (m_lListenSDK > 0)
            {
                if (HCEHomeCMS.NET_ECMS_StopListenProxy(m_lListenSDK, (int)HCEHomePublic.NET_CMS_ENUM_PROXY_TYPE.ENUM_PROXY_TYPE_NETSDK))
                {
                    m_lListenSDK = -1;
                    MessageBox.Show("Start NetSDK PT proxy Success!");
                }
                else
                {
                    MessageBox.Show("Start NetSDK PT proxy fail!");
                }
            }
        }
    }
}
