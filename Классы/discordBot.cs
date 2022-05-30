using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Бункер2._0.Классы;

namespace Бункер2._0
{
    class discordBot : Game
    {
        static DiscordClient discord;
        Game obj = new Game();
        static void too(string[] args)
        {
            MainTask(args).ConfigureAwait(false).GetAwaiter().GetResult();

        }
        static public async Task MainTask(string[] args)
        {
            string[] rules = Game.readingText;
            string token = "NzQ5NTQyMjU5OTEyODY3ODUx.X0tfoA.l5C39drNU9aAGdxY9me4kQbPN7I";
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!правила"))
                {
                    for(int i=0; i<rules.Length; i++)
                    {
                        await e.Message.RespondAsync(rules[i]);
                    }
                    
                }

            };
            //discord.
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
