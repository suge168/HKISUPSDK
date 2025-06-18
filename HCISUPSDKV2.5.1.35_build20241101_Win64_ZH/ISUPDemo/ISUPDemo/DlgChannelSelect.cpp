// DlgChannelSelect.cpp : 实现文件
//

#include "stdafx.h"
#include "DlgChannelSelect.h"
#include "afxdialogex.h"


// CDlgChannelSelect 对话框

IMPLEMENT_DYNAMIC(CDlgChannelSelect, CDialogEx)

BOOL CDlgChannelSelect::OnInitDialog()
{
    // TODO:  在此添加控件通知处理程序代码
    CRect rect;
    GetClientRect(&rect);
    int width = rect.Width();
    int radioCount = m_strChannelList.size(); // RadioButton控件的数量

    // 控件的宽度和高度
    int controlWidth = 110;
    int controlHeight = 20;

    // 控件间的水平和垂直间距
    int horizontalSpacing = 30;
    int verticalSpacing = 20;

    // 计算每行可以容纳的控件数量
    int controlsPerRow = (width - horizontalSpacing) / (controlWidth + horizontalSpacing);
    // 创建RadioButton和Label控件
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


// CDlgChannelSelect 消息处理程序


// 获取选中的通道列表
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
    // TODO:  在此添加控件通知处理程序代码
    CDialogEx::OnOK();
    for (int i = 0; i < m_strChannelList.size(); i++)
    {
        // 检查每一个button的是否选中
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
