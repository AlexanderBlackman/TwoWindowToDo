using CommunityToolkit.WinUI.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWindowToDo.Model;

namespace TwoWindowToDo.ViewModels
{
    public class TodoItemViewModel: ViewModelBase
    {
        public TodoItemViewModel(TodoItem todoItem)
        {
            this.TodoItem = todoItem;
        }

        public TodoItem TodoItem { get; private set; }

        public string Title
        {
            get { return TodoItem.Title; }
            set
            {
                TodoItem.Title = value;
                RaisePropertyChanged();
            }
        }

        public string Content
        {
            get { return TodoItem.Content; }
            set
            {
                TodoItem.Content = value;
                RaisePropertyChanged();
            }
        }

        public bool Urgent
        {
            get { return TodoItem.Urgent; }
            set
            {
                TodoItem.Urgent = value;
                RaisePropertyChanged();
            }
        }

        public List<string> Tags
        {
            get { return TodoItem.stringTag; }
            set
            {
                TodoItem.stringTag = value;
                RaisePropertyChanged();
            }
        }

    public TodoItem ToModel() => new TodoItem{ Title = Title, Content = Content, Urgent = Urgent, stringTag = Tags };

        
        
    }


}

