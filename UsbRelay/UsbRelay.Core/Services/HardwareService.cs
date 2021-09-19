using UsbRelay.RelayApi;

namespace UsbRelay.Core.Services
{
    public class HardwareService
    {
        public byte[] GetTurnOnMessage(string relayType) 
        {
            switch (relayType)
            {
                case "LCUS1":
                default:
                    return LCUS1.PowerOn;
            }
        }

        public byte[] GetTurnOffMessage(string relayType)
        {
            switch (relayType)
            {
                case "LCUS1":
                default:
                    return LCUS1.PowerOff;
            }
        }
    }
}
