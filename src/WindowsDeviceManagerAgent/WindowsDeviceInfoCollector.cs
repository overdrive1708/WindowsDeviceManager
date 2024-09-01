using System.Diagnostics;
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
            WindowsDeviceInfo collectData = new();

            ConsoleWrapper.WriteLine(Resources.Strings.CollectResultStart);

            collectData.HostName = GetHostName();
            ConsoleWrapper.WriteLine($"{Resources.Strings.HostName}:{collectData.HostName}");

            collectData.UserName = GetUserName();
            ConsoleWrapper.WriteLine($"{Resources.Strings.UserName}:{collectData.UserName}");

            collectData.OSName = GetOSName();
            ConsoleWrapper.WriteLine($"{Resources.Strings.OSName}:{collectData.OSName}");

            collectData.OSBuildNumber = GetOSBuildNumber();
            ConsoleWrapper.WriteLine($"{Resources.Strings.OSBuildNumber}:{collectData.OSBuildNumber}");

            collectData.OSVersion = GetOSVersion();
            ConsoleWrapper.WriteLine($"{Resources.Strings.OSVersion}:{collectData.OSVersion}");

            collectData.ComputerManufacturer = GetComputerManufacturer();
            ConsoleWrapper.WriteLine($"{Resources.Strings.ComputerManufacturer}:{collectData.ComputerManufacturer}");

            collectData.ComputerModel = GetComputerModel();
            ConsoleWrapper.WriteLine($"{Resources.Strings.ComputerModel}:{collectData.ComputerModel}");

            collectData.Processor = GetProcessor();
            ConsoleWrapper.WriteLine($"{Resources.Strings.Processor}:{collectData.Processor}");

            collectData.BIOSManufacturer = GetBIOSManufacturer();
            ConsoleWrapper.WriteLine($"{Resources.Strings.BIOSManufacturer}:{collectData.BIOSManufacturer}");

            collectData.BIOSVersion = GetBIOSVersion();
            ConsoleWrapper.WriteLine($"{Resources.Strings.BIOSVersion}:{collectData.BIOSVersion}");

            collectData.BitLockerStatus = GetBitLockerStatus();
            ConsoleWrapper.WriteLine($"{Resources.Strings.BitLockerStatus}:{collectData.BitLockerStatus}");

            collectData.AntiVirusSoftware = GetAntiVirusSoftware();
            ConsoleWrapper.WriteLine($"{Resources.Strings.AntiVirusSoftware}:{collectData.AntiVirusSoftware}");

            collectData.JavaVersioncheckResult = GetJavaVersioncheckResult();
            ConsoleWrapper.WriteLine($"{Resources.Strings.JavaVersioncheckResult}:{collectData.JavaVersioncheckResult}");

            collectData.LastUpdate = GetLastUpdate();
            ConsoleWrapper.WriteLine($"{Resources.Strings.LastUpdate}:{collectData.LastUpdate}");
            ConsoleWrapper.WriteLine(Resources.Strings.CollectResultEnd);

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

            try
            {
                ManagementClass mc = new("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc.Cast<ManagementObject>())
                {
                    userName = mo["UserName"].ToString();
                }
            }
            catch (NullReferenceException)
            {
                // リモートデスクトップでログインした状態で実行するとNullReferenceExceptionが発生する｡
                // 無処理でUnknownを返す｡
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
                    "22631" => "23H2",
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
        /// アンチウィルスソフトウェア取得処理
        /// </summary>
        /// <returns></returns>
        private static string GetAntiVirusSoftware()
        {
            string antiVirusSoftware = Resources.Strings.Unknown;

            try
            {
                ManagementObjectSearcher mos = new(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                ManagementObjectCollection moc = mos.Get();
                foreach (ManagementObject mo in moc.Cast<ManagementObject>())
                {
                    antiVirusSoftware = mo["displayName"].ToString();
                }
            }
            catch
            {
                // 例外が発生してアンチウィルスソフトウェアの情報が取得できない場合
                // 無処理でUnknownを返す｡
            }

            return antiVirusSoftware;
        }

        /// <summary>
        /// Javaのバージョンチェック結果取得処理
        /// </summary>
        /// <returns></returns>
        private static string GetJavaVersioncheckResult()
        {
            string javaVersioncheckResult = string.Empty;
            bool isFindJava = false;

            // インストール済みアプリケーションの一覧からの検索
            try
            {
                // インストール済みアプリケーションを取得
                List<InstalledApplicationInfo> installedApps = InstalledApplicationsCollector.GetInstalledApplications();

                // Javaがインストールされているかどうか確認
                foreach (InstalledApplicationInfo installedApp in installedApps)
                {
                    if ((installedApp.Name.Contains("java", StringComparison.OrdinalIgnoreCase)) && (!installedApp.Name.Contains("javascript", StringComparison.OrdinalIgnoreCase)))
                    {
                        javaVersioncheckResult += $"Name=[{installedApp.Name}], Version=[{installedApp.Version}], Publisher=[{installedApp.Publisher}];";
                        isFindJava = true;
                    }
                }
            }
            catch
            {
                // 例外が発生してJavaのバージョンチェック結果が取得できない場合
                // Unknownにする｡
                javaVersioncheckResult = Resources.Strings.Unknown;
            }

            // Javaを検出したら処理を終了する
            if (isFindJava)
            {
                return javaVersioncheckResult;
            }

            // インストール済みアプリケーションの一覧にない場合は念のため[java -version]のコマンドを実行して調査
            try
            {
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = "java",
                    Arguments = "-version",
                    RedirectStandardError = true,   // Javaのバージョン出力は標準エラー出力
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = Process.Start(processStartInfo);
                using StreamReader reader = process.StandardError;
                javaVersioncheckResult = reader.ReadToEnd().Replace(Environment.NewLine, ";");  // 改行があるとファイル出力時に読みづらくなるため別の文字に置き換える
            }
            catch
            {
                // 例外が発生してJavaのバージョンチェック結果が取得できない場合はJavaが存在しない
                javaVersioncheckResult = Resources.Strings.Undetected;
            }

            return javaVersioncheckResult;
        }

        /// <summary>
        /// 最終更新日時取得処理
        /// </summary>
        /// <returns>最終更新日時</returns>
        private static string GetLastUpdate() => DateTime.Now.ToString();
    }
}
