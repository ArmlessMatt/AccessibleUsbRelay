using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UsbRelayInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var progress = (ProgressBar)FindName("progress");
            progress.Value = 10;
            var installer = new Installer();
            installer.Install();
            installer.CreateDesktopShortcut();
            FinishProgress();
        }

        private async void FinishProgress() 
        {
            var progressBar = (ProgressBar)FindName("progress");

            var progress = new Progress<int>(value => progressBar.Value = value);
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i+=5)
                {
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(150);
                }
            });
        }

        private void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var progressBar = (ProgressBar)FindName("progress");
            if (progress.Value == 100)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
