namespace UsbRelay.Core.Entities
{
    public class RelayAction
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string DeviceName { get; set; }
        public string Port { get; set; }
        public string Type { get; set; }
        public int StartDelay { get; set; }
        public bool AutoEnd { get; set; }
        public int? DurationBeforeEnd { get; set; }
    }
}
