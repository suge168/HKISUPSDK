#pragma once
#include "Resource.h"
#include <vector>
#include <string>

// CDlgChannelSelect 对话框

class CDlgChannelSelect : public CDialogEx
{
	DECLARE_DYNAMIC(CDlgChannelSelect)

public:
	CDlgChannelSelect(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDlgChannelSelect();
    CListBox m_ListBox; // 列表框控件
    virtual BOOL OnInitDialog();

// 对话框数据
	enum { IDD = IDD_DIAL_CHANNEL_SELECT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
    void SetChannelList(std::vector<std::string> channelList);
    std::vector<std::string> GetSelectChannelList();
    CButton m_radioButtons[256];   // RadioButton控件数组
    std::vector<std::string> m_strChannelList;
    std::vector<std::string> m_strChannelSelectList;
    afx_msg void OnBnClickedOk();
};
