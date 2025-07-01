using Microsoft.Extensions.Options;
using SentinelAbstraction.Settings;
using SentinelBLL.Injections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<UserClientSettings>(
    builder.Configuration.GetSection(UserClientSettings.SectionName)
);

builder.Services.AddRefitInjections(builder.Configuration);

builder.Services.AddSentinelServices();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development\n");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
