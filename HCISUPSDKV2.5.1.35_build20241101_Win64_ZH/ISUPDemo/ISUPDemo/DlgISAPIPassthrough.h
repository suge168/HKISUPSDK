#pragma once
#include "afxwin.h"
#include "HCNetUtils.h"
#include "HTTPDefine.h"

// CDlgISAPIPassthrough 对话框

class CDlgISAPIPassthrough : public CDialog
{
	DECLARE_DYNAMIC(CDlgISAPIPassthrough)

public:
	CDlgISAPIPassthrough(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDlgISAPIPassthrough();

// 对话框数据
	enum { IDD = IDD_DLG_ISAPI_PT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnBnClickedBtnIsapiStartListen();
    afx_msg void OnBnClickedBtnIsapiStopListen();
public:
    LONG m_lUserID;
    int m_lHttpListenHandle;                   //HTTP监听服务句柄
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
