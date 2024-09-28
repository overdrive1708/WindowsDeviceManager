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
                using SQLiteConnection connection = new($"Data Source = {databasefile.Replace(@"\\",@"\\\\")}");
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
                                "0" => Resources.Strings.NotCollected,
                                "1" => executeReader["ComputerManufacturer"].ToString(),
                                "2" => executeReader["ComputerManufacturer"].ToString(),
                                "3" => executeReader["ComputerManufacturer"].ToString(),
                                "4" => executeReader["ComputerManufacturer"].ToString(),
                                "5" => executeReader["ComputerManufacturer"].ToString(),
                                "6" => executeReader["ComputerManufacturer"].ToString(),
                                "7" => executeReader["ComputerManufacturer"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            ComputerModel = databaseVersion switch
                            {
                                // コンピュータの製品名はuser_versionが1以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => executeReader["ComputerModel"].ToString(),
                                "2" => executeReader["ComputerModel"].ToString(),
                                "3" => executeReader["ComputerModel"].ToString(),
                                "4" => executeReader["ComputerModel"].ToString(),
                                "5" => executeReader["ComputerModel"].ToString(),
                                "6" => executeReader["ComputerModel"].ToString(),
                                "7" => executeReader["ComputerModel"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            Processor = databaseVersion switch
                            {
                                // プロセッサはuser_versionが2以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => executeReader["Processor"].ToString(),
                                "3" => executeReader["Processor"].ToString(),
                                "4" => executeReader["Processor"].ToString(),
                                "5" => executeReader["Processor"].ToString(),
                                "6" => executeReader["Processor"].ToString(),
                                "7" => executeReader["Processor"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            BIOSManufacturer = databaseVersion switch
                            {
                                // BIOSの製造元はuser_versionが3以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => Resources.Strings.NotCollected,
                                "3" => executeReader["BIOSManufacturer"].ToString(),
                                "4" => executeReader["BIOSManufacturer"].ToString(),
                                "5" => executeReader["BIOSManufacturer"].ToString(),
                                "6" => executeReader["BIOSManufacturer"].ToString(),
                                "7" => executeReader["BIOSManufacturer"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            BIOSVersion = databaseVersion switch
                            {
                                // BIOSのバージョンはuser_versionが3以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => Resources.Strings.NotCollected,
                                "3" => executeReader["BIOSVersion"].ToString(),
                                "4" => executeReader["BIOSVersion"].ToString(),
                                "5" => executeReader["BIOSVersion"].ToString(),
                                "6" => executeReader["BIOSVersion"].ToString(),
                                "7" => executeReader["BIOSVersion"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            BitLockerStatus = databaseVersion switch
                            {
                                // BitLockerの状態はuser_versionが4以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => Resources.Strings.NotCollected,
                                "3" => Resources.Strings.NotCollected,
                                "4" => executeReader["BitLockerStatus"].ToString(),
                                "5" => executeReader["BitLockerStatus"].ToString(),
                                "6" => executeReader["BitLockerStatus"].ToString(),
                                "7" => executeReader["BitLockerStatus"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            AntiVirusSoftware = databaseVersion switch
                            {
                                // アンチウィルスソフトウェアはuser_versionが5以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => Resources.Strings.NotCollected,
                                "3" => Resources.Strings.NotCollected,
                                "4" => Resources.Strings.NotCollected,
                                "5" => executeReader["AntiVirusSoftware"].ToString(),
                                "6" => executeReader["AntiVirusSoftware"].ToString(),
                                "7" => executeReader["AntiVirusSoftware"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            JavaVersioncheckResult = databaseVersion switch
                            {
                                // Javaのバージョンチェック結果はuser_versionが6以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => Resources.Strings.NotCollected,
                                "3" => Resources.Strings.NotCollected,
                                "4" => Resources.Strings.NotCollected,
                                "5" => Resources.Strings.NotCollected,
                                "6" => executeReader["JavaVersioncheckResult"].ToString(),
                                "7" => executeReader["JavaVersioncheckResult"].ToString(),
                                _ => Resources.Strings.NotCollected,
                            },
                            InstallCheckResult = databaseVersion switch
                            {
                                // インストールチェック結果はuser_versionが7以上の場合に取得可能
                                "0" => Resources.Strings.NotCollected,
                                "1" => Resources.Strings.NotCollected,
                                "2" => Resources.Strings.NotCollected,
                                "3" => Resources.Strings.NotCollected,
                                "4" => Resources.Strings.NotCollected,
                                "5" => Resources.Strings.NotCollected,
                                "6" => Resources.Strings.NotCollected,
                                "7" => executeReader["InstallCheckResult"].ToString(),
                                _ => Resources.Strings.NotCollected,
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

            using SQLiteConnection connection = new($"Data Source = {databasefile.Replace(@"\\", @"\\\\")}");
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
