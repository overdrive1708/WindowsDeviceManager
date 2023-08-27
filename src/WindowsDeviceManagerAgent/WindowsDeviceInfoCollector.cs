using System.Management;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// Windowsデバイス情報収集クラス
    /// </summary>
    internal class WindowsDeviceInfoCollector
    {
        /// <summary>
        /// Windowsデバイス情報収集処理
        /// </summary>
        /// <returns>Windowsデバイス情報</returns>
        public static WindowsDeviceInfo GetWindowsDeviceInfo()
        {
            WindowsDeviceInfo collectData = new()
            {
                HostName = GetHostName(),
                UserName = GetUserName(),
                OSName = GetOSName(),
                OSBuildNumber = GetOSBuildNumber(),
                OSVersion = GetOSVersion(),
                LastUpdate = GetLastUpdate()
            };

            return collectData;
        }

        /// <summary>
        /// ホスト名取得処理
        /// </summary>
        /// <returns>ホスト名</returns>
        private static string GetHostName()
        {
            string hostName = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                hostName = mo["DNSHostName"].ToString();
            }

            return hostName;
        }

        /// <summary>
        /// ユーザ名取得処理
        /// </summary>
        /// <returns>ユーザ名</returns>
        private static string GetUserName()
        {
            string userName = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                userName = mo["UserName"].ToString();
            }

            return userName;
        }

        /// <summary>
        /// OS名取得処理
        /// </summary>
        /// <returns>OS名</returns>
        private static string GetOSName()
        {
            string osName = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                osName = mo["Caption"].ToString();
            }

            return osName;
        }

        /// <summary>
        /// OSビルド番号取得処理
        /// </summary>
        /// <returns>OSビルド番号</returns>
        private static string GetOSBuildNumber()
        {
            string osBuildNumber = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                osBuildNumber = mo["BuildNumber"].ToString();
            }

            return osBuildNumber;
        }

        /// <summary>
        /// OSバージョン取得処理
        /// </summary>
        /// <returns>OSバージョン</returns>
        private static string GetOSVersion()
        {
            string osVersion = Resources.Strings.Unknown;

            string osName = GetOSName();
            string osBuildNumber = GetOSBuildNumber();

            if (osName.Contains("Windows 10"))
            {
                // Windows10の場合のビルド番号=>バージョン情報変換
                osVersion = osBuildNumber switch
                {
                    "10240" => "1507",
                    "10586" => "1511",
                    "14393" => "1607",
                    "15063" => "1703",
                    "16299" => "1709",
                    "17134" => "1803",
                    "17763" => "1809",
                    "18362" => "1903",
                    "18363" => "1909",
                    "19041" => "2004",
                    "19042" => "20H2",
                    "19043" => "21H1",
                    "19044" => "21H2",
                    "19045" => "22H2",
                    _ => $"{Resources.Strings.Unknown}(OS Build:{osBuildNumber})"
                };
            }
            else if (osName.Contains("Windows 11"))
            {
                // Windows11の場合のビルド番号=>バージョン情報変換
                osVersion = osBuildNumber switch
                {
                    "22000" => "21H2",
                    "22621" => "22H2",
                    _ => $"{Resources.Strings.Unknown}(OS Build:{osBuildNumber})"
                };
            }
            else
            {
                // Windows10/11以外の場合は無処理
            }

            return osVersion;
        }

        /// <summary>
        /// 最終更新日時取得処理
        /// </summary>
        /// <returns>最終更新日時</returns>
        private static string GetLastUpdate() => DateTime.Now.ToString();
    }
}
