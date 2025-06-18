package DevModule.DeepInMind;

import Common.ConfigFileUtil;
import Common.SdkFunctionWrapUtil;

import java.util.HashMap;
import java.util.Map;

/**
 * @author zhengxiaohui
 * @date 2023/9/7 13:50
 * @desc 人脸比对管理
 */
public class FaceCompareManage {

    /**
     * 查询人脸图片分析
     *
     * @param loginID 登录句柄
     * @param picUrl 图片的url信息
     */
    public static void getFacePicAnalyzeResult(int loginID, String picUrl) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/analysisImage/face?dataType=url";

        // 输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        // 图片URL:当url包含dataType=url时，表示图片采用url方式导入，此时传入报文为<FaceContrastData>，Content-Type: application/xml
        parameter.put("URL", picUrl);
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceCompare/GetFaceAnalyzeModelData.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }

    /**
     * 抓拍库图片检索
     * 查询人脸（多通道）
     *
     * @param loginID 登录句柄
     * @param modeData 目标模型数据
     */
    public static void searchFaceCompareResult(int loginID, String modeData) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib/FCSearch";

        // 输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("snapStartTime", "2020-05-25T23:59:59Z"); // 抓拍图片的开始时间
        parameter.put("snapEndTime", "2033-05-25T23:59:59Z"); // 抓拍图片的结束时间
        parameter.put("maxResults", 20); //  本次查询(每页显示)最大记录数
        parameter.put("searchResultPosition", 1); //  查询结果在结果列表中的起始位置
        parameter.put("similarity", 50); //   相似度, range:[0.0,100.0]
        parameter.put("modeData", modeData); //  目标模型数据, desc:传输过程中针对二进制非建模数据进行base64的加密处理
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceCompare/SearchFCByImage.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }

    /**
     * 查询人脸比对报警记录
     *
     * @param loginID 登录句柄
     */
    public static void searchFaceCompareEventResult(int loginID) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib/FCSearch";

        // 输入参数，XML或者JSON数据
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("snapStartTime", "2020-05-25T23:59:59Z"); // 抓拍图片的开始时间
        parameter.put("snapEndTime", "2033-05-25T23:59:59Z"); // 抓拍图片的结束时间
        parameter.put("maxResults", 20); //  本次查询(每页显示)最大记录数
        parameter.put("searchResultPosition", 1); //  查询结果在结果列表中的起始位置
        parameter.put("similarity", 50); //   相似度, range:[0.0,100.0]
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceCompare/SearchFaceEventCompare.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }
}
