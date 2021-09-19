using IWshRuntimeLibrary;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UsbRelay.Core.Helpers;
using UsbRelay.Core.Services;

namespace UsbRelay.UI
{
    /// <summary>
    /// Interaction logic for ActionList.xaml
    /// </summary>
    public partial class ActionList : Page
    {
        private ActionService actionService = new ActionService();

        public ActionList()
        {
            InitializeComponent();

            LoadRelaysToGrid();
        }

        private void addAction_Click(object sender, RoutedEventArgs e) => Navigator.Navigate("AddAction.xaml");

        private async Task runAction_ClickAsync(object sender, RoutedEventArgs e)
        {
            await actionService.ExecuteActionAsync("Test COM3");
        }

        private void deleteAction_Click(object sender, RoutedEventArgs e)
        {
            var grid = (Grid)FindName("relayGrid");
            var buttons = LogicalTreeHelper.GetChildren(grid).OfType<RadioButton>();
            var selected = buttons.FirstOrDefault(button => button.IsChecked == true);
            if (selected == null)
            {
                return;
            }
            var indexToRemove = int.Parse(selected.Name.Remove(0, selected.Name.Length -1));
            actionService.RemoveRelay(indexToRemove);
            EmptyGrid();
            LoadRelaysToGrid();
        }

        private void shortcutButton_Click(object sender, RoutedEventArgs e)
        {
            var grid = (Grid)FindName("relayGrid");
            var buttons = LogicalTreeHelper.GetChildren(grid).OfType<RadioButton>();
            var selected = buttons.FirstOrDefault(button => button.IsChecked == true);
            if (selected == null)
            {
                return;
            }
            var index = int.Parse(selected.Name.Remove(0, selected.Name.Length - 1));
            var relay = actionService.RelayActions[index];
            CreateShortcut("Lancer relais " + relay.Name , Environment.GetFolderPath(Environment.SpecialFolder.Desktop), AppContext.BaseDirectory + "UsbRelay.UI.exe", relay.Guid, relay.DeviceName);
        }

        private void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string guid, string deviceName)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Arguments = "guid=" + guid + " " + "devicename=" + deviceName;
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }

        private void EmptyGrid()
        {
            var grid = (Grid)FindName("relayGrid");
            var labels = LogicalTreeHelper.GetChildren(grid).OfType<Label>().ToList();
            var radios = LogicalTreeHelper.GetChildren(grid).OfType<RadioButton>().ToList();
            foreach (var label in labels)
            {
                grid.Children.Remove(label);
            }
            foreach (var radio in radios)
            {
                grid.Children.Remove(radio);
            }
        }

        private void LoadRelaysToGrid()
        {
            foreach (var relays in actionService.RelayActions)
            {
                var grid = (Grid)FindName("relayGrid");
                var index = actionService.RelayActions.IndexOf(relays);
                var radiobutton = new RadioButton();
                radiobutton.GroupName = "list";
                radiobutton.HorizontalAlignment = HorizontalAlignment.Left;
                radiobutton.VerticalAlignment = VerticalAlignment.Center;
                radiobutton.Name = "radio" + index;
                radiobutton.Margin = new Thickness(5, 2, 0, 0);
                radiobutton.Checked += EnableActionButtons;
                Grid.SetRow(radiobutton, index);
                Grid.SetColumn(radiobutton, 0);
                var label = new Label();
                label.Name = "lbl" + index;
                label.Content = relays.Name;
                label.FontSize = 16;
                Grid.SetRow(label, index);
                Grid.SetColumn(label, 0);
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                var label2 = new Label();
                label2.Name = "lblrelay" + index;
                label2.Content = relays.Port + ", Type : " + relays.Type;
                label2.FontSize = 16;
                Grid.SetRow(label2, index);
                Grid.SetColumn(label2, 1);
                Grid.SetColumnSpan(label2, 2);
                label2.HorizontalAlignment = HorizontalAlignment.Center;
                label2.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(radiobutton);
                grid.Children.Add(label);
                grid.Children.Add(label2);
            }
        }

        private void EnableActionButtons(object sender, RoutedEventArgs e)
        {
            var delete = (Button)FindName("deleteAction");
            delete.IsEnabled = true;
            var shortcut = (Button)FindName("shortcutButton");
            shortcut.IsEnabled = true;
        }
    }
}
