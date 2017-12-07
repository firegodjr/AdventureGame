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
        public void Input(Player_Stats player)
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
                            Eat(player);
                            break;
                        case "R":
                        case "RUN":
                            move = "back";
                            break;
                        case "Exit":
                        case "Stop":
                            TextWriter.Write("Are you sure you wish to quit? y for yes or any key for no");
                            string quit = Console.ReadLine();
                            if ((quit.ToLower() == "yes")||(quit.ToLower() == "y"))
                            {
                                Environment.Exit(0);
                            }
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

        //gets player stats
        public void Stats()
        {

        }

        //gets player stats
        public void CheckInventory()
        {

        }

        //gets player stats
        public void GetLocation()
        {

        }

        //gets the user input
        public void Help()
        {
            TextWriter.Write("To move South type s. \nTo move East type e. \nTo move West type w. \nTo move North type n. \nTo buy stuff type b. \nTo eat food type c. \nTo run from battle type r. \nTo Exit the game type exit or stop. \nTo get player stats type ps or stats \nTo check your Inventory type i. \nTo grab an item type g. \nTo check your Location type l.");
        }


        //user eats
        public void Eat(Player_Stats player)
        {
            string foodInput = "";
            TextWriter.Write("Would you like to consume apple(A), bread(B), or cake(C)");
            foodInput = Console.ReadLine();
            if (foodInput == "a" || foodInput == "A")
            {
                if(player.RemoveItem("Apple"))
                {
                    player.hunger += 5;
                }
            }
            else if (foodInput == "b" || foodInput == "B")
            {
                if (player.RemoveItem("Bread"))
                {
                    player.hunger += 20;
                }
            }
            else if (foodInput == "c" || foodInput == "C")
            {
                if (player.RemoveItem("Cake"))
                {
                    player.hunger += 50;
                }
            }
        }

        //opens the user shop 
        public void Buy()
        {
            TextWriter.Write("What weapon would you like to buy: \n1.Wood Sword: Damage = +5, Cost = $15\n2.Iron Sword: Damage = +10, Cost = $30\n3.Master Sword: Damage = +15, Cost = $60\n4.Big Black Sword: Damage = +25, Cost = $100\n5.Armor: +5 Damage Redcued, Cost = $100\n6Magic Amulet: 1 Revive, Cost = $75");
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
