using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Бункер2._0.Классы;

namespace Bunker3._1
{
    public class MyCommands : Game
    {
        string filepath = @"C:\Users\User\Desktop\с#\Бункер";

        [Command("hi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!", is_tts: true);
        }
        [Command("Card")]
        public async Task CardPlayer(CommandContext ctx, int number)
        {
            await ctx.RespondAsync("Карточка игрока: " + number);
            filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + number + ".txt";
            await ctx.RespondWithFileAsync(filepath);
        }
        [Command("SwapAll")]
        public async Task SwapAll(CommandContext ctx, string character, int kolvo)
        {
            AllSwapCharacter(character, filepath, kolvo);
            await ctx.RespondAsync("Характеристики изменены", is_tts: true);
        }
        [Command("SwapOne")]
        public async Task SwapOne(CommandContext ctx, string character, string number)
        {
            OneSwapCharacter(character, filepath, number);
            await ctx.RespondAsync("Характеристика игрока " + number + " изменена", is_tts: true);
        }
        [Command("Restart")]
        public async Task Restart(CommandContext ctx)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            await ctx.RespondAsync("Бот перезапускается");
            //это мы узнали полное имя запущенного приложения.
            //чтоб запустить его снова сделаем так
            System.Diagnostics.Process.Start(path);
            //далее чтоб закрыть это приложение сделаем так
            Process.GetCurrentProcess().Kill();
            //хотя думаю просто вернув return в функции Main() закроет приложение
        }
        [Command("Ссылка")]
        public async Task Link(CommandContext ctx)
        {
            await ctx.RespondAsync("https://docs.google.com/spreadsheets/d/1brGlFnYy0s1JL3SSHgmzaUQv6iXIcrLn2vZSfIuRcRw/edit#gid=0");
        }

        [Command("Vote")]
        public async Task Vote(CommandContext ctx, int NumberOfPlayer)
        {
            await ctx.RespondAsync("Голосование выберите эмодзи от 1-12", is_tts: true);
        }

        [Command("GG")]
        public async Task GG(CommandContext ctx)
        {
             filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + 1 + ".jpg";
             await ctx.RespondWithFileAsync(filepath);
        }
        [Command("Осуждаю")]
        public async Task ViGde(CommandContext ctx)
        {
            filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + 2 + ".jpg";
            await ctx.RespondAsync("Кто не пришёл тот", is_tts: true);
            await ctx.RespondWithFileAsync(filepath);
        }
    }
}
