using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using WindowsDeviceManagerViewer.Utilities;

namespace WindowsDeviceManagerViewer.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
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
        /// データベースファイル存在有無
        /// </summary>
        private bool _isExistDatabaseFile = false;
        public bool IsExistDatabaseFile
        {
            get { return _isExistDatabaseFile; }
            set { SetProperty(ref _isExistDatabaseFile, value); }
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
        /// 表示データ作成
        /// </summary>
        private DelegateCommand _commandCreateDisplayData;
        public DelegateCommand CommandCreateDispData =>
            _commandCreateDisplayData ?? (_commandCreateDisplayData = new DelegateCommand(ExecuteCommandCreateDispData));

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
            // TODO:DB選択､データ生成
            IsExistDatabaseFile = true;
        }
    }
}
