using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// 設定管理クラス
    /// </summary>
    internal class ConfigManager
    {
        /// <summary>
        /// 設定クラス
        /// </summary>
        public class Config
        {
            /// <summary>
            /// 収集設定：ユーザ名
            /// </summary>
            private bool _isCollectUserName = true;
            public bool IsCollectUserName { get => _isCollectUserName; set => _isCollectUserName = value; }

            /// <summary>
            /// 収集設定：OS名
            /// </summary>
            private bool _isCollectOsName = true;
            public bool IsCollectOSName { get => _isCollectOsName; set => _isCollectOsName = value; }

            /// <summary>
            /// 収集設定：OSビルド番号
            /// </summary>
            private bool _isCollectOsBuildNumber = true;
            public bool IsCollectOSBuildNumber { get => _isCollectOsBuildNumber; set => _isCollectOsBuildNumber = value; }

            /// <summary>
            /// 収集設定：OSバージョン
            /// </summary>
            private bool _isCollectOsVersion = true;
            public bool IsCollectOSVersion { get => _isCollectOsVersion; set => _isCollectOsVersion = value; }

            /// <summary>
            /// 収集設定：コンピュータの製造元
            /// </summary>
            private bool _isCollectComputerManufacturer = true;
            public bool IsCollectComputerManufacturer { get => _isCollectComputerManufacturer; set => _isCollectComputerManufacturer = value; }

            /// <summary>
            /// 収集設定：コンピュータの製品名
            /// </summary>
            private bool _isCollectComputerModel = true;
            public bool IsCollectComputerModel { get => _isCollectComputerModel; set => _isCollectComputerModel = value; }

            /// <summary>
            /// 収集設定：プロセッサ
            /// </summary>
            private bool _isCollectProcessor = true;
            public bool IsCollectProcessor { get => _isCollectProcessor; set => _isCollectProcessor = value; }

            /// <summary>
            /// 収集設定：BIOSの製造元
            /// </summary>
            private bool _isCollectBiosManufacturer = true;
            public bool IsCollectBIOSManufacturer { get => _isCollectBiosManufacturer; set => _isCollectBiosManufacturer = value; }

            /// <summary>
            /// 収集設定：BIOSのバージョン
            /// </summary>
            private bool _isCollectBiosVersion = true;
            public bool IsCollectBIOSVersion { get => _isCollectBiosVersion; set => _isCollectBiosVersion = value; }

            /// <summary>
            /// 収集設定：BitLockerの状態
            /// </summary>
            private bool _isCollectBitLockerStatus = true;
            public bool IsCollectBitLockerStatus { get => _isCollectBitLockerStatus; set => _isCollectBitLockerStatus = value; }

            /// <summary>
            /// 収集設定：アンチウィルスソフトウェア
            /// </summary>
            private bool _isCollectAntiVirusSoftware = true;
            public bool IsCollectAntiVirusSoftware { get => _isCollectAntiVirusSoftware; set => _isCollectAntiVirusSoftware = value; }

            /// <summary>
            /// 収集設定：Javaのバージョンチェック結果
            /// </summary>
            private bool _isCollectJavaVersioncheckResult = true;
            public bool IsCollectJavaVersioncheckResult { get => _isCollectJavaVersioncheckResult; set => _isCollectJavaVersioncheckResult = value; }

            /// <summary>
            /// 収集設定：インストールチェック結果
            /// </summary>
            private bool _isCollectInstallCheckResult = true;
            public bool IsCollectInstallCheckResult { get => _isCollectInstallCheckResult; set => _isCollectInstallCheckResult = value; }

            /// <summary>
            /// インストールチェック：Name一覧
            /// </summary>
            private List<string> _installCheckNameList = [];
            public List<string> InstallCheckNameList { get => _installCheckNameList; set => _installCheckNameList = value; }

            /// <summary>
            /// インストールチェック：Publisher一覧
            /// </summary>
            private List<string> _installCheckPublisherList = [];
            public List<string> InstallCheckPublisherList { get => _installCheckPublisherList; set => _installCheckPublisherList = value; }
        }

        //--------------------------------------------------
        // 定数(コンフィギュレーション)
        //--------------------------------------------------
        /// <summary>
        /// 設定ファイル名
        /// </summary>
        private static readonly string _fileName = "Config.json";

        //--------------------------------------------------
        // プロパティ
        //--------------------------------------------------
        /// <summary>
        /// 設定値
        /// </summary>
        private static Config _configValue = new();
        public static Config ConfigValue { get => _configValue; set => _configValue = value; }

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// 設定ファイル読み込み処理
        /// </summary>
        public static void ReadConfig()
        {
            // 設定ファイルがない場合は新規作成する
            if (!File.Exists(_fileName))
            {
                WriteConfig();
            }

            // 設定ファイルの読み込み
            string jsonString = File.ReadAllText(_fileName);

            // デシリアライズ
            ConfigValue = JsonSerializer.Deserialize<Config>(jsonString)!;
        }

        /// <summary>
        /// 設定ファイル書き込み処理
        /// </summary>
        private static void WriteConfig()
        {
            // シリアライズ(インデントあり/日本語ありのためエンコーダ設定/高速化のためUTF-8 バイトの配列にシリアル化)
            JsonSerializerOptions options = new() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(ConfigValue, options);

            // ファイル出力
            using FileStream fs = new(_fileName, FileMode.Create);
            fs.Write(jsonUtf8Bytes);
            fs.Close();
        }
    }
}
