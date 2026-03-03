# 🚀 БЫСТРИЙ СТАРТ: Git за 5 хвилин

## 📍 ТИ ЗНАХОДИШСЯ ТУТ:
```
E:\Family Raven\Web Raven Family\Raven Family\Raven Family
```

---

## ⚡ ШВИДКІ КОМАНДИ (скопіюй і вставь)

### КРОК 1: Встановити Git
Завантажи з: https://git-scm.com/download/win

### КРОК 2: Скопіюй це у PowerShell

```powershell
# Налаштування
git config --global user.name "Dmitro"
git config --global user.email "dmitro.ra@gmail.com"

# Ініціалізація
git init

# Додавання файлів
git add .

# Перший commit
git commit -m "🐦 Initial commit: Raven Family project"
```

### КРОК 3 (Опціонально): Залити на GitHub

```powershell
# 1. Перейти на https://github.com/new
# 2. Назвати repo: raven-family
# 3. Скопіюй цей код (замінити твої дані):

git remote add origin https://github.com/твій-юзернейм/raven-family.git
git branch -M main
git push -u origin main
```

---

## ✅ ВСЕ! Готово!

**Перевір:**
```powershell
git log --oneline
```

Має показати твій commit. 🎉

---

## 📚 Додаткові файли в папці:

- **GIT_SETUP_GUIDE.md** - Детальна інструкція
- **appsettings.Example.json** - Шаблон без паролів
- **SETUP_GUIDE.md** - Як запустити сайт
- **.gitignore** - Файли які не залиш у GitHub

---

## 🔐 ВАЖЛИВО: Конфіденційність

**appsettings.json містить паролі! Він не буде залитий на GitHub (.gitignore робить це)**

Якщо когось запросиш - дай їм `appsettings.Example.json` з інструкцією як заповнити свої дані.

---

**Готово! Твій проект у Git! 🐦**
