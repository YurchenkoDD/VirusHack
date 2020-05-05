using System;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using System.Threading;
using CoreLib;

namespace ConsoleApp
{
    class Program
    {
        //private static WaveInEvent waveIn;
        //public static WaveFileWriter writer;
        static void Main(string[] args)
        {
            //waveIn = new WaveInEvent();
            //waveIn.DeviceNumber = 0;
            //waveIn.DataAvailable += WaveIn_DataAvailable;
            //waveIn.WaveFormat = new WaveFormat(44100, 2);
            //waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(WaveIn_RecordingStopped);
            //writer = new WaveFileWriter(@"C:\Users\User1\Desktop\this.wav", waveIn.WaveFormat);
            //waveIn.StartRecording();
            //Thread.Sleep(5000);
            //waveIn.StopRecording();
            //Thread.Sleep(2000);
            //Core.inputDevice.SensitivityTest();
            Core.Test();
        }

        //private static void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        //{
        //    writer.Close();
        //}

        //private static void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        //{
        //    writer.Write(e.Buffer, 0, e.BytesRecorded);
        //    for (int index = 0; index < e.BytesRecorded; index += 2)
        //    {
        //        short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);

        //        float amplitude = sample / 32768f;
        //        float level = Math.Abs(amplitude); // от 0 до 1
        //        if (level > 0.5)
        //        {
        //            Console.WriteLine("Уровень: {0}%.", level * 100);
        //        }
        //    }
        //}
    }
}
