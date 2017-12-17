using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using StickDownWindows.Services;

namespace StickDownWindows
{
    public static class WindowsResources
    {
        public static bool AppKeyToRegistry(bool delete = false)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (registryKey == null) return false;

            if (!delete)
            {
                registryKey.SetValue(AppInfo.ApplicationName, Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + @"\Release\WindowsApplication.exe"); // TODO
            }
            else
            {
                registryKey.DeleteValue(AppInfo.ApplicationName);
            }

            registryKey.Close();
            return true;
        }

        public static bool AddPathToRegistry(string syncFolderPath, bool delete = false)
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("StickDown_Info");
            if (registryKey == null) return false;

            if (!delete)
            {
                registryKey.SetValue("SyncPath", syncFolderPath);
            }
            else
            {
                registryKey.DeleteValue("SyncPath");
            }
            registryKey.Close();
            return true;
        }

        public static string GetSyncFolderFromRegistry()
        {
            var registryStickDown = Registry.CurrentUser.OpenSubKey("StickDown_Info");
            var registryPath = registryStickDown?.GetValue("SyncPath");
            return registryPath?.ToString();
        }
    }
}
