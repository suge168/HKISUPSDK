// DlgChanInfo.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgChanInfo.h"


// CDlgChanInfo �Ի���

IMPLEMENT_DYNAMIC(CDlgChanInfo, CDialog)

CDlgChanInfo::CDlgChanInfo(CWnd* pParent /*=NULL*/)
    : CDialog(CDlgChanInfo::IDD, pParent)
    , m_wPort(0)
    ,m_iDeviceIndex(-1)
    ,m_iChannelIndex(-1)
    , m_bEncrypt(FALSE)
{

}

CDlgChanInfo::~CDlgChanInfo()
{
}

void CDlgChanInfo::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_IPADDRESS1, m_ipAddress);
    DDX_Text(pDX, IDC_EDIT_PORT, m_wPort);
    DDX_Control(pDX, IDC_COMBO_PROTOCOL, m_cmProtocol);
    DDX_Control(pDX, IDC_COMBO_STREAM_TYPE, m_cmStreamType);
    DDX_Check(pDX, IDC_CHECK_ENCRYPT, m_bEncrypt);
    DDX_Control(pDX, IDC_COMBO_STREAM_FORMAT, m_cmStreamFormat);
}


BEGIN_MESSAGE_MAP(CDlgChanInfo, CDialog)
    ON_BN_CLICKED(IDC_BTN_SURE, &CDlgChanInfo::OnBnClickedBtnSure)
END_MESSAGE_MAP()


// CDlgChanInfo ��Ϣ�������

BOOL CDlgChanInfo::OnInitDialog()
{
    CDialog::OnInitDialog();
    m_cmProtocol.ResetContent();
    m_cmProtocol.AddString("TCP");
    m_cmProtocol.AddString("UDP");
    m_cmProtocol.AddString("HRUDP");
    m_cmProtocol.AddString("NPQ");
    if (8 == g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwLinkMode)
    {
        m_cmProtocol.SetCurSel(3);
    }
    else
    {
        m_cmProtocol.SetCurSel(g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwLinkMode);
    }

    char szLan[128] = {0};
    CString csTemp = _T("");

    m_cmStreamType.ResetContent();

    g_StringLanType(szLan, "������", "Main Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Sub Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Third Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "��������", "VOICELISTEN");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������������������", "Virtual Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Fifth Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Sixth Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Seventh Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Eighth Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "������", "Ninth Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    g_StringLanType(szLan, "ʮ����", "Tenth Stream");
    csTemp = szLan;
    m_cmStreamType.AddString(csTemp);

    m_cmStreamType.SetCurSel(g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwStreamType);

    DWORD dwIP = ntohl(inet_addr(g_pMainDlg->m_sLocalIP));
    if (g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].struIP.szIP[0] != '\0')
    {
        dwIP = ntohl(inet_addr(g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].struIP.szIP));
    }
    m_ipAddress.SetAddress(dwIP);

    m_wPort = g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].struIP.wPort;
    m_bEncrypt = g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].byEncrypt;

    //�����ص���������װ��ʽ. 0-PS��װ; 1-RTP��װ
    m_cmStreamFormat.ResetContent();
    m_cmStreamFormat.AddString("PS");
    m_cmStreamFormat.AddString("RTP");
    m_cmStreamFormat.SetCurSel(g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwStreamFormat);

    UpdateData(FALSE);
    return TRUE;
}

void CDlgChanInfo::OnBnClickedBtnSure()
{
    // TODO: �ڴ���ӿؼ�֪ͨ����������
    UpdateData(TRUE);
    if (3 == m_cmProtocol.GetCurSel())
    {
        g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwLinkMode = 8;
    }
    else
    {
        g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwLinkMode = m_cmProtocol.GetCurSel();
    }
    //g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwLinkMode = m_cmProtocol.GetCurSel();
    g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwStreamType = m_cmStreamType.GetCurSel();
    DWORD dwIP = 0;
    m_ipAddress.GetAddress(dwIP);
    memcpy(g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].struIP.szIP, IPToStr(dwIP), 16);
    g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].struIP.wPort = (WORD)m_wPort;
    g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].byEncrypt = (BYTE)m_bEncrypt;
    g_struDeviceInfo[m_iDeviceIndex].struChanInfo[m_iChannelIndex].dwStreamFormat = m_cmStreamFormat.GetCurSel();
    OnOK();
}
