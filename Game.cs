using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    public class Game
    {
        public Encounter[] encounters;
        public Room[] rooms;
        public Item[] items;

        public Player_Stats player = new Player_Stats();

        public Item GetItemFromName(string name)
        {
            foreach(Item i in items)
            {
                if(i.name.ToLower() == name.ToLower())
                {
                    return i;
                }
            }

            return null;
        }
    }
}
