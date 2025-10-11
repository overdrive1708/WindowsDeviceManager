[日本語](README.md) | [English](README-en.md)

<h1 align="center">
    <a href="https://github.com/overdrive1708/WindowsDeviceManager">
        <img alt="WindowsDeviceManager" src="docs/images/AppIconReadme.png" width="50" height="50">
    </a><br>
    WindowsDeviceManager
</h1>

<h2 align="center">
    Windows device information collection and management application
</h2>

<div align="center">
    <img alt="csharp" src="https://img.shields.io/badge/csharp-blue.svg?style=plastic&logo=csharp">
    <img alt="dotnet" src="https://img.shields.io/badge/.NET-blue.svg?style=plastic&logo=dotnet">
    <img alt="license" src="https://img.shields.io/github/license/overdrive1708/WindowsDeviceManager?style=plastic">
    <br>
    <img alt="repo size" src="https://img.shields.io/github/repo-size/overdrive1708/WindowsDeviceManager?style=plastic&logo=github">
    <img alt="release" src="https://img.shields.io/github/release/overdrive1708/WindowsDeviceManager?style=plastic&logo=github">
    <img alt="download" src="https://img.shields.io/github/downloads/overdrive1708/WindowsDeviceManager/total?style=plastic&logo=github&color=brightgreen">
    <img alt="open issues" src="https://img.shields.io/github/issues-raw/overdrive1708/WindowsDeviceManager?style=plastic&logo=github&color=brightgreen">
    <img alt="closed issues" src="https://img.shields.io/github/issues-closed-raw/overdrive1708/WindowsDeviceManager?style=plastic&logo=github&color=brightgreen">
</div>

## How to download

### WindowsDeviceManagerAgent

Download WindowsDeviceManagerAgent_vx.x.x.zip from Latest Assets in [GitHub Releases](https://github.com/overdrive1708/WindowsDeviceManager/releases).

This application is for end users. Extract it to a location that end users can access.

All files in the folder are necessary, so do not delete them, but place the whole folder.

### WindowsDeviceManagerViewer

Download WindowsDeviceManagerViewer_vx.x.x.zip from Latest Assets in [GitHub Releases](https://github.com/overdrive1708/WindowsDeviceManager/releases).

This application is for administrators. Place it anywhere you like.

All files in the folder are necessary, so do not delete them, but place the whole folder.

## Requirements

### WindowsDeviceManagerAgent

No runtime required. Works with Windows 10/Windows 11.

### WindowsDeviceManagerViewer

[.NET Desktop Runtime 8.x.x](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is required.

Please download and install in advance.

## How to use

### WindowsDeviceManagerAgent settings

A configuration file will be created with default values when WindowsDeviceManagerAgent.exe is started for the first time.

\* The file name is Config.json.

\* The character code should be UTF-8.

Configure Config.json as necessary.

```JSON
{
  "IsCollectUserName": true,
  "IsCollectOSName": true,
  "IsCollectOSBuildNumber": true,
  "IsCollectOSVersion": true,
  "IsCollectComputerManufacturer": true,
  "IsCollectComputerModel": true,
  "IsCollectProcessor": true,
  "IsCollectBIOSManufacturer": true,
  "IsCollectBIOSVersion": true,
  "IsCollectBitLockerStatus": true,
  "IsCollectAntiVirusSoftware": true,
  "IsCollectJavaVersioncheckResult": true,
  "IsCollectInstallCheckResult": true,
  "InstallCheckNameList": [],
  "InstallCheckPublisherList": []
}
```

| Setting item | Setting contents |
| --- | --- |
| IsCollectUserName | Set whether to collect user name<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectOSName | Set whether to collect OS name<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectOSBuildNumber | Set whether to collect OS build number<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectOSVersion | Set whether to collect OS version<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectComputerManufacturer | Set whether to collect computer manufacturer<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectComputerModel | Set whether to collect computer product name<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectProcessor | Whether to collect processor information<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectBIOSManufacturer | Whether to collect BIOS manufacturer information<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectBIOSVersion | Whether to collect BIOS version information<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectBitLockerStatus | Whether to collect BitLocker status information<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectAntiVirusSoftware | Whether to collect antivirus software information<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectJavaVersioncheckResult | Whether to collect Java version check results<br>If you want to collect, set it to true.<br>If you don't want to collect, set it to false. |
| IsCollectInstallCheckResult | Whether to collect install check results<br>If you want to collect them, set it to true.<br>If you do not want to collect them, set it to false. |
| InstallCheckNameList | Please enter the names of the applications you want to detect in the install check.<br>If not specified, the install check will not be performed. <br>e.g.<br>"InstallCheckNameList": [<br>"AppName1",<br>"AppName2"<br>],|
| InstallCheckPublisherList | Please enter the publishers of the applications you want to detect in the install check.<br>If not specified, the install check will not be performed. <br>e.g.<br>"InstallCheckPublisherList": [<br>"AppPublisher1",<br>"AppPublisher2"<br>]|

### Collecting Windows device information
```
WindowsDeviceManagerAgent.exe [options]
options:[-o json|xml|csv] [--output json|xml|csv] [-v] [--verbose] [--help] [--version]
  -o, --output [json|xml|csv]   Output the results of the process to the file.(--output [json|xml|csv])

  -v, --verbose                 The results of the processing are displayed in detail.

  --help                        Display this help screen.

  --version                     Display version information.
```
Please start WindowsDeviceManagerAgent.exe in the WindowsDeviceManagerAgent folder.

Windows device information will be recorded in WindowsDeviceInfo.db.

If you do not specify the verbose option, the command prompt will open for a moment and then disappear without displaying anything.

### Displaying Windows device information collection results

Launch WindowsDeviceManagerViewer.exe in the WindowsDeviceManagerViewer folder.

A screen for selecting a database file will open, so specify the database file (WindowsDeviceInfo.db) and open it.

## Windows device information to collect and collection example
- Host name: hoge
- User name: hogehoge
- OS name: Microsoft Windows 11 Pro
- OS build number: 22621
- OS version: 22H2
- Computer manufacturer: MouseComputer Co.,Ltd.
- Computer model: NG-im610
- Processor: Intel(R) Core(TM) i7-9700 CPU @ 3.00GHz
- BIOS manufacturer: American Megatrends Inc.
- BIOS version: 1.08
- BitLocker status: All fixed disks are enable
- Antivirus software: Windows Defender
- Java version check result: openjdk version "22.0.2" 2024-07-16;OpenJDK Runtime Environment (build 22.0.2+9-70);OpenJDK 64-Bit Server VM (build 22.0.2+9-70, mixed mode, sharing);
- Installation check result: Not detected

## Supported OS versions
- Windows10
  - Version 1507
  - Version 1511
  - Version 1607
  - Version 1703
  - Version 1709
  - Version 1803
  - Version 1809
  - Version 1903
  - Version 1909
  - Version 2004
  - Version 20H2
  - Version 21H1
  - Version 21H2
  - Version 22H2
- Windows11
  - Version 21H2
  - Version 22H2
  - Version 23H2
  - Version 24H2
  - Version 25H2

## Restrictions
- When logged in via remote desktop, the user name cannot be obtained and is recorded as "Unknown".

## Development environment
- Microsoft Visual Studio Community 2022

## Libraries used
See [NOTICE.md](NOTICE.md) for details.

## License
This project is licensed under the MIT license.
See [LICENSE](LICENSE) for details.

## Bug reports and feature requests
Please report via [GitHub Issue](https://github.com/overdrive1708/WindowsDeviceManager/issues/new/choose).

## 作者
[overdrive1708](https://github.com/overdrive1708)
