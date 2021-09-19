using System.IO.Ports;

namespace UsbRelay.Core.Services
{
    public class ComPortService
    {
        public void SendMessage(int portNumber, byte[] data) 
        {
            SerialPort port = new SerialPort("COM" + portNumber.ToString(), 9600, Parity.None, 8, StopBits.One);
            //open the port
            port.Open();

            try
            {
                // start the communication
                port.Write(data, 0, data.Length);
            }
            finally 
            {
                port.Close();
            }
        }
    }
}
