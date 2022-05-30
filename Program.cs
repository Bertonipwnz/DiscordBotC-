using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using Бункер2._0.Классы;


namespace Bunker3._1
{
    class Program : Game
    {
        static public DiscordClient discord;
        static string[] oborudovano;
        static string[] estsInBunker;
        static string[] proffesionRnd;
        static string[] healthRnd;
        static string[] phobiaRnd;
        static string[] hobbyRnd;
        static string[] HTR;
        static string[] luggageRnd;
        static string[] addInfoRnd;
        static string[] kartochka1Rnd;
        static string[] genders;
        static int session, session1, session2, session3, session4, session5, session6, session7, session8, session9, session10, session11;
       static Game character = new Game();
        static CommandsNextModule commands;
        static void Main(string[] args)
        {
            MainTask(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainTask(string[] args)
        {
            Random rnd = new Random(); //иницилизируем переменную типа рандом и выделяем для неё память в куче
            int kolvo = 12; //Инициализация переменной для количества игроков
            proffesionRnd = character.ShuffleAndRandom(Game.profession.Length, Game.profession);
            healthRnd = character.ShuffleAndRandom(Game.health.Length, Game.health);
            phobiaRnd = character.ShuffleAndRandom(Game.phobia.Length, Game.phobia);
            hobbyRnd = character.ShuffleAndRandom(Game.hobby.Length, Game.hobby);
            HTR = character.ShuffleAndRandom(Game.humanTraitRand.Length, Game.humanTraitRand);
            luggageRnd = character.ShuffleAndRandom(Game.luggage.Length, Game.luggage);
            addInfoRnd = character.ShuffleAndRandom(Game.additionalInformation.Length, Game.additionalInformation);
            kartochka1Rnd = character.ShuffleAndRandom(Game.kartochka1.Length, Game.kartochka1);
            oborudovano = character.ShuffleAndRandom(Game.oborudovanieBunker.Length, Game.oborudovanieBunker);
            estsInBunker = character.ShuffleAndRandom(Game.estInBunker.Length, Game.estInBunker);
            genders = Game.gender;
            string filepath; //переменная для пути к папке
            int i;
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
                
                if (message.StartsWith("!правила") )
                {
                    await e.Message.RespondWithFileAsync(Game.pathPravilo);
                }
                if (message.StartsWith("!начать игру") && e.Message.Author.Username == "Michail")
                {
                    message = "Катастрофа: " + character.Disaster(); //записываем в richTextBox1 выход метода Disaster;
                    await e.Message.RespondAsync(message);
                    message = "Бункер: " + character.Bunker(oborudovano, estsInBunker); //записываем в richTextBox2 выход метода Bunker с входными параметрами
                    await e.Message.RespondAsync(message);
                    int shkolnikID = rnd.Next(1, kolvo); //переменная для формирования школьника
                    int dedID = rnd.Next(1, kolvo); //переменная для формирования деда/бабушки
                    while (shkolnikID == dedID) //цикл на проверку совпадений номеров игрококов, если повторяется вызываем рандом ещё раз
                    {
                        shkolnikID = rnd.Next(1, kolvo);
                        dedID = rnd.Next(1, kolvo);
                    }
                    for (int numberplayer = 1, counter = 0, kart = kolvo; numberplayer < kolvo + 1; kart++, numberplayer++, counter++)//номер игрока, счётчик данных, чтобы не было повторений kart;
                    {
                        filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + numberplayer + ".txt"; //записываем в переменную номер игрока - номер документа
                        StreamWriter f = new StreamWriter(filepath, false); //создаём экземпляр и передаём filepath, false - создаёт новый документ если нету
                        int age = character.age(23, 50); //вызываем функцию для определения возраста и записываем в переменную age
                        if (numberplayer == shkolnikID || numberplayer == dedID) //формирования отдельных карточек среди общих для школьника/деда
                        {
                            if (numberplayer == shkolnikID)
                            {
                                //школьник
                                int shkolaAge = character.age(14, 18);
                                f.WriteLine("Желание работать или работает(на ваш выбор): " + proffesionRnd[counter]);
                                f.WriteLine("Пол: " + gender[rnd.Next(0, 2)]);
                                f.WriteLine("Возраст: " + shkolaAge + " лет");
                                f.WriteLine("Пассивка школьника(вы не можете выжить без бабушки/дедушки) под номером: " + dedID);
                                f.WriteLine("Багаж школьника+: " + luggageRnd[kart]);
                                f.WriteLine("Хобби: " + hobbyRnd[counter] + ". Стаж хобби: " + character.expHobby(shkolaAge) + " лет");
                            }
                            else
                            {
                                //дед/бабушка
                                int starikAge = character.age(60, 95);
                                f.WriteLine("Профессия: " + proffesionRnd[counter] + " Стаж работы: " + character.expDedWork(starikAge));
                                f.WriteLine("Профессия 2: " + proffesionRnd[kart] + " Стаж работы: " + character.expDedWork(starikAge));
                                f.WriteLine("Пол: " + gender[rnd.Next(0, 2)]);
                                f.WriteLine("Возраст: " + starikAge + " лет");
                                f.WriteLine("Ваш внук/внучка под номером: " + shkolnikID);
                                f.WriteLine("Хобби: " + hobbyRnd[counter] + ". Стаж хобби: " + character.expHobby(starikAge) + " лет");
                            }
                        }
                        else
                        {
                            //для остальных игроков
                            f.WriteLine("Профессия: " + proffesionRnd[counter] + " Стаж работы: " + character.expWork(age));
                            f.WriteLine("Пол: " + gender[rnd.Next(0, 2)]);
                            f.WriteLine("Возраст: " + age);
                            f.WriteLine("Хобби: " + hobbyRnd[counter] + ". Стаж хобби: " + character.expHobby(age) + " лет");
                        }
                        //характеристики для всех
                        f.WriteLine(character.Health(healthRnd[counter]));
                        f.WriteLine("Фобия: " + phobiaRnd[counter]);
                        f.WriteLine("Человеческая черта: " + HTR[counter]);
                        f.WriteLine("Багаж: " + luggageRnd[counter]);
                        f.WriteLine("Телосложение: " + character.RashetIMT(healthRnd[counter]));
                        f.WriteLine("Дополнительная информация: " + addInfoRnd[counter]);
                        f.WriteLine("Карточка действий 1: " + kartochka1Rnd[counter]);
                        f.WriteLine("Карточка действий 2: " + kartochka1Rnd[kart]);
                        f.Close(); //закрываем документ
                        character.VragDrug(filepath);//вызываем метод для генерация карточек врага/друга
                    }

                }
                
            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 1") && session == 0)
                {
                    i = 1;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session++;
                }


            };

            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 2") && session1 == 0)
                {
                    i = 2;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session1++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 3") && session2 == 0)
                {
                    i = 3;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session2++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 4") && session3 == 0)
                {
                    i = 4;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session3++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 5") && session4 == 0)
                {
                    i = 5;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session4++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 6") && session5 == 0)
                {
                    i = 6;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session5++;
                }

            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 7") && session6 == 0)
                {
                    i = 7;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session6++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 8") && session7 == 0)
                {
                    i = 8;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session7++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 9") && session8 == 0)
                {
                    i = 9;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session8++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 10") && session9 == 0)
                {
                    i = 10;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session9++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 11") && session10 == 0)
                {
                    i = 11;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session10++;
                }


            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!карточка 12") && session11 == 0)
                {
                    i = 12;
                    await e.Message.RespondAsync("Карточка игрока: " + i);
                    filepath = @"C:\Users\User\Desktop\с#\Бункер" + @"\" + i + ".txt";
                    await e.Message.RespondWithFileAsync(filepath);
                    session11++;
                }


            };

            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                string[] stroka;
                if (message.StartsWith("!голосование" ) && e.Message.Author.Username == "Michail")
                {
                    for (int count = 1; count != 12; count++)
                    {
                        await e.Message.RespondAsync(count.ToString());
                    }
                    //await e.Message.RespondAsync(stroka);
                }
            };
            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!рестарт"))
                {
                    await e.Message.RespondAsync("Перезапуск");
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    //это мы узнали полное имя запущенного приложения.
                    //чтоб запустить его снова сделаем так
                    System.Diagnostics.Process.Start(path);
                    //далее чтоб закрыть это приложение сделаем так
                    Process.GetCurrentProcess().Kill();
                    //хотя думаю просто вернув return в функции Main() закроет приложение
                    session = 0;
                    session1 = 0;
                    session2 = 0;
                    session3 = 0;
                    session4 = 0;
                    session5 = 0;
                    session6 = 0;
                    session7 = 0;
                    session8 = 0;
                    session9 = 0;
                    session10 = 0;
                    session11 = 0;
                }
               
            };

            discord.MessageCreated += async e =>
            {
                string message = e.Message.Content;
                if (message.StartsWith("!профессия"))
                {
                    await e.Message.RespondAsync("Новая профессия: " + proffesionRnd[rnd.Next(0, proffesionRnd.Length - 1)]);
                }
                if (message.StartsWith("!пол"))
                {
                    await e.Message.RespondAsync("Новый пол: " + genders[rnd.Next(0, genders.Length)]);
                }
                if (message.StartsWith("!возраст"))
                {
                    await e.Message.RespondAsync("Новый возраст: " + character.age(23, 50));
                }
                if (message.StartsWith("!здоровье"))
                {
                    await e.Message.RespondAsync("Новый состояние здоровья: " + healthRnd[rnd.Next(0, healthRnd.Length)]);
                }
                if (message.StartsWith("!фобия"))
                {
                    await e.Message.RespondAsync("Новая фобия: " + phobiaRnd[rnd.Next(0, phobiaRnd.Length)]);
                }
                if (message.StartsWith("!багаж"))
                {
                    await e.Message.RespondAsync("Новый багаж: " + luggageRnd[rnd.Next(0, luggageRnd.Length)]);
                }
                if (message.StartsWith("!черта"))
                {
                    await e.Message.RespondAsync("Новая человеческая черта: " + HTR[rnd.Next(0, HTR.Length)]);
                }
                if (message.StartsWith("!допинфа"))
                {
                    await e.Message.RespondAsync("Новая доп инфа: " + addInfoRnd[rnd.Next(0, addInfoRnd.Length)]);
                }
                if (message.StartsWith("!картадействия"))
                {
                    await e.Message.RespondAsync("Новая карточка действий: " + kartochka1Rnd[rnd.Next(0, kartochka1Rnd.Length)]);
                }
                if (message.StartsWith("!хобби"))
                {
                    await e.Message.RespondAsync("Новое хобби: " + hobbyRnd[rnd.Next(0, hobbyRnd.Length)]);
                }
   
                if (message.StartsWith("!сменитьвсемпрофессии"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.profession.Length, Game.profession);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемпол"))
                {
                    string stroka = null;
                    string A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = genders[rnd.Next(0, genders.Length)];
                        stroka += "Игрок " + numberplayer + " " + A + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемвозраст"))
                {
                    string stroka = null;
                    int A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.age(23, 50);
                        stroka += "Игрок " + numberplayer + " " + A + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемздоровье"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.health.Length, Game.health);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемфобию"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.phobia.Length, Game.phobia);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемхобби"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.hobby.Length, Game.hobby);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсембагаж"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.luggage.Length, Game.luggage);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемчерту"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.humanTraitRand.Length, Game.humanTraitRand);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
                if (message.StartsWith("!сменитьвсемдопинфу"))
                {
                    string stroka = null;
                    string[] A;
                    for (int numberplayer = 1, counter = 0; numberplayer != kolvo + 1; numberplayer++, counter++)
                    {
                        A = character.ShuffleAndRandom(Game.additionalInformation.Length, Game.additionalInformation);
                        stroka += "Игрок " + numberplayer + " " + A[counter] + "\n";
                    }
                    await e.Message.RespondAsync(stroka);
                }
            };
            discord.MessageCreated += async e =>
            {
                string stroka = null;
                string message = e.Message.Content;
                if (message.StartsWith("!help"))
                {
                    stroka =  "1) !правила - набор правил бункер" + "\n";
                    stroka += "2) !начать игру - генерирует карточки, катастрофу и бункер" + "\n";
                    stroka += "3) !карточка 1-12 - пишите в лс боту, карточка и ваш номер, карточка выдаётся только один раз!!!" + "\n";
                    stroka += "4) !голосование - возвращает цифры 1-12 подсчёт идёт лайками" + "\n";
                    stroka += "5) !профессия - генерирует новую рандомную профессию" + "\n";
                    stroka += "6) !пол - генерирует новый рандомный пол" + "\n";
                    stroka += "7) !возраст - генерирует новый рандомный возраст" + "\n";
                    stroka += "8) !фобия - генерирует новую рандомную фобию" + "\n";
                    stroka += "9) !багаж - генерирует новый рандомный багаж" + "\n";
                    stroka += "10) !черта - генерирует новую рандомную человеческую черту" + "\n";
                    stroka += "11) !допинфа - генерирует новую рандомную доп. информацию" + "\n";
                    stroka += "12) !картадействия - генерирует новую рандомную карточку действия" + "\n";
                    stroka += "13) !сменитьвсемпрофессии - генерирует всем новые профессии" + "\n";
                    stroka += "14) !сменитьвсемпол - генерирует всем новый пол" + "\n";
                    stroka += "15) !сменитьвсемвозраст - генерирует всем новый возраст" + "\n";
                    stroka += "16) !сменитьвсемздоровье - генерирует всем новое здоровье" + "\n";
                    stroka += "17) !сменитьвсемфобию - генерирует всем новую фобию" + "\n";
                    stroka += "18) !сменитьвсембагаж - генерирует всем новый багаж" + "\n";
                    stroka += "19) !сменитьвсемчерту - генерирует всем новую челов. черту" + "\n";
                    stroka += "20) !сменитьвсемдопинфу - генерирует всем новую доп информацию" + "\n";
                    await e.Message.RespondAsync(stroka);
                }
            };
            

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = ";;"
            });
            commands.RegisterCommands<MyCommands>();





            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
        


    }
}
