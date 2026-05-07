using Full_proj.Auth;
using Full_proj.DbContext;
using Full_proj.JWTService;
using Full_proj.repository;

using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataBase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);



builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie()
    .AddJwtBearer(options =>
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Path.StartsWithSegments("/ChatHub"))
                {
                    var accessToken = context.Request.Query["access_token"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }
                }

                return Task.CompletedTask;
            }
        });

builder.Services.AddAuthorization();
builder.Services.AddSignalR();
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
// --- РУЧНАЯ ОБРАБОТКА CORS (ВРЕМЕННОЕ РЕШЕНИЕ) ---

// ----------------------------------------------
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<Hub>("/ChatHub");
app.Run();
