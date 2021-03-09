using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Models
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            Minecraft = new MinecraftConfig();
        }

        public BaseConfig(string token, string prefix = "!")
        {
            Token = token;
            Prefix = prefix;
            Minecraft = new MinecraftConfig();
        }

        public string Token { get; set; }
        public string Prefix { get; set; }
        public MinecraftConfig Minecraft { get; set; }
    }
}
