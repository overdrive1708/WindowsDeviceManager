using System.Data.SQLite;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// データベース書き込みクラス
    /// </summary>
    internal class DatabaseWriter
    {
        //--------------------------------------------------
        // 定数(コンフィギュレーション)
        //--------------------------------------------------
        /// <summary>
        /// データベースファイル名
        /// </summary>
        private static readonly string _databaseFileName = "WindowsDeviceInfo.db";

        /// <summary>
        /// データベースuser_version
        /// </summary>
        private static readonly string _databaseUserVersion = "7";

        /// <summary>
        /// SQLコマンド(テーブル作成)
        /// </summary>
        private static readonly string _createTableCommand = "CREATE TABLE IF NOT EXISTS WindowsDeviceInfo("
                                                             + "HostName TEXT PRIMARY KEY, "
                                                             + "UserName TEXT, "
                                                             + "OSName TEXT, "
                                                             + "OSBuildNumber TEXT, "
                                                             + "OSVersion TEXT, "
                                                             + "ComputerManufacturer TEXT, "
                                                             + "ComputerModel TEXT, "
                                                             + "Processor TEXT, "
                                                             + "BIOSManufacturer TEXT, "
                                                             + "BIOSVersion TEXT, "
                                                             + "BitLockerStatus TEXT, "
                                                             + "AntiVirusSoftware TEXT, "
                                                             + "JavaVersioncheckResult TEXT, "
                                                             + "InstallCheckResult TEXT, "
                                                             + "LastUpdate TEXT)";

        /// <summary>
        /// SQLコマンド(レコード登録)
        /// </summary>
        private static readonly string _insertCommand = "INSERT OR REPLACE INTO WindowsDeviceInfo"
                                                        + "(HostName, UserName, OSName, OSBuildNumber, OSVersion, ComputerManufacturer, ComputerModel, Processor, BIOSManufacturer, BIOSVersion, BitLockerStatus, AntiVirusSoftware, JavaVersioncheckResult, InstallCheckResult, LastUpdate) "
                                                        + "VALUES(@p_HostName, @p_UserName, @p_OSName, @p_OSBuildNumber, @p_OSVersion, @p_ComputerManufacturer, @p_ComputerModel, @p_Processor, @p_BIOSManufacturer, @p_BIOSVersion, @p_BitLockerStatus, @p_AntiVirusSoftware, @p_JavaVersioncheckResult, @p_InstallCheckResult, @p_LastUpdate)";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="writeValue">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecord(WindowsDeviceInfo writeValue)
        {
            // データベースファイルが見つからない場合は作成する
            if (!File.Exists(_databaseFileName))
            {
                ConsoleWrapper.WriteErrorLine(Resources.Strings.MessageDatabaseNotFound);
                CreateDatabase();
            }
            else
            {
                // 無処理
            }

            // データベースにWindowsデバイス情報を書き込む
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = _insertCommand;
                _ = command.Parameters.Add(new SQLiteParameter("@p_HostName", writeValue.HostName));
                _ = command.Parameters.Add(new SQLiteParameter("@p_UserName", writeValue.UserName));
                _ = command.Parameters.Add(new SQLiteParameter("@p_OSName", writeValue.OSName));
                _ = command.Parameters.Add(new SQLiteParameter("@p_OSBuildNumber", writeValue.OSBuildNumber));
                _ = command.Parameters.Add(new SQLiteParameter("@p_OSVersion", writeValue.OSVersion));
                _ = command.Parameters.Add(new SQLiteParameter("@p_ComputerManufacturer", writeValue.ComputerManufacturer));
                _ = command.Parameters.Add(new SQLiteParameter("@p_ComputerModel", writeValue.ComputerModel));
                _ = command.Parameters.Add(new SQLiteParameter("@p_Processor", writeValue.Processor));
                _ = command.Parameters.Add(new SQLiteParameter("@p_BIOSManufacturer", writeValue.BIOSManufacturer));
                _ = command.Parameters.Add(new SQLiteParameter("@p_BIOSVersion", writeValue.BIOSVersion));
                _ = command.Parameters.Add(new SQLiteParameter("@p_BitLockerStatus", writeValue.BitLockerStatus));
                _ = command.Parameters.Add(new SQLiteParameter("@p_AntiVirusSoftware", writeValue.AntiVirusSoftware));
                _ = command.Parameters.Add(new SQLiteParameter("@p_JavaVersioncheckResult", writeValue.JavaVersioncheckResult));
                _ = command.Parameters.Add(new SQLiteParameter("@p_InstallCheckResult", writeValue.InstallCheckResult));
                _ = command.Parameters.Add(new SQLiteParameter("@p_LastUpdate", writeValue.LastUpdate));
                command.Prepare();
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理
        /// </summary>
        public static void UpdateDatabase()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルのバージョン情報を取得する
                string version = GetDatabaseUserVersion();

                switch (version)
                {
                    case "0":
                        // user_versionが0のときは､0から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion1();
                        UpdateDatabaseVersion2();
                        UpdateDatabaseVersion3();
                        UpdateDatabaseVersion4();
                        UpdateDatabaseVersion5();
                        UpdateDatabaseVersion6();
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "1":
                        // user_versionが1のときは､1から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion2();
                        UpdateDatabaseVersion3();
                        UpdateDatabaseVersion4();
                        UpdateDatabaseVersion5();
                        UpdateDatabaseVersion6();
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "2":
                        // user_versionが2のときは､2から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion3();
                        UpdateDatabaseVersion4();
                        UpdateDatabaseVersion5();
                        UpdateDatabaseVersion6();
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "3":
                        // user_versionが3のときは､3から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion4();
                        UpdateDatabaseVersion5();
                        UpdateDatabaseVersion6();
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "4":
                        // user_versionが4のときは､4から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion5();
                        UpdateDatabaseVersion6();
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "5":
                        // user_versionが5のときは､5から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion6();
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "6":
                        // user_versionが6のときは､6から7に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion7();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "7":
                        // user_versionが7のときは最新のため更新不要
                        break;
                    default:
                        // user_versionが想定外のときは更新不要
                        break;
                }
            }
            else
            {
                // データベースファイルが見つからない場合は更新不要
            }
        }

        /// <summary>
        /// データベースファイル作成処理
        /// </summary>
        private static void CreateDatabase()
        {
            SQLiteConnection.CreateFile(_databaseFileName);

            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = _createTableCommand;
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = {_databaseUserVersion}";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();

            ConsoleWrapper.WriteLine(Resources.Strings.MessageCreateDatabase);
        }

        /// <summary>
        /// データベースuser_version取得処理
        /// </summary>
        /// <returns>データベースのuser_version</returns>
        private static string GetDatabaseUserVersion()
        {
            string version = string.Empty;

            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA user_version";
                version = command.ExecuteScalar().ToString();
            }
            connection.Close();

            return version;
        }

        /// <summary>
        /// データベースファイル更新処理(user_version1化)
        /// </summary>
        private static void UpdateDatabaseVersion1()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN ComputerManufacturer TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN ComputerModel TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 1";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理(user_version2化)
        /// </summary>
        private static void UpdateDatabaseVersion2()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN Processor TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 2";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理(user_version3化)
        /// </summary>
        private static void UpdateDatabaseVersion3()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN BIOSManufacturer TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN BIOSVersion TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 3";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理(user_version4化)
        /// </summary>
        private static void UpdateDatabaseVersion4()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN BitLockerStatus TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 4";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理(user_version5化)
        /// </summary>
        private static void UpdateDatabaseVersion5()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN AntiVirusSoftware TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 5";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理(user_version6化)
        /// </summary>
        private static void UpdateDatabaseVersion6()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN JavaVersioncheckResult TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 6";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        /// <summary>
        /// データベースファイル更新処理(user_version7化)
        /// </summary>
        private static void UpdateDatabaseVersion7()
        {
            using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "ALTER TABLE WindowsDeviceInfo ADD COLUMN InstallCheckResult TEXT";
                _ = command.ExecuteNonQuery();
                command.CommandText = $"PRAGMA user_version = 7";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
