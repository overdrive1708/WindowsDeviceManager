﻿using CommandLine;
using CommandLine.Text;
using System.Reflection;

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

            // CommandLineParserのSentenceBuilderの多言語化を行う｡
            SentenceBuilder.Factory = () => new LocalizableSentenceBuilder();

            // Configを読み込む｡
            ConfigManager.ReadConfig();

            // コマンドライン引数を解析する｡
            _ = Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(HandleParseSuccess)
                .WithNotParsed(HandleParseError);
        }

        /// <summary>
        /// 例外ハンドラー登録処理
        /// </summary>
        private static void EntryExceptionHandler()
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
            // ConsoleWrapperの設定
            ConsoleWrapper.IsVerboseMode = opts.IsVerboseMode;

            // Welcomeメッセージの表示
            ConsoleWrapper.WriteLine(GetWelcomeMessage());

            // データベースファイルの更新
            DatabaseWriter.UpdateDatabase();

            // Windowsデバイス情報の収集
            ConsoleWrapper.WriteLine(Resources.Strings.MessageNowCollecting);
            WindowsDeviceInfo collectData = WindowsDeviceInfoCollector.GetWindowsDeviceInfo();
            DatabaseWriter.WriteWindowsDeviceInfoRecord(collectData);
            switch (opts.OutputFileType)
            {
                case CommandLineOptions.OutputFileNone:
                    ;
                    break;
                case CommandLineOptions.OutputFileJson:
                    JsonWriter.WriteWindowsDeviceInfoRecord(collectData);
                    break;
                case CommandLineOptions.OutputFileXml:
                    XmlWriter.WriteWindowsDeviceInfoRecord(collectData);
                    break;
                case CommandLineOptions.OutputFileCsv:
                    CsvWriter.WriteWindowsDeviceInfoRecord(collectData);
                    break;
                default:
                    ;
                    break;
            }
            ConsoleWrapper.WriteLine(Resources.Strings.MessageComplete);
            ConsoleWrapper.WriteLine(Resources.Strings.MessageThanks);

            // アプリケーションの終了待ち
            ConsoleWrapper.WriteLine(Resources.Strings.MessagePause);
            _ = ConsoleWrapper.ReadKey();

            // アプリケーションの終了
        }

        /// <summary>
        /// コマンドライン引数解析失敗時の処理
        /// </summary>
        /// <param name="errs">解析エラー</param>
        private static void HandleParseError(IEnumerable<CommandLine.Error> errs)
        {
            // 無処理
        }

        /// <summary>
        /// Welcomeメッセージ取得処理
        /// </summary>
        /// <returns>Welcomeメッセージ</returns>
        private static string GetWelcomeMessage()
        {
            // アセンブリ名とバージョン情報を取得してWelcomeメッセージを作成する｡
            Assembly assm = Assembly.GetExecutingAssembly();
            string assemblyName = assm.GetName().Name;
            string version = assm.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return ($"Welcome to {assemblyName} Ver.{version} !!");
        }
    }
}
