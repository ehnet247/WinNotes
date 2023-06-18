using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Hardcodet.Wpf.TaskbarNotification;
using WinNotes.Config;
using ExpressionEncrypter;
using System.Configuration;
using System.IO;

namespace WinNotes.Notify
{
    /// <summary>
    /// Simple application. Check the XAML for comments.
    /// </summary>
    public partial class App : Application
    {
        public string FileName
        {
            get
            {
                string? fileName = ConfigurationManager.AppSettings["expressionsFileName"];
                if (!string.IsNullOrEmpty(fileName))
                {
                    return fileName;
                }
                else
                {
                    return "Expressions.notes";
                }
            }
        }

        private TaskbarIcon notifyIcon;

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
            notifyIcon.DataContext = new NotifyIconViewModel(ReadExpressions());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }

        private ExpressionCollection ReadExpressions()
        {
            Encrypter encrypter = new Encrypter();
            object? expressions = null;
            if (File.Exists(FileName))
            {
                encrypter.Read(FileName, out expressions, typeof(ExpressionCollection));
                if ((expressions != null) &&
                    (expressions.GetType() == typeof(ExpressionCollection)))
                {
                    return (ExpressionCollection)expressions;
                }
            }
            return null;
        }
    }
}
