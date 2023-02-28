using Microsoft.EntityFrameworkCore;
using libGatekeeper;
using libGatekeeper.Services.Authentication;
using libGatekeeper.Services.UserManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GatekeeperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"), b => b.MigrationsAssembly("libGatekeeper")));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHsts();
app.MapControllers();

app.Run();
