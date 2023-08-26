[日本語](README.md)

<h1 align="center">
    WindowsDeviceManager
</h1>

<h2 align="center">
    Windowsデバイス情報収集･管理アプリケーション
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

<h1 align="center">
    <span style="color: red; ">現在開発中 以下予定情報</span>
</h1>

## ダウンロード方法
[GitHubのReleases](https://github.com/overdrive1708/WindowsDeviceManager/releases)にあるLatestのAssetsよりWindowsDeviceManager_vx.x.x.zipをダウンロードしてください｡

## 必要要件
WindowsDeviceManagerViewer.exeの実行には[.NET デスクトップ ランタイム 6.x.x](https://dotnet.microsoft.com/ja-jp/download/dotnet/6.0)が必要です｡

予めダウンロード･インストールをお願いします｡

## 使用方法

### Windowsデバイス情報の収集
```
WindowsDeviceManagerAgent.exe [options]
options:[-v] [--verbose] [--help] [--version]
  -v, --verbose    処理の結果を詳細に表示します.

  --help          このアプリのヘルプを表示します.

  --version       このアプリのバージョン情報を表示します.
```

### Windowsデバイス情報収集結果の表示

WindowsDeviceManagerViewer.exeを起動してデータベースファイルを読み込んでください｡

## サポートするOSバージョン
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

## 開発環境
- Microsoft Visual Studio Community 2022

## 使用しているライブラリ
詳細は[GitHubのDependency graph](https://github.com/overdrive1708/WindowsDeviceManager/network/dependencies)か､各プロジェクトの"*.csproj"を参照してください｡

## ライセンス
このプロジェクトはMITライセンスです。  
詳細は [LICENSE](LICENSE) を参照してください。

## 不具合報告と機能要望
[GitHubのIssue](https://github.com/overdrive1708/WindowsDeviceManager/issues/new/choose)より報告してください｡

## 作者
[overdrive1708](https://github.com/overdrive1708)
