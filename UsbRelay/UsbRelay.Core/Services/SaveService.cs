using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UsbRelay.Core.Entities;

namespace UsbRelay.Core.Services
{
    public class SaveService
    {
        public const string SAVE_PATH = @".\data\";

        public List<RelayAction> LoadFromSavedData()
        {
            var baseurl = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AccessibleUsbRelays\";
            if (!File.Exists(baseurl + "data.json"))
            {
                Directory.CreateDirectory(baseurl);
                File.Create(baseurl + "data.json").Close();
            }
            FileStream savedData = File.OpenRead(baseurl + "data.json");
            StreamReader reader = new StreamReader(savedData);
            string unparsedData = string.Empty;
            try
            {
                unparsedData = reader.ReadToEnd();
                reader.Close();
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                reader.Close();
                savedData.Close();
            }
            var data = JsonConvert.DeserializeObject<List<RelayAction>>(unparsedData);
            return data != null ? data : new List<RelayAction>();
        }

        public void SaveRelays(List<RelayAction> relays) 
        {
            var baseurl = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AccessibleUsbRelays\";

            var data = JsonConvert.SerializeObject(relays);
            if (File.Exists(baseurl + "data.json"))
            {
                File.Delete(baseurl + "data.json");
            }
            FileStream file = File.Create(baseurl + "data.json");
            file.Write(Encoding.ASCII.GetBytes(data));
            file.Close();
        }
    }
}
