using Anipat.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVICES (Konfiqurasiya)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Identity (İstifadəçi idarəetməsi)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// JSON-da hərflərin kiçilməməsi üçün (JavaScript-də problem yaşamamaq üçün vacibdir)
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// 2. MIDDLEWARE (Ardıcıllıq çox vacibdir!)

// Səhifə ilk açılanda wwwroot içindəki index.html-i tapır
app.UseDefaultFiles();

// Şəkillərin (img/), CSS və JS fayllarının görünməsi üçün mütləqdir
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication hər zaman Authorization-dan əvvəl gəlməlidir
app.UseAuthentication();
app.UseAuthorization();

// API marşrutlarını xəritələyir
app.MapControllers();

// Qısa yolla Feedbacks API (əgər ayrıca controller yoxdursa)
app.MapGet("/api/feedbacks", async (AppDbContext context) =>
{
    return await context.Feedbacks.ToListAsync();
});

// Seed Data (Bazaya ilkin məlumatların doldurulması)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await Anipat.DAL.SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        // Seed zamanı xəta olarsa burada görmək olar
        Console.WriteLine(ex.Message);
    }
}

app.Run();