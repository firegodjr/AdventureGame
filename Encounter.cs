using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Encounter
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
        public void PerformEncounter(Player_Stats ps)
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
                    //Randomize whether or not the player attacks
                    //Also make sure the player hasn't attacked more than once already
                    if (random.Next(2) == 1 && playerRepeat < 2)
                    {
                        //Increase the count of times the player attacked
                        playerRepeat++;

                        //Reset the count of times the enemy attacked
                        if (enemyRepeat >= 2)
                        {
                            enemyRepeat = 0;
                        }

                        int damage = PlayerAttack(ps, random, health);
                        health -= damage;

                        if (health < 0)
                        {
                            health = 0;
                        }

                        if (damage > 0)
                        {
                            TextWriter.Write($"%5{enemyName}%0's health is now %3{health}%0!");
                        }
                    }
                    //Make sure the enemy hasn't attacked more than once already
                    else if (enemyRepeat < 2)
                    {
                        //Increase the count of times the enemy attacked
                        enemyRepeat++;

                        //Reset the count of times the player attacked
                        if (playerRepeat >= 2)
                        {
                            playerRepeat = 0;
                        }

                        int damage = EnemyAttack(ps, random, ref health);
                        ps.Health -= damage;

                        if (ps.Health < 0)
                        {
                            ps.Health = 0;
                        }

                        if (damage > 0)
                        {
                            TextWriter.Write($"Your health is now %3{ps.Health}%0.");
                        }
                    }

                    if (health <= 0)
                    {
                        TextWriter.Write(win);
                        TextWriter.Write($"You gain %5{rewardGold} gold%0!");
                        //TODO: GIVE PLAYER ITEM REWARDS
                        //foreach (Item i in itemrewards)
                        //  ps.GiveItem(i)
                    }
                    else if (ps.Health <= 0)
                    {
                        TextWriter.Write(lose);
                    }
                }
            }
        }

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
                    UseAbility(abilityNames[attIndex], abilityModifiers[attIndex], ref health, ref rewardGold);
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

        private int PlayerAttack(Player_Stats ps, Random random, int enemyhealth)
        {
            int playerDamage = Convert.ToInt32(ps.EquippedWeapon + (random.Next(7) - 3));

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
            for(int i = 0; currAttackProbTotal < randomnum; ++i)
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

        public void UseAbility(string abilityName, string abilityModifier, ref int health, ref int gold)
        {
            string mod = abilityModifier.Substring(0, 1);
            int amnt = Convert.ToInt32(abilityModifier.Substring(1));
            switch(mod)
            {
                case "h":
                    TextWriter.Write($"%5{enemyName}%0 %4{abilityName}%0 and recovers {amnt} health. %4{enemyName}%0's health is now %3{health}%0!");
                    health += amnt;
                    break;
                case "g":
                    TextWriter.Write($"%5{enemyName}%0 %4{abilityName}%0 and {(amnt < 0 ? "spends" : "gains")} {amnt} gold! %4{enemyName}%0 now has %5{gold} gold%0.");
                    gold += amnt;
                    break;
                case "s":
                    TextWriter.Write($"%5{enemyName}%0 %4{abilityName}%0 and swaps your health with theirs! %5{enemyName}%0 now has %3{health}%0 health and you now have %3{enemyHealth}%0 health.");
                    break;
            }
        }
    }
}
