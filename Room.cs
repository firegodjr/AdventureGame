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

        public Room(string description, Encounter encounter)
        {
            this.description = description;
            this.encounter = encounter;
        }
    }
}
