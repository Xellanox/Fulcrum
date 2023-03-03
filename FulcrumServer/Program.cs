using Microsoft.EntityFrameworkCore;
using libGatekeeper;
using libGatekeeper.Services.Authentication;
using libGatekeeper.Services.UserManagement;
using Microsoft.OpenApi.Writers;
using libFulcrum;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGatekeeper(builder.Configuration);
builder.Services.AddFulcrum(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MigrateGatekeeper();
app.MigrateFulcrum();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
