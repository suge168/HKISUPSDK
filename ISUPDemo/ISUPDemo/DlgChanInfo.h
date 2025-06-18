#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CDlgChanInfo �Ի���

class CDlgChanInfo : public CDialog
{
    DECLARE_DYNAMIC(CDlgChanInfo)

public:
    CDlgChanInfo(CWnd* pParent = NULL);   // ��׼���캯��
    virtual ~CDlgChanInfo();

// �Ի�������
    enum { IDD = IDD_DLG_CHANNEL_INFO };

protected:
    virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

    DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    CIPAddressCtrl m_ipAddress;
    CComboBox m_cmProtocol;
    CComboBox m_cmStreamType;
    int m_iDeviceIndex;
    int m_iChannelIndex;
    afx_msg void OnBnClickedBtnSure();
    DWORD m_wPort;  
    BOOL m_bEncrypt;
    CComboBox m_cmStreamFormat;
};
