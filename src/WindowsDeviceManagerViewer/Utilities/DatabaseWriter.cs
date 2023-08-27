﻿using System.Collections.Generic;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

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
                        using SQLiteConnection connection = new($"Data Source = {databasefile}");
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
            else
            {
                // データベースファイルがない場合はエラーメッセージを表示してアプリケーションを終了する
                _ = MessageBox.Show(Resources.Strings.MessageErrorDatabaseNotFound,
                                    Resources.Strings.Error,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                Environment.Exit(1);
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
