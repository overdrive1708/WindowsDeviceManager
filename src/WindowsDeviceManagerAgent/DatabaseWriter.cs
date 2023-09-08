﻿using System.Data.SQLite;

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
        private static readonly string _databaseUserVersion = "1";

        /// <summary>
        /// SQLコマンド(テーブル作成)
        /// </summary>
        private static readonly string _createTableCommand = "CREATE TABLE IF NOT EXISTS WindowsDeviceInfo(HostName TEXT PRIMARY KEY, UserName TEXT, OSName TEXT, OSBuildNumber TEXT, OSVersion TEXT, ComputerManufacturer TEXT, ComputerModel TEXT, LastUpdate TEXT)";

        /// <summary>
        /// SQLコマンド(レコード登録)
        /// </summary>
        private static readonly string _insertCommand = "INSERT OR REPLACE INTO WindowsDeviceInfo(HostName, UserName, OSName, OSBuildNumber, OSVersion, ComputerManufacturer, ComputerModel, LastUpdate) VALUES(@p_HostName, @p_UserName, @p_OSName, @p_OSBuildNumber, @p_OSVersion, @p_ComputerManufacturer, @p_ComputerModel, @p_LastUpdate)";

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
                        // user_versionが0のときは､0から1に更新する
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageDetectOldDatabase);
                        UpdateDatabaseVersion1();
                        ConsoleWrapper.WriteLine(Resources.Strings.MessageUpdateDatabaseComplete);
                        break;
                    case "1":
                        // user_versionが1のときは最新のため更新不要
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
    }
}
