using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    public class Item
    {
        public string name;
        public ItemTypes type;
        public int metadata;

        public Item(string name, ItemTypes type, int metadata)
        {
            this.name = name;
            this.type = type;
            this.metadata = metadata;
        }
    }
}
