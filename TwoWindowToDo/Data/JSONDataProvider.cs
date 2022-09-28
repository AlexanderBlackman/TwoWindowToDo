using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWindowToDo.Contracts;
using TwoWindowToDo.Model;
using System.Text.Json;

namespace TwoWindowToDo.Data
{
    public class JSONDataProvider : IDataProvider
    {
        public Task<IEnumerable<TodoItem>> GetDataAsync()
        {
            var TodoItems = (File.Exists("TodoItems.json")) ?
                JsonSerializer.Deserialize<IEnumerable<TodoItem>>(File.ReadAllText("TodoItems.json")) :
                new List<TodoItem>() { new TodoItem() { Title = "Fill out **todo**list", Urgent = false } };

            return Task.FromResult(TodoItems);
        }    
        public Task SaveDataAsync(IEnumerable<TodoItem> TodoItems)
        {
            var jsonString = JsonSerializer.Serialize(TodoItems);
            File.WriteAllText("TodoItems.json", jsonString);
            return Task.CompletedTask;
        }
    }
 }


