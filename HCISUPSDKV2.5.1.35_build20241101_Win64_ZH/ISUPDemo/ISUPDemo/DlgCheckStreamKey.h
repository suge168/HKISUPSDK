#pragma once


// CDlgCheckStreamKey �Ի���

class CDlgCheckStreamKey : public CDialog
{
	DECLARE_DYNAMIC(CDlgCheckStreamKey)

public:
	CDlgCheckStreamKey(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgCheckStreamKey();
    LONG m_lUserID;
    int m_iDeviceIndex;
// �Ի�������
    enum { IDD = IDD_DLG_CHCKE_STREAM_KEY };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    CString m_csKey;
    afx_msg void OnBnClickedOk();
};
