#pragma once
#include "afxwin.h"
#include "HCNetUtils.h"
#include "HTTPDefine.h"

// CDlgISAPIPassthrough �Ի���

class CDlgISAPIPassthrough : public CDialog
{
	DECLARE_DYNAMIC(CDlgISAPIPassthrough)

public:
	CDlgISAPIPassthrough(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgISAPIPassthrough();

// �Ի�������
	enum { IDD = IDD_DLG_ISAPI_PT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnBnClickedBtnIsapiStartListen();
    afx_msg void OnBnClickedBtnIsapiStopListen();
public:
    LONG m_lUserID;
    int m_lHttpListenHandle;                   //HTTP����������
    int m_iDeviceIndex;
    CIPAddressCtrl m_ipAddress;
    int m_iPort;
    static void CALLBACK HttpServerDataCB(LONG iCallbackHandle, void* pData, void* pUserData);
    BOOL ProcessISAPIRequest(int iCallbackHandle, NET_UTILS_HTTP_SERVER_REQ_PARAM& struReq, NET_UTILS_HTTP_SERVER_DATA_CB* pStruParam, char* strErr);
public:
    CComboBox m_cmbCmdType;
    CString m_sCond;
    CString m_sInput;
    CString m_sOutput;
    CString m_sUrl;
    afx_msg void OnClickedBtnCommand();
    virtual BOOL OnInitDialog();
    DWORD m_dwTimeout;
};
