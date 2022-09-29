using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWindowToDo.Contracts;
using TwoWindowToDo.Model;
using System.Text.Json;
using System.Diagnostics;

namespace TwoWindowToDo.Data
{
    public class JSONDataProvider : IDataProvider
    {
        private string jsonfile = @"d:\TodoItems.json";
        public Task<IEnumerable<TodoItem>> GetDataAsync()
        {
            var TodoItems = (File.Exists(jsonfile)) ?
                JsonSerializer.Deserialize<IEnumerable<TodoItem>>(File.ReadAllText(jsonfile)) :
                new List<TodoItem>() { new TodoItem() { Title = "Fill out **todo**list", Urgent = false } };

            return Task.FromResult(TodoItems);
        }    
        public Task SaveDataAsync(IEnumerable<TodoItem> TodoItems)
        {
            var jsonString = JsonSerializer.Serialize(TodoItems);
            File.WriteAllText(jsonfile, jsonString);
            Debug.WriteLine(jsonString);
            return Task.CompletedTask;
        }
    }
 }


