using Anipat.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();              // API controller-lər üçün
builder.Services.AddEndpointsApiExplorer();    // Swagger üçün
builder.Services.AddSwaggerGen();              // Swagger
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();
// Frontend (wwwroot)
app.UseDefaultFiles();   // index.html-i default açır
app.UseStaticFiles();    // CSS/JS üçün

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

// API route-lər
app.MapControllers();
app.MapGet("/api/feedbacks", async (AppDbContext context) =>
{
    return await context.Feedbacks.ToListAsync();
});
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Anipat.DAL.SeedData.Initialize(services);
}

app.Run();
