namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// BitLocker状態情報クラス
    /// </summary>
    public class BitLockerStatusInfo
    {
        /// <summary>
        /// ドライブレター
        /// </summary>
        private string _driveLetter = string.Empty;
        public string DriveLetter { get => _driveLetter; set => _driveLetter = value; }

        /// <summary>
        /// BitLocker状態
        /// </summary>
        private Status _bitLockerStatus = Status.Unknown;
        public Status BitLockerStatus { get => _bitLockerStatus; set => _bitLockerStatus = value; }
        public enum Status
        {
            /// <summary>
            /// 有効
            /// </summary>
            On,

            /// <summary>
            /// 無効
            /// </summary>
            Off,

            /// <summary>
            /// 暗号化中
            /// </summary>
            Encrypting,

            /// <summary>
            /// 復号化中
            /// </summary>
            Decrypting,

            /// <summary>
            /// 中断
            /// </summary>
            Suspended,

            /// <summary>
            /// 有効(ロック)
            /// </summary>
            OnLocked,

            /// <summary>
            /// アクティブ化を待機中
            /// </summary>
            WaitingForActivation,

            /// <summary>
            /// 不明
            /// </summary>
            Unknown
        }
    }
}
