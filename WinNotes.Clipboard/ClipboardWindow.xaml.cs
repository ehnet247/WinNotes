using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinNotes.Notify;

namespace WinNotes.Clipboard
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ClipboardWindow : Window
    {
        public ClipboardWindow(INotesService notesService)
        {
            InitializeComponent();
        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
            //Dispatcher.BeginInvoke( new Action(() => { this.Close(); }), System.Windows.Threading.DispatcherPriority.ContextIdle);
        }
    }
}