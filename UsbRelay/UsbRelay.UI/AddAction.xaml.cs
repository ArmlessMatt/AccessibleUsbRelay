using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Controls;
using UsbRelay.Core.Entities;
using UsbRelay.Core.Helpers;
using UsbRelay.Core.Services;

namespace UsbRelay.UI
{
    /// <summary>
    /// Interaction logic for AddAction.xaml
    /// </summary>
    public partial class AddAction : Page
    {
        private ActionService actionService;
        public AddAction()
        {
            InitializeComponent();

            LoadPortList();
            actionService = new ActionService();

            var typerelais = (ComboBox)FindName("lstTypeRelais");
            typerelais.Items.Add("LCUS1");
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigator.Navigate("ActionList.xaml");
        }

        private void LoadPortList() 
        {
            var list = (ComboBox)FindName("lstPort");
            foreach (var port in DeviceManagerHelper.GetComPortsName())
            {
                list.Items.Add(port);
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var txtbDuration = (TextBox)FindName("txtboxDuration");
            txtbDuration.IsEnabled = true;
        }

        private void chkbAutoEnd_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var txtbDuration = (TextBox)FindName("txtboxDuration");
            txtbDuration.IsEnabled = false;
        }

        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var name = (TextBox)FindName("txtboxName");
            var ports = (ComboBox)FindName("lstPort");
            var types = (ComboBox)FindName("lstTypeRelais");
            var startDelay = (TextBox)FindName("txtboxStartDelay");
            var autoEnd = (CheckBox)FindName("chkbAutoEnd");
            var txtbDuration = (TextBox)FindName("txtboxDuration");

            RelayAction relay = new RelayAction
            {
                Name = name.Text,
                Port = ports.SelectedItem.ToString(),
                Type = types.SelectedItem.ToString(),
                AutoEnd = autoEnd.IsChecked.Value,
                DurationBeforeEnd = int.Parse(txtbDuration.Text),
                StartDelay = int.Parse(startDelay.Text),
                Guid = Guid.NewGuid().ToString()
            };
            relay.DeviceName = relay.Port.Remove(relay.Port.LastIndexOf(" "), relay.Port.Length - relay.Port.LastIndexOf(" ")).Replace(" ", "");

            actionService.AddRelay(relay);
            Navigator.Navigate("ActionList.xaml");
        }
    }
}
