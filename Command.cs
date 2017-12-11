using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    public static class Command
    {
        public static string move = "";

        //gets the user 
        public static void Input(Player_Stats player)
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
                            Buy(player, Program.game);
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
                            Stats(player);
                            break;
                        case "INVENTORY":
                        case "I":
                            CheckInventory(player);
                            break;
                        case "LOCATION":
                        case "L":
                            GetLocation();
                            break;
                        case "HEAL":
                        case "H":
                            Heal(player);
                            break;
                        case "M":
                        case "Map":
                            DisplayMap(player);
                            break;
                        default:
                            TextWriter.Write("Your code is broken");
                            break;
                    }
                }
            }
        }

        //gets player stats
        public static void Stats(Player_Stats player)
        {
            int strength = player.strength;
            int money = player.money;
            int hunger = player.hunger;
            int health = player.health;
            int defense = player.defense;

            TextWriter.Write("Health:%3\t" + health+"%0\n"+
                "Strength:%3\t" + strength + "%0\n" +
                "Hunger:%3\t" + hunger + "%0\n" +
                "Money:%3\t" + money + "%0\n" +
                "Defense:%3\t" + defense + "%0\n");
        }

        //gets player stats
        public static void CheckInventory(Player_Stats player)
        {
            for (int x = 0; x < player.inv.Count; x++)
            {
                TextWriter.Write(player.inv[x].name);
            }
        }

        //gets player stats
        public static void GetLocation()
        {

        }

        //gets player stats
        public static void DisplayMap(Player_Stats player)
        {
            if (player.GetItemCount("Map") != 0)
            {
                //TODO: map code
            }
            else
            {
                TextWriter.Write("You do not have a map. To get one buy one from the shop");
            }
        }

        //gets the user input
        public static void Help()
        {
            TextWriter.Write("To move South type '%4S%0'. \nTo move East type '%4E%0'. \nTo move West type '%4W%0'. \nTo move North type '%4N%0'. \nTo buy stuff type '%4B%0'. \nTo eat food type '%4C%0'. \nTo run from battle type '%4R%0'. \nTo Exit the game type exit or stop. \nTo get player stats type '%4ps%0' or stats \nTo check your Inventory type '%4I%0'. \nTo grab an item type '%4G%0'. \nTo check your Location type '%4L%0'. \n To look at the map type '%4M%0'.");
        }


        //user eats
        public static void Eat(Player_Stats player)
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

        //user eats
        public static void Heal(Player_Stats player)
        {
            string Healinput = "";
            TextWriter.Write("Would you like to use a health potion? y for yes");
            Healinput = Console.ReadLine();
            if (Healinput.ToLower() == "y" || Healinput.ToLower() == "yes")
            {
                if (player.RemoveItem("HealthPotion"))
                {
                    player.health += 20;
                }
            }
        }

        //opens the user shop 
        public static void Buy(Player_Stats ps, Game game)
        {
            TextWriter.Write(
                "What would you like to buy?\n\n" + 
                "%1Item Name\tCost\tEffects%0\n" + 
                "1\\.Wood Sword\t %5$15%0\t%2+5atk%0\n" + 
                "2\\.Iron Sword\t %5$30%0\t%2+10atk%0\n" + 
                "3\\.Master Sword\t %5$60%0\t%2+15atk%0\n" + 
                "4\\.Necreant Blade %5$100%0\t%2+25atk%0\n" + 
                "5\\.Health Potion\t %5$15%0\t%4+20 Health on Use%0\n" + 
                "6\\.Magic Armor\t %5$100%0\t%4+5 Defense%0\n" +
                "7\\.Area Map\t %5$100%0\t%4View the area map with 'M'%0\n" + 
                "Type '%5exit%0' to exit the shop.\n", 10);

            int input = Convert.ToInt32(char.GetNumericValue(Console.ReadKey().KeyChar));
            Console.CursorLeft--;
            Console.Write(" ");
            Console.CursorLeft--;

            switch(input)
            {
                case 1:
                    PurchaseItem(ps, game.GetItemFromName("wooden sword"), 15);
                    break;
                case 2:
                    PurchaseItem(ps, game.GetItemFromName("iron sword"), 30);
                    break;
                case 3:
                    PurchaseItem(ps, game.GetItemFromName("master sword"), 60);
                    break;
                case 4:
                    PurchaseItem(ps, game.GetItemFromName("necreant blade"), 100);
                    break;
                case 5:
                    PurchaseItem(ps, game.GetItemFromName("health potion"), 15);
                    break;
                case 6:
                    //TODO: just increase player defense
                    break;
                case 7:
                    //TODO: enable player map
                    break;
            }
        }

        private static void PurchaseItem(Player_Stats ps, Item item, int cost)
        {
            if(ps.money >= cost)
            {
                ps.money -= cost;
                ps.inv.Add(item);
                TextWriter.Write($"%5{cost}%0 gold removed.");
                TextWriter.Write($"You got the %4{item.name}%0!`");
            }
        }

        public static string Move
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
