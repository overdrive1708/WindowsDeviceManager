using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace WindowsDeviceManagerViewer.Utilities
{
    /// <summary>
    /// データベース書き込みクラス
    /// </summary>
    internal class DatabaseWriter
    {
        /// <summary>
        /// Windowsデバイス情報OSバージョン再判定処理
        /// </summary>
        /// <param name="databasefile">データベースファイル名</param>
        public static void RecheckWindowsDeviceInfoRecords(string databasefile)
        {
            if (File.Exists(databasefile))
            {
                // データベースファイルがある場合は読み込む
                List<WindowsDeviceInfo> readRecords = DatabaseReader.ReadWindowsDeviceInfoRecords(databasefile);
                foreach (WindowsDeviceInfo readRecord in readRecords)
                {
                    // OSバージョンが不明になっているものを再判定して更新する
                    if (readRecord.OSVersion.Contains(Resources.Strings.Unknown))
                    {
                        using SQLiteConnection connection = new($"Data Source = {databasefile.Replace(@"\\", @"\\\\")}");
                        connection.Open();
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE WindowsDeviceInfo SET OSVersion = @p_OSVersion, LastUpdate = @p_LastUpdate WHERE HostName = @p_HostName";
                            _ = command.Parameters.Add(new SQLiteParameter("@p_HostName", readRecord.HostName));
                            _ = command.Parameters.Add(new SQLiteParameter("@p_OSVersion", GetOSVersion(readRecord.OSName, readRecord.OSBuildNumber)));
                            _ = command.Parameters.Add(new SQLiteParameter("@p_LastUpdate", DateTime.Now.ToString()));
                            command.Prepare();
                            _ = command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Windowsデバイス情報Javaバージョン再判定処理
        /// </summary>
        /// <param name="databasefile">データベースファイル名</param>
        public static void RecheckJavaVersion(string databasefile)
        {
            if (File.Exists(databasefile))
            {
                // データベースファイルがある場合は読み込む
                List<WindowsDeviceInfo> readRecords = DatabaseReader.ReadWindowsDeviceInfoRecords(databasefile);
                foreach (WindowsDeviceInfo readRecord in readRecords)
                {
                    // Javaバージョンに｢JavaScript｣が含まれるものを再判定して更新する
                    if (readRecord.JavaVersioncheckResult.Contains("javascript", StringComparison.OrdinalIgnoreCase))
                    {
                        // Javaバージョンチェック結果の文字列から｢;｣区切りで項目を抜き出してJavaScript以外のものを書き直す
                        string[] beforeJavaVersioncheckResults = readRecord.JavaVersioncheckResult.Split(';');
                        string afterJavaVersioncheckResult = string.Empty;
                        bool isDetect = false;

                        foreach (string beforeJavaVersioncheckResult in beforeJavaVersioncheckResults)
                        {
                            if ((beforeJavaVersioncheckResult != string.Empty) && (!beforeJavaVersioncheckResult.Contains("javascript", StringComparison.OrdinalIgnoreCase)))
                            {
                                afterJavaVersioncheckResult += $"{beforeJavaVersioncheckResult};";
                                isDetect = true;
                            }
                        }

                        // 再判定した結果検出しなかった場合｢未検出｣にする
                        if (!isDetect)
                        {
                            afterJavaVersioncheckResult = Resources.Strings.Undetected;
                        }

                        // 再判定結果をDBに反映
                        using SQLiteConnection connection = new($"Data Source = {databasefile.Replace(@"\\", @"\\\\")}");
                        connection.Open();
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE WindowsDeviceInfo SET JavaVersioncheckResult = @p_JavaVersioncheckResult, LastUpdate = @p_LastUpdate WHERE HostName = @p_HostName";
                            _ = command.Parameters.Add(new SQLiteParameter("@p_HostName", readRecord.HostName));
                            _ = command.Parameters.Add(new SQLiteParameter("@p_JavaVersioncheckResult", afterJavaVersioncheckResult));
                            _ = command.Parameters.Add(new SQLiteParameter("@p_LastUpdate", DateTime.Now.ToString()));
                            command.Prepare();
                            _ = command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Windowsデバイス情報レコード削除処理
        /// </summary>
        /// <param name="databasefile">データベースファイル名</param>
        /// <param name="hostname">削除対象ホスト名</param>
        public static void DeleteWindowsDeviceInfoRecord(string databasefile, string hostname)
        {
            if (File.Exists(databasefile))
            {
                // データベースファイルがある場合はホスト名を指定して削除
                using SQLiteConnection connection = new($"Data Source = {databasefile.Replace(@"\\", @"\\\\")}");
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE from WindowsDeviceInfo WHERE HostName = @p_HostName";
                    _ = command.Parameters.Add(new SQLiteParameter("@p_HostName", hostname));
                    command.Prepare();
                    _ = command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// データベースクリーンアップ処理
        /// </summary>
        /// <param name="databasefile">データベースファイル名</param>
        public static void CleanupDatabase(string databasefile)
        {
            if (File.Exists(databasefile))
            {
                // データベースファイルがある場合はVACUUMコマンドを実行
                using SQLiteConnection connection = new($"Data Source = {databasefile.Replace(@"\\", @"\\\\")}");
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "VACUUM";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// OSバージョン取得処理
        /// </summary>
        /// <param name="osName">OS名</param>
        /// <param name="osBuildNumber">OSビルド番号</param>
        /// <returns>OSバージョン</returns>
        private static string GetOSVersion(string osName, string osBuildNumber)
        {
            string osVersion = Resources.Strings.Unknown;

            if (osName.Contains("Windows 10"))
            {
                // Windows10の場合のビルド番号=>バージョン情報変換
                osVersion = osBuildNumber switch
                {
                    "10240" => "1507",
                    "10586" => "1511",
                    "14393" => "1607",
                    "15063" => "1703",
                    "16299" => "1709",
                    "17134" => "1803",
                    "17763" => "1809",
                    "18362" => "1903",
                    "18363" => "1909",
                    "19041" => "2004",
                    "19042" => "20H2",
                    "19043" => "21H1",
                    "19044" => "21H2",
                    "19045" => "22H2",
                    _ => $"{Resources.Strings.Unknown}(OS Build:{osBuildNumber})"
                };
            }
            else if (osName.Contains("Windows 11"))
            {
                // Windows11の場合のビルド番号=>バージョン情報変換
                osVersion = osBuildNumber switch
                {
                    "22000" => "21H2",
                    "22621" => "22H2",
                    "22631" => "23H2",
                    "26100" => "24H2",
                    "26200" => "25H2",
                    _ => $"{Resources.Strings.Unknown}(OS Build:{osBuildNumber})"
                };
            }
            else
            {
                // Windows10/11以外の場合は無処理
            }

            return osVersion;
        }
    }
}
