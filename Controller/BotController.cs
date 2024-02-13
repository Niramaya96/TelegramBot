using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using TelegramBot.BotClient;
using Telegram.Bot.Types;

namespace TelegramBot.Bot
{
    [ApiController]
    [Route("/bot")]
    public class BotController : Controller
    {
        private readonly BotService botService;

        public BotController(BotService botService)
        {
            this.botService = botService;
        }

        #region Singleton
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Update update)
        //{
        //    await Console.Out.WriteLineAsync($"Пришло сообщение");

        //    try
        //    {
        //        var client = BotClient.Bot.GetClient().Result;
        //        var chatId = update.Message.Chat.Id;
        //        var from = update.Message.From;
        //        var text = update.Message.Text;

        //        await client.SendTextMessageAsync(chatId, $"{from} - {text}");

        //        await Console.Out.WriteLineAsync($"{update.Message.From}: {update.Message.Text}");

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        await Console.Out.WriteLineAsync(ex.Message);
        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetBotInfo()
        //{
        //    var client = BotClient.Bot.GetClient();

        //    return Ok(client.Status);
        //}
        #endregion
        #region Di
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            await Console.Out.WriteLineAsync($"Пришло сообщение");

            try
            {
                var client = botService.botClient;
                var chatId = update.Message.Chat.Id;
                var from = update.Message.From;
                var text = update.Message.Text;

                await client.SendTextMessageAsync(chatId, $"{from} - {text}");

                await Console.Out.WriteLineAsync($"{update.Message.From}: {update.Message.Text}");

                return Ok();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [Route("/")]
        public async Task<IActionResult> GetBotInfo()
        {
            var client = botService.botClient;

            return Ok(client.GetWebhookInfoAsync().Result.Url);
        }
        #endregion

    }
}
