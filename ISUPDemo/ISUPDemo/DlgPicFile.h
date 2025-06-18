#pragma once
#include "afxcmn.h"
#include "afxwin.h"

typedef enum _PICTURE_FILE_TYPE_
{
    PIC_SCHEDULED_CAP = 0, //��ʱץͼ
    PIC_MOTION_CAP = 1, //�ƶ����ץͼ
    PIC_ALARM_CAP = 2, //����ץͼ
    PIC_MOTION_OR_ALARMIN_CAP = 3, //����|�ƶ����ץͼ
    PIC_MOTION_AND_ALARMIN_CAP = 4, //����&�ƶ����ץͼ
    PIC_COMMAND_CAP = 5, //�����¼��ץͼ
    PIC_MANUAL_CAP = 6, //�ֶ�ץͼ
    PIC_VIBRATING_ALARM_CAP = 7, //�𶯱���ץͼ
    PIC_ENVIRONMENT_ALARM_CAP = 8, //������������ץͼ
    PIC_VCA_ALARM = 9, //���ܱ���ͼƬ
    PIC_PIR_ALARM = 10, //PIR����¼��ͼƬ
    PIC_WIRELESS_ALARM = 11, //���߱���ͼƬ
    PIC_EMERGENCY_ALARM = 12, //���ȱ���ͼƬ
    PIC_FACE_DETECTION = 13, //�������ͼƬ
    PIC_LINE_CROSSING_DETECTION = 14, //Խ�����ͼƬ
    PIC_INTRUSION_DETECTION = 15, //�����������ͼƬ
    PIC_SCENCE_CHANGE_DETECTION = 16, //����������ͼƬ
    PIC_LOCAL_PLAYBACK = 17, //�豸���ػط�ʱ��ͼ
    PIC_VCA_DETECTION = 18, //�������ͼƬ
    PIC_REGION_ENTRANCE_DETECTION = 19,//�����������ͼƬ
    PIC_REGION_EXITING_DETECTION = 20,//�뿪�������ͼƬ
    PIC_LOITERING_DETECTION = 21,//�ǻ����ͼƬ
    PIC_PEOPLE_GATHERING_DETECTION = 22,//��Ա�ۼ����ͼƬ
    PIC_FAST_MOVING_DETECTION = 23,//�����˶����ͼƬ
    PIC_PARKING_DETECTION = 24,//ͣ�����ͼƬ
    PIC_UNATTENDED_BAGGAGE_DETECTION = 25,//��Ʒ�������ͼƬ
    PIC_OBJECT_REMOVAL_DETECTION = 26,//��Ʒ��ȡ���ͼƬ
    PIC_LICENSE_DETECTION = 27,//�������ͼƬ
    PIC_UPLOADED_TO_CLIENT = 28, //�ͻ����ϴ�ͼƬ
    PIC_ALL_PIC = 255, //ALL
}PICTURE_FILE_TYPE;

// CDlgPicFile �Ի���

class CDlgPicFile : public CDialog
{
    DECLARE_DYNAMIC(CDlgPicFile)

public:
    CDlgPicFile(CWnd* pParent = NULL);   // ��׼���캯��
    virtual ~CDlgPicFile();

// �Ի�������
    enum { IDD = IDD_DIALOG_PIC_FILE };

    BOOL CheckInitParam();
    static UINT GetFileThread(LPVOID pParam);

protected:
    virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

    DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnBnClickedBtnSearchPicFile();
    virtual BOOL OnInitDialog();
    CListCtrl m_lstPicFile;
    CTime m_dateStart;
    CTime m_timeStart;
    CTime m_DateStop;
    CTime m_timeStop;
    CComboBox m_cmbPicType;

    LONG m_iDeviceIndex;
    LONG m_lLoginID;
    BOOL m_bSearching;
    BOOL m_bQuit;
    LONG m_iChanIndex;
    LONG m_lFileHandle;
    HANDLE m_hFileThread;
    DWORD m_dwFileNum;
    NET_EHOME_FINDCOND m_struFindCond;
    NET_EHOME_FINDDATA m_struFindData;
    int m_byTimeType;
};
