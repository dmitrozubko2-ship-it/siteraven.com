using System;
using System.Collections.Generic;
using System.Linq;
using Raven_Family.Models;

namespace Raven_Family.Services
{
    public class ShopService
    {
        private List<ShopItem> shopItems = new()
        {
            new ShopItem { Id = 1, Name = "🎭 Шут придворний", Description = "Розвеселювачка", Category = "role", Price = 150, IsActive = true },
            new ShopItem { Id = 2, Name = "🧙 Чарівник", Description = "Магічна роль", Category = "role", Price = 250, IsActive = true },
            new ShopItem { Id = 3, Name = "⚔️ Войовник", Description = "Боєць", Category = "role", Price = 300, IsActive = true },
            new ShopItem { Id = 4, Name = "✨ Золотий Ореол", Description = "Ефект", Category = "cosmetic", Price = 100, IsActive = true },
            new ShopItem { Id = 5, Name = "⚡ Нітро Тиждень", Description = "Подвійний XP", Category = "boost", Price = 80, IsActive = true },
            new ShopItem { Id = 6, Name = "💎 VIP Месяц", Description = "Місяцевий VIP", Category = "boost", Price = 500, IsActive = true },
        };

        public List<ShopItem> GetAllItems()
        {
            return shopItems;
        }

        public List<ShopItem> GetItemsByCategory(string category)
        {
            return shopItems.Where(item => item.Category == category).ToList();
        }

        public ShopItem? GetItem(int id)
        {
            return shopItems.FirstOrDefault(item => item.Id == id);
        }

        public bool PurchaseItem(int userId, int itemId, long userCoins)
        {
            var item = GetItem(itemId);
            if (item == null || !item.IsActive)
                return false;

            if (userCoins >= item.Price)
            {
                return true;
            }

            return false;
        }

        public long GetItemPrice(int itemId)
        {
            var item = GetItem(itemId);
            return item?.Price ?? 0;
        }
    }
}

