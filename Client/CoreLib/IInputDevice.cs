using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Text;

namespace CoreLib
{
    public interface IInputDevice
    {
        MMDevice Device { get; }
        float Sensitivity { get; set; }
        float VolumeLevel { get; }
        void StartListen();
        void StopListen();
    }
}
