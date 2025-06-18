package DemoApp;

import DevModule.Acs.*;

import java.io.UnsupportedEncodingException;

/**
 * @author zhengxiaohui
 * @date 2023/8/16 18:42
 * @desc 门禁系统相关的demo代码汇总
 */
public class AcsFunctionDemo {

    public static void dispatch(String command, int lLoginID) throws UnsupportedEncodingException {
        switch (command) {
            case "10001": {
                System.out.println("\n[Function]下发人员工号信息");
                AcsUserManagement.addEmployeeInfo(lLoginID);
                break;
            }
            case "10002": {
                System.out.println("\n[Function]删除人员工号信息");
                AcsUserManagement.deleteEmployeeInfo(lLoginID);
                break;
            }
            case "10003": {
                System.out.println("\n[Function]查询人员工号信息");
                AcsUserManagement.searchEmployeeInfo(lLoginID);
                break;
            }
            case "10004": {
                System.out.println("\n[Function]查询卡号信息");
                AcsCardManagement.searchCardInfo(lLoginID, "test001");
                break;
            }
            case "10011": {
                System.out.println("\n[Function]下发人脸图片数据");
                AcsFaceManagement.addFacePicInfo(lLoginID, "test001", "http://10.19.37.220:6011/pic?AE96D0B5814A0713D2B1C20291CFE6ACFDLib.jpg");
                break;
            }
            case "10012": {
                System.out.println("\n[Function]修改人脸图片数据");
                AcsFaceManagement.modifyFacePicInfo(lLoginID, "test001", "http://10.19.37.220:6011/pic?3505F0234A52A3B9E803A096EFC1F4FDFDLibNew.jpg");
                break;
            }
            case "10013": {
                System.out.println("\n[Function]删除人脸图片数据");
                AcsFaceManagement.deleteFacePicInfo(lLoginID, "test001");
                break;
            }
            case "10014": {
                System.out.println("\n[Function]查询人脸图片数据");
                AcsFaceManagement.searchFacePicInfo(lLoginID, "test001");
                break;
            }
            case "10021": {
                System.out.println("\n[Function]获取门禁参数");
                AcsParamCfg.getAcsCfg(lLoginID);
                break;
            }
            case "10022": {
                System.out.println("\n[Function]设置门禁参数");
                AcsParamCfg.setAcsCfg(lLoginID);
                break;
            }
            case "10023": {
                System.out.println("\n[Function]获取门禁主机状态");
                AcsParamCfg.getAcsStatus(lLoginID);
                break;
            }
            case "10024": {
                System.out.println("\n[Function]远程控门");
                AcsParamCfg.remoteControDoor(lLoginID);
                break;
            }
            case "10025": {
                System.out.println("\n[Function]查询门禁历史事件");
                AcsSearchEvent.searchAcsEventInfo(lLoginID);
                break;
            }
            case "10026": {
                System.out.println("\n[Function]查询门禁历史事件总条数");
                AcsSearchEvent.SearchAcsEventTotalNum(lLoginID);
                break;
            }
            case "10031":
                System.out.println("\n[Function]下发门禁指纹数据");
                AcsFingerPrintManagement.addFingerPrintInfo(lLoginID, "test001");
                break;
            case "10032": {
                System.out.println("\n[Function]查询门禁指纹数据");
                AcsFingerPrintManagement.searchFingerPrintInfo(lLoginID, "test001");
                break;
            }
            case "10033": {
                System.out.println("\n[Function]删除门禁指纹数据");
                AcsFingerPrintManagement.deleteFingerPrintInfo(lLoginID, "test001");
                break;
            }
            case "10034":{
                System.out.println("\n[Function]获取人员计划模板参数");
                AcsUserRightPlanTemplate.getUserRightPlanTemplateParm(lLoginID,1);
                break;
            }
            case "10035":{
                System.out.println("\n[Function]获取人员周计划模板参数");
                AcsUserRightPlanTemplate.getUserRightWeekPlanCfg(lLoginID,2);
                break;
            }
            case "10036":{
                System.out.println("\n[Function]设置人员计划周模板参数");
                AcsUserRightPlanTemplate.setUserRightWeekPlanCfg(lLoginID,2);
                break;
            }
            case "10037":{
                System.out.println("\n[Function]设置人员计划模板参数");
                AcsUserRightPlanTemplate.setUserRightPlanTemplateParm(lLoginID,2);
                break;
            }
        }
    }
}
