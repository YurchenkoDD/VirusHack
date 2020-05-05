using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoreLib
{
    public class SoundInputDevice
    {
        MMDevice _device;

        private WaveInEvent _waveIn = new WaveInEvent();
        private WaveFileWriter _writer;

        //Чувствительность микрофона
        public float MicrophoneSens { get; set; }

        public SoundInputDevice(MMDevice device) {
            _device = device;
            _waveIn.WaveFormat = new WaveFormat(44100, 2);
            _waveIn.DataAvailable += _waveIn_DataAvailable;
        }

        //Public: Публичные методы

        /// <summary>
        /// Тест чувствительности микрофона.
        /// </summary>
        public void SensitivityTest() {
            _waveIn.DataAvailable += Sensitivity_Tick;
            MicrophoneSens = 0;

            _waveIn.StartRecording();

            Thread.Sleep(Core.TIME_TO_NOISETEST);

            _waveIn.StopRecording();

            _waveIn.DataAvailable -= Sensitivity_Tick;
        }

        public float VolumeLevel() { 
            return _device.AudioMeterInformation.MasterPeakValue * 100;
        }

        public void StartRecord(string filename) {
            _waveIn.StartRecording();
        }

        public void StopRecord()
        {
            _waveIn.StopRecording();
        }


        //Private : Приватные методы
        private void Sensitivity_Tick(object sender, WaveInEventArgs e)
        {
            var volLevel = VolumeLevel();
            if (volLevel > MicrophoneSens) {
                MicrophoneSens = volLevel;
            }
        }

        private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            throw new NotImplementedException();
        }

        //private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        //{
        //    var volLevel = VolumeLevel();
        //    if (volLevel > MicrophoneSens)
        //    {
        //        if (isRecord)
        //        {
        //            stopwatch.Restart();
        //        }
        //        else {
        //            isRecord = true;
        //            Console.WriteLine("*начало*");
        //        }
        //    }
        //    else {
        //        if (isRecord)
        //        {
        //            if (stopwatch.ElapsedMilliseconds > Core.TIME_PERIOD)
        //            {
        //                stopwatch.Stop();
        //                isRecord = false;
        //                Console.WriteLine("*конец*");
        //                Console.WriteLine(stopwatch.ElapsedMilliseconds);
        //            }
        //        }
        //        else
        //        {
        //            stopwatch.Reset();
        //            stopwatch.Start();
        //        }
        //    }
    }
    }
