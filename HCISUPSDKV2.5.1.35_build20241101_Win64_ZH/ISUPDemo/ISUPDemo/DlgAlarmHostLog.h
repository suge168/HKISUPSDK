#pragma once
#include "afxwin.h"
#include "afxdtctl.h"
#include "afxcmn.h"


// CDlgDevLog �Ի���

class CDlgAlarmHostLog : public CDialog
{
	DECLARE_DYNAMIC(CDlgAlarmHostLog)

public:
	CDlgAlarmHostLog(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CDlgAlarmHostLog();

// �Ի�������
    enum { IDD = IDD_DLG_ALARM_HOST_LOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
    virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()
public:
    CComboBox m_comboMajorType;
    CComboBox m_comboMinorType;
    CTime m_ctDateStart;
    CTime m_ctDateStop;
    CTime m_ctTimeStart;
    CTime m_ctTimeStop;
    CListCtrl m_listDeviceLog;
    afx_msg void OnBnClickedBtnSearchDevLog();



    LONG m_iDeviceIndex;
    LONG m_lLoginID;
    BOOL m_bSearching;
    BOOL m_bQuit;
    LONG m_iChanIndex;
    LONG m_lFileHandle;
    HANDLE m_hFileThread;
    DWORD m_dwLogNum;

    int m_iMode;
    int m_iMajType;
    int m_iMinType;
    BOOL m_bSearch;
    LONG m_lLogHandle;

    NET_EHOME_ALARM_HOST_LOG_COND m_struAlarmHostLogCond;
    NET_EHOME_ALARM_HOST_LOG m_struAlarmHostLog;

    static UINT GetFileThread(LPVOID pParam);
    BOOL CheckInitParam();

    typedef enum _ALARM_TYPE_
    {
        FILE_TIMING = 0, //��ʱ¼��
        FILE_MOTION_DETECT = 1, //�ƶ����¼��
        FILE_ALARMIN = 2, //��������¼��
        FILE_MOTION_OR_ALARMIN = 3, //����|�ƶ����
        FILE_MOTION_AND_ALARMIN = 4, //����&�ƶ����
        FILE_COMMAND = 5, //�����¼��
        FILE_MANUAL = 6, //�ֶ�¼��
        FILE_VCA = 7, //����¼��
        FILE_PIR_ALARM = 10, //PIR����¼��
        FILE_WIRELESS_ALARM = 11, //���߱���¼��
        FILE_CALLHELP_ALARM = 12, //���ȱ���
        FILE_PIR_WIRELESS_CALLHELP = 13,  //�ƶ�|��������|PIR|���߱���|���ȱ���
        FILE_INTELLIGENT_TRANSPORT = 14, //���ܽ�ͨ�¼�����
        FILE_LINE_DETECTION = 15,// Խ�����
        FILE_FIELD_DETECTUIN = 16,// ��������
        FILE_AUDIO_EXCEPTION = 17,// �����쳣
        //	FILE_IPC_SMART = 18,
        FILE_SCENECHANGE_DETECTION = 18,//���������� 2013-07-16
        FILE_INTELLIGENT_DETECTION = 19,
        FILE_FACE_DETECTION = 20, //�������
        FILE_POS_RECORD = 21, //POS����
        FILE_REGION_ENTRANCE_DETECTION = 26, //�����������
        FILE_REGION_EXITING_DETECTION = 27,//�뿪�������
        FILE_LOITERING_DETECTION = 28,//�ǻ����
        FILE_PEOPLE_GATHERING_DETECTION = 29,//��Ա�ۼ����
        FILE_FAST_MOVING_DETECTION = 30,//�����ƶ����
        FILE_PARKING_DETECTION = 31,//ͣ�����
        FILE_UNATTENDED_BAGGAGE_DETECTION = 32,//��Ʒ�������
        FILE_OBJECT_REMOVAL_DETECTION = 33,//��Ʒ��ȡ���
        FILE_OBJECT_FIRE_DETECTION = 34,//��Ʒ��ȡ���
        FILE_OBJECT_VANDALPROOF_DETECTION = 35,//���ƻ����
        FILE_THERMAL_SHIPS_DETECTION = 36, //��ֻ���
        FILE_THERMAL_THERMOMETRY_EARLYWARNING = 37, //����Ԥ��
        FILE_THERMAL_THERMOMETRY_ALARM = 38, //���±���
        FILE_THERMOMETRY_DIFF_ALARM = 42,//����
        FILE_THERMOMETRY_OFFLINE_ALARM = 43,//���߲��±���
        FILE_ZONE_ALARMIN_ALARM = 44,//��������
        FILE_EMERGENCY_CALL_HELP_ALARM = 45,//��������
        FILE_CONSULT = 46,//ҵ����ѯ
        FILE_VCA_EVENT_GET_UP = 47,// ������
        FILE_VCA_EVENT_ADV_REACH_HEIGHT = 48,// �����ʸ�
        FILE_VCA_EVENT_TOILET_TARRY = 49,// ��ʱ
        FILE_ALL = 0xffff //ȫ��
    }ALARM_TYPE;
};
