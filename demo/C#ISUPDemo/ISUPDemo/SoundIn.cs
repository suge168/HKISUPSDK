using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using ISUPDemo.Public;

namespace ISUPDemo
{
    class SoundIn
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
        #endregion



        #region "waveIn dll"
        [DllImport("winmm.dll")]
        public static extern int waveInGetNumDevs();
        [DllImport("winmm.dll")]
        public static extern int waveInAddBuffer(IntPtr hwi, ref WAVEHDR pwh, int cbwh);
        [DllImport("winmm.dll")]
        public static extern int waveInClose(IntPtr hwi);
        [DllImport("winmm.dll")]
        public static extern int waveInOpen(out IntPtr phwi, int uDeviceID, WAVEFORMAT lpFormat, IntPtr dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm.dll")]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveInReset(IntPtr hwi);
        [DllImport("winmm.dll")]
        public static extern int waveInStart(IntPtr hwi);
        [DllImport("winmm.dll")]
        public static extern int waveInStop(IntPtr hwi);
        #endregion
    }
}
