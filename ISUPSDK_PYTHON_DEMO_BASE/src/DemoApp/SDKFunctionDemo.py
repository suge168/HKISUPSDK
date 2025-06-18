# --*-- conding:utf-8 --*--
# @Time : 2024/9/19 14:20
# @Author : wangchao124
# @Email : wangchao124@hikvision.com
# @File : SDKFunctionDemo.py
# @Software : PyCharm
import time

from ..Common import ConfRead, glo
from ..SdkService.CmsService import CmsDemo
from ..SdkService.AlarmService import AlarmDemo
from ..SdkService.SsService import SsDemo
from ..SdkService.StreamService import StreamDemo, VoiceDemo


def dispatch(command, lLoginID, cmsDemo, alarmDemo, ssDemo, streamDemo, VoiceDemo):
    if command == "f0001":
        print("\n[Function]CMS控制信令透传(需要设备在线, 获取NVR设备工作状态、远程升级等)")
        cmsDemo.CMS_XMLRemoteControl()

    elif command == "f0002":
        print("\n[Function]ISUP透传接口(获取云存储配置)")
        cmsDemo.CMS_ISAPIPassThrough("GET /ISAPI/System/PictureServer?format=json", "", "")

    elif command == "f0003":
        print("\n[Function]ISUP透传接口(获取设备信息)")
        cmsDemo.CMS_ISAPIPassThrough("GET /ISAPI/System/deviceInfo", "", "")

    elif command == "f0004":
        print("\n[Function]ISUP透传接口(获取设备校时参数)")
        cmsDemo.CMS_ISAPIPassThrough("GET /ISAPI/System/time", "", "")

    elif command == "f0005":
        print("\n[Function]ISUP透传接口(设置设备校时参数)")
        parameter = {"time": "2024-09-23T14:12:47+08:00"}  # 设备校时参数
        time_parameter = ConfRead.get_req_body_from_template("../../resources/conf/deviceParameter/time.xml", parameter)
        print(time_parameter)
        cmsDemo.CMS_ISAPIPassThrough("PUT /ISAPI/System/time", time_parameter, "")

    elif command == "f2000":
        print("\n[Function]上传图片至存储服务器(返回图片URL可以用于人脸下发)")
        pic_name = input(
            "请输入您想要进行图片分析的图片URL地址(图片URL可以参见 f2000:上传图片至存储服务器(返回图片URL可以用于人脸下发) 获取)")
        pic_path = "..//..//resources//pics//" + pic_name
        ssDemo.ssCreateClient(pic_path)
        ssDemo.ssUploadPic()
        ssDemo.ssDestroyClient()

    elif command == "f2001":
        print("\n[Function]抓图")
        cmsDemo.CMS_ISAPIPassThrough(
            "GET /ISAPI/Streaming/channels/101/picture/async?format=json&imageType=JPEG&URLType=cloudURL", "", "")

    elif command == "f3001":
        print("\n[Function]取流预览模块(有预览窗口)")


    elif command == "f3002":
        print("\n[Function]取流预览模块(保存到本地文件)")
        streamDemo.startRealPlayListen_File()
        streamDemo.RealPlay(channel=1)
        time.sleep(5)  # 程序等待 5 秒
        streamDemo.StopRealPlay()


    elif command == "f3003":
        print("\n[Function]按时间回放模块(有回放窗口)")


    elif command == "f3004":
        print("\n[Function]按时间回放模块(保存到本地文件)")
        # 回放监听函数调用
        streamDemo.startPlayBackListen_FILE()
        streamDemo.PlayBackByTime(lchannel=1)
        while not glo.get_value("stopPlayBackFlag"):
            try:
                time.sleep(1)  # 每秒检查一次
            except KeyboardInterrupt:
                # 如果线程被中断，通常需要清除中断状态
                print("Playback was interrupted")
                glo.set_value("stopPlayBackFlag", True)  # 设置标志位，退出循环
                break  # 当前线程被中断时退出循环
        streamDemo.stopPlayBackByTime()
        return

    elif command == "f3005":
        print("\n[Function]语音转发")
        VoiceDemo.start_voice_serve_listen()
        VoiceDemo.start_voice_trans()
        VoiceDemo.StopVoiceTrans()


    elif command == "f3006":
        print("\n[Function]ISUP透传接口(云台控制)")
        parameter = {"pan": "-60", "tilt": "0", "zoom": "0"}  # 向左转动
        in_parameter = ConfRead.get_req_body_from_template("../../resources/conf/video/PTZControl.xml", parameter)
        print(in_parameter.decode('utf-8'))
        cmsDemo.CMS_ISAPIPassThrough("PUT /ISAPI/PTZCtrl/channels/1/continuous", in_parameter, "")
        time.sleep(5)
        parameter = {"pan": "0", "tilt": "0", "zoom": "0"}  # 停止转动
        in_parameter = ConfRead.get_req_body_from_template("../../resources/conf/video/PTZControl.xml", parameter)
        print(in_parameter.decode('utf-8'))
        cmsDemo.CMS_ISAPIPassThrough("PUT /ISAPI/PTZCtrl/channels/1/continuous", in_parameter, "")
    elif command == "f3008":
        cmsDemo.FindRecored()


    else:
        print("未定义的指令类型!" + command)
