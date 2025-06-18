#pragma once


// CDlgISAPIConfig �Ի���

class CDlgISAPIConfig : public CDialogEx
{
	DECLARE_DYNAMIC(CDlgISAPIConfig)

public:
	CDlgISAPIConfig(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgISAPIConfig();

// �Ի�������
	enum { IDD = IDD_DLG_ISAPI_CFG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()


public:
    LONG m_lUserID;
    int m_iDeviceIndex;
public:
    CString m_sInput;
    CString m_sOutput;
    CString m_sUrl;
	CString m_sPath;
    afx_msg void OnBnClickedBtnCommand();
    virtual BOOL OnInitDialog();
};
