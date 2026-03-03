using System;

namespace Raven_Family.Models
{
    /// <summary>
    /// Товар магазину
    /// </summary>
    public class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // "role", "badge", "cosmetic"
        public long Price { get; set; } // Ціна в монетах
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
