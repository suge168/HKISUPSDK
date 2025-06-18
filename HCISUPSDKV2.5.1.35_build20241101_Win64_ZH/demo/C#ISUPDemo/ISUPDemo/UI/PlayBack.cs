using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ISUPDemo;
using ISUPDemo.Public;
using ISUPDemo.Language;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace ISUPDemo.UI
{
    public partial class PlayBack : UserControl
    {

        private Int32 m_lFileHandle;
        public Int32 m_nFileNum;
        public Int32 m_iSessionID = -1;
        public static int m_lPort;   //播放库port号
        public static IntPtr m_hWnd;
        public static int m_iLoginID;
        private int m_lLogNum = 0;
        public static String m_csLocalIPAddr;
        public static Int16 m_lLocalPort;
        public string m_csFileName = "";

        public string m_saveFilePath = null;
        public static bool m_DownloadFile = false;
        public static FileStream fs = null;

        private static DeviceLogList g_formList = DeviceLogList.Instance();
        private static DeviceTree g_deviceTree = DeviceTree.Instance();

        private static HCEHomeStream.PLAYBACK_DATA_CB fnPLAYBACK_DATA_CB_Func = new HCEHomeStream.PLAYBACK_DATA_CB(fnPLAYBACK_DATA_CB);
        private static HCEHomeStream.PLAYBACK_NEWLINK_CB fnPLAYBACK_NEWLINK_CB_Func = new HCEHomeStream.PLAYBACK_NEWLINK_CB(fnPLAYBACK_NEWLINK_CB);

        public PlayBack()
        {
            InitializeComponent();
            m_hWnd = m_PlayBackPanel.Handle;
        }

        public void CheckInitParam()
        {
            int m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_iLoginID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
            Control.CheckForIllegalCrossThreadCalls = false;

            m_cmbPlayType.Items.Clear();
            m_cmbPlayType.Items.Add(string.Format("File(ISUP5.0 no support)"));
            m_cmbPlayType.Items.Add(string.Format("Time"));
            m_cmbPlayType.SelectedIndex = 1;

            this.Refresh();
        }

        public static bool fnPLAYBACK_DATA_CB(int iPlayBackHandle, ref HCEHomeStream.NET_EHOME_PLAYBACK_DATA_CB_INFO pPlayBackCBMsg, IntPtr pUserData)
        {
            int iWinIndex = (int)pUserData;
            if (pPlayBackCBMsg.pData == null || pPlayBackCBMsg.dwDataLen == 0)
            {
                return false;
            }

            if (m_DownloadFile)
            {
                try
                {
                    byte[] byData = new byte[(int)pPlayBackCBMsg.dwDataLen];  
                    Marshal.Copy(pPlayBackCBMsg.pData, byData, 0, (int)pPlayBackCBMsg.dwDataLen);
                    fs.Write(byData, 0, pPlayBackCBMsg.dwDataLen); 
                }
                catch
                {
                    return false;
                }
                                                  
            }

            if (pPlayBackCBMsg.dwType == HCEHomeStream.NET_EHOME_SYSHEAD)
            {

                if (!PlayCtrl.PlayM4_GetPort(ref m_lPort))
                {
                    return false;
                }
                if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_lPort, PlayCtrl.STREAME_FILE ))
                {
                    return false;
                }
                // 先输入头,前40个字节
                if (!PlayCtrl.PlayM4_OpenStream(m_lPort, pPlayBackCBMsg.pData, (uint)pPlayBackCBMsg.dwDataLen, (uint)2 * 1024 * 1024 ))
                {
                    return false;
                }
                if (!PlayCtrl.PlayM4_Play(m_lPort, m_hWnd))
                {
                    return false;
                }
            }
            else if (pPlayBackCBMsg.dwType == HCEHomeStream.NET_EHOME_STREAMDATA)
            {
                PlayCtrl.PlayM4_InputData(m_lPort, pPlayBackCBMsg.pData, (uint)pPlayBackCBMsg.dwDataLen);
            }

            return true;
        }

        private void GetFileThread()
        {

            HCEHomeCMS.NET_EHOME_REC_FILE struFileInfo = new HCEHomeCMS.NET_EHOME_REC_FILE();
            int dwOutBufferSize = Marshal.SizeOf(struFileInfo);
            IntPtr lpOutputFile = Marshal.AllocHGlobal(dwOutBufferSize);
            Marshal.StructureToPtr(struFileInfo, lpOutputFile, false);

            while (true)
             {
                 if (-1 == m_lFileHandle)
                 {
                     return;
                 }

                 int iRet = HCEHomeCMS.NET_ECMS_FindNextFile_V11(m_lFileHandle, lpOutputFile, dwOutBufferSize);
                 if (iRet == HCEHomeCMS.ENUM_GET_NEXT_STATUS_SUCCESS)
                 {
                     struFileInfo = (HCEHomeCMS.NET_EHOME_REC_FILE)Marshal.PtrToStructure(lpOutputFile, typeof(HCEHomeCMS.NET_EHOME_REC_FILE));
                                   
                     listViewRecordFile.BeginUpdate();
                     ListViewItem listItem = new ListViewItem();
                     listItem.Text = (++m_lLogNum).ToString();

                     listItem.SubItems.Add(new string(struFileInfo.sFileName));
                     listItem.SubItems.Add(struFileInfo.dwFileSize.ToString());

                     string strTime = struFileInfo.struStartTime.wYear.ToString() + "-" + struFileInfo.struStartTime.byMonth.ToString() + "-" + struFileInfo.struStartTime.byDay.ToString() + " "
                         + struFileInfo.struStartTime.byHour.ToString() + ":" + struFileInfo.struStartTime.byMinute.ToString() + ":" + struFileInfo.struStartTime.bySecond.ToString();
                     listItem.SubItems.Add(strTime);
                     strTime = struFileInfo.struStopTime.wYear.ToString() + "-" + struFileInfo.struStopTime.byMonth.ToString() + "-" + struFileInfo.struStopTime.byDay.ToString() + " "
                        + struFileInfo.struStopTime.byHour.ToString() + ":" + struFileInfo.struStopTime.byMinute.ToString() + ":" + struFileInfo.struStopTime.bySecond.ToString();
                     listItem.SubItems.Add(strTime);
                     listItem.SubItems.Add(struFileInfo.dwFileMainType.ToString());
                     listItem.SubItems.Add(struFileInfo.dwFileSubType.ToString());
                     listViewRecordFile.Items.Add(listItem);
                     listViewRecordFile.EndUpdate();
                  }
                else
                {
                    if (iRet == HCEHomeCMS.ENUM_GET_NETX_STATUS_NEED_WAIT)
                    {
                        Thread.Sleep(5);
                        continue;
                    }
                    if ((iRet == HCEHomeCMS.ENUM_GET_NETX_STATUS_NO_FILE))
                    {
                        g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ECMS_FindNextFile_V11 FAIL [no file]");
                        break;
                    }
                    else if( (iRet == HCEHomeCMS.ENUM_GET_NEXT_STATUS_FINISH))
                    {
                        g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ECMS_FindNextFile_V11 FINISH");
                         break;
                    }
                    else if (iRet == HCEHomeCMS.ENUM_GET_NEXT_STATUS_NOT_SUPPORT)
                    {
                        g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, "NET_ECMS_FindNextFile_V11 FAIL [no support]");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
              }
              return;
    }



        private void buttonSearch_Click(object sender, EventArgs e)
        {

            listViewRecordFile.Items.Clear();

            HCEHomeCMS.NET_EHOME_REC_FILE_COND struSearchCond = new HCEHomeCMS.NET_EHOME_REC_FILE_COND();
            int iSize = Marshal.SizeOf(struSearchCond);

            struSearchCond.dwChannel = 1;
            struSearchCond.dwMaxFileCountPer = 8;
            struSearchCond.dwStartIndex = 0;

            struSearchCond.struStartTime.wYear = (Int16)RecordFileStartTime.Value.Year;
            struSearchCond.struStartTime.byMonth = (byte)RecordFileStartTime.Value.Month;
            struSearchCond.struStartTime.byDay = (byte)RecordFileStartTime.Value.Day;
            struSearchCond.struStartTime.byHour = (byte)RecordFileStartTime2.Value.Hour;
            struSearchCond.struStartTime.byMinute = (byte)RecordFileStartTime2.Value.Minute;
            struSearchCond.struStartTime.bySecond = (byte)RecordFileStartTime2.Value.Second;
            struSearchCond.struStartTime.wMSecond = (Int16)RecordFileStartTime2.Value.Millisecond;

            struSearchCond.struStopTime.wYear = (Int16)RecordFileEndTime.Value.Year;
            struSearchCond.struStopTime.byMonth = (byte)RecordFileEndTime.Value.Month;
            struSearchCond.struStopTime.byDay = (byte)RecordFileEndTime.Value.Day;
            struSearchCond.struStopTime.byHour = (byte)RecordFileEndTime2.Value.Hour;
            struSearchCond.struStopTime.byMinute = (byte)RecordFileEndTime2.Value.Minute;
            struSearchCond.struStopTime.bySecond = (byte)RecordFileEndTime2.Value.Second;
            struSearchCond.struStopTime.wMSecond = (Int16)RecordFileEndTime2.Value.Millisecond;

            IntPtr pSearchCond = Marshal.AllocHGlobal(iSize);
            Marshal.StructureToPtr(struSearchCond, pSearchCond, false);

            try
            {
                m_lFileHandle = HCEHomeCMS.NET_ECMS_StartFindFile_V11(m_iLoginID, HCEHomeCMS.ENUM_SEARCH_RECORD_FILE, pSearchCond, iSize);

                if (m_lFileHandle < 0)
                {
                    string str = string.Format("NET_ECMS_StartFindFile_V11 err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
                    return;
                }

                Thread thFileThread = new Thread(new ThreadStart(GetFileThread));

                thFileThread.Start();

                while (!thFileThread.IsAlive)
                {
                    thFileThread.Join();
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pSearchCond);
            }
        }

        public static bool fnPLAYBACK_NEWLINK_CB(Int32 iPlayBackLinkHandle, ref HCEHomeStream.NET_EHOME_PLAYBACK_NEWLINK_CB_INFO pNewLinkCBMsg, IntPtr pUserData)
        {

            HCEHomeStream.NET_EHOME_PLAYBACK_DATA_CB_PARAM struDataCBParam = new HCEHomeStream.NET_EHOME_PLAYBACK_DATA_CB_PARAM();
            struDataCBParam.fnPlayBackDataCB = fnPLAYBACK_DATA_CB_Func;
            struDataCBParam.pUserData = pUserData;
            struDataCBParam.byStreamFormat = 0;
            struDataCBParam.byRes = new byte[128];
            IntPtr ptrDataCBParam = Marshal.AllocHGlobal(Marshal.SizeOf(struDataCBParam));
            Marshal.StructureToPtr(struDataCBParam, ptrDataCBParam, false);
            if (!HCEHomeStream.NET_ESTREAM_SetPlayBackDataCB(iPlayBackLinkHandle, ptrDataCBParam))
            {
                Marshal.FreeHGlobal(ptrDataCBParam);
                return false;
            }
            Marshal.FreeHGlobal(ptrDataCBParam);
            return true;
        }

        private void buttonStartPlay_Click(object sender, EventArgs e)
        {
            HCEHomeStream.NET_EHOME_PLAYBACK_LISTEN_PARAM struPlayBackListen = new HCEHomeStream.NET_EHOME_PLAYBACK_LISTEN_PARAM();
            Int32 dwSize = (Int32)Marshal.SizeOf(struPlayBackListen);
            struPlayBackListen.struIPAdress.Init();
            m_csLocalIPAddr.CopyTo(0, struPlayBackListen.struIPAdress.szIP, 0, m_csLocalIPAddr.Length);
            struPlayBackListen.struIPAdress.wPort = 8888;
            struPlayBackListen.fnNewLinkCB = fnPLAYBACK_NEWLINK_CB_Func;
            struPlayBackListen.pUserData = this.Handle;
            struPlayBackListen.byLinkMode = 0;
            struPlayBackListen.byRes = new byte[127];

            Int32 iHandle = HCEHomeStream.NET_ESTREAM_StartListenPlayBack(ref struPlayBackListen);
            if (iHandle > -1)
            {
                g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ESTREAM_StartListenPlayBack Success");
            }
            else
            {
                string str = string.Format("NET_ESTREAM_StartListenPlayBack err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
            }


            HCEHomeCMS.NET_EHOME_PLAYBACK_INFO_OUT struPlayBackInfoOut = new HCEHomeCMS.NET_EHOME_PLAYBACK_INFO_OUT();

            if (m_cmbPlayType.SelectedIndex == 0)   //playback by searchfile
            {
                HCEHomeCMS.NET_EHOME_PLAYBACK_INFO_IN_NAME struPlayBackInfoIn = new HCEHomeCMS.NET_EHOME_PLAYBACK_INFO_IN_NAME();
                struPlayBackInfoIn.Init();

                struPlayBackInfoIn.dwSize = (Int32)Marshal.SizeOf(struPlayBackInfoIn);
                struPlayBackInfoIn.dwChannel = 1;
                m_csLocalIPAddr.CopyTo(0, struPlayBackInfoIn.struStreamSever.szIP, 0, m_csLocalIPAddr.Length);
                struPlayBackInfoIn.struStreamSever.wPort = 8888;

                struPlayBackInfoIn.byPlayBackMode = 0;  //ISUP5.0  no support
                m_csFileName = m_csFileName.TrimEnd('\0');
                m_csFileName.CopyTo(0, struPlayBackInfoIn.struPlayBackbyName.szFileName, 0, m_csFileName.Length);

                if (!HCEHomeCMS.NET_ECMS_StartPlayBack(m_iLoginID, ref struPlayBackInfoIn, ref struPlayBackInfoOut))
                {
                    string str = string.Format("NET_ECMS_StartPlayBack err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
                    return;
                }   
            }
            else if (m_cmbPlayType.SelectedIndex == 1) //playback by time
            {
                HCEHomeCMS.NET_EHOME_PLAYBACK_INFO_IN_TIME struPlayBackInfoIn = new HCEHomeCMS.NET_EHOME_PLAYBACK_INFO_IN_TIME();
                struPlayBackInfoIn.Init();

                struPlayBackInfoIn.dwSize = (Int32)Marshal.SizeOf(struPlayBackInfoIn);
                struPlayBackInfoIn.dwChannel = 1;
                m_csLocalIPAddr.CopyTo(0, struPlayBackInfoIn.struStreamSever.szIP, 0, m_csLocalIPAddr.Length);
                struPlayBackInfoIn.struStreamSever.wPort = 8888;

                struPlayBackInfoIn.byPlayBackMode = 1;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.wYear = (Int16)RecordFileStartTime.Value.Year;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.byMonth = (byte)RecordFileStartTime.Value.Month;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.byDay = (byte)RecordFileStartTime.Value.Day;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.byHour = (byte)RecordFileStartTime2.Value.Hour;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.byMinute = (byte)RecordFileStartTime2.Value.Minute;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.bySecond = (byte)RecordFileStartTime2.Value.Second;
                struPlayBackInfoIn.struPlayBackbyTime.struStartTime.wMSecond = 0;

                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.wYear = (Int16)RecordFileEndTime.Value.Year;
                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.byMonth = (byte)RecordFileEndTime.Value.Month;
                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.byDay = (byte)RecordFileEndTime.Value.Day;
                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.byHour = (byte)RecordFileEndTime2.Value.Hour;
                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.byMinute = (byte)RecordFileEndTime2.Value.Minute;
                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.bySecond = (byte)RecordFileEndTime2.Value.Second;
                struPlayBackInfoIn.struPlayBackbyTime.struStopTime.wMSecond = 0;

                if (!HCEHomeCMS.NET_ECMS_StartPlayBack(m_iLoginID, ref struPlayBackInfoIn, ref struPlayBackInfoOut))
                {
                    string str = string.Format("NET_ECMS_StartPlayBack err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
                    return;
                }   
            }                            

            HCEHomeCMS.NET_EHOME_PUSHPLAYBACK_IN struPushPlayBackIn = new HCEHomeCMS.NET_EHOME_PUSHPLAYBACK_IN();
            struPushPlayBackIn.dwSize = (Int32)Marshal.SizeOf(struPushPlayBackIn);
            struPushPlayBackIn.lSessionID =  struPlayBackInfoOut.lSessionID;

            m_iSessionID = struPlayBackInfoOut.lSessionID;

            HCEHomeCMS.NET_EHOME_PUSHPLAYBACK_OUT struPushPlayBackOut = new HCEHomeCMS.NET_EHOME_PUSHPLAYBACK_OUT();
            struPushPlayBackOut.dwSize = (Int32)Marshal.SizeOf(struPushPlayBackOut);
            if (!HCEHomeCMS.NET_ECMS_StartPushPlayBack(m_iLoginID, ref struPushPlayBackIn, ref struPushPlayBackOut))
            {
                string str = string.Format("NET_ECMS_StartPushPlayBack err{0}", HCEHomeCMS.NET_ECMS_GetLastError());
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
                return;
            }

            if (checkBoxDownload.Checked)
            {
                m_DownloadFile = true;

                string path = textBoxSaveFilePath.Text + "RecordFile.mp4" ;
                fs = new FileStream(path, FileMode.Append);
            }
        }

        private void buttonStopPlay_Click(object sender, EventArgs e)
        {
            if (checkBoxDownload.Checked)
            {
                m_DownloadFile = false;
                fs.Flush();
                fs.Close();
            }
            if (m_iSessionID != -1)
            {
                HCEHomeCMS.NET_EHOME_STOPPLAYBACK_PARAM struStopParam = new HCEHomeCMS.NET_EHOME_STOPPLAYBACK_PARAM();
                struStopParam.lSessionID = m_iSessionID;
                 
                IntPtr pStopParam = Marshal.AllocHGlobal(Marshal.SizeOf(struStopParam));
                Marshal.StructureToPtr(struStopParam, pStopParam, false);

                if (!HCEHomeCMS.NET_ECMS_StopPlayBackEx(m_iLoginID, pStopParam))
                {
                    string str = string.Format("NET_ECMS_StopPlayBack err = {0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
                }
                else
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_SUCC_T, 2, "NET_ECMS_StopPlayBack Succ");
                    m_PlayBackPanel.Controls.Clear();
                }
            }     
       
             if (m_lPort >= 0)
            {
                 PlayCtrl.PlayM4_Stop(m_lPort);
                 PlayCtrl.PlayM4_CloseStream(m_lPort);
                 PlayCtrl.PlayM4_FreePort(m_lPort);
                m_lPort = -1;
             }
        }

        private void listViewFileInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewRecordFile.SelectedItems.Count > 0)
                {
                    m_csFileName = listViewRecordFile.SelectedItems[0].SubItems[1].Text.ToString();
                }
            }
            catch(Exception ex)
            {
                string str = string.Format("{0}", ex.Message);
                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 2, str);
            }
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileSavePath = new FolderBrowserDialog();
            if (DialogResult.OK != fileSavePath.ShowDialog())
            {
                MessageBox.Show("please select corrct file", "Error", MessageBoxButtons.OK);
            }
            m_saveFilePath = fileSavePath.SelectedPath;
            textBoxSaveFilePath.Text = m_saveFilePath;
        }

        private void m_cmbPlayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_cmbPlayType.SelectedIndex == 0)  //by file
            {
                buttonSearch.Enabled = true ;
            }
            else if(m_cmbPlayType.SelectedIndex == 1)  //by time
            {
                buttonSearch.Enabled = false;

            }
        }

        private void checkBoxDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDownload.Checked)
            {
                btnBrowse.Enabled = true;
            }
            else
            {
                btnBrowse.Enabled = false;
            }
        }


    }
}
