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
            if(!completed)
            {
                int enemyRepeat = 0;
                int playerRepeat = 0;
                Random random = new Random();
                int health = enemyHealth;
                TextWriter.Write(announcement);

                //While nobody is dead
                while (health > 0 && ps.Health > 0)
                {
                    //Do this twice per turn
                    for(int i = 0; i < 2; ++i)
                    {

                        int damage = PlayerAttack(ps, random, health);
                        health -= damage;

                        if (health <= 0)
                        {
                            health = 0;
                            TextWriter.Write($"%5{enemyName}%0 has %30%0 health!", 25);
                            break;
                        }

                        if (damage > 0)
                        {
                            TextWriter.Write($"%5{enemyName}%0's health is now %3{health}%0!");
                        }


                        int enemydamage = EnemyAttack(ps, random, ref health);
                        ps.Health -= enemydamage;

                        if (ps.Health <= 0)
                        {
                            ps.Health = 0;
                            TextWriter.Write($"You have no health left!", 25);
                            break;
                        }

                        if (enemydamage > 0)
                        {
                            TextWriter.Write($"Your health is now %3{ps.Health}%0.");
                        }
                    }
                    

                    if (health <= 0)
                    {
                        TextWriter.Write($"\n%4{win}%0\n");
                        TextWriter.Write($"You gain %5{rewardGold} gold%0!");
                        foreach(int i in itemrewards)
                        {
                            TextWriter.Write($"You got a %2{game.items[i].name}%0!");
                            ps.inv.Add(game.items[i]);
                        }
                    }
                    else if (ps.Health <= 0)
                    {
                        TextWriter.Write($"\n%2{lose}%0\n");
                    }
                    else
                    {
                        TextWriter.Write($"\nPlayer Health: %3{ps.health}%0\nPlayer Attack: %4{ps.EquippedWeapon.metadata}%0\n\nWhat will you do\\?\n'%3A%0' - continue battle\\! ({ps.EquippedWeapon.name})\n'%3H%0' - use health potion (You have {ps.GetItemCount("health potion")})\n'%3R%0' - run (Lose a random amount of gold\\!)\n%1>>>%0 ", 5);
                        char input = Console.ReadKey().KeyChar;

                        switch(input)
                        {
                            case 'a':
                                Console.Clear();
                                TextWriter.Write("En garde!\n");
                                break;
                            case 'h':
                                if(ps.RemoveItem("health potion"))
                                {
                                    ps.health += game.GetItemFromName("health potion").metadata;
                                    Console.Clear();
                                    TextWriter.Write($"You chug a health potion. Your health is now %3{ps.health}%0!\n");
                                }
                                break;
                            case 'r':
                                if(ps.money <= 0)
                                {
                                    TextWriter.Write($"You have no money! {enemyName} damages you for {random.Next(10)} as you run!");
                                    Console.Clear();
                                }
                                else
                                {
                                    int moneyloss = random.Next(15);
                                    if(ps.money < moneyloss)
                                    {
                                        moneyloss = ps.money;
                                        ps.money = 0;
                                    }
                                    else
                                    {
                                        ps.money -= moneyloss;
                                    }

                                    TextWriter.Write($"You lost %4{moneyloss} gold%0 as you run away...` You now have %4{ps.money} gold%0.");
                                    Console.Clear();
                                }
                                break;
                        }
                    }
                }
            }
        }

        //Gets enemy attack damage and announces attack
        private int EnemyAttack(Player_Stats ps, Random random, ref int health)
        {
            int enemyDamage = 0;
            int enemyAttack = Convert.ToInt32(random.Next(attackProbabilities.Sum() + abilityProbabilities.Sum()));
            int attIndex = GetIndexFromProbability(enemyAttack);
            if(attIndex != -1)
            {
                if (attIndex >= attackProbabilities.Length)
                {
                    attIndex -= attackProbabilities.Length;
                    UseAbility(abilityNames[attIndex], abilityModifiers[attIndex], ps, ref health, ref rewardGold);
                }
                else
                {
                    enemyDamage = Convert.ToInt32(attackBaseDamages[attIndex] + (random.Next(7) - 3));

                    if (enemyDamage < 0)
                    {
                        enemyDamage = 0;
                    }

                    TextWriter.Write($"%5{enemyName}%0 %4{attackNames[attIndex]}%0 for %5{enemyDamage}%0 points of damage!");
                }
            }

            return enemyDamage;
        }

        //Calculates player attack damage and announces attack
        private int PlayerAttack(Player_Stats ps, Random random, int enemyhealth)
        {
            int playerDamage = Convert.ToInt32(ps.EquippedWeapon.metadata + (random.Next(7) - 3));

            if(playerDamage < 0)
            {
                playerDamage = 0;
            }

            TextWriter.Write($"You attack %5{enemyName}%0 for %3{playerDamage}%0 points of damage!");

            return playerDamage;
        }

        public int GetDamage(Player_Stats ps)
        {
            //TODO: calculate player damage
            return 50;
        }

        public int GetIndexFromProbability(int randomnum)
        {
            int currAttackProbTotal = 0;
            for(int i = 0; currAttackProbTotal <= randomnum; ++i)
            {
                if(i < attackProbabilities.Length)
                {
                    currAttackProbTotal += attackProbabilities[i];
                    if(currAttackProbTotal > randomnum)
                    {
                        return i;
                    }
                }
                else if(i - attackProbabilities.Length < abilityProbabilities.Length)
                {
                    currAttackProbTotal += attackProbabilities[i - attackProbabilities.Length];
                    if(currAttackProbTotal > randomnum)
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
            switch(mod)
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
