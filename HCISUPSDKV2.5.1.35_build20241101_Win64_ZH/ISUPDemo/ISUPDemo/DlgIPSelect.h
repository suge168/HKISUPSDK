#pragma once


// DlgIPSelect �Ի���

class DlgIPSelect : public CDialog
{
	DECLARE_DYNAMIC(DlgIPSelect)

public:
	DlgIPSelect(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~DlgIPSelect();

// �Ի�������
	enum { IDD = IDD_DLG_IP_SELECT };


    void GetLocalIP();//ֻ�ܻ�ȡ����ipv4��ַ
    void GetLocalIPEx();//���Ի�ȡ����ipv4��ipv6��ַ
    void DoGetIP();

protected:
    virtual BOOL OnInitDialog();
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnBnClickedOk();

    int m_nPort;
    CString m_csIP;

private:
    CComboBox m_cmIP;
};
