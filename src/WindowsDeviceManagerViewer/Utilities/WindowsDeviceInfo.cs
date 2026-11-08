using CsvHelper;

namespace WindowsDeviceManagerViewer.Utilities
{
    /// <summary>
    /// Windowsデバイス情報クラス
    /// </summary>
    [CsvRecord]
    public partial class WindowsDeviceInfo
    {
        /// <summary>
        /// ホスト名
        /// </summary>
        [CsvColumn("HostName")]
        private string _hostName = string.Empty;
        public string HostName { get => _hostName; set => _hostName = value; }

        /// <summary>
        /// ユーザ名
        /// </summary>
        [CsvColumn("UserName")]
        private string _userName = string.Empty;
        public string UserName { get => _userName; set => _userName = value; }

        /// <summary>
        /// OS名
        /// </summary>
        [CsvColumn("OSName")]
        private string _osName = string.Empty;
        public string OSName { get => _osName; set => _osName = value; }

        /// <summary>
        /// OSビルド番号
        /// </summary>
        [CsvColumn("OSBuildNumber")]
        private string _osBuildNumber = string.Empty;
        public string OSBuildNumber { get => _osBuildNumber; set => _osBuildNumber = value; }

        /// <summary>
        /// OSバージョン
        /// </summary>
        [CsvColumn("OSVersion")]
        private string _osVersion = string.Empty;
        public string OSVersion { get => _osVersion; set => _osVersion = value; }

        /// <summary>
        /// コンピュータの製造元
        /// </summary>
        [CsvColumn("ComputerManufacturer")]
        private string _computerManufacturer = string.Empty;
        public string ComputerManufacturer { get => _computerManufacturer; set => _computerManufacturer = value; }

        /// <summary>
        /// コンピュータの製品名
        /// </summary>
        [CsvColumn("ComputerModel")]
        private string _computerModel = string.Empty;
        public string ComputerModel { get => _computerModel; set => _computerModel = value; }

        /// <summary>
        /// プロセッサ
        /// </summary>
        [CsvColumn("Processor")]
        private string _processor = string.Empty;
        public string Processor { get => _processor; set => _processor = value; }

        /// <summary>
        /// BIOSの製造元
        /// </summary>
        [CsvColumn("BIOSManufacturer")]
        private string _biosManufacturer = string.Empty;
        public string BIOSManufacturer { get => _biosManufacturer; set => _biosManufacturer = value; }

        /// <summary>
        /// BIOSのバージョン
        /// </summary>
        [CsvColumn("BIOSVersion")]
        private string _biosVersion = string.Empty;
        public string BIOSVersion { get => _biosVersion; set => _biosVersion = value; }

        /// <summary>
        /// BitLockerの状態
        /// </summary>
        [CsvColumn("BitLockerStatus")]
        private string _bitLockerStatus = string.Empty;
        public string BitLockerStatus { get => _bitLockerStatus; set => _bitLockerStatus = value; }

        /// <summary>
        /// アンチウィルスソフトウェア
        /// </summary>
        [CsvColumn("AntiVirusSoftware")]
        private string _antiVirusSoftware = string.Empty;
        public string AntiVirusSoftware { get => _antiVirusSoftware; set => _antiVirusSoftware = value; }

        /// <summary>
        /// Javaのバージョンチェック結果
        /// </summary>
        [CsvColumn("JavaVersioncheckResult")]
        private string _javaVersioncheckResult = string.Empty;
        public string JavaVersioncheckResult { get => _javaVersioncheckResult; set => _javaVersioncheckResult = value; }

        /// <summary>
        /// インストールチェック結果
        /// </summary>
        [CsvColumn("InstallCheckResult")]
        private string _installCheckResult = string.Empty;
        public string InstallCheckResult { get => _installCheckResult; set => _installCheckResult = value; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        [CsvColumn("LastUpdate")]
        private string _lastUpdate = string.Empty;
        public string LastUpdate { get => _lastUpdate; set => _lastUpdate = value; }
    }
}
