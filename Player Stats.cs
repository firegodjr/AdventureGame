using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Player_Stats
    {
        //starting strength
        int strength = 50;

        //starting money
        int money = 100;

        //starting hunger
        int hunger = 100;

        //starting health
        int health = 100;

        string name = getName();        

        public static string getName()
        {
            //gets name from user
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            return name;
        }
        

        public static void beginningMessage(string name, int strength, int money, int hunger, int health)
        {
            //prints 1st message at beginning of game
            Console.WriteLine(name + "Your strength is " + strength + "\nYou have " + money + "\nYour hunger is " + hunger + "\nYour helath is " + health);
        }

        int apple = 0;
        int bread = 0;
        int cake = 0;
        int woodSword = 0;
        int ironSword = 0;
        int goldSword = 0;
        int diamondSword = 0;
        int torch = 0;
        int armor = 0;
        int magicAmulet = 0;

        int saveRoom = 0;

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
    }
}
