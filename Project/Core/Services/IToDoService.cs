using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtusProject.Project.Core.Entities;

namespace MyOtusProject.Project.Core.Services
{
    internal interface IToDoService
    {
        Task<IReadOnlyList<ToDoItem>> GetAllByUserId(Guid userId, CancellationToken ct);
        Task<IReadOnlyList<ToDoItem>> GetActiveByUserId(Guid userId, CancellationToken ct);
        Task<ToDoItem> Add(ToDoUser user, string name, CancellationToken ct);
        Task MarkCompleted(Guid id, CancellationToken ct);
        Task Delete(Guid id, CancellationToken ct);
        Task<IReadOnlyList<ToDoItem>> Find(ToDoUser user, string namePrefix, CancellationToken ct);
    }
}
