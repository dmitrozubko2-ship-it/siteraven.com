namespace Raven_Family.Services
{
    public class ThemeService
    {
        private string _currentTheme = "dark";
        public event Action? OnThemeChanged;

        public string CurrentTheme => _currentTheme;

        public bool IsDarkTheme => _currentTheme == "dark";

        public void ToggleTheme()
        {
            _currentTheme = _currentTheme == "dark" ? "light" : "dark";
            OnThemeChanged?.Invoke();
        }

        public void SetTheme(string theme)
        {
            if (theme == "dark" || theme == "light")
            {
                _currentTheme = theme;
                OnThemeChanged?.Invoke();
            }
        }

        public string GetThemeClass()
        {
            return _currentTheme == "dark" ? "theme-dark" : "theme-light";
        }
    }
}
