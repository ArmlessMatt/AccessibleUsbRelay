using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Reflection;

namespace UsbRelayInstaller
{
    public class Installer
    {
        public void Install() 
        {
            string installPath = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%") + @"\AccessibleUsbRelays";
            var fileToCopyPath = Path.GetDirectoryName(AppContext.BaseDirectory) + @"\AccessibleUsbRelays";
            DirectoryCopy(fileToCopyPath, installPath, true);
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public void CreateDesktopShortcut() 
        {
            string exePath = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%") + @"\AccessibleUsbRelays\UsbRelay.UI.exe";
            CreateShortcut("Usb Relay Configurator", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), exePath);
        }

        private void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.TargetPath = targetFileLocation;
            shortcut.Save();
        }
    }
}
