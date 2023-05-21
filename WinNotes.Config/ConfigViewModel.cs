using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressionEncrypter;

namespace WinNotes.Config
{
    public class ConfigViewModel : ObservableRecipient
    {
        public ICommand Cmd_New { get; private set; }
        public ICommand Cmd_Save { get; private set; }
        private ExpressionCollection _Expressions;
        public ExpressionCollection Expressions
        {
            get => _Expressions;
            set
            {
                _Expressions = value;
                OnPropertyChanged(nameof(Expressions));
            }
        }
        public const string FileName = "Expressions.xml";

        public ConfigViewModel()
        {
            Cmd_New = new RelayCommand(NewExpression);
            Cmd_Save = new RelayCommand(Save);
            Open();
        }

        private void Save()
        {
            Encrypter encrypter = new Encrypter();
            encrypter.Save(Expressions, FileName);
        }

        private void NewExpression()
        {
            Expressions.AddExpression("New", string.Empty);
            OnPropertyChanged(nameof(Expressions));
        }

        private void Open()
        {
            Encrypter encrypter = new Encrypter();
            object? expressions = null;
            if (File.Exists(FileName))
            {
                encrypter.Read(FileName, out expressions, typeof(ExpressionCollection));
                if ((expressions != null) &&
                    (expressions.GetType() == typeof(ExpressionCollection)))
                {
                    Expressions = (ExpressionCollection)expressions;
                }
            }

        }
    }
}
