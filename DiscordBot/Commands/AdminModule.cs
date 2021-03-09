using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class AdminModule : BaseCommandModule
    {
        [Command("delete")]
        public async Task DeleteCommand(CommandContext ctx, int amount, [RemainingText] string reason)
        {
            if (CheckForAdminPermission(ctx))
            {
                try
                {
                    // Get list of messages and flip it to get newst first
                    var messages = await ctx.Channel.GetMessagesAsync(amount);
                    if (messages.Count == 0)
                    {
                        await ctx.RespondAsync("Could not find any messages to delete");
                        return;
                    }
                    else
                    {
                        messages.Reverse();
                        var sortedMessage = messages.Where(x => (x.Timestamp - DateTimeOffset.Now).TotalDays < 14);
                        await ctx.Channel.DeleteMessagesAsync(sortedMessage, reason);
                    }
                }
                catch (Exception e)
                {
                    if (e is DSharpPlus.Exceptions.BadRequestException)
                    {
                        await ctx.RespondAsync($"Failed to delete all messages. Reason: {(e as DSharpPlus.Exceptions.BadRequestException).JsonMessage}");
                    };
                }
            }
            else
            {
                await ctx.RespondAsync($"I can't let you do that {ctx.User.Mention}");
            }
        }

        private bool CheckForAdminPermission(CommandContext ctx)
        {
            //Check if user is admin
            bool isAdmin = false;
            foreach (var role in ctx.Member.Roles)
            {
                var permissionLevel = role.CheckPermission(DSharpPlus.Permissions.Administrator);
                if (permissionLevel == DSharpPlus.PermissionLevel.Allowed)
                {
                    isAdmin = true;
                    break;
                }
            }

            return isAdmin;
        }
    }
}
