using CsvHelper;
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

            CsvWriterOptions options = new()
            {
                FormatProvider = CultureInfo.InvariantCulture,
                Quoting = CsvQuoting.Always
            };
            using (CsvHelper.CsvWriter csv = CsvHelper.CsvWriter.Create(swCsv, options))
            {
                csv.WriteHeader<WindowsDeviceInfo>();
                csv.WriteRecords(windowsDeviceInfos);
            }

            swCsv.Close();
        }
    }
}
