#pragma once
#include "afxwin.h"
#include "Resource.h"
#include <iostream>
#include <string>
#include "HTTPDefine.h"
using namespace std;
// CDlgWebConfig �Ի���
#define RECV_BUF 4096*4
#define SOCKET_NUM 2048
#define HTTP_RECV_LEN 1024*1024*10

struct ClientInfo
{
    SOCKET sockClient;
    SOCKADDR_IN addrClient;
};

class CDlgWebConfig : public CDialog
{
	DECLARE_DYNAMIC(CDlgWebConfig)

public:
	CDlgWebConfig(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgWebConfig();

// �Ի�������
	enum { IDD = IDD_DLG_WEB_CONFIG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    virtual BOOL OnInitDialog();
    afx_msg void OnBnClickedButtonTest();
    afx_msg void OnBnClickedButtonClose();
private:
    BOOL Start();
    VOID Stop();
    void ListenScoket();
    BOOL StartNginx();
    BOOL StopNginx();
    string GetExePath();
    BOOL ProcessCmd(const char* strCmd, BOOL bWait = FALSE);
    BOOL SendWebReq(char* pInput, int inputlength,char* pOutput, int& iReturnLen);
    static DWORD WINAPI ListenThread(LPVOID lpParameter);
    static DWORD WINAPI WorkThread(LPVOID lpParameter);
private:
    LONG m_iDeviceIndex;
    LONG m_lLoginID;
    BOOL m_bStartNginx;
    BOOL m_bExit;
    string  m_strExeOrder;
    string m_strIndexPath;
    int m_iPort;
    int m_iSockConn;//�������ӵ���Ŀ
    HANDLE m_hMutex;
    ClientInfo m_Client[SOCKET_NUM];
    SOCKET m_sockListen;
    HANDLE m_hWorkHandle;
    HANDLE m_hListenHandle;
    char* m_pRecvData;
};
