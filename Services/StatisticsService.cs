using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raven_Family.Models;

namespace Raven_Family.Services
{
    public class StatisticsService
    {
        private List<ActivityStatistic> activityStats = new();

        public StatisticsService()
        {
            InitializeTestData();
        }

        private void InitializeTestData()
        {
            var now = DateTime.Now;
            for (int i = 7; i >= 0; i--)
            {
                var date = now.AddDays(-i);
                activityStats.Add(new ActivityStatistic
                {
                    Date = date,
                    DayOfWeek = (int)date.DayOfWeek,
                    Hour = 12,
                    LoginCount = Random.Shared.Next(5, 20),
                    NewApplicationsCount = Random.Shared.Next(0, 5),
                    ContractsCompletedCount = Random.Shared.Next(2, 8),
                    VotesCount = Random.Shared.Next(3, 15),
                    ActiveUsersCount = Random.Shared.Next(8, 25),
                    OnlineUsersCount = Random.Shared.Next(3, 12),
                    CoinsMovedTotal = Random.Shared.Next(100, 1000) * 100
                });
            }
        }

        public async Task<int> GetOnlineUsersCountAsync()
        {
            await Task.Delay(10);
            return Random.Shared.Next(5, 15);
        }

        public async Task<int> GetNewApplicationsTodayAsync()
        {
            await Task.Delay(10);
            return Random.Shared.Next(0, 7);
        }

        public async Task<List<ActivityStatistic>> GetWeeklyActivityAsync()
        {
            await Task.Delay(50);
            return activityStats.TakeLast(7).ToList();
        }

        public async Task<Dictionary<string, int>> GetTopActiveUsersAsync(int topCount = 10)
        {
            await Task.Delay(30);
            return new Dictionary<string, int>
            {
                { "Dmitro", 1250 },
                { "admin", 1100 },
                { "Vasyl", 950 },
                { "Olena", 820 },
                { "Ihor", 750 },
                { "Natalia", 680 },
                { "Sergii", 620 },
                { "Maria", 550 },
                { "Pavlo", 480 },
                { "Tetiana", 420 }
            };
        }

        public async Task<Dictionary<string, int>> GetTopVeteransAsync(int topCount = 10)
        {
            await Task.Delay(30);
            return new Dictionary<string, int>
            {
                { "admin", 365 },
                { "Dmitro", 280 },
                { "Vasyl", 245 },
                { "Olena", 200 },
                { "Ihor", 185 },
                { "Natalia", 160 },
                { "Sergii", 150 },
                { "Maria", 120 },
                { "Pavlo", 95 },
                { "Tetiana", 75 }
            };
        }

        public async Task<SystemStats> GetSystemStatsAsync()
        {
            await Task.Delay(20);
            var todayStats = activityStats.LastOrDefault();
            return new SystemStats
            {
                TotalUsers = 42,
                OnlineUsers = Random.Shared.Next(5, 15),
                NewApplicationsToday = todayStats?.NewApplicationsCount ?? 3,
                TotalContracts = 156,
                TotalCoinsInCirculation = 125000,
                AverageUserReputation = 85,
                ServerStatusGood = true
            };
        }
    }

    public class SystemStats
    {
        public int TotalUsers { get; set; }
        public int OnlineUsers { get; set; }
        public int NewApplicationsToday { get; set; }
        public int TotalContracts { get; set; }
        public long TotalCoinsInCirculation { get; set; }
        public int AverageUserReputation { get; set; }
        public bool ServerStatusGood { get; set; }
    }
}
