// DlgChannelSelect.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "DlgChannelSelect.h"
#include "afxdialogex.h"


// CDlgChannelSelect �Ի���

IMPLEMENT_DYNAMIC(CDlgChannelSelect, CDialogEx)

BOOL CDlgChannelSelect::OnInitDialog()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CRect rect;
    GetClientRect(&rect);
    int width = rect.Width();
    int radioCount = m_strChannelList.size(); // RadioButton�ؼ�������

    // �ؼ��Ŀ�Ⱥ͸߶�
    int controlWidth = 110;
    int controlHeight = 20;

    // �ؼ����ˮƽ�ʹ�ֱ���
    int horizontalSpacing = 30;
    int verticalSpacing = 20;

    // ����ÿ�п������ɵĿؼ�����
    int controlsPerRow = (width - horizontalSpacing) / (controlWidth + horizontalSpacing);
    // ����RadioButton��Label�ؼ�
    for (int i = 0; i < radioCount; i++)
    {
        int row = i / controlsPerRow;
        int col = i % controlsPerRow;

        int x = horizontalSpacing + col * (controlWidth + horizontalSpacing);
        int y = verticalSpacing + row * (controlHeight + verticalSpacing);

        m_radioButtons[i].Create(_T(m_strChannelList[i].c_str()), WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON, CRect(x, y, x + controlWidth, y + controlHeight),
            this, IDD_DIAL_CHANNEL_SELECT);
        m_radioButtons[i].ModifyStyle(0, BS_AUTOCHECKBOX);
    }
    return TRUE;
}

CDlgChannelSelect::CDlgChannelSelect(CWnd* pParent /*=NULL*/)
	: CDialogEx(CDlgChannelSelect::IDD, pParent)
{

}

CDlgChannelSelect::~CDlgChannelSelect()
{
}

void CDlgChannelSelect::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDlgChannelSelect, CDialogEx)
    ON_BN_CLICKED(IDOK, &CDlgChannelSelect::OnBnClickedOk)
END_MESSAGE_MAP()


// CDlgChannelSelect ��Ϣ�������


// ��ȡѡ�е�ͨ���б�
std::vector<std::string> CDlgChannelSelect::GetSelectChannelList()
{
    return m_strChannelSelectList;
}

void CDlgChannelSelect::SetChannelList(std::vector<std::string> channelList)
{
    for (int i = 0; i < channelList.size(); i++)
    {
        m_strChannelList.push_back("channel " + channelList[i]);
    }
}


void CDlgChannelSelect::OnBnClickedOk()
{
    // TODO:  �ڴ���ӿؼ�֪ͨ����������
    CDialogEx::OnOK();
    for (int i = 0; i < m_strChannelList.size(); i++)
    {
        // ���ÿһ��button���Ƿ�ѡ��
        if (1 == m_radioButtons[i].GetCheck())
        {
            CString channelName;
            m_radioButtons[i].GetWindowText(channelName);
            CStringA channelNameA(channelName);
            const char* channelNameStr = channelNameA.GetString();
            std::string strChannelName(channelNameStr);
            m_strChannelSelectList.push_back(strChannelName);
        }
    }
    EndDialog(IDOK);
}
