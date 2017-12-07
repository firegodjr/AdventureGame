using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    public class Player_Stats
    {

        //Items in inv
        public List<Item> inv = new List<Item>();

        //starting strength
        public int strength = 50;

        //starting money
        public int money = 100;

        //starting hunger
        public int hunger = 100;

        //starting health
        public int health = 100;

        Item equippedWeapon = new Item("Fists", ItemTypes.WEAPON, 0);

        public string name;

        public Player_Stats()
        {
            name = getName();
        }

        public string getName()
        {
            //gets name from user
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            return name;
        }


        public void beginningMessage(string name, int strength, int money, int hunger, int health)
        {
            //prints 1st message at beginning of game
            Console.WriteLine(name + "Your strength is " + strength + "\nYou have " + money + "\nYour hunger is " + hunger + "\nYour health is " + health);
        }

        public bool RemoveItem(string itemname)
        {
            foreach (Item i in inv)
            {
                if (i.name.ToLower() == itemname.ToLower())
                {
                    inv.Remove(i);
                    return true;
                }
            }
            return false;
        }

        public int GetItemCount(string name)
        {
            int count = 0;
            foreach(Item i in inv)
            {
                if(i.name.ToLower() == name)
                {
                    count++;
                }
            }
            return count;
        }

        public static int saveRoom = 0;

        public int SaveRoom
        {
            get
            {
                return saveRoom;
            }

            set
            {
                saveRoom = value;
            }
        }

        public int Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public int Money
        {
            get
            {
                return money;
            }

            set
            {
                money = value;
            }
        }

        public int Hunger
        {
            get
            {
                return hunger;
            }

            set
            {
                hunger = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Item EquippedWeapon
        {
            get
            {
                return equippedWeapon;
            }

            set
            {
                equippedWeapon = value;
            }
        }
    }
}
