package DemoApp;

import Common.CommonMethod;
import SdkService.AlarmService.AlarmDemo;
import SdkService.CmsService.CmsDemo;
import SdkService.CmsService.HCISUPCMS;
import SdkService.SsService.SsDemo;
import SdkService.StreamService.StreamDemo;
import SdkService.StreamService.VoiceDemo;
import UIModule.PlaybackVideoUI;
import UIModule.PreviewVideoUI;

import java.io.UnsupportedEncodingException;

/**
 * @author zhengxiaohui
 * @date 2023/8/19 14:44
 * @desc Isup SDK 通用框架层的示例代码模块
 */
public class SdkFunctionDemo {

    public static void dispatch(String command, int lLoginID,
                                CmsDemo cmsDemo, AlarmDemo alarmDemo, SsDemo ssDemo, StreamDemo streamDemo, VoiceDemo voiceDemo) throws UnsupportedEncodingException, InterruptedException {
        switch (command) {
            /**
             * 约定： f0001 ~ f0999 预留给 [cms] 模块的代码示例
             */
            case "f0001": {
                System.out.println("\n[Function]CMS控制信令透传(需要设备在线, 获取NVR设备工作状态、远程升级等)");
                cmsDemo.CMS_XMLRemoteControl();
                break;
            }
            case "f0002": {
                System.out.println("\n[Function]ISUP透传接口(云存储配置))");
                cmsDemo.CMS_ISAPIPassThrough();
                break;
            }
            /**
             * 约定： f1000 ~ f1999 预留给 [ams] 模块的代码示例
             */
            /**
             * 约定： f2000 ~ f2999 预留给 [ss] 模块的代码示例
             */
            case "f2000": {
                System.out.println("\n[Function]上传图片至存储服务器(返回图片URL可以用于人脸下发)");
                //上传图片至存储服务器(返回图片URL可以用于人脸下发)
                String picName = CommonMethod.getConsoleInput("请输入想要上传的文件名称，且文件名带后缀(图片文件请提前放置在resources/pics 路径下)：" +
                        "\n如: 'FDLib.jpg' 具体请以您的实际图片名称为准!");

                String fileAbsPath = CommonMethod.getResFileAbsPath("pics/" + picName);
                ssDemo.ssCreateClient(fileAbsPath);
                ssDemo.ssUploadPic();
                ssDemo.ssDestroyClient();
                break;
            }
            /**
             * 约定： f3000 ~ f3999 预留给 [sms] 模块的代码示例
             */
            case "f3001": {
                /**
                 * 实时预览模块(需要设备在线, 需要实现前面初始化流媒体服务(StreamDemo.eStream_Init)的代码)
                 */
                System.out.println("\n[Function]取流预览模块(有预览窗口)");
                PreviewVideoUI.jRealWinInit();

                streamDemo.startRealPlayListen_Win();
                // FIXME 注意这里的IChannel，不同设备类型可能不太一样
                streamDemo.RealPlay(1);

                // 这里只预览20s, 方便demo示例代码的效果演示
                Thread.sleep(20 * 1000);

                streamDemo.StopRealPlay();

                //关闭预览窗口
                PreviewVideoUI.jRealWinDestroy();
                break;
            }
            case "f3002": {
                /**
                 * 实时预览模块(需要设备在线, 需要实现前面初始化流媒体服务(StreamDemo.eStream_Init)的代码)
                 */
                System.out.println("\n[Function]取流预览模块(保存到本地文件)");
                streamDemo.startRealPlayListen_File();
                // FIXME 注意这里的IChannel，不同设备类型可能不太一样
                streamDemo.RealPlay(1);

                // 这里只预览20s, 方便demo示例代码的效果演示
                Thread.sleep(20 * 1000);

                streamDemo.StopRealPlay();
                break;
            }
            case "f3003": {
                /**
                 * 按时间回放模块(需要设备在线, 需要实现前面初始化流媒体服务(StreamDemo.eStream_Init)的代码)
                 */
                System.out.println("\n[Function]按时间回放模块(有回放窗口)");
                PlaybackVideoUI.jRealWinInit();
                // 回放监听函数调用
                streamDemo.startPlayBackListen_WIN();
                // FIXME 注意这里的IChannel，不同设备类型可能不太一样
                // 开启回放预览
                streamDemo.PlayBackByTime(1);

                // 这里根据回调中收到的设备回放结束信令来判断设备推流结束，然后cms调用停止回放
                // 如果需要再回放过程中调用停止回放，则修改这里的while循环逻辑即可。
                while(!streamDemo.stopPlayBackFlag){

                    try {
                        Thread.sleep(1000); // 每秒检查一次
                    } catch (InterruptedException e) {
                        // 如果线程被中断，通常需要清除中断状态
                        Thread.currentThread().interrupt();
                        System.out.println("Playback was interrupted");
                        break; // 当前线程被中断时退出循环
                    }
                }

                //停止回放预览
                streamDemo.stopPlayBackByTime();

                //关闭预览窗口
                PlaybackVideoUI.jRealWinDestroy();
                break;
            }
            case "f3004": {
                /**
                 * 按时间回放模块(需要设备在线, 需要实现前面初始化流媒体服务(StreamDemo.eStream_Init)的代码)
                 */
                System.out.println("\n[Function]按时间回放模块(保存到本地文件)");
                // 回放监听函数调用
                streamDemo.startPlayBackListen_FILE();
                // FIXME 注意这里的IChannel，不同设备类型可能不太一样
                // 开启回放预览
                streamDemo.PlayBackByTime(1);

                // 这里根据回调中收到的设备回放结束信令来判断设备推流结束，然后cms调用停止回放
                // 如果需要再回放过程中调用停止回放，则修改这里的while循环逻辑即可。
                while(!streamDemo.stopPlayBackFlag){

                    try {
                        Thread.sleep(1000); // 每秒检查一次
                    } catch (InterruptedException e) {
                        // 如果线程被中断，通常需要清除中断状态
                        Thread.currentThread().interrupt();
                        System.out.println("Playback was interrupted");
                        break; // 当前线程被中断时退出循环
                    }
                }

                //停止回放预览
                streamDemo.stopPlayBackByTime();

                break;
            }
            case "f3005": {

                // 本demo音频编解码处理复用了设备网络sdk的音频编解码接口，所以需要导入设备网络sdk动态库到【/lib/netsdk】路径下进行加载，
                // 如果应用层自行编解码设备，则注释掉相关编解码代码即可。
                voiceDemo.voice_Init();
                voiceDemo.startVoiceServeListen();

                //获取设备通道对讲信息，包括编码格式，起始对讲通道号等，跟nvr对讲前先获取
                HCISUPCMS.NET_EHOME_DEVICE_INFO res = cmsDemo.getDevInfo();

                voiceDemo.makeVoice(20); //采集本地20s音频保存为pcm

                /**
                 * ISUP5.0语音转发模块(需要设备在线, 需要确定设备是否支持此功能, 需要实现前面初始化语音流媒体服务的代码)
                 */
                int dwVoiceChan = res.byStartDTalkChan + 3;
                byte dwAudioEncType = (byte) res.dwAudioEncType;

                voiceDemo.StartVoiceTrans(dwVoiceChan, dwAudioEncType);
                voiceDemo.StopVoiceTrans();
                break;
            }
            case "f3006":{

                //ISUP5.0抓图,ISUP抓图需要先开启存储服务，设置存储服务类型为云存储，下发云存储参数给到设备
                //抓图成功后，设备返回云存储URL，
                cmsDemo.GetPicByCloud(IsupTest.lLoginID);

            }
            case "f3007":{

                //云台转动
                cmsDemo.PTZCtrl(IsupTest.lLoginID);

            }
            case "f3008":{
                //录像查询
                cmsDemo.FindRecored(IsupTest.lLoginID);
            }
            case "f3009":{
                //获取设备信息
                cmsDemo.GetDevInfo(IsupTest.lLoginID);
            }

            case "f3010":{
                //手动校时
                cmsDemo.SetDevTimeCfg(IsupTest.lLoginID);
            }

            default: {
                System.out.println("未定义的指令类型!" + command);
            }
        }
    }
}
