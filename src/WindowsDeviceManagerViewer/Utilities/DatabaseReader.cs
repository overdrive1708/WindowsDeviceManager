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
                        WindowsDeviceInfo readRecord = new()
                        {
                            HostName = executeReader["HostName"].ToString(),
                            UserName = executeReader["UserName"].ToString(),
                            OSName = executeReader["OSName"].ToString(),
                            OSBuildNumber = executeReader["OSBuildNumber"].ToString(),
                            OSVersion = executeReader["OSVersion"].ToString(),
                            ComputerManufacturer = databaseVersion switch
                            {
                                // コンピュータの製造元はuser_versionが1以上の場合に取得可能
                                "0" => string.Empty,
                                "1" => executeReader["ComputerManufacturer"].ToString(),
                                "2" => executeReader["ComputerManufacturer"].ToString(),
                                "3" => executeReader["ComputerManufacturer"].ToString(),
                                "4" => executeReader["ComputerManufacturer"].ToString(),
                                _ => string.Empty,
                            },
                            ComputerModel = databaseVersion switch
                            {
                                // コンピュータの製品名はuser_versionが1以上の場合に取得可能
                                "0" => string.Empty,
                                "1" => executeReader["ComputerModel"].ToString(),
                                "2" => executeReader["ComputerModel"].ToString(),
                                "3" => executeReader["ComputerModel"].ToString(),
                                "4" => executeReader["ComputerModel"].ToString(),
                                _ => string.Empty,
                            },
                            Processor = databaseVersion switch
                            {
                                // プロセッサはuser_versionが2以上の場合に取得可能
                                "0" => string.Empty,
                                "1" => string.Empty,
                                "2" => executeReader["Processor"].ToString(),
                                "3" => executeReader["Processor"].ToString(),
                                "4" => executeReader["Processor"].ToString(),
                                _ => string.Empty,
                            },
                            BIOSManufacturer = databaseVersion switch
                            {
                                // BIOSの製造元はuser_versionが3以上の場合に取得可能
                                "0" => string.Empty,
                                "1" => string.Empty,
                                "2" => string.Empty,
                                "3" => executeReader["BIOSManufacturer"].ToString(),
                                "4" => executeReader["BIOSManufacturer"].ToString(),
                                _ => string.Empty,
                            },
                            BIOSVersion = databaseVersion switch
                            {
                                // BIOSのバージョンはuser_versionが3以上の場合に取得可能
                                "0" => string.Empty,
                                "1" => string.Empty,
                                "2" => string.Empty,
                                "3" => executeReader["BIOSVersion"].ToString(),
                                "4" => executeReader["BIOSVersion"].ToString(),
                                _ => string.Empty,
                            },
                            BitLockerStatus = databaseVersion switch
                            {
                                // BitLockerの状態はuser_versionが4以上の場合に取得可能
                                "0" => string.Empty,
                                "1" => string.Empty,
                                "2" => string.Empty,
                                "3" => string.Empty,
                                "4" => executeReader["BitLockerStatus"].ToString(),
                                _ => string.Empty,
                            },
                            LastUpdate = executeReader["LastUpdate"].ToString()
                        };

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
