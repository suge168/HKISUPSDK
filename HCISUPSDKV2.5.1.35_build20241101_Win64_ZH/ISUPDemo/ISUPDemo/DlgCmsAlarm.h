#pragma once
#include "afxcmn.h"


// CDlgCmsAlarm �Ի���

class CDlgCmsAlarm : public CDialog
{
	DECLARE_DYNAMIC(CDlgCmsAlarm)

public:
	CDlgCmsAlarm(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgCmsAlarm();

// �Ի�������
	enum { IDD = IDD_DLG_CMS_ALARM };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    DWORD m_wPort;
    CIPAddressCtrl m_IPAddr;
    afx_msg void OnBnClickedBtnOpen();
    NET_EHOME_AMS_ADDRESS m_struAmsAddr;
    int m_iDeviceIndex;
    afx_msg void OnBnClickedBtnClose();
};
