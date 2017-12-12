using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Authors: Drew Jones, Ethan Schrunk, Shilen Patel
    Description: A text-based adventure game that loads its details from an XML file
    Date: 12/12/17
*/
namespace RPG_Final
{
    class Program
    {
        public static Game game;
        public static SaveData savedata;
        public static int currRoom = 0;
        public static bool GameOver = false;
        static void Main(string[] args)
        {
            game = new Game();
            savedata = new SaveData();
            
            //Load the game from a file
            Console.OutputEncoding = Encoding.UTF8;
            GameLoader.LoadWorld(game, "H:\\AdventureGameUtil\\AdventureGameUtil\\bin\\Debug\\Output\\game.xml");

            //Player gets one health potion
            game.player.inv.Add(game.GetItemFromName("health potion"));

            //Load previous save
            TextWriter.Write("Load previous save? (Y/n)\n");
            char saveinput = Console.ReadKey().KeyChar;
            switch (saveinput.ToString().ToLower())
            {
                case "y":
                    LoadGame();
                    TextWriter.Write("\n%5Game Loaded!```");
                    Console.Clear();
                    break;
                case "n":
                    TextWriter.Write("Starting new game...```");
                    Console.Clear();
                    break;
            }

            //Keep looping as long as the game didn't end
            while (!GameOver)
            {
                //Perform the encounter if there is one, otherwise look for a chest. Don't look for a chest if it's the starting room.
                if (game.rooms[currRoom].encounter != -1)
                {
                    game.encounters[game.rooms[currRoom].encounter].PerformEncounter(game.player, game);

                    if(game.player.health <= 0)
                    {
                        GameOver = true;
                        break;
                    }

                    TextWriter.Write("%5\n" + game.rooms[currRoom].description + "%0\n");
                }
                else if(currRoom != 0)
                {
                    TextWriter.Write("%5\n" + game.rooms[currRoom].description + "%0\n");
                    DoRandomRoll(game.player);
                }
                else
                {
                    TextWriter.Write("%5\n" + game.rooms[currRoom].description + "%0\n");
                }

                TextWriter.Write("%0What will you do?%0\n");

                Command.Input(game.player);
                
                int nextroom = -1;
                int keyreq = -1;
                switch(Command.move)
                {
                    case Directions.NORTH:
                        nextroom = game.rooms[currRoom].navTable[0];
                        keyreq = game.rooms[currRoom].keyTable[0];
                        break;
                    case Directions.SOUTH:
                        nextroom = game.rooms[currRoom].navTable[1];
                        keyreq = game.rooms[currRoom].keyTable[1];
                        break;
                    case Directions.EAST:
                        nextroom = game.rooms[currRoom].navTable[2];
                        keyreq = game.rooms[currRoom].keyTable[2];
                        break;
                    case Directions.WEST:
                        nextroom = game.rooms[currRoom].navTable[3];
                        keyreq = game.rooms[currRoom].keyTable[3];
                        break;
                }

                if(Command.move != Directions.NONE)
                {
                    if (nextroom == -1)
                    {
                        TextWriter.Write("You can't go that way. There's no door.");
                    }
                    else if (keyreq != -1)
                    {
                        TextWriter.Write($"You can't open the door without a %5{game.items[keyreq].name}%0.");
                    }
                    else
                    {
                        currRoom = nextroom;
                        SaveGame();
                    }
                }

                Console.Clear();
            }

            //This happens when the game is over
            if(game.player.health <= 0)
            {
                TextWriter.Write("You died. That's pretty sad, tbh. Want to retry from the last room?\n'%4C%0' - Continue\n'%4X%0' - Exit Game");
                char input = Console.ReadKey().KeyChar;

                switch(input.ToString().ToLower())
                {
                    case "c":
                        break;
                    case "x":
                        TextWriter.Write("Thanks for playing!");
                        break;
                }
            }
            else
            {
                TextWriter.Write($"You beat the game! Congratulations!``\n\n%3Your Stats%0\n\nName: %5{game.player.name}%0\nStrength: %5{game.player.strength}%0\nGold: %5{game.player.money}%0\n\n``%3Thanks for playing!%0");
                Console.ReadKey();
            }
        }

        public static void DoRandomRoll(Player_Stats player)
        {
            Random random = new Random();
            if(random.Next(4) == 0)
            {
                TextWriter.Write("%4You found a hidden chest in this room! Inside you find...%0");

                int goldamnt = random.Next(20);
                int healthpotionamnt = random.Next(player.strength / 3);

                if (goldamnt > 0)
                {
                    TextWriter.Write($"%5{goldamnt} gold!%0");
                }

                if (healthpotionamnt > 0)
                {
                    TextWriter.Write($"%3{healthpotionamnt} health potion{(healthpotionamnt > 1 || healthpotionamnt == 0 ? "s" : "")}!%0");
                }
            }
            else
            {
                TextWriter.Write("%4You look around, but don't find anything particularly interesting.%0");
            }
        }

        private static void LoadGame()
        {
            //Load the previous save
            savedata.LoadXML("savegame.xml");
            game.player.name = savedata.GetValue<string>("playername");
            string[] invItemNames = savedata.GetValue<string>("playerinventory").Split(':');
            foreach (string s in invItemNames)
            {
                game.player.inv.Add(game.GetItemFromName(s));
            }
        }

        private static void SaveGame()
        {
            savedata.Set("playername", game.player.name);
            savedata.Set("playerhealth", game.player.health);
            savedata.Set("playergold", game.player.money);
            savedata.Set("playerstrength", game.player.strength);

            string invstring = "";

            foreach (Item i in game.player.inv)
            {
                invstring += i.name.ToLower() + ":";
            }

            invstring = invstring.Substring(0, invstring.Length - 1);

            savedata.Set("playerinventory", invstring);
            savedata.Set("currentroom", currRoom);

            savedata.SaveXML("savegame.xml");
        }
    }
}
