# -*- coding: utf-8 -*-
# @Time : 2023/9/8 11:03
# @Author : qiu jiahang
import ctypes
import os
from ctypes import *
from datetime import datetime

from src.Common import glo
from src.SdkService.CmsService.CmsDemo import cmsClass
from src.SdkService.CmsService.HCISUPCMS import load_library, libeay_dllpath, ssleay_dllpath, strPathCom_dllpath, \
    NET_EHOME_LOCAL_CFG_TYPE, sys_platform, target_dir, NET_EHOME_PREVIEWINFO_IN, NET_EHOME_PREVIEWINFO_OUT, \
    NET_EHOME_PUSHSTREAM_IN, NET_EHOME_PUSHSTREAM_OUT
from src.SdkService.StreamService.HCISUPStream import *


class smsClass(object):
    def __init__(self):
        self.smsDLL = self.loadDLL()  # 初始化sdk
        self.cmsDLL = glo.get_value('cmsDLL')
        self.smsHandle = -1  # 监听句柄
        self.backSessionID = -1  # 回放sessionID
        self.lPreviewHandle = -1  # 预览句柄
        self.m_lPlayBackLinkHandle = -1  # 回放句柄
        self.fPREVIEW_NEWLINK_CB_FILE = PREVIEW_NEWLINK_CB(self.FPREVIEW_NEWLINK_CB_FILE)  # 预览监听回调函数实现 - 文件模式
        self.fPREVIEW_DATA_CB_FILE = PREVIEW_DATA_CB(self.FPREVIEW_DATA_CB_FILE)  # 预览回调函数实现 - 文件存储

        self.fPLAYBACK_NEWLINK_CB_FILE = PLAYBACK_NEWLINK_CB(self.PLAYBACK_NEWLINK_CB_FILE)  # 回放监听回调函数实现 - 文件模式
        self.fPLAYBACK_DATA_CB_FILE = PLAYBACK_DATA_CB(self.PLAYBACK_DATA_CB_FILE)  # 回放回调函数实现 - 文件存储

        self.Count = 0  # 降低回调打印频率使用
        # 预览视频文件的输出位置设置
        self.preview_file_path = target_dir + '\\outputFiles\\' + str(
            datetime.timestamp(datetime.now())) + '_previewVideo.mp4'
        self.playbackFile = target_dir + '\\outputFiles\\' + str(
            datetime.timestamp(datetime.now())) + '_playbackVideo.mp4'
        glo.set_value('smsDLL', self.smsDLL)

    def loadDLL(self):
        '''
        根据操作系统加载sdk动态库
        :return:
        '''
        try:
            print("sms_sdkdllpath: ", sms_sdkdllpath)
            self.smsDLL = load_library(sms_sdkdllpath)

            libeay32_p = ctypes.cast(ctypes.c_char_p(libeay_dllpath.encode()), POINTER(c_void_p))
            ssleay32_p = ctypes.cast(ctypes.c_char_p(ssleay_dllpath.encode()), POINTER(c_void_p))
            self.smsDLL.NET_ESTREAM_SetSDKInitCfg(
                NET_EHOME_ESTREAM_INIT_CFG_TYPE.NET_EHOME_ESTREAM_INIT_CFG_LIBEAY_PATH.value,
                libeay32_p)
            self.smsDLL.NET_ESTREAM_SetSDKInitCfg(
                NET_EHOME_ESTREAM_INIT_CFG_TYPE.NET_EHOME_ESTREAM_INIT_CFG_SSLEAY_PATH.value,
                ssleay32_p)

            # 流媒体初始化
            self.smsDLL.NET_ESTREAM_Init()

            # 设置HCAapSDKCom组件库文件夹所在路径
            strPathCom_p = ctypes.cast(ctypes.c_char_p(strPathCom_dllpath.encode()), POINTER(c_void_p))
            self.smsDLL.NET_ESTREAM_SetSDKLocalCfg(NET_EHOME_LOCAL_CFG_TYPE.COM_PATH.value, strPathCom_p)

            self.smsDLL.NET_ESTREAM_SetLogToFile(3, (target_dir + '/IsupSDKLog/').encode('utf-8'), False)  # 日志
            return self.smsDLL
        except OSError as e:
            print('sms 动态库加载失败', e)

    def startRealPlayListen_File(self):
        """开启sms服务监听"""
        struPreviewListen = NET_EHOME_LISTEN_PREVIEW_CFG()
        ssAddress = ctypes.create_string_buffer(glo.get_value("SmsServerListenIP").encode())
        ctypes.memmove(struPreviewListen.struIPAdress.szIP, ssAddress, ctypes.sizeof(ssAddress))
        struPreviewListen.struIPAdress.wPort = glo.get_value("SmsServerListenPort")
        struPreviewListen.pUser = None
        struPreviewListen.byLinkMode = 0  # 0- TCP方式，1- UDP方式
        struPreviewListen.fnNewLinkCB = self.fPREVIEW_NEWLINK_CB_FILE

        if self.smsHandle < 0:
            self.smsHandle = self.smsDLL.NET_ESTREAM_StartListenPreview(ctypes.byref(struPreviewListen))
            if (self.smsHandle < 0):
                print('NET_ESTREAM_StartListenPreview Fail,err code: ', self.smsDLL.NET_ESTREAM_GetLastError())
            else:
                print('流媒体服务器: ', glo.get_value("SmsServerListenIP") + '_' + str(glo.get_value("SmsServerListenPort")))

    def startPlayBackListen_FILE(self):
        """按时间回放-开启sms服务监听"""
        struPlayBackListen = NET_EHOME_PLAYBACK_LISTEN_PARAM()
        ssAddress = ctypes.create_string_buffer(glo.get_value("SmsBackServerListenIP").encode())
        ctypes.memmove(struPlayBackListen.struIPAdress.szIP, ssAddress, ctypes.sizeof(ssAddress))
        struPlayBackListen.struIPAdress.wPort = glo.get_value("SmsBackServerListenPort")
        struPlayBackListen.byLinkMode = 0  # 0- TCP方式，1- UDP方式
        struPlayBackListen.fnNewLinkCB = self.fPLAYBACK_NEWLINK_CB_FILE

        if self.smsHandle < 0:
            self.smsHandle = self.smsDLL.NET_ESTREAM_StartListenPlayBack(ctypes.byref(struPlayBackListen))
            if (self.smsHandle < 0):
                print('NET_ESTREAM_StartListenPlayBack Fail,err code: ', self.smsDLL.NET_ESTREAM_GetLastError())
            else:
                print('回放流媒体服务器: ',
                      glo.get_value("SmsBackServerListenIP") + '_' + str(glo.get_value("SmsBackServerListenPort")))

    # 将视频流保存到本地
    def writeFile(self, filePath, pBuffer, dwBufSize):
        # 使用memmove函数将指针数据读到数组中
        data_array = (c_byte * dwBufSize)()
        memmove(data_array, pBuffer, dwBufSize)

        # 判断文件路径是否存在
        if not os.path.exists(filePath):
            # 如果不存在，使用 open() 函数创建一个空文件
            open(filePath, "w").close()

        preview_file_output = open(filePath, 'ab')
        preview_file_output.write(data_array)
        preview_file_output.close()

    def stopSmsListen(self):
        """sms服务停止监听"""
        if (self.smsHandle > -1):
            self.smsDLL.NET_ESTREAM_StopListenPreview(self.smsHandle)
            self.smsDLL.NET_ESTREAM_Fini()

    def FPREVIEW_DATA_CB_FILE(self, iPreviewHandle, pPreviewCBMsg, pUserData):
        pPreviewCBMsg = cast(pPreviewCBMsg, LPNET_EHOME_PREVIEW_CB_MSG).contents
        if self.Count == 100:  # 降低打印频率
            print(f"FPREVIEW_DATA_CB callback, data length: {pPreviewCBMsg.dwDataLen}")
            self.Count = 0
        self.Count += 1

        # 码流回调函数
        if pPreviewCBMsg.byDataType == 1:
            pass
        elif pPreviewCBMsg.byDataType == 2:
            self.writeFile(self.preview_file_path, pPreviewCBMsg.pRecvdata, pPreviewCBMsg.dwDataLen)
        else:
            print(u'其他数据,长度:', pPreviewCBMsg.dwDataLen)

    def PLAYBACK_DATA_CB_FILE(self, iPlayBackLinkHandle, pDataCBInfo, pUserData):
        pDataCBInfo = cast(pDataCBInfo, LPNET_EHOME_PLAYBACK_DATA_CB_INFO).contents
        if self.Count == 100:  # 降低打印频率
            print(f"FPLAYBACK_DATA_CB callback , dwDataLen: {pDataCBInfo.dwDataLen},dwType:{pDataCBInfo.dwType}")
            self.Count = 0
        self.Count += 1

        # 码流回调函数
        if pDataCBInfo.dwType == 1:
            glo.set_value("stopPlayBackFlag", False)  # 初始化回放标记
            # self.stopPlayBackFlag = False
        elif pDataCBInfo.dwType == 3:
            print("收到回放结束信令！")
            glo.set_value("stopPlayBackFlag", True)  # 用来标记是否回放结束
            # self.stopPlayBackFlag = True
        elif pDataCBInfo.dwType == 2:
            self.writeFile(self.playbackFile, pDataCBInfo.pData, pDataCBInfo.dwDataLen)
        else:
            print(u'其他数据,长度:', pDataCBInfo.dwDataLen)

    def FPREVIEW_NEWLINK_CB_FILE(self, lLinkHandle, pNewLinkCBMsg, pUserData):
        print("FPREVIEW_NEWLINK_CB_File callback")
        # 预览数据回调参数
        self.lPreviewHandle = lLinkHandle
        struDataCB = NET_EHOME_PREVIEW_DATA_CB_PARAM()
        struDataCB.fnPreviewDataCB = self.fPREVIEW_DATA_CB_FILE
        if self.smsDLL.NET_ESTREAM_SetPreviewDataCB(lLinkHandle, struDataCB) is False:
            print("NET_ESTREAM_SetPreviewDataCB failed err:：", self.smsDLL.NET_ESTREAM_GetLastError())
            return False
        return True

    def PLAYBACK_NEWLINK_CB_FILE(self, lPlayBackLinkHandle, pNewLinkCBInfo, pUserData):
        pNewLinkCBInfo = cast(pNewLinkCBInfo, LPNET_EHOME_PLAYBACK_NEWLINK_CB_INFO).contents
        szDeviceID = str(pNewLinkCBInfo.szDeviceID, encoding="utf-8").rstrip('\x00')
        print(
            f"PLAYBACK_NEWLINK_CB callback, szDeviceID:{szDeviceID} ,lSessionID:{pNewLinkCBInfo.iSessionID}, dwChannelNo:{pNewLinkCBInfo.dwChannelNo}")
        # 回放数据回调参数
        self.m_lPlayBackLinkHandle = lPlayBackLinkHandle
        struCBParam = NET_EHOME_PLAYBACK_DATA_CB_PARAM()
        struCBParam.fnPlayBackDataCB = self.fPLAYBACK_DATA_CB_FILE
        struCBParam.byStreamFormat = 0
        if self.smsDLL.NET_ESTREAM_SetPlayBackDataCB(lPlayBackLinkHandle, struCBParam) is False:
            print("NET_ESTREAM_SetPlayBackDataCB failed err:：", self.smsDLL.NET_ESTREAM_GetLastError())
            return False
        return True

    def RealPlay(self, channel):
        struPreviewIn = NET_EHOME_PREVIEWINFO_IN()
        struPreviewIn.iChannel = channel  # 预览通道号
        struPreviewIn.dwLinkMode = 0  # 0 - TCP方式，1 - UDP方式
        struPreviewIn.dwStreamType = 0  # 码流类型：0 - 主码流，1 - 子码流, 2 - 第三码流

        smsAddress = ctypes.create_string_buffer(glo.get_value("SmsServerIP").encode())  # 流媒体服务器IP地址, 公网地址
        ctypes.memmove(struPreviewIn.struStreamSever.szIP, smsAddress, ctypes.sizeof(smsAddress))
        struPreviewIn.struStreamSever.wPort = glo.get_value("SmsServerPort")  # 流媒体服务器端口，需要跟服务器启动监听端口一致
        struPreviewOut = NET_EHOME_PREVIEWINFO_OUT()
        getRS = self.cmsDLL.NET_ECMS_StartGetRealStream(glo.get_value('iUserID'), ctypes.byref(struPreviewIn),
                                                        ctypes.byref(struPreviewOut))
        if getRS:
            print(f"NET_ECMS_StartGetRealStream succeed, sessionID: {struPreviewOut.lSessionID}")
            self.sessionID = struPreviewOut.lSessionID
        else:
            print(f"NET_ECMS_StartGetRealStream failed, error code: {self.cmsDLL.NET_ECMS_GetLastError()}")
            return

        struPushInfoIn = NET_EHOME_PUSHSTREAM_IN()
        struPushInfoIn.dwSize = sizeof(struPushInfoIn)
        struPushInfoIn.lSessionID = self.sessionID

        struPushInfoOut = NET_EHOME_PUSHSTREAM_OUT()
        struPushInfoOut.dwSize = sizeof(struPushInfoOut)

        if self.cmsDLL.NET_ECMS_StartPushRealStream(glo.get_value('iUserID'), struPushInfoIn,
                                                    struPushInfoOut) is False:
            print(f"NET_ECMS_StartPushRealStream failed, error code: {self.cmsDLL.NET_ECMS_GetLastError()}")
            return
        else:
            print(f"NET_ECMS_StartPushRealStream succeed, sessionID:{struPushInfoIn.lSessionID}")

    def StopRealPlay(self):
        if self.smsDLL.NET_ESTREAM_StopPreview(self.lPreviewHandle):
            print(f"NET_ESTREAM_StopPreview succ ")
        else:
            print(f"NET_ESTREAM_StopPreview failed, error code: {self.smsDLL.NET_ESTREAM_GetLastError()}")
            return
        print("停止Stream的实时流转发")

        if self.cmsDLL.NET_ECMS_StopGetRealStream(glo.get_value('iUserID'), self.sessionID):
            print(f"NET_ECMS_StopGetRealStream succ ")
        else:
            print(f"NET_ECMS_StopGetRealStream failed,err =  {self.cmsDLL.NET_ECMS_GetLastError()}")
            return
        print("CMS发送预览停止请求")

    def PlayBackByTime(self, lchannel):
        m_struPlayBackInfoIn = NET_EHOME_PLAYBACK_INFO_IN()
        m_struPlayBackInfoIn.dwSize = sizeof(m_struPlayBackInfoIn)
        m_struPlayBackInfoIn.dwChannel = lchannel  # 通道号
        m_struPlayBackInfoIn.byPlayBackMode = 1  # 0 - 按文件名回放，1 - 按时间回放

        play_back_by_time = NET_EHOME_PLAYBACKBYTIME()
        play_back_by_time.struStartTime.wYear = 2024
        play_back_by_time.struStartTime.byMonth = 9
        play_back_by_time.struStartTime.byDay = 4
        play_back_by_time.struStartTime.byHour = 13
        play_back_by_time.struStartTime.byMinute = 2
        play_back_by_time.struStartTime.bySecond = 0

        play_back_by_time.struStopTime.wYear = 2024
        play_back_by_time.struStopTime.byMonth = 9
        play_back_by_time.struStopTime.byDay = 4
        play_back_by_time.struStopTime.byHour = 13
        play_back_by_time.struStopTime.byMinute = 3
        play_back_by_time.struStopTime.bySecond = 30

        m_struPlayBackInfoIn.unionPlayBackMode.struPlayBackbyTime = play_back_by_time

        smsAddress = ctypes.create_string_buffer(glo.get_value("SmsBackServerIP").encode())  # 流媒体服务器IP地址, 公网地址
        ctypes.memmove(m_struPlayBackInfoIn.struStreamSever.szIP, smsAddress, ctypes.sizeof(smsAddress))
        m_struPlayBackInfoIn.struStreamSever.wPort = glo.get_value("SmsBackServerPort")  # 流媒体服务器端口，需要跟服务器启动监听端口一致

        m_struPlayBackInfoOut = NET_EHOME_PLAYBACK_INFO_OUT()

        if self.cmsDLL.NET_ECMS_StartPlayBack(glo.get_value('iUserID'), ctypes.byref(m_struPlayBackInfoIn),
                                              ctypes.byref(m_struPlayBackInfoOut)):
            print(f"NET_ECMS_StartPlayBack succeed, lSessionID: {m_struPlayBackInfoOut.lSessionID}")
        else:
            print(f"NET_ECMS_StartPlayBack failed, error code: {self.cmsDLL.NET_ECMS_GetLastError()}")
            return

        m_struPushPlayBackIn = NET_EHOME_PUSHPLAYBACK_IN()
        m_struPushPlayBackIn.dwSize = sizeof(m_struPushPlayBackIn)
        m_struPushPlayBackIn.lSessionID = m_struPlayBackInfoOut.lSessionID
        self.backSessionID = m_struPushPlayBackIn.lSessionID

        m_struPushPlayBackOut = NET_EHOME_PUSHPLAYBACK_OUT()
        m_struPushPlayBackOut.dwSize = sizeof(m_struPushPlayBackOut)

        if self.cmsDLL.NET_ECMS_StartPushPlayBack(glo.get_value('iUserID'), ctypes.byref(m_struPushPlayBackIn),
                                                  ctypes.byref(m_struPushPlayBackOut)):
            print(
                f"NET_ECMS_StartPushPlayBack succeed, sessionID: {m_struPushPlayBackIn.lSessionID} ,lUserID: {glo.get_value('iUserID')}")
        else:
            print(f"NET_ECMS_StartPushPlayBack failed, error code: {self.cmsDLL.NET_ECMS_GetLastError()}")
            return

    def stopPlayBackByTime(self):
        if not self.cmsDLL.NET_ECMS_StopPlayBack(glo.get_value('iUserID'), self.backSessionID):
            print(f"NET_ECMS_StopPlayBack failed,err = {self.cmsDLL.NET_ECMS_GetLastError()}")
            return
        print("CMS发送回放停止请求")

        if not self.smsDLL.NET_ESTREAM_StopPlayBack(self.m_lPlayBackLinkHandle):
            print(f"NET_ESTREAM_StopPlayBack failed,err =  {self.smsDLL.NET_ESTREAM_GetLastError()}")
            return
        print("停止回放Stream服务的实时流转发")
