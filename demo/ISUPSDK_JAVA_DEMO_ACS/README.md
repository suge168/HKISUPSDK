# 一、Demo操作说明

### 1.库文件拷贝和相关配置

<ol>
<li>登录设备网页，网络配置中设备接入模块里面配置ISUP启用和服务（isupsdk）的监听信息。</li>
<li>将开发包提供的HCAapSDKCom文件夹和动态库拷贝到项目lib目录下。如果是windows64位环境（windows32类似），则为HCISUPSDKV2.5.1.10_build20230324_Win64_ZH\lib64目录下HCAapSDKCom整个文件夹和dll后缀的动态库，如果是linux64环境（linux32类似），则对应HCISUPSDKV2.5.1.10_build20230324_Linux64_ZH\lib路径下的so库和HCAapSDKCom文件夹。
 </li>
<li>修改config.properties文件中的服务监听ip端口和ISUPKey等信息（登录密钥，同设备网页ISUP配置里面自定义设置的加密密钥），需要注意，所监听的端口需要空闲非占用状态</li>
<li>启动DemoApp/IsupTest.main()运行项目开启监听，接收来自设备的注册包，完成设备注册上线</li>
</ol>

### 2.ISUP服务介绍

ISUP主要有四个服务，分别为CMS，AMS，SS，SMS，分别对应src/SdkService下的四个服务

<ol>
<li>cms：接收设备的注册包并进行响应，对应CmsService/CmsDemo，注册交互逻辑见CmsDemo.FRegisterCallBack </li>
<li>ss：存储服务，设备上传的事件图片会先传到ss；平台需要通过isup下发图片数据到设备的话也可以将图片上传到ss，然后再下发到设备。</li>
<li>ams：接收事件报文，设备事件触发后，会进入AlarmDemo.EHomeMsgCallBack事件回调中进行数据解析，回调中不要进行耗时业务操作和sdk接口调用。</li>
<li>sms：预览功能，ISUP5.0对讲等业务使用</li>
</ol>

# 二、目录分层结构说明

<ol>
<li>lib：库文件路径</li>
<li>resources：资源目录，包括语音转发的音频文件，透传配置操作的json报文，以及下发到设备的图片文件等</li>
<li>EhomeSDKLog：ISUPSDK的日志文件路径</li>
<li>Common：读取配置文件等公共方法</li>
<li>DemoApp：功能测试，项目入口，其中IsupTest.main()为程序启动类</li>
<li>DevModule：按设备类型的定义实现</li>
<li>SdkService：组件服务，包含ISUP各个组件的结构体方法定义和基础业务实现</li>
<li>UIModule：UI界面的模块，保留</li>
</ol>


## 2.1、 DemoApp demo实例入口
### 2.1.1 DemoApp/IsupTest.main() 示例代码入口
#### ISUP SDK基础框架功能示例代码-SdkFunctionDemo

<ol>
<li>上传图片至存储服务器(返回图片URL可以用于人脸下发)--SdkFunctionDemo.dispatch(case "f2000") </li>
<li>取流预览模块(有预览窗口)--SdkFunctionDemo.dispatch(case "f3001") </li>
<li>取流预览模块(保存到本地文件)--SdkFunctionDemo.dispatch(case "f3002") </li>
<li>按时间回放模块(有回放窗口)--SdkFunctionDemo.dispatch(case "f3003") </li>
<li>按时间回放模块(保存到本地文件) --SdkFunctionDemo.dispatch(case "f3004") </li>
<li>语音转发示例 --SdkFunctionDemo.dispatch(case "f3005") </li>
</ol>

#### 2.2.2 DevModule 门禁产品类型的定义实现-AcsFunctionDemo

<ol>
<li>下发人员工号信息--AcsFunctionDemo.dispatch(case "10001") </li>
<li>删除人员工号信息--AcsFunctionDemo.dispatch(case "10002") </li>
<li>查询人员工号信息--AcsFunctionDemo.dispatch(case "10003") </li>
<li>查询卡号信息 --AcsFunctionDemo.dispatch(case "10004") </li>
<li>下发人脸图片数据 --AcsFunctionDemo.dispatch(case "10011") </li>
<li>修改人脸图片数据 --AcsFunctionDemo.dispatch(case "10012") </li>
<li>删除人脸图片数据 --AcsFunctionDemo.dispatch(case "10013") </li>
<li>查询人脸图片数据 --AcsFunctionDemo.dispatch(case "10014") </li>
<li>获取门禁参数 --AcsFunctionDemo.dispatch(case "10021") </li>
<li>设置门禁参数 --AcsFunctionDemo.dispatch(case "10022") </li>
<li>获取门禁主机状态 --AcsFunctionDemo.dispatch(case "10023") </li>
<li>远程控门 --AcsFunctionDemo.dispatch(case "10024") </li>
<li>查询门禁历史事件 --AcsFunctionDemo.dispatch(case "10025") </li>
<li>查询门禁历史事件总条数 --AcsFunctionDemo.dispatch(case "10026") </li>
<li>下发门禁指纹数据 --AcsFunctionDemo.dispatch(case "10031") </li>
<li>查询门禁指纹数据 --AcsFunctionDemo.dispatch(case "10032") </li>
<li>删除门禁指纹数据 --AcsFunctionDemo.dispatch(case "10033") </li>
<li>获取人员计划模板参数 --AcsFunctionDemo.dispatch(case "10034") </li>
<li>获取人员周计划模板参数 --AcsFunctionDemo.dispatch(case "10035") </li>
<li>设置人员计划周模板参数 --AcsFunctionDemo.dispatch(case "10036") </li>
<li>设置人员计划模板参数 --AcsFunctionDemo.dispatch(case "10037") </li>
</ol>


## 2.2 resources 文件说明

#### 1）audioFile 存放本地下发送给设备的音频文件
#### 2）conf 存放示例代码中业务报文信息
> 其中 ****-desc.xml文件为业务报文的字段解释，req表示字段必填，req表示字段非必填，可以根据您的实际业务需求自行删减组件请求报文内容！！！
eg：SearchFCByImage-desc.xml为查询告警事件的完整请求报文字段说明
#### 3）pics 存放发送给图片存储服务的演示图片数据



## 2.3 Acs 门禁系统相关的功能点demo演示
目前Demo实现门禁人员管理（工号，卡号和人脸）、门禁事件、门禁基础配置、事件查询和人员权限模板功能

    # AcsUserManagement.java   门禁人员管理
    # AcsCardManagement.java  门禁卡号管理
    # AcsFaceManagement.java 门禁人脸管理
    # AcsFingerPrintManagement.java 门禁指纹管理
    # AcsParamCfg.java 门禁参数配置+远程控门
    # AcsSearchEvent.java 门禁事件查询
    # AcsUserRightPlanTemplate.java 门禁人员计划模板配置
    # 门禁事件实时接收，参考ISUP通用的报警服务模块接口



## 2.4 Common 通用的模块定义

## 2.5 SdkService sdk相关的模块实现

### 2.5.1 AlarmService 告警服务模块
HCISUPAlarm.java 管理所有的告警服务的dll接口定义

CmsService 注册管理模块

### 2.5.2 HCISUPCMS.java 管理所有的注册服务的dll接口定义

SsService 存储管理模块

HCISUPSS.java 管理所有的存储服务的dll接口定义

### 2.5.3 StreamService 流处理模块
HCISUPStream.java 管理所有的流处理的dll接口定义

## 2.6 UIModule UI界面的模块定义