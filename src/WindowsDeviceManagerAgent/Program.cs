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

            Console.WriteLine("Hello, World!");
        }

        /// <summary>
        /// 例外ハンドラー登録処理
        /// </summary>
        static void EntryExceptionHandler()
        {
            TaskScheduler.UnobservedTaskException += ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.OnUnhandledException;
        }
    }
}