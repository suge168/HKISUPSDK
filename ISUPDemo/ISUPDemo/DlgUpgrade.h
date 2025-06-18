#pragma once
#include "afxcmn.h"


// CDlgUpgrade �Ի���

class CDlgUpgrade : public CDialog
{
	DECLARE_DYNAMIC(CDlgUpgrade)

public:
	CDlgUpgrade(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgUpgrade();

// �Ի�������
	enum { IDD = IDD_DLG_UPGRADE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
    BOOL CheckInitParam();
	DECLARE_MESSAGE_MAP()
public:
    CString m_csPassword;
    DWORD m_dwPort;
    CString m_csUserName;
    CString m_csFileName;
    CIPAddressCtrl m_IPServer;
    CComboBox m_cmbDevType;
    int m_iDevIndex;
    CString m_csSubDevIndex;
    CString m_strDevType;
    afx_msg void OnBnClickedOk();
    int m_iDeviceIndex;
    int m_iChannel;
    LONG m_lLoginID;
    virtual BOOL OnInitDialog();
    void SetDevTypeValue();
};
