// DlgISAPIConfig.cpp : 实现文件
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgISAPIConfig.h"
#include "afxdialogex.h"
#include "public/cjson/cJson.h"

// CDlgISAPIConfig 对话框

IMPLEMENT_DYNAMIC(CDlgISAPIConfig, CDialogEx)

CDlgISAPIConfig::CDlgISAPIConfig(CWnd* pParent /*=NULL*/)
	: CDialogEx(CDlgISAPIConfig::IDD, pParent)
    , m_sInput(_T(""))
    , m_sOutput(_T(""))
    , m_sUrl(_T(""))
	, m_sPath(_T(""))
{
    m_lUserID = -1;
    m_iDeviceIndex = -1;
}

CDlgISAPIConfig::~CDlgISAPIConfig()
{
}

void CDlgISAPIConfig::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Text(pDX, IDC_EDIT_INPUT, m_sInput);
    DDX_Text(pDX, IDC_EDIT_OUTPUT, m_sOutput);
    DDX_Text(pDX, IDC_EDIT_URL, m_sUrl);
    DDX_Text(pDX, IDC_EDIT_PATH, m_sPath);
}


BEGIN_MESSAGE_MAP(CDlgISAPIConfig, CDialogEx)
    ON_BN_CLICKED(IDC_BTN_COMMAND, &CDlgISAPIConfig::OnBnClickedBtnCommand)
END_MESSAGE_MAP()


// CDlgISAPIConfig 消息处理程序


void CDlgISAPIConfig::OnBnClickedBtnCommand()
{
    // TODO:  在此添加控件通知处理程序代码
    UpdateData(TRUE);
    NET_EHOME_PTXML_PARAM struISAPIJSON = { 0 };
    struISAPIJSON.pRequestUrl = m_sUrl.GetBuffer(0);
    struISAPIJSON.dwRequestUrlLen = m_sUrl.GetLength();
    struISAPIJSON.pInBuffer = m_sInput.GetBuffer(0);
    struISAPIJSON.dwInSize = m_sInput.GetLength();
    char sOutput[1024 * 10] = { 0 };
    struISAPIJSON.pOutBuffer = sOutput;
    struISAPIJSON.dwOutSize = sizeof(sOutput);
	int len = strlen(m_sInput.GetBuffer(0));
	if (m_sPath.GetLength() != 0)
	{
		unsigned char * pBuff = NULL;
		DWORD dwFileLen = 0, curpos;
		FILE* pFile = fopen(m_sPath.GetBuffer(0), "rb+");
		if (pFile == NULL)
		{
			g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 4, "OnBnClickedBtnCommand, file open failed");
			UpdateData(FALSE);
			return;
		}
		curpos = ftell(pFile);
		fseek(pFile, 0, SEEK_END);
		dwFileLen = ftell(pFile) + m_sInput.GetLength() + 1;
		if (dwFileLen > 50 * 1024 * 1024)
		{
			g_pMainDlg->AddLog(g_pMainDlg->GetCurDeviceIndex(), OPERATION_FAIL_T, 4, "NET_ESS_ClientDoUpload, file excced 50M");
			fclose(pFile);
			UpdateData(FALSE);
			return;
		}
		fseek(pFile, curpos, SEEK_SET);
		pBuff = new unsigned char[dwFileLen];
		memset(pBuff, 0, dwFileLen);
		memcpy(pBuff, m_sInput.GetBuffer(0), m_sInput.GetLength());
		size_t dwRead = fread(pBuff + m_sInput.GetLength(), 1, dwFileLen - m_sInput.GetLength() - 1, pFile);
		if (dwRead != dwFileLen)
		{
		}

		struISAPIJSON.pInBuffer = pBuff;
		struISAPIJSON.dwInSize = dwFileLen - 1;
	}
	else
	{
		struISAPIJSON.pInBuffer = m_sInput.GetBuffer(0);
		struISAPIJSON.dwInSize = m_sInput.GetLength();
	}



    //m_sInput = "\"Head\": {\r\n  \"URL\": \"%s\",\r\n  \"seq\": \"%u\"\r\n}";
    //cJSON* pRoot = cJSON_Parse("\"Head\": {\r\n  \"URL\": \"%s\",\r\n  \"seq\": \"%u\"\r\n}");
    //cJSON* pHeadaa = cJSON_GetObjectItem(pRoot, "Head");

    //cJSON* pNodedata = cJSON_GetObjectItem(const_cast<cJSON*>(pHeadaa), "URL");
    //if (pNodedata == NULL || pNodedata->type != cJSON_String)
    //{
    //    return ;
    //}
    //CString nVal = pHeadaa->valuestring;

    if (NET_ECMS_ISAPIPassThrough(m_lUserID, &struISAPIJSON))
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_SUCC_T, 1, "NET_ECMS_ISAPIPassThrough");
    }
    else
    {
        g_pMainDlg->AddLog(m_iDeviceIndex, OPERATION_FAIL_T, 1, "NET_ECMS_ISAPIPassThrough");
        return;
    }

    m_sOutput = sOutput;
   // m_sOutput = nVal;

    UpdateData(FALSE);
}


BOOL CDlgISAPIConfig::OnInitDialog()
{
    CDialogEx::OnInitDialog();

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
