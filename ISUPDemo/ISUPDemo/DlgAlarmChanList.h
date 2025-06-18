#pragma once
#include "afxcmn.h"


// CDlgAlarmChanList �Ի���

class CDlgAlarmChanList : public CDialog
{
    DECLARE_DYNAMIC(CDlgAlarmChanList)

public:
    CDlgAlarmChanList(CWnd* pParent = NULL);   // ��׼���캯��
    virtual ~CDlgAlarmChanList();

// �Ի�������
    enum { IDD = IDD_DLG_ALARM_TREE };

protected:
    virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

    DECLARE_MESSAGE_MAP()

public:
    virtual BOOL OnInitDialog();
    afx_msg void OnBnClickedBtnOk();
    afx_msg void OnBnClickedBtnCancel();
    void InitTreeList();
    void GetDataFromTreeList();

public:
    CString m_strDlgTitle;
    BYTE m_byDataType; //0-���������1-¼��ģ��ͨ����2-¼������ͨ��
    BYTE m_byNum;
    BYTE m_byAnalogAlarmOut[MAX_ANALOG_ALARMOUT];
    BYTE m_byAnalogAlarmChan[MAX_ANALOG_CHANNUM];
    BYTE m_byDigitalAlarmChan[MAX_DIGIT_CHANNUM];
    DWORD m_dwDigitalAlarmChan;

public:
    CTreeCtrl m_treeAlarmChan;
    afx_msg void OnNMClickTree1(NMHDR *pNMHDR, LRESULT *pResult);
    afx_msg void OnUpdateMenuWirelessInfo(CCmdUI *pCmdUI);
};
