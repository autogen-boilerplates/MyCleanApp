using MyCleanApp.Application.Common.Mappings;
using MyCleanApp.Domain.Entities;

namespace MyCleanApp.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
