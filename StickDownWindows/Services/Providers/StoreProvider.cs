using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace StickDownWindows.Services.Providers
{
    public class StoreProvider
    {
        public MobileServiceSQLiteStore MobileServiceSqLiteStore;
        public string LocalDatabaseFilePath => Path.Combine(AppInfo.AppDataFolderPath, AppInfo.DatabeFileName);

        public StoreProvider()
        {
            if (File.Exists(LocalDatabaseFilePath))
                File.Create(LocalDatabaseFilePath);
        }
    }
}
