using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickDownWindows.Services
{
    public static class AppInfo
    {
        public static string UriApp = @"https://stickdown.azurewebsites.net";

        public static string ApplicationName = "StickDown";
        public static string AppDataFolderPath => 
            Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), ApplicationName);

        public static string DefaultSyncFolderPath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ApplicationName);

        public static string DatabeFileName = "stickdowndatabase.db";
        public static string AccountInfoFileName = "accountinfo";

        public static string LocalSyncFolderPath = DefaultSyncFolderPath;
    }
}
