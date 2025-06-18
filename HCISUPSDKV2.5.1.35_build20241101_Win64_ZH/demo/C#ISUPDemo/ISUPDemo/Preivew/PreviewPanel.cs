using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


//每个PreviewPanel对应一个设备
namespace ISUPDemo.Public
{
    public partial class PreviewPanel : UserControl
    {
        public GlobalDefinition.LOCAL_DEVICE_INFO[] g_struDeviceInfo = new GlobalDefinition.LOCAL_DEVICE_INFO[GlobalDefinition.MAX_DEVICES];
        public static PreviewPanel[]       g_PreviewPanel = new PreviewPanel[512];
        //日志实例
        private DeviceLogList g_formList = DeviceLogList.Instance();

        public IntPtr m_hWnd;
        public IntPtr m_hWnd1;       //预览窗口1句柄
        public IntPtr m_hWnd2;       //预览窗口2句柄
        public IntPtr m_hWnd3;       //预览窗口3句柄
        public IntPtr m_hWnd4;       //预览窗口4句柄
        public IntPtr m_hWndFather; //预览窗口父窗口句柄
        public int    m_lPort;
        public int    m_iChanelType;
        public int    m_iDeviceIndex;
        public int    m_iChanIndex;
        public int    m_iSessionID;
        public int    m_lPlayHandle;
        public int    m_iWndIndex;

        public Panel m_CurPreviewPanel; //当前播放窗口
        public String m_cslocalIP;     //本机IP地址
        public int m_nPort;            //本机端口号

        public bool m_bPlaying;         //是否正在播放

        public PreviewPanel()
        {
            InitializeComponent();
            //for (int i = 0; i < GlobalDefinition.MAX_DEVICES; i++)
            //{
            //    g_struDeviceInfo[i].Init();
            //    for (int j = 0; j < GlobalDefinition.MAX_CHAN_NUM_DEMO; j++)
            //    {
            //        g_struDeviceInfo[i].struChanInfo[j].Init();
            //    }
            //}
            
            m_bPlaying = false;
            //m_hWnd = this.
        }

        public static PreviewPanel[] Instance()
        {
            for (int i = 0; i < GlobalDefinition.MAX_OUTPUTS; i++)
            {
                g_PreviewPanel[i] = new PreviewPanel();
            }
            
            return g_PreviewPanel;
        }

      
        public bool InputStreamData(byte byDataType, IntPtr pBuffer, int iDataLen)
        {

            if(IntPtr.Zero == pBuffer)
            {
                return false;
            }

            if (1 == byDataType)
            {

                if (!PlayCtrl.PlayM4_GetPort(ref m_lPort))
                {
                    StopPlay();
                    return false;
                }
                if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_lPort, PlayCtrl.STREAME_REALTIME/*实时流*/ ))
                {
                    StopPlay();
                    return false;
                }
                // 先输入头,前40个字节
                if (!PlayCtrl.PlayM4_OpenStream(m_lPort, pBuffer, (uint)iDataLen, (uint)2 * 1024 * 1024/*缓冲区*/ ))
                {
                    StopPlay();
                    return false;
                }
                if (!PlayCtrl.PlayM4_Play(m_lPort, m_hWnd))
                {
                    StopPlay();
                    return false;
                }
            }
            else
            {
                int time = 1000;
                while (time > 0)
                {
                    //fileTest.Write(pBuffer, iDataLen);
                    bool bRet = PlayCtrl.PlayM4_InputData(m_lPort, pBuffer, (uint)iDataLen);

                    if (!bRet)
                    {
                        // g_pMainDlg->AddLog(0, OPERATION_FAIL_T, 2, "PlayM4_InputData failed");
                        //Sleep(5);
                        time--;
                        continue;
                    }
                    break;
                }
                //if(!PlayM4_InputData(m_lPort, (unsigned char *)pBuffer, (DWORD)iDataLen))
                //{
                //   g_pMainDlg->AddLog(0, OPERATION_FAIL_T, 2, "PlayM4_InputData failed");
                //}
            }// End of while( TRUE )
            return true;
        }

        //开始预览
        public bool StartPlay(int iDeviceIndex, int iChannelIndex)
        {
            //String str = null;
            m_iDeviceIndex = iDeviceIndex;
            m_iChanIndex = iChannelIndex;
            int lUserID = DeviceTree.g_struDeviceInfo[iDeviceIndex].iLoginID;

            if (DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].bPlay)
            {
                if (!StopPlay())
                {
                    g_formList.AddLog(iDeviceIndex, GlobalDefinition.OPERATION_FAIL_T, 0, "StopPlay Failed");
                    return false;
                }
            }

            HCEHomeCMS.NET_EHOME_PREVIEWINFO_OUT struParamOut = new HCEHomeCMS.NET_EHOME_PREVIEWINFO_OUT();
            if (DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].iChanType == GlobalDefinition.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_ZERO)
            {
                HCEHomeCMS.NET_EHOME_PREVIEWINFO_IN_V11 struParamIn = new HCEHomeCMS.NET_EHOME_PREVIEWINFO_IN_V11();
                struParamIn.Init();
                struParamIn.byDelayPreview = 1;//是否延时取流

                struParamIn.dwLinkMode = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].dwLinkMode;
                struParamIn.struStreamSever.wPort = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].struIP.wPort;
                DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].struIP.szIP.CopyTo(struParamIn.struStreamSever.szIP, 0);
                struParamIn.dwStreamType = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].dwStreamType;
                struParamIn.iChannel = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].iChannelNO; //零通道通道号

                if (!HCEHomeCMS.NET_ECMS_StartGetRealStreamV11(lUserID, ref struParamIn, ref struParamOut))
                {
                    //m_hChanItem = NULL;
                    //memset(&m_struChanInfo, 0, sizeof(STRU_CHANNEL_INFO));
                    //CleanPlayParam();
                    string str = string.Format("NET_ECMS_StartGetRealStreamV11 err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                    return false;
                }
            }
            else
            {
                HCEHomeCMS.NET_EHOME_PREVIEWINFO_IN struParamIn = new HCEHomeCMS.NET_EHOME_PREVIEWINFO_IN();
                struParamIn.Init();
                struParamIn.dwLinkMode = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].dwLinkMode;
                struParamIn.struStreamSever.wPort = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].struIP.wPort;
                DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].struIP.szIP.CopyTo(struParamIn.struStreamSever.szIP, 0);
                
                m_cslocalIP.CopyTo(0, struParamIn.struStreamSever.szIP, 0, m_cslocalIP.Length);
                
                struParamIn.struStreamSever.wPort = 8000;

                struParamIn.dwStreamType = DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].dwStreamType;
                struParamIn.iChannel = m_iChanIndex+1;

                if (!HCEHomeCMS.NET_ECMS_StartGetRealStream(lUserID, ref struParamIn, ref struParamOut))
                {
                    //m_hChanItem = NULL;
                    //memset(&m_struChanInfo, 0, sizeof(STRU_CHANNEL_INFO));
                    //CleanPlayParam();
                    string str = string.Format("NET_ECMS_StartGetRealStream err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                    return false;
                }
            }


            HCEHomeCMS.NET_EHOME_PUSHSTREAM_IN struPushIn = new HCEHomeCMS.NET_EHOME_PUSHSTREAM_IN();
            struPushIn.Init();
            HCEHomeCMS.NET_EHOME_PUSHSTREAM_OUT struPushOut = new HCEHomeCMS.NET_EHOME_PUSHSTREAM_OUT();
            struPushOut.Init();
            struPushIn.dwSize = Marshal.SizeOf(struPushIn);
            struPushIn.lSessionID = struParamOut.lSessionID;
            if (!HCEHomeCMS.NET_ECMS_StartPushRealStream(lUserID, ref struPushIn, ref struPushOut))
            {
                string str = string.Format("NET_ECMS_StartPushRealStream err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                //这里老的设备可能不支持NET_ECMS_StartPushRealStream, 不需要预览请求推流命令 所以这里不返回错误
            }
            DeviceTree.g_struDeviceInfo[iDeviceIndex].struChanInfo[m_iChanIndex].bPlay = true;
            m_iSessionID = struParamOut.lSessionID;
            m_bPlaying = true;
            return true;
        }
        

        //停止预览
        public bool StopPlay()
        {
           
            bool bRet = true;
            String strInfo;
            Int32 iUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;

            //释放CMS预览请求资源
            if (!HCEHomeCMS.NET_ECMS_StopGetRealStream(iUserID, m_iSessionID))//m_iSessionID从预览请求推流中拿到
            {
                strInfo = string.Format("NET_ECMS_StopGetRealStream failed, m_iSessionID = [0]", m_iSessionID);
                bRet = false;
            }

            if (!HCEHomeStream.NET_ESTREAM_StopPreview(m_lPlayHandle))
            {
                strInfo = string.Format("NET_ESTREAM_StopPreview[0]", m_lPlayHandle);
                g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 2, strInfo);
                bRet = false;
            }
            //CleanPlayParam();

            if (m_lPort >= 0)
            {
                if (!PlayCtrl.PlayM4_Stop(m_lPort))
                {
                    
                    strInfo = string.Format("PlayM4_Stop err[%d]", PlayCtrl.PlayM4_GetLastError(m_lPort));
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 0, strInfo);
                }
                else
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 0, "PlayM4_Stop");
                }
                if (!PlayCtrl.PlayM4_CloseStream(m_lPort))
                {
                    strInfo = string.Format("PlayM4_CloseStream err[%d]", PlayCtrl.PlayM4_GetLastError(m_lPort));
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 0, strInfo);
                }
                else
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 0, "PlayM4_CloseStream");
                }
                if (!PlayCtrl.PlayM4_FreePort(m_lPort))
                {
                    strInfo = string.Format("PlayM4_FreePort err[%d]", PlayCtrl.PlayM4_GetLastError(m_lPort));
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 0, strInfo);
                }
                else
                {
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 0, "PlayM4_FreePort");
                }

                m_lPort = -1;
            }
            DeviceTree.g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChanIndex].bPlay = false;
            m_bPlaying = false;
            return bRet;
        }


    }
}
