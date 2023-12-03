using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        /// <summary>
        /// JSONファイルの拡張子
        /// </summary>
        private const string JSONFileExtensionList = "*.json";

        /// <summary>
        /// 出力JSONファイル名
        /// </summary>
        private static readonly string _outputJsonFileName = "WindowsDeviceInfo.json";

        /// <summary>
        /// XMLファイルの拡張子
        /// </summary>
        private const string XMLFileExtensionList = "*.xml";

        /// <summary>
        /// 出力XMLファイル名
        /// </summary>
        private static readonly string _outputXmlFileName = "WindowsDeviceInfo.xml";

        /// <summary>
        /// CSVファイルの拡張子
        /// </summary>
        private const string CSVFileExtensionList = "*.csv";

        /// <summary>
        /// 出力CSVファイル名
        /// </summary>
        private static readonly string _outputCsvFileName = "WindowsDeviceInfo.csv";

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
        /// コピーライト
        /// </summary>
        private string _copyright;
        public string Copyright
        {
            get { return _copyright; }
            set { SetProperty(ref _copyright, value); }
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

        /// <summary>
        /// 表示データリロードコマンド
        /// </summary>
        private DelegateCommand _commandReloadDisplayData;
        public DelegateCommand CommandReloadDisplayData =>
            _commandReloadDisplayData ?? (_commandReloadDisplayData = new DelegateCommand(ExecuteCommandReloadDisplayData));

        /// <summary>
        /// JSON出力コマンド
        /// </summary>
        private DelegateCommand _commandOutputJson;
        public DelegateCommand CommandOutputJson =>
            _commandOutputJson ?? (_commandOutputJson = new DelegateCommand(ExecuteCommandOutputJson));

        /// <summary>
        /// XML出力コマンド
        /// </summary>
        private DelegateCommand _commandOutputXml;
        public DelegateCommand CommandOutputXml =>
            _commandOutputXml ?? (_commandOutputXml = new DelegateCommand(ExecuteCommandOutputXml));

        /// <summary>
        /// CSV出力コマンド
        /// </summary>
        private DelegateCommand _commandOutputCsv;
        public DelegateCommand CommandOutputCsv =>
            _commandOutputCsv ?? (_commandOutputCsv = new DelegateCommand(ExecuteCommandOutputCsv));

        /// <summary>
        /// URLを開く
        /// </summary>
        private DelegateCommand<string> _commandOpenUrl;
        public DelegateCommand<string> CommandOpenUrl =>
            _commandOpenUrl ?? (_commandOpenUrl = new DelegateCommand<string>(ExecuteCommandOpenUrl));

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            Assembly assm = Assembly.GetExecutingAssembly();
            string version = assm.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            // バージョン情報を取得してタイトルに反映する
            Title = $"{Resources.Strings.ApplicationName} Ver.{version}";

            // コピーライト情報を取得して設定
            Copyright = assm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
        }

        /// <summary>
        /// 表示データ作成コマンド実行処理
        /// </summary>
        private void ExecuteCommandCreateDispData()
        {
            _databaseFileName = GetDatabaseFileName();
            CreateWindowsDeviceInfoCollectData();
        }

        /// <summary>
        /// データベースクリーンアップコマンド実行処理
        /// </summary>
        private void ExecuteCommandCleanupDatabase()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合はクリーンアップ
                DatabaseWriter.CleanupDatabase(_databaseFileName);
                
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
        private void ExecuteCommandRecheckOSVersion()
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
        /// 表示データリロードコマンド実行処理
        /// </summary>
        private void ExecuteCommandReloadDisplayData()
        {
            // データベースファイルの再読み込み
            CreateWindowsDeviceInfoCollectData();

            // 完了メッセージの表示
            _ = MessageBox.Show(Resources.Strings.MessageReloadComplete,
                                Resources.Strings.Notice,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
        }

        /// <summary>
        /// JSON出力コマンド実行処理
        /// </summary>
        private void ExecuteCommandOutputJson()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合は保存先を確認して出力処理を行う
                string jsonFileName = GetJsonFileName();

                if (jsonFileName != string.Empty)
                {
                    // 出力先が指定されている場合はJSON出力を行い完了メッセージを表示する
                    List<WindowsDeviceInfo> readRecords = DatabaseReader.ReadWindowsDeviceInfoRecords(_databaseFileName);
                    JsonWriter.WriteWindowsDeviceInfoRecords(jsonFileName, readRecords);
                    _ = MessageBox.Show(Resources.Strings.MessageOutputJsonComplete,
                                    Resources.Strings.Notice,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    // 出力先が指定されていない場合はエラーメッセージを表示して処理を終わる
                    _ = MessageBox.Show(Resources.Strings.MessageErrorOutputJsonFileNotSelected,
                                Resources.Strings.Notice,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                }
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
        /// XML出力コマンド実行処理
        /// </summary>
        private void ExecuteCommandOutputXml()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合は保存先を確認して出力処理を行う
                string xmlFileName = GetXmlFileName();

                if (xmlFileName != string.Empty)
                {
                    // 出力先が指定されている場合はXML出力を行い完了メッセージを表示する
                    List<WindowsDeviceInfo> readRecords = DatabaseReader.ReadWindowsDeviceInfoRecords(_databaseFileName);
                    XmlWriter.WriteWindowsDeviceInfoRecords(xmlFileName, readRecords);
                    _ = MessageBox.Show(Resources.Strings.MessageOutputXmlComplete,
                                    Resources.Strings.Notice,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    // 出力先が指定されていない場合はエラーメッセージを表示して処理を終わる
                    _ = MessageBox.Show(Resources.Strings.MessageErrorOutputXmlFileNotSelected,
                                Resources.Strings.Notice,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                }
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
        /// CSV出力コマンド実行処理
        /// </summary>
        private void ExecuteCommandOutputCsv()
        {
            if (File.Exists(_databaseFileName))
            {
                // データベースファイルがある場合は保存先を確認して出力処理を行う
                string csvFileName = GetCsvFileName();

                if(csvFileName != string.Empty)
                {
                    // 出力先が指定されている場合はCSV出力を行い完了メッセージを表示する
                    List<WindowsDeviceInfo> readRecords = DatabaseReader.ReadWindowsDeviceInfoRecords(_databaseFileName);
                    CsvWriter.WriteWindowsDeviceInfoRecords(csvFileName, readRecords);
                    _ = MessageBox.Show(Resources.Strings.MessageOutputCsvComplete,
                                    Resources.Strings.Notice,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    // 出力先が指定されていない場合はエラーメッセージを表示して処理を終わる
                    _ = MessageBox.Show(Resources.Strings.MessageErrorOutputCsvFileNotSelected,
                                Resources.Strings.Notice,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                }
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
        /// URLを開くコマンド実行処理
        /// </summary>
        private void ExecuteCommandOpenUrl(string url)
        {
            ProcessStartInfo psi = new()
            {
                FileName = url,
                UseShellExecute = true,
            };
            Process.Start(psi);
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

        /// <summary>
        /// 出力JSONファイルファイル名取得処理
        /// </summary>
        /// <returns>出力JSONファイル名</returns>
        private static string GetJsonFileName()
        {
            // CommonSaveFileDialogを使ってユーザにファイルを選択させる
            using CommonSaveFileDialog dialog = new()
            {
                Title = Resources.Strings.MessageSelectOutputJsonFile,
                DefaultFileName = _outputJsonFileName
            };
            dialog.Filters.Add(new CommonFileDialogFilter(Resources.Strings.JSONFile, JSONFileExtensionList));

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
        /// 出力XMLファイルファイル名取得処理
        /// </summary>
        /// <returns>出力XMLファイル名</returns>
        private static string GetXmlFileName()
        {
            // CommonSaveFileDialogを使ってユーザにファイルを選択させる
            using CommonSaveFileDialog dialog = new()
            {
                Title = Resources.Strings.MessageSelectOutputXmlFile,
                DefaultFileName = _outputXmlFileName
            };
            dialog.Filters.Add(new CommonFileDialogFilter(Resources.Strings.XMLFile, XMLFileExtensionList));

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
        /// 出力CSVファイルファイル名取得処理
        /// </summary>
        /// <returns>出力CSVファイル名</returns>
        private static string GetCsvFileName()
        {
            // CommonSaveFileDialogを使ってユーザにファイルを選択させる
            using CommonSaveFileDialog dialog = new()
            {
                Title = Resources.Strings.MessageSelectOutputCsvFile,
                DefaultFileName = _outputCsvFileName
            };
            dialog.Filters.Add(new CommonFileDialogFilter(Resources.Strings.CSVFile, CSVFileExtensionList));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
