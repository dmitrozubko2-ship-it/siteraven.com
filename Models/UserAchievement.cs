using System;

namespace Raven_Family.Models
{
    /// <summary>
    /// Досягнення користувача
    /// </summary>
    public class UserAchievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserProfile? UserProfile { get; set; }

        public string AchievementKey { get; set; } = string.Empty; // "first_contract", "100_reputation"
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty; // Емодзі
        public int RewardXP { get; set; } = 100;
        public bool IsLocked { get; set; } = true;

        public DateTime? UnlockedAt { get; set; }
    }
}
