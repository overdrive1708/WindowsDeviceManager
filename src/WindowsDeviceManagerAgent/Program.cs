using CommandLine;

namespace WindowsDeviceManagerAgent
{
    internal class Program
    {
        /// <summary>
        /// メイン処理(エントリーポイント)
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        static void Main(string[] args)
        {
            // 未処理の例外が発生したときの処理を登録する｡
            EntryExceptionHandler();

            // コマンドライン引数を解析する｡
            _ = Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(HandleParseSuccess)
                .WithNotParsed(HandleParseError);
        }

        /// <summary>
        /// 例外ハンドラー登録処理
        /// </summary>
        static void EntryExceptionHandler()
        {
            TaskScheduler.UnobservedTaskException += ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.OnUnhandledException;
        }

        /// <summary>
        /// コマンドライン引数解析成功時の処理
        /// </summary>
        /// <param name="opts">解析結果</param>
        private static void HandleParseSuccess(CommandLineOptions opts)
        {
        }

        /// <summary>
        /// コマンドライン引数解析失敗時の処理
        /// </summary>
        /// <param name="errs">解析エラー</param>
        private static void HandleParseError(IEnumerable<CommandLine.Error> errs)
        {
            // 無処理
        }
    }
}
