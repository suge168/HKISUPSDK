
// EHomeDemo.cpp : ����Ӧ�ó��������Ϊ��
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "EHomeDemoDlg.h"
#include "DlgIPSelect.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif



// CEHomeDemoApp

BEGIN_MESSAGE_MAP(CEHomeDemoApp, CWinAppEx)
    ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CEHomeDemoApp ����

CEHomeDemoApp::CEHomeDemoApp()
{
    // TODO: �ڴ˴���ӹ�����룬
    // ��������Ҫ�ĳ�ʼ�������� InitInstance ��
}


// Ψһ��һ�� CEHomeDemoApp ����

CEHomeDemoApp theApp;


// CEHomeDemoApp ��ʼ��

BOOL CEHomeDemoApp::InitInstance()
{
    // ���һ�������� Windows XP �ϵ�Ӧ�ó����嵥ָ��Ҫ
    // ʹ�� ComCtl32.dll �汾 6 ����߰汾�����ÿ��ӻ���ʽ��
    //����Ҫ InitCommonControlsEx()�����򣬽��޷��������ڡ�
    INITCOMMONCONTROLSEX InitCtrls;
    InitCtrls.dwSize = sizeof(InitCtrls);
    // ��������Ϊ��������Ҫ��Ӧ�ó�����ʹ�õ�
    // �����ؼ��ࡣ
    InitCtrls.dwICC = ICC_WIN95_CLASSES;
    InitCommonControlsEx(&InitCtrls);

    NET_EHOME_VOICETALK_DATA pVoicTalkData = { 0 };
    LONG ret = NET_ESTREAM_SendVoiceTalkData(0, &pVoicTalkData);
    ret = NET_ESTREAM_GetLastError();

    CWinAppEx::InitInstance();

    AfxEnableControlContainer();

    // ��׼��ʼ��
    // ���δʹ����Щ���ܲ�ϣ����С
    // ���տ�ִ���ļ��Ĵ�С����Ӧ�Ƴ�����
    // ����Ҫ���ض���ʼ������
    // �������ڴ洢���õ�ע�����
    // TODO: Ӧ�ʵ��޸ĸ��ַ�����
    // �����޸�Ϊ��˾����֯��
    SetRegistryKey(_T("Ӧ�ó��������ɵı���Ӧ�ó���"));

    //
    DlgIPSelect ipDlg;
    INT_PTR nResponse = ipDlg.DoModal();
    if (nResponse != IDOK)
    {
        return FALSE;
    }

    CEHomeDemoDlg dlg;

    dlg.SetAddr(ipDlg.m_csIP, ipDlg.m_nPort);

    m_pMainWnd = &dlg;
    nResponse = dlg.DoModal();
    if (nResponse == IDOK)
    {
        // TODO: �ڴ˷��ô����ʱ��
        //  ��ȷ�������رնԻ���Ĵ���
    }
    else if (nResponse == IDCANCEL)
    {
        // TODO: �ڴ˷��ô����ʱ��
        //  ��ȡ�������رնԻ���Ĵ���
    }

    // ���ڶԻ����ѹرգ����Խ����� FALSE �Ա��˳�Ӧ�ó���
    //  ����������Ӧ�ó������Ϣ�á�
    return FALSE;
}


CString IPToStr(DWORD dwIP)
{
    CString strIP = _T("");
    WORD add1,add2,add3,add4;

    add1=(WORD)(dwIP&255);
    add2=(WORD)((dwIP>>8)&255);
    add3=(WORD)((dwIP>>16)&255);
    add4=(WORD)((dwIP>>24)&255);
    strIP.Format("%d.%d.%d.%d",add4,add3,add2,add1);
    return strIP;
}
