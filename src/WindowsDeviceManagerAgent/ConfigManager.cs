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
