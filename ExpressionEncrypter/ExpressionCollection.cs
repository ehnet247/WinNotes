using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextCopy;

namespace ExpressionEncrypter
{
    public class ExpressionCollection : ObservableCollection<Expression>
    {
        public Expression AddExpression(string name, string content, [CallerMemberName] string caller = "")
        {
            Expression expression = new Expression(name, content);
            if ((expression != null) && (!this.ContainsKey(expression.Name)))
            {
                this.Add(expression);
                PropertyChangedEventArgs e =
                    new PropertyChangedEventArgs(caller);
                OnPropertyChanged(e);
            }
            return expression;
        }

        public bool ContainsKey(string name)
        {
            var expression = this.Where(x => x.Name == name).SingleOrDefault();
            if ((expression != null) && (expression != default))
            {
                return true;
            }
            return false;
        }

        public void Delete(string name, [CallerMemberName] string caller = "")
        {
            if (this.ContainsKey(name))
            {
                var expression = this.Where(x => x.Name == name).SingleOrDefault();
                if ((expression != null) && (expression != default))
                {
                    this.Remove(expression);
                }
                PropertyChangedEventArgs e =
                    new PropertyChangedEventArgs(caller);
                OnPropertyChanged(e);
            }
        }

        public void CopyToClipboard(string text)
        {
            ClipboardService.SetText(text);
        }
    }

    public class Expression : ObservableRecipient
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public Expression()
        {
            Name = string.Empty;
            Content = string.Empty;
        }

        public Expression(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public void ClipboardCommand()
        {
            TextCopy.ClipboardService.SetText(Content);
        }
    }
}
