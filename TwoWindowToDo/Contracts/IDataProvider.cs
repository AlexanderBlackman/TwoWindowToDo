using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWindowToDo.Model;

namespace TwoWindowToDo.Contracts
{
    public interface IDataProvider
    {
        //Change TodoItem to a generic type

        Task<IEnumerable<TodoItem>> GetDataAsync();
        Task SaveDataAsync(IEnumerable<TodoItem> TodoItems);
    }
}
