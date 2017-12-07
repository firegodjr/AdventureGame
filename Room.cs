using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    public class Room
    {
        public string name = "";
        public string description = "";
        public int encounter;
        public int[] navTable = new int[4];
        public int[] keyTable = new int[4];

        public Room(string name, string description, int encounter, int[] navTable, int[] keyTable)
        {
            this.name = name;
            this.description = description;
            this.encounter = encounter;
            this.navTable = navTable;
            this.keyTable = keyTable;
        }

        //public bool GetCanProgress(Player_Stats ps, Directions direction)
        //{
        //    foreach (Item i in ps.items)
        //    {
        //        if (i.type == ItemType.KEY && i.metadata == keyMetas[(int)direction])
        //        {
        //            return true;
        //          }
        //    }

        //    return true;
        //}
    }
}
