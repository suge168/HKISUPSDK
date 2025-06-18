package SdkService.CmsService;

import Common.*;
import DemoApp.IsupTest;
import SdkService.AlarmService.AlarmDemo;
import com.sun.jna.Native;
import com.sun.jna.Pointer;
import com.sun.jna.ptr.IntByReference;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

public class CmsDemo {

    public static HCISUPCMS hCEhomeCMS = null;
    public static int CmsHandle = -1; //CMS监听句柄
    static FRegisterCallBack fRegisterCallBack;//注册回调函数实现
    HCISUPCMS.NET_EHOME_CMS_LISTEN_PARAM struCMSListenPara = new HCISUPCMS.NET_EHOME_CMS_LISTEN_PARAM();
    static String configPath = "./config.properties";
    PropertiesUtil propertiesUtil;

    {
        try {
            propertiesUtil = new PropertiesUtil(configPath);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * 根据不同操作系统选择不同的库文件和库路径
     *
     * @return
     */
    private static boolean CreateSDKInstance() {
        if (hCEhomeCMS == null) {
            synchronized (HCISUPCMS.class) {
                String strDllPath = "";
                try {
                    //System.setProperty("jna.debug_load", "true");
                    if (osSelect.isWindows())
                        //win系统加载库路径(路径不要带中文)
                        strDllPath = System.getProperty("user.dir") + "\\lib\\HCISUPCMS.dll";
                    else if (osSelect.isLinux())
                        //Linux系统加载库路径(路径不要带中文)
                        strDllPath = System.getProperty("user.dir") + "/lib/libHCISUPCMS.so";
                    hCEhomeCMS = (HCISUPCMS) Native.loadLibrary(strDllPath, HCISUPCMS.class);
                } catch (Exception ex) {
                    System.out.println("loadLibrary: " + strDllPath + " Error: " + ex.getMessage());
                    return false;
                }
            }
        }
        return true;
    }

    /**
     * cms服务初始化，开启监听
     *
     * @throws IOException
     */
    public void cMS_Init() throws IOException {

        if (hCEhomeCMS == null) {
            if (!CreateSDKInstance()) {
                System.out.println("Load CMS SDK fail");
                return;
            }
        }
        if (osSelect.isWindows()) {
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCrypto = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCrypto = System.getProperty("user.dir") + "\\lib\\libeay32.dll"; //Linux版本是libcrypto.so库文件的路径
            System.arraycopy(strPathCrypto.getBytes(), 0, ptrByteArrayCrypto.byValue, 0, strPathCrypto.length());
            ptrByteArrayCrypto.write();
            hCEhomeCMS.NET_ECMS_SetSDKInitCfg(0, ptrByteArrayCrypto.getPointer());

            //设置libssl.so所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArraySsl = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathSsl = System.getProperty("user.dir") + "\\lib\\ssleay32.dll";    //Linux版本是libssl.so库文件的路径
            System.arraycopy(strPathSsl.getBytes(), 0, ptrByteArraySsl.byValue, 0, strPathSsl.length());
            ptrByteArraySsl.write();
            hCEhomeCMS.NET_ECMS_SetSDKInitCfg(1, ptrByteArraySsl.getPointer());
            //注册服务初始化
            boolean binit = hCEhomeCMS.NET_ECMS_Init();
            //设置HCAapSDKCom组件库文件夹所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCom = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCom = System.getProperty("user.dir") + "\\lib\\HCAapSDKCom";        //只支持绝对路径，建议使用英文路径
            System.arraycopy(strPathCom.getBytes(), 0, ptrByteArrayCom.byValue, 0, strPathCom.length());
            ptrByteArrayCom.write();
            hCEhomeCMS.NET_ECMS_SetSDKLocalCfg(5, ptrByteArrayCom.getPointer());

        } else if (osSelect.isLinux()) {
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCrypto = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCrypto = System.getProperty("user.dir") + "/lib/libcrypto.so"; //Linux版本是libcrypto.so库文件的路径
            System.arraycopy(strPathCrypto.getBytes(), 0, ptrByteArrayCrypto.byValue, 0, strPathCrypto.length());
            ptrByteArrayCrypto.write();
            hCEhomeCMS.NET_ECMS_SetSDKInitCfg(0, ptrByteArrayCrypto.getPointer());

            //设置libssl.so所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArraySsl = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathSsl = System.getProperty("user.dir") + "/lib/libssl.so";    //Linux版本是libssl.so库文件的路径
            System.arraycopy(strPathSsl.getBytes(), 0, ptrByteArraySsl.byValue, 0, strPathSsl.length());
            ptrByteArraySsl.write();
            hCEhomeCMS.NET_ECMS_SetSDKInitCfg(1, ptrByteArraySsl.getPointer());
            //注册服务初始化
            boolean binit = hCEhomeCMS.NET_ECMS_Init();
            //设置HCAapSDKCom组件库文件夹所在路径
            HCISUPCMS.BYTE_ARRAY ptrByteArrayCom = new HCISUPCMS.BYTE_ARRAY(256);
            String strPathCom = System.getProperty("user.dir") + "/lib/HCAapSDKCom/";        //只支持绝对路径，建议使用英文路径
            System.arraycopy(strPathCom.getBytes(), 0, ptrByteArrayCom.byValue, 0, strPathCom.length());
            ptrByteArrayCom.write();
            hCEhomeCMS.NET_ECMS_SetSDKLocalCfg(5, ptrByteArrayCom.getPointer());

        }
        hCEhomeCMS.NET_ECMS_SetLogToFile(3, System.getProperty("user.dir") + "/EHomeSDKLog", false);
    }

    public void startCmsListen() {
        if (fRegisterCallBack == null) {
            fRegisterCallBack = new FRegisterCallBack();
        }
        System.arraycopy(propertiesUtil.readValue("CmsServerIP").getBytes(), 0, struCMSListenPara.struAddress.szIP, 0, propertiesUtil.readValue("CmsServerIP").length());
        struCMSListenPara.struAddress.wPort = Short.parseShort(propertiesUtil.readValue("CmsServerPort"));
        struCMSListenPara.fnCB = fRegisterCallBack;
        struCMSListenPara.write();
        //启动监听，接收设备注册信息
        CmsHandle = hCEhomeCMS.NET_ECMS_StartListen(struCMSListenPara);
        if (CmsHandle < 0) {
            System.out.println("NET_ECMS_StartListen failed, error code:" + hCEhomeCMS.NET_ECMS_GetLastError());
            hCEhomeCMS.NET_ECMS_Fini();
            return;
        }
        String CmsListenInfo = new String(struCMSListenPara.struAddress.szIP).trim() + "_" + struCMSListenPara.struAddress.wPort;
        System.out.println("注册服务器:" + CmsListenInfo + ",NET_ECMS_StartListen succeed!\n");

        //设置CMS异步回调

    }

    //注册回调函数
    public class FRegisterCallBack implements HCISUPCMS.DEVICE_REGISTER_CB {
        public boolean invoke(int lUserID, int dwDataType, Pointer pOutBuffer, int dwOutLen, Pointer pInBuffer, int dwInLen, Pointer pUser) {
            System.out.println("FRegisterCallBack, dwDataType:" + dwDataType + ", lUserID:" + lUserID);
            switch (dwDataType) {
                case HCISUPCMS.EHOME_REGISTER_TYPE.ENUM_DEV_ON: { //ENUM_DEV_ON
                    HCISUPCMS.NET_EHOME_DEV_REG_INFO_V12 strDevRegInfo = new HCISUPCMS.NET_EHOME_DEV_REG_INFO_V12();
                    strDevRegInfo.write();
                    Pointer pDevRegInfo = strDevRegInfo.getPointer();
                    pDevRegInfo.write(0, pOutBuffer.getByteArray(0, strDevRegInfo.size()), 0, strDevRegInfo.size());
                    strDevRegInfo.read();
                    HCISUPCMS.NET_EHOME_SERVER_INFO_V50 strEhomeServerInfo = new HCISUPCMS.NET_EHOME_SERVER_INFO_V50();
                    strEhomeServerInfo.read();
                    //strEhomeServerInfo.dwSize = strEhomeServerInfo.size();
                    byte[] byCmsIP = new byte[0];
                    //设置报警服务器地址、端口、类型
                    byCmsIP = propertiesUtil.readValue("AlarmServerIP").getBytes();
                    System.arraycopy(byCmsIP, 0, strEhomeServerInfo.struUDPAlarmSever.szIP, 0, byCmsIP.length);
                    System.arraycopy(byCmsIP, 0, strEhomeServerInfo.struTCPAlarmSever.szIP, 0, byCmsIP.length);
                    //报警服务器类型：0- 只支持UDP协议上报，1- 支持UDP、TCP两种协议上报 2-MQTT
                    strEhomeServerInfo.dwAlarmServerType = Integer.parseInt(propertiesUtil.readValue("AlarmServerType"));
                    strEhomeServerInfo.struTCPAlarmSever.wPort = Short.parseShort(propertiesUtil.readValue("AlarmServerTCPPort"));
                    strEhomeServerInfo.struUDPAlarmSever.wPort = Short.parseShort(propertiesUtil.readValue("AlarmServerUDPPort"));
                    byte[] byClouldAccessKey = "test".getBytes();
                    System.arraycopy(byClouldAccessKey, 0, strEhomeServerInfo.byClouldAccessKey, 0, byClouldAccessKey.length);
                    byte[] byClouldSecretKey = "12345".getBytes();
                    System.arraycopy(byClouldSecretKey, 0, strEhomeServerInfo.byClouldSecretKey, 0, byClouldSecretKey.length);
                    strEhomeServerInfo.dwClouldPoolId = 1;
                    //设置图片存储服务器地址、端口、类型
                    byte[] bySSIP = new byte[0];
                    bySSIP = propertiesUtil.readValue("PicServerIP").getBytes();
                    System.arraycopy(bySSIP, 0, strEhomeServerInfo.struPictureSever.szIP, 0, bySSIP.length);
                    strEhomeServerInfo.struPictureSever.wPort = Short.parseShort(propertiesUtil.readValue("PicServerPort"));
                    strEhomeServerInfo.dwPicServerType = Integer.parseInt(propertiesUtil.readValue("PicServerType"));    //存储服务器（SS）类型：0-Tomcat，1-VRB，2-云存储，3-KMS，4-ISUP5.0。
                    strEhomeServerInfo.write();
                    dwInLen = strEhomeServerInfo.size();
                    pInBuffer.write(0, strEhomeServerInfo.getPointer().getByteArray(0, dwInLen), 0, dwInLen);

                    String deviceID = new String(strDevRegInfo.struRegInfo.byDeviceID);
                    System.out.println("Device online, DeviceID is:" + deviceID.trim());

                    // FIXME demo逻辑中默认只支持一台设备的功能演示，多台设备需要自行调整这里设备登录后的句柄信息
                    IsupTest.lLoginID = lUserID;
                    IsupTest.deviceInfoMap.put(lUserID, deviceID);
                    return true;
                }
                case HCISUPCMS.EHOME_REGISTER_TYPE.ENUM_DEV_AUTH: { //ENUM_DEV_AUTH
                    HCISUPCMS.NET_EHOME_DEV_REG_INFO_V12 strDevRegInfo = new HCISUPCMS.NET_EHOME_DEV_REG_INFO_V12();
                    strDevRegInfo.write();
                    Pointer pDevRegInfo = strDevRegInfo.getPointer();
                    pDevRegInfo.write(0, pOutBuffer.getByteArray(0, strDevRegInfo.size()), 0, strDevRegInfo.size());
                    strDevRegInfo.read();
                    byte[] bs = new byte[0];
                    String szEHomeKey = propertiesUtil.readValue("ISUPKey"); //ISUP5.0登录校验值
                    bs = szEHomeKey.getBytes();
                    pInBuffer.write(0, bs, 0, szEHomeKey.length());
                    break;
                }
                case HCISUPCMS.EHOME_REGISTER_TYPE.ENUM_DEV_SESSIONKEY: { //HCISUPCMS.ENUM_DEV_SESSIONKEY
                    HCISUPCMS.NET_EHOME_DEV_REG_INFO_V12 strDevRegInfo = new HCISUPCMS.NET_EHOME_DEV_REG_INFO_V12();
                    strDevRegInfo.write();
                    Pointer pDevRegInfo = strDevRegInfo.getPointer();
                    pDevRegInfo.write(0, pOutBuffer.getByteArray(0, strDevRegInfo.size()), 0, strDevRegInfo.size());
                    strDevRegInfo.read();
                    HCISUPCMS.NET_EHOME_DEV_SESSIONKEY struSessionKey = new HCISUPCMS.NET_EHOME_DEV_SESSIONKEY();
                    System.arraycopy(strDevRegInfo.struRegInfo.byDeviceID, 0, struSessionKey.sDeviceID, 0, strDevRegInfo.struRegInfo.byDeviceID.length);
                    System.arraycopy(strDevRegInfo.struRegInfo.bySessionKey, 0, struSessionKey.sSessionKey, 0, strDevRegInfo.struRegInfo.bySessionKey.length);
                    struSessionKey.write();
                    Pointer pSessionKey = struSessionKey.getPointer();
                    hCEhomeCMS.NET_ECMS_SetDeviceSessionKey(pSessionKey);
                    AlarmDemo.hcEHomeAlarm.NET_EALARM_SetDeviceSessionKey(pSessionKey);
                    break;
                }
                case HCISUPCMS.EHOME_REGISTER_TYPE.ENUM_DEV_DAS_REQ: {  //HCISUPCMS.ENUM_DEV_DAS_REQ
                    String dasInfo = "{\n" +
                            "    \"Type\":\"DAS\",\n" +
                            "    \"DasInfo\": {\n" +
                            "        \"Address\":\"" + propertiesUtil.readValue("DasServerIP") + "\",\n" +
                            "        \"Domain\":\"\",\n" +
                            "        \"ServerID\":\"\",\n" +
                            "        \"Port\":" + propertiesUtil.readValue("DasServerPort") + ",\n" +
                            "        \"UdpPort\":0\n" +
                            "    }\n" +
                            "}";
                    System.out.println(dasInfo);
                    byte[] bs1 = dasInfo.getBytes();
                    pInBuffer.write(0, bs1, 0, dasInfo.length());
                    break;
                }
                case HCISUPCMS.EHOME_REGISTER_TYPE.ENUM_DEV_OFF: {
                    // 设备掉线
                    // 输出设备信息
                    System.out.println("[Device Offline] DeviceID is " + IsupTest.deviceInfoMap.get(lUserID) +"\n");
                    IsupTest.deviceInfoMap.remove(lUserID);
                    break;
                }
                default:
                    System.out.println("FRegisterCallBack default type:" + dwDataType);
                    break;
            }
            return true;
        }
    }

    /**
     * ISUP透传接口
     */
    public void CMS_ISAPIPassThrough() throws UnsupportedEncodingException {
        /**************************************************************
         * GET方法示例（以获取云存储配置为例）
         */
        HCISUPCMS.NET_EHOME_PTXML_PARAM m_struParam = new HCISUPCMS.NET_EHOME_PTXML_PARAM();
        m_struParam.read();

        //透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String url = "GET /ISAPI/System/PictureServer?format=json"; //获取云存储配置
        HCISUPCMS.BYTE_ARRAY ptrUrl = new HCISUPCMS.BYTE_ARRAY(url.length() + 1);
        System.arraycopy(url.getBytes(), 0, ptrUrl.byValue, 0, url.length());
        ptrUrl.write();
        m_struParam.pRequestUrl = ptrUrl.getPointer();
        m_struParam.dwRequestUrlLen = url.length();

        //输入参数，XML或者JSON数据
        m_struParam.pInBuffer = null;//GET获取时不需要输入参数，输入为null
        m_struParam.dwInSize = 0;

        //输出参数，分配的内存用于存储返回的数据，需要大于等于实际内容大小
        int iOutSize = 2 * 1024 * 1024;
        HCISUPCMS.BYTE_ARRAY ptrOutByte = new HCISUPCMS.BYTE_ARRAY(iOutSize);
        m_struParam.pOutBuffer = ptrOutByte.getPointer();
        m_struParam.dwOutSize = iOutSize;

        m_struParam.dwRecvTimeOut = 5000; //接收超时时间，单位毫秒
        m_struParam.write();

        if (!hCEhomeCMS.NET_ECMS_ISAPIPassThrough(IsupTest.lLoginID, m_struParam)) {
            System.out.println("NET_ECMS_ISAPIPassThrough failed,error：" + hCEhomeCMS.NET_ECMS_GetLastError());
            return;
        } else {
            m_struParam.read();
            ptrOutByte.read();
            System.out.println("NET_ECMS_ISAPIPassThrough succeed\n" + "ptrOutByte:" + new String(ptrOutByte.byValue).trim());
        }

        /**************************************************************
         * PUT方法示例（以设置云存储配置为例）
         */
        HCISUPCMS.NET_EHOME_PTXML_PARAM m_struParam2 = new HCISUPCMS.NET_EHOME_PTXML_PARAM();
        m_struParam2.read();

        //透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String url2 = "PUT /ISAPI/System/PictureServer?format=json"; //获取云存储配置
        HCISUPCMS.BYTE_ARRAY ptrUrl2 = new HCISUPCMS.BYTE_ARRAY(url2.length() + 1);
        System.arraycopy(url2.getBytes(), 0, ptrUrl2.byValue, 0, url2.length());
        ptrUrl2.write();
        m_struParam2.pRequestUrl = ptrUrl2.getPointer();
        m_struParam2.dwRequestUrlLen = url2.length();

        //输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("ipv4Address", propertiesUtil.readValue("PicServerIP"));
        parameter.put("portNo", Short.parseShort(propertiesUtil.readValue("PicServerPort")));
        String input = ConfigFileUtil.getReqBodyFromTemplate("conf/SdkCms/PutPicServerParam.json", parameter);
        System.out.println(input);

        int iInSize = input.getBytes("utf-8").length;
        HCISUPCMS.BYTE_ARRAY ptrInParam = new HCISUPCMS.BYTE_ARRAY(iInSize + 1);
        System.arraycopy(input.getBytes("utf-8"), 0, ptrInParam.byValue, 0, iInSize);
        ptrInParam.write();
        m_struParam2.pInBuffer = ptrInParam.getPointer();//GET获取时不需要输入参数，输入为null
        m_struParam2.dwInSize = iInSize;

        //输出参数，分配的内存用于存储返回的数据，需要大于等于实际内容大小
        int iOutSize2 = 2 * 1024 * 1024;
        HCISUPCMS.BYTE_ARRAY ptrOutByte2 = new HCISUPCMS.BYTE_ARRAY(iOutSize2);
        m_struParam2.pOutBuffer = ptrOutByte2.getPointer();
        m_struParam2.dwOutSize = iOutSize2;

        m_struParam2.dwRecvTimeOut = 5000; //接收超时时间，单位毫秒
        m_struParam2.write();

        if (!hCEhomeCMS.NET_ECMS_ISAPIPassThrough(IsupTest.lLoginID, m_struParam2)) {
            System.out.println("NET_ECMS_ISAPIPassThrough failed,error：" + hCEhomeCMS.NET_ECMS_GetLastError());
            return;
        } else {
            m_struParam2.read();
            ptrOutByte2.read();
            System.out.println("NET_ECMS_ISAPIPassThrough succeed\n" + "ptrOutByte:" + new String(ptrOutByte2.byValue).trim());
        }
        return;
    }

    /**
     * 通过透传接口下发人脸图片数据进行分析建模(超脑NVR等设备支持)
     */
    public void CMS_ISAPIPassThroughByFile() {
        HCISUPCMS.NET_EHOME_PTXML_PARAM m_struParam = new HCISUPCMS.NET_EHOME_PTXML_PARAM();
        m_struParam.read();

        String url = "POST /ISAPI/Intelligent/analysisImage/face";
        HCISUPCMS.BYTE_ARRAY ptrUrl1 = new HCISUPCMS.BYTE_ARRAY(url.length());
        System.arraycopy(url.getBytes(), 0, ptrUrl1.byValue, 0, url.length());
        ptrUrl1.write();
        m_struParam.pRequestUrl = ptrUrl1.getPointer();
        m_struParam.dwRequestUrlLen = url.length();

        FileInputStream picfile = null;
        int picdataLength = 0;
        try {
            picfile = new FileInputStream(CommonMethod.getResFileAbsPath("pics/FDLib.jpg"));
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

        try {
            picdataLength = picfile.available();
        } catch (IOException e1) {
            e1.printStackTrace();
        }
        if (picdataLength < 0) {
            System.out.println("input file dataSize < 0");
            return;
        }

        HCISUPCMS.BYTE_ARRAY ptrpicByte = new HCISUPCMS.BYTE_ARRAY(picdataLength);
        try {
            picfile.read(ptrpicByte.byValue);
        } catch (IOException e2) {
            e2.printStackTrace();
        }
        ptrpicByte.write();
        m_struParam.pInBuffer = ptrpicByte.getPointer();
        m_struParam.dwInSize = picdataLength;
        HCISUPCMS.BYTE_ARRAY ptrOutByte = new HCISUPCMS.BYTE_ARRAY(20 * 1024);
        m_struParam.pOutBuffer = ptrOutByte.getPointer();
        m_struParam.dwOutSize = 20 * 1024;
        m_struParam.write();

        if (!hCEhomeCMS.NET_ECMS_ISAPIPassThrough(IsupTest.lLoginID, m_struParam)) {
            int iErr = hCEhomeCMS.NET_ECMS_GetLastError();
            System.out.println("NET_ECMS_ISAPIPassThrough failed,error：" + iErr);
            return;
        } else {
            m_struParam.read();
            ptrOutByte.read();
            System.out.println("NET_ECMS_ISAPIPassThrough succeed\n" + "ptrOutByte:" + new String(ptrOutByte.byValue).trim());
        }
    }

    /**
     * NET_ECMS_XMLRemoteControl接口示例
     */
    public void CMS_XMLRemoteControl() {
        //远程控制输入参数
        HCISUPCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM struRemoteCtrl = new HCISUPCMS.NET_EHOME_XML_REMOTE_CTRL_PARAM();
        struRemoteCtrl.read();
        struRemoteCtrl.dwSize = struRemoteCtrl.size();
        struRemoteCtrl.dwRecvTimeOut = 5000; //接收超时时间
        struRemoteCtrl.dwSendTimeOut = 5000; //发送超时时间

        //获取设备工作状态(比如NVR设备，可以获取IP通道状态)
        String inputCfg = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<PPVSPMessage>\n" +
                "<Version>4.0</Version>\n" +
                "<Sequence>1</Sequence>\n" +
                "<CommandType>REQUEST</CommandType>\n" +
                "<Method>QUERY</Method>\n" +
                "<Command>GETDEVICEWORKSTATUS</Command>\n" +
                "<Params>\n" +
                "</Params>\n" +
                "</PPVSPMessage>";
//        //远程升级的报文
//        String inputCfg = "<?xml version=\"1.0\" encoding=\"GB2312\" ?>\n" +
//                "<PPVSPMessage>\n" +
//                "    <Version>4.0</Version>\n" +
//                "    <Sequence>1</Sequence>\n" +
//                "    <CommandType>REQUEST</CommandType>\n" +
//                "    <Method>CONTROL</Method>\n" +
//                "    <Command>UPDATE</Command>\n" +
//                "    <Params>\n" +
//                "        <FTPServerIP>10.17.34.106</FTPServerIP>\n" +
//                "        <FTPServerPort>8089</FTPServerPort>\n" +
//                "        <Account>hik</Account>\n" +
//                "        <Password>hik12345</Password>\n" +
//                "        <File>digicap.dav</File>\n" +
//                "    </Params>\n" +
//                "</PPVSPMessage>";

        HCISUPCMS.BYTE_ARRAY m_struInbuffer = new HCISUPCMS.BYTE_ARRAY(inputCfg.length() + 1);
        System.arraycopy(inputCfg.getBytes(), 0, m_struInbuffer.byValue, 0, inputCfg.length());
        m_struInbuffer.write();
        struRemoteCtrl.lpInbuffer = m_struInbuffer.getPointer();
        struRemoteCtrl.dwInBufferSize = m_struInbuffer.size();

        //输出参数
        HCISUPCMS.BYTE_ARRAY m_struOutbuffer = new HCISUPCMS.BYTE_ARRAY(5 * 1024);
        struRemoteCtrl.lpOutBuffer = m_struOutbuffer.getPointer();
        struRemoteCtrl.dwOutBufferSize = m_struOutbuffer.size();
        //输出状态信息
        HCISUPCMS.BYTE_ARRAY m_struStatusBuffer = new HCISUPCMS.BYTE_ARRAY(2 * 1024);
        struRemoteCtrl.lpStatusBuffer = m_struStatusBuffer.getPointer();
        struRemoteCtrl.dwStatusBufferSize = m_struStatusBuffer.size();

        struRemoteCtrl.write();

        if (!hCEhomeCMS.NET_ECMS_XMLRemoteControl(IsupTest.lLoginID, struRemoteCtrl, struRemoteCtrl.size())) {
            int iErr = hCEhomeCMS.NET_ECMS_GetLastError();
            System.out.println("NET_ECMS_XMLRemoteControl failed,error：" + iErr);
            return;
        } else {
            struRemoteCtrl.read();
            m_struOutbuffer.read();
            m_struStatusBuffer.read();
            System.out.println("NET_ECMS_XMLRemoteControl succeed：");
            System.out.println("lpOutBuffer:" + new String(m_struOutbuffer.byValue).trim());
            System.out.println("lpStatusBuffer:" + new String(m_struStatusBuffer.byValue).trim());

//            //输出示例报文
//            "<PPVSPMessage>\n" +
//                    "<Version>2.0</Version>\n" +
//                    "<Sequence>422</Sequence>\n" +
//                    "<CommandType>RESPONSE</CommandType>\n" +
//                    "<WhichCommand>GETDEVICEWORKSTATUS</WhichCommand>\n" +
//                    "<Status>200</Status>\n" +
//                    "<Description>OK</Description>\n" +
//                    "<Params>\n" +
//                    "<DeviceStatusXML>\n" +
//                    "<Run>0</Run>\n" +
//                    "<CPU>14</CPU>\n" +
//                    "<Mem>19</Mem>\n" +
//                    "<DSKStatus>\n" +
//                    "<DSK>1-5723166-1381376-0</DSK>\n" +
//                    "<DSK>3-0-0-2</DSK>\n" +
//                    "</DSKStatus>\n" +
//                    "<CHStatus>\n" +
//                    "<CH>1-1-0-1-2048-0-1-1</CH>\n" +
//                    "<CH>2-0-1-0-0-0-1-0</CH>\n" +
//                    "</CHStatus>\n" +
//                    "<AlarmInStatus/>\n" +
//                    "<AlarmOutStatus/>\n" +
//                    "<LocalDisplayStatus>0</LocalDisplayStatus>\n" +
//                    "<Remark>test/debug</Remark>\n" +
//                    "</DeviceStatusXML>\n" +
//                    "</Params>\n" +
//                    "</PPVSPMessage>"
//  其中CHStatus的CH通道状态用一串字符串表示多个信息，用“-”分割：
// 通道号-录像状态（0：不录像；1：录像）-视频连接信号状态（0：正常；1：视频信号丢失）-通道编码状态（0：正常；1：异常，例如DSP死掉）-实际码率(kbps)-客户端连接数目
        }
    }


    /**
     * NET_ECMS_GetDevConfig接口示例
     *
     * @return
     */
    public boolean getDevConfig() {
        boolean bRet;
        //  关于报警输入配置条件参数的结构体
        HCISUPCMS.NET_EHOME_ALARMIN_COND netEhomeAlarminCond = new HCISUPCMS.NET_EHOME_ALARMIN_COND();
        netEhomeAlarminCond.read();
        netEhomeAlarminCond.dwSize = netEhomeAlarminCond.size();
        netEhomeAlarminCond.dwAlarmInNum = 1;
        netEhomeAlarminCond.dwPTZChan = 1;
        netEhomeAlarminCond.write();
        HCISUPCMS.NET_EHOME_ALARMIN_CFG netEhomeAlarminCfg = new HCISUPCMS.NET_EHOME_ALARMIN_CFG();
        netEhomeAlarminCfg.read();
        netEhomeAlarminCfg.dwSize = netEhomeAlarminCfg.size();
        netEhomeAlarminCfg.write();

        HCISUPCMS.NET_EHOME_CONFIG strEhomeCfd = new HCISUPCMS.NET_EHOME_CONFIG();
        strEhomeCfd.pCondBuf = netEhomeAlarminCond.getPointer();
        strEhomeCfd.dwCondSize = netEhomeAlarminCond.size();
        strEhomeCfd.pOutBuf = netEhomeAlarminCfg.getPointer();
        strEhomeCfd.dwOutSize = netEhomeAlarminCfg.size();
        strEhomeCfd.pInBuf = null;
        strEhomeCfd.dwInSize = 0;
        strEhomeCfd.write();


        bRet = hCEhomeCMS.NET_ECMS_GetDevConfig(IsupTest.lLoginID, 11, strEhomeCfd.getPointer(), strEhomeCfd.size());
        if (!bRet) {
            int dwErr = hCEhomeCMS.NET_ECMS_GetLastError();
            System.out.println("获取报警输入参数失败，Error:" + dwErr);
        }
        //  读取返回的数据
        netEhomeAlarminCfg.read();
        System.out.println("获取报警输入参数成功" + netEhomeAlarminCfg.byAlarmInType);
        return bRet;
    }

    /**
     * 获取设备信息的命令：NET_EHOME_GET_DEVICE_INFO
     *
     * @return
     */
    public HCISUPCMS.NET_EHOME_DEVICE_INFO getDevInfo() {
        boolean bRet;

        HCISUPCMS.NET_EHOME_DEVICE_INFO ehomeDeviceInfo = new HCISUPCMS.NET_EHOME_DEVICE_INFO();
        ehomeDeviceInfo.read();
        ehomeDeviceInfo.dwSize = ehomeDeviceInfo.size();
        ehomeDeviceInfo.write();

        HCISUPCMS.NET_EHOME_CONFIG strEhomeCfd = new HCISUPCMS.NET_EHOME_CONFIG();
        strEhomeCfd.pCondBuf = null;
        strEhomeCfd.dwCondSize = 0;
        strEhomeCfd.pOutBuf = ehomeDeviceInfo.getPointer();
        strEhomeCfd.dwOutSize = ehomeDeviceInfo.size();
        strEhomeCfd.pInBuf = null;
        strEhomeCfd.dwInSize = 0;
        strEhomeCfd.write();


        bRet = hCEhomeCMS.NET_ECMS_GetDevConfig(IsupTest.lLoginID, 1, strEhomeCfd.getPointer(), strEhomeCfd.size());
        if (!bRet) {
            int dwErr = hCEhomeCMS.NET_ECMS_GetLastError();
            System.out.println("获取报警输入参数失败，Error:" + dwErr);
        }else{
            //  读取返回的数据
            ehomeDeviceInfo.read();
            System.out.println("语音对讲的音频格式:" + ehomeDeviceInfo.dwAudioEncType);
            System.out.println("起始数字对讲通道号:" + ehomeDeviceInfo.byStartDTalkChan);
        }
        return ehomeDeviceInfo;
    }

    /**
     * NET_ECMS_RemoteControl接口示例
     */
    public static void RemoteControl() {
        //云台控制
        HCISUPCMS.NET_EHOME_REMOTE_CTRL_PARAM net_ehome_remote_ctrl_param = new HCISUPCMS.NET_EHOME_REMOTE_CTRL_PARAM();
        HCISUPCMS.NET_EHOME_PTZ_PARAM net_ehome_ptz_param = new HCISUPCMS.NET_EHOME_PTZ_PARAM();
        net_ehome_ptz_param.read();
        net_ehome_ptz_param.dwSize = net_ehome_ptz_param.size();
        net_ehome_ptz_param.byPTZCmd = 2;//0-向上,1-向下,2-向左,3-向右，更多取值参考接口文档
        net_ehome_ptz_param.byAction = 0;//云台动作：0- 开始云台动作，1- 停止云台动作
        net_ehome_ptz_param.bySpeed = 5;//云台速度，取值范围：0~7，数值越大速度越快
        net_ehome_ptz_param.write();
        net_ehome_remote_ctrl_param.read();
        net_ehome_remote_ctrl_param.dwSize = net_ehome_remote_ctrl_param.size();
        net_ehome_remote_ctrl_param.lpInbuffer = net_ehome_ptz_param.getPointer();//输入控制参数
        net_ehome_remote_ctrl_param.dwInBufferSize = net_ehome_ptz_param.size();

        //条件参数输入通道号
        int iChannel = 1; //视频通道号
        IntByReference channle = new IntByReference(iChannel);
        net_ehome_remote_ctrl_param.lpCondBuffer = channle.getPointer();
        net_ehome_remote_ctrl_param.dwCondBufferSize = 4;

        net_ehome_remote_ctrl_param.write();

        boolean b_ptz = hCEhomeCMS.NET_ECMS_RemoteControl(IsupTest.lLoginID, HCISUPCMS.NET_EHOME_PTZ_CTRL, net_ehome_remote_ctrl_param);
        if (!b_ptz) {
            int iErr = hCEhomeCMS.NET_ECMS_GetLastError();
            System.out.println("NET_ECMS_XMLConfig failed,error：" + iErr);
            return;
        }
        System.out.println("云台控制调用成功");
    }

    /**
     * 远程抓图
     *  URL中的<ID>，格式A0B，A表示实际通道号，B传1表示主码流，传2表示子码流。
     * 示例：GET /ISAPI/Streaming/channels/101/picture/async?format=json&imageType=JPEG&URLType=cloudURL表示获取通道1的主码流分辨率的抓图。
     * @param lUserID
     */
    public  void GetPicByCloud(int lUserID)
    {

        String getPicURL= "GET /ISAPI/Streaming/channels/101/picture/async?format=json&imageType=JPEG&URLType=cloudURL";
        SdkFunctionWrapUtil.isapiPassThrough(lUserID,getPicURL,"");

    }

    /**
     * 云台控制 ISUP5.0透传接口
     * @param lUserID
     */
    public void PTZCtrl(int lUserID)
    {
        String PTZCtrlUrl = "PUT /ISAPI/PTZCtrl/channels/1/continuous";
        /**
         * pan 表示水平运行速度 +60表示右移，其他方向运行参考类似方法
         * tilt 表示垂直运行速度
         */
        String PTZCtrlInput = "<?xml version: \"1.0\" encoding=\"UTF-8\"?>\n" +
                "<PTZData>\n" +
                "    <pan>60</pan>\n" +
                "    <tilt>0</tilt>\n" +
                "</PTZData>";
        //接口调用成功后，云台会一直按照设置速度进行右转
        SdkFunctionWrapUtil.isapiPassThrough(lUserID,PTZCtrlUrl,PTZCtrlInput);
        try {
            Thread.sleep(1000); //云台运动持续时间1s后调用停止云台运动接口
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        //发送云台运动停止请求
        String PTZCtrlStopInput = "<?xml version: \"1.0\" encoding=\"UTF-8\"?>\n" +
                "<PTZData>\n" +
                "    <pan>0</pan>\n" +
                "    <tilt>0</tilt>\n" +
                "</PTZData>";

        SdkFunctionWrapUtil.isapiPassThrough(lUserID,PTZCtrlUrl,PTZCtrlStopInput);

    }

    /**
     * 录像文件查找
     * @param lUserID
     */
    public void FindRecored(int lUserID)
    {
        HCISUPCMS.NET_EHOME_REC_FILE_COND  RecFileCond = new HCISUPCMS.NET_EHOME_REC_FILE_COND();
        //录像查询条件
        RecFileCond.dwChannel = 1; //通道号
        RecFileCond.dwRecType = 0xff; //录像类型 0xff:全部录像
        RecFileCond.dwStartIndex = 0; //查询的起始位置，从0开始。
        RecFileCond.dwMaxFileCountPer = 8; //单次搜索可查询的最大文件数，由实际网络环境决定。建议最大文件数设为8。
        RecFileCond.byLocalOrUTC =0 ;
        RecFileCond.struStartTime.wYear = 2024;
        RecFileCond.struStartTime.byMonth = 9;
        RecFileCond.struStartTime.byDay = 3;
        RecFileCond.struStartTime.byHour = 7;
        RecFileCond.struStartTime.byMinute = 00;
        RecFileCond.struStartTime.bySecond = 00;
        RecFileCond.struStopTime.wYear = 2024;
        RecFileCond.struStopTime.byMonth = 9;
        RecFileCond.struStopTime.byDay = 3;
        RecFileCond.struStopTime.byHour = 18;
        RecFileCond.struStopTime.byMinute = 00;
        RecFileCond.struStopTime.bySecond = 00;
        RecFileCond.write();
        int FindFileHandle = hCEhomeCMS.NET_ECMS_StartFindFile_V11(lUserID,HCISUPCMS.ENUM_SEARCH_RECORD_FILE,RecFileCond.getPointer(),RecFileCond.size());
        if (FindFileHandle<=-1)
        {
            System.out.println("NET_ECMS_StartFindFile_V11 error code:"+hCEhomeCMS.NET_ECMS_GetLastError());
            return;
        }
        while (true)
        {
            HCISUPCMS.NET_EHOME_REC_FILE struFindData = new HCISUPCMS.NET_EHOME_REC_FILE();

            long State=hCEhomeCMS.NET_ECMS_FindNextFile_V11(FindFileHandle,struFindData.getPointer(),struFindData.size());
            if (State<=-1)
            {

                System.out.println("查找失败，错误码为" + hCEhomeCMS.NET_ECMS_GetLastError());
                return;

            }
            else if (State==1000)  //获取文件信息成功
            {
                struFindData.read();
                try {
                    String strFileName=new String(struFindData.sFileName,"UTF-8").trim();
                    System.out.println("文件名称："+strFileName);
                    System.out.println("文件大小:"+struFindData.dwFileSize);
                } catch (UnsupportedEncodingException e) {
                    e.printStackTrace();
                }
                System.out.println("获取文件成功");
                continue;
            }
            else if (State==1001) //未查找到文件
            {
                System.out.println("未查找到文件");
                break;
            }
            else if (State==1002) //正在查找请等待
            {
                System.out.println("正在查找，请等待");
                continue;

            }

            else if (State==1003) //没有更多的文件，查找结束
            {
                System.out.println("没有更多的文件，查找结束");
                break;

            }
            else if (State==1004) //查找文件时异常
            {

                System.out.println("检索异常");
                break;

            }
            else if (State==1005) //查找文件超时
            {

                System.out.println("设备不支持改操作");
                break;

            }

        }

         if(!hCEhomeCMS.NET_ECMS_StopFindFile(FindFileHandle))
         {
             System.out.println("NET_ECMS_StopFindFile error code:"+hCEhomeCMS.NET_ECMS_GetLastError());
         }
        System.out.println("NET_ECMS_StopFindFile suss");
         return;
    }

    /**
     * 获取设备信息（型号、版本、序列号等）
     * @param lUserID
     */
    public void GetDevInfo(int lUserID)
    {
        String getDevInfoURL= "GET /ISAPI/System/deviceInfo";
        SdkFunctionWrapUtil.isapiPassThrough(lUserID,getDevInfoURL,"");
        return;
    }

    /**
     * 手动校时
     * @param lUserID
     */
    public void SetDevTimeCfg(int lUserID){

        String SetDevTimeURL= "PUT /ISAPI/System/time";
        //输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("localTime", "2024-09-25T11:11:29");
        String input = ConfigFileUtil.getReqBodyFromTemplate("/conf/SdkCms/SetTimeCfg.xml", parameter);
        System.out.println(input);
        SdkFunctionWrapUtil.isapiPassThrough(lUserID,SetDevTimeURL,input);
        return;
    }








}
