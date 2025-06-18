// DlgIFrame.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgIFrame.h"
#include "afxdialogex.h"


// CDlgIFrame �Ի���

IMPLEMENT_DYNAMIC(CDlgIFrame, CDialog)

CDlgIFrame::CDlgIFrame(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgIFrame::IDD, pParent)
    , m_lUserID(-1)
    , m_iDeviceIndex(-1)
    , m_iChanIndex(-1)
    , m_iChannelNo(-1)
{

}

CDlgIFrame::~CDlgIFrame()
{
}

void CDlgIFrame::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_COMBO_STREAM_TYPE, m_cmbStreamType);
}


BEGIN_MESSAGE_MAP(CDlgIFrame, CDialog)
    ON_BN_CLICKED(IDOK, &CDlgIFrame::OnBnClickedOk)
END_MESSAGE_MAP()


// CDlgIFrame ��Ϣ�������
BOOL CDlgIFrame::OnInitDialog()
{
  
    CDialog::OnInitDialog();
    m_lUserID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;
    m_iChanIndex = g_pMainDlg->GetCurChanIndex();
    m_iChannelNo=g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChanIndex].iChannelNO;
    return TRUE;
}

void CDlgIFrame::OnBnClickedOk()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    NET_EHOME_MAKE_I_FRAME struIFrame = { 0 };
    struIFrame.iChannel = m_iChannelNo;
    struIFrame.dwStreamType = m_cmbStreamType.GetCurSel() + 1;
    if (!NET_ECMS_MakeIFrame(m_lUserID, &struIFrame))
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_MakeIFrame failed");
        return ;
    }
    else
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_MakeIFrame success");
        return;
    }
}
