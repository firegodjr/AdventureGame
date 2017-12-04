using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Weapon
    {
        public string name;
        public int damage;

        public Weapon(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }
    }
}
