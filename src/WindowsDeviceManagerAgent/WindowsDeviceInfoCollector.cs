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
                ComputerManufacturer = GetComputerManufacturer(),
                ComputerModel = GetComputerModel(),
                Processor = GetProcessor(),
                BIOSManufacturer = GetBIOSManufacturer(),
                BIOSVersion = GetBIOSVersion(),
                BitLockerStatus = GetBitLockerStatus(),
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
        /// コンピュータの製造元取得処理
        /// </summary>
        /// <returns>コンピュータの製造元</returns>
        private static string GetComputerManufacturer()
        {
            string computerManufacturer = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                computerManufacturer = mo["Manufacturer"].ToString();
            }

            return computerManufacturer;
        }

        /// <summary>
        /// コンピュータの製品名取得処理
        /// </summary>
        /// <returns>コンピュータの製品名</returns>
        private static string GetComputerModel()
        {
            string computerModel = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                computerModel = mo["Model"].ToString();
            }

            return computerModel;
        }

        /// <summary>
        /// プロセッサ取得処理
        /// </summary>
        /// <returns>プロセッサ</returns>
        private static string GetProcessor()
        {
            string processor = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                processor = mo["Name"].ToString();
            }

            return processor;
        }

        /// <summary>
        /// BIOSの製造元取得処理
        /// </summary>
        /// <returns>BIOSの製造元</returns>
        private static string GetBIOSManufacturer()
        {
            string biosmanufacturer = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_BIOS");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                biosmanufacturer = mo["Manufacturer"].ToString();
            }

            return biosmanufacturer;
        }

        /// <summary>
        /// BIOSのバージョン取得処理
        /// </summary>
        /// <returns>BIOSのバージョン</returns>
        private static string GetBIOSVersion()
        {
            string biosversion = Resources.Strings.Unknown;

            ManagementClass mc = new("Win32_BIOS");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                biosversion = mo["SMBIOSBIOSVersion"].ToString();
            }

            return biosversion;
        }

        /// <summary>
        /// BitLockerの状態取得処理
        /// </summary>
        /// <returnsBitLockerの状態></returns>
        private static string GetBitLockerStatus()
        {
            // 固定ディスクごとのBitLocker状態を取得
            List<BitLockerStatusInfo> bitLockerStatusInfos = BitLockerStatusInfoCollector.GetBitLockerStatusInfo();

            if (bitLockerStatusInfos.All(x => x.BitLockerStatus == BitLockerStatusInfo.Status.On))
            {
                // すべての固定ディスクが有効
                return Resources.Strings.BitLockerStatusAllDiskEnable;
            }

            if (bitLockerStatusInfos.All(x => x.BitLockerStatus == BitLockerStatusInfo.Status.Off))
            {
                // すべての固定ディスクが無効
                return Resources.Strings.BitLockerStatusAllDiskDisable;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.Off))
            {
                // いずれかの固定ディスクが無効
                return Resources.Strings.BitLockerStatusAnyDiskDisable;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.Encrypting))
            {
                // いずれかの固定ディスクが暗号化中
                return Resources.Strings.BitLockerStatusAnyDiskEncrypting;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.Decrypting))
            {
                // いずれかの固定ディスクが復号化中
                return Resources.Strings.BitLockerStatusAnyDiskDecrypting;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.Suspended))
            {
                // いずれかの固定ディスクが中断
                return Resources.Strings.BitLockerStatusAnyDiskSuspended;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.OnLocked))
            {
                // いずれかの固定ディスクが有効(ロック)
                return Resources.Strings.BitLockerStatusAnyDiskOnLocked;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.WaitingForActivation))
            {
                // いずれかの固定ディスクがアクティブ化を待機中
                return Resources.Strings.BitLockerStatusAnyDiskWaitingForActivation;
            }

            if (bitLockerStatusInfos.Any(x => x.BitLockerStatus == BitLockerStatusInfo.Status.Unknown))
            {
                // いずれかの固定ディスクが状態不明
                return Resources.Strings.BitLockerStatusAnyDiskUnknown;
            }

            return Resources.Strings.BitLockerStatusAnyDiskUnknown;
        }

        /// <summary>
        /// 最終更新日時取得処理
        /// </summary>
        /// <returns>最終更新日時</returns>
        private static string GetLastUpdate() => DateTime.Now.ToString();
    }
}
