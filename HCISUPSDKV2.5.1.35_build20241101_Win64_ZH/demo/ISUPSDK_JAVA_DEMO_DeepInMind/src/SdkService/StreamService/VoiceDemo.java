package SdkService.StreamService;

import Common.CommonMethod;
import SdkService.CmsService.CmsDemo;
import SdkService.CmsService.HCISUPCMS;
import Common.PropertiesUtil;
import Common.osSelect;
import DemoApp.IsupTest;
import com.sun.jna.Native;
import com.sun.jna.Pointer;

import javax.sound.sampled.AudioFormat;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.DataLine;
import javax.sound.sampled.TargetDataLine;
import java.io.*;
import java.nio.ByteBuffer;

public class VoiceDemo {

    public static HCISUPStream hCEhomeVoice = null;
    static HCNetSDK hCNetSDK = null;
    public static int VoiceHandle = -1;   //语音监听句柄
    public static int lVoiceLinkHandle = -1; //语音连接句柄
    public static int VoicelServHandle = -1; //语音流媒体监听句柄
    static File fileEncode = null;
    public static Pointer pDecHandle = null; // 音频解码句柄

    static FileOutputStream outputStreamG711 = null;

    public static VOICETALK_NEWLINK_CB VOICETALK_newlink_cb;//语音转发连接回调函数实现
    public static VOICE_DATA_CB VOICE_data_cb; //语音数据回调函数

    static int voiceTalkSessionId = -1;


    static String configPath = "./config.properties";
    PropertiesUtil propertiesUtil;

    {
        try {
            propertiesUtil = new PropertiesUtil(configPath);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


    public void voice_Init() {
        if (hCEhomeVoice == null) {
            if (!CreateSDKInstance()) {
                System.out.println("Load Stream SDK fail");
                return;
            }

        }

        if (osSelect.isWindows()) {
            //设置libcrypto.so所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCrypto = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCrypto = System.getProperty("user.dir") + "\\lib\\libeay32.dll"; //Linux版本是libcrypto.so库文件的路径
            System.arraycopy(strPathCrypto.getBytes(), 0, ptrByteArrayCrypto.byValue, 0, strPathCrypto.length());
            ptrByteArrayCrypto.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetSDKInitCfg(0, ptrByteArrayCrypto.getPointer())) {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 0 failed, error:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            } else {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 0 succeed");
            }
            //设置libssl.so所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArraySsl = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathSsl = System.getProperty("user.dir") + "\\lib\\ssleay32.dll";    //Linux版本是libssl.so库文件的路径
            System.arraycopy(strPathSsl.getBytes(), 0, ptrByteArraySsl.byValue, 0, strPathSsl.length());
            ptrByteArraySsl.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetSDKInitCfg(1, ptrByteArraySsl.getPointer())) {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 1 failed, error:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            } else {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 1 succeed");
            }
            //语音流媒体初始化
            hCEhomeVoice.NET_ESTREAM_Init();
            //设置HCAapSDKCom组件库文件夹所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCom = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCom = System.getProperty("user.dir") + "\\lib\\HCAapSDKCom";      //只支持绝对路径，建议使用英文路径
            System.arraycopy(strPathCom.getBytes(), 0, ptrByteArrayCom.byValue, 0, strPathCom.length());
            ptrByteArrayCom.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetSDKLocalCfg(5, ptrByteArrayCom.getPointer())) {
                System.out.println("NET_ESTREAM_SetSDKLocalCfg 5 failed, error:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            } else {
                System.out.println("NET_ESTREAM_SetSDKLocalCfg 5 succeed");
            }
            hCEhomeVoice.NET_ESTREAM_SetLogToFile(3, "./EHomeSDKLog", false);

        } else if (osSelect.isLinux()) {
            //设置libcrypto.so所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCrypto = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCrypto = System.getProperty("user.dir") + "/lib/libcrypto.so"; //Linux版本是libcrypto.so库文件的路径
            System.arraycopy(strPathCrypto.getBytes(), 0, ptrByteArrayCrypto.byValue, 0, strPathCrypto.length());
            ptrByteArrayCrypto.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetSDKInitCfg(0, ptrByteArrayCrypto.getPointer())) {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 0 failed, error:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            } else {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 0 succeed");
            }
            //设置libssl.so所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArraySsl = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathSsl = System.getProperty("user.dir") + "/lib/libssl.so";    //Linux版本是libssl.so库文件的路径
            System.arraycopy(strPathSsl.getBytes(), 0, ptrByteArraySsl.byValue, 0, strPathSsl.length());
            ptrByteArraySsl.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetSDKInitCfg(1, ptrByteArraySsl.getPointer())) {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 1 failed, error:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            } else {
                System.out.println("NET_ESTREAM_SetSDKInitCfg 1 succeed");
            }
            hCEhomeVoice.NET_ESTREAM_Init();

            //设置HCAapSDKCom组件库文件夹所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCom = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCom = System.getProperty("user.dir") + "/lib/HCAapSDKCom/";      //只支持绝对路径，建议使用英文路径
            System.arraycopy(strPathCom.getBytes(), 0, ptrByteArrayCom.byValue, 0, strPathCom.length());
            ptrByteArrayCom.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetSDKLocalCfg(5, ptrByteArrayCom.getPointer())) {
                System.out.println("NET_ESTREAM_SetSDKLocalCfg 5 failed, error:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            } else {
                System.out.println("NET_ESTREAM_SetSDKLocalCfg 5 succeed");
            }

            hCEhomeVoice.NET_ESTREAM_SetLogToFile(3, "..\\EHomeSDKLog", false);
        }

        /**
         * 这里加载网络sdk，用来处理语音对讲过程中的音频编解码。
         * 如果上层应用自行编解码音频，则无须调用网络sdk库
         */
        if (hCNetSDK == null) {
            if (!createHCNetSDKInstance()) {
                System.out.println("Load Stream SDK fail");
                return;
            }
        }
    }

    private static boolean createHCNetSDKInstance() {
        if (hCNetSDK == null) {
            synchronized (HCISUPStream.class) {
                String strDllPath = "";
                try {
                    if (osSelect.isWindows()) {
                        //win系统加载库路径(路径不要带中文)
                        strDllPath = System.getProperty("user.dir") + "\\lib\\netsdk\\HCNetSDK.dll";
                    } else if (osSelect.isLinux()) {
                        //Linux系统加载库路径(路径不要带中文)
                        strDllPath = System.getProperty("user.dir") + "/lib/libhcnetsdk.so";
                    }
                    hCNetSDK = (HCNetSDK) Native.loadLibrary(strDllPath, HCNetSDK.class);
                } catch (Exception ex) {
                    System.out.println("loadLibrary:" + strDllPath + " Error: " + ex.getMessage());
                    return false;
                }
            }

            hCNetSDK.NET_DVR_Init();
        }
        return true;
    }

    /**
     * 开启语音流媒体服务监听
     */
    public void startVoiceServeListen() {
        if (VOICETALK_newlink_cb == null) {
            VOICETALK_newlink_cb = new VOICETALK_NEWLINK_CB();
        }
        HCISUPStream.NET_EHOME_LISTEN_VOICETALK_CFG net_ehome_listen_voicetalk_cfg = new HCISUPStream.NET_EHOME_LISTEN_VOICETALK_CFG();
        net_ehome_listen_voicetalk_cfg.struIPAdress.szIP = propertiesUtil.readValue("VoiceSmsServerListenIP").getBytes();
        net_ehome_listen_voicetalk_cfg.struIPAdress.wPort = Short.parseShort(propertiesUtil.readValue("VoiceSmsServerListenPort"));
        net_ehome_listen_voicetalk_cfg.fnNewLinkCB = VOICETALK_newlink_cb;
        net_ehome_listen_voicetalk_cfg.byLinkMode = 0;
        net_ehome_listen_voicetalk_cfg.write();
        VoicelServHandle = hCEhomeVoice.NET_ESTREAM_StartListenVoiceTalk(net_ehome_listen_voicetalk_cfg);
        if (VoicelServHandle == -1) {
            System.out.println("NET_ESTREAM_StartListenPreview failed, error code:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
            hCEhomeVoice.NET_ESTREAM_Fini();
            return;
        } else {
            String VoiceStreamListenInfo = new String(net_ehome_listen_voicetalk_cfg.struIPAdress.szIP).trim() + "_" + net_ehome_listen_voicetalk_cfg.struIPAdress.wPort;
            System.out.println("语音流媒体服务：" + VoiceStreamListenInfo + ",NET_ESTREAM_StartListenVoiceTalk succeed, " + VOICETALK_newlink_cb);
        }
    }

    /**
     * 开启语音转发
     * 说明：byEncodingType为获取设备通道对讲信息中返回的编码格式，对应结构体NET_EHOME_DEVICE_INFO.dwAudioEncType;  // 语音对讲的音频格式：0-G.722，1-G.711U，2-G.711A，3-G.726，4-AAC，5-MP2L2。
     * NET_EHOME_DEVICE_INFO.dwAudioEncType为1表示g711u，对应NET_EHOME_TALK_ENCODING_TYPE中为2表示g711u
     *
     * typedef enum tagNET_EHOME_TALK_ENCODING_TYPE{
     * ENUM_ENCODING_START = 0,
     * ENUM_ENCODING_G722_1, = 1
     * ENUM_ENCODING_G711_MU, = 2
     * ENUM_ENCODING_G711_A, = 3
     * ENUM_ENCODING_G723, = 4
     * ENUM_ENCODING_MP1L2, = 5
     * ENUM_ENCODING_MP2L2, = 6
     * ENUM_ENCODING_G726, = 7
     * ENUM_ENCODING_AAC, = 8
     * ENUM_ENCODING_RAW = 100
     * }NET_EHOME_TALK_ENCODING_TYPE;
     */
    public void StartVoiceTrans(int dwVoiceChan, byte byEncodingType) throws InterruptedException {
        byEncodingType = (byte)(byEncodingType + 1); //这里是将NET_EHOME_DEVICE_INFO.dwAudioEncType跟byEncodingType值对齐，区别见结构体NET_EHOME_DEVICE_INFO和NET_EHOME_TALK_ENCODING_TYPE的定义
        // 语音对讲开启请求的输入参数
        HCISUPCMS.NET_EHOME_VOICE_TALK_IN net_ehome_voice_talk_in = new HCISUPCMS.NET_EHOME_VOICE_TALK_IN();
        net_ehome_voice_talk_in.struStreamSever.szIP = propertiesUtil.readValue("VoiceSmsServerIP").getBytes();
        net_ehome_voice_talk_in.struStreamSever.wPort = Short.parseShort(propertiesUtil.readValue("VoiceSmsServerPort"));
        net_ehome_voice_talk_in.dwVoiceChan = dwVoiceChan; //语音通道号,NVR设备起始通道号为3，dwVoiceChan传6对应nvr的通道4
        net_ehome_voice_talk_in.byEncodingType[0] = byEncodingType;  //跟NVR通道对讲必须带上此参数，ENUM_ENCODING_G722_1 = 1；ENUM_ENCODING_G711_MU = 2 ；ENUM_ENCODING_G711_A = 3
//        net_ehome_voice_talk_in.byAudioSamplingRate = 5;
        net_ehome_voice_talk_in.write();
        // 语音对讲开启请求的输出参数
        HCISUPCMS.NET_EHOME_VOICE_TALK_OUT net_ehome_voice_talk_out = new HCISUPCMS.NET_EHOME_VOICE_TALK_OUT();
        // 将语音对讲开启请求从CMS 发送给设备发送SMS 的地址和端口号给设备，设备自动为CMS 分配一个会话ID。
        if (!CmsDemo.hCEhomeCMS.NET_ECMS_StartVoiceWithStmServer(IsupTest.lLoginID, net_ehome_voice_talk_in, net_ehome_voice_talk_out)) {
            System.out.println("NET_ECMS_StartVoiceWithStmServer failed, error code:" + CmsDemo.hCEhomeCMS.NET_ECMS_GetLastError());
            return;
        } else {
            net_ehome_voice_talk_out.read();
            System.out.println("NET_ECMS_StartVoiceWithStmServer suss sessionID=" + net_ehome_voice_talk_out.lSessionID);
        }

        // 语音传输请求的输入参数
        HCISUPCMS.NET_EHOME_PUSHVOICE_IN struPushVoiceIn = new HCISUPCMS.NET_EHOME_PUSHVOICE_IN();
        struPushVoiceIn.dwSize = struPushVoiceIn.size();
        struPushVoiceIn.lSessionID = net_ehome_voice_talk_out.lSessionID;
        voiceTalkSessionId = net_ehome_voice_talk_out.lSessionID;
        struPushVoiceIn.write();
        // 语音传输请求的输出参数
        HCISUPCMS.NET_EHOME_PUSHVOICE_OUT struPushVoiceOut = new HCISUPCMS.NET_EHOME_PUSHVOICE_OUT();
        struPushVoiceOut.dwSize = struPushVoiceOut.size();
        struPushVoiceOut.write();
        // 将语音传输请求从CMS 发送给设备。设备自动连接SMS 并开始发送音频数据给SMS
        if (!CmsDemo.hCEhomeCMS.NET_ECMS_StartPushVoiceStream(IsupTest.lLoginID, struPushVoiceIn, struPushVoiceOut)) {
            System.out.println("NET_ECMS_StartPushVoiceStream failed, error code:" + CmsDemo.hCEhomeCMS.NET_ECMS_GetLastError());
            return;
        }
        System.out.println("NET_ECMS_StartPushVoiceStream success!\n");

        //发送音频数据
        FileInputStream voiceInputStream = null;
        int dataLength = 0;
        try {
            //创建从文件读取数据的FileInputStream流
//            voiceInputStream = new FileInputStream(CommonMethod.getResFileAbsPath("audioFile/twoWayTalk.g7"));
            voiceInputStream = new FileInputStream(CommonMethod.getResFileAbsPath("audioFile/audioCollected.pcm"));
            //返回文件的总字节数
            dataLength = voiceInputStream.available();
        } catch (IOException e) {
            e.printStackTrace();
        }

        fileEncode = new File(System.getProperty("user.dir") + "\\resources\\audioFile\\EncodedAudioData.g7");  //保存音频编码数据

        if (!fileEncode.exists()) {
            try {
                fileEncode.createNewFile();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        }

        try {
            outputStreamG711 = new FileOutputStream(fileEncode);
        } catch (FileNotFoundException e3) {
            // TODO Auto-generated catch block
            e3.printStackTrace();
        }

        if (dataLength < 0) {
            System.out.println("input file dataSize < 0");
            throw new RuntimeException("输入的文件");
//            return false;
        }

        HCNetSDK.BYTE_ARRAY ptrVoiceByte = new HCNetSDK.BYTE_ARRAY(dataLength);
        try {
            voiceInputStream.read(ptrVoiceByte.byValue);
        } catch (IOException e2) {
            e2.printStackTrace();
        }
        ptrVoiceByte.write();

        int iEncodeSize = 0;
        HCNetSDK.NET_DVR_AUDIOENC_INFO enc_info = new HCNetSDK.NET_DVR_AUDIOENC_INFO();
        enc_info.write();

        boolean initSuc = hCNetSDK.NET_DVR_Init();
        if (!initSuc) {
            System.out.println("初始化失败");
            return;
        }
        hCNetSDK.NET_DVR_SetLogToFile(3, "./sdklog", false);
        Pointer encoder = hCNetSDK.NET_DVR_InitG711Encoder(enc_info);
        while ((dataLength - iEncodeSize) > 640) {
            HCNetSDK.BYTE_ARRAY ptrPcmData = new HCNetSDK.BYTE_ARRAY(640);
            System.arraycopy(ptrVoiceByte.byValue, iEncodeSize, ptrPcmData.byValue, 0, 640);
            ptrPcmData.write();

            HCNetSDK.BYTE_ARRAY ptrG711Data = new HCNetSDK.BYTE_ARRAY(320);
            ptrG711Data.write();

            HCNetSDK.NET_DVR_AUDIOENC_PROCESS_PARAM struEncParam = new HCNetSDK.NET_DVR_AUDIOENC_PROCESS_PARAM();
            struEncParam.in_buf = ptrPcmData.getPointer();
            struEncParam.out_buf = ptrG711Data.getPointer();
            struEncParam.out_frame_size = 320;
            struEncParam.g711_type = 0;//G711编码类型：0- U law，1- A law
            struEncParam.write();

            if (!hCNetSDK.NET_DVR_EncodeG711Frame(encoder, struEncParam)) {
                System.out.println("NET_DVR_EncodeG711Frame failed, error code:" + hCNetSDK.NET_DVR_GetLastError());
                hCNetSDK.NET_DVR_ReleaseG711Encoder(encoder);
            }
            struEncParam.read();
            ptrG711Data.read();

            long offsetG711 = 0;
            ByteBuffer buffersG711 = struEncParam.out_buf.getByteBuffer(offsetG711, struEncParam.out_frame_size);
            byte[] bytesG711 = new byte[struEncParam.out_frame_size];
            buffersG711.rewind();
            buffersG711.get(bytesG711);
            try {
                outputStreamG711.write(bytesG711);
            } catch (IOException e1) {
                // TODO Auto-generated catch block
                e1.printStackTrace();
            }
            iEncodeSize += 640;
            System.out.println("编码字节数：" + iEncodeSize);

            for (int i = 0; i < struEncParam.out_frame_size / 160; i++) {
                HCISUPStream.BYTE_ARRAY ptrG711Send = new HCISUPStream.BYTE_ARRAY(160);
                System.arraycopy(ptrG711Data.byValue, i * 160, ptrG711Send.byValue, 0, 160);
                ptrG711Send.write();
                HCISUPStream.NET_EHOME_VOICETALK_DATA struVoicTalkData = new HCISUPStream.NET_EHOME_VOICETALK_DATA();
                struVoicTalkData.pData = ptrG711Send.getPointer();
                struVoicTalkData.dwDataLen = 160;
                struVoicTalkData.write();
                // 将音频数据发送给设备
                if (hCEhomeVoice.NET_ESTREAM_SendVoiceTalkData(lVoiceLinkHandle, struVoicTalkData) <= -1) {
                    System.out.println("NET_ESTREAM_SendVoiceTalkData failed, error code:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
                }

                //需要实时速率发送数据
                try {
                    Thread.sleep(20);
                } catch (InterruptedException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
            }
        }

    }

    private FileOutputStream getFileFromAbsPath(String fileAbsPath) {
        File fileEncode = new File(fileAbsPath);  //保存音频编码数据

        if (!fileEncode.exists()) {
            try {
                fileEncode.createNewFile();
                return new FileOutputStream(fileEncode);
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        }
        return null;
    }


    /**
     * 停止语音对讲
     */
    public void StopVoiceTrans() {
        //SMS 停止语音对讲
        if (lVoiceLinkHandle >= 0) {
            if (!hCEhomeVoice.NET_ESTREAM_StopVoiceTalk(lVoiceLinkHandle)) {
                System.out.println("NET_ESTREAM_StopVoiceTalk failed, error code:" + hCEhomeVoice.NET_ESTREAM_GetLastError());
                return;
            }
        }
        //释放语音对讲请求资源
        if (!CmsDemo.hCEhomeCMS.NET_ECMS_StopVoiceTalkWithStmServer(IsupTest.lLoginID, voiceTalkSessionId)) {
            System.out.println("NET_ECMS_StopVoiceTalkWithStmServer failed, error code:" + CmsDemo.hCEhomeCMS.NET_ECMS_GetLastError());
            return;
        }
        // 释放SDK资源
        hCNetSDK.NET_DVR_Cleanup();
    }


    /**
     * 语音对讲请求的回调函数
     */
    public class VOICETALK_NEWLINK_CB implements HCISUPStream.VOICETALK_NEWLINK_CB {
        public boolean invoke(int lHandle, HCISUPStream.NET_EHOME_VOICETALK_NEWLINK_CB_INFO pNewLinkCBInfo, Pointer pUserData) {
            System.out.println("fVOICE_NEWLINK_CB callback");
            lVoiceLinkHandle = lHandle;
            HCISUPStream.NET_EHOME_VOICETALK_DATA_CB_PARAM net_ehome_voicetalk_data_cb_param = new HCISUPStream.NET_EHOME_VOICETALK_DATA_CB_PARAM();
            if (VOICE_data_cb == null) {
                VOICE_data_cb = new VOICE_DATA_CB();
            }
            net_ehome_voicetalk_data_cb_param.fnVoiceTalkDataCB = VOICE_data_cb;
            net_ehome_voicetalk_data_cb_param.write();
            if (!hCEhomeVoice.NET_ESTREAM_SetVoiceTalkDataCB(lHandle, net_ehome_voicetalk_data_cb_param)) {
                System.out.println("NET_ESTREAM_SetVoiceTalkDataCB()错误代码号：" + hCEhomeVoice.NET_ESTREAM_GetLastError());
                return false;
            }
            return true;
        }

    }

    /**
     * 语音对讲数据的回调函数
     */
    public class VOICE_DATA_CB implements HCISUPStream.VOICETALK_DATA_CB {
        private File fileG7 = null;
        private File filePcm = null;

        private FileOutputStream outputStreamG7 = null;
        private FileOutputStream outputStreamPcm = null;

        public VOICE_DATA_CB() {
            // 保存回调函数的音频数据
            fileG7 = new File(CommonMethod.getResFileAbsPath("audioFile/DeviceToPlat.g7"));
            try {
                if (!fileG7.exists()) {
                    fileG7.createNewFile();
                }
                outputStreamG7 = new FileOutputStream(fileG7);
            } catch (IOException e) {
                // TODO 自行处理异常
                e.printStackTrace();
            }

            // 保存回调函数的音频数据（解码后的pcm数据，播放和确认时长）
            filePcm = new File(CommonMethod.getResFileAbsPath("audioFile/DeviceToPlat.pcm"));
            try {
                if (!filePcm.exists()) {
                    filePcm.createNewFile();
                }
                outputStreamPcm = new FileOutputStream(filePcm);
            } catch (IOException e) {
                // TODO 自行处理异常
                e.printStackTrace();
            }
        }

        public boolean invoke(int lHandle, HCISUPStream.NET_EHOME_VOICETALK_DATA_CB_INFO pNewLinkCBInfo, Pointer pUserData) {
            //回调函数保存设备返回的语音数据
            //将设备发送过来的语音数据写入文件
            System.out.println("设备音频发送.....");
            VoiceHandle = lHandle;
            long offset = 0;
            ByteBuffer buffers = pNewLinkCBInfo.pData.getByteBuffer(offset, pNewLinkCBInfo.dwDataLen);
            byte[] bytes = new byte[pNewLinkCBInfo.dwDataLen];
            buffers.rewind();
            buffers.get(bytes);
            try {
                outputStreamG7.write(bytes);
            } catch (IOException e) {
                e.printStackTrace();
            }

            //解码为pcm（播放和确认时长）
            pDecHandle = hCNetSDK.NET_DVR_InitG711Decoder();
            // 回调中一次可能返回多帧数据，需要逐帧解码为pcm
            for (int i = 0; i < bytes.length; i += 160) {
                HCNetSDK.BYTE_ARRAY byteArray = new HCNetSDK.BYTE_ARRAY(160);
                // 检查是否到达源数组末尾，避免数组越界
                int actualReadSize = Math.min(160, bytes.length - i);
                System.arraycopy(bytes, i, byteArray.byValue, 0, actualReadSize);
                byteArray.write();
                HCNetSDK.NET_DVR_AUDIODEC_PROCESS_PARAM struAudioParam = new HCNetSDK.NET_DVR_AUDIODEC_PROCESS_PARAM();
                struAudioParam.in_buf = byteArray.getPointer();
                struAudioParam.in_data_size = byteArray.size();

                HCNetSDK.BYTE_ARRAY ptrVoiceData = new HCNetSDK.BYTE_ARRAY(320);
                ptrVoiceData.write();
                struAudioParam.out_buf = ptrVoiceData.getPointer();
                struAudioParam.out_frame_size = 320;
                struAudioParam.g711_type = 0; //G711编码类型：0- U law，1- A law
                struAudioParam.write();
                if (!hCNetSDK.NET_DVR_DecodeG711Frame(pDecHandle, struAudioParam)) {
                    System.out.println("NET_DVR_DecodeG711Frame failed, error code:" + hCNetSDK.NET_DVR_GetLastError());
                }
                struAudioParam.read();
                //将解码之后PCM音频数据写入文件
                long offsetPcm = 0;
                ByteBuffer buffersPcm = struAudioParam.out_buf.getByteBuffer(offsetPcm, struAudioParam.out_frame_size);
                byte[] bytesPcm = new byte[struAudioParam.out_frame_size];
                buffersPcm.rewind();
                buffersPcm.get(bytesPcm);
                try {
                    outputStreamPcm.write(bytesPcm);  //这里实现的是将设备发送的pcm音频数据写入文件，（前面的代码实现的就是将g711解码成pcm音频）
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            return true;
        }

    }


    /**
     * 动态库加载
     *
     * @return
     */
    private static boolean CreateSDKInstance() {
        if (hCEhomeVoice == null) {
            synchronized (HCISUPStream.class) {
                String strDllPath = "";
                try {
                    if (osSelect.isWindows())
                        //win系统加载库路径(路径不要带中文)
                        strDllPath = System.getProperty("user.dir") + "\\lib\\HCISUPStream.dll";

                    else if (osSelect.isLinux())
                        //Linux系统加载库路径(路径不要带中文)
                        strDllPath = System.getProperty("user.dir") + "/lib/libHCISUPStream.so";
                    hCEhomeVoice = (HCISUPStream) Native.loadLibrary(strDllPath, HCISUPStream.class);
                } catch (Exception ex) {
                    System.out.println("loadLibrary: " + strDllPath + " Error: " + ex.getMessage());
                    return false;
                }
            }
        }
        return true;

    }

    /**
     * 录制电脑麦克风声音文件
     *
     * @param duration ：采集时长
     */
    public static void makeVoice(int duration) {
        int sampleRate = 8000; // 采样率
        int bitsPerSample = 16; // 每样本比特数
        int channels = 1; // 双声道
        int frameSize = channels * bitsPerSample / 8; // 单个样本大小
        boolean signed = true; // 有符号
        boolean bigEndian = false; // 小端字节序

        AudioFormat format = new AudioFormat(
                sampleRate,
                bitsPerSample,
                channels,
                signed,
                bigEndian
        );

        DataLine.Info info = new DataLine.Info(TargetDataLine.class, format);

        if (!AudioSystem.isLineSupported(info)) {
            System.out.println("Line not supported");
            return;
        }

        TargetDataLine line = null;
        try {
            line = (TargetDataLine) AudioSystem.getLine(info);
            line.open(format);
            line.start();

            int bufferSize = (int) (format.getFrameRate() * format.getFrameSize()); // 计算缓冲区大小
            byte[] buffer = new byte[bufferSize];

            File file = new File(CommonMethod.getResFileAbsPath("audioFile/audioCollected.pcm")); // PCM文件路径
            int numBytesRead = 0;
            int totalBytesRead = 0;
            try (FileOutputStream out = new FileOutputStream(file)) {
                while (totalBytesRead < format.getFrameRate() * format.getFrameSize() * duration && (numBytesRead = line.read(buffer, 0, buffer.length)) >= 0) {
                    out.write(buffer, 0, numBytesRead);
                    totalBytesRead += numBytesRead;
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (line != null) {
                line.stop();
                line.close();
            }
        }
    }
}






















