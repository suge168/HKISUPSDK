// DlgIPSelect.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "EHomeDemo.h"
#include "DlgIPSelect.h"
#include "afxdialogex.h"

#include <WinSock2.h>
#include <Iphlpapi.h>
#include <ws2tcpip.h>

#pragma comment(lib,"iphlpapi.lib")
#pragma comment(lib,"ws2_32.lib")

// DlgIPSelect �Ի���

IMPLEMENT_DYNAMIC(DlgIPSelect, CDialog)

DlgIPSelect::DlgIPSelect(CWnd* pParent /*=NULL*/)
	: CDialog(DlgIPSelect::IDD, pParent)
{
    m_nPort = 7660;
}

DlgIPSelect::~DlgIPSelect()
{
}

void DlgIPSelect::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_COM_IP, m_cmIP);
    DDX_Text(pDX, IDC_EDT_PORT, m_nPort);
    DDX_Text(pDX, IDC_COM_IP, m_csIP);
}


BEGIN_MESSAGE_MAP(DlgIPSelect, CDialog)
    ON_BN_CLICKED(IDOK, &DlgIPSelect::OnBnClickedOk)
END_MESSAGE_MAP()


// DlgIPSelect ��Ϣ�������


BOOL DlgIPSelect::OnInitDialog()
{
    CDialog::OnInitDialog();

    UpdateData(FALSE);

    //GetLocalIP();
    GetLocalIPEx();

    return TRUE;
}

void DlgIPSelect::OnBnClickedOk()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    UpdateData(TRUE);

    CDialog::OnOK();
}

void DlgIPSelect::GetLocalIP()
{
    //PIP_ADAPTER_INFO�ṹ��ָ��洢����������Ϣ
    BYTE *pArray = NULL;
    PIP_ADAPTER_INFO pIpAdapterInfoTemp = NULL;

    pIpAdapterInfoTemp = new(std::nothrow) IP_ADAPTER_INFO();
    if (pIpAdapterInfoTemp == NULL)
    {
        return;
    }
    PIP_ADAPTER_INFO pIpAdapterInfo = pIpAdapterInfoTemp;
    //�õ��ṹ���С,����GetAdaptersInfo����
    unsigned long stSize = sizeof(IP_ADAPTER_INFO);
    //����GetAdaptersInfo����,���pIpAdapterInfoָ�����;����stSize��������һ��������Ҳ��һ�������
    int nRel = GetAdaptersInfo(pIpAdapterInfo, &stSize);
    if (ERROR_BUFFER_OVERFLOW == nRel)
    {
        //����������ص���ERROR_BUFFER_OVERFLOW
        //��˵��GetAdaptersInfo�������ݵ��ڴ�ռ䲻��,ͬʱ�䴫��stSize,��ʾ��Ҫ�Ŀռ��С
        //��Ҳ��˵��ΪʲôstSize����һ��������Ҳ��һ�������

        //���������ڴ�ռ������洢����������Ϣ
        pArray = new(std::nothrow) BYTE[stSize];
        if (pArray == NULL)
        {
            //�ͷ�ԭ��������ڴ�
            if (pIpAdapterInfoTemp)
            {
                delete pIpAdapterInfoTemp;
                pIpAdapterInfoTemp = NULL;
            }

            return;
        }
        pIpAdapterInfo = (IP_ADAPTER_INFO*)pArray;

        //�ٴε���GetAdaptersInfo����,���pIpAdapterInfoָ�����
        nRel = GetAdaptersInfo(pIpAdapterInfo, &stSize);
    }

    if (ERROR_SUCCESS == nRel)
    {
        m_cmIP.AddString("0.0.0.0");
        //���������Ϣ
        while (pIpAdapterInfo)
        {
            IP_ADDR_STRING *pIpAddrString = &(pIpAdapterInfo->IpAddressList);
            do
            {
                CString csIp;
                csIp.Format("%s", pIpAddrString->IpAddress.String);
                m_cmIP.AddString(csIp);
                pIpAddrString = pIpAddrString->Next;
            } while (pIpAddrString);
            pIpAdapterInfo = pIpAdapterInfo->Next;
        }
    }

    //�ͷ��ڴ�ռ�
    if (pIpAdapterInfoTemp)
    {
        delete pIpAdapterInfoTemp;
        pIpAdapterInfoTemp = NULL;
    }

    if (pArray)
    {
        delete[]pArray;
        pArray = NULL;
    }

    m_cmIP.SetCurSel(0);

}

//���Ի�ȡipv4��ipv6��ַ
void DlgIPSelect::DoGetIP()
{
    struct addrinfo *ailist, *aip;
    struct addrinfo hint;
    struct sockaddr_in6 *sinp6;
    PHOSTENT hostinfo;
    char szHostName[255] = { 0 }; //������
    char *pPort = "3294";      //�˿ں�
    const char *addr;

    gethostname(szHostName, sizeof(szHostName));
    if ((hostinfo = gethostbyname(szHostName)) == NULL) //��ñ���ipv4��ַ
    {
        return;
    }

    LPCSTR ipv4;
    m_cmIP.AddString("0.0.0.0");
    while (*(hostinfo->h_addr_list) != NULL) //���ipv4��ַ
    {
        ipv4 = inet_ntoa(*(struct in_addr *) *hostinfo->h_addr_list);

        CString csIp;
        csIp.Format("%s", ipv4);
        m_cmIP.AddString(csIp);

        hostinfo->h_addr_list++;
    }

    hint.ai_family = AF_INET6;        //hint ���޶�����
    hint.ai_socktype = SOCK_STREAM;   //����������� socket type    ����  SOCK_DGRAM
    hint.ai_flags = AI_PASSIVE;       // flags �ı�־�ܶࡣ���õ���AI_CANONNAME;
    hint.ai_protocol = 0;             //����Э��  һ��Ϊ0��Ĭ��
    hint.ai_addrlen = 0;              //���治�������ã�Ϊ0������ΪNULL
    hint.ai_canonname = NULL;
    hint.ai_addr = NULL;
    hint.ai_next = NULL;
    int ilRc = getaddrinfo(szHostName, pPort, &hint, &ailist); //ͨ����������õ�ַ��Ϣ
    if (ilRc < 0)
    {
        return;
    }

    if (ailist == NULL)
    {
        //����
    }

    char ipv6[128] = { 0 };
    for (aip = ailist; aip != NULL; aip = aip->ai_next) //��ʾ��ȡ����Ϣ
    {
        CString csIp;
        memset(ipv6, 0, 128);

        aip->ai_family == AF_INET6;
        sinp6 = (struct sockaddr_in6 *)aip->ai_addr;
        for (int i = 0; i < 16; i++)
        {
            if (((i - 1) % 2) && (i > 0))
            {
                sprintf(ipv6, "%s:", ipv6);
            }
            sprintf(ipv6, "%s%02x", ipv6, sinp6->sin6_addr.u.Byte[i]);
        }

        csIp.Format("%s", ipv6);
        m_cmIP.AddString(csIp);
    }
}

void DlgIPSelect::GetLocalIPEx()
{
    WORD wVersionRequested;
    WSADATA wsaData;
    wVersionRequested = MAKEWORD(1, 1);

    int err = WSAStartup(wVersionRequested, &wsaData);//initiate the ws2_32.dll and match the version
    if (err != 0)
    {
        return;
    }

    if (LOBYTE(wsaData.wVersion) != 1 || HIBYTE(wsaData.wVersion) != 1)//if the version is not matched ,then quit and terminate the ws3_32.dll
    {
        WSACleanup();
        return;
    }

    DoGetIP();
    m_cmIP.SetCurSel(0);

    WSACleanup();
    return;
}
