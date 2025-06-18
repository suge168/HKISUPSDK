using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;


using EHomeDemo.Public;
namespace EHomeDemo
{
    public class AlarmListenCB
    {

        //日志实例
        private DeviceLogList g_formList = DeviceLogList.Instance();


        public void ProcessAlarmData(uint dwAlarmType, IntPtr pStru, uint dwStruLen, IntPtr pXml, uint dwXmlLen)
        {
            switch (dwAlarmType)
            {
                case HCEHomeAlarm.EHOME_ALARM:
                    ProcessEhomeAlarm(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_HEATMAP_REPORT:
                    //ProcessEhomeHeatMapReport(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_FACESNAP_REPORT:
                    //ProcessEhomeFaceSnapReport(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_GPS:
                    ProcessEhomeGps(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_CID_REPORT:
                    ProcessEhomeCid(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_NOTICE_PICURL:
                    //ProcessEhomeNoticePicUrl(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                case HCEHomeAlarm.EHOME_ALARM_NOTIFY_FAIL:
                    // ProcessEhomeNotifyFail(pStru, dwStruLen, pXml, dwXmlLen);
                    break;
                default:
                    g_formList.AddLog(-1, HCEHomePublic.ALARM_INFO_T, 0, "[Unknown_Alarm]");
                    break;
            }
        }

        void ProcessEhomeCid(IntPtr pStru, uint dwStruLen, IntPtr pXml, uint dwXmlLen)
        {
            HCEHomeAlarm.NET_EHOME_CID_INFO struCIDInfo = new HCEHomeAlarm.NET_EHOME_CID_INFO();
            struCIDInfo = (HCEHomeAlarm.NET_EHOME_CID_INFO)Marshal.PtrToStructure(pStru, typeof(HCEHomeAlarm.NET_EHOME_CID_INFO));
            string str;
            str=string.Format(@"[CID]DeviceID:{0},Code:{1},Type:{2},SubSysNo:{3},Describe:{4},byTriggerTime:{5},
            UploadTime:{6},UserType:{7},ZoneNo:{8},KeyboardNo:{9},VideoChanNo:{10},DiskNo:{11},
            ModuleAddr:{12},UserName:{13}", struCIDInfo.byDeviceID,struCIDInfo.dwCIDCode,struCIDInfo.dwCIDType,
            struCIDInfo.dwSubSysNo, struCIDInfo.byCIDDescribe, struCIDInfo.byTriggerTime, struCIDInfo.byUploadTime,
            struCIDInfo.struCIDParam.dwUserType, struCIDInfo.struCIDParam.lZoneNo, struCIDInfo.struCIDParam.lKeyboardNo,
            struCIDInfo.struCIDParam.lVideoChanNo, struCIDInfo.struCIDParam.lDiskNo, struCIDInfo.struCIDParam.lModuleAddr,
            struCIDInfo.struCIDParam.byUserName);
            g_formList.AddLog(-1, HCEHomePublic.ALARM_INFO_T, 0, str);
        }

        public void ProcessEhomeAlarm(IntPtr pStru, uint dwStruLen, IntPtr pXml, uint dwXmlLen)
        {

            HCEHomeAlarm.NET_EHOME_ALARM_INFO struAlarmInfo = new HCEHomeAlarm.NET_EHOME_ALARM_INFO();
            struAlarmInfo = (HCEHomeAlarm.NET_EHOME_ALARM_INFO)Marshal.PtrToStructure(pStru, typeof(HCEHomeAlarm.NET_EHOME_ALARM_INFO));
            
            String str;
            str = string.Format("[ALARM]DeviceID:{0},Time:{1},Type:{2},Action:{3},Channel:{4},AlarmIn:{5},DiskNo:{6}", struAlarmInfo.szDeviceID,
                struAlarmInfo.szAlarmTime, struAlarmInfo.dwAlarmType, struAlarmInfo.dwAlarmAction, struAlarmInfo.dwVideoChannel,
                struAlarmInfo.dwAlarmInChannel, struAlarmInfo.dwDiskNumber);
            g_formList.AddLog(-1, HCEHomePublic.ALARM_INFO_T, 0, str);
        }

        public void ProcessEhomeGps(IntPtr pStru, uint dwStruLen, IntPtr pXml, uint dwXmlLen)
        {
            HCEHomeAlarm.NET_EHOME_GPS_INFO struGps/* = new HCEHomeAlarm.NET_EHOME_GPS_INFO()*/;
            /*struGps.Init();*/
            struGps = (HCEHomeAlarm.NET_EHOME_GPS_INFO)Marshal.PtrToStructure(pStru, typeof(HCEHomeAlarm.NET_EHOME_GPS_INFO));
            String str;
            str = string.Format("[GPS]DeviceID:{0},SampleTime:{1},Division:[{2} {3}],Satelites:{4},Precision:{5},Longitude:{6},Latitude:{7},Direction:{8},Speed:{9},Height:{10}",
              struGps.byDeviceID, struGps.bySampleTime, struGps.byDivision[0], struGps.byDivision[1], struGps.bySatelites, struGps.byPrecision, struGps.dwLongitude,
              struGps.dwLatitude, struGps.dwDirection, struGps.dwSpeed, struGps.dwHeight);
            g_formList.AddLog(-1, HCEHomePublic.ALARM_INFO_T, 0, str);
        }




    }
}
