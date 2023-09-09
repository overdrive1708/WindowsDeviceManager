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
                string databaseVersion = GetDatabaseUserVersion(databasefile);
                using SQLiteConnection connection = new($"Data Source = {databasefile}");
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM WindowsDeviceInfo";
                    using var executeReader = command.ExecuteReader();
                    while (executeReader.Read())
                    {
                        WindowsDeviceInfo readRecord;
                        switch (databaseVersion)
                        {
                            case "0":       // user_versionが0のとき
                                readRecord = new()
                                {
                                    HostName = executeReader["HostName"].ToString(),
                                    UserName = executeReader["UserName"].ToString(),
                                    OSName = executeReader["OSName"].ToString(),
                                    OSBuildNumber = executeReader["OSBuildNumber"].ToString(),
                                    OSVersion = executeReader["OSVersion"].ToString(),
                                    ComputerManufacturer = string.Empty,
                                    ComputerModel = string.Empty,
                                    Processor = string.Empty,
                                    LastUpdate = executeReader["LastUpdate"].ToString()
                                };
                                break;

                            case "1":       // user_versionが1のとき
                                readRecord = new()
                                {
                                    HostName = executeReader["HostName"].ToString(),
                                    UserName = executeReader["UserName"].ToString(),
                                    OSName = executeReader["OSName"].ToString(),
                                    OSBuildNumber = executeReader["OSBuildNumber"].ToString(),
                                    OSVersion = executeReader["OSVersion"].ToString(),
                                    ComputerManufacturer = executeReader["ComputerManufacturer"].ToString(),
                                    ComputerModel = executeReader["ComputerModel"].ToString(),
                                    Processor = string.Empty,
                                    LastUpdate = executeReader["LastUpdate"].ToString()
                                };
                                break;

                            case "2":       // user_versionが2のとき
                                readRecord = new()
                                {
                                    HostName = executeReader["HostName"].ToString(),
                                    UserName = executeReader["UserName"].ToString(),
                                    OSName = executeReader["OSName"].ToString(),
                                    OSBuildNumber = executeReader["OSBuildNumber"].ToString(),
                                    OSVersion = executeReader["OSVersion"].ToString(),
                                    ComputerManufacturer = executeReader["ComputerManufacturer"].ToString(),
                                    ComputerModel = executeReader["ComputerModel"].ToString(),
                                    Processor = executeReader["Processor"].ToString(),
                                    LastUpdate = executeReader["LastUpdate"].ToString()
                                };
                                break;

                            default:        // user_versionが想定外のとき
                                readRecord = new()
                                {
                                    HostName = string.Empty,
                                    UserName = string.Empty,
                                    OSName = string.Empty,
                                    OSBuildNumber = string.Empty,
                                    OSVersion = string.Empty,
                                    ComputerManufacturer = string.Empty,
                                    ComputerModel = string.Empty,
                                    Processor = string.Empty,
                                    LastUpdate = string.Empty
                                };
                                break;
                        }
                        readRecords.Add(readRecord);
                    }
                }
                connection.Close();
            }

            return readRecords;
        }

        /// <summary>
        /// データベースuser_version取得処理
        /// </summary>
        /// <param name="databasefile">データベースファイル名</param>
        /// <returns>データベースのuser_version</returns>
        private static string GetDatabaseUserVersion(string databasefile)
        {
            string version = string.Empty;

            using SQLiteConnection connection = new($"Data Source = {databasefile}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA user_version";
                version = command.ExecuteScalar().ToString();
            }
            connection.Close();

            return version;
        }
    }
}
