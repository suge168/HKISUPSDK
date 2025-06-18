#pragma once
#include "afxwin.h"
#include "afxcmn.h"
#include "EHomeDemoDlg.h"


// CDlgListen �Ի���
class CEHomeDemoDlg;
class CDlgListen : public CDialog
{
    DECLARE_DYNAMIC(CDlgListen)

public:
    CDlgListen(CWnd* pParent = NULL);   // ��׼���캯��
    virtual ~CDlgListen();

// �Ի�������
    enum { IDD = IDD_DLG_LOCAL_LISTEN };

protected:
    virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

    DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    CComboBox m_cmListenType;
    CComboBox m_cmPortNo;
    afx_msg void OnCbnSelchangeComboPortNo();
    CIPAddressCtrl m_ipAddress;
    DWORD m_wPort;
    afx_msg void OnBnClickedBtnStartListen();
    CComboBox m_cmLinkType;
    afx_msg void OnBnClickedBtnStopListen();
    void SetDefautVar(BOOL bSetCtrl, int index);

    CEHomeDemoDlg *m_pFatherDlg;
    int m_rdLinkType;
    afx_msg void OnBnClickedRadio1();
    BOOL m_byEncrypt;
};
