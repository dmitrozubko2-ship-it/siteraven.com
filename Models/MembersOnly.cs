using System;
using System.Collections.Generic;

namespace Raven_Family.Models
{
    /// <summary>
    /// Закритий розділ для авторизованих користувачів
    /// </summary>
    public class MembersOnly
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; } // "strategy", "rules", "documents", "chat"
        
        public string RequiredRole { get; set; } = "УчасникСім_ї"; // Мінімальна роль для доступу
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? UpdatedByUserId { get; set; }
        
        public bool IsPinned { get; set; } = false;
    }
}
