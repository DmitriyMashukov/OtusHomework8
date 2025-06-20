using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtusProject.Project.Core.Entities;

namespace MyOtusProject.Project.Core.DataAccess
{
    interface IToDoRepository
    {
        Task<IReadOnlyList<ToDoItem>> GetAllByUserId(Guid userId, CancellationToken ct);
        Task<IReadOnlyList<ToDoItem>> GetActiveByUserId(Guid userId, CancellationToken ct);
        Task<ToDoItem?> Get(Guid id, CancellationToken ct);
        Task Add(ToDoItem item, CancellationToken ct);
        Task Update(ToDoItem item, CancellationToken ct);
        Task Delete(Guid id, CancellationToken ct);
        Task<bool> ExistsByName(Guid userId, string name, CancellationToken ct);
        Task<int> CountActive(Guid userId, CancellationToken ct);
        Task<IReadOnlyList<ToDoItem>> Find(Guid userId, Func<ToDoItem, bool> predicate, CancellationToken ct);
    }
}
