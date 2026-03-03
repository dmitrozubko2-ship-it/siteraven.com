# 📋 Git Команди - Шпаргалка

## 🔧 Налаштування

```bash
# Перший раз (один раз)
git config --global user.name "Твоє Ім'я"
git config --global user.email "твій@email.com"

# Перевірити
git config --global --list
```

---

## 📦 Ініціалізація репозиторію

```bash
# Створити новий репозиторій
git init

# Клонувати існуючий
git clone https://github.com/користувач/проект.git
```

---

## ➕ Додавання змін

```bash
# Додати ВСІ файли
git add .

# Додати конкретний файл
git add Program.cs

# Додати конкретну папку
git add Models/

# Показати що буде додано
git status
```

---

## 💾 Commit (збереження)

```bash
# Простий commit
git commit -m "Описання змін"

# Детальний commit
git commit -m "Заголовок

Детальне описання змін.
- Що було додано
- Що було змінено
- Запозичення"

# Amendify (виправити останній commit)
git commit --amend -m "Нове описання"
```

---

## 🌐 Remote (Удалений репозиторій)

```bash
# Додати remote
git remote add origin https://github.com/юзер/проект.git

# Переглянути remote
git remote -v

# Видалити remote
git remote remove origin

# Змінити URL
git remote set-url origin https://github.com/юзер/новий-проект.git
```

---

## 📤 Push (Залити на сервер)

```bash
# Перший раз на нову гілку
git push -u origin main

# Наступні разі
git push

# Залити конкретну гілку
git push origin feature-branch

# Залити всі гілки
git push --all
```

---

## 📥 Pull (Стягнути зі сервера)

```bash
# Стягнути оновлення з main
git pull

# Стягнути з конкретної гілки
git pull origin feature-branch

# Показати що буде стягнено
git fetch
```

---

## 🔍 Перегляд історії

```bash
# Список commits (коротко)
git log --oneline

# Список commits (детально)
git log

# Показати конкретний commit
git show abc123

# Показати які файли змінилися
git log --name-status

# Красивий графік гілок
git log --graph --oneline --all
```

---

## 🌳 Гілки (Branches)

```bash
# Список локальних гілок
git branch

# Список всіх гілок (включая remote)
git branch -a

# Створити нову гілку
git branch feature/new-feature

# Переключитися на гілку
git checkout feature/new-feature

# Створити і переключитися
git checkout -b feature/new-feature

# Видалити гілку
git branch -d feature/new-feature

# Видалити гілку (force)
git branch -D feature/new-feature

# Переименувати гілку
git branch -m старе-ім'я нове-ім'я
```

---

## 🔄 Merge (Об'єднання гілок)

```bash
# Об'єднати feature в main
git checkout main
git merge feature/new-feature

# Скасувати merge (якщо є конфлікти)
git merge --abort
```

---

## 🔙 Скасування змін

```bash
# Скасувати зміни в файлі (до додавання)
git checkout -- Program.cs

# Скасувати всі зміни в робочій папці
git checkout -- .

# Видалити файл з staging (перед commit)
git reset HEAD Program.cs

# Скасувати останній commit (файли залишуються)
git reset --soft HEAD~1

# Скасувати останній commit (файли видаляються)
git reset --hard HEAD~1

# Скасувати конкретний commit (усередині історії)
git revert abc123
```

---

## 🏷️ Tags (Мітки версій)

```bash
# Створити тег (для версій)
git tag v1.0.0

# Залити тег
git push origin v1.0.0

# Залити всі теги
git push origin --tags

# Список тегів
git tag

# Видалити тег
git tag -d v1.0.0
```

---

## 📊 Статус і інформація

```bash
# Статус репозиторію
git status

# Різниця між версіями (не збереженими)
git diff

# Різниця між commited версіями
git diff --cached

# Статистика (хто скільки написав)
git shortlog -sn

# Статистика файлів
git diff --stat
```

---

## 🔍 Пошук

```bash
# Знайти commit за словом
git log -S "текст"

# Знайти commit за повідомленням
git log --grep="pattern"

# Знайти commit що видалив текст
git log -p -S "текст"
```

---

## 💡 Корисні комбо

```bash
# Переглянути що ви змінили порівняно з GitHub
git diff origin/main

# Добавить лишь частину файлу (interactive)
git add -p

# Зробити commit усіх відстежуваних файлів без git add
git commit -a -m "Повідомлення"

# Переглянути всіх хто був на цій гілці
git branch -r

# Переглянути останні змінена (красиво)
git log -p --max-count=5
```

---

## 🚨 Швидкої救援команди

```bash
# Якщо все зломалось - зробити backup
git bundle create my-repo.bundle --all

# Видалити локальні зміни (увага!)
git reset --hard

# Почати з чистої доски
git clean -fd
```

---

## 📝 Приклад робочого дня

```bash
# Ранок: взяти оновлення
git pull

# Протягом дня: робити commits
git add .
git commit -m "feat: Add new feature"

# Кіль кілька раз
git add Models/
git commit -m "fix: Fix bug in model"

# Вечір: залити всі зміни
git push

# Перед сном: переглянути що сьогодні було
git log --oneline -10
```

---

## 🎯 Best Practices

```bash
# ✅ Добрі описи commits
git commit -m "feat: Add user authentication"
git commit -m "fix: Correct password validation bug"
git commit -m "docs: Update README with setup instructions"

# ❌ Погані описи commits
git commit -m "fix"
git commit -m "asdf"
git commit -m "updates"

# Типи commits:
# feat:  Нова функція
# fix:   Виправлення помилки
# docs:  Документація
# style: Форматування коду
# refactor: Переструктурування коду
# test: Тести
# chore: Утримування коду
```

---

## 🤝 Для команди

```bash
# Див хто писав кожну строку
git blame Program.cs

# Хто написав конкретний рядок
git blame -L 10,20 Program.cs

# Історія конкретного файлу
git log -p Program.cs

# Все що одна людина написала
git log --author="Dmitro"
```

---

**Готово! Тепер ти знаєш всі важливі команди Git! 🚀**
