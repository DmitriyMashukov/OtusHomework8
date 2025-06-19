using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtusProject.Project.Core.DataAccess;
using MyOtusProject.Project.Core.Entities;

namespace MyOtusProject.Project.Infrastructure.DataAccess
{
    internal class InMemoryUserRepository : IUserRepository
    {
        private readonly List<ToDoUser> _users = new List<ToDoUser>();
        public Task Add(ToDoUser user, CancellationToken ct)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task <ToDoUser?> GetUser(Guid userId, CancellationToken ct)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.UserId == userId));
        }

        public Task<ToDoUser?> GetUserByTelegramUserId(long telegramUserId, CancellationToken ct)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.TelegramUserId == telegramUserId));
        }
    }
}
