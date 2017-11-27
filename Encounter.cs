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
        string[] attackNames;
        int[] attackBaseDamages;
        int enemyHealth;

        public Encounter(string enemyName, int enemyHealth, string announcement, string[] attackNames, int[] attackBaseDamages)
        {
            this.announcement = announcement;
            this.enemyHealth = enemyHealth;
            this.enemyName = enemyName;
            this.attackNames = attackNames;
            this.attackBaseDamages = attackBaseDamages;
        }

        //Does the encounter
        public void PerformEncounter(PlayerStats ps)
        {
            Random random = new Random();
            Console.WriteLine(announcement);
            while(enemyHealth > 0) //TODO: and player health > 0
            {
                //TODO: damage player and have player attack

                int enemyAttack = Convert.ToInt16(random.Next(attackNames.Length + 1));
                int enemyDamage = Convert.ToInt16(attackBaseDamages[enemyAttack] + (random.Next(7) - 6));
                Console.WriteLine($"{enemyName} {attackNames[enemyAttack]} you for {enemyDamage} points of damage!");
            }
        }

        public int GetDamage(PlayerStats ps)
        {
            //TODO: calculate player damage
            return 50;
        }
    }
}
