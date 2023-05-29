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
using System.Windows;
using System.Configuration;

namespace WinNotes.Config
{
    public class ConfigViewModel : ObservableRecipient
    {
        public ICommand Cmd_New { get; private set; }
        public ICommand Cmd_Ok { get; private set; }
        public ICommand Cmd_Delete { get; private set; }
        public ICommand Cmd_Cancel { get; private set; }
        public ICommand Cmd_Duplicate { get; private set; }
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
        private object _SelectedExpression;
        public object SelectedExpression
        {
            get => _SelectedExpression;
            set
            {
                _SelectedExpression = value;
                var selectedExpression = value as ExpressionEncrypter.Expression;
                if (selectedExpression != null)
                {
                    ExpressionNameEdit = selectedExpression.Name;
                ExpressionContentEdit = selectedExpression.Content;
                }
                else
                {
                    ExpressionNameEdit = string.Empty;
                    ExpressionContentEdit = string.Empty;
                }
                OnPropertyChanged(nameof(SelectedExpression));
            }
        }

        private string _ExpressionNameEdit;
        public string ExpressionNameEdit
        {
            get => _ExpressionNameEdit;
            set
            {
                _ExpressionNameEdit = value;
                var selectedExpression = SelectedExpression as ExpressionEncrypter.Expression;
                if (selectedExpression != null)
                {
                    selectedExpression.Name = value;
                }
                OnPropertyChanged(nameof(ExpressionNameEdit));
                OnPropertyChanged(nameof(Expressions));
            }
        }

        private string _ExpressionContentEdit;
        public string ExpressionContentEdit
        {
            get => _ExpressionContentEdit;
            set
            {
                _ExpressionContentEdit = value;
                var selectedExpression = SelectedExpression as ExpressionEncrypter.Expression;
                if (selectedExpression != null)
                {
                    selectedExpression.Content = value;
                }
                OnPropertyChanged(nameof(ExpressionContentEdit));
                OnPropertyChanged(nameof(Expressions));
            }
        }

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

        public ConfigViewModel()
        {
            Cmd_New = new RelayCommand(NewExpression);
            Cmd_Delete = new RelayCommand(DeleteExpression);
            Cmd_Duplicate = new RelayCommand(Duplicate);
            Cmd_Ok = new RelayCommand(Save);
            Cmd_Cancel = new RelayCommand(CancelEdit);
            Open();
        }

        private void Save()
        {
            Encrypter encrypter = new Encrypter();
            encrypter.Save(Expressions, FileName, null);
        }

        private void CancelEdit()
        {
            SelectedExpression = null;
        }

        private void NewExpression()
        {
            if (Expressions == null)
            {
                Expressions = new ExpressionCollection();
            }
            Expressions.AddExpression("New", string.Empty);
            OnPropertyChanged(nameof(Expressions));
        }

        private void Duplicate()
        {
            if ((Expressions != null) && (SelectedExpression != null))
            {
                var selectedExpression = SelectedExpression as ExpressionEncrypter.Expression;
                if (selectedExpression != null)
                {
                    string name = selectedExpression.Name + " bis";
                    SelectedExpression = Expressions.AddExpression(name,
                                         selectedExpression.Content);
                    OnPropertyChanged(nameof(Expressions));
                }
            }
        }

        private void DeleteExpression()
        {
            if ((Expressions != null) && (SelectedExpression != null))
            {
                var selectedExpression = SelectedExpression as ExpressionEncrypter.Expression;
                if (selectedExpression != null)
                {
                    Expressions.Delete(selectedExpression.Name);
                    Save();
                    SelectedExpression = null;
                    OnPropertyChanged(nameof(Expressions));
                }
            }
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
