using System.Xml.Serialization;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// XMLファイル書き込みクラス
    /// </summary>
    internal class XmlWriter
    {
        //--------------------------------------------------
        // 定数(コンフィギュレーション)
        //--------------------------------------------------
        /// <summary>
        /// データベースファイル名
        /// </summary>
        private static readonly string _fileName = "WindowsDeviceInfo.xml";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="writeValue">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecord(WindowsDeviceInfo writeValue)
        {
            // シリアライズ&ファイル出力
            XmlSerializer serializer = new(typeof(WindowsDeviceInfo));
            using StreamWriter swXml = new(_fileName, false, System.Text.Encoding.UTF8);
            serializer.Serialize(swXml, writeValue);
            swXml.Close();

            // 完了メッセージ表示
            ConsoleWrapper.WriteLine(Resources.Strings.MessageCreateXmlFile);
        }
    }
}
