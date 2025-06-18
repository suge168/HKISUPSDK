package DevModule.DeepInMind;

import Common.ConfigFileUtil;
import Common.SdkFunctionWrapUtil;

import java.util.HashMap;
import java.util.Map;

/**
 * @author zhengxiaohui
 * @date 2023/9/6 16:19
 * @desc 人脸库记录管理模块
 */
public class FaceLibraryRecordManage {

    /**
     * 导入图片到人脸库（url方式）
     *
     * @param loginID ISUP登录的句柄
     * @param fdId    人脸库FDID
     * @param picUrl  图片URL地址
     */
    public static void addFace2FaceLib(int loginID, String fdId, String picUrl) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib/pictureUpload";

        // 透传参数组织
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("FDID", fdId); // 人脸比对库唯一标示ID
        parameter.put("picURL", picUrl); // 图片URL
        parameter.put("bornTime", "2000-01-01"); // 出生日期
        parameter.put("name", "张三"); // 姓名
        parameter.put("sex", "male"); // 性别 [male#男,female#女]

        // 这里获取外部的ISAPI报文模板并做参数的占位符替换操作，实际可以在代码中拼接字符串方式实现
        // 另外：如果需要完整报文请您申请并参考我司ISAPI协议文档说明: https://open.hikvision.com/agreement?type=100
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceLibRecord/AddFace2FaceLib.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }

    /**
     * 查询人脸图片数据
     *
     * @param loginID ISUP登录的句柄
     * @param fdId    人脸库FDID
     */
    public static void searchFaceImageRecord(int loginID, String fdId) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib/FDSearch";

        // 透传参数组织
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("FDID", fdId); // 人脸比对库唯一标示ID
        parameter.put("maxResults", 30); //  本次查询(每页显示)最大记录数
        parameter.put("searchID", "searchID_test"); // string, 搜索记录唯一标识, desc:用来确认上层客户端是否为同一个(倘若是同一个,则设备记录内存,下次搜索加快速度
        parameter.put("searchResultPosition", 0); //  查询结果在结果列表中的起始位置, desc:当记录条数很多时, 一次查询不能获取所有的记录, 下一次查询时指定位置可以查询后面的记录

        // 这里获取外部的ISAPI报文模板并做参数的占位符替换操作，实际可以在代码中拼接字符串方式实现
        // 另外：如果需要完整报文请您申请并参考我司ISAPI协议文档说明: https://open.hikvision.com/agreement?type=100
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceLibRecord/SearchFaceImage.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }

    /**
     * 更新人脸图片
     *
     * @param loginID   登录句柄
     * @param fdId      人脸库FDID
     * @param pId       图片ID
     * @param newPicUrl 要更新的图片url
     */
    public static void updateFaceImageInfo(int loginID, String fdId, String pId, String newPicUrl) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "POST /ISAPI/Intelligent/FDLib/" + fdId + "/picture/" + pId + "/imageData";

        // 透传参数组织
        Map<String, Object> parameter = new HashMap<>();
        parameter.put("picUrl", newPicUrl); // 人脸比对库唯一标示ID

        // 这里获取外部的ISAPI报文模板并做参数的占位符替换操作，实际可以在代码中拼接字符串方式实现
        // 另外：如果需要完整报文请您申请并参考我司ISAPI协议文档说明: https://open.hikvision.com/agreement?type=100
        String reqContent = ConfigFileUtil.getReqBodyFromTemplate("conf/deepInMind/faceLibRecord/UpdateFaceImage.xml", parameter);
        System.out.println(reqContent);

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, reqContent);
    }


    /**
     * 删除人脸图片
     *
     * @param loginID 登录句柄
     * @param fdId    人脸库FDID
     * @param pId     图片ID
     */
    public static void deleteFaceImageInfo(int loginID, String fdId, String pId) {
        // 透传URL，不同功能对应不同的URL，完整协议报文说明需要参考ISAPI协议文档
        String reqUrl = "DELETE /ISAPI/Intelligent/FDLib/" + fdId + "/picture/" + pId;

        // 调用CMS服务透传ISAPI接口
        SdkFunctionWrapUtil.isapiPassThrough(loginID, reqUrl, null);
    }


}
