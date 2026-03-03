using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raven_Family.Services
{
    public class Quest
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";
        public int RewardXP { get; set; }
        public int RewardCoins { get; set; }
        public string Status { get; set; } = "available"; // available, in_progress, completed
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? CompletedByUserId { get; set; }
    }

    public class QuestService
    {
        private List<Quest> quests = new()
        {
            new Quest
            {
                Id = 1,
                Title = "🎯 Дебютант",
                Description = "Завершите свій перший логін на сайт. Добро пожалуйте в сім'ю!",
                Icon = "🎯",
                RewardXP = 50,
                RewardCoins = 25,
                Status = "available"
            },
            new Quest
            {
                Id = 2,
                Title = "📝 Оратор",
                Description = "Напишіть коментар на 3 різних сторінках",
                Icon = "📝",
                RewardXP = 100,
                RewardCoins = 50,
                Status = "available"
            },
            new Quest
            {
                Id = 3,
                Title = "👥 Соціальна Метелик",
                Description = "Проголосуйте у 5 різних голосуваннях",
                Icon = "🗳️",
                RewardXP = 150,
                RewardCoins = 75,
                Status = "available"
            },
            new Quest
            {
                Id = 4,
                Title = "📜 Дослідник",
                Description = "Відвідайте всі сторінки сайту",
                Icon = "🔍",
                RewardXP = 200,
                RewardCoins = 100,
                Status = "available"
            },
            new Quest
            {
                Id = 5,
                Title = "⚡ Активист",
                Description = "Заробіть 500 очків активності",
                Icon = "⚡",
                RewardXP = 300,
                RewardCoins = 150,
                Status = "in_progress"
            },
            new Quest
            {
                Id = 6,
                Title = "🎁 День Народження Сім'ї",
                Description = "Возьми участь в святкуванні дня народження",
                Icon = "🎂",
                RewardXP = 250,
                RewardCoins = 120,
                Status = "available"
            },
            new Quest
            {
                Id = 7,
                Title = "🏆 Переможець",
                Description = "Завершите 10 контрактів",
                Icon = "🏆",
                RewardXP = 400,
                RewardCoins = 200,
                Status = "available"
            },
            new Quest
            {
                Id = 8,
                Title = "🔮 Містик",
                Description = "Знайдіть 5 пасхалок на сайті",
                Icon = "🔮",
                RewardXP = 500,
                RewardCoins = 250,
                Status = "available"
            }
        };

        public List<Quest> GetAllQuests()
        {
            return quests;
        }

        public List<Quest> GetAvailableQuests()
        {
            return quests.Where(q => q.Status == "available").ToList();
        }

        public List<Quest> GetUserQuests(int userId)
        {
            return quests.Where(q => q.Status == "in_progress" || q.CompletedByUserId == userId).ToList();
        }

        public Quest? GetQuest(int id)
        {
            return quests.FirstOrDefault(q => q.Id == id);
        }

        public bool CompleteQuest(int questId, int userId)
        {
            var quest = GetQuest(questId);
            if (quest != null && quest.Status != "completed")
            {
                quest.Status = "completed";
                quest.CompletedAt = DateTime.Now;
                quest.CompletedByUserId = userId;
                return true;
            }
            return false;
        }

        public async Task<List<Quest>> GetRandomDailyQuestsAsync()
        {
            await Task.Delay(50);
            return GetAvailableQuests().OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        }
    }
}
