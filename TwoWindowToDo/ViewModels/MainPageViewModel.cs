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
        
        [ObservableProperty]
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
            TodoItems.Remove(TopTodo);
            if (TodoItems.Any())
            {
                TopTodo = TodoItems[0];
            }
            else
            {
                TodoItems.Add(new TodoItemViewModel(new TodoItem{ Title = "Add more tasks" }) );
                TopTodo = TodoItems[0];
            }
        }

        
        public void AddTodo()
        {
            if (string.IsNullOrWhiteSpace(newTodoTitle)) return;
            var newTodo = new TodoItemViewModel(new TodoItem { Title = newTodoTitle, Urgent = newIsUrgent });
            TodoItems.Add(newTodo);
            selectedTodo = newTodo;
        }

        public void DeleteTodo()
        {
            TodoItems.Remove(selectedTodo);
            if (TodoItems.Any())
            {
                TopTodo = TodoItems[0];
            }
            else
            {
                TodoItems.Add(new TodoItemViewModel(new TodoItem { Title = "Add more tasks" }));
                TopTodo = TodoItems[0];
            }
        }
    }
}
