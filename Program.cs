using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Program
    {
        public static Game game;
        static void Main(string[] args)
        {
            game = new Game();
            Console.OutputEncoding = Encoding.UTF8;
            GameLoader.LoadWorld(game, "H:\\RPGFinal\\Output\\game.xml");

            game.player.strength = 5;
            game.player.inv.Add(game.GetItemFromName("health potion"));
            game.player.EquippedWeapon = game.GetItemFromName("wooden sword");
            game.encounters[3].PerformEncounter(game.player, game);

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
