using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// JSONファイル書き込みクラス
    /// </summary>
    internal class JsonWriter
    {
        //--------------------------------------------------
        // 定数(コンフィギュレーション)
        //--------------------------------------------------
        /// <summary>
        /// データベースファイル名
        /// </summary>
        private static readonly string _fileName = "WindowsDeviceInfo.json";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="writeValue">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecord(WindowsDeviceInfo writeValue)
        {
            // シリアライズ(インデントあり/日本語ありのためエンコーダ設定/高速化のためUTF-8 バイトの配列にシリアル化)
            JsonSerializerOptions options = new() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(writeValue, options);

            // ファイル出力
            using FileStream fs = new(_fileName, FileMode.Create);
            fs.Write(jsonUtf8Bytes);
            fs.Close();

            // 完了メッセージ表示
            ConsoleWrapper.WriteLine(Resources.Strings.MessageCreateJsonFile);
        }
    }
}
