using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Models
{
    public class BaseConfig
    {
        public BaseConfig()
        {

        }

        public BaseConfig(string token, string prefix = "!")
        {
            Token = token;
            Prefix = prefix;
        }

        public string Token { get; set; }
        public string Prefix { get; set; }
    }
}
