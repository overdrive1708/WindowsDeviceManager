using Microsoft.Win32;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// インストール済みアプリケーション情報
    /// </summary>
    public record InstalledApplicationInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
    }

    /// <summary>
    /// インストール済みアプリケーション収集クラス
    /// </summary>
    internal class InstalledApplicationsCollector
    {
        /// <summary>
        /// インストール済みアプリケーション情報取得処理
        /// </summary>
        /// <returns>インストール済みアプリケーション情報</returns>
        public static List<InstalledApplicationInfo> GetInstalledApplications()
        {
            List<InstalledApplicationInfo> apps = [];

            string[] registryKeys = new string[]
            {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",                 // 32bit環境 or 64bit環境かつ64bitアプリケーション
                @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"      // 64bit環境かつ32bitアプリケーション
            };

            foreach (string key in registryKeys)
            {
                using RegistryKey uninstallKey = Registry.LocalMachine.OpenSubKey(key);
                if (uninstallKey != null)
                {
                    foreach (string subKeyName in uninstallKey.GetSubKeyNames())
                    {
                        using RegistryKey subKey = uninstallKey.OpenSubKey(subKeyName);
                        if (subKey != null)
                        {
                            InstalledApplicationInfo appInfo = new()
                            {
                                Name = subKey.GetValue("DisplayName") as string ?? string.Empty,
                                Version = subKey.GetValue("DisplayVersion") as string ?? string.Empty,
                                Publisher = subKey.GetValue("Publisher") as string ?? string.Empty
                            };

                            if (!string.IsNullOrEmpty(appInfo.Name) && !apps.Contains(appInfo))
                            {
                                apps.Add(appInfo);
                            }
                        }
                    }
                }
            }

            return apps;
        }
    }
}
