#pragma once
#include "afxwin.h"


// CDlgStreamEncrypt �Ի���

class CDlgStreamEncrypt : public CDialog
{
	DECLARE_DYNAMIC(CDlgStreamEncrypt)

public:
	CDlgStreamEncrypt(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgStreamEncrypt();
    LONG m_lUserID;
    int m_iDeviceIndex;
// �Ի�������
	enum { IDD = IDD_DLG_STREAM_ENCRYPT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
    
	DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    afx_msg void OnBnClickedOk();
    CComboBox m_cmbEncrypt;
    CString m_csNewKey;
    CString m_csOldKey;
};
