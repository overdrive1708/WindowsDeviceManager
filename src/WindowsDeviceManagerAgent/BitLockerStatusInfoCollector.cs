using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace WindowsDeviceManagerAgent
{
    /// <summary>
    /// BitLocker状態情報収集クラス
    /// </summary>
    internal class BitLockerStatusInfoCollector
    {
        /// <summary>
        /// BitLocker状態情報収集処理
        /// </summary>
        /// <returns>BitLocker状態情報(固定ディスク数分)</returns>
        public static List<BitLockerStatusInfo> GetBitLockerStatusInfo()
        {
            List<BitLockerStatusInfo> bitLockerStatusInfos = new();

            // すべてのドライブの情報を取得
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                // 固定ディスクで｢Google Drive｣ではないドライブのBitLocker状態を取得
                if((drive.DriveType == DriveType.Fixed) && (drive.VolumeLabel != "Google Drive"))
                {
                    BitLockerStatusInfo bitLockerStatusInfo = new()
                    {
                        DriveLetter = drive.Name,
                        BitLockerStatus = GetDriveBitLockerStatus(drive.Name)
                    };
                    bitLockerStatusInfos.Add(bitLockerStatusInfo);
                }
            }

            return bitLockerStatusInfos;
        }

        /// <summary>
        /// BitLocker状態取得処理
        /// </summary>
        /// <param name="driveLetter">ドライブレター</param>
        /// <returns>BitLocker状態</returns>
        private static BitLockerStatusInfo.Status GetDriveBitLockerStatus(string driveLetter)
        {
            BitLockerStatusInfo.Status bitlockerStatus = BitLockerStatusInfo.Status.Unknown;

            IShellProperty property = ShellObject.FromParsingName(driveLetter).Properties.GetProperty("System.Volume.BitLockerProtection");
            int? bitLockerProtectionStatus = (property as ShellProperty<int?>).Value;

            if(bitLockerProtectionStatus.HasValue)
            {
                bitlockerStatus = bitLockerProtectionStatus switch
                {
                    1 => BitLockerStatusInfo.Status.On,
                    2 => BitLockerStatusInfo.Status.Off,
                    3 => BitLockerStatusInfo.Status.Encrypting,
                    4 => BitLockerStatusInfo.Status.Decrypting,
                    5 => BitLockerStatusInfo.Status.Suspended,
                    6 => BitLockerStatusInfo.Status.OnLocked,
                    8 => BitLockerStatusInfo.Status.WaitingForActivation,
                    _ => BitLockerStatusInfo.Status.Unknown,
                };
            }

            return bitlockerStatus;
        }
    }
}
