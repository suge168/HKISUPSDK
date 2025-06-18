using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

using EHomeDemo;
using EHomeDemo.Public;
using EHomeDemo.Language;
namespace EHomeDemo
{
    public partial class AudioTalk : UserControl
    {
        public HCEHomeStream.VOICETALK_DATA_CB AudioTalkStreamCallback_Func = null;      //语音数据回调委托
        public HCEHomeStream.VOICETALK_NEWLINK_CB onDeviceAudioTalkConnectCB_Func = null;   //设备上线回调委托
        public HCEHomeCMS.fVoiceDataCallBack g_fVoiceDataCallBack_Func = null;
        public HCEHomeCMS.NET_EHOME_VOICETALK_PARA m_struVoiceTalkPara;
        public int m_nLocalType = 0;
        public Int32 m_lVoiceTalkHandle;
        public Int32 m_lListenHandle;
        public static String m_csIPAddr;
        public static Int16 m_lPort;

        private DeviceLogList g_formList = DeviceLogList.Instance();
        private DeviceTree g_deviceTree = DeviceTree.Instance();

        //音频输入
        private SoundIn m_SoundIn = new SoundIn();
        //音频输出
        private WaveSound m_SoundOut = new WaveSound();

        private Int32 m_lAudioTalkHandle;
        private Int32 m_iDeviceIndex;
        private int m_iSessionID;
        private byte[] m_pRenderBuf;     //buffer
        private int m_rIndexRV;          //data that has already been read
        private int m_ReceiveIndexRV;    //received data for m_pRenderBuf
        private int m_nBufNo;
        private bool m_bOpenPlayThread;
        private bool m_bCMSAudioTalk;
        private bool g_bStopAudioTalk;
        private bool m_bExitThread;
        private Thread m_hVoiceTransmit;
        public struct STRUCT_TALK_MR
        {
            public byte byFlag;
            public byte byAudioType;
            public byte byIndex;
            public byte res;
            public void Init()
            {
                byFlag = 0;
                byAudioType = 0;
                byIndex = 0;
                res = 0;
            }
        }

        public AudioTalk()
        {
            InitializeComponent();
            m_csIP.Text = m_csIPAddr;
            m_nPort.Text = "7500";
        }

        public void CheckInitParam()
        {
            String strInfo;
            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            uint iAuidoNum = DeviceTree.g_struDeviceInfo[iDeviceIndex].dwAudioNum;
            m_cmbAudioChannel.Items.Clear();
            m_cmbCBDataType.Items.Clear();
            for (int i = 0 ; i < iAuidoNum; i++)
            {
                strInfo= string.Format("channel {0}", i + 1);
                m_cmbAudioChannel.Items.Add(strInfo);
            }
            strInfo= string.Format("编码数据");
            m_cmbCBDataType.Items.Add(strInfo);
            strInfo = string.Format("PCM数据");
            m_cmbCBDataType.Items.Add(strInfo);
            this.Refresh();
        }

        public bool SendDataToDevice(byte[] buf, uint dwSize)
        {
            String strInfo;
            HCEHomeStream.NET_EHOME_VOICETALK_DATA struVoicTalkData = new HCEHomeStream.NET_EHOME_VOICETALK_DATA();

            struVoicTalkData.pSendBuf = buf;
            struVoicTalkData.dwDataLen = dwSize;
            strInfo = string.Format("NET_ESTREAM_SendVoiceTalkData, dwDataLen={0}", dwSize);
            g_formList.AddLog(m_iDeviceIndex, GlobalDefinition.OPERATION_SUCC_T, 1, strInfo);
            return (HCEHomeStream.NET_ESTREAM_SendVoiceTalkData(m_lAudioTalkHandle, ref struVoicTalkData/*NULL*/) > 0);
        }

        private void btnCMSStartTalk_Click(object sender, EventArgs e)
        {
            String strInfo;

            if (m_cmbCBDataType.SelectedIndex == 0)
            {
                m_struVoiceTalkPara.bNeedCBNoEncData = 0;
            }
            else
            {
                m_struVoiceTalkPara.bNeedCBNoEncData = 1;
            }

            //g_fVoiceDataCallBack_Func = new HCEHomeCMS.fVoiceDataCallBack(g_fVoiceDataCallBack);
            m_struVoiceTalkPara.pUser = this.Handle;
            m_struVoiceTalkPara.byVoiceTalk = 0;
            m_struVoiceTalkPara.cbVoiceDataCallBack = g_fVoiceDataCallBack_Func;

            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            int iLoginID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;
            uint uiVoiceChannel = (uint)(m_cmbAudioChannel.SelectedIndex + 1);

            m_lVoiceTalkHandle = HCEHomeCMS.NET_ECMS_StartVoiceTalk(iLoginID, uiVoiceChannel, ref m_struVoiceTalkPara);
            if (m_lVoiceTalkHandle < 0)
            {
                strInfo = string.Format("NET_ECMS_StartVoiceTalk Failed.ERR[{0}]", HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(iDeviceIndex, GlobalDefinition.OPERATION_FAIL_T, 1, "NET_ECMS_StartVoiceTalk");
                return;
            }
            btnCMSStartTalk.Enabled = false;
            btnCMSStopTalk.Enabled = true;
        }

        private void btnCMSStopTalk_Click(object sender, EventArgs e)
        {
            if (m_lVoiceTalkHandle < 0)
            {
                return;
            }
            if (HCEHomeCMS.NET_ECMS_StopVoiceTalk(m_lVoiceTalkHandle))
            {
                m_lVoiceTalkHandle = -1;
                btnCMSStartTalk.Enabled = true;
                btnCMSStopTalk.Enabled = true;
            }
        }

        private void btnStartListen_Click(object sender, EventArgs e)
        {
            String strInfo;

            HCEHomeStream.NET_EHOME_LISTEN_VOICETALK_CFG struListen = new HCEHomeStream.NET_EHOME_LISTEN_VOICETALK_CFG();
            //onDeviceAudioTalkConnectCB_Func = new HCEHomeStream.VOICETALK_NEWLINK_CB(onDeviceAudioTalkConnectCB);
            struListen.fnNewLinkCB = onDeviceAudioTalkConnectCB_Func;
            struListen.pUser = this.Handle;
            struListen.struIPAdress.szIP = new char[128];

            m_csIPAddr.CopyTo(0, struListen.struIPAdress.szIP, 0, m_csIPAddr.Length);
            struListen.struIPAdress.wPort = m_lPort;     //端口
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lListenHandle = HCEHomeStream.NET_ESTREAM_StartListenVoiceTalk(ref struListen);
            if (m_lListenHandle > -1)
            {
                strInfo = string.Format("NET_ESTREAM_StartListenAudioTalk, Error={0}", HCEHomeStream.NET_ESTREAM_GetLastError());
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, strInfo);
            }
            else
            {
                strInfo = string.Format("NET_ESTREAM_StartListenAudioTalk, Error=[%d]", HCEHomeStream.NET_ESTREAM_GetLastError());
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 2, strInfo);
            }
        }

        private void btnReqAudioTalk_Click(object sender, EventArgs e)
        {
            m_bCMSAudioTalk = false;

            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            int iLoginID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;
            HCEHomeCMS.NET_EHOME_VOICE_TALK_IN struVoiceTalkIn = new HCEHomeCMS.NET_EHOME_VOICE_TALK_IN();
            struVoiceTalkIn.dwVoiceChan = 1;//m_cmbVoiceChan.GetCurSel() + 1;
            struVoiceTalkIn.struStreamSever.wPort = m_lPort;
            struVoiceTalkIn.struStreamSever.szIP = new char[128];
            m_csIPAddr.CopyTo(0, struVoiceTalkIn.struStreamSever.szIP, 0, m_csIPAddr.Length);
            HCEHomeCMS.NET_EHOME_VOICE_TALK_OUT struVoiceTalkOut = new HCEHomeCMS.NET_EHOME_VOICE_TALK_OUT();

            if (!HCEHomeCMS.NET_ECMS_StartVoiceWithStmServer(iLoginID, ref struVoiceTalkIn, ref struVoiceTalkOut))
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_StartVoiceWithStmServer");
            }
            else
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_StartVoiceWithStmServer");
                m_iSessionID = struVoiceTalkOut.lSessionID;
            }
        }

        private void btnStartPushStream_Click(object sender, EventArgs e)
        {
            m_nLocalType = m_cmbLocalType.SelectedIndex;
            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            int iLoginID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;
            HCEHomeCMS.NET_EHOME_PUSHVOICE_IN struPushVoiceIn = new HCEHomeCMS.NET_EHOME_PUSHVOICE_IN();
            struPushVoiceIn.dwSize = (uint)Marshal.SizeOf(struPushVoiceIn);
            struPushVoiceIn.lSessionID = m_iSessionID;

            HCEHomeCMS.NET_EHOME_PUSHVOICE_OUT struPushVoiceOut = new HCEHomeCMS.NET_EHOME_PUSHVOICE_OUT();
            struPushVoiceOut.dwSize = (uint)Marshal.SizeOf(struPushVoiceOut);

            if (!HCEHomeCMS.NET_ECMS_StartPushVoiceStream(iLoginID, ref struPushVoiceIn, ref struPushVoiceOut))
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_StartPushVoiceStream");
            }
            else
            {
                g_bStopAudioTalk = false;
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_StartPushVoiceStream");
            }
        }

        private void btnStopListen_Click(object sender, EventArgs e)
        {
            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            int iLoginID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;

            if (HCEHomeStream.NET_ESTREAM_StopListenVoiceTalk(m_lListenHandle))
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ESTREAM_StopListenVoiceTalk");
            }
            else
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 2, "NET_ESTREAM_StopListenVoiceTalk");
            }
        }

        private void btnStopAudioTalk_Click(object sender, EventArgs e)
        {
            g_bStopAudioTalk = true;

            m_bExitThread = true;

            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            int iLoginID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;

            if (!HCEHomeCMS.NET_ECMS_StopVoiceTalkWithStmServer(iLoginID, m_iSessionID))
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_StopVoiceTalkWithStmServer");
            }
            else
            {
                g_bStopAudioTalk = false;
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_StopVoiceTalkWithStmServer");
            }

            HCEHomeStream.NET_ESTREAM_StopVoiceTalk(m_lAudioTalkHandle);

        }

        private void btnStartTransmit_Click(object sender, EventArgs e)
        {
            m_struVoiceTalkPara.pUser = this.Handle;
            m_struVoiceTalkPara.byVoiceTalk = 1;
            //m_struVoiceTalkPara.cbVoiceDataCallBack = g_fVoiceDataCallBack;
            int iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            int iLoginID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;
            uint uiVoiceChannel = (uint)(m_cmbAudioChannel.SelectedIndex + 1);
            m_lVoiceTalkHandle = HCEHomeCMS.NET_ECMS_StartVoiceTalk(iLoginID, uiVoiceChannel, ref m_struVoiceTalkPara);
            if (m_lVoiceTalkHandle < 0)
            {
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, "NET_ECMS_StartVoiceTalk");
                return;
            }
        }

        private void AudioTalk_Load(object sender, EventArgs e)
        {
            MultiLanguage.LoadLanguage(this);
        }
    }
}
