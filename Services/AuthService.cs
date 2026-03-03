using System.Collections.Generic;
using System.Linq;

namespace Raven_Family.Services
{
    public enum UserRole
    {
        УчасникСім_ї = 0,
        СтаршийСкладСім_ї = 1,
        Заступник = 2,
        Лідер = 3,
        ТехАдмін = 4
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public UserRole Role { get; set; } = UserRole.УчасникСім_ї;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsEmailConfirmed { get; set; } = false;
        public string EmailConfirmationToken { get; set; } = string.Empty;
        public DateTime? EmailConfirmedAt { get; set; }

        // Нові поля для профілю
        public string AvatarUrl { get; set; } = "https://ui-avatars.com/api/?name=User&background=c41e3a&color=fff";
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "🟢 Активний";
        public string Bio { get; set; } = string.Empty;
    }

    public class AuthService
    {
        private int minimumPasswordLength = 6;
        private List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin", Email = "admin@ravenFamily.com", Password = "admin123", IsAdmin = true, Role = UserRole.Лідер, IsEmailConfirmed = true, CreatedAt = DateTime.UtcNow.AddDays(-365), JoinedAt = DateTime.UtcNow.AddDays(-365), AvatarUrl = "https://ui-avatars.com/api/?name=admin&background=FFD700&color=000", Status = "👑 Лідер", Bio = "Засновник сім'ї Воронів" },
            new User { Id = 2, Username = "Dmitro", Email = "dmitro@gmail.com", Password = "09102014", IsAdmin = true, Role = UserRole.ТехАдмін, IsEmailConfirmed = true, CreatedAt = DateTime.UtcNow.AddDays(-200), JoinedAt = DateTime.UtcNow.AddDays(-200), AvatarUrl = "https://ui-avatars.com/api/?name=Dmitro&background=c41e3a&color=fff", Status = "🔧 Тех Адмін", Bio = "Менеджер технічної частини" }
        };

        private User? currentUser;

        public User? GetCurrentUser() => currentUser;

        public bool Register(string username, string email, string password, string fullName = "")
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return false;

            if (users.Any(u => u.Username == username || u.Email == email))
                return false;

            if (password.Length < minimumPasswordLength)
                return false;

            var confirmationToken = GenerateEmailConfirmationToken();
            var newUser = new User
            {
                Id = users.Max(u => u.Id) + 1,
                Username = username,
                Email = email,
                Password = password,
                IsAdmin = false,
                Role = UserRole.УчасникСім_ї,
                IsEmailConfirmed = false,
                EmailConfirmationToken = confirmationToken,
                CreatedAt = DateTime.UtcNow,
                JoinedAt = DateTime.UtcNow,
                AvatarUrl = GenerateAvatarUrl(username),
                Status = "🟢 Активний",
                Bio = ""
            };

            users.Add(newUser);
            return true;
        }

        private string GenerateAvatarUrl(string username)
        {
            // Генеруємо унікальний аватар на основі імені користувача
            return $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(username)}&background=c41e3a&color=fff&bold=true&size=200";
        }

        private string GenerateEmailConfirmationToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        public string? GetEmailConfirmationToken(string email)
        {
            var user = users.FirstOrDefault(u => u.Email == email && !u.IsEmailConfirmed);
            return user?.EmailConfirmationToken;
        }

        public bool ConfirmEmail(string token)
        {
            var user = users.FirstOrDefault(u => u.EmailConfirmationToken == token && !u.IsEmailConfirmed);
            if (user != null)
            {
                user.IsEmailConfirmed = true;
                user.EmailConfirmedAt = DateTime.UtcNow;
                user.EmailConfirmationToken = string.Empty;
                return true;
            }
            return false;
        }

        public User? GetUserByEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email == email);
        }

        public bool Login(string username, string password)
        {
            var user = users.FirstOrDefault(u => (u.Username == username || u.Email == username) && u.Password == password);
            if (user != null)
            {
                if (!user.IsEmailConfirmed)
                {
                    return false;
                }
                currentUser = user;
                return true;
            }
            return false;
        }

        public (bool Success, string Message) LoginWithMessage(string username, string password)
        {
            var user = users.FirstOrDefault(u => (u.Username == username || u.Email == username) && u.Password == password);
            if (user == null)
                return (false, "Невірні дані для входу");

            if (!user.IsEmailConfirmed)
                return (false, "Будь ласка, підтвердіть ваш email перед входом");

            currentUser = user;
            return (true, "Успішний вхід");
        }

        public void Logout()
        {
            currentUser = null;
        }

        public bool IsLoggedIn() => currentUser != null;

        public bool IsAdmin() => currentUser?.IsAdmin ?? false;

        public UserRole GetCurrentUserRole() => currentUser?.Role ?? UserRole.УчасникСім_ї;

        public bool CanManageRoles()
        {
            if (currentUser == null) return false;
            // Лідер, Заступник і ТехАдмін можуть керувати ролями
            // ТехАдмін - має всі права
            return currentUser.Role == UserRole.Лідер || 
                   currentUser.Role == UserRole.Заступник || 
                   currentUser.Role == UserRole.ТехАдмін;
        }

        public bool CanPromoteToSenior()
        {
            if (currentUser == null) return false;
            // Лідер, Заступник і ТехАдмін можуть підвищувати до Старшого складу
            return currentUser.Role == UserRole.Лідер || 
                   currentUser.Role == UserRole.Заступник ||
                   currentUser.Role == UserRole.ТехАдмін;
        }

        public bool CanPromoteToDeputy()
        {
            if (currentUser == null) return false;
            // Тільки Лідер і ТехАдмін можуть видавати роль Заступника
            return currentUser.Role == UserRole.Лідер || 
                   currentUser.Role == UserRole.ТехАдмін;
        }

        public bool CanPromoteToLeader()
        {
            if (currentUser == null) return false;
            // Тільки ТехАдмін може видавати роль Лідера
            return currentUser.Role == UserRole.ТехАдмін;
        }

        public bool IsTechAdmin()
        {
            return currentUser?.Role == UserRole.ТехАдмін;
        }

        public List<User> GetAllUsers() => users;

        public void DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                users.Remove(user);
        }

        // Методи для редагування профілю
        public bool UpdateUsername(int userId, string newUsername)
        {
            if (string.IsNullOrWhiteSpace(newUsername) || newUsername.Length < 3)
                return false;

            // Перевірити, чи новий юзернейм вже використовується
            if (users.Any(u => u.Username == newUsername && u.Id != userId))
                return false;

            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Username = newUsername;
                if (currentUser?.Id == userId)
                {
                    currentUser.Username = newUsername;
                }
                return true;
            }
            return false;
        }

        public bool UpdateAvatar(int userId, string avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
                return false;

            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.AvatarUrl = avatarUrl;
                if (currentUser?.Id == userId)
                {
                    currentUser.AvatarUrl = avatarUrl;
                }
                return true;
            }
            return false;
        }

        public bool UpdateBio(int userId, string bio)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Bio = bio ?? string.Empty;
                if (currentUser?.Id == userId)
                {
                    currentUser.Bio = user.Bio;
                }
                return true;
            }
            return false;
        }

        public bool CanEditUser(int userId)
        {
            // Користувач може редагувати свій профіль або адмін будь-якого
            return currentUser?.Id == userId || currentUser?.IsAdmin == true;
        }

        public void UpdateUserRole(int id, UserRole role)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Role = role;
            }
        }

        public void UpdateUserAdmin(int id, bool isAdmin)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsAdmin = isAdmin;
            }
        }

        public string GetRoleDisplayName(UserRole role)
        {
            return role switch
            {
                UserRole.УчасникСім_ї => "Участник сім'ї",
                UserRole.СтаршийСкладСім_ї => "Старший склад сім'ї",
                UserRole.Заступник => "Заступник",
                UserRole.Лідер => "Лідер",
                UserRole.ТехАдмін => "Тех адмін",
                _ => "Невідома роль"
            };
        }

        public List<UserRole> GetAvailableRoles()
        {
            return new List<UserRole>
            {
                UserRole.УчасникСім_ї,
                UserRole.СтаршийСкладСім_ї,
                UserRole.Заступник,
                UserRole.Лідер,
                UserRole.ТехАдмін
            };
        }

        // Налаштування мінімальної довжини пароля
        public int GetMinimumPasswordLength() => minimumPasswordLength;

        public void SetMinimumPasswordLength(int length)
        {
            if (length >= 4 && length <= 20)
                minimumPasswordLength = length;
        }

        // Експорт користувачів у CSV
        public string ExportUsersToCSV()
        {
            var csv = "ID,Ім'я,Email,Роль,Адмін,Дата реєстрації\n";
            foreach (var user in users)
            {
                csv += $"{user.Id},\"{user.Username}\",\"{user.Email}\",\"{GetRoleDisplayName(user.Role)}\",{(user.IsAdmin ? "Так" : "Ні")},{user.CreatedAt:dd.MM.yyyy}\n";
            }
            return csv;
        }

        // Експорт статистики у JSON
        public string ExportStatisticsToJSON()
        {
            var stats = new
            {
                TotalUsers = users.Count,
                Admins = users.Count(u => u.IsAdmin),
                Leaders = users.Count(u => u.Role == UserRole.Лідер),
                Deputies = users.Count(u => u.Role == UserRole.Заступник),
                SeniorMembers = users.Count(u => u.Role == UserRole.СтаршийСкладСім_ї),
                RegularMembers = users.Count(u => u.Role == UserRole.УчасникСім_ї),
                TechAdmins = users.Count(u => u.Role == UserRole.ТехАдмін),
                ExportDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                SystemVersion = "1.0.0"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(stats, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            return json;
        }

        // Експорт всіх користувачів у JSON
        public string ExportAllUsersToJSON()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(users.Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                Role = GetRoleDisplayName(u.Role),
                IsAdmin = u.IsAdmin ? "Так" : "Ні",
                CreatedAt = u.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss")
            }), new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            return json;
        }
    }
}
