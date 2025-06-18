// DlgWebConfig.cpp : ʵ���ļ�
//
#include "stdafx.h"
#include "DlgWebConfig.h"
#include "EHomeDemo.h"
#include <WinSock2.h>
#include <stdio.h>
#pragma comment(lib,"ws2_32.lib")
// CDlgWebConfig �Ի���


IMPLEMENT_DYNAMIC(CDlgWebConfig, CDialog)

DWORD WINAPI CDlgWebConfig::ListenThread(LPVOID lpParameter)
{
    CDlgWebConfig* pWeb = (CDlgWebConfig*)lpParameter;
    if (pWeb == NULL)
    {
        return 0;
    }
    pWeb->ListenScoket();
    return 0;
}
const char *strnstr(const char* pSrcBuff, const char* pDstBuff, int dwSrcBuffLen)
{
    if (NULL == pSrcBuff)
    {
        return NULL;
    }

    if (NULL == pDstBuff)
    {
        return pSrcBuff;
    }

    int  dwDstLen = (int)strlen(pDstBuff);
    if (0 == dwDstLen)
    {
        return pSrcBuff;
    }

    const char *pTemp = pSrcBuff;
    int dwRetLen = ((int)strlen(pSrcBuff) > dwSrcBuffLen) ? dwSrcBuffLen : (int)strlen(pSrcBuff);

    for (; dwRetLen >= dwDstLen && *pTemp != '\0'; dwRetLen--)
    {
        if (*pTemp == *pDstBuff) //���ٱȽϴ���
        {
            if (memcmp(pTemp, pDstBuff, dwDstLen) == 0)
            {
                return pTemp;
            }
        }
        pTemp++;
    }

    return NULL;
}

BOOL GetMiddleString(const char *pInBuff, int dwInLen, const char *pPreStr, const char *pSufStr, char *pOutBuf, int dwOutLen)
{
    if (NULL == pInBuff || NULL == pOutBuf)
    {
        return FALSE;
    }

    if (NULL == pPreStr && NULL == pSufStr)
    {
        return FALSE;
    }

    const char *pStartPos = NULL;
    if (NULL == pPreStr)
    {
        //���ǰ��Ϊ���ַ������������ʼλ��Ϊ�տ�ʼ��λ��
        pStartPos = pInBuff;
    }
    else
    {
        //��Ϊ�����ҵ���Ӧλ��
        pStartPos = strnstr(pInBuff, pPreStr, dwInLen);
        if (NULL == pStartPos)
        {
            return FALSE;
        }
        pStartPos += strlen(pPreStr);
    }

    int dwRetLen = 0;
    if (NULL == pSufStr)
    {
        //�������Ϊ�գ����ʾһֱ����������λ��
        dwRetLen = (int)strlen(pStartPos);
    }
    else
    {
        const char *pEndPos = strnstr(pStartPos, pSufStr, dwInLen - (int)(pStartPos - pInBuff));
        if (NULL == pEndPos)
        {
            return FALSE;
        }

        dwRetLen = (int)(pEndPos - pStartPos);
    }

    if (dwOutLen <= dwRetLen)
    {
        return FALSE;
    }
    strncpy(pOutBuf, pStartPos, dwRetLen);
    strncpy(pOutBuf + dwRetLen, "\0", 1); //����һ��������

    return TRUE;
}

BOOL GetValueWithRecursive(char *pRecvBuff, const char *pInBuffer, int dwInLen, char *pOutBuff, int dwOutLen)
{
    if (strlen(pInBuffer) == 0)
    {
        return FALSE;
    }

    //�ݹ��˳�����1���Ҳ�����Ӧ���ַ�����
    char *pSubStr = strstr(pRecvBuff, pInBuffer);
    if (NULL == pSubStr)
    {
        return FALSE;
    }
    if (!GetMiddleString(pSubStr, (int)strlen(pSubStr), ":", "\r\n", pOutBuff, dwOutLen))
    {
        //����������ˣ���ʧ�ܣ�ֻ���ǻ�����������
        return FALSE;
    }
    //��ǰ��Ŀո�ȥ��
    //DelPreSpace(pOutBuff, dwOutLen);
    return TRUE;

}
DWORD WINAPI CDlgWebConfig::WorkThread(LPVOID lpParameter)
{
    CDlgWebConfig* pWeb = (CDlgWebConfig*)lpParameter;
    if (pWeb == NULL)
    {
        return 0;
    }
    FD_SET fdRead;
    int iRet = 0;//��¼���ͻ��߽��ܵ��ֽ���
    TIMEVAL tv;//���ó�ʱ�ȴ�ʱ��
    tv.tv_sec = 1;
    tv.tv_usec = 0;
    char *pRecvBuf = new char[2048*1024];

    while (!pWeb->m_bExit)
    {
        FD_ZERO(&fdRead);
        for (int i = 0; i < pWeb->m_iSockConn; i++)
        {
            FD_SET(pWeb->m_Client[i].sockClient, &fdRead);
        }
        for (int i = 0; i < pWeb->m_iSockConn; i++)
        {
            int iHeadLen = 0;
            int iContentLen = 0;
            int iRecvLen = 0;
            char szContentLen[16] = { 0 };
            char *pBeginRecvBuf = pRecvBuf;
            //ֻ����read�¼����������滹�ǻ��ж�д��Ϣ���͵�
            iRet = select(0, &fdRead, NULL, NULL, &tv);

            if (iRet == 0)
            {//û�����ӻ���û�ж��¼�
                continue;
            }
            if (FD_ISSET(pWeb->m_Client[i].sockClient, &fdRead))
            {
                memset(pRecvBuf, 0, 2048 * 1024);
                while (TRUE)
                {
                    iRet = recv(pWeb->m_Client[i].sockClient, pBeginRecvBuf, 2048 * 1024, 0);
                    if (iRet == 0 || (iRet == SOCKET_ERROR && WSAGetLastError() == WSAECONNRESET))
                    {
                        WaitForSingleObject(pWeb->m_hMutex, INFINITE);//�ȴ������� 
                        closesocket(pWeb->m_Client[i].sockClient);

                        if (i <= pWeb->m_iSockConn - 1)
                        {
                            //��ʧЧ��sockClient�޳�������������һ������ȥ
                            --pWeb->m_iSockConn;
                            pWeb->m_Client[i].addrClient = pWeb->m_Client[pWeb->m_iSockConn].addrClient;
                            pWeb->m_Client[i].sockClient = pWeb->m_Client[pWeb->m_iSockConn].sockClient;
                            i--;
                        }
                        ReleaseMutex(pWeb->m_hMutex);
                        break;
                    }
                    else if (iRet > 0)
                    {
                        pBeginRecvBuf += iRet;
                        iRecvLen += iRet;
                        //��鱨��ͷ
                        char *pos = strstr(pRecvBuf, "\r\n\r\n");
                        if (!pos)
                        {
                            continue;
                        }
                        //����ͷ����
                        iHeadLen = int(pos + strlen("\r\n\r\n") - pRecvBuf);
                        //����content����
                        if (GetValueWithRecursive(pRecvBuf, "Content-Length", (int)strlen("Content-Length"), szContentLen, 16))
                        {
                            iContentLen = atoi(szContentLen);
                            //����body
                            if (iRecvLen >= iContentLen + iHeadLen)
                            {
                                int iLen = 0;
                                if (strlen(pRecvBuf) > 0)
                                {
                                    char *pData = new char[HTTP_RECV_LEN];
                                    if (pData == NULL)
                                    {
                                        continue;
                                    }
                                    memset(pData, 0, HTTP_RECV_LEN);

                                    pWeb->SendWebReq(pRecvBuf, iRecvLen, pData, iLen);
                                    if (iLen > 0)
                                    {
                                        send(pWeb->m_Client[i].sockClient, pData, iLen, 0);
                                    }

                                    if (pData != NULL)
                                    {
                                        delete[] pData;
                                        pData = NULL;
                                    }
                                }
                                break;
                            }
                            continue;
                        }
                        else
                        {
                            //û�ҵ�content��ֱ�ӷ�������
                            int iLen = 0;
                            if (strlen(pRecvBuf) > 0)
                            {
                                char *pData = new char[HTTP_RECV_LEN];
                                if (pData == NULL)
                                {
                                    continue;
                                }
                                memset(pData, 0, HTTP_RECV_LEN);

                                pWeb->SendWebReq(pRecvBuf, iRecvLen, pData, iLen);
                                if (iLen > 0)
                                {
                                    send(pWeb->m_Client[i].sockClient, pData, iLen, 0);
                                }

                                if (pData != NULL)
                                {
                                    delete[] pData;
                                    pData = NULL;
                                }
                            }
                            break;
                        }
 
                        
                    }
                    else
                    {
                        break;
                        //cout << "TCP ����˶Ͽ�����......" << endl;
                    }
                }



            }
        }
        Sleep(10);
    }
    return 0;
}



CDlgWebConfig::CDlgWebConfig(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgWebConfig::IDD, pParent)
    , m_iDeviceIndex(-1)
    , m_lLoginID(-1)
    ,m_bStartNginx(FALSE)
    , m_bExit(FALSE)
    , m_iPort(18081)
    , m_iSockConn(0)
    , m_hMutex(nullptr)
    , m_sockListen(0)
    , m_hWorkHandle(nullptr)
    , m_hListenHandle(nullptr)
    , m_pRecvData(nullptr)
{
    memset(m_Client, 0, sizeof(m_Client));
}

CDlgWebConfig::~CDlgWebConfig()
{
    Stop();
}

void CDlgWebConfig::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDlgWebConfig, CDialog)
    ON_BN_CLICKED(IDC_BUTTON_TEST, &CDlgWebConfig::OnBnClickedButtonTest)
    ON_BN_CLICKED(IDC_BUTTON_CLOSE, &CDlgWebConfig::OnBnClickedButtonClose)
END_MESSAGE_MAP()


// CDlgWebConfig ��Ϣ�������


BOOL CDlgWebConfig::OnInitDialog()
{
    // TODO:  �ڴ���Ӷ���ĳ�ʼ��

    CDialog::OnInitDialog();
    m_iDeviceIndex = g_pMainDlg->GetCurDeviceIndex();
    if (m_iDeviceIndex == -1)
    {
        AfxMessageBox("please select a device.");
        return FALSE;
    }

    m_lLoginID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;
    if (m_lLoginID < 0)
    {
        AfxMessageBox("please login device first.");
        return FALSE;
    }
    DWORD dwThreadIDRecv = 0;
    m_hListenHandle = CreateThread(NULL, 0, ListenThread, this, 0, &dwThreadIDRecv);//���������ַ���Ϣ�Ľ���
    if (nullptr == m_hListenHandle)
    {
        return FALSE;
    }

    m_pRecvData = new char[HTTP_RECV_LEN];
    if (nullptr == m_pRecvData)
    {
        CloseHandle(m_hListenHandle);
        m_hListenHandle = nullptr;
        return FALSE;
    }
    Start();
    return TRUE;  // return TRUE unless you set the focus to a control
    // �쳣:  OCX ����ҳӦ���� FALSE
}

void CDlgWebConfig::ListenScoket()
{
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    m_sockListen = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);//SOCK_STREAM

    SOCKADDR_IN addrSrv;
    addrSrv.sin_addr.S_un.S_addr = INADDR_ANY;
    addrSrv.sin_family = AF_INET;
    addrSrv.sin_port = htons(m_iPort);

    if (bind(m_sockListen, (SOCKADDR*)&addrSrv, sizeof(SOCKADDR)) == SOCKET_ERROR)
    {
         g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 2, "bind error![%d]", GetLastError());
    }
    //��ʼ����  
    if (listen(m_sockListen, 64) == SOCKET_ERROR)
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 2, "listen error![%d]", GetLastError());
        return;
    }

    m_hMutex = CreateMutex(NULL, FALSE, NULL);

    DWORD dwThreadIDRecv = 0;

    m_hWorkHandle = CreateThread(NULL, 0, WorkThread, this, 0, &dwThreadIDRecv);//���������ַ���Ϣ�Ľ���
    if (nullptr == m_hWorkHandle)
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 2, "Create work thread failed");
        getchar();
        return ;
    }

    SOCKET sockClient;
    SOCKADDR_IN addrClient;
    int nLenAddrClient = sizeof(SOCKADDR);//������0���Ի����˰�����ҳ�����
    
    while (!m_bExit)
    {
        // WaitForSingleObject(hMutex, INFINITE);//�ȴ������� 
        sockClient = accept(m_sockListen, (SOCKADDR*)&addrClient, &nLenAddrClient);//����������һ��Ҫ����addrClient��С��ʼ��

        WaitForSingleObject(m_hMutex, INFINITE);//�ȴ������� 
        if (sockClient != INVALID_SOCKET)
        {
            int uiRcvBuf = 128 * 1024;
            setsockopt(sockClient, SOL_SOCKET, SO_SNDBUF, (char*)&uiRcvBuf, sizeof(int));
            m_Client[m_iSockConn].addrClient = addrClient;//�������Ӷ˵�ַ��Ϣ
            m_Client[m_iSockConn].sockClient = sockClient;//���������߶���
            m_iSockConn++;
        }
        ReleaseMutex(m_hMutex);
        Sleep(10);
    }
}

void CDlgWebConfig::OnBnClickedButtonTest()
{
     char szEhomeID[MAX_DEVICE_ID_LEN + 1] = { 0 };
    memcpy(szEhomeID, g_struDeviceInfo[m_iDeviceIndex].byDeviceID, MAX_DEVICE_ID_LEN);
    string strUrl = "http://127.0.0.1:18082/index?deviceId=" + string(szEhomeID);
    ShellExecute(NULL, _T("open"), _T("iexplore.exe"), _T(strUrl.c_str()), NULL, SW_SHOW);//
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
}

void CDlgWebConfig::OnBnClickedButtonClose()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    Stop();
    CDlgWebConfig::OnCancel();
}

BOOL CDlgWebConfig::Start()
{
    //����nginx
    if (!m_bStartNginx)
    {
        if (!StartNginx())
        {
            return FALSE;
        }
        m_bStartNginx = TRUE;
    }
    return TRUE;
}

VOID  CDlgWebConfig::Stop()
{
    m_bExit = TRUE;
    if (m_bStartNginx)
    {
        StopNginx();
        m_bStartNginx = FALSE;
    }

    if (m_sockListen != 0)
    {
        closesocket(m_sockListen);
        m_sockListen = 0;
    }

    for (int i = 0; i < SOCKET_NUM; i++)
    {
        if (m_Client[i].sockClient != 0)
        {
            closesocket(m_Client[i].sockClient);
            m_Client[i].sockClient = 0;
        }
    }
    
    if (nullptr != m_hListenHandle)
    {
        CloseHandle(m_hListenHandle);
        m_hListenHandle = nullptr;
    }

    if (nullptr != m_hWorkHandle)
    {
        CloseHandle(m_hWorkHandle);
        m_hWorkHandle = nullptr;
    }

    if (nullptr != m_pRecvData)
    {
        delete[] m_pRecvData;
        m_pRecvData = nullptr;
    }
    return;
}

/** @fn BOOL CDlgWebConfig::StartNginx()
 *  @brief ����nginx
 *  @return BOOL
 */
BOOL CDlgWebConfig::StartNginx()
{
    string strCurPath = GetExePath();
    m_strExeOrder = "\"\"" + strCurPath + "nginx/ISUPDemo-nginx\" -p \"" + strCurPath + "nginx\"";
    return ProcessCmd(m_strExeOrder.c_str());
}

/** @fn BOOL CDlgWebConfig::StopNginx()
 *  @brief �ر�nginx
 *  @return BOOL
 */
BOOL CDlgWebConfig::StopNginx()
{

    string strCmd = m_strExeOrder + " -s quit";
    strCmd += "\"";
    ProcessCmd(strCmd.c_str(), TRUE); //�˳���ʱ�����ȴ�ִ����ɣ������µ�pid�ᱻ�첽ɾ��
    ProcessCmd("taskkill /IM ISUPDemo-nginx.exe /T /F", TRUE);
    return TRUE;
}

/** @fn std::string CDlgWebConfig::GetExePath()
 *  @brief ��ȡ��ǰexe����λ��
 *  @return std::string
 */
string CDlgWebConfig::GetExePath()
{
    char szPath[512] = { 0 };
    GetModuleFileName(NULL, szPath, 512);

    string strDBPath = szPath;
    int iPos;
    while ((iPos = (int)strDBPath.find("\\")) != -1)
    {
        strDBPath = strDBPath.replace(iPos, 1, "/");
    }
    strDBPath = strDBPath.substr(0, strDBPath.find_last_of("/") + 1);

    return  strDBPath;
}

/** @fn BOOL CDlgWebConfig::ProcessCmd(const char* strCmd, BOOL bWait)
 *  @brief ����CMD������
 *  @param (in)   const char * strCmd   
 *  @param (in)   BOOL bWait   
 *  @return BOOL
 */
BOOL CDlgWebConfig::ProcessCmd(const char* strCmd, BOOL bWait)
{
    char szCmdLine[MAX_PATH * 2] = { 0 };
    if (!GetSystemDirectory(szCmdLine, MAX_PATH))
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 2, "GetSystemDirectory error[%d]", GetLastError());
        return  FALSE;
    }
    _tcscat_s(szCmdLine, MAX_PATH * 2, _T("\\cmd.exe /c "));
    _tcscat_s(szCmdLine, MAX_PATH * 2, strCmd);

    STARTUPINFO         StartupInfo = { 0 };
    PROCESS_INFORMATION ProcessInfo = { 0 };
    StartupInfo.cb = sizeof(STARTUPINFO);
    StartupInfo.dwFlags = STARTF_USESHOWWINDOW;
    StartupInfo.wShowWindow = SW_HIDE;

    if (!CreateProcess(NULL, szCmdLine, NULL, NULL, FALSE, CREATE_NO_WINDOW, NULL, NULL, &StartupInfo, &ProcessInfo))
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 2, "CreateProcess error[%d]", GetLastError());
        return  FALSE;
    }
    else
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_SUCC_T, 1, "CreateProcess [%s] success", szCmdLine);
        if (bWait)
        {
            WaitForSingleObject(ProcessInfo.hProcess, INFINITE);
        }
        if (ProcessInfo.hProcess)
        {
            CloseHandle(ProcessInfo.hProcess);
        }
        if (ProcessInfo.hThread)
        {
            CloseHandle(ProcessInfo.hThread);
        }
        return TRUE;
    }
}

/** @fn BOOL CDlgWebConfig::SendWebReq(char* pInput, char* pOutput, int& iReturnLen)
 *  @brief  �������ݵ�ISUPSDK��
 *  @param (in)   char * pInput   ��������
 *  @param (in)   char * pOutput   ��������
 *  @param (in)   int & iReturnLen   �������ݵĳ���
 *  @return BOOL
 */
BOOL CDlgWebConfig::SendWebReq(char* pInput, int inputlength,char* pOutput, int& iReturnLen)
{
    NET_EHOME_HTTP_PARAM struParam = { 0 };
    memset(m_pRecvData, 0, sizeof(m_pRecvData));
    struParam.pInBuffer = pInput;
    struParam.dwInSize = inputlength;
    struParam.pOutBuffer = m_pRecvData;
    struParam.dwOutSize = HTTP_RECV_LEN;
    struParam.dwRecvTimeOut = 30000; //5s

    char szLan[1024] = { 0 };
    if (m_lLoginID != -1 && !NET_ECMS_HTTPConfig(m_lLoginID, &struParam))
    {
        sprintf_s(szLan, "NET_ECMS_HTTPConfig error:%d", NET_ECMS_GetLastError());
        g_pMainDlg->AddLog(-1, OPERATION_FAIL_T, 0, szLan);
        return FALSE;
    }
    else
    {
        memcpy(pOutput, m_pRecvData, struParam.dwReturnedXMLLen);
        iReturnLen = struParam.dwReturnedXMLLen;
        sprintf_s(szLan, "NET_ECMS_HTTPConfig success");
    }

    return TRUE;
}
