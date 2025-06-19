using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtusProject.Project.Core.DataAccess;
using MyOtusProject.Project.Core.Entities;

namespace MyOtusProject.Project.Core.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ToDoUser?> GetUser(long telegramUserId, CancellationToken ct)
        {
            return await _userRepository.GetUserByTelegramUserId(telegramUserId, ct);
        }

        public async Task<ToDoUser> RegisterUser(long telegramUserId, string telegramUserName, CancellationToken ct)
        {
            var newUser = new ToDoUser
            {
                UserId = Guid.NewGuid(),
                TelegramUserId = telegramUserId,
                TelegramUserName = telegramUserName,
                RegisteredAt = DateTime.UtcNow
            };
            await _userRepository.Add(newUser, ct);
            return newUser;
        }
    }
}
