using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WindowsDeviceManagerViewer.Utilities
{
    /// <summary>
    /// XMLファイル書き込みクラス
    /// </summary>
    internal class XmlWriter
    {
        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Windowsデバイス情報書き込み処理
        /// </summary>
        /// <param name="outputXmlFile">出力XMLファイル名</param>
        /// <param name="windowsDeviceInfos">Windowsデバイス情報</param>
        public static void WriteWindowsDeviceInfoRecords(string outputXmlFile, List<WindowsDeviceInfo> windowsDeviceInfos)
        {
            // シリアライズ&ファイル出力
            XmlSerializer serializer = new(typeof(List<WindowsDeviceInfo>));
            using StreamWriter swXml = new(outputXmlFile, false, System.Text.Encoding.UTF8);
            serializer.Serialize(swXml, windowsDeviceInfos);
            swXml.Close();
        }
    }
}
