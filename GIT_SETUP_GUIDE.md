# 📚 Git Setup Guide для Raven Family

## 🎯 Мета
Перевести весь проект у Git репозиторій та залити на GitHub для спільної роботи та резервної копії.

---

## ✅ Передумови

### 1️⃣ Встановити Git

**Windows:**
- Завантажити: https://git-scm.com/download/win
- Запустити інсталер
- Обрати "Use Git from Windows Command Prompt"

**Linux:**
```bash
sudo apt-get install git
```

**Mac:**
```bash
brew install git
```

**Перевірити встановлення:**
```bash
git --version
```

---

## 🚀 Пошаговий процес

### КРОК 1: Відкрити PowerShell/Terminal

Перейти в корзину проекту:
```bash
cd "E:\Family Raven\Web Raven Family\Raven Family\Raven Family"
```

---

### КРОК 2: Налаштувати Git

```bash
# Встановити ім'я користувача
git config --global user.name "Dmitro"

# Встановити email
git config --global user.email "dmitro.ra@gmail.com"

# (Опціонально) Перевірити налаштування
git config --list
```

---

### КРОК 3: Ініціалізувати Git репозиторій

```bash
# Ініціалізувати новий репозиторій
git init

# Перевірити статус
git status
```

**Очікуваний результат:**
```
On branch master

No commits yet

Untracked files:
  (use "git add <file>..." to include in what will be committed)
    README.md
    Program.cs
    ...
```

---

### КРОК 4: Додати всі файли

```bash
# Додати ВСІ файли
git add .

# Перевірити що додалось
git status
```

**Очікуваний результат:** Побачиш зелені файли зі статусом "new file:"

---

### КРОК 5: Зробити перший commit

```bash
git commit -m "🐦 Initial commit: Raven Family project with all features

- User authentication and registration
- Profile management (avatars, nicknames)
- Members list and leaderboard
- News, gallery, rules
- Contracts system
- Dashboard and statistics
- Admin panel
- Email verification
- Responsive design"
```

**Очікуваний результат:**
```
[master (root-commit) abc1234] 🐦 Initial commit: Raven Family project...
 X files changed, Y insertions(+)
```

---

## 🌐 КРОК 6: Залити на GitHub (Опціонально, але РЕКОМЕНДОВАНО)

### 6.1 Створити репозиторій на GitHub

1. Перейти на https://github.com/new
2. **Repository name:** `raven-family`
3. **Description:** "Raven Family - Interactive community platform with contracts, leaderboards, and user profiles"
4. **Public** або **Private** (обрати на свій розсуд)
5. **Не ставити галочку** на "Initialize this repository"
6. Натиснути **"Create repository"**

### 6.2 Пов'язати з GitHub

```bash
# Додати remote репозиторій
git remote add origin https://github.com/твій-GitHub-юзернейм/raven-family.git

# Перейменувати гілку на main (опціонально)
git branch -M main

# Залити всі файли
git push -u origin main
```

**Очікуваний результат:**
```
Enumerating objects: 150, done.
Counting objects: 100% (150/150), done.
...
To https://github.com/твій-юзернейм/raven-family.git
 * [new branch]      main -> main
Branch 'main' is set up to track remote branch 'main' from origin.
```

---

## 📋 Важливі файли які НЕ будуть залиті (за .gitignore)

✅ **Це ПРАВИЛЬНО!** Ці файли не повинні бути у GitHub:

```
bin/                      # Скомпільовані файли
obj/                      # Об'єктні файли
appsettings.json          # ⚠️ Містить паролі!
appsettings.*.json        # Конфігурація з секретами
.vs/                      # Visual Studio файли
.vscode/                  # VS Code файли
*.user                    # User-specific настройки
logs/                     # Логи
```

---

## 🔐 Безпека: Як обійтись без конфіденційних даних

### Проблема:
`appsettings.json` містить паролі SMTP та інші секрети!

### Рішення:

**1. Створити `appsettings.Example.json`:**

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "Kestrel": {
        "Endpoints": {
            "Http": {
                "Url": "http://0.0.0.0:5000"
            }
        }
    },
    "Smtp": {
        "Server": "smtp.gmail.com",
        "Port": "587",
        "From": "your-email@gmail.com",
        "Password": "YOUR_APP_PASSWORD_HERE"
    }
}
```

**2. Зберігати реальний `appsettings.json` локально**

```bash
# Видалити appsettings.json з git (якщо він там)
git rm --cached appsettings.json
git commit -m "Remove appsettings.json (contains secrets)"
```

**3. Додати інструкцію в README.md:**

```markdown
## Setup

1. Clone the repository
2. Copy `appsettings.Example.json` to `appsettings.json`
3. Fill in your SMTP credentials
4. Run `dotnet run`
```

---

## 📊 Корисні Git команди після першого commit

```bash
# Перевірити статус
git status

# Переглянути історію commits
git log --oneline

# Переглянути версію файлу
git show HEAD:Program.cs

# Откатити зміни файлу
git checkout -- Models/User.cs

# Додати конкретний файл
git add Models/User.cs

# Зробити commit
git commit -m "✨ feat: Add new user fields"

# Залити на GitHub
git push

# Стягнути обновлення з GitHub
git pull
```

---

## 🎯 Оптимальна структура Git workflow

### Для одного розробника:
```bash
# Редагування файлів
nano Program.cs

# Додаток змін
git add .

# Commit
git commit -m "Описання змін"

# Залити на GitHub
git push
```

### Для команди (у майбутньому):
```bash
# Створити нову гілку для фічі
git checkout -b feature/new-feature

# Редагування
nano file.cs

# Commit
git commit -m "feat: Add new feature"

# Залити гілку
git push -u origin feature/new-feature

# На GitHub: Зробити Pull Request
# Після review: Merge в main
```

---

## ✅ Контрольний список

- [ ] Git встановлено (`git --version` працює)
- [ ] Git налаштовано (`git config --global user.name` встановлено)
- [ ] Репозиторій ініціалізовано (`git init`)
- [ ] Всі файли додані (`git add .`)
- [ ] Перший commit зроблено (`git commit`)
- [ ] GitHub репозиторій створено (опціонально)
- [ ] Remote додано (`git remote -v`)
- [ ] Push зроблено (`git push`)
- [ ] `.gitignore` налаштовано для `.json` файлів

---

## 📞 Якщо щось не працює

### Помилка: "fatal: not a git repository"
```bash
# Рішення: ініціалізувати репозиторій
git init
```

### Помилка: "Please tell me who you are"
```bash
# Рішення: встановити користувача
git config --global user.name "Твоє Ім'я"
git config --global user.email "твій@email.com"
```

### Помилка: "fatal: 'origin' does not appear to be a 'git' repository"
```bash
# Рішення: додати remote
git remote add origin https://github.com/твій-юзернейм/raven-family.git
```

---

## 🎉 Готово!

Тепер твій проект у Git! 🚀

**Наступні кроки:**
1. Запрошувати друзів як collaborators (якщо приватний репо)
2. Використовувати Git для контролю версій
3. Робити регулярні commits
4. Залишати детальні описи змін

**Успіхів! 🐦**
