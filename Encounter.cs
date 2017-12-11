using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    /// <summary>
    /// Represents an encounter that can happen
    /// </summary>
    public class Encounter
    {
        bool completed = false;

        string announcement;
        string enemyName;
        string win, lose;
        string[] attackNames;
        int[] attackProbabilities;
        int[] attackBaseDamages;
        string[] abilityNames;
        int[] abilityProbabilities;
        string[] abilityModifiers;
        int enemyHealth;
        int rewardGold;
        int[] itemrewards;

        public Encounter(string enemyName, int enemyHealth, int rewardGold, int[] itemrewards, string announcement, string win, string lose, string[] attackNames, int[] attackBaseDamages, int[] attackProbabilities, string[] abilityNames, string[] abilityModifiers, int[] abilityProbabilities)
        {
            this.announcement = announcement;
            this.enemyHealth = enemyHealth;
            this.enemyName = enemyName;
            this.attackNames = attackNames;
            this.attackBaseDamages = attackBaseDamages;
            this.attackProbabilities = attackProbabilities;
            this.abilityNames = abilityNames;
            this.abilityModifiers = abilityModifiers;
            this.abilityProbabilities = abilityProbabilities;
            this.win = win;
            this.lose = lose;
            this.rewardGold = rewardGold;
            this.itemrewards = itemrewards;
        }

        //Does the encounter
        public void PerformEncounter(Player_Stats ps, Game game)
        {
            if (!completed)
            {
                Random random = new Random();
                int currEnemyHealth = enemyHealth;
                TextWriter.Write(announcement);
                bool playerRan = false;

                //While nobody is dead
                while (currEnemyHealth > 0 && ps.Health > 0 && !playerRan)
                {
                    bool doneWithInput = false;
                    char input = ' ';

                    WritePlayerOptions(ps, true, enemyName.ToLower() != "trap");

                    while (!doneWithInput)
                    {
                        doneWithInput = true;
                        input = Console.ReadKey().KeyChar;

                        switch (input)
                        {
                            case 'a':
                                Console.Clear();
                                TextWriter.Write("En garde!");
                                break;
                            case 'h':
                                if (ps.RemoveItem("health potion"))
                                {
                                    int potionHealth = game.GetItemFromName("health potion").metadata;
                                    ps.health += potionHealth;
                                    Console.Clear();
                                    TextWriter.Write($"You chug a health potion. %3+{potionHealth} health%0!`\nYour health is now %3{ps.health}%0!");
                                }
                                else
                                {
                                    Console.Clear();
                                    TextWriter.Write($"%2You have no health potions!%0");
                                }
                                break;
                            case 'r':
                                if (ps.money <= 0)
                                {
                                    TextWriter.Write($"\nYou have no gold! %4{enemyName}%0 attacks you for %5{random.Next(10)}%0 damage as you run!````");
                                    Console.Clear();
                                }
                                else
                                {
                                    int moneyloss = random.Next(15);
                                    if (ps.money < moneyloss)
                                    {
                                        moneyloss = ps.money;
                                        ps.money = 0;
                                    }
                                    else
                                    {
                                        ps.money -= moneyloss;
                                    }

                                    TextWriter.Write($"\nYou lost %5{moneyloss} gold%0 as you run away...` You now have %5{ps.money} gold%0.````");
                                    playerRan = true;
                                    Console.Clear();
                                }
                                break;
                            default:
                                Console.CursorLeft--;
                                Console.Write(" ");
                                Console.CursorLeft--;
                                doneWithInput = false;
                                break;
                        }
                    }

                    if (playerRan)
                    {
                        break;
                    }

                    //Randomly make the player or enemy attack first
                    if (random.Next(2) == 0)
                    {
                        PerformPlayerAttack(ps, random, ref currEnemyHealth);
                        if (currEnemyHealth > 0)
                        {
                            PerformEnemyAttack(ps, random, ref currEnemyHealth);
                        }
                    }
                    else
                    {
                        PerformEnemyAttack(ps, random, ref currEnemyHealth);
                        if (ps.health > 0)
                        {
                            PerformPlayerAttack(ps, random, ref currEnemyHealth);
                        }
                    }

                    if (currEnemyHealth <= 0)
                    {
                        TextWriter.Write($"\n%5YOU WIN!%0\n%4{win}%0\n");

                        ps.strength += 1;
                        ps.money += rewardGold;

                        TextWriter.Write($"Your strength increases to %3{ps.strength}%0!");
                        TextWriter.Write($"You gain %5{rewardGold} gold%0!");
                        foreach (int i in itemrewards)
                        {
                            string itemname = game.items[i].name.ToLower();
                            bool useAn = itemname[0] == 'a' || itemname[0] == 'e' || itemname[0] == 'i' || itemname[0] == 'o' || itemname[0] == 'u';
                            TextWriter.Write($"You got a{(useAn ? "n" : "")} %2{game.items[i].name}%0!");
                            ps.inv.Add(game.items[i]);
                        }
                        completed = true;
                    }
                    else if (ps.Health <= 0)
                    {
                        TextWriter.Write($"\n%2{lose}%0\n");
                    }
                }
            }
            else //if previously completed
            {
                TextWriter.Write($"You've already defeated %5{enemyName}%0.");
            }
        }

        private void WritePlayerOptions(Player_Stats ps, bool canDrinkPotion = true, bool canRun = true)
        {
            TextWriter.Write(
                $"\n%1------------------%0\n\n" +
                $"Player Health: %3{ps.health}%0\n" +
                $"Player Strength: %3{ps.strength}%0\n" +
                $"Player Weapon Attack: %4{ps.EquippedWeapon.metadata}%0\n" +
                $"Total Attack Power: %4{GetDamage(ps)}%0\n" +
                $"\nWhat will you do\\?\n" +
                $"'%3A%0' - Attack\\! ({ps.EquippedWeapon.name})\n" +
                $"{(canDrinkPotion ? $"'%3H%0' - Drink Health Potion (You have {ps.GetItemCount("health potion")}" : "")})" +
                $"{(canRun ? "\n'%3R%0' - Run Away (Lose a random amount of gold\\!)" : "")}" +
                "\n%1>>>%0 ", 10);
        }

        private void PerformPlayerAttack(Player_Stats ps, Random random, ref int enemyhealth)
        {
            Console.WriteLine();
            int damage = AnnouncePlayerAttack(ps, random, enemyhealth);
            enemyhealth -= damage;

            if (enemyhealth <= 0)
            {
                enemyhealth = 0;
                TextWriter.Write($"%5{enemyName}%0 has %30%0 health!", 25);
                return;
            }

            if (damage > 0)
            {
                TextWriter.Write($"%5{enemyName}%0's health is now %3{enemyhealth}%0!");
            }
        }

        private void PerformEnemyAttack(Player_Stats ps, Random random, ref int enemyhealth)
        {
            Console.WriteLine();
            int enemydamage = AnnounceEnemyAttack(ps, random, ref enemyhealth);
            ps.Health -= enemydamage;

            if (ps.Health <= 0)
            {
                ps.Health = 0;
                TextWriter.Write($"You have no health left!", 25);
                return;
            }

            if (enemydamage > 0)
            {
                TextWriter.Write($"Your health is now %3{ps.Health}%0.");
            }
        }

        //Gets enemy attack damage and announces attack
        private int AnnounceEnemyAttack(Player_Stats ps, Random random, ref int enemyhealth)
        {
            int enemyDamage = 0;
            int enemyAttack = Convert.ToInt32(random.Next(attackProbabilities.Sum() + abilityProbabilities.Sum()));
            int attIndex = GetIndexFromProbability(enemyAttack);
            if (attIndex != -1)
            {
                if (attIndex >= attackProbabilities.Length)
                {
                    attIndex -= attackProbabilities.Length;
                    UseAbility(abilityNames[attIndex], abilityModifiers[attIndex], ps, ref enemyhealth, ref rewardGold);
                }
                else
                {
                    enemyDamage = Convert.ToInt32(attackBaseDamages[attIndex] + (random.Next(7) - 3));

                    if (enemyDamage < 0)
                    {
                        enemyDamage = 0;
                    }

                    TextWriter.Write($"%5{enemyName}%0 %4{attackNames[attIndex]}%0 for %5{enemyDamage - ps.defense}%0 point{(enemyDamage > 1 ? "s" : "")} of damage!");
                }
            }

            return enemyDamage;
        }

        //Calculates player attack damage and announces attack
        private int AnnouncePlayerAttack(Player_Stats ps, Random random, int enemyhealth)
        {
            int playerDamage = Convert.ToInt32(GetDamage(ps) + (random.Next(7) - 3));
            if (playerDamage <= 0)
            {
                playerDamage = random.Next(3);
            }

            if (playerDamage < 0)
            {
                playerDamage = 0;
            }

            TextWriter.Write($"You attack %5{enemyName}%0 for %3{playerDamage}%0 point{(playerDamage > 1 ? "s" : "")} of damage!");

            return playerDamage;
        }

        //Gets the player's damage value
        //
        //  weapondmg^2 + strength
        //  ---------------------- = damage
        //          strength
        //
        //
        public int GetDamage(Player_Stats ps)
        {
            return Convert.ToInt32(ps.EquippedWeapon.metadata + ps.strength);
        }

        //Gets an attack or ability index given a probability
        public int GetIndexFromProbability(int randomnum)
        {
            int currAttackProbTotal = 0;
            for (int i = 0; currAttackProbTotal <= randomnum; ++i)
            {
                if (i < attackProbabilities.Length)
                {
                    currAttackProbTotal += attackProbabilities[i];
                    if (currAttackProbTotal > randomnum)
                    {
                        return i;
                    }
                }
                else if (i - attackProbabilities.Length < abilityProbabilities.Length)
                {
                    currAttackProbTotal += attackProbabilities[i - attackProbabilities.Length];
                    if (currAttackProbTotal > randomnum)
                    {
                        return i;
                    }
                }
                else
                {
                    return -1;
                }
            }

            return -1;
        }

        public void UseAbility(string abilityName, string abilityModifier, Player_Stats ps, ref int health, ref int gold)
        {
            string mod = abilityModifier.Substring(0, 1);
            int amnt = Convert.ToInt32(abilityModifier.Substring(1));
            switch (mod)
            {
                case "h":
                    health += amnt;
                    TextWriter.Write($"%5{enemyName}%0 %4{abilityName}%0 and recovers {amnt} health. %4{enemyName}%0's health is now %3{health}%0!");
                    break;
                case "g":
                    gold += amnt;
                    TextWriter.Write($"%5{enemyName}%0 %4{abilityName}%0 and {(amnt < 0 ? "gives you" : "takes")} {amnt} gold! %4{enemyName}%0 you now have %5{ps.Money} gold%0.");
                    break;
                case "s":
                    TextWriter.Write($"%5{enemyName}%0 %4{abilityName}%0 and swaps your health with theirs! %5{enemyName}%0 now has %3{ps.Health}%0 health and you now have %3{enemyHealth}%0 health.");
                    int healthswap = ps.Health;
                    ps.Health = enemyHealth;
                    enemyHealth = healthswap;
                    break;
            }
        }
    }
}