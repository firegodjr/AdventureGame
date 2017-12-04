using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Command
    {
        string move;

        //gets the user input
        public void Input()
        {
            int x = 0;
            while (x == 0)
            {
                Console.WriteLine("");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Error, What you entered is not acceptable. You haven't entered anything. Try again.\n");
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
                        case "GRAB":
                        case "G":
                            Grab();
                            break;
                        case "LOCATION":
                        case "L":
                            GetLocation();
                            break;
                        default:
                            Console.WriteLine("Your code is broken");
                            break;
                    }
                }
            }
        }

        //gets the user input
        public void Help()
        {
            Console.WriteLine("To move South type s. \nTo move East type e. \nTo move West type w. \nTo move North type n. \nTo buy stuff type b. \nTo eat food type c. \nTo run from battle type r. \nTo Fight the monster type f. \nTo get player stats type ps or stats \nTo check your Inventory type i. \nTo grab an item type g. \nTo check your Location type l.");
        }
    }
}
