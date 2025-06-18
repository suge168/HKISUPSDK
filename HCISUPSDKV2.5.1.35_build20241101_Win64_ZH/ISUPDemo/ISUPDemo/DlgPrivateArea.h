#pragma once

#define HIDE_AREA_NUM (4)
// CDlgPrivateArea �Ի���


typedef struct HideArea
{
    int iLeft;
    int iRight;
    int iBottom;
    int iTop;
}HIDE_AREA;

class CDlgPrivateArea : public CDialogEx
{
	DECLARE_DYNAMIC(CDlgPrivateArea)

public:
	CDlgPrivateArea(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgPrivateArea();

// �Ի�������
	enum { IDD = IDD_DLG_PRIVATE_AREA };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()

public:
    BOOL CheckInitParam();
    int m_iDeviceIndex;
    LONG m_lLoginID;
    DWORD m_dwChannelNo;
    int m_iChanIndex;
    bool m_bStartValid[HIDE_AREA_NUM];
    CPoint m_startPoint[HIDE_AREA_NUM];
    CPoint m_endPoint[HIDE_AREA_NUM];
    int m_iAreaNum;

    void RefreshAreaNum();
public:
    afx_msg void OnBnClickedBtnGet();
    afx_msg void OnBnClickedBtnClear();
    afx_msg void OnBnClickedButtonSet();
    afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
    afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
};
