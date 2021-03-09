using DiscordBot.Commands;
using DiscordBot.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
    //Docs: https://dsharpplus.github.io/articles/preamble.html
    public class Program
    {
        public static BaseConfig Config;
        private const string CONFIG_FOLDER = "\\config";

        private static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            BaseConfig config;

            if (File.Exists(currentPath + CONFIG_FOLDER + "\\config.json"))
            {
                var configFile = File.ReadAllText(currentPath + CONFIG_FOLDER + "\\config.json");
                config = JsonConvert.DeserializeObject<BaseConfig>(configFile);

                // HACK: Used to update the config file, if there are new items added
                if(config != null)
                {
                    var configText = JsonConvert.SerializeObject(config, Formatting.Indented);
                    File.WriteAllText(currentPath + CONFIG_FOLDER + "\\config.json", configText);
                }
            }
            else
            {
                Console.WriteLine("no config.json found, creating file");
                var emptyConfig = new BaseConfig();
                var configText = JsonConvert.SerializeObject(emptyConfig, Formatting.Indented);
                Directory.CreateDirectory(currentPath + CONFIG_FOLDER);
                File.WriteAllText(currentPath + CONFIG_FOLDER + "\\config.json", configText);
                Console.WriteLine("File has been created, please fill out the information, and start the bot again");
                return;
            }

            if(config == null)
            {
                Console.WriteLine("Config could not be loaded");
                return;
            }

            Config = config;

            var client = new DiscordClient(new DiscordConfiguration()
            {
                Token = config.Token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            });

            var commands = client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { config.Prefix }
            });

            commands.RegisterCommands(Assembly.GetExecutingAssembly());

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}