using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoWindowToDo.Model
{
    public class TodoItem
    {
        public string Title { get; set; }
        public string Content { get; set; } = String.Empty;
        public bool Urgent { get; set; } = false;
        //public Tag[]
        //public DateTime dueDate { get; set; }
        //public string[] stringTag { get; set; } 
        public List<string> stringTag { get; set; }
    }
}
