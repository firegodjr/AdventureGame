using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Final
{
    class Encounter
    {
        //"A dragon attacks" or something
        string announcement;
        string enemyName;
        string win, lose;
        string[] attackNames;
        int[] attackBaseDamages;
        int enemyHealth;
        int rewardGold;

        public Encounter(string enemyName, int enemyHealth, int rewardGold, string announcement, string win, string lose, string[] attackNames, int[] attackBaseDamages)
        {
            this.announcement = announcement;
            this.enemyHealth = enemyHealth;
            this.enemyName = enemyName;
            this.attackNames = attackNames;
            this.attackBaseDamages = attackBaseDamages;
            this.win = win;
            this.lose = lose;
            this.rewardGold = rewardGold;
        }

        //Does the encounter
        public void PerformEncounter(Player_Stats ps)
        {
            int enemyRepeat = 0;
            int playerRepeat = 0;
            Random random = new Random();
            int health = enemyHealth;
            TextWriter.Write(announcement);
            while(health > 0 && ps.Health > 0)
            {
                if (random.Next(2) == 1 && playerRepeat < 2)
                {
                    playerRepeat++;
                    if(playerRepeat > 2)
                    {
                        playerRepeat = 0;
                    }

                    health -= PlayerAttack(ps, random, health);

                    if(health < 0)
                    {
                        health = 0;
                    }

                    TextWriter.Write($"{enemyName}'s health is now %3{health}%0!");
                }
                else if(enemyRepeat < 2)
                {
                    if(enemyRepeat > 2)
                    {
                        enemyRepeat = 0;
                    }

                    ps.Health -= EnemyAttack(ps, random);

                    if(ps.Health < 0)
                    {
                        ps.Health = 0;
                    }

                    TextWriter.Write($"Your health is now %3{ps.Health}%0.");
                }

                if(health <= 0)
                {
                    TextWriter.Write(win);
                    TextWriter.Write($"You gain %4{rewardGold} gold!");
                }
                else if(ps.Health <= 0)
                {
                    TextWriter.Write(lose);
                }
            }
        }

        private int EnemyAttack(Player_Stats ps, Random random)
        {
            int enemyAttack = Convert.ToInt32(random.Next(attackNames.Length));
            int enemyDamage = Convert.ToInt32(attackBaseDamages[enemyAttack] + (random.Next(7) - 3));

            if(enemyDamage < 0)
            {
                enemyDamage = 0;
            }

            TextWriter.Write($"%5{enemyName}%0 %4{attackNames[enemyAttack]}%0 for %5{enemyDamage}%0 points of damage!");

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
    }
}
