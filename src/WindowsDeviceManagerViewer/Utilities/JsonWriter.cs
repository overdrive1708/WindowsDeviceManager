using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace WindowsDeviceManagerViewer.Utilities
{
    /// <summary>
    /// JSONファイル書き込みクラス
    /// </summary>
    internal class JsonWriter
    {
        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="outputJsonFile">出力JSONファイル名</param>
        /// <param name="windowsDeviceInfos">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecords(string outputJsonFile, List<WindowsDeviceInfo> windowsDeviceInfos)
        {
            // シリアライズ(インデントあり/日本語ありのためエンコーダ設定/高速化のためUTF-8 バイトの配列にシリアル化)
            JsonSerializerOptions options = new() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(windowsDeviceInfos, options);

            // ファイル出力
            using FileStream fs = new(outputJsonFile, FileMode.Create);
            fs.Write(jsonUtf8Bytes);
            fs.Close();
        }
    }
}
