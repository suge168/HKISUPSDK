# -*- coding: utf-8 -*-
# @Time : 2023/9/8 11:03
# @Author : qiu jiahang
import os
import time
from ctypes import *
import ctypes

from src.SdkService.CmsService import HCISUPCMS
from src.SdkService.CmsService.CmsDemo import cmsClass
from src.Common import glo
from src.SdkService.StreamService import HCISUPStream
from src.SdkService.StreamService.HCISUPStream import sms_sdkdllpath, NET_EHOME_ESTREAM_INIT_CFG_TYPE, \
    NET_EHOME_LISTEN_PREVIEW_CFG, PREVIEW_NEWLINK_CB, NET_EHOME_PREVIEW_DATA_CB_PARAM, PREVIEW_DATA_CB, \
    LPNET_EHOME_PREVIEW_CB_MSG
from src.SdkService.CmsService.HCISUPCMS import load_library, libeay_dllpath, ssleay_dllpath, strPathCom_dllpath, \
    NET_EHOME_LOCAL_CFG_TYPE, sys_platform, target_dir, NET_EHOME_PREVIEWINFO_IN, NET_EHOME_PREVIEWINFO_OUT, \
    NET_EHOME_PUSHSTREAM_IN, NET_EHOME_PUSHSTREAM_OUT


class voiceClass(object):
    def __init__(self):
        self.smsDLL = self.loadDLL()  # 初始化sdk
        self.cmsClass = cmsClass()
        self.smsHandle = -1  # 监听句柄
        self.lPreviewHandle = -1  # 预览句柄
        self.fVOICETALK_NEWLINK_CB = HCISUPStream.VOICETALK_NEWLINK_CB(self.FVOICETALK_NEWLINK_CB)
        self.fVOICE_DATA_CB = HCISUPStream.VOICETALK_DATA_CB(self.FVOICE_DATA_CB)

        self.Count = 0  # 降低回调打印频率使用
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

    def FVOICE_DATA_CB(self, lHandle, pNewLinkCBInfo, pUserData):
        print('设备音频发送.....')
        # 回调函数保存设备返回的语音数据
        self.file_pcm = os.path.abspath("..//..//resources//audioFile//DevicetoPlat.g7")

        # 确保目录存在
        os.makedirs(os.path.dirname(self.file_pcm), exist_ok=True)

        try:
            with open(self.file_pcm, 'ab') as output_stream_pcm:  # 使用追加模式
                pData_pointer = ctypes.cast(pNewLinkCBInfo.pData, POINTER(c_byte * pNewLinkCBInfo.dwDataLen)).contents
                bytes_data = (c_byte * pNewLinkCBInfo.dwDataLen)()
                ctypes.memmove(bytes_data, pData_pointer, pNewLinkCBInfo.dwDataLen)
                output_stream_pcm.write(bytes(bytes_data))  # 转换为 bytes
        except IOError as e:
            print(e)
        except Exception as e:
            print(e)

        return True

    def FVOICETALK_NEWLINK_CB(self, lHandle, pNewLinkCBInfo, pUserData):
        print("fVOICE_NEWLINK_CB callback")
        self.lVoiceLinkHandle = lHandle

        # 创建回调参数结构
        net_ehome_voicetalk_data_cb_param = HCISUPStream.NET_EHOME_VOICETALK_DATA_CB_PARAM()

        net_ehome_voicetalk_data_cb_param.fnVoiceTalkDataCB = self.fVOICE_DATA_CB

        # 调用设置回调函数的方法
        result = self.smsDLL.NET_ESTREAM_SetVoiceTalkDataCB(lHandle, net_ehome_voicetalk_data_cb_param)
        if not result:
            print("NET_ESTREAM_SetVoiceTalkDataCB()错误代码号：", self.smsDLL.NET_ESTREAM_GetLastError())
            return False

        return True

    def start_voice_serve_listen(self):
        if 'VOICETALK_newlink_cb' not in globals():
            global VOICETALK_newlink_cb
            VOICETALK_newlink_cb = HCISUPStream.VOICETALK_NEWLINK_CB()  # 创建回调实例

        net_ehome_listen_voicetalk_cfg = HCISUPStream.NET_EHOME_LISTEN_VOICETALK_CFG()

        # 从配置中读取IP和端口
        ip_bytes = glo.get_value("VoiceSmsServerListenIP").encode()
        ip_array = (c_ubyte * 128)(*ip_bytes)  # 创建 c_ubyte 数组并填充数据
        port = int(glo.get_value("VoiceSmsServerListenPort"))

        # 设置IP地址和端口
        net_ehome_listen_voicetalk_cfg.struIPAdress.szIP = ip_array
        net_ehome_listen_voicetalk_cfg.struIPAdress.wPort = port
        net_ehome_listen_voicetalk_cfg.fnNewLinkCB = self.fVOICETALK_NEWLINK_CB
        net_ehome_listen_voicetalk_cfg.byLinkMode = 0

        # 启动语音监听
        VoicelServHandle = self.smsDLL.NET_ESTREAM_StartListenVoiceTalk(net_ehome_listen_voicetalk_cfg)

        if VoicelServHandle == -1:
            print("NET_ESTREAM_StartListenVoiceTalk failed, error code:", self.smsDLL.NET_ESTREAM_GetLastError())
            self.smsDLL.NET_ESTREAM_Fini()
        else:
            voice_stream_listen_info = f"{ip_bytes.decode().strip()}_{port}"
            glo.set_value("VoicelServHandle", VoicelServHandle)
            print("语音流媒体服务：", voice_stream_listen_info, ", NET_ESTREAM_StartListenVoiceTalk succeed")

    def start_voice_trans(self):
        # 语音对讲开启请求的输入参数
        net_ehome_voice_talk_in = HCISUPStream.NET_EHOME_VOICE_TALK_IN()
        ip_bytes = glo.get_value("VoiceSmsServerIP").encode()
        ip_array = (c_ubyte * 128)(*ip_bytes)
        net_ehome_voice_talk_in.struStreamSever.szIP = ip_array
        net_ehome_voice_talk_in.struStreamSever.wPort = int(glo.get_value("VoiceSmsServerPort"))
        net_ehome_voice_talk_in.dwVoiceChan = 1  # 语音通道号

        # 语音对讲开启请求的输出参数
        net_ehome_voice_talk_out = HCISUPStream.NET_EHOME_VOICE_TALK_OUT()

        # 发送语音对讲开启请求
        if not self.cmsClass.cmsDLL.NET_ECMS_StartVoiceWithStmServer(glo.get_value('iUserID'),
                                                                     byref(net_ehome_voice_talk_in),
                                                                     byref(net_ehome_voice_talk_out)):
            print("NET_ECMS_StartVoiceWithStmServer failed, error code:",
                  self.cmsClass.cmsDLL.hCEhomeCMS.NET_ECMS_GetLastError())
        else:
            print("NET_ECMS_StartVoiceWithStmServer success, sessionID =", net_ehome_voice_talk_out.lSessionID)

        # 语音传输请求的输入参数
        struPushVoiceIn = HCISUPStream.NET_EHOME_PUSHVOICE_IN()
        struPushVoiceIn.dwSize = sizeof(struPushVoiceIn)
        struPushVoiceIn.lSessionID = net_ehome_voice_talk_out.lSessionID
        voiceTalkSessionId = net_ehome_voice_talk_out.lSessionID

        # 语音传输请求的输出参数
        struPushVoiceOut = HCISUPStream.NET_EHOME_PUSHVOICE_OUT()
        struPushVoiceOut.dwSize = sizeof(struPushVoiceOut)

        # 发送语音传输请求
        if not self.cmsClass.cmsDLL.NET_ECMS_StartPushVoiceStream(glo.get_value('iUserID'), byref(struPushVoiceIn),
                                                                  byref(struPushVoiceOut)):
            print("NET_ECMS_StartPushVoiceStream failed, error code:", self.cmsClass.cmsDLL.NET_ECMS_GetLastError())
        else:
            print("NET_ECMS_StartPushVoiceStream success!")

        file_path = "..//..//resources//audioFile//temp.g711u"
        try:
            # 打开G711编码的音频文件
            with open(file_path, 'rb') as audio_file:
                while True:
                    buffer = audio_file.read(160)  # 每次读取160字节
                    if not buffer:
                        break

                    ptr_g711_send = (c_ubyte * len(buffer))(*buffer)
                    stru_voice_talk_data = HCISUPStream.NET_EHOME_VOICETALK_DATA()
                    stru_voice_talk_data.pData = cast(ptr_g711_send, c_void_p)
                    stru_voice_talk_data.dwDataLen = len(buffer)

                    # 发送音频数据
                    result = self.smsDLL.NET_ESTREAM_SendVoiceTalkData(glo.get_value("VoicelServHandle"),
                                                                       stru_voice_talk_data)
                    if result <= -1:
                        print("NET_ESTREAM_SendVoiceTalkData failed, error code:",
                              self.smsDLL.NET_ESTREAM_GetLastError())

                    # 控制发送速率
                    time.sleep(0.02)  # 20毫秒
        except IOError as e:
            print("Error reading file:", e)
        except Exception as e:
            print("An error occurred:", e)
        return True

    def StopVoiceTrans(self):
        # SMS 停止语音对讲
        if self.lVoiceLinkHandle >= 0:
            if not self.smsDLL.NET_ESTREAM_StopVoiceTalk(self.lVoiceLinkHandle):
                print(f"NET_ESTREAM_StopVoiceTalk failed, error code: {self.smsDLL.NET_ESTREAM_GetLastError()}")
                return
            else:
                print("NET_ESTREAM_StopVoiceTalk success")
        if not self.smsDLL.NET_ESTREAM_StopListenVoiceTalk(glo.get_value("VoicelServHandle")):
            print(
                f"NET_ESTREAM_StopListenVoiceTalk failed, error code: {self.smsDLL.NET_ESTREAM_GetLastError()}")
            return

        # 释放语音对讲请求资源
        if not self.cmsClass.cmsDLL.NET_ECMS_StopVoiceTalkWithStmServer(glo.get_value('iUserID'), glo.get_value("VoicelServHandle")):
            print(
                f"NET_ECMS_StopVoiceTalkWithStmServer failed, error code: {self.cmsClass.cmsDLL.NET_ECMS_GetLastError()}")
            return
        else:
            print("NET_ECMS_StopVoiceTalkWithStmServer success")
