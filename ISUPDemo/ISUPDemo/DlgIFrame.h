#pragma once


// CDlgIFrame �Ի���

class CDlgIFrame : public CDialog
{
	DECLARE_DYNAMIC(CDlgIFrame)

public:
	CDlgIFrame(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgIFrame();

// �Ի�������
	enum { IDD = IDD_DLG_IFRAME };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
    
	DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    LONG m_lUserID;
    int m_iDeviceIndex;
    int m_iChanIndex;
    int m_iChannelNo;
    CComboBox m_cmbStreamType;
    afx_msg void OnBnClickedOk();
};
