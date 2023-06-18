using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinNotes.Config;

namespace WinNotes.Notify
{
    public partial class NotifyIconResources
    {
        private readonly ServiceProvider _serviceProvider;

        public ObservableCollection<MenuItemViewModel> MenuItems { get; private set; }

        public NotifyIconResources()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private void ConfigureServices(IServiceCollection services)
        {
            // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/ioc
            //services.AddSingleton<INotesService, NotesService>();
            services.AddSingleton<ConfigWindow>();
            //services.AddSingleton<IClipboardService, ClipboardService>();
        }

        private void ConfigClick(object sender, RoutedEventArgs e)
        {
            var configWindow = _serviceProvider.GetService<ConfigWindow>();
            configWindow.Show();
        }
    }
}
