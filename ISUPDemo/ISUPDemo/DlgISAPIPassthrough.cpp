// DlgISAPIPassthrough.cpp : 实现文件
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgISAPIPassthrough.h"
#include "Public/Convert/Convert.h"
#include "afxdialogex.h"
#include <dbghelp.h>

// CDlgISAPIPassthrough 对话框

IMPLEMENT_DYNAMIC(CDlgISAPIPassthrough, CDialog)

CDlgISAPIPassthrough::CDlgISAPIPassthrough(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgISAPIPassthrough::IDD, pParent)
    , m_sCond(_T(""))
    , m_iPort(18243)
    , m_sInput(_T(""))
    , m_sOutput(_T(""))
    , m_sUrl(_T(""))
    , m_lHttpListenHandle(-1)
    , m_dwTimeout(5000)
{
    m_lUserID = -1;
    m_iDeviceIndex = -1;
}

CDlgISAPIPassthrough::~CDlgISAPIPassthrough()
{
}

int GenerateMiniDump1(PEXCEPTION_POINTERS pExceptionPointers)
{
    // 定义函数指针
    typedef BOOL(WINAPI * MiniDumpWriteDumpT)(
        HANDLE,
        DWORD,
        HANDLE,
        MINIDUMP_TYPE,
        PMINIDUMP_EXCEPTION_INFORMATION,
        PMINIDUMP_USER_STREAM_INFORMATION,
        PMINIDUMP_CALLBACK_INFORMATION
        );
    // 从 "DbgHelp.dll" 库中获取 "MiniDumpWriteDump" 函数
    MiniDumpWriteDumpT pfnMiniDumpWriteDump = NULL;
    HMODULE hDbgHelp = LoadLibrary("DbgHelp.dll");
    if (NULL == hDbgHelp)
    {
        return EXCEPTION_CONTINUE_EXECUTION;
    }
    pfnMiniDumpWriteDump = (MiniDumpWriteDumpT)GetProcAddress(hDbgHelp, "MiniDumpWriteDump");

    if (NULL == pfnMiniDumpWriteDump)
    {
        FreeLibrary(hDbgHelp);
        return EXCEPTION_CONTINUE_EXECUTION;
    }
    // 创建 dmp 文件件
    TCHAR szFileName[MAX_PATH] = { 0 };
    strcpy_s(szFileName, "Dump1.dmp");
    HANDLE hDumpFile = CreateFile(szFileName, GENERIC_READ | GENERIC_WRITE,
        FILE_SHARE_WRITE | FILE_SHARE_READ, 0, CREATE_ALWAYS, 0, 0);
    if (INVALID_HANDLE_VALUE == hDumpFile)
    {
        FreeLibrary(hDbgHelp);
        return EXCEPTION_CONTINUE_EXECUTION;
    }
    // 写入 dmp 文件
    MINIDUMP_EXCEPTION_INFORMATION expParam;
    expParam.ThreadId = GetCurrentThreadId();
    expParam.ExceptionPointers = pExceptionPointers;
    expParam.ClientPointers = FALSE;
    pfnMiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(),
        hDumpFile, MiniDumpWithDataSegs, (pExceptionPointers ? &expParam : NULL), NULL, NULL);
    // 释放文件
    CloseHandle(hDumpFile);
    FreeLibrary(hDbgHelp);
    return EXCEPTION_EXECUTE_HANDLER;
}
LONG WINAPI ExceptionFilter1(LPEXCEPTION_POINTERS lpExceptionInfo)
{
    // 这里做一些异常的过滤或提示
    if (IsDebuggerPresent())
    {
        return EXCEPTION_CONTINUE_SEARCH;
    }
    return GenerateMiniDump1(lpExceptionInfo);

}


void CDlgISAPIPassthrough::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_COMBO_CMD_TYPE, m_cmbCmdType);
    DDX_Text(pDX, IDC_EDIT_COND, m_sCond);
    DDX_Text(pDX, IDC_EDIT_INPUT, m_sInput);
    DDX_Text(pDX, IDC_EDIT_OUTPUT, m_sOutput);
    DDX_Text(pDX, IDC_EDIT_URL, m_sUrl);
    DDX_Text(pDX, IDC_EDIT2, m_dwTimeout);
    DDX_Text(pDX, IDC_EDIT_PORT, m_iPort);
    DDX_Control(pDX, IDC_IPADDR_ISAPI_SERVER, m_ipAddress);
}


BEGIN_MESSAGE_MAP(CDlgISAPIPassthrough, CDialog)
    ON_BN_CLICKED(IDC_BTN_COMMAND, &CDlgISAPIPassthrough::OnClickedBtnCommand)
    ON_BN_CLICKED(IDC_BTN_ISAPI_START_LISTEN, &CDlgISAPIPassthrough::OnBnClickedBtnIsapiStartListen)
    ON_BN_CLICKED(IDC_BTN_ISAPI_STOP_LISTEN, &CDlgISAPIPassthrough::OnBnClickedBtnIsapiStopListen)
END_MESSAGE_MAP()


// CDlgISAPIPassthrough 消息处理程序

#include <io.h>
void CDlgISAPIPassthrough::OnClickedBtnCommand()
{
    // TODO:  在此添加控件通知处理程序代码
    UpdateData(TRUE);

    int iCmdType = m_cmbCmdType.GetCurSel();

    NET_EHOME_PTXML_PARAM struPTXML = { 0 };
    struPTXML.pRequestUrl = m_sUrl.GetBuffer(0);
    struPTXML.dwRequestUrlLen = m_sUrl.GetLength();

    char *sInput = new char[512 * 1024];
    char *sCond = new char[512 * 1024];
    char *sOutput = new char[512 * 1024];

    memset(sInput, 0, 512 * 1024);
    memset(sCond, 0, 512 * 1024);
    memset(sOutput, 0, 512 * 1024);

    sprintf(sInput, "%s", m_sInput);
    sprintf(sCond, "%s", m_sCond);

    //超过8K的数据测试
//     HANDLE handle = CreateFile("E:\\Task2018\\EHomeSDK2.0.1\\common\\HCEHomeCMS\\win\\lib64\\IsapiTestData.xml", 
//         GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_READONLY, NULL);
//     int nFileSize = 0;
//     DWORD dwRSize = 0;
//     if (handle != INVALID_HANDLE_VALUE)
//     {
//         nFileSize = GetFileSize(handle, NULL);
//         ReadFile(handle, sInput, GetFileSize(handle, NULL), &dwRSize, NULL);
//         CloseHandle(handle);
//     }

    struPTXML.pCondBuffer = sCond;
    struPTXML.dwCondSize = (DWORD)strlen(sCond);
    struPTXML.dwInSize = (DWORD)strlen(sInput);

    if (struPTXML.dwInSize == 0)
    {
        struPTXML.pInBuffer = NULL;
    }
    else
    {
        struPTXML.pInBuffer = sInput;
    }

    struPTXML.pOutBuffer = sOutput;
    struPTXML.dwOutSize = 512 * 1024;
    struPTXML.dwRecvTimeOut = m_dwTimeout;

    //需要将字符串字段转换成UTF-8
    //A2UTF8((char*)sInput, strlen(sInput), (char*)struPTXML.pInBuffer, 512 * 1024, &struPTXML.dwInSize); 
    if (struPTXML.dwInSize != 0)
    {
        if (!A2UTF8((char*)struPTXML.pInBuffer, (char*)struPTXML.pInBuffer, 512 * 1024, &struPTXML.dwInSize))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_GetPTXMLConfig A2UTF8 failed");
            return;
        }
    }
  
    if (struPTXML.dwCondSize != 0)
    {
        if (!A2UTF8((char*)struPTXML.pCondBuffer, (char*)struPTXML.pCondBuffer, 512 * 1024, &struPTXML.dwCondSize))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_GetPTXMLConfig A2UTF8 failed");
            return;
        }
    }
    

    if (iCmdType == 0) //GET
    {
        if (NET_ECMS_GetPTXMLConfig(m_lUserID, &struPTXML))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_GetPTXMLConfig");
        }
        else
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_GetPTXMLConfig");
            //delete[] sInput;
            //delete[] sCond;
            //delete[] sOutput;
            //return;
        }
    }
    else if (iCmdType == 1) //PUT
    {
        if (NET_ECMS_PutPTXMLConfig(m_lUserID, &struPTXML))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_PutPTXMLConfig");
        }
        else
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_PutPTXMLConfig");
    /*        delete[] sInput;
            delete[] sCond;
            delete[] sOutput;
            return;*/
        }
    }
    else if (iCmdType == 2) //POST
    {
        if (NET_ECMS_PostPTXMLConfig(m_lUserID, &struPTXML))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_PostPTXMLConfig");
        }
        else
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_PostPTXMLConfig");
            //delete[] sInput;
            //delete[] sCond;
            //delete[] sOutput;
            //return;
        }
    }
    else if (iCmdType == 3) //DELETE
    {
        if (NET_ECMS_DeletePTXMLConfig(m_lUserID, &struPTXML))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_DeletePTXMLConfig");
        }
        else
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_DeletePTXMLConfig");
            //delete[] sInput;
            //delete[] sCond;
            //delete[] sOutput;
            //return;
        }
    }
    else
    {
        delete[] sInput;
        delete[] sCond;
        delete[] sOutput;

        AfxMessageBox("请选择请求类型");
        return;
    }
    //需要将字符串字段转换成GB2312
    UTF82A((char*)sOutput, (char*)sOutput, struPTXML.dwOutSize, &struPTXML.dwOutSize);
    m_sOutput = sOutput;

    delete[] sInput;
    delete[] sCond;
    delete[] sOutput;

    UpdateData(FALSE);
}
void CALLBACK CDlgISAPIPassthrough::HttpServerDataCB(LONG iCallbackHandle, void* pData, void* pUserData)
{
    CDlgISAPIPassthrough* pISAPITrans = static_cast<CDlgISAPIPassthrough*>(pUserData);
    if (pISAPITrans == NULL)
    {
        return;
    }

    if (g_pMainDlg->m_bProxyExitStatus)
    {
        return;
    }

    NET_UTILS_HTTP_SERVER_DATA_CB* pStruParam = static_cast<NET_UTILS_HTTP_SERVER_DATA_CB*>(pData);
    if (pStruParam == NULL || pStruParam->dwStatus >= 0xff00)
    {
        EnterCriticalSection(&g_pMainDlg->m_csProxy);
        g_pMainDlg->m_vecProxyHttpHandle.push_back(iCallbackHandle);
        LeaveCriticalSection(&g_pMainDlg->m_csProxy);

        return;
    }

    NET_UTILS_HTTP_SERVER_REQ_PARAM struReq = { 0 };
    BOOL bRet_Req = NET_HTTPSERVER_GetParamV2(iCallbackHandle,pStruParam->dwStreamID,ENUM_HTTP_SERVER_GENERAL_REQ,NULL,0,&struReq,sizeof(struReq));

    if (!bRet_Req)
    {
        return;
    }
    //报文处理
    char strErr[512] = { 0 };
    BOOL bRet = pISAPITrans->ProcessISAPIRequest(iCallbackHandle, struReq, pStruParam, strErr);

    if (!bRet)
    {
        int iStatus = Http500;
        NET_HTTPSERVER_SetParamV2(iCallbackHandle,
            pStruParam->dwStreamID,
            ENUM_HTTP_SERVER_STATS,
            NULL,
            0,
            &iStatus,
            sizeof(iStatus));

        NET_HTTPSERVER_SetParamV2(iCallbackHandle,
            pStruParam->dwStreamID,
            ENUM_HTTP_SERVER_CONTENT,
            NULL,
            0,
            strErr,
            (UINT32)strlen(strErr));
    }

    NET_HTTPSERVER_ReplyV2(iCallbackHandle, pStruParam->dwStreamID, FALSE);
    return;
}
void CDlgISAPIPassthrough::OnBnClickedBtnIsapiStartListen()
{
    // TODO:  在此添加控件通知处理程序代码
    UpdateData(TRUE);

    char szLan[1024] = { 0 };
    m_iDeviceIndex = g_pMainDlg->GetCurDeviceIndex();
    if (m_iDeviceIndex < 0)
    {
        g_StringLanType(szLan, "请先选择一个设备!", "please select a device first!");
        g_pMainDlg->AddLog(-1, OPERATION_FAIL_T, 0, szLan);
        return;
    }
    m_lUserID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;
    if (m_lUserID < 0)
    {
        g_StringLanType(szLan, "设备未注册!", "device offline!");
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 0, szLan);
        return;
    }

    char szIp[129] = { 0 };
    DWORD dwIP = 0;
    m_ipAddress.GetAddress(dwIP);
    CString csIP = IPToStr(dwIP);

    NET_UTILS_HTTP_LISTEN_PARAM struHttpParam = { 0 };
    memcpy(struHttpParam.szIP, csIP, sizeof(struHttpParam.szIP));
    struHttpParam.wPort = m_iPort;
    struHttpParam.dwRecvTimeOut = 30 * 60 * 1000;
    struHttpParam.pUserData = this;
    struHttpParam.fnCB = HttpServerDataCB;
    m_lHttpListenHandle = NET_HTTPSERVER_CreateListen(&struHttpParam);

    if (m_lHttpListenHandle == -1)
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 0, "NET_HTTPSERVER_CreateListen Error[%d]", NET_UTILS_GetLastError());
        return;
    }
    else
    {
        g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_SUCC_T, 0, "NET_HTTPSERVER_CreateListen");
    }
    return;
}

void CDlgISAPIPassthrough::OnBnClickedBtnIsapiStopListen()
{
    if (m_lHttpListenHandle >= 0)
    {
        if (!NET_HTTPSERVER_DestroyListen(m_lHttpListenHandle))
        {
            g_pMainDlg->AddLog(-1, OPERATION_FAIL_T, 0, "NET_HTTPSERVER_DestroyListen handle[%d]", m_lHttpListenHandle);
        }
        else
        {
            g_pMainDlg->AddLog(-1, OPERATION_SUCC_T, 0, "NET_HTTPSERVER_DestroyListen handle[%d] Error[%d]", m_lHttpListenHandle, NET_UTILS_GetLastError());
        }
    }
}


int GetHttpStatus(char *pXmldata)
{

    char *pos = strstr(pXmldata, "statusCode");
    if (!pos)
    {
        //没找到状态码就认为正常
        return Http200;
    }
    char szStatusCode[16] = { 0 };
    int iStatusCode = 0;
    if (strstr(pXmldata, "xml version="))
    {
        strncpy(szStatusCode, pos + sizeof("statusCode"), 1);
    }
    else
    {
        strncpy(szStatusCode, pos + sizeof("statusCode: "), 1);
    }

    iStatusCode = atoi(szStatusCode);
    if (iStatusCode!=1)
    {
        return Http404;
    }
    else
    {
        return Http200;
    }

}
int XmlPcketJudgment(char *pData)
{
    if (strstr(pData, "xmlns"))
    {
        return TRUE;
    }
    return FALSE;
}


BOOL CDlgISAPIPassthrough::ProcessISAPIRequest(int iCallbackHandle, NET_UTILS_HTTP_SERVER_REQ_PARAM& struReq, NET_UTILS_HTTP_SERVER_DATA_CB* pStruParam, char* strErr)
{
    NET_EHOME_PTXML_PARAM struISAPIJSON = { 0 };
    char pUrl[1024] = { 0 };
    DWORD dwUrlLen = 0;
    switch (struReq.byMethod)
    {
    case ENUM_UTILS_HTTP_GET://GET
    {
        memcpy(pUrl, "GET ", strlen("GET "));
        dwUrlLen += (DWORD)strlen("GET ");
    }
    break;
    case ENUM_UTILS_HTTP_PUT://PUT
    {
        memcpy(pUrl, "PUT ", strlen("PUT "));
        dwUrlLen += (DWORD)strlen("PUT ");
    }
    break;
    case ENUM_UTILS_HTTP_POST://POST
    {
        memcpy(pUrl, "POST ", strlen("POST "));
        dwUrlLen += (DWORD)strlen("POST ");
    }
    break;
    case ENUM_UTILS_HTTP_DELETE://DELETE
    {
        memcpy(pUrl, "DELETE ", strlen("DELETE "));
        dwUrlLen += (DWORD)strlen("DELETE ");
    }
    break;
    default:
        break;
    }

    memcpy(pUrl + dwUrlLen, struReq.szUrl, strlen(struReq.szUrl));
    dwUrlLen += (DWORD)strlen(struReq.szUrl);
    struISAPIJSON.pRequestUrl = pUrl;
    struISAPIJSON.dwRequestUrlLen = dwUrlLen;
    struISAPIJSON.pInBuffer = pStruParam->pRecvData;
    struISAPIJSON.dwInSize = pStruParam->dwDataLen;

    char *pOutput = new char[1024 * 512];
    memset(pOutput, 0, 1024 * 512);
    struISAPIJSON.pOutBuffer = pOutput;
    struISAPIJSON.dwOutSize = 512 * 1024;

    char *pRecv = new char[1024 * 512];
    memset(pRecv, 0, 1024 * 512);
    memcpy(pRecv, pStruParam->pRecvData, pStruParam->dwDataLen > (1024 * 512) ? (1024 * 512) : pStruParam->dwDataLen);
    SetDlgItemText(IDC_EDIT_URL, pUrl);
    SetDlgItemText(IDC_EDIT_INPUT, pRecv);

    if (struISAPIJSON.dwCondSize != 0)
    {
        if (!A2UTF8((char*)struISAPIJSON.pCondBuffer, (char*)struISAPIJSON.pCondBuffer, 512 * 1024, &struISAPIJSON.dwCondSize))
        {
            g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_GetPTXMLConfig A2UTF8 failed");
            return FALSE;
        }
    }

    if (NET_ECMS_ISAPIPassThrough(m_lUserID, &struISAPIJSON))
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 0, "OTAP_CMS_ISAPIPassThrough");
    }
    else
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 0, "OTAP_CMS_ISAPIPassThrough");
        delete[] pOutput;
        delete[] pRecv;
        return FALSE;
    }

    //设置回复
    //int iStatus = Http200;
    int iStatus = GetHttpStatus(pOutput);
    NET_HTTPSERVER_SetParamV2(iCallbackHandle,
        pStruParam->dwStreamID,
        ENUM_HTTP_SERVER_STATS,
        NULL,
        0,
        &iStatus,
        sizeof(iStatus));

    if (XmlPcketJudgment(pOutput))
    {
        NET_HTTPSERVER_SetParamV2(iCallbackHandle,
            pStruParam->dwStreamID,
            ENUM_HTTP_SERVER_ANY,
            "Content-Type",
            sizeof("Content-Type"),
            "application/xml",
            sizeof("application/xml"));

    }

    if (strlen(pOutput) >= 0)
    {
        NET_HTTPSERVER_SetParamV2(iCallbackHandle,
            pStruParam->dwStreamID,
            ENUM_HTTP_SERVER_CONTENT,
            NULL,
            0,
            pOutput,
            (DWORD)strlen(pOutput));
    }

    SetDlgItemText(IDC_EDIT_OUTPUT, pOutput);

    delete[] pOutput;
    delete[] pRecv;

    return TRUE;
}

BOOL CDlgISAPIPassthrough::OnInitDialog()
{
    CDialog::OnInitDialog();
    SetUnhandledExceptionFilter(ExceptionFilter1);
    // TODO:  在此添加额外的初始化
    m_iDeviceIndex = g_pMainDlg->GetCurDeviceIndex();
    if (m_iDeviceIndex < 0)
    {
        AfxMessageBox("请选择一个设备");
        return TRUE;
    }
    m_lUserID = g_struDeviceInfo[m_iDeviceIndex].lLoginID;
    if (m_lUserID < 0)
    {
        AfxMessageBox("请先登陆设备");
        return TRUE;
    }

    return TRUE;  // return TRUE unless you set the focus to a control
    // 异常:  OCX 属性页应返回 FALSE
}
