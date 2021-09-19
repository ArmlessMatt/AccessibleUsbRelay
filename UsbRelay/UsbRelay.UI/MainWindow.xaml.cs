using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UsbRelay.Core.Helpers;
using UsbRelay.Core.Services;

namespace UsbRelay.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        public MainWindow()
        {
            var args = Environment.GetCommandLineArgs().ToList();
            var shortcutArgs = args.FirstOrDefault(arg => arg.Contains("guid="));
            var shortcutFallbackArgs = args.FirstOrDefault(arg => arg.Contains("devicename="));
            if (shortcutArgs != null)
            {
                var guid = shortcutArgs.Replace("guid=", "");
                var devicename = shortcutFallbackArgs.Replace("devicename=", "");
                var service = new ActionService();
                var relay = service.RelayActions.FirstOrDefault(re => re.Guid == guid || re.DeviceName == devicename);
                if (relay != null)
                {
                    Task.Run(() => service.ExecuteActionAsync(relay.Name)).Wait();
                }
                Application.Current.Shutdown();
                return;
            }
            InitializeComponent();
            Navigator.Init(Navigate);
        }

        public void Navigate(string uri) 
        {
            var mainFrame = (Frame)FindName("mainFrame");
            mainFrame.Navigate(new Uri(uri,
                UriKind.RelativeOrAbsolute));
        }
    }
}
