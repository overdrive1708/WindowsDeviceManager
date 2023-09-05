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
        private static readonly string _databaseFileName = "WindowsDeviceInfo.db";

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
                command.CommandText = "INSERT OR REPLACE INTO WindowsDeviceInfo(HostName, UserName, OSName, OSBuildNumber, OSVersion, LastUpdate) VALUES(@p_HostName, @p_UserName, @p_OSName, @p_OSBuildNumber, @p_OSVersion, @p_LastUpdate)";
                _ = command.Parameters.Add(new SQLiteParameter("@p_HostName", writeValue.HostName));
                _ = command.Parameters.Add(new SQLiteParameter("@p_UserName", writeValue.UserName));
                _ = command.Parameters.Add(new SQLiteParameter("@p_OSName", writeValue.OSName));
                _ = command.Parameters.Add(new SQLiteParameter("@p_OSBuildNumber", writeValue.OSBuildNumber));
                _ = command.Parameters.Add(new SQLiteParameter("@p_OSVersion", writeValue.OSVersion));
                _ = command.Parameters.Add(new SQLiteParameter("@p_LastUpdate", writeValue.LastUpdate));
                command.Prepare();
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS WindowsDeviceInfo(HostName TEXT PRIMARY KEY, UserName TEXT, OSName TEXT, OSBuildNumber TEXT, OSVersion TEXT, LastUpdate TEXT)";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();

            ConsoleWrapper.WriteLine(Resources.Strings.MessageCreateDatabase);
        }
    }
}
