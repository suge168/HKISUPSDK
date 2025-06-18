#pragma once
#include "Resource.h"
#include <vector>
#include <string>

// CDlgChannelSelect �Ի���

class CDlgChannelSelect : public CDialogEx
{
	DECLARE_DYNAMIC(CDlgChannelSelect)

public:
	CDlgChannelSelect(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgChannelSelect();
    CListBox m_ListBox; // �б��ؼ�
    virtual BOOL OnInitDialog();

// �Ի�������
	enum { IDD = IDD_DIAL_CHANNEL_SELECT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    void SetChannelList(std::vector<std::string> channelList);
    std::vector<std::string> GetSelectChannelList();
    CButton m_radioButtons[256];   // RadioButton�ؼ�����
    std::vector<std::string> m_strChannelList;
    std::vector<std::string> m_strChannelSelectList;
    afx_msg void OnBnClickedOk();
};
