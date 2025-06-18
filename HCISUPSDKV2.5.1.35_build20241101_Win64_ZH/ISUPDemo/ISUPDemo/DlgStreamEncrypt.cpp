// DlgStreamEncrypt.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgStreamEncrypt.h"
#include "afxdialogex.h"


// CDlgStreamEncrypt �Ի���

IMPLEMENT_DYNAMIC(CDlgStreamEncrypt, CDialog)

CDlgStreamEncrypt::CDlgStreamEncrypt(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgStreamEncrypt::IDD, pParent)
    , m_csNewKey(_T(""))
    , m_csOldKey(_T(""))
{
    m_lUserID = -1;
    m_iDeviceIndex = -1;
}

CDlgStreamEncrypt::~CDlgStreamEncrypt()
{
}

void CDlgStreamEncrypt::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_COMBO1, m_cmbEncrypt);
    DDX_Text(pDX, IDC_EDIT_NEW_CODE, m_csNewKey);
    DDX_Text(pDX, IDC_EDIT_OLD_CODE, m_csOldKey);
}


BEGIN_MESSAGE_MAP(CDlgStreamEncrypt, CDialog)
    ON_BN_CLICKED(IDOK, &CDlgStreamEncrypt::OnBnClickedOk)
END_MESSAGE_MAP()


// CDlgStreamEncrypt ��Ϣ�������
BOOL CDlgStreamEncrypt::OnInitDialog()
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

void CDlgStreamEncrypt::OnBnClickedOk()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CDialog::OnOK();
    NET_EHOME_STREAM_PASSWORD struStreamPassword = { 0 };
    struStreamPassword.byEnable = m_cmbEncrypt.GetCurSel();
    if (struStreamPassword.byEnable==1)
    {
        strncpy((char*)struStreamPassword.byNewKey, m_csNewKey.GetBuffer(), m_csNewKey.GetLength());
    }
    else if (struStreamPassword.byEnable==2)
    {
        strncpy((char*)struStreamPassword.byNewKey, m_csNewKey.GetBuffer(), m_csNewKey.GetLength());
        strncpy((char*)struStreamPassword.byOldKey, m_csOldKey.GetBuffer(), m_csOldKey.GetLength());
    }
    struStreamPassword.byEnable;
    if (!NET_ECMS_SetStreamEncrypt(m_lUserID, &struStreamPassword))
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_SetStreamEncrypt failed");
        return;
    }
    else
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_SetStreamEncrypt success");
        return;
    }
}
