namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// CommandLineParser用オプションクラス
    /// </summary>
    internal class CommandLineOptions
    {
        // outputオプション解析設定･プロパティインターフェイス
        [CommandLine.Option('o', "output", Required = false, HelpText = "HelpTextOutput", ResourceType = typeof(Resources.Strings))]
        public string OutputFileType { get => _outputFileType; set => _outputFileType = value; }
        public const string OutputFileNone = "none";
        public const string OutputFileJson = "json";
        public const string OutputFileXml = "xml";
        public const string OutputFileCsv = "csv";
        private string _outputFileType = OutputFileNone;

        // verboseオプション解析設定･プロパティインターフェイス
        [CommandLine.Option('v', "verbose", Required = false, HelpText = "HelpTextVerbose", ResourceType = typeof(Resources.Strings))]
        public bool IsVerboseMode { get => _isVerboseMode; set => _isVerboseMode = value; }
        private bool _isVerboseMode = false;
    }
}
