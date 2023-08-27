using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using WindowsDeviceManagerViewer.Utilities;

namespace WindowsDeviceManagerViewer.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //--------------------------------------------------
        // 定数(コンフィギュレーション)
        //--------------------------------------------------
        /// <summary>
        /// データベースファイルの拡張子
        /// </summary>
        private const string SQLiteDatabaseFileExtensionList = "*.db,*.sqlite,*.sqlite3";

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// データベースファイル名
        /// </summary>
        private string _databaseFileName = string.Empty;

        //--------------------------------------------------
        // バインディングデータ
        //--------------------------------------------------
        /// <summary>
        /// タイトル
        /// </summary>
        private string _title = Resources.Strings.ApplicationName;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Windowsデバイス情報
        /// </summary>
        private ObservableCollection<WindowsDeviceInfo> _windowsDeviceInfoCollectData = new();
        public ObservableCollection<WindowsDeviceInfo> WindowsDeviceInfoCollectData
        {
            get { return _windowsDeviceInfoCollectData; }
            set { SetProperty(ref _windowsDeviceInfoCollectData, value); }
        }

        /// <summary>
        /// Windowsデバイス情報読み込み完了フラグ
        /// </summary>
        private bool _isCompleteReadWindowsDeviceInfo = false;
        public bool IsCompleteReadWindowsDeviceInfo
        {
            get { return _isCompleteReadWindowsDeviceInfo; }
            set { SetProperty(ref _isCompleteReadWindowsDeviceInfo, value); }
        }

        /// <summary>
        /// 表示項目設定：ホスト名
        /// </summary>
        private bool _isDisplayHostName = true;
        public bool IsDisplayHostName
        {
            get { return _isDisplayHostName; }
            set { SetProperty(ref _isDisplayHostName, value); }
        }

        /// <summary>
        /// 表示項目設定：ユーザ名
        /// </summary>
        private bool _isDisplayUserName = true;
        public bool IsDisplayUserName
        {
            get { return _isDisplayUserName; }
            set { SetProperty(ref _isDisplayUserName, value); }
        }

        /// <summary>
        /// 表示項目設定：OS名
        /// </summary>
        private bool _isDisplayOSName = true;
        public bool IsDisplayOSName
        {
            get { return _isDisplayOSName; }
            set { SetProperty(ref _isDisplayOSName, value); }
        }

        /// <summary>
        /// 表示項目設定：OSビルド番号
        /// </summary>
        private bool _isDisplayOSBuildNumber = true;
        public bool IsDisplayOSBuildNumber
        {
            get { return _isDisplayOSBuildNumber; }
            set { SetProperty(ref _isDisplayOSBuildNumber, value); }
        }

        /// <summary>
        /// 表示項目設定：OSバージョン
        /// </summary>
        private bool _isDisplayOSVersion = true;
        public bool IsDisplayOSVersion
        {
            get { return _isDisplayOSVersion; }
            set { SetProperty(ref _isDisplayOSVersion, value); }
        }

        /// <summary>
        /// 表示項目設定：最終更新日時
        /// </summary>
        private bool _isDisplayLastUpdate = true;
        public bool IsDisplayLastUpdate
        {
            get { return _isDisplayLastUpdate; }
            set { SetProperty(ref _isDisplayLastUpdate, value); }
        }

        //--------------------------------------------------
        // バインディングコマンド
        //--------------------------------------------------
        /// <summary>
        /// 表示データ作成コマンド
        /// </summary>
        private DelegateCommand _commandCreateDisplayData;
        public DelegateCommand CommandCreateDispData =>
            _commandCreateDisplayData ?? (_commandCreateDisplayData = new DelegateCommand(ExecuteCommandCreateDispData));

        /// <summary>
        /// データベースクリーンアップコマンド
        /// </summary>
        private DelegateCommand _commandCleanupDatabase;
        public DelegateCommand CommandCleanupDatabase =>
            _commandCleanupDatabase ?? (_commandCleanupDatabase = new DelegateCommand(ExecuteCommandCleanupDatabase));

        /// <summary>
        /// OSバージョン再判定コマンド
        /// </summary>
        private DelegateCommand _commandRecheckOSVersion;
        public DelegateCommand CommandRecheckOSVersion =>
            _commandRecheckOSVersion ?? (_commandRecheckOSVersion = new DelegateCommand(ExecuteCommandRecheckOSVersion));

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// 表示データ作成コマンド実行処理
        /// </summary>
        public void ExecuteCommandCreateDispData()
        {
            _databaseFileName = GetDatabaseFileName();
            CreateWindowsDeviceInfoCollectData();
        }

        /// <summary>
        /// データベースクリーンアップコマンド実行処理
        /// </summary>
        void ExecuteCommandCleanupDatabase()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合はVACUUMコマンドを実行
                using SQLiteConnection connection = new($"Data Source = {_databaseFileName}");
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "VACUUM";
                    command.ExecuteNonQuery();
                }
                connection.Close();

                // データベースファイルの再読み込み
                CreateWindowsDeviceInfoCollectData();

                // 完了メッセージの表示
                _ = MessageBox.Show(Resources.Strings.MessageCleanComplete,
                                    Resources.Strings.Notice,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
            }
            else
            {
                // データベースファイルがない場合はエラーメッセージを表示して処理を終わる
                _ = MessageBox.Show(Resources.Strings.MessageErrorDatabaseNotFound,
                                    Resources.Strings.Error,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// OSバージョン再判定コマンド実行処理
        /// </summary>
        void ExecuteCommandRecheckOSVersion()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合はOSバージョンの再判定を行う
                DatabaseWriter.RecheckWindowsDeviceInfoRecords(_databaseFileName);

                // データベースファイルの再読み込み
                CreateWindowsDeviceInfoCollectData();

                // 完了メッセージの表示
                _ = MessageBox.Show(Resources.Strings.MessageRecheckOSVersionComplete,
                                    Resources.Strings.Notice,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
            }
            else
            {
                // データベースファイルがない場合はエラーメッセージを表示して処理を終わる
                _ = MessageBox.Show(Resources.Strings.MessageErrorDatabaseNotFound,
                                    Resources.Strings.Error,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// データベースファイルファイル名取得処理
        /// </summary>
        /// <returns>データベースファイル名</returns>
        private static string GetDatabaseFileName()
        {
            // CommonOpenFileDialogを使ってユーザにファイルを選択させる
            using CommonOpenFileDialog dialog = new()
            {
                Title = Resources.Strings.MessageSelectDatabaseFile
            };
            dialog.Filters.Add(new CommonFileDialogFilter(Resources.Strings.SQLiteDatabaseFile, SQLiteDatabaseFileExtensionList));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Windowsデバイス情報生成処理
        /// </summary>
        private void CreateWindowsDeviceInfoCollectData()
        {
            WindowsDeviceInfoCollectData.Clear();
            
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合は読み込み表示する
                List<WindowsDeviceInfo> readRecords = DatabaseReader.ReadWindowsDeviceInfoRecords(_databaseFileName);
                foreach (WindowsDeviceInfo readRecord in readRecords)
                {
                    WindowsDeviceInfoCollectData.Add(readRecord);
                }
                IsCompleteReadWindowsDeviceInfo = true;
            }
            else
            {
                // データベースファイルがない場合はエラーメッセージを表示してアプリケーションを終了する
                IsCompleteReadWindowsDeviceInfo = false;
                _ = MessageBox.Show(Resources.Strings.MessageErrorDatabaseNotFound,
                                    Resources.Strings.Error,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }
    }
}
