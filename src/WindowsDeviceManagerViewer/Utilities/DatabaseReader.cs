using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace WindowsDeviceManagerViewer.Utilities
{
    /// <summary>
    /// データベース読み込みクラス
    /// </summary>
    internal class DatabaseReader
    {
        /// <summary>
        /// Windowsデバイス情報読み込み処理
        /// </summary>
        /// <param name="databasefile">データベースファイル名</param>
        /// <returns>Windowsデバイス情報</returns>
        public static List<WindowsDeviceInfo> ReadWindowsDeviceInfoRecords(string databasefile)
        {
            List<WindowsDeviceInfo> readRecords = new();

            if (File.Exists(databasefile))
            {
                using SQLiteConnection connection = new($"Data Source = {databasefile}");
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM WindowsDeviceInfo";
                    using var executeReader = command.ExecuteReader();
                    while (executeReader.Read())
                    {
                        WindowsDeviceInfo readRecord = new()
                        {
                            HostName = executeReader["HostName"].ToString(),
                            UserName = executeReader["UserName"].ToString(),
                            OSName = executeReader["OSName"].ToString(),
                            OSBuildNumber = executeReader["OSBuildNumber"].ToString(),
                            OSVersion = executeReader["OSVersion"].ToString(),
                            LastUpdate = executeReader["LastUpdate"].ToString()
                        };
                        readRecords.Add(readRecord);
                    }
                }
                connection.Close();
            }

            return readRecords;
        }
    }
}
