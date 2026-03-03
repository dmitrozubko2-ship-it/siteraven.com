using System;
using System.Collections.Generic;

namespace Raven_Family.Models
{
    /// <summary>
    /// Профіль користувача з системою репутації, XP та нагород
    /// </summary>
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        // Базова інформація
        public string Bio { get; set; } = "";
        public string AvatarUrl { get; set; } = "";

        // Рівнева система
        public int Level { get; set; } = 1;
        public long ExperiencePoints { get; set; } = 0; // XP для поточного рівня
        public long TotalExperiencePoints { get; set; } = 0; // Всього XP

        // Репутація
        public int Reputation { get; set; } = 0;
        public int ReputationThisMonth { get; set; } = 0;

        // Активність
        public int ActivityPoints { get; set; } = 0; // Очки активності
        public int LoginStreak { get; set; } = 0; // Поточна серія входів
        public int MaxLoginStreak { get; set; } = 0; // Максимальна серія входів
        public int TotalLogins { get; set; } = 0; // Всього входів

        // Статус
        public bool IsOnline { get; set; } = false;
        public DateTime LastSeenAt { get; set; } = DateTime.Now;
        public DateTime LastLoginAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Рангова система
        public string Rank { get; set; } = "Новачок"; // Новачок, Учасник, Ветеран, Герой, Легенда
        public int DaysInFamily { get; set; } = 0; // Днів у сім'ї

        // Статистика
        public int ContractsCompleted { get; set; } = 0;
        public int ContractsActive { get; set; } = 0;
        public int TotalContractValue { get; set; } = 0; // На монетах
        public int QuestsCompleted { get; set; } = 0;
        public int VotesParticipated { get; set; } = 0;

        // Баланс
        public long CoinsBalance { get; set; } = 100; // Стартовий баланс

        // Прихована метрика для пасхалки
        public int RavenClickCount { get; set; } = 0;
        public bool HasSecretAchievement { get; set; } = false;

        // Відносини
        public virtual ICollection<UserAchievement> Achievements { get; set; } = new List<UserAchievement>();
        public virtual ICollection<UserAward> Awards { get; set; } = new List<UserAward>();
    }
}
