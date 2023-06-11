using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEncrypter;
using System.IO;
using WinNotes.Config;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace WinNotes.Clipboard
{
    public class ClipboardViewModel : ObservableRecipient
    {
        private object _SelectedValue;
        private string _SelectedText;
        public ExpressionCollection Expressions { get; set; }
        public string SelectedText
        {
            get => _SelectedText;
            set
            {
                _SelectedText = value;
                Expressions.CopyToClipboard(_SelectedText);
            }
        }
        public object SelectedValue
        {
            get => _SelectedValue;
            set
            {
                _SelectedValue = value;
                var selectedExpr = _SelectedValue as ExpressionEncrypter.Expression;
                if (selectedExpr != null)
                {
                    SelectedText = selectedExpr.Content;
                }
            }
        }

        public ClipboardViewModel()
        {
        }
        public void SetFileName(string fileName)
        {
            Encrypter encrypter = new Encrypter();
            Expressions = new ExpressionCollection();
            object? expressions = null;
            if ((!String.IsNullOrEmpty(fileName)) && (File.Exists(fileName)))
            {
                encrypter.Read(fileName, out expressions, typeof(ExpressionCollection));
                if ((expressions != null) &&
                    (expressions.GetType() == typeof(ExpressionCollection)))
                {
                    Expressions = (ExpressionCollection)expressions;
                    OnPropertyChanged(nameof(Expressions));
                }
            }
        }
    }
}
