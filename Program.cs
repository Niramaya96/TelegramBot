using Microsoft.Extensions.Options;
using System;
using Telegram.Bot;
using TelegramBot.BotClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BotService>();

builder.Services.Configure<BotConfig>(builder.Configuration.GetSection("BotConfig"));

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();
//app.Map("/", (BotService bot) =>
//{
//    var client = bot.botClient;
//    return client.GetWebhookInfoAsync().Result.Url;
//});
app.Run();

