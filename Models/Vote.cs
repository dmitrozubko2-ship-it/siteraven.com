using System;
using System.Collections.Generic;

namespace Raven_Family.Models
{
    /// <summary>
    /// Голосування (рада лідерів)
    /// </summary>
    public class Vote
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "active"; // "active", "completed"
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EndedAt { get; set; }

        public int? CreatedByUserId { get; set; } // Лідер

        // Результат
        public int WinningOption { get; set; } = 0;
        public string Conclusion { get; set; } = string.Empty; // Обговорення результату

        public virtual ICollection<VoteOption> Options { get; set; } = new List<VoteOption>();
    }

    /// <summary>
    /// Варіанти голосування
    /// </summary>
    public class VoteOption
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int VoteCount { get; set; } = 0;

        // navigation property can be null until associated
        public Vote? Vote { get; set; }
        public virtual ICollection<UserVote> UserVotes { get; set; } = new List<UserVote>();
    }

    /// <summary>
    /// Голос користувача
    /// </summary>
    public class UserVote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VoteOptionId { get; set; }
        public DateTime VotedAt { get; set; } = DateTime.Now;

        // navigation property can be null until loaded/assigned
        public VoteOption? VoteOption { get; set; }
    }
}
