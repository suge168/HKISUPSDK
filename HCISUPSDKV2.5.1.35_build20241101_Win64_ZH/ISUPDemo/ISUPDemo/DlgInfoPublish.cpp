// DlgInfoPublish.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgInfoPublish.h"
#include "afxdialogex.h"
#include "DlgInfoPubTerCfg.h"

// CDlgInfoPublish �Ի���

IMPLEMENT_DYNAMIC(CDlgInfoPublish, CDialogEx)

CDlgInfoPublish::CDlgInfoPublish(CWnd* pParent /*=NULL*/)
	: CDialogEx(CDlgInfoPublish::IDD, pParent)
    , m_terminalName(_T(""))
    , m_dwServerPort(0)
    , m_dwClientFd(0)
    , m_strPicKmsAddr(_T(""))
    , m_dwCommunityFd(0)
    , m_dwTransIdentify(0)
    , m_strPicIdentify(_T(""))
    , m_strRetPicKmsAddr(_T(""))
{

}

CDlgInfoPublish::~CDlgInfoPublish()
{
}

void CDlgInfoPublish::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Text(pDX, IDC_EDIT_TERMINAL_NAME, m_terminalName);
    DDX_Text(pDX, IDC_EDIT_SERVER_PORT, m_dwServerPort);
    DDX_Text(pDX, IDC_EDIT_CLIENT_FD, m_dwClientFd);
    DDX_Text(pDX, IDC_EDIT_PIC_KMS_ADDR, m_strPicKmsAddr);
    DDX_Text(pDX, IDC_EDIT_COMMUNITY_FD, m_dwCommunityFd);
    DDX_Text(pDX, IDC_EDIT_TRANS_IDENTIFY, m_dwTransIdentify);
    DDX_Text(pDX, IDC_EDIT_PIC_IDENTIFY, m_strPicIdentify);
    DDX_Text(pDX, IDC_EDIT_RET_PIC_KMS_ADDR, m_strRetPicKmsAddr);
}


BEGIN_MESSAGE_MAP(CDlgInfoPublish, CDialogEx)
	ON_BN_CLICKED(IDC_BTN_TER_GROUND_LIGHT, &CDlgInfoPublish::OnBnClickedBtnTerGroundLight)
    ON_BN_CLICKED(IDC_BTN_SET_TERMINAL_NAME, &CDlgInfoPublish::OnBnClickedBtnSetTerminalName)
    ON_BN_CLICKED(IDC_BTN_SCREEN_SHOT, &CDlgInfoPublish::OnBnClickedBtnScreenShot)
    ON_BN_CLICKED(IDC_BTN_GET_TERMINAL_WORK_STATUS, &CDlgInfoPublish::OnBnClickedBtnGetTerminalWorkStatus)
    ON_BN_CLICKED(IDC_BTN_GET_TERMINAL_NAME, &CDlgInfoPublish::OnBnClickedBtnGetTerminalName)
    ON_BN_CLICKED(IDC_BTN_SCREEN_SHOT_SECOND, &CDlgInfoPublish::OnBnClickedBtnScreenShotSecond)
END_MESSAGE_MAP()

BOOL CDlgInfoPublish::CheckInitParam()
{
    char szLan[1024] = { 0 };
    m_iDeviceIndex = g_pMainDlg->GetCurDeviceIndex();
    if (m_iDeviceIndex < 0)
    {
        g_StringLanType(szLan, "����ѡ��һ���豸!", "please select a device first!");
        g_pMainDlg->AddLog(-1, OPERATION_FAIL_T, 0, szLan);
        return FALSE;
    }
    m_lLoginID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;
    if (m_lLoginID < 0)
    {
        g_StringLanType(szLan, "�豸δע��!", "device offline!");
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 0, szLan);
        return FALSE;
    }
    return TRUE;
}
// CDlgInfoPublish ��Ϣ�������


void CDlgInfoPublish::OnBnClickedBtnTerGroundLight()
{
	// TODO:  �ڴ���ӿؼ�֪ͨ����������
	CDlgInfoPubTerCfg dlg;
	dlg.DoModal();
}


void CDlgInfoPublish::OnBnClickedBtnSetTerminalName()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CheckInitParam();
    NET_EHOME_TERMINAL_NAME struTerminalName = { 0 };

    NET_EHOME_BINARY_SEND_DATA struSendData = { 0 };
    struSendData.dwSize = sizeof(struSendData);
    struSendData.lpInBuffer = &struTerminalName;
    struSendData.dwInBufferSize = sizeof(struTerminalName);

   // NET_EHOME_BINARY_RECV_DATA struRecvData = { 0 };
    //struRecvData.dwSize = sizeof(struRecvData);
    //struRecvData.lpOutBuffer = NULL;
    //struRecvData.dwOutBufferSize = NULL;
    if (!NET_ECMS_STDBinaryCtrl(m_lLoginID, NET_EHOME_S2C_SET_TERMINAL_NAME, &struSendData))
    {
        DWORD dwErr = NET_ECMS_GetLastError();
        AfxMessageBox("NET_EHOME_S2C_SET_TERMINAL_NAME failed insert, err=%d", dwErr);
    }
    else
    {
        AfxMessageBox("NET_EHOME_S2C_SET_TERMINAL_NAME success insert");
    }
}


void CDlgInfoPublish::OnBnClickedBtnScreenShot()
{
    //����ƽ̨ר�ý���
    CheckInitParam();
    UpdateData(TRUE);
    NET_EHOME_SCREEN_SHOT_EX struShot = { 0 };
    NET_EHOME_BINARY_SEND_DATA struSendData = { 0 };
    struSendData.dwSize = sizeof(struSendData);
    struSendData.lpInBuffer = &struShot;
    struSendData.dwInBufferSize = sizeof(struShot);
    struShot.dwServerPort = m_dwServerPort;
    struShot.dwClientFdIndex = m_dwClientFd;
    memcpy(struShot.szKmsURL, m_strPicKmsAddr, m_strPicKmsAddr.GetLength());
    NET_EHOME_SCREEN_SHOT_RET struResult = { 0 };
    NET_EHOME_BINARY_RECV_DATA struRecvData = { 0 };
    struRecvData.dwSize = sizeof(struRecvData);
    struRecvData.lpOutBuffer = &struResult;
    struRecvData.dwOutBufferSize = sizeof(struResult);
    if (!NET_ECMS_STDBinaryCfg(m_lLoginID, NET_EHOME_S2C_SCREEN_SHOT, &struSendData, &struRecvData))
    {
        DWORD dwErr = NET_ECMS_GetLastError();
        AfxMessageBox("NET_EHOME_S2C_GET_SCREENSHOT failed, err=%d", dwErr);
    }
    else
    {
        NET_EHOME_SCREEN_SHOT_RET struShotRet = { 0 };
        memcpy(&struShotRet, struRecvData.lpOutBuffer, sizeof(NET_EHOME_SCREEN_SHOT_RET));
        m_dwCommunityFd = struShotRet.dwConnfd;
        m_dwTransIdentify = struShotRet.dwSeq;
        m_strPicIdentify = struShotRet.szUUID;
        m_strRetPicKmsAddr = struShotRet.szPicURL;
    }
    UpdateData(FALSE);
}


void CDlgInfoPublish::OnBnClickedBtnGetTerminalWorkStatus()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CheckInitParam();
    char szLan[128] = { 0 };
    UpdateData(TRUE);
    NET_EHOME_DEV_WORK_STATUS struDevWorkStatus = { 0 };
    NET_EHOME_BINARY_SEND_DATA struSendData = { 0 };
    struSendData.dwSize = sizeof(struSendData);

    NET_EHOME_BINARY_RECV_DATA struRecvData = { 0 };
    struRecvData.dwSize = sizeof(struRecvData);
    struRecvData.lpOutBuffer = &struDevWorkStatus;
    struRecvData.dwOutBufferSize = sizeof(struDevWorkStatus);
    if (!NET_ECMS_STDBinaryCfg(m_lLoginID, NET_EHOME_S2C_GET_WORKSTATUS, &struSendData, &struRecvData))
    {
        DWORD dwErr = NET_ECMS_GetLastError();
        AfxMessageBox("��ȡ����״̬ʧ��, err=%d", dwErr);
    }
    else
    {
        g_StringLanType(szLan, "��ȡ����״̬�ɹ�", "get work status success");
        AfxMessageBox("szLan");
    }
    UpdateData(FALSE);
}


void CDlgInfoPublish::OnBnClickedBtnGetTerminalName()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
}


//����˫�����ն�ʹ�ã��ظ���
void CDlgInfoPublish::OnBnClickedBtnScreenShotSecond()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CheckInitParam();
    UpdateData(TRUE);
    NET_EHOME_SCREEN_SHOT_EX struShot = { 0 };
    NET_EHOME_BINARY_SEND_DATA struSendData = { 0 };
    struSendData.dwSize = sizeof(struSendData);
    struSendData.lpInBuffer = &struShot;
    struSendData.dwInBufferSize = sizeof(struShot);
    struShot.dwServerPort = m_dwServerPort;
    struShot.dwClientFdIndex = m_dwClientFd;
    memcpy(struShot.szKmsURL, m_strPicKmsAddr, m_strPicKmsAddr.GetLength());
    NET_EHOME_SCREEN_SHOT_RET struResult = { 0 };
    NET_EHOME_BINARY_RECV_DATA struRecvData = { 0 };
    struRecvData.dwSize = sizeof(struRecvData);
    struRecvData.lpOutBuffer = &struResult;
    struRecvData.dwOutBufferSize = sizeof(struResult);
    if (!NET_ECMS_STDBinaryCfg(m_lLoginID, NET_EHOME_S2C_SCREEN_SHOT_SECOND, &struSendData, &struRecvData))
    {
        DWORD dwErr = NET_ECMS_GetLastError();
        AfxMessageBox("NET_EHOME_S2C_GET_SCREENSHOT failed, err=%d", dwErr);
    }
    else
    {
        NET_EHOME_SCREEN_SHOT_RET struShotRet = { 0 };
        memcpy(&struShotRet, struRecvData.lpOutBuffer, sizeof(NET_EHOME_SCREEN_SHOT_RET));
        m_dwCommunityFd = struShotRet.dwConnfd;
        m_dwTransIdentify = struShotRet.dwSeq;
        m_strPicIdentify = struShotRet.szUUID;
        m_strRetPicKmsAddr = struShotRet.szPicURL;
    }
    UpdateData(FALSE);
}
