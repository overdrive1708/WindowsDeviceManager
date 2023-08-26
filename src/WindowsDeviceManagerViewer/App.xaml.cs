﻿using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using WindowsDeviceManagerViewer.Views;

namespace WindowsDeviceManagerViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        /// <summary>
        /// Startup処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            // 未処理の例外が発生したときの処理を登録する｡
            DispatcherUnhandledException += ExceptionHandler.OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.OnUnhandledException;
        }
    }
}
