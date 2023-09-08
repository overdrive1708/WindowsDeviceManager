namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// Windowsデバイス情報クラス
    /// </summary>
    public class WindowsDeviceInfo
    {
        /// <summary>
        /// ホスト名
        /// </summary>
        private string _hostName = string.Empty;
        public string HostName { get => _hostName; set => _hostName = value; }

        /// <summary>
        /// ユーザ名
        /// </summary>
        private string _userName = string.Empty;
        public string UserName { get => _userName; set => _userName = value; }

        /// <summary>
        /// OS名
        /// </summary>
        private string _osName = string.Empty;
        public string OSName { get => _osName; set => _osName = value; }

        /// <summary>
        /// OSビルド番号
        /// </summary>
        private string _osBuildNumber = string.Empty;
        public string OSBuildNumber { get => _osBuildNumber; set => _osBuildNumber = value; }

        /// <summary>
        /// OSバージョン
        /// </summary>
        private string _osVersion = string.Empty;
        public string OSVersion { get => _osVersion; set => _osVersion = value; }

        /// <summary>
        /// コンピュータの製造元
        /// </summary>
        private string _computerManufacturer = string.Empty;
        public string ComputerManufacturer { get => _computerManufacturer; set => _computerManufacturer = value; }

        /// <summary>
        /// コンピュータの製品名
        /// </summary>
        private string _computerModel = string.Empty;
        public string ComputerModel { get => _computerModel; set => _computerModel = value; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        private string _lastUpdate = string.Empty;
        public string LastUpdate { get => _lastUpdate; set => _lastUpdate = value; }
    }
}
