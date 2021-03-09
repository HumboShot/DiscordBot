using DiscordBot.Models;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MineStatLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class MinecraftModule : BaseCommandModule
    {
        private static BaseConfig config;
        public MinecraftModule()
        {
            config = Program.Config;
        }

        [Command("mcstatus")]
        public async Task StatusCommand(CommandContext ctx)
        {
            if (config.Minecraft.Enabled)
            {
                MineStat ms = new MineStat(config.Minecraft.ServerIp, config.Minecraft.ServerPort);
                if(ms.ServerUp)
                {
                    await ctx.RespondAsync($"The server currently has {ms.CurrentPlayers} players on it");
                } 
                else
                {
                    await ctx.RespondAsync(config.Minecraft.DownMessage);
                }
            }
        }

        [Command("mcmap")]
        public async Task MapCommand(CommandContext ctx)
        {
            if (config.Minecraft.Enabled)
            {
                await ctx.RespondAsync(Program.Config.Minecraft.ServerMapWebsite);
            }
        }
    }
}
