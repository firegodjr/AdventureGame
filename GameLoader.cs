using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace RPG_Final
{
    static class GameLoader
    {
        static public void LoadWorld(string filepath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(new StreamReader(filepath, Encoding.GetEncoding("UTF-8")), settings);

            List<Encounter> encounters = new List<Encounter>();
            List<Room> rooms = new List<Room>();

            while(reader.Read())
            {
                string name = reader.Name;
                string value = reader.Value;

                switch(name)
                {
                    case "encounter":

                        reader.ReadStartElement();
                        string announcement = reader.ReadElementContentAsString();
                        string monsterName = reader.ReadElementContentAsString();
                        int monsterHealth = reader.ReadElementContentAsInt();
                        int gold = reader.ReadElementContentAsInt();
                        List<string> attackNames = new List<string>();
                        List<int> attackDamages = new List<int>();

                        reader.ReadStartElement();
                        while (reader.Name == "att")
                        {
                            string[] attack = reader.ReadElementContentAsString().Split(':');
                            attackNames.Add(attack[0]);
                            attackDamages.Add(Convert.ToInt32(attack[1]));
                        }
                        reader.ReadEndElement();

                        string win = reader.ReadElementContentAsString();
                        string lose = reader.ReadElementContentAsString();

                        encounters.Add(new Encounter(monsterName, monsterHealth, gold, announcement, win, lose, attackNames.ToArray(), attackDamages.ToArray()));
                        break;
                    case "room":
                        reader.ReadStartElement();

                        string desc = reader.ReadElementContentAsString();
                        int encIndex = reader.ReadElementContentAsInt();
                        string[] navStrings = reader.ReadElementContentAsString().Split(':');

                        int[] navTable = new int[] 
                        {
                            Convert.ToInt32(navStrings[0]),
                            Convert.ToInt32(navStrings[1]),
                            Convert.ToInt32(navStrings[2]),
                            Convert.ToInt32(navStrings[3])
                        };

                        Room room = new Room(desc, encounters[encIndex], navTable); //TODO: get the Encounter from the Encounter array
                        reader.ReadEndElement();
                        break;
                }
            }

            Game.encounters = encounters.ToArray();
            Game.rooms = rooms.ToArray();
        }
    }
}
