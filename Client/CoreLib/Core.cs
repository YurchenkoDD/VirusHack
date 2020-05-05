using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoreLib
{
    public static class Core
    {
        //Время молчания, после которой фраза считается записанной.
        public const int TIME_PERIOD = 1000;
        //Время в течении которого проводится тестирование звука.
        public const int TIME_TO_NOISETEST = 5000;

        private static DeviceManager deviceManager = new DeviceManager();

        public static IInputDevice inputDevice;

        static Core() {
            inputDevice = deviceManager.InputDevices[0];
        }

        public static void Test() {
            inputDevice.Sensitivity = 7;
            inputDevice.StartListen();
            Thread.Sleep(50000);
        }
    }
}
