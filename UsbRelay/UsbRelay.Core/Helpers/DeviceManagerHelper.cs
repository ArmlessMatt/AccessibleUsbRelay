using System.Collections.Generic;
using System.Management;

namespace UsbRelay.Core.Helpers
{
    public class DeviceManagerHelper
    {
        public static List<string> GetComPortsName() 
        {
            List<string> ports = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "root\\CIMV2",
                "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""
            );

            foreach (ManagementObject portQueryObj in searcher.Get())
            {
                ports.Add(portQueryObj.GetPropertyValue("Name").ToString());
            }
            return ports;
        } 
    }
}
