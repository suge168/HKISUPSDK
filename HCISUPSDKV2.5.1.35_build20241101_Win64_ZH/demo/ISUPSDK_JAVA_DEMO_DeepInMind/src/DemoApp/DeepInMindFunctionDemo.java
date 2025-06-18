package DemoApp;

import Common.CommonMethod;
import DevModule.DeepInMind.FaceCompareManage;
import DevModule.DeepInMind.FaceLibraryManage;
import DevModule.DeepInMind.FaceLibraryRecordManage;

import java.io.UnsupportedEncodingException;

import static SdkService.CmsService.CmsDemo.findFile;

/**
 * @author zhengxiaohui
 * @date 2023/8/16 18:52
 * @desc 超脑相关的代码实现
 */
public class DeepInMindFunctionDemo {

    public static void dispatch(String command, int lLoginID) throws UnsupportedEncodingException {
        switch (command) {
            case "30001": {
                System.out.println("\n[Function]创建人脸库");
                FaceLibraryManage.addFaceLibrary(lLoginID, "isupDemoFaceLib");
                break;
            }
            case "30002": {
                System.out.println("\n[Function]获取全部的人脸库信息");
                FaceLibraryManage.getAllFaceLibrary(lLoginID);
                break;
            }
            case "30003": {
                System.out.println("\n[Function]删除单个人脸库");
                String fdID = CommonMethod.getConsoleInput("请输入您想要删除的人脸库FDID，FDID信息可以先调用 获取全部的人脸库信息 获取：");
                FaceLibraryManage.deleteFaceLibraryByFDID(lLoginID, fdID);
                break;
            }
            case "30004": {
                System.out.println("\n[Function]获取人脸比对库全部配置");
                FaceLibraryManage.getAllFaceLibraryCfgParam(lLoginID);
                break;
            }
            case "30005": {
                System.out.println("\n[Function]人脸库图片查询");
                String modeData = CommonMethod.getConsoleInput("请输入您想要查询的图片建模数据（图片建模数据可以通过 30201:查询图片分析-获取图片建模数据 获取)");

                FaceLibraryManage.searchFaceLibImageByImage(lLoginID, modeData);
                break;
            }
            case "30100": {
                System.out.println("\n[Function]导入图片到人脸库（图片url方式）");
                String fdID = CommonMethod.getConsoleInput("请输入您想要导入的人脸库FDID（FDID信息可以先调用 30002:获取全部的人脸库信息 获取）");
                String picUrl = CommonMethod.getConsoleInput("请输入您想要下发图片的URL地址(图片URL可以参见 f2000:上传图片至存储服务器(返回图片URL可以用于人脸下发) 获取)");
                FaceLibraryRecordManage.addFace2FaceLib(lLoginID, fdID, picUrl);
                break;
            }
            case "30101": {
                System.out.println("\n[Function]查询人脸图片数据");
                String fdID = CommonMethod.getConsoleInput("请输入您想要查询的人脸库FDID（FDID信息可以先调用 30002:获取全部的人脸库信息 获取）");
                FaceLibraryRecordManage.searchFaceImageRecord(lLoginID, fdID);
                break;
            }
            case "30102": {
                System.out.println("\n[Function]更新人脸图片");
                String fdID = CommonMethod.getConsoleInput("请输入您想要更新图片的人脸库FDID（FDID信息可以先调用 30002:获取全部的人脸库信息 获取）");
                String pID = CommonMethod.getConsoleInput("请输入您想要更新图片PID（FDID信息可以先调用 30101:查询人脸图片数据 获取）");
                String newPicUrl = CommonMethod.getConsoleInput("请输入您想要下发更新的图片URL地址(图片URL可以参见 f2000:上传图片至存储服务器(返回图片URL可以用于人脸下发) 获取)");

                FaceLibraryRecordManage.updateFaceImageInfo(lLoginID, fdID, pID, newPicUrl);
                break;
            }
            case "30103": {
                System.out.println("\n[Function]删除人脸图片");
                String fdID = CommonMethod.getConsoleInput("请输入您想要删除图片的人脸库FDID（FDID信息可以先调用 30002:获取全部的人脸库信息 获取）");
                String pID = CommonMethod.getConsoleInput("请输入您想要删除的图片PID（FDID信息可以先调用 30101:查询人脸图片数据 获取）");

                FaceLibraryRecordManage.deleteFaceImageInfo(lLoginID, fdID, pID);
                break;
            }
            case "30201": {
                System.out.println("\n[Function]查询人脸图片分析-获取图片建模数据");
                String picUrl = CommonMethod.getConsoleInput("请输入您想要进行图片分析的图片URL地址(图片URL可以参见 f2000:上传图片至存储服务器(返回图片URL可以用于人脸下发) 获取)");

                FaceCompareManage.getFacePicAnalyzeResult(lLoginID, picUrl);
                break;
            }
            case "30202": {
                System.out.println("\n[Function]抓拍库图片检索");
                String modeData = CommonMethod.getConsoleInput("请输入您想要查询的图片建模数据（图片建模数据可以通过 30201:查询人脸图片分析-获取图片建模数据 获取)");

                FaceCompareManage.searchFaceCompareResult(lLoginID, modeData);
                break;
            }
            case "30203": {
                System.out.println("\n[Function]查询人脸比对报警记录");

                FaceCompareManage.searchFaceCompareEventResult(lLoginID);
                break;
            }
            case "30301": {
                System.out.println("\n[Function]查询录像文件");
                findFile(1); //查询通道号1的录像
                break;
            }
        }
    }
}
