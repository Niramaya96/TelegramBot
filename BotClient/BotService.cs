using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using TelegramBot.Models;

namespace TelegramBot.BotClient
{
    public class BotService
    {
        public TelegramBotClient botClient { get; private set; }
        private readonly IOptions<BotConfig> options;

        public BotService(IOptions<BotConfig> options)
        {
            this.options = options;
            botClient = new TelegramBotClient(options.Value.BotToken);
            SetWebhook();
        }

        private async void SetWebhook()
        {
            try
            {
                string url = $"{options.Value.Url}{options.Value.Route}";
                //string route = $"{url}/bot";

                await botClient.SetWebhookAsync(url, cancellationToken: default);

                Console.WriteLine("Webhook установлен успешно.");

            }
            catch (ApiRequestException ex) when (ex.Message.Contains("retry after"))
            {
                Console.WriteLine($"Ошибка при установке вебхука: {ex.Message}. Повторная попытка через 1 секунду.");
                await Task.Delay(TimeSpan.FromSeconds(1));  // Повторная попытка после задержки
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при установке вебхука: {ex.Message}");
            }
        }
    }
}
