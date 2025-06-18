package DevModule.DeepInMind;

import Common.ConfigFileUtil;
import Common.SdkFunctionWrapUtil;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * @author zhengxiaohui
 * @date 2023/8/30 19:28
 * @desc 人脸库管理模块
 */
public class FaceLibraryManage {

    /**
     * 添加人脸库
     *
     * @param loginID
     * @param name 人脸库demo名称
     * @throws UnsupportedEncodingException
     */
    public static void addFaceLibrary(int loginID, String name) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib";

        // 输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("name", name); // 人脸库名称
        parameter.put("thresholdValue", 60); // 检测阈值, range:[0,100]
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceLib/AddFaceLib.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }

    /**
     * 获取全部的人脸库信息
     *
     * @param loginID
     */
    public static void getAllFaceLibrary(int loginID)  {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "GET /ISAPI/Intelligent/FDLib?format=json";

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, null);
    }

    /**
     * 删除单个人脸库
     *
     * @param loginID
     * @param fdId 人脸库id FDID字段
     */
    public static void deleteFaceLibraryByFDID(int loginID, String fdId) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "DELETE /ISAPI/Intelligent/FDLib/" + fdId;

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, null);
    }

    /**
     * 获取人脸比对库全部配置
     *
     * @param loginID
     */
    public static void getAllFaceLibraryCfgParam(int loginID) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "GET /ISAPI/Intelligent/FDLib";

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, null);
    }

    /**
     * 人脸库图片检索
     * 查询人脸图片数据
     *
     * @param loginID 登录句柄
     * @param modeData 目标模型数据
     */
    public static void searchFaceLibImageByImage(int loginID, String modeData) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib/FDSearch";

        // 输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("importStartTime", "2020-05-25T23:59:59Z"); // 入库开始时间
        parameter.put("importEndTime", "2033-05-25T23:59:59Z"); // 入库结束时间
        parameter.put("maxResults", 20); //  本次查询(每页显示)最大记录数
        parameter.put("searchResultPosition", 1); //  查询结果在结果列表中的起始位置
        parameter.put("similarity", 50); //   相似度, range:[0.0,100.0]
        parameter.put("modeData", modeData); //  目标模型数据, desc:传输过程中针对二进制非建模数据进行base64的加密处理
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceLib/SearchFDByImage.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }



}
