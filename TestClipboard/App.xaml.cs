﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WinNotes.Clipboard;

namespace TestClipboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = new WindowClipboard();
            window.Show();
            var dataContext = window.DataContext as ClipboardViewModel;
            if (dataContext != null)
            {
                var expressionsFileName = ConfigurationManager.AppSettings["expressionsFileName"];
                dataContext.SetFileName(expressionsFileName);
            }
        }
    }
}
