#pragma once


// CDlgInfoPubTerCfg �Ի���

class CDlgInfoPubTerCfg : public CDialogEx
{
	DECLARE_DYNAMIC(CDlgInfoPubTerCfg)

public:
	CDlgInfoPubTerCfg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgInfoPubTerCfg();
	BOOL CheckInitParam();
// �Ի�������
	enum { IDD = IDD_DLG_SETPLAYERPARAM };
	int m_iDeviceIndex;
	LONG m_lLoginID;
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBtnSetplayerparam();
	afx_msg void OnBnClickedBtnGetplayerparam();
};
