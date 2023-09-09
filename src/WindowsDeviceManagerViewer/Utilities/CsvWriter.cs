using System.Collections.Generic;
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

            swCsv.WriteLine($"\"{Resources.Strings.HostName}\",\"{Resources.Strings.UserName}\",\"{Resources.Strings.OSName}\",\"{Resources.Strings.OSBuildNumber}\",\"{Resources.Strings.OSVersion}\",\"{Resources.Strings.ComputerManufacturer}\",\"{Resources.Strings.ComputerModel}\",\"{Resources.Strings.Processor}\",\"{Resources.Strings.BIOSManufacturer}\",\"{Resources.Strings.BIOSVersion}\",\"{Resources.Strings.LastUpdate}\"");

            foreach (WindowsDeviceInfo info in windowsDeviceInfos)
            {
                swCsv.WriteLine($"\"{info.HostName}\",\"{info.UserName}\",\"{info.OSName}\",\"{info.OSBuildNumber}\",\"{info.OSVersion}\",\"{info.ComputerManufacturer}\",\"{info.ComputerModel}\",\"{info.Processor}\",\"{info.BIOSManufacturer}\",\"{info.BIOSVersion}\",\"{info.LastUpdate}\"");
            }

            swCsv.Close();
        }
    }
}
