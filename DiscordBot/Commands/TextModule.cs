using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class TextModule : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"Greetings {ctx.User.Mention}!");
        }

        [Command("repeat")]
        public async Task RepeatCommand(CommandContext ctx, [RemainingText] string text)
        {
            await ctx.RespondAsync(text);
        }

        [Command("currentTime")]
        public async Task GetCurrentTimeForBotCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"The current datetime for the bot is {DateTimeOffset.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
        }

        [Command("random")]
        public async Task RandomCommand(CommandContext ctx, int min, int max)
        {
            var random = new Random();
            await ctx.RespondAsync($"Your number is: {random.Next(min, max)}");
        }

        [Command("ping")]
        public async Task PongCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Pong!");
        }
    }
}
