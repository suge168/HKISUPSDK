
// EHomeDemoDlg.h : ͷ�ļ�
//

#pragma once

#include "DlgOutput.h"
#include "DlgPlayBack.h"
#include "DlgAudioTalk.h"
#include "DlgCommonCfg.h"
#include "DlgLocalCfg.h"
#include "DlgOutputCtrl.h"
#include "DlgHttpAlarm.h"
#include "HCISUPStream.h"
#include "Public/TinyXML/XmlBase.h"
#include "DlgPassthroughProxy.h"
#include "DlgVideoIntercomCall.h"

#include "afxwin.h"
#include "afxcmn.h"
#include <vector>
#include <map>

#define MAX_WIN_NUM    64    //��󴰿���
#define MAX_FILE_PATH    260            /**< windows MAX_PATH */
#define CENTRAL_SQL_TABLE_NAME "central_path_uri"
#define UN_REFERENCED_PARAMETER(x)    {(x) = (x);}
typedef enum
{
    NODE_STRING_TO_BOOL = 0,    //stringתbool(0,1)
    NODE_STRING_TO_INT = 1,    //stringתint(HPR_UINT32)
    NODE_STRING_TO_ARRAY = 2,   //stringת����(HPR_UINT8[],char[])
    NODE_STRING_TO_BYTE = 3,    //stringתHPR_UINT8,HPR_UINT8��Ϊ����ʱ
    NODE_STRING_TO_WORD = 4,    //stringתHPR_UINT16
    NODE_STRING_TO_FLOAT = 5,    //stringתFLOAT

    NODE_TYPE_REVERSE = 64,   //���ͷ�ת,��������ת������   
    NODE_BOOL_TO_STRING = 65,  //bool(0,1)תstring
    NODE_INT_TO_STRING = 66,  //int(HPR_UINT32)תstring
    NODE_ARRAY_TO_STRING = 67,  //����(HPR_UINT8[],char[])תstring
    NODE_BYTE_TO_STRING = 68,   //HPR_UINT8תstring,HPR_UINT8��Ϊ����ʱ
    NODE_WORD_TO_STRING = 69    //HPR_UINT16תstring
}XML_NODE_TYPE;
void g_StringLanType(char *szDstLan, char *szLanCn, char *szLanEn);
extern LONG g_lCmsAlarm;
BOOL ConvertSingleNodeData(void *pOutVale, CXmlBase &struXml, const char* pNodeName, BYTE byDataType, int iArrayLen = 0);

typedef void (CALLBACK *DEVIDCHANGECB)(LONG iDeviceID, void* pUser);


//�豸�ɽ�����Ϣ
typedef struct tagAccessDeviceInfo
{
    char sSerialNumber[12]; //�豸���к�
    char sIdentifyCode[32]; //�豸��֤��
    
} ACCESS_DEVICE_INFO, *LPACCESS_DEVICE_INFO;

typedef struct tagNET_EHOME_LOG_INFO
{
    DWORD                  dwSize;
    BYTE                   byLogPath[256];            //��־·��
    DWORD                  dwLogLevel;                //��־����
    BOOL                   bAutoDel;                  //�Ƿ��Զ�ɾ��
    BYTE                   byRes[512];
}NET_EHOME_LOG_INFO, *LPNET_EHOME_LOG_INFO;

typedef enum tagVideoIntercomeCmd
{
    VI_UNKNOWN = 0,
    VI_REQUEST,
    VI_CANCEL,
    VI_ANSWER,
    VI_REJECT,
    VI_BELL_TIMEOUT,
    VI_HANGUP,
    VI_DEVICE_ON_CALL
}VI_CMD;

// CEHomeDemoDlg �Ի���
class CEHomeDemoDlg : public CDialog
{
// ����
public:
    CEHomeDemoDlg(CWnd* pParent = NULL);    // ��׼���캯��
    virtual ~CEHomeDemoDlg();
    static BOOL CALLBACK fnPREVIEW_NEWLINK_CB(LONG iPreviewHandle,NET_EHOME_NEWLINK_CB_MSG *pNewLinkCBMsg, void *pUserData);
    static void CALLBACK fnPREVIEW_EXCEPTION_CB(DWORD dwType, LONG iUserID, LONG iHandle, void* pUser);
// �Ի�������
    enum { IDD = IDD_EHOMEDEMO_DIALOG };
    void SetAddr(CString csIP, int nPort);
    void GetLocalIP(char* pIPAddr) const;
    char m_sLocalIP[128];
    int  m_nPort;
	LONG m_lCmsLFHandle;
	LONG m_lCMSLFListenHandle;

    void GetAddressByType(LONG iType, DWORD dwVersion, char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen) const;
    void RecordDevAuthInfo(const char* pDeviceID, const char* pDevAuthKey);
    std::string GetDevAuthKey(const char* pDeviceID);
protected:
    virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
    static void CALLBACK fnPREVIEW_DATA_CB( LONG  iPreviewHandle, NET_EHOME_PREVIEW_CB_MSG *pPreviewCBMsg, void *pUserData);
    // ʵ��
protected:
    HICON m_hIcon;

    // ���ɵ���Ϣӳ�亯��
    virtual BOOL OnInitDialog();
    afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
    afx_msg void OnPaint();
    afx_msg HCURSOR OnQueryDragIcon();
    //afx_msg void OnTimer(UINT_PTR nIDEvent);
    DECLARE_MESSAGE_MAP()
private:
    void InitLanguage();
    void InitChildWindow(void);
    void InitMainWin();
    void LoadTreeImage(void);
    void CreateTree();
    void InitParamFromXML();

    void InitPreviewRect();
   

    void OnSelchangeComboListType();
    void ListRestore();
    BOOL IsPlaying(void);
    void EnlargeList(CListCtrl &list, BOOL &bEnlarge);
    void PreviewReferShow(BOOL bShow);
    void MoveChildWin(DWORD dwWinType);
    HTREEITEM GetAfterItem(HTREEITEM hRoot);

    LRESULT OnWMAddLog(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMAddDev(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMDelDev(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMDelOfflineDev(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMProcException(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMChangeIPAddr(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMSetPreview(WPARAM wParam, LPARAM lParam);
    LRESULT OnWMDevSleep(WPARAM wParam, LPARAM lParam);

    void PlayChan(int iDeviceIndex, int iChanIndex,HTREEITEM hChanItem);
    void PlayDevice(int iDeviceIndex, int iStartOutputIndex);
    void SetStreamExceptionCallBack();
    LRESULT ChangeChannelItemImage(WPARAM wParam, LPARAM lParam);
    HTREEITEM GetChanItem(int iDeviceIndex, int iChanIndex);
    void StopPlayAll(void);
    void ChangePlayBtnState(void);
    void CyclePreview();
    void InitLib();
    void InitPreviewListenParam();
    void StopPreviewListen();
    void SetDeviceIndex(LONG iDeviceIndex);
    void FormatRegAddress(char *pOutValue, DWORD dwOutLen, DWORD dwVersion, LONG iMapType, CXmlBase &xmlBase) const; //����ע��ʱ�ĵ�ַ
    void GetStreamAddress(char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen, LONG iMapType, CXmlBase &xmlBase) const;
    void GetPlayBackServerAddress(char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen, LONG iMapType, CXmlBase &xmlBase) const;
    void GetAudioAddress(char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen, LONG iMapType, CXmlBase &xmlBase) const;
    void GetDasAddress(char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen, LONG iMapType, CXmlBase &xmlBase) const;
    BOOL IsNULLAddress(char *pAddress) const;
    void GetPicServerAddress(char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen, LONG iMapType, CXmlBase &xmlBase) const;

    void ReadDevAuthInfoFromXML();

private:
    int m_iTreeWidth;   //device list displaywidth on main interface
    int  m_iRightWidth;    //preview config dialog box size or video control
    int  m_iFunBtnHeight;    //height of main function button area
    int  m_iListLogHeight;    //height of log

    int m_iSelListType; //0-log list,1-alarm list

    CRect m_rectPreviewBG;//preview background
    CRect m_rectRightArea;//ptz area    

    CImageList m_imageTreeList;

    BOOL m_bListLogEnlarge;
    BOOL m_bListAlarmEnlarge;
    int m_iMainType;    

    LONG m_lCmsAlarm;

//     LONG m_lPreviewListen1;
//     LONG m_lPreviewListen2;

    HTREEITEM m_hCurDeviceItem;
    HTREEITEM m_hCurChanItem;
    BOOL    m_bCyclePreview;

   

    typedef std::map<LONG, LONG> HandleSessiuonMap;
    typedef HandleSessiuonMap::iterator HandleSessiuonMapIter;
    HandleSessiuonMap m_mapHandleSession;

    DEVIDCHANGECB m_fnDevCB;
    void *m_pDevUserData;

    enum
    {
        DevOnServerAdd = 0,
        StreamAdd = 1,
        AudioAdd = 2,
        DASAdd = 3,
        PSSAdd = 4,
        PlayBackAdd = 5,
    };

    typedef std::map<string, string> DevAuthKeyMap;
    typedef DevAuthKeyMap::iterator DevAuthKeyMapIter;
    DevAuthKeyMap m_mapDevAuthKeyInfos;
public:

    static void CALLBACK EcmsCallback(NET_EHOME_CMSCB_DATA *pData, void* pUser);
    BOOL m_bAsyncCalllback;

    
    BOOL PopSessionByHandle(LONG handle, LONG *pSession = NULL);
    BOOL RegisterDevIDMsg(DEVIDCHANGECB fnCB, void *pUserData);
    void DeviceIDChanged();
    
    CDlgOutput* m_pDlgPreview;
    CDlgPlayBack* m_dlgPlayBack;
    CDlgAudioTalk* m_dlgAudioTalk;
    CDlgCommonCfg* m_dlgCommonCfg;
    CDlgLocalCfg* m_dlgLocalCfg;
    CDlgOutputCtrl* m_dlgOutputCtrl;
    CDlgHttpAlarm* m_dlgHttpAlarm;
    LONG m_lUdpAlarmHandle;
    LONG m_lTcpAlarmHandle;
    LONG m_lEhome50AlarmHandle;
    LONG m_lSSHandle;

    CComboBox m_comboWinNum;
    CTreeCtrl m_treeDeviceList;
    CListCtrl m_listAllLog;
    CListCtrl m_listAlarmInfo;

    int m_iCurWndNum;    //screen split mode 1, 4, 9, 16, 25, 36
    int m_iCurWndIndex; //current selected split window index, start from 0
    int m_iCurDeviceIndex;
    int m_iCurChanIndex;
    BOOL   m_bRecord;
    NET_EHOME_SERVER_INFO struServInfo;
    void ArrangeOutputs(int iNumber);
    BOOL IsInAccessDeviceList(char* pSerialNumber);

    void AddLog(int iDeviceIndex, int iLogType, int iComType, const char* format, ...);
    void ProcessAlarmData(DWORD dwAlarmType, void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen, void *pUrl, DWORD dwUrlLen);
    int GetCurDeviceIndex();
    int GetCurChanIndex();
    void ProcessEhomeAlarm(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeHeatMapReport(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeFaceSnapReport(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeGps(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeCid(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeNoticePicUrl(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeNotifyFail(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeWirelessInfo(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    //void ProcessEhomeGpsData(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen);
    void ProcessEhomeAlarmAcs(void *pXml, DWORD dwXmlLen);
    void ProcessEhomeAlarmQrCode(void *pXml, DWORD dwXmlLen);
    void ProcessEhomeAlarmFaceTemp(void *pXml, DWORD dwXmlLen);
    void ProcessHttpAlarmInfo(void *pXml, DWORD dwXmlLen, void *pUrl, DWORD dwUrlLen);
    void ProcessEhomeMPDCData(void *pStru, DWORD dwStruLen, void *pXml, DWORD dwXmlLen); 
    BOOL XmlPrase(char* pXml, char* pInputBefore, char* pInputAfter, char* pOutput);
    BOOL MakeRecDir();
    void ManageVideoIntercomeCmd(const char *cCmdType, const char *cDeviceID, const char *cEvent);

    afx_msg void OnBnClickedRadioAlarmInfo();
    afx_msg void OnBnClickedRadioLocalLog();
    afx_msg void OnCbnSelchangeComboWndnum();
    afx_msg void OnLvnColumnclickListAllLog(NMHDR *pNMHDR, LRESULT *pResult);
    afx_msg void OnBnClickedBtnPlayback();
    afx_msg void OnBnClickedBtnPreview();
    afx_msg void OnBnClickedBtnAudioTalk();
    afx_msg void OnBnClickedBtnCfg();
    afx_msg void OnBnClickedBtnLocalCfg();
    afx_msg void OnBnClickedBtnExit();
protected:
    virtual void OnCancel();
    BOOL GetChannelbySessionID(char* pDevice, int iSessionID, int& iIndex, int& iChanel);
public:
    afx_msg void OnLvnColumnclickListAlarmInfo(NMHDR *pNMHDR, LRESULT *pResult);
//    afx_msg void OnTvnSelchangedTreeDev(NMHDR *pNMHDR, LRESULT *pResult);
    afx_msg void OnNMClickTreeDev(NMHDR *pNMHDR, LRESULT *pResult);
    afx_msg void OnNMDblclkTreeDev(NMHDR *pNMHDR, LRESULT *pResult);
    afx_msg void OnBnClickedBtnPreviewListen();
    afx_msg void OnBnClickedBtnOther();
    afx_msg void OnNMRClickTreeDev(NMHDR *pNMHDR, LRESULT *pResult);
    afx_msg void OnBnClickedBtnCirclePreview();
    afx_msg void OnTimer(UINT_PTR nIDEvent);
    afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
    afx_msg void OnBnClickedBtnClosePreview();
    int m_iPreviewHandle;
    afx_msg void OnMenuChannelInfo();
    void DelDev(LONG lLoginID);
    void DelOfflineDev(char* sDeviceSerial);
    WINDOWPLACEMENT m_struOldWndpl;
    


    BOOL    m_bUseAccessList;
    ACCESS_DEVICE_INFO m_stAccessDeviceList[64];

    NET_EHOME_SERVER_INFO_V50 m_struServInfo;
    NET_EHOME_LOG_INFO m_struLogInfo;

    BYTE m_byUseCmsPort;

    DWORD m_byCmsSecureAccessType;
    DWORD m_byAlarmSecureAccessType;
    DWORD m_byStreamSecureAccessType;
    DWORD m_byAlarmPictureSeparate;

    char m_szDBKey[32];
    BOOL m_bSound;

    WORD m_wAlarmServerMqttPort;

    CDlgVideoIntercomCall *m_pDlgVI;
    DWORD m_dwType; //
    BOOL m_bRtpAudio;
private:
    CDlgPassthroughProxy m_PassthroughProxyDlg;
public:
    afx_msg void OnMenuProxy();
    afx_msg void OnGetGpsInfo();
    afx_msg void OnMenuIsapiPt();
    afx_msg void OnBnClickedBtnSalve();
    afx_msg void OnMenuUpgrade();
    afx_msg void FullScreen(BOOL bFullScreen);

    afx_msg void OnBtnWirelessInfo();
    afx_msg void OnBnClickedBtnSound();
    afx_msg void OnEnableCfg();
    afx_msg void OnRecodrCfg();
    afx_msg void OnCapturePic();
    afx_msg void OnMontionArea();
    afx_msg void OnPrivateArea();
    afx_msg void OnHideAlarm();
    afx_msg void OnMenuHttpUpgrade();
    afx_msg void OnMenuIsapiCfg();
    afx_msg void OnMenuAddEhome50DevAuth();
    afx_msg void OnMenuSetEhome50DevAuth();
    afx_msg void OnBnClickedBtnIframe();
    afx_msg void OnBnClickedBtnStreamEncrypt();
    afx_msg void OnMenuStreamCheck();
    afx_msg void OnMenuHttpAlarm();
    afx_msg void OnBnClickedChkAsync();
    afx_msg void OnMenuStorageserver();
    void GetServerType(int &iServerType, char *pDasInfoPath, DWORD dwDasInfoPathLen);
    void GetDasAddressByType(LONG iType, DWORD dwVersion, char *pOutValue, DWORD dwOutLen, char *pSubValue, DWORD dwSubLen) const;
    afx_msg void OnMenuLogout();
    afx_msg void OnMenuWakeupDevice();
    afx_msg void OnMenuSleepDevice();
    afx_msg void OnMenuWebConfig();
    CString m_strDasConfigFile;
    BOOL GetISAPIChannels(LONG lUserID, DWORD *dwChannelNum);
    afx_msg void OnBnClickedCheckRtpAudio();
    //ISAPI͸����ز���
    static DWORD WINAPI ProxyWorkThread(LPVOID lpParameter);
    HANDLE m_hProxyWorkHandle;
    BOOL   m_bProxyExitStatus;
    HANDLE m_hProxySemaphore;
    std::vector<long> m_vecProxyHttpHandle;
    CRITICAL_SECTION m_csProxy;
};

extern BOOL g_bArrayEncrypt[MAX_LISTEN_NUM];
