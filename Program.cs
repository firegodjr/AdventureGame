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
            GameLoader.LoadWorld(game, "C:\\Users\\709864\\Desktop\\gameformat.xml");
            Player_Stats ps = new Player_Stats();
            game.encounters[3].PerformEncounter(ps);
        }
    }
}
