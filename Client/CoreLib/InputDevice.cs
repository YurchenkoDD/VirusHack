using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Diagnostics;

namespace CoreLib
{
    public class InputDevice : IInputDevice
    {
        private MMDevice _device;
        private WaveInEvent _waveIn = new WaveInEvent();
        private WaveFileWriter _writer;
        private bool _isRecord = false;
        private Stopwatch _silenceTimer = new Stopwatch();
        public InputDevice(MMDevice device) {
            _device = device;
            _waveIn.WaveFormat = new WaveFormat(44100, 2);
            _waveIn.DataAvailable += _waveIn_DataAvailable;
        }

        private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            var volLevel = VolumeLevel;
            if (volLevel > Sensitivity)
            {
                if (!_isRecord)
                {
                    _isRecord = true;
                    //Запись начинается
                    Console.WriteLine("*Начать*");
                    _silenceTimer.Start();
                }
                else {
                    //Запись продолжается
                    _silenceTimer.Restart();
                }
            }
            else {
                if (_isRecord && (_silenceTimer.ElapsedMilliseconds > Core.TIME_PERIOD))
                {
                    _isRecord = false;
                    _silenceTimer.Stop();
                    _silenceTimer.Reset();
                    Console.WriteLine("*Закончить*");
                }
            }
        }

        //Interface : Реализация интерфейса

        public MMDevice Device => _device;

        public float Sensitivity { get ; set; }

        public float VolumeLevel => _device.AudioMeterInformation.MasterPeakValue* 100;

        public void StartListen()
        {
            _waveIn.StartRecording();
        }

        public void StopListen()
        {
            _waveIn.StopRecording();
        }
    }
}
