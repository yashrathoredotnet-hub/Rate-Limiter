using Rate_Limiter.DbStore;
using Rate_Limiter.Interfaces;
using Rate_Limiter.Model;
using Rate_Limiter.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IRateLimiter, TokenBucketRateLimiter>();
builder.Services.AddSingleton<IRateLimitStore, MemoryRateLimitStore>();

builder.Services.Configure<RateLimitOptions>(
    builder.Configuration.GetSection("RateLimiting"));
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();