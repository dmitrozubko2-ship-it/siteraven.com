using System;

namespace Raven_Family.Models
{
    /// <summary>
    /// Нагороди користувача (бейджи, звання)
    /// </summary>
    public class UserAward
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserProfile? UserProfile { get; set; }

        public string AwardType { get; set; } = string.Empty; // "badge", "title", "role"
        public string Name { get; set; } = string.Empty; // Ім'я нагороди
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty; // Емодзі або URL
        public string Color { get; set; } = "#c41e3a"; // Колір

        public DateTime AwardedAt { get; set; } = DateTime.Now;
        public string AwardedBy { get; set; } = "System"; // Хто видав
    }
}
