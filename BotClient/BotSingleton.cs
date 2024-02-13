using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace TelegramBot.BotClient
{
    public static class BotSingleton
    {
        private static TelegramBotClient _client;

        public static async Task<TelegramBotClient> GetClient()
        {
            if (_client != null)
                return _client;

            _client = SetBotClient().Result;

            return _client;
        }

        private static async Task<TelegramBotClient> SetBotClient()
        {
            try
            {
                var client = new TelegramBotClient("6459513818:AAHIr-O0I0VCBKyjgau6f8poCXmZxyIikrM");

                string ngrokUrl = "https://fa25-212-164-38-153.ngrok-free.app";
                string webhookUrl = $"{ngrokUrl}/bot";

                await client.SetWebhookAsync(webhookUrl, cancellationToken: default);

                Console.WriteLine("Webhook установлен успешно.");

                return client;

            }
            catch (ApiRequestException ex) when (ex.Message.Contains("retry after"))
            {
                Console.WriteLine($"Ошибка при установке вебхука: {ex.Message}. Повторная попытка через 1 секунду.");
                await Task.Delay(TimeSpan.FromSeconds(1));
                return SetBotClient().Result;  // Повторная попытка после задержки
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при установке вебхука: {ex.Message}");
                return null;
            }
        }
    }
}
