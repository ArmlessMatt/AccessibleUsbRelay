using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UsbRelay.Core.Entities;

namespace UsbRelay.Core.Services
{
    public class ActionService
    {
        private ComPortService portService;
        private HardwareService hardwareService;
        private SaveService saveService;
        public List<RelayAction> RelayActions;

        public ActionService()
        {
            portService = new ComPortService();
            saveService = new SaveService();
            hardwareService = new HardwareService();
            RelayActions = saveService.LoadFromSavedData();
        }

        public void AddRelay(RelayAction relay) 
        {
            RelayActions.Add(relay);
            saveService.SaveRelays(RelayActions);
        }

        public void RemoveRelay(int index)
        {
            RelayActions.RemoveAt(index);
            saveService.SaveRelays(RelayActions);
        }

        public async Task ExecuteActionAsync(string name)
        {
            RelayAction action = RelayActions.FirstOrDefault(action => action.Name == name);
            if (action == null) return;
            await Task.Run(() => RunAction(action));
        }

        private void RunAction(RelayAction action)
        {
            Thread.Sleep(action.StartDelay * 1000);
            int portNumberIndex = action.Port.IndexOf("COM") + 3;
            string portnumber = action.Port[portNumberIndex].ToString();
            portService.SendMessage(int.Parse(portnumber), hardwareService.GetTurnOnMessage(action.Type));
            if (action.AutoEnd)
            {
                Thread.Sleep(action.DurationBeforeEnd.Value * 1000);
                portService.SendMessage(int.Parse(portnumber), hardwareService.GetTurnOffMessage(action.Type));
            }
        }
    }
}
