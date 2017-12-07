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
            GameLoader.LoadWorld(game, "H:\\RPGFinal\\Output\\game.xml");
            game.encounters[2].PerformEncounter(game.player, game);
        }
    }
}
