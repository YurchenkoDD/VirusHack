using NAudio.CoreAudioApi;

namespace CoreLib
{
    public class DeviceManager : IDeviceManager
    {
        private static IInputDevice[] _inputDevices;
        static DeviceManager() {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            _inputDevices = new IInputDevice[devices.Count];

            for (int i = 0; i < devices.Count; i++)
            {
                _inputDevices[i] = new InputDevice(devices[i]);
            }
        }
        public IInputDevice[] InputDevices => _inputDevices;
    }
}
