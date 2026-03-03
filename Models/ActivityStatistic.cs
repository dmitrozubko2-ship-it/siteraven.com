using System;

namespace Raven_Family.Models
{
    /// <summary>
    /// Статистика активності для графіків
    /// </summary>
    public class ActivityStatistic
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        public int DayOfWeek { get; set; } // 0-6
        public int Hour { get; set; } // 0-23
        
        public int LoginCount { get; set; }
        public int NewApplicationsCount { get; set; }
        public int ContractsCompletedCount { get; set; }
        public int VotesCount { get; set; }
        
        public int ActiveUsersCount { get; set; }
        public int OnlineUsersCount { get; set; }
        
        public long CoinsMovedTotal { get; set; }
    }
}
