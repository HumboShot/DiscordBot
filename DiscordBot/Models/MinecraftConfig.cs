using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Models
{
    public class MinecraftConfig
    {
        public MinecraftConfig()
        {
            ServerPort = 25565;
            DownMessage = "The server is currently down. Contact the owner!";
        }

        public bool Enabled { get; set; }
        public string ServerIp { get; set; }
        public ushort ServerPort { get; set; }
        public string ServerMapWebsite { get; set; }
        public string DownMessage { get; set; }
    }
}
