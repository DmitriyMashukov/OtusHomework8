using MyOtusProject.Project.Core.DataAccess;
using MyOtusProject.Project.Core.Entities;
using MyOtusProject.Project.Core.Exceptions;
using Otus.ToDoList.ConsoleBot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyOtusProject.Project.Core.Services
{
    internal class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;
        private readonly int _maxTaskCount;
        private readonly int _maxTaskLength;

        public ToDoService(IToDoRepository repository, int maxTaskCount, int maxTaskLength)
        {
            _repository = repository;
            _maxTaskCount = maxTaskCount;
            _maxTaskLength = maxTaskLength;
        }
        public async Task<ToDoItem> Add(ToDoUser user, string name, CancellationToken ct)
        {
            var activeCount = await _repository.CountActive(user.UserId, ct);
            if (activeCount >= _maxTaskCount)
                throw new TaskCountLimitException(_maxTaskCount);

            if (name.Length > _maxTaskLength)
                throw new TaskLengthLimitException(name.Length, _maxTaskLength);

            if (await _repository.ExistsByName(user.UserId, name, ct))
                throw new DuplicateTaskException(name);

            var newTask = new ToDoItem
            {
                User = user,
                Name = name,
                State = ToDoItemState.Active,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.Add(newTask, ct);
            return newTask;
        }

        public Task Delete(Guid id, CancellationToken ct)
        {
           return _repository.Delete(id, ct);
        }

        public Task<IReadOnlyList<ToDoItem>> GetActiveByUserId(Guid userId, CancellationToken ct)
        {
            return _repository.GetActiveByUserId(userId, ct);
        }

        public Task<IReadOnlyList<ToDoItem>> GetAllByUserId(Guid userId, CancellationToken ct)
        {
            return _repository.GetAllByUserId(userId, ct);
        }

        public async Task MarkCompleted(Guid id, CancellationToken ct)
        {
            var task = await _repository.Get(id, ct);
            if (task != null)
            {
                task.State = ToDoItemState.Completed;
                task.StateChangedAt = DateTime.UtcNow;
                await _repository.Update(task, ct);
            }
        }

        public async Task<IReadOnlyList<ToDoItem>> Find(ToDoUser user, string namePrefix, CancellationToken ct)
        {
            return await _repository.Find(user.UserId, task => task.Name.StartsWith(namePrefix, StringComparison.OrdinalIgnoreCase), ct);
        }
    }
}
