namespace UsbRelay.RelayApi
{
    public static class LCUS1
    {
        public static byte[] PowerOn = { 0xA0, 0x01, 0x01, 0xA2 };
        public static byte[] PowerOff = { 0xA0, 0x01, 0x00, 0xA1 };
    }
}
