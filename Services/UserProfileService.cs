using System.Collections.Concurrent;
using System.Threading.Tasks;
using Raven_Family.Models;
using System.Collections.Generic;
using System.Linq;

namespace Raven_Family.Services
{
    // Minimal in-memory service to provide UserProfile operations for the app.
    public class UserProfileService
    {
        private readonly ConcurrentDictionary<int, UserProfile> _profiles = new();

        public Task<UserProfile?> GetByUserIdAsync(int userId)
        {
            _profiles.TryGetValue(userId, out var profile);
            return Task.FromResult(profile);
        }

        public Task SaveAsync(UserProfile profile)
        {
            _profiles[profile.UserId] = profile;
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int userId)
        {
            return Task.FromResult(_profiles.ContainsKey(userId));
        }

        // Return all stored profiles (for leaderboard / listing). Returns empty list if none.
        public Task<List<UserProfile>> GetAllProfilesAsync()
        {
            var list = new List<UserProfile>(_profiles.Values);
            return Task.FromResult(list);
        }

        // Оновлення аватарки користувача
        public Task<bool> UpdateAvatarAsync(int userId, string avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
                return Task.FromResult(false);

            if (_profiles.TryGetValue(userId, out var profile))
            {
                profile.AvatarUrl = avatarUrl;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        // Оновлення аватарки з файлу (base64)
        public Task<bool> UpdateAvatarFromFileAsync(int userId, string base64Data)
        {
            if (string.IsNullOrWhiteSpace(base64Data))
                return Task.FromResult(false);

            try
            {
                if (_profiles.TryGetValue(userId, out var profile))
                {
                    // Зберігаємо base64 дані як URL для зображення
                    profile.AvatarUrl = $"data:image/jpeg;base64,{base64Data}";
                    return Task.FromResult(true);
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(false);
        }

        // Отримання юзера за ID для редагування
        public Task<UserProfile?> GetProfileForEditAsync(int userId)
        {
            if (_profiles.TryGetValue(userId, out var profile))
            {
                return Task.FromResult(profile);
            }

            // Якщо профілю нема, повертаємо порожний
            return Task.FromResult<UserProfile?>(null);
        }
    }
}
