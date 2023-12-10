using CsvHelper.Configuration;
using System.Globalization;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// CSVファイル書き込みクラス
    /// </summary>
    internal class CsvWriter
    {
        //--------------------------------------------------
        // 定数(コンフィギュレーション)
        //--------------------------------------------------
        /// <summary>
        /// 出力ファイル名
        /// </summary>
        private static readonly string _fileName = "WindowsDeviceInfo.csv";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="writeValue">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecord(WindowsDeviceInfo writeValue)
        {
            // ファイル出力
            using StreamWriter swCsv = new(_fileName, false, System.Text.Encoding.UTF8);
            CsvConfiguration options = new(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (context) => true
            };
            using (CsvHelper.CsvWriter csv = new(swCsv, options))
            {
                csv.WriteHeader<WindowsDeviceInfo>();
                csv.NextRecord();
                csv.WriteRecord(writeValue);
            }
            swCsv.Close();

            // 完了メッセージ表示
            ConsoleWrapper.WriteLine(Resources.Strings.MessageCreateCsvFile);
        }
    }
}
