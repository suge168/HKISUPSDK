#pragma once
#include "afxwin.h"


// CDlgCaptureCfg �Ի���

class CDlgCaptureCfg : public CDialogEx
{
	DECLARE_DYNAMIC(CDlgCaptureCfg)

public:
	CDlgCaptureCfg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgCaptureCfg();

// �Ի�������
	enum { IDD = IDD_DLG_TRIGGER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()

public:

    BOOL CheckInitParam();
    int m_iDeviceIndex;
    LONG m_lLoginID;
    DWORD m_dwChannelNo;
    int m_iChanIndex;
    BYTE m_arrDigChan[1024];
    BYTE m_arrAloChan[1024];
    int m_iDigNum;
    int m_iAloNum;
public:
    afx_msg void OnBnClickedButtonAnalogchan();
    afx_msg void OnBnClickedButtonDigitalchan();
    afx_msg void OnBnClickedBtnGetCap();
    afx_msg void OnBnClickedBtnCapSet();
    int m_iAlarmType;
    int m_iAlarmChan;
    int m_iSendTo;
    BOOL m_bCap;
    int m_iResolution;
    int m_iQuality;
    int m_iTime;
    virtual BOOL OnInitDialog();
    int m_iAlo;
    int m_iDig;
    CComboBox m_cmbSendTo;
};
