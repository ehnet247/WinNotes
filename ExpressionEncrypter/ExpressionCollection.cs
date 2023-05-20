using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEncrypter
{
    public class ExpressionCollection : ObservableCollection<Expression>
    {
        public void AddExpression(string name, string content, [CallerMemberName] string caller = "")
        {
            Expression expression = new Expression(name, content);
            if ((expression != null) && (!this.ContainsKey(expression.Name)))
            {
                this.Add(expression);
                PropertyChangedEventArgs e =
                    new PropertyChangedEventArgs(caller);
                OnPropertyChanged(e);
            }
        }

        public bool ContainsKey(string key)
        {
            return false;
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
    }
}
