# -*- coding: utf-8 -*-
# @Time : 2023/9/13 9:38
# @Author : qiu jiahang
import time

from src.Common.ConfRead import *
from src.Common import glo
from src.DemoApp import SDKFunctionDemo
from src.SdkService.AlarmService.AlarmDemo import amsClass
from src.SdkService.CmsService.CmsDemo import cmsClass
from src.SdkService.SsService.SsDemo import ssClass
from src.SdkService.StreamService.StreamDemo import smsClass
from src.SdkService.StreamService.VoiceDemo import voiceClass

if __name__ == '__main__':
    loadJsonConfToGlobal(getConfJson())
    # 设置全局的userid
    glo.set_value('iUserID', -1)

    # 报警服务模块
    ams = amsClass()
    ams.startAmsListen()

    # 存储服务模块
    ss = ssClass()
    ss.startSsListen()

    # 注册服务模块
    cms = cmsClass()
    cms.startCmsListen()

    sms = smsClass()

    voice = voiceClass()

    while glo.get_value('iUserID') < 0:
        print('等待设备注册上线！ iUserID：', glo.get_value('iUserID'))
        time.sleep(1)

    # 存储客户端，上传图片到存储服务生成URL
    # ss.ssCreateClient('E:\sdktest\pic\\01.jpg')
    # ss.ssUploadPic()
    # ss.ssDestroyClient()

    # ISUP透传接口
    # cms.CMS_ISAPIPassThrough('/ISAPI/System/PictureServer?format=json', 'GET', '', '')

    # 门禁下发人员接口，作为POST透传接口示例说明
    # json_str = '{"UserInfo":{"employeeNo":"test003","name":"测试人员","userType":"normal","Valid":{"enable":true,"beginTime":"2023-01-01T00:00:00",' \
    #            '"endTime":"2030-12-31T23:59:59",' \
    #            '"timeType":"local"},"belongGroup":"1","doorRight":"1","RightPlan":[{"doorNo":1,"planTemplateNo":"1"}]}}'
    # str_bytes = bytes(json_str, encoding="utf-8")
    # cms.CMS_ISAPIPassThrough('/ISAPI/AccessControl/UserInfo/Record?format=json', 'POST', str_bytes, '')

    exit_program = False
    while not exit_program:
        print("请输入您想要执行的demo实例! （退出请输入yes）")
        command = input().strip().lower()
        try:
            if command == "yes":
                exit_program = True
                break

            elif command[0] == 'f':
                SDKFunctionDemo.dispatch(command, glo.get_value("iUserID"), cms, ams, ss, sms, voice)
                print("\n[Module]通用的sdk服务实例代码")

            elif command[0] == '1':
                print("\n[Module]门禁系统相关的demo示例代码")

            elif command[0] == '2':
                print("\n[Module]出入口系统相关的demo示例代码")

            elif command[0] == '3':
                print("\n[Module]超脑系统相关的demo示例代码")
            else:
                print("\n未知的指令操作!请重新输入!\n")
        except IndexError:
            print("command is empty or not initialized")

    # while True:
    #     cmd = input("是否退出监听（Y/N）：")
    #     if cmd == 'Y':
    #         break
    sms.stopSmsListen()
    cms.stopCmsListen()
    ams.stopAmsListen()
    ss.stopSsListen()
