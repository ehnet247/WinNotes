using CommunityToolkit.Mvvm.ComponentModel;
using ExpressionEncrypter;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WinNotes.Config;

namespace WinNotes.Notify
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel : ObservableRecipient
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                bool canExecuteFunc = Application.Current.MainWindow == null;
                return new DelegateCommand
                {
                    CanExecuteFunc = () => canExecuteFunc,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new ConfigWindow();
                        OnPropertyChanged(nameof(ShowWindowCommand));
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }
        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowConfigWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new ConfigWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }


        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()};
            }
        }

        public NotifyIconViewModel(ExpressionCollection expressions)
        {
            var expressionsMenuItems = new ObservableCollection<MenuItemViewModel>();
            if (expressions.Count > 0)
            {
                foreach (var expression in expressions)
                {
                    var menuItem = new MenuItemViewModel(expression.Name, expression.ClipboardCommand);
                    expressionsMenuItems.Add(menuItem);
                }
            }
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { Header = "Config" }
            };
            if (expressions.Count > 0)
            {
                MenuItems.Add(new MenuItemViewModel { Header = "Clipboard",
                    MenuItems = expressionsMenuItems
                });
            };
        }

        public NotifyIconViewModel()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { Header = "Config" },
                new MenuItemViewModel { Header = "Beta",
                    MenuItems = new ObservableCollection<MenuItemViewModel>
                        {
                            new MenuItemViewModel { Header = "Beta1" },
                            new MenuItemViewModel { Header = "Beta2",
                                MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "Beta1a" },
                                    new MenuItemViewModel { Header = "Beta1b" },
                                    new MenuItemViewModel { Header = "Beta1c" }
                                }
                            },
                            new MenuItemViewModel { Header = "Beta3" }
                        }
                },
                new MenuItemViewModel { Header = "Gamma" }
            };
        }
    }
}
