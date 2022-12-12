using Demo_ASP__SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        // Autorisé tout les headers et toutes les méthodes
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();

        // Limitation de l'origin
#if DEBUG
        // - Tout autorisé
        policy.AllowAnyOrigin();
#else
        // - Limité à une source
        policy.WithOrigins("https://localhost:4200");
#endif
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapHub<MessageHub>("/chathub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
