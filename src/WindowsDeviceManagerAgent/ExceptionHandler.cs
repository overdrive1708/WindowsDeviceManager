namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// 例外ハンドラークラス
    /// </summary>
    public class ExceptionHandler
    {
        //--------------------------------------------------
        // 定数
        //--------------------------------------------------
        /// <summary>
        /// 例外情報記録ファイル
        /// </summary>
        private const string FatalErrorInformationPath = @"FatalErrorInformation.log";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// UnobservedTaskExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) => HandleException(e.Exception.InnerException);

        /// <summary>
        /// UnhandledExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException((Exception)e.ExceptionObject);

        /// <summary>
        /// 例外発生時の処理
        /// </summary>
        /// <param name="e">例外情報</param>
        private static void HandleException(Exception exception)
        {
            // 例外の詳細情報をファイルに出力する｡
            using (StreamWriter fatalErrorInformationFile = new(FatalErrorInformationPath, true, System.Text.Encoding.UTF8))
            {
                fatalErrorInformationFile.WriteLine($"=========={DateTime.Now}==========");
                fatalErrorInformationFile.WriteLine(exception.ToString());
                fatalErrorInformationFile.Close();
            }

            // 例外が発生したことをコンソールに出力する｡
            ConsoleWrapper.WriteErrorLine(Resources.Strings.ImportantNotice);
            ConsoleWrapper.WriteErrorLine(Resources.Strings.MessageFatalError);

            // アプリケーションの終了待ち
            ConsoleWrapper.WriteLine(Resources.Strings.MessagePause);
            _ = ConsoleWrapper.ReadKey();

            // 終了する｡
            Environment.Exit(1);
        }
    }
}
