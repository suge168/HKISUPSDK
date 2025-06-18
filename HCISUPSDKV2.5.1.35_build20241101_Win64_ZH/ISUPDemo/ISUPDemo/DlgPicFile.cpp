// DlgPicFile.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgPicFile.h"


// CDlgPicFile �Ի���

IMPLEMENT_DYNAMIC(CDlgPicFile, CDialog)

CDlgPicFile::CDlgPicFile(CWnd* pParent /*=NULL*/)
    : CDialog(CDlgPicFile::IDD, pParent)
    , m_dateStart(0)
    , m_timeStart(0)
    , m_DateStop(0)
    , m_timeStop(0)
    , m_byTimeType(0)
{

}

CDlgPicFile::~CDlgPicFile()
{
}

void CDlgPicFile::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_LIST_PIC_FILE, m_lstPicFile);
    DDX_DateTimeCtrl(pDX, IDC_COMBO_PIC_DATE_START, m_dateStart);
    DDX_DateTimeCtrl(pDX, IDC_COMBO_PIC_TIME_START, m_timeStart);
    DDX_DateTimeCtrl(pDX, IDC_COMBO_PIC_DATE_STOP, m_DateStop);
    DDX_DateTimeCtrl(pDX, IDC_COMBO_PIC_TIME_STOP, m_timeStop);
    DDX_Control(pDX, IDC_COMBO_PIC_FILE_TYPE, m_cmbPicType);
    DDX_CBIndex(pDX, IDC_COMBO_TIME_TYPE, m_byTimeType);
}


BEGIN_MESSAGE_MAP(CDlgPicFile, CDialog)
    ON_BN_CLICKED(IDC_BTN_SEARCH_PIC_FILE, &CDlgPicFile::OnBnClickedBtnSearchPicFile)
END_MESSAGE_MAP()


BOOL CDlgPicFile::CheckInitParam()
{
    int iDeviceIndex = g_pMainDlg->GetCurDeviceIndex();
    if (iDeviceIndex == -1)
    {
        UpdateData(FALSE);
        return FALSE;
    }

    if ( m_iDeviceIndex == -1 || m_iDeviceIndex != iDeviceIndex)
    {
        m_iChanIndex = -1;
    }

    int iChanIndex = g_pMainDlg->GetCurChanIndex();
    if (iChanIndex == -1)
    {
        //g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, "set the channel index = 0");
        //iChanIndex = 0;
        AfxMessageBox("please select a channel!");
        UpdateData(FALSE);
        return FALSE;
    }

    UpdateData(TRUE);

    m_iDeviceIndex = iDeviceIndex;
    m_lLoginID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;

    //initialize parameters again while switching channel
    if (m_iChanIndex != iChanIndex)
    {
        m_iChanIndex = iChanIndex;


        m_bSearching = FALSE;

        //m_hPareDlgWnd = NULL;
        //m_hPareTabWnd = NULL;

        m_hFileThread = NULL;
        m_dwFileNum = 0;

    }

    if (m_lLoginID < 0)
    {
        UpdateData(FALSE);
        return FALSE;
    }

    return TRUE;

}


/*********************************************************
Function:    GetFileThread
Desc:        get recorded file list thread
Input:        pParam,pointer to parameters
Output:    
Return:    
**********************************************************/
UINT CDlgPicFile::GetFileThread(LPVOID pParam)
{
    CDlgPicFile *pThis = static_cast<CDlgPicFile*>(pParam);
    LONG lRet = -1;
    //NET_EHOME_FINDDATA struFileInfo;
   // NET_EHOME_DEV_LOG struFileInfo;
    NET_EHOME_PIC_FILE struFileInfo;
    memset(&struFileInfo, 0, sizeof(struFileInfo));
    CString csTmp;
    char szLan[128] = {0};


    while (!pThis->m_bQuit)
    {
        lRet = lRet = NET_ECMS_FindNextFile_V11(pThis->m_lFileHandle, &struFileInfo, sizeof(struFileInfo));
        if (lRet == ENUM_GET_NEXT_STATUS_SUCCESS)
        {            
            memcpy(&pThis->m_struFindData, &struFileInfo, sizeof(struFileInfo)); 
            pThis->m_lstPicFile.InsertItem(pThis->m_dwFileNum, struFileInfo.sFileName, 0);            
            if (struFileInfo.dwFileSize / 1024 == 0)
            {
                csTmp.Format("%d",struFileInfo.dwFileSize);
            }
            else if (struFileInfo.dwFileSize / 1024 > 0 && struFileInfo.dwFileSize / (1024*1024) == 0)
            {
                csTmp.Format("%dK",struFileInfo.dwFileSize / 1024);
            }
            else// if ()
            {
                csTmp.Format("%dM",struFileInfo.dwFileSize / 1024/1024);//different from hard disk capacity, files need tranformation
            }
            csTmp.Format("%d",struFileInfo.dwFileSize);
            pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 1, csTmp);
            csTmp.Format("%04d%02d%02d%02d%02d%02d",struFileInfo.struPicTime.wYear, \
                struFileInfo.struPicTime.byMonth, struFileInfo.struPicTime.byDay, \
                struFileInfo.struPicTime.byHour, struFileInfo.struPicTime.byMinute, \
                struFileInfo.struPicTime.bySecond);
             pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 2, csTmp);
//             csTmp.Format("%04d%02d%02d%02d%02d%02d", struFileInfo.struStopTime.wYear, struFileInfo.struStopTime.byMonth,\
//                 struFileInfo.struStopTime.byDay, struFileInfo.struStopTime.byHour, \
//                 struFileInfo.struStopTime.byMinute, struFileInfo.struStopTime.bySecond);
//             pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 3, csTmp);


            csTmp.Format("%d",struFileInfo.dwFileMainType);
            pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 4, csTmp);

           // csTmp.Format("%d",struFileInfo.dwFileSubType);
          //  pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 5, csTmp);

            csTmp.Format("%d",struFileInfo.dwFileIndex);
            pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 6, csTmp);

            csTmp.Format("%d", struFileInfo.byTimeDiffH);
            pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 7, csTmp);

            csTmp.Format("%d", struFileInfo.byTimeDiffM);
            pThis->m_lstPicFile.SetItemText(pThis->m_dwFileNum, 8, csTmp);


            pThis->m_dwFileNum++;
        }
        else
        {
            if (lRet == ENUM_GET_NETX_STATUS_NEED_WAIT)
            {
                Sleep(5);
                continue;
            }

            if ((lRet == ENUM_GET_NETX_STATUS_NO_FILE) || (lRet == ENUM_GET_NEXT_STATUS_FINISH))
            {
                g_StringLanType(szLan, "����", "Search");
                pThis->GetDlgItem(IDC_BTN_SEARCH_PIC_FILE)->SetWindowText(szLan);
                pThis->m_bSearching = FALSE;
                pThis->GetDlgItem(IDC_STATIC_FINDING_PIC_FILE)->ShowWindow(SW_HIDE);
                g_pMainDlg->AddLog(pThis->m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_FindNextFile file num[%d]", pThis->m_dwFileNum);
                pThis->m_dwFileNum = 0;
                break;
            }
            else if(lRet == ENUM_GET_NEXT_STATUS_NOT_SUPPORT)
            {
                pThis->GetDlgItem(IDC_BTN_SEARCH_PIC_FILE)->SetWindowText("����");
                pThis->m_bSearching = FALSE;
                pThis->GetDlgItem(IDC_STATIC_FINDING_PIC_FILE)->ShowWindow(SW_HIDE);
                g_pMainDlg->AddLog(pThis->m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_FindNextFile");
                g_StringLanType(szLan, "�豸��֧�ָò���", "Device do not support");
                AfxMessageBox(szLan);
                pThis->m_dwFileNum = 0;
                break;
            }
            else
            {
                pThis->GetDlgItem(IDC_BTN_SEARCH_PIC_FILE)->SetWindowText("����");
                pThis->m_bSearching = FALSE;
                pThis->GetDlgItem(IDC_STATIC_FINDING_PIC_FILE)->ShowWindow(SW_HIDE);
                g_pMainDlg->AddLog(pThis->m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_FindNextFile");
                g_StringLanType(szLan, "���ڷ�����æ,���������,��ȡ�ļ��б��쳣��ֹ", "Since the server is busy, or network failure, abnormal termination of access to the file list");
                AfxMessageBox(szLan);
                pThis->m_dwFileNum = 0;
                break;
            }
        }
    }
    CloseHandle(pThis->m_hFileThread);
    pThis->m_hFileThread = NULL;
    NET_ECMS_StopFindFile(pThis->m_lFileHandle);
    pThis->m_lFileHandle = -1;
    pThis->m_bSearching = FALSE;
    return 0;
}

// CDlgPicFile ��Ϣ�������

void CDlgPicFile::OnBnClickedBtnSearchPicFile()
{
    // TODO: �ڴ���ӿؼ�֪ͨ����������
    UpdateData(TRUE);
    CheckInitParam();
    char szLan[128] = {0};
    if (m_iDeviceIndex < 0)
    {
        g_StringLanType(szLan,"���ȵ�¼�豸!", "Please Login First!");
        AfxMessageBox(szLan);
        return;
    }

    if (!m_bSearching)
    {
        m_bQuit = FALSE;

        memset(&m_struFindCond, 0, sizeof(m_struFindCond));
        m_struFindCond.dwSize = sizeof(m_struFindCond);
        m_struFindCond.enumSearchType = ENUM_SEARCH_PICTURE_FILE;
        m_struFindCond.dwMaxFileCountPer = 10;
        m_struFindCond.iChannel = g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChanIndex].iChannelNO;
        m_struFindCond.unionSearchParam.struPicFileParam.dwFileType = m_cmbPicType.GetItemData(m_cmbPicType.GetCurSel());

        m_struFindCond.struStartTime.wYear = (WORD)m_dateStart.GetYear();
        m_struFindCond.struStartTime.byMonth = (BYTE)m_dateStart.GetMonth();
        m_struFindCond.struStartTime.byDay = (BYTE)m_dateStart.GetDay();
        m_struFindCond.struStartTime.byHour = (BYTE)m_timeStart.GetHour();
        m_struFindCond.struStartTime.byMinute = (BYTE)m_timeStart.GetMinute();
        m_struFindCond.struStartTime.bySecond = (BYTE)m_timeStart.GetSecond();

        m_struFindCond.struStopTime.wYear = (WORD)m_DateStop.GetYear();
        m_struFindCond.struStopTime.byMonth = (BYTE)m_DateStop.GetMonth();
        m_struFindCond.struStopTime.byDay = (BYTE)m_DateStop.GetDay();
        m_struFindCond.struStopTime.byHour = (BYTE)m_timeStop.GetHour();
        m_struFindCond.struStopTime.byMinute = (BYTE)m_timeStop.GetMinute();
        m_struFindCond.struStopTime.bySecond = (BYTE)m_timeStop.GetSecond();

        
        NET_EHOME_PIC_FILE_COND struPicCond = { 0 };
        struPicCond.dwMaxFileCountPer = 10;
        struPicCond.dwChannel = g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChanIndex].iChannelNO;
        struPicCond.dwPicType = m_cmbPicType.GetItemData(m_cmbPicType.GetCurSel());
        

        struPicCond.struStartTime.wYear = (WORD)m_dateStart.GetYear();
        struPicCond.struStartTime.byMonth = (BYTE)m_dateStart.GetMonth();
        struPicCond.struStartTime.byDay = (BYTE)m_dateStart.GetDay();
        struPicCond.struStartTime.byHour = (BYTE)m_timeStart.GetHour();
        struPicCond.struStartTime.byMinute = (BYTE)m_timeStart.GetMinute();
        struPicCond.struStartTime.bySecond = (BYTE)m_timeStart.GetSecond();

        struPicCond.struStopTime.wYear = (WORD)m_DateStop.GetYear();
        struPicCond.struStopTime.byMonth = (BYTE)m_DateStop.GetMonth();
        struPicCond.struStopTime.byDay = (BYTE)m_DateStop.GetDay();
        struPicCond.struStopTime.byHour = (BYTE)m_timeStop.GetHour();
        struPicCond.struStopTime.byMinute = (BYTE)m_timeStop.GetMinute();
        struPicCond.struStopTime.bySecond = (BYTE)m_timeStop.GetSecond();

        NET_EHOME_PIC_FILE_COND struPicFileCond = { 0 };
        struPicFileCond.dwChannel = g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChanIndex].iChannelNO;
        struPicFileCond.byLocalOrUTC = (BYTE)m_byTimeType;
        struPicFileCond.dwPicType = m_cmbPicType.GetItemData(m_cmbPicType.GetCurSel());

        struPicFileCond.struStartTime.wYear = (WORD)m_dateStart.GetYear();
        struPicFileCond.struStartTime.byMonth = (BYTE)m_dateStart.GetMonth();
        struPicFileCond.struStartTime.byDay = (BYTE)m_dateStart.GetDay();
        struPicFileCond.struStartTime.byHour = (BYTE)m_timeStart.GetHour();
        struPicFileCond.struStartTime.byMinute = (BYTE)m_timeStart.GetMinute();
        struPicFileCond.struStartTime.bySecond = (BYTE)m_timeStart.GetSecond();

        struPicFileCond.struStopTime.wYear = (WORD)m_DateStop.GetYear();
        struPicFileCond.struStopTime.byMonth = (BYTE)m_DateStop.GetMonth();
        struPicFileCond.struStopTime.byDay = (BYTE)m_DateStop.GetDay();
        struPicFileCond.struStopTime.byHour = (BYTE)m_timeStop.GetHour();
        struPicFileCond.struStopTime.byMinute = (BYTE)m_timeStop.GetMinute();
        struPicFileCond.struStopTime.bySecond = (BYTE)m_timeStop.GetSecond();
        struPicFileCond.dwMaxFileCountPer = 8;
        


       // m_lFileHandle = NET_ECMS_StartFindFile(m_lLoginID, &m_struFindCond);

       //m_lFileHandle = NET_ECMS_StartFindFile_V11(m_lLoginID, ENUM_SEARCH_PICTURE_FILE, &struPicCond, sizeof(struPicCond));
        m_lFileHandle = NET_ECMS_StartFindFile_V11(m_lLoginID, ENUM_SEARCH_PICTURE_FILE, &struPicFileCond, sizeof(struPicFileCond));
        if (m_lFileHandle < 0)
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_StartFindFile");
            g_StringLanType(szLan, "�����ļ��б�ʧ��!", "Fail to get file list");
            AfxMessageBox(szLan);
            return;
        }
        m_lstPicFile.DeleteAllItems();
        DWORD dwThreadId;
        if (m_hFileThread == NULL)
        {
            m_hFileThread = CreateThread(NULL,0,LPTHREAD_START_ROUTINE(GetFileThread),this,0,&dwThreadId);      
            if (m_hFileThread == NULL)
            {
                g_StringLanType(szLan, "�򿪲����߳�ʧ��!", "Fail to open finding thread!");
                AfxMessageBox(szLan);
                return;
            }
        }


        g_StringLanType(szLan, "ֹͣ����", "Stop Searching");
        GetDlgItem(IDC_BTN_SEARCH_PIC_FILE)->SetWindowText(szLan);
        m_bSearching = TRUE;
        GetDlgItem(IDC_STATIC_FINDING_PIC_FILE)->ShowWindow(SW_SHOW);
    }
    else
    {
        if (m_hFileThread)
        {
            m_bQuit = TRUE;
            //TerminateThread(m_hFileThread, 0);
        }
        CloseHandle(m_hFileThread);
        m_hFileThread = NULL;
        NET_ECMS_StopFindFile(m_lFileHandle);
        g_StringLanType(szLan, "����", "Search");
        GetDlgItem(IDC_BTN_SEARCH_PIC_FILE)->SetWindowText(szLan);
        m_bSearching = FALSE;
        GetDlgItem(IDC_STATIC_FINDING_PIC_FILE)->ShowWindow(SW_HIDE);
        m_dwFileNum = 0;
    }
}

BOOL CDlgPicFile::OnInitDialog()
{
    CDialog::OnInitDialog();

    // TODO:  �ڴ���Ӷ���ĳ�ʼ��

    char szLan[128] = {0};

    g_StringLanType(szLan, "�ļ�����", "File Name");
    m_lstPicFile.InsertColumn(0, szLan,LVCFMT_LEFT,150,-1);
    g_StringLanType(szLan, "��С", "Size");
    m_lstPicFile.InsertColumn(1, szLan,LVCFMT_LEFT,50,-1);
    g_StringLanType(szLan, "��ʼʱ��", "Start time");
    m_lstPicFile.InsertColumn(2, szLan, LVCFMT_LEFT, 120, -1);

    g_StringLanType(szLan, "����ʱ��", "Stop Time");
    m_lstPicFile.InsertColumn(3, szLan, LVCFMT_LEFT, 120, -1);

    g_StringLanType(szLan,"������","Main Type");
    m_lstPicFile.InsertColumn(4, szLan,LVCFMT_LEFT,50,-1);

    g_StringLanType(szLan,"������","Sub Type");
    m_lstPicFile.InsertColumn(5, szLan,LVCFMT_LEFT,50,-1);

    g_StringLanType(szLan,"����","Index");
    m_lstPicFile.InsertColumn(6, szLan,LVCFMT_LEFT,50,-1);

    g_StringLanType(szLan, "ʱ��ʱ", "Time Diff H");
    m_lstPicFile.InsertColumn(7, szLan, LVCFMT_LEFT, 50, -1);

    g_StringLanType(szLan, "ʱ���", "Time Diff M");
    m_lstPicFile.InsertColumn(8, szLan, LVCFMT_LEFT, 50, -1);



    CTime timeCur = CTime::GetCurrentTime();
    CTime timeStart(timeCur.GetYear(),timeCur.GetMonth(),timeCur.GetDay(),0,0,0);
    CTime timeStop(timeCur.GetYear(),timeCur.GetMonth(),timeCur.GetDay(),23,59,59);
    m_dateStart = timeStart;
    m_timeStart = timeStart;
    m_DateStop = timeStop;
    m_timeStop = timeStop;

    m_bQuit = FALSE;

    int index = 0;
    m_cmbPicType.ResetContent();
    g_StringLanType(szLan, "ȫ��", "All");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_ALL_PIC);
    index++;

    g_StringLanType(szLan, "��ʱץͼ", "Scheduled Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_SCHEDULED_CAP);
    index++;

    g_StringLanType(szLan, "�ƶ����ץͼ", "Motion Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_MOTION_CAP);
    index++;

    g_StringLanType(szLan, "����ץͼ", "Alarm Capture ");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_ALARM_CAP);
    index++;

    g_StringLanType(szLan, "����|�ƶ����ץͼ", "Alarm|Motion Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_MOTION_OR_ALARMIN_CAP);
    index++;

    g_StringLanType(szLan, "����&�ƶ����ץͼ", "Alarm&Motion Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_MOTION_AND_ALARMIN_CAP);
    index++;

    g_StringLanType(szLan, "�����ץͼ", "Command Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_COMMAND_CAP);
    index++;

    g_StringLanType(szLan, "�ֶ�ץͼ", "Manual Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_MANUAL_CAP);
    index++;

    g_StringLanType(szLan, "�𶯱���ץͼ", "Vibrating Alarm Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_VIBRATING_ALARM_CAP);
    index++;

    g_StringLanType(szLan, "������������ץͼ", "Environment Alarm Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_ENVIRONMENT_ALARM_CAP);
    index++;

    g_StringLanType(szLan, "���ܱ���ͼƬ", "VCA Alarm Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_VCA_ALARM);
    index++;

    g_StringLanType(szLan, "PIR����ͼƬ", "pIR Alarm Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_PIR_ALARM);
    index++;

    g_StringLanType(szLan, "���߱���ͼƬ", "Wireless Alarm Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_WIRELESS_ALARM);
    index++;

    g_StringLanType(szLan, "���ȱ���ͼƬ", "Emergency Alarm Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_EMERGENCY_ALARM);
    index++;

    g_StringLanType(szLan, "�������ͼƬ", "Face Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_FACE_DETECTION);
    index++;

    g_StringLanType(szLan, "Խ�����ͼƬ", "Line Crossing Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_LINE_CROSSING_DETECTION);
    index++;


    g_StringLanType(szLan, "Խ�����ͼƬ", "Intrusion Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_INTRUSION_DETECTION);
    index++;


    g_StringLanType(szLan, "����������ͼƬ", "Scene Change Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_SCENCE_CHANGE_DETECTION);
    index++;

    g_StringLanType(szLan, "�豸���ػط�ʱ��ͼ", "Local Playback Capture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_LOCAL_PLAYBACK);
    index++;

    g_StringLanType(szLan, "�������ͼƬ", "VCA Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_VCA_DETECTION);
    index++;

    g_StringLanType(szLan, "�����������ͼƬ", "Region Entrance Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_REGION_ENTRANCE_DETECTION);
    index++;

    g_StringLanType(szLan, "�뿪�������ͼƬ", "Region Exiting Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_REGION_EXITING_DETECTION);
    index++;

    g_StringLanType(szLan, "�ǻ����ͼƬ", "Loitering Detection");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_LOITERING_DETECTION);
    index++;

    g_StringLanType(szLan, "��Ա�ۼ����ͼƬ", "People Gathering Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_PEOPLE_GATHERING_DETECTION);
    index++;

    g_StringLanType(szLan, "�����˶����ͼƬ", "Fast Moving Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_FAST_MOVING_DETECTION);
    index++;

    g_StringLanType(szLan, "ͣ�����ͼƬ", "Parking Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_PARKING_DETECTION);
    index++;

    g_StringLanType(szLan, "��Ʒ�������ͼƬ", "Unattended Baggage Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_UNATTENDED_BAGGAGE_DETECTION);
    index++;

    g_StringLanType(szLan, "��Ʒ��ȡ���ͼƬ", "Object Removal Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_OBJECT_REMOVAL_DETECTION);
    index++;

    g_StringLanType(szLan, "�������ͼƬ", "License Detection Picture");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_LICENSE_DETECTION);
    index++;

    g_StringLanType(szLan, "�ͻ����ϴ�ͼƬ", "Picture Uploaded to Client");
    m_cmbPicType.InsertString(index, szLan);
    m_cmbPicType.SetItemData(index, PIC_UPLOADED_TO_CLIENT);
    index++;

    m_cmbPicType.SetCurSel(0);

    UpdateData(FALSE);

    return TRUE;  // return TRUE unless you set the focus to a control
    // �쳣: OCX ����ҳӦ���� FALSE
}
