using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISUPDemo
{
    class EHomeExternalCommand
    {
        
        public const uint NET_EHOME_GET_DEVICE_INFO  = 1;    //获取设备信息
        public const int NET_EHOME_GET_VERSION_INFO = 2;    //获取版本信息
        public const int NET_EHOME_GET_DEVICE_CFG = 3;    //获取设备基本参数
        public const int NET_EHOME_SET_DEVICE_CFG = 4;    //设置设备基本参数
        public const int NET_EHOME_GET_NETWORK_CFG = 5;    //获取网络参数
        public const int NET_EHOME_SET_NETWORK_CFG = 6;    //设置网络参数
//#define    NET_EHOME_GET_COMPRESSION_CFG    7    //获取压缩参数
//#define    NET_EHOME_SET_COMPRESSION_CFG    8    //设置压缩参数
//#define    NET_EHOME_GET_IMAGE_CFG            9    //获取图像参数
//#define    NET_EHOME_SET_IMAGE_CFG            10    //设置图像参数
//#define    NET_EHOME_GET_ALARMIN_CFG        11        //获取报警输入参数
//#define    NET_EHOME_SET_ALARMIN_CFG        12    //设置报警输入参数
//#define    NET_EHOME_GET_ALARM_TIME_CFG    13    //获取报警布防时间参数
//#define    NET_EHOME_SET_ALARM_TIME_CFG    14    //设置报警布防时间参数
//#define    NET_EHOME_GET_ALARMOUT_CFG        15    //获取报警输出参数
//#define    NET_EHOME_SET_ALARMOUT_CFG        16    //设置报警输出参数
//#define    NET_EHOME_GET_ALARMOUT_STATUS_CFG        17    //获取报警输出状态参数
//#define    NET_EHOME_SET_ALARMOUT_STATUS_CFG        18    //设置报警输出状态参数
//#define    NET_EHOME_MANUAL_IOOUT            19    //手动控制IO输出
    }
}
