﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TwoWindowToDo.Contracts;
using System.Text.RegularExpressions;
using TwoWindowToDo.Model;
using Windows.ApplicationModel.Email.DataProvider;

namespace TwoWindowToDo.ViewModels
{
    [ObservableObject]
    public partial class MainPageViewModel
    {
        private readonly IDataProvider dataProvider;
        public string newTodoTitle { get; set; }
        public bool newIsUrgent { get; set; } = false;
        
        public ObservableCollection<TodoItemViewModel> TodoItems { get; } = new();
        public ObservableCollection<TodoItemViewModel> CompletedTodos { get; } = new();

        public ObservableQueue<TodoItemViewModel> TodoQueue { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteTodoCommand))]
        public TodoItemViewModel selectedTodo;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(MarkCompletedCommand))]
        public TodoItemViewModel topTodo;

        public bool IsTopTodoNotNull() => (TopTodo is not null);


        public MainPageViewModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public async Task LoadAsync()
        {
            if (TodoItems.Any()) return;

            var todoItems = await dataProvider.GetDataAsync();
            if (todoItems is null) return;
            foreach (var todoItem in todoItems)
            {
                TodoItems.Add(new TodoItemViewModel(todoItem));
            }
            TopTodo = TodoItems[0];
        }

        //public async Task LoadAsync()
        //{
        //    if (TodoQueue.Any()) return;

        //    var todoItems = await dataProvider.GetDataAsync();
        //    if (todoItems is null) return;
        //    foreach (var todoItem in todoItems)
        //    {
        //        TodoQueue.Enqueue(new TodoItemViewModel(todoItem));
        //    }
        //    TopTodo = TodoQueue.Peek();
        //}
        [RelayCommand]
        public async Task SaveAsync()
        {
            var todoItems = new List<TodoItem>();
            foreach (var todoItem in TodoItems)
            {
                todoItems.Add(todoItem.ToModel());
            }
            await dataProvider.SaveDataAsync(todoItems);
        }


        [RelayCommand(CanExecute = nameof(IsTopTodoNotNull))]
        void MarkCompleted()
        {
            CompletedTodos.Add(TopTodo);
            TodoItems.RemoveAt(0);
            if (!TodoItems.Any()) 
            {
                TodoItems.Add(new TodoItemViewModel(new TodoItem { Title = "Add more tasks" }));               
            }
            TopTodo = TodoItems[0];

            //TodoQueue.Dequeue();
            //if (TodoQueue.Any())
            //{
            //    TopTodo = TodoQueue.Peek();
            //}
            //else
            //{
            //    TodoQueue.Enqueue(new TodoItemViewModel(new TodoItem { Title = "Add more tasks" }));
            //    TopTodo = TodoQueue.Peek();
            //}
        }

        [RelayCommand]
        void SkipTodo()
        {
            //var temp = TodoQueue.Dequeue();
            //TodoQueue.Enqueue(temp);
            //TopTodo = TodoQueue.Peek();
            var temp = TodoItems[0];
            TodoItems.RemoveAt(0);
            TodoItems.Add(temp);
            TopTodo = TodoItems[0];            
        }
        
        public void ProcessInputString(string input)
        {
            /* Prettify the regex with RegexOptions.IgnorePatternWhitespace. 
             * Add RegexOptions.NonBacktracking after upgrading to .NET 7
             * OR add a TimeSpan.FromSeconds(1) to the Regex.Match call
             */

            if (string.IsNullOrWhiteSpace(input)) return;
            
            var tagPattern = @"#([^\s.。!?\b]+)";
            var tagMatches = Regex.Matches(input, tagPattern);
            var tagList = new List<string>();

            var titlePattern = @"(.+)[.!?。！？]";
            var titleString = Regex.Match(input, titlePattern).Value;
            if (string.IsNullOrEmpty(titleString)){ titleString = input; }
            titleString = Regex.Replace(titleString, "#", "");
            titleString = Regex.Replace(titleString, @"\s+", " ");



            MatchCollection matches = Regex.Matches(input, tagPattern);
            foreach (Match match in matches)
            {
                tagList.Add(match.Value);
            }



            TodoItems.Add(new TodoItemViewModel(new TodoItem
            {

                Title = titleString,
                Urgent = (input[0] == '!') ? true : false,
                stringTag = tagList
            }));
        }



        public void AddTodo()
        {
            ProcessInputString(newTodoTitle);
            //var newTodo = new TodoItemViewModel(new TodoItem { Title = newTodoTitle });
            //TodoQueue.Enqueue(newTodo);
            //TodoItems.Add(newTodo);
            //selectedTodo = newTodo;
        }
        [RelayCommand]
        public void DeleteTodo()
        {
            if (SelectedTodo is not null)
            {
                TodoItems.Remove(SelectedTodo);
                SelectedTodo = null;
                if (!TodoItems.Any())                
                {
                    TodoItems.Add(new TodoItemViewModel(new TodoItem { Title = "Add more tasks" }));
                    TopTodo = TodoItems[0];
                }
                


            }
            //TodoQueue.Dequeue(selectedTodo);
            //if (TodoQueue.Any())
            //{
            //    TopTodo = TodoItems[0];
            //}
            //else
            //{
            //    TodoItems.Add(new TodoItemViewModel(new TodoItem { Title = "Add more tasks" }));
            //    TopTodo = TodoItems[0];
            //}
        }
    }
}
