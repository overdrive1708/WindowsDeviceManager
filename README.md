[日本語](README.md)

<h1 align="center">
    <a href="https://github.com/overdrive1708/WindowsDeviceManager">
        <img alt="WindowsDeviceManager" src="docs/images/AppIconReadme.png" width="50" height="50">
    </a><br>
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

[.NET デスクトップ ランタイム 8.x.x](https://dotnet.microsoft.com/ja-jp/download/dotnet/8.0)が必要です｡

予めダウンロード･インストールをお願いします｡

## 使用方法

### WindowsDeviceManagerAgentの設定

WindowsDeviceManagerAgent.exeの初回起動時にデフォルト値で設定ファイルが作成されます｡

※ファイル名はConfig.jsonです｡

※文字コードはUTF-8にしてください｡

必要に応じてConfig.jsonで設定を行ってください｡

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

| 設定項目 | 設定内容 |
| --- | --- |
| IsCollectUserName | ユーザ名の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectOSName | OS名の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectOSBuildNumber | OSビルド番号の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectOSVersion | OSバージョンの収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectComputerManufacturer | コンピュータの製造元の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectComputerModel | コンピュータの製品名の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectProcessor | プロセッサの収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectBIOSManufacturer | BIOSの製造元の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectBIOSVersion | BIOSのバージョンの収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectBitLockerStatus | BitLockerの状態の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectAntiVirusSoftware | アンチウィルスソフトウェアの収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectJavaVersioncheckResult | Javaのバージョンチェック結果の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| IsCollectInstallCheckResult | インストールチェック結果の収集有無設定<br>収集する場合はtrueを設定してください｡<br>収集しない場合はfalseを設定してください｡ |
| InstallCheckNameList | インストールチェックで検出したいアプリケーションの名前を記述してください｡<br>指定されない場合はインストールチェックは行われません｡ |
| InstallCheckPublisherList | インストールチェックで検出したいアプリケーションの発行元を記述してください｡<br>指定されない場合はインストールチェックは行われません｡ |

### Windowsデバイス情報の収集
```
WindowsDeviceManagerAgent.exe [options]
options:[-o json|xml|csv] [--output json|xml|csv] [-v] [--verbose] [--help] [--version]
  -o, --output [json|xml|csv]   処理の結果をファイルに出力します｡(json|xml|csv)

  -v, --verbose                 処理の結果を詳細に表示します｡

  --help                        このアプリのヘルプを表示します｡

  --version                     このアプリのバージョン情報を表示します｡
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
- アンチウィルスソフトウェア:Windows Defender
- Javaのバージョンチェック結果:openjdk version "22.0.2" 2024-07-16;OpenJDK Runtime Environment (build 22.0.2+9-70);OpenJDK 64-Bit Server VM (build 22.0.2+9-70, mixed mode, sharing);
- インストールチェック結果:未検出

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
  - Version 23H2

## 制限事項
- リモートデスクトップでログインしている時は､ユーザ名が取得できないため､｢不明｣と記録されます｡

## 開発環境
- Microsoft Visual Studio Community 2022

## 使用しているライブラリ
詳細は[NOTICE.md](NOTICE.md)を参照してください｡

## ライセンス
このプロジェクトはMITライセンスです。  
詳細は [LICENSE](LICENSE) を参照してください。

## 不具合報告と機能要望
[GitHubのIssue](https://github.com/overdrive1708/WindowsDeviceManager/issues/new/choose)より報告してください｡

## 作者
[overdrive1708](https://github.com/overdrive1708)
