# 💾 Інструкція з інтеграції базової даних

## 🎯 Мета

Цей документ показує, як підключити вашу адмін панель до базової даних, щоб дані зберігалися постійно.

---

## 📋 Передумови

- Entity Framework Core встановлена
- DbContext налаштований
- Міграції налаштовані

---

## 🛠️ Кроки для інтеграції

### 1. Створити моделі в папці `Models`

#### `Models/News.cs`
```csharp
using System;

namespace Raven_Family.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
```

#### `Models/FamilyMember.cs`
```csharp
using System;

namespace Raven_Family.Models
{
    public class FamilyMember
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Rank { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
```

#### `Models/Rule.cs`
```csharp
using System;

namespace Raven_Family.Models
{
    public class Rule
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
```

### 2. Оновити DbContext

Додайте до вашого `DbContext`:

```csharp
public DbSet<News> News { get; set; }
public DbSet<FamilyMember> FamilyMembers { get; set; }
public DbSet<Rule> Rules { get; set; }
```

### 3. Створити міграції

```bash
dotnet ef migrations add AddContentTables
dotnet ef database update
```

### 4. Створити сервіси для роботи з базою

#### `Services/NewsService.cs`
```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Raven_Family.Models;

namespace Raven_Family.Services
{
    public class NewsService
    {
        private readonly ApplicationDbContext _context;

        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<News>> GetAllNewsAsync()
        {
            return await _context.News.OrderByDescending(n => n.Date).ToListAsync();
        }

        public async Task<News> GetNewsAsync(int id)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<News> CreateNewsAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News> UpdateNewsAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task DeleteNewsAsync(int id)
        {
            var news = await GetNewsAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
        }
    }
}
```

#### `Services/FamilyMemberService.cs`
```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Raven_Family.Models;

namespace Raven_Family.Services
{
    public class FamilyMemberService
    {
        private readonly ApplicationDbContext _context;

        public FamilyMemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FamilyMember>> GetAllMembersAsync()
        {
            return await _context.FamilyMembers.OrderBy(m => GetRankOrder(m.Rank)).ToListAsync();
        }

        public async Task<FamilyMember> GetMemberAsync(int id)
        {
            return await _context.FamilyMembers.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<FamilyMember> CreateMemberAsync(FamilyMember member)
        {
            _context.FamilyMembers.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<FamilyMember> UpdateMemberAsync(FamilyMember member)
        {
            _context.FamilyMembers.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await GetMemberAsync(id);
            if (member != null)
            {
                _context.FamilyMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        private int GetRankOrder(string rank) => rank switch
        {
            "Leader" => 1,
            "Admin" => 2,
            "Officer" => 3,
            "Member" => 4,
            "Recruit" => 5,
            _ => 6
        };
    }
}
```

#### `Services/RuleService.cs`
```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Raven_Family.Models;

namespace Raven_Family.Services
{
    public class RuleService
    {
        private readonly ApplicationDbContext _context;

        public RuleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rule>> GetAllRulesAsync()
        {
            return await _context.Rules.OrderBy(r => r.Category).ToListAsync();
        }

        public async Task<Rule> GetRuleAsync(int id)
        {
            return await _context.Rules.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rule> CreateRuleAsync(Rule rule)
        {
            _context.Rules.Add(rule);
            await _context.SaveChangesAsync();
            return rule;
        }

        public async Task<Rule> UpdateRuleAsync(Rule rule)
        {
            _context.Rules.Update(rule);
            await _context.SaveChangesAsync();
            return rule;
        }

        public async Task DeleteRuleAsync(int id)
        {
            var rule = await GetRuleAsync(id);
            if (rule != null)
            {
                _context.Rules.Remove(rule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
```

### 5. Зареєструвати сервіси в `Program.cs`

```csharp
// Додайте в Program.cs:
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<FamilyMemberService>();
builder.Services.AddScoped<RuleService>();
```

### 6. Оновити компонент Admin.razor

Змініть на:

```csharp
@inject NewsService newsService
@inject FamilyMemberService memberService
@inject RuleService ruleService

protected override async Task OnInitializedAsync()
{
    newsList = await newsService.GetAllNewsAsync();
    membersList = await memberService.GetAllMembersAsync();
    rulesList = await ruleService.GetAllRulesAsync();
}

private async Task SaveNews()
{
    if (editingNews != null)
    {
        if (editingNews.Id == 0)
        {
            await newsService.CreateNewsAsync(new News
            {
                Icon = editingNews.Icon,
                Title = editingNews.Title,
                Content = editingNews.Content,
                Author = editingNews.Author,
                Date = editingNews.Date
            });
        }
        else
        {
            var existing = await newsService.GetNewsAsync(editingNews.Id);
            if (existing != null)
            {
                existing.Icon = editingNews.Icon;
                existing.Title = editingNews.Title;
                existing.Content = editingNews.Content;
                existing.Author = editingNews.Author;
                existing.Date = editingNews.Date;
                await newsService.UpdateNewsAsync(existing);
            }
        }
        newsList = await newsService.GetAllNewsAsync();
    }
    CancelNewsEdit();
    StateHasChanged();
}

// Аналогічно для членів та правил
```

---

## 🧪 Тестування

1. Запустіть додаток
2. Перейдіть на адмін панель
3. Додайте нову новину/члена/правило
4. Перезавантажте сторінку
5. Дані повинні залишитися (якщо все налаштовано правильно)

---

## 🚨 Можливі проблеми

### Помилка: "DbContext is not registered"
**Рішення:** Переконайтеся, що ви зареєстрували DbContext в `Program.cs`

### Помилка: "Table does not exist"
**Рішення:** Запустіть міграції: `dotnet ef database update`

### Помилка: "Cannot insert NULL into..."
**Рішення:** Переконайтеся, що всі обов'язкові поля заповнені

---

## 📞 Підтримка

Якщо у вас виникають проблеми, зв'яжіться з технічним адміністратором.

---

**Версія:** 1.0  
**Статус:** ✅ Готово до впровадження
