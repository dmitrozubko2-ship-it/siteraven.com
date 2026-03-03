using Raven_Family.Components;
using Raven_Family.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ===== SINGLETON SERVICES =====
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<ThemeService>();
builder.Services.AddSingleton<StatisticsService>();
builder.Services.AddSingleton<ShopService>();
builder.Services.AddSingleton<QuestService>();
builder.Services.AddSingleton<UserProfileService>();

// ===== SCOPED SERVICES =====
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<DiscordService>();

// Додати CORS для доступу з інших машин
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    // У розробці - не редиректити на HTTPS
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// Вимкнути HTTPS редирект для локальної розробки
// app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

Console.WriteLine("🐦 Сім'я Воронів запущена!");
Console.WriteLine("📍 Доступна за адресою: http://0.0.0.0:5000");
Console.WriteLine("🔗 Локальний доступ: http://localhost:5000");

app.Run();
