using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWindowToDo.Contracts;
using TwoWindowToDo.Model;

namespace TwoWindowToDo.Data
{
    public class DataProvider : IDataProvider
    {
        public async Task<IEnumerable<TodoItem>?> GetDataAsync()
        {
            await Task.Delay(100);

            return new List<TodoItem>()
            {
                //new TodoItem {TodoItemName = "Australia", TodoItemCapital="Canberra", inEurope = false },
                //new TodoItem {TodoItemName = "Spain", TodoItemCapital = "Madrid", inEurope = true},
                //new TodoItem {TodoItemName = "France", TodoItemCapital= "Paris", inEurope = true},
                //new TodoItem {TodoItemName = "Germany", TodoItemCapital = "Berlin", inEurope = true},
                //new TodoItem {TodoItemName = "Italy", TodoItemCapital = "Rome", inEurope = true},
                //new TodoItem {TodoItemName = "United Kingdom", TodoItemCapital = "London", inEurope = true},
                //new TodoItem {TodoItemName = "United States", TodoItemCapital = "Washington DC", inEurope = false},
                //new TodoItem {TodoItemName = "Canada", TodoItemCapital = "Ottawa", inEurope = false},
                //new TodoItem {TodoItemName = "Mexico", TodoItemCapital = "Mexico City", inEurope = false},
                //new TodoItem {TodoItemName = "Brazil", TodoItemCapital = "Brasilia", inEurope = false},
                //new TodoItem {TodoItemName = "Argentina", TodoItemCapital = "Buenos Aires", inEurope = false}

            };
        }

    }
}
