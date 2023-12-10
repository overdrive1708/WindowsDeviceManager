using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace WindowsDeviceManagerViewer.Utilities
{
    /// <summary>
    /// CSVファイル書き込みクラス
    /// </summary>
    internal class CsvWriter
    {
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="outputCsvFile">出力CSVファイル名</param>
        /// <param name="windowsDeviceInfos">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecords(string outputCsvFile, List<WindowsDeviceInfo> windowsDeviceInfos)
        {
            using StreamWriter swCsv = new(outputCsvFile, false, System.Text.Encoding.UTF8);

            CsvConfiguration options = new(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (context) => true
            };
            using (CsvHelper.CsvWriter csv = new(swCsv, options))
            {
                csv.WriteHeader<WindowsDeviceInfo>();
                csv.NextRecord();
                csv.WriteRecords(windowsDeviceInfos);
            }

            swCsv.Close();
        }
    }
}
