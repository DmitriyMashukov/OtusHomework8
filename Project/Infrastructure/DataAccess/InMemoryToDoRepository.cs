using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtusProject.Project.Core.DataAccess;
using MyOtusProject.Project.Core.Entities;

namespace MyOtusProject.Project.Infrastructure.DataAccess
{
    internal class InMemoryToDoRepository : IToDoRepository
    {
        private readonly List<ToDoItem> _items = new List<ToDoItem>();
        public Task Add(ToDoItem item, CancellationToken ct)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task<int> CountActive(Guid userId, CancellationToken ct)
        {
            return Task.FromResult(_items.Count(x => x.User.UserId == userId && x.State == ToDoItemState.Active));
        }

        public Task Delete(Guid id, CancellationToken ct)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _items.Remove(item);
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsByName(Guid userId, string name, CancellationToken ct)
        {
            return Task.FromResult(_items.Any(x => x.User.UserId == userId &&
                              x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<ToDoItem?> Get(Guid id, CancellationToken ct)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public Task<IReadOnlyList<ToDoItem>> GetActiveByUserId(Guid userId, CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyList<ToDoItem>>(_items.Where(x => x.User.UserId == userId &&
                               x.State == ToDoItemState.Active).ToList().AsReadOnly());
        }

        public Task<IReadOnlyList<ToDoItem>> GetAllByUserId(Guid userId, CancellationToken ct)
        {
            return Task.FromResult <IReadOnlyList<ToDoItem>>(_items.Where(x => x.User.UserId == userId).ToList().AsReadOnly());
        }

        public Task Update(ToDoItem item, CancellationToken ct)
        {
            var existingItem = _items.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
                _items.Add(item);
            }
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<ToDoItem>> Find(Guid userId, Func<ToDoItem, bool> predicate, CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyList<ToDoItem>>(_items.Where(x => x.User.UserId == userId).Where(predicate).ToList().AsReadOnly());
        }
    }
}
