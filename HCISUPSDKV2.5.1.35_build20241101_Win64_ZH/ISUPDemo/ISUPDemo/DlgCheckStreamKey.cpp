// DlgCheckStreamKey.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgCheckStreamKey.h"
#include "afxdialogex.h"


// CDlgCheckStreamKey �Ի���

IMPLEMENT_DYNAMIC(CDlgCheckStreamKey, CDialog)

CDlgCheckStreamKey::CDlgCheckStreamKey(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgCheckStreamKey::IDD, pParent)
    , m_csKey(_T(""))
    ,m_lUserID(-1)
    ,m_iDeviceIndex(-1)
{

}

CDlgCheckStreamKey::~CDlgCheckStreamKey()
{
}

void CDlgCheckStreamKey::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    DDX_Text(pDX, IDC_EDIT_STREAM_KEY, m_csKey);
}


BEGIN_MESSAGE_MAP(CDlgCheckStreamKey, CDialog)
    ON_BN_CLICKED(IDOK, &CDlgCheckStreamKey::OnBnClickedOk)
END_MESSAGE_MAP()


// CDlgCheckStreamKey ��Ϣ�������
BOOL CDlgCheckStreamKey::OnInitDialog()
{
    CDialog::OnInitDialog();
    m_iDeviceIndex = g_pMainDlg->GetCurDeviceIndex();
    if (m_iDeviceIndex < 0)
    {
        AfxMessageBox("��ѡ��һ���豸");
        return FALSE;
    }
    m_lUserID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;
    if (m_lUserID < 0)
    {
        AfxMessageBox("���ȵ�½�豸");
        return FALSE;
    }

    return TRUE;
}

void CDlgCheckStreamKey::OnBnClickedOk()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CDialog::OnOK();
    char szStreamKey[33] = { 0 };
    sprintf_s(szStreamKey, m_csKey, m_csKey.GetBuffer());
    BOOL bRet = NET_ECMS_CheckStreamEncrypt(m_lUserID, szStreamKey);
    if (!bRet)
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_CheckStreamEncrypt");
    }
    else
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_CheckStreamEncrypt");
    }
    return;
}
