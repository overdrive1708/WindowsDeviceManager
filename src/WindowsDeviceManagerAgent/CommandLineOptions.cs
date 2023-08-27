namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// CommandLineParser用オプションクラス
    /// </summary>
    internal class CommandLineOptions
    {
        // verboseオプション解析設定･プロパティインターフェイス
        [CommandLine.Option('v', "verbose", Required = false, HelpText = "HelpTextVerbose", ResourceType = typeof(Resources.Strings))]
        public bool IsVerboseMode { get => _isVerboseMode; set => _isVerboseMode = value; }
        private bool _isVerboseMode = false;
    }
}
