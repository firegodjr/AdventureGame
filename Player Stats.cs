using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Player_Stats
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

        Item equippedWeapon = null;

        public string name;

        public Player_Stats()
        {
            name = getName();
            addItems();
        }

        public string getName()
        {
            //gets name from user
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            return name;
        }

        public void addItems()
        {
            //sets the inv
            //inv.Add("apple");
            //inv.Add("apple");
            //inv.Add("apple");
           // inv.Add("apple");
           // inv.Add("apple");
           // inv.Add("apple");
           // inv.Add("apple");
        }


        public void beginningMessage(string name, int strength, int money, int hunger, int health)
        {
            //prints 1st message at beginning of game
            Console.WriteLine(name + "Your strength is " + strength + "\nYou have " + money + "\nYour hunger is " + hunger + "\nYour health is " + health);
        }

        public bool RemoveItem(string itemname)
        {
            foreach(Item i in inv)
            {
                if(i.name.ToLower() == itemname.ToLower())
                {
                    inv.Remove(i);
                    return true;
                }
            }
            return false;
        }

        public static int apple = 0;
        public static int bread = 0;
        public static int cake = 0;
        public static int woodSword = 0;
        public static int ironSword = 0;
        public static int goldSword = 0;
        public static int diamondSword = 0;
        public static int torch = 0;
        public static int armor = 0;
        public static int magicAmulet = 0;
        public static int saveRoom = 0;

        public int Apple
        {
            get
            {
                return apple;
            }

            set
            {
                apple = value;
            }
        }

        public int Bread
        {
            get
            {
                return bread;
            }

            set
            {
                bread = value;
            }
        }

        public int Cake
        {
            get
            {
                return cake;
            }

            set
            {
                cake = value;
            }
        }

        public int WoodSword
        {
            get
            {
                return woodSword;
            }

            set
            {
                woodSword = value;
            }
        }

        public int IronSword
        {
            get
            {
                return ironSword;
            }

            set
            {
                ironSword = value;
            }
        }

        public int GoldSword
        {
            get
            {
                return goldSword;
            }

            set
            {
                goldSword = value;
            }
        }

        public int DiamondSword
        {
            get
            {
                return diamondSword;
            }

            set
            {
                diamondSword = value;
            }
        }

        public int Torch
        {
            get
            {
                return torch;
            }

            set
            {
                torch = value;
            }
        }

        public int Armor
        {
            get
            {
                return armor;
            }

            set
            {
                armor = value;
            }
        }

        public int MagicAmulet
        {
            get
            {
                return magicAmulet;
            }

            set
            {
                magicAmulet = value;
            }
        }

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
