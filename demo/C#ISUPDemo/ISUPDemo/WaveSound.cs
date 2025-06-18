using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using ISUPDemo.Public;
namespace ISUPDemo
{
    class WaveSound
    {
        #region "SoundIn struct"
        [StructLayoutAttribute(LayoutKind.Sequential)]
        /* wave data block header */
        public struct WAVEHDR {
            String     lpData;                 /* pointer to locked data buffer */
            uint       dwBufferLength;         /* length of data buffer */
            uint       dwBytesRecorded;        /* used for input only */
            uint       dwUser;                 /* for client's use */
            uint       dwFlags;                /* assorted flags (see defines) */
            uint       dwLoops;                /* loop control counter */
            //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.U1)]
            WAVEHDR[] lpNext;                  /* reserved for driver */
            uint   reserved;                   /* reserved for driver */
        } 

        [StructLayoutAttribute(LayoutKind.Sequential)]
        /* OLD general waveform format structure (information common to all formats) */
        public struct WAVEFORMAT
        {
            UInt16    wFormatTag;        /* format type */
            UInt16    nChannels;         /* number of channels (i.e. mono, stereo, etc.) */
            uint      nSamplesPerSec;    /* sample rate */
            uint      nAvgBytesPerSec;   /* for buffer estimation */
            UInt16    nBlockAlign;       /* block size of data */
        } 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WAVEFORMATE
        {
            UInt16     wFormatTag;         /* format type */
            UInt16     nChannels;          /* number of channels (i.e. mono, stereo...) */
            uint       nSamplesPerSec;        /* sample rate */
            uint       nAvgBytesPerSec;       /* for buffer estimation */
            UInt16     nBlockAlign;        /* block size of data */
            UInt16     wBitsPerSample;     /* number of bits per sample of mono data */
            UInt16     cbSize;             /* the count in bytes of the size of */
                                              /* extra information (after cbSize) */
        } 
        #endregion





        #region "wave put dll"
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutClose(IntPtr hwo);
        //检取由指定的错误代码标识的文本说明
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetErrorText(uint mmrError, StringBuilder pszText, uint cchText);
        //检取系统中存在的波形输出设备的数量
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint waveOutGetNumDevs();
        //查询一个波形输出设备当前播放的速度
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetPlaybackRate(IntPtr hwo, uint dwRate);
        // 查询指定波形输出设备的当前音量设置
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetVolume(IntPtr hwo, uint dwVolume);
        //为播放打开一个波形输出设备
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutOpen(ref IntPtr hWaveOut, IntPtr uDeviceID, ref WAVEFORMATE lpFormat, IntPtr dwCallback, IntPtr dwInstance, uint dwFlags);
        //暂停指定波形输出设备上的播放
        [DllImport("winmm.dll")]
        public static extern uint waveOutPause(IntPtr hwo);
        //为播放准备一个波形缓冲区
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutPrepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        //停止给定的波形输出设备的输出，且将当前位置清零
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutReset(IntPtr hwo);
        //设置指定波形输出设备的速度
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutSetPlaybackRate(IntPtr hwo, uint dwRate);
        //设置指定的波形输出设备的音量
        [DllImport("winmm.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int waveOutSetVolume(int uDeviceID, int dwVolume);
        //清除由waveOutPrepareHeader函数实现的准备
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutUnprepareHeader(IntPtr hwo, ref WAVEHDR pwh, uint cbwh);
        //向指定的波形输出设备发送一个数据块
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutWrite(IntPtr hwo, ref WAVEHDR pwh, uint cbwh);

        #endregion

    }
}
