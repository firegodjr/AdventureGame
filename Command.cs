using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Command
    {
        string move = "";

        //gets the user 
        public void Eat()
        {
            int apple = Player_Stats.apple;
            int bread = Player_Stats.bread;
            int cake = Player_Stats.cake;
            string foodInput = "";
            TextWriter.Write("Would you like to consume apple(A), bread(B), or cake(C)");
            foodInput = Console.ReadLine();
            if (foodInput == "a" || foodInput == "A")
            {
                Player_Stats.apple -= 1;
                Player_Stats.hunger += 5;
            }
            else if (foodInput == "b" || foodInput == "B")
            {
                Player_Stats.bread -= 1;
                Player_Stats.hunger += 25;
            }
            else if (foodInput == "c" || foodInput == "C")
            {
                Player_Stats.cake -= 1;
                Player_Stats.hunger += 50;
            }
        }
        public void Buy()
        {
            TextWriter.Write("What weapon would you like to buy: \n1.Wood Sword: Damage = +5, Cost = $15\n2.Iron Sword: Damage = +10, Cost = $30\n3.Gold Sword: Damage = +15, Cost = $60\n4.Diamond Sword: Damage = +25, Cost = $100\n5.Armor: +5 Damage Redcued, Cost = $100\n6Magic Amulet: 1 Revive, Cost = $75");
        }
        public void Input()
        {
            int x = 0;
            while (x == 0)
            {
                TextWriter.Write("");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    TextWriter.Write("Error, What you entered is not acceptable. You haven't entered anything. Try again.\n");
                }
                else
                {
                    input = input.ToUpper();
                    switch (input)
                    {
                        case "?":
                        case "HELP":
                            Help();
                            break;
                        case "SOUTH":
                        case "S":
                            move = "south";
                            break;
                        case "E":
                        case "EAST":
                            move = "east";
                            break;
                        case "WEST":
                        case "W":
                            move = "west";
                            break;
                        case "N":
                        case "NORTH":
                            move = "north";
                            break;
                        case "BUY":
                        case "B":
                            Buy();
                            break;
                        case "CONSUME":
                        case "C":
                            Eat();
                            break;
                        case "R":
                        case "RUN":
                            move = "back";
                            break;
                        case "FIGHT":
                        case "F":
                            Inconter();
                            break;
                        case "PS":
                        case "STATS":
                        case "PLAYERSTATS":
                            Stats();
                            break;
                        case "INVENTORY":
                        case "I":
                            CheckInventory();
                            break;
                        case "LOCATION":
                        case "L":
                            GetLocation();
                            break;
                        default:
                            TextWriter.Write("Your code is broken");
                            break;
                    }
                }
            }
        }

        //gets the user input
        public void Help()
        {
            TextWriter.Write("To move South type s. \nTo move East type e. \nTo move West type w. \nTo move North type n. \nTo buy stuff type b. \nTo eat food type c. \nTo run from battle type r. \nTo Fight the monster type f. \nTo get player stats type ps or stats \nTo check your Inventory type i. \nTo grab an item type g. \nTo check your Location type l.");
        }

        public string Move
        {
            get
            {
                return move;
            }

            set
            {
                move = value;
            }
        }
    }
}
