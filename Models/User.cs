namespace Raven_Family.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public UserRole Role { get; set; } = UserRole.УчасникСім_ї;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum UserRole
{
    УчасникСім_ї = 0,
    СтаршийСкладСім_ї = 1,
    Заступник = 2,
    Лідер = 3,
    ТехАдмін = 4
}
