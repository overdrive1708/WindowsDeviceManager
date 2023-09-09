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

## ダウンロード方法
[GitHubのReleases](https://github.com/overdrive1708/WindowsDeviceManager/releases)にあるLatestのAssetsよりWindowsDeviceManager_vx.x.x.zipをダウンロードしてください｡

WindowsDeviceManagerAgentフォルダはエンドユーザー用です｡エンドユーザーがアクセスできるところに展開してください｡

WindowsDeviceManagerViewerフォルダは管理者用です｡任意の場所に配置してください｡

フォルダ内のファイルはすべて必要なので､削除せず､フォルダごと配置してください｡

## 必要要件

### WindowsDeviceManagerAgent

ランタイム不要です｡Windows10/Windows11であれば動作します｡

### WindowsDeviceManagerViewer

[.NET デスクトップ ランタイム 6.x.x](https://dotnet.microsoft.com/ja-jp/download/dotnet/6.0)が必要です｡

予めダウンロード･インストールをお願いします｡

## 使用方法

### Windowsデバイス情報の収集
```
WindowsDeviceManagerAgent.exe [options]
options:[-v] [--verbose] [--help] [--version]
  -v, --verbose   処理の結果を詳細に表示します｡

  --help          このアプリのヘルプを表示します｡

  --version       このアプリのバージョン情報を表示します｡
```
WindowsDeviceManagerAgentフォルダ内のWindowsDeviceManagerAgent.exeを起動してください｡

WindowsDeviceInfo.dbにWindowsデバイス情報が記録されます｡

verboseオプションを指定しない場合､コマンドプロンプトが一瞬開いて何も表示されずに消えます｡

### Windowsデバイス情報収集結果の表示

WindowsDeviceManagerViewerフォルダ内のWindowsDeviceManagerViewer.exeを起動してください｡

データベースファイルを選択する画面が開くので､データベースファイル(WindowsDeviceInfo.db)を指定して開いてください｡

## 収集するWindowsデバイス情報と収集例
- ホスト名：hoge
- ユーザ名：hogehoge
- OS名：Microsoft Windows 11 Pro
- OSビルド番号：22621
- OSバージョン：22H2
- コンピュータの製造元：MouseComputer Co.,Ltd.
- コンピュータの製品名：NG-im610
- プロセッサ：Intel(R) Core(TM) i7-9700 CPU @ 3.00GHz
- BIOSの製造元：American Megatrends Inc.
- BIOSのバージョン：1.08
- BitLockerの状態：すべての固定ディスクが有効

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
