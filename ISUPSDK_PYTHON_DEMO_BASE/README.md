# 目录分层结构说明
## 一、 Demo操作说明

### 1.1 库文件拷贝和相关配置

<ol>
<li>登录设备网页，网络配置中设备接入模块里面配置ISUP启用和服务（isupsdk）的监听信息。</li>
<li>将开发包提供的HCAapSDKCom文件夹和动态库拷贝到项目lib目录下。如果是windows64位环境（windows32类似），则为HCISUPSDKV2.5.1.10_build20230324_Win64_ZH\lib64目录下HCAapSDKCom整个文件夹和dll后缀的动态库，如果是linux64环境（linux32类似），则对应HCISUPSDKV2.5.1.10_build20230324_Linux64_ZH\lib路径下的so库和HCAapSDKCom文件夹。
 </li>
<li>修改isupConf.json文件中的服务监听ip端口和ISUPKey等信息（登录密钥，同设备网页ISUP配置里面自定义设置的加密密钥），需要注意，所监听的端口需要空闲非占用状态</li>
<li>启动src/DemoApp/ISUPTest.py运行项目开启监听，接收来自设备的注册包，完成设备注册上线</li>
</ol>

### 1.2 ISUP服务介绍

ISUP主要有四个服务，分别为CMS，AMS，SS，SMS，分别对应SdkService下的四个服务

<ol>
<li>CMS：接收设备的注册包并进行响应，对应CmsService/CmsDemo，注册交互逻辑见CmsDemo.FRegisterCallBack </li>
<li>SS：存储服务，设备上传的事件图片会先传到ss；平台需要通过isup下发图片数据到设备的话也可以将图片上传到ss，然后再下发到设备。</li>
<li>AMS：接收事件报文，设备事件触发后，会进入AlarmDemo.AlarmMsgCallBack_Func事件回调中进行数据解析，回调中不要进行耗时业务操作和sdk接口调用。</li>
<li>SMS：预览功能，ISUP5.0对讲等业务使用</li>
</ol>

## 二、目录分层结构说明

<ol>
<li>lib：库文件路径</li>
<li>resources：资源目录，包含isupConf.json（服务器监听IP和端口的配置文件），语音转发的音频文件，透传配置操作的json报文，以及下发到设备的图片文件等</li>
<li>IsupSDKLog（Demo运行时自动生成）：ISUPSDK的日志文件路径</li>
<li>src-Common：读取配置文件等公共方法</li>
<li>DemoApp-IsupTest.py：功能测试，项目入口</li>
<li>DemoApp-SdkService：组件服务，包含ISUP各个组件的结构体方法定义和基础业务实现</li>
</ol>

### 2.1 Demo实例入口

#### 2.1.1 src/DemoApp/IsupTest.py 示例代码入口

**ISUP SDK基础框架功能示例代码 SdkFunctionDemo.py**

```html
f0001：CMS控制信令透传(需要设备在线, 获取NVR设备工作状态、远程升级等)
f0002：获取云存储配置
f0003：获取设备信息
f0004：获取设备校时参数
f0005：设置设备校时参数

f2000: 上传图片至存储服务器(返回图片URL可以用于人脸下发)
f2001：抓图

f3001: 取流预览模块(有预览窗口)
f3002: 取流预览模块(保存到本地文件)
f3003: 按时间回放模块(有回放窗口)
f3004: 按时间回放模块(保存到本地文件)
f3005: 语音转发
f3006: 云台控制
```

### 2.2 Common通用的模块定义

<ol>
<li>ConfRead.py--包含从json\xml配置文件读取key值并替换value方法 </li>
<li>glo--用dict实现的全局变量</li>  
</ol>

### 2.3 SdkService sdk相关的模块实现

**AlarmService 告警服务模块**

**HCISUPAlarm.py 管理所有的告警服务的dll接口定义**

**CmsService 注册管理模块**

**HCISUPCMS.py 管理所有的注册服务的dll接口定义**

**SsService 存储管理模块**

**HCISUPSS.py 管理所有的存储服务的dll接口定义**

**StreamService 流媒体管理模块**

**HCISUPStream.py 管理所有的流媒体服务的dll接口定义**