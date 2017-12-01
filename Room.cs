using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Room
    {
        string description = "";
        Encounter encounter;
        int[] navTable = new int[4];

        public Room(string description, Encounter encounter, int[] navTable)
        {
            this.description = description;
            this.encounter = encounter;
            this.navTable = navTable;
        }
    }
}
