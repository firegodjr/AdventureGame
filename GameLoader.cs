using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace RPG_Final
{
    static class GameLoader
    {
        static public void LoadWorld(Game game, string filepath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(new StreamReader(filepath, Encoding.GetEncoding("UTF-8")), settings);

            List<Encounter> encounters = new List<Encounter>();
            List<Room> rooms = new List<Room>();
            List<Item> items = new List<Item>();

            while (reader.Read())
            {
                string name = reader.Name;
                string value = reader.Value;

                switch (name)
                {
                    case "encounter":

                        reader.ReadStartElement();
                        string announcement = reader.ReadElementContentAsString();
                        string monsterName = reader.ReadElementContentAsString();
                        int monsterHealth = reader.ReadElementContentAsInt();
                        int gold = reader.ReadElementContentAsInt();
                        List<string> attackNames = new List<string>();
                        List<int> attackProbabilities = new List<int>();
                        List<int> attackDamages = new List<int>();
                        List<string> abilityNames = new List<string>();
                        List<int> abilityProbabilities = new List<int>();
                        List<string> abilityMods = new List<string>();
                        List<int> itemRewards = new List<int>();

                        reader.ReadStartElement();
                        while (reader.Name == "att")
                        {
                            string[] attack = reader.ReadElementContentAsString().Split(':');
                            attackProbabilities.Add(Convert.ToInt32(attack[0]));
                            attackNames.Add(attack[1]);
                            attackDamages.Add(Convert.ToInt32(attack[2]));
                        }
                        if (attackNames.Count > 0)
                        {
                            reader.ReadEndElement();
                        }

                        reader.ReadStartElement();
                        while (reader.Name == "att")
                        {
                            string[] attack = reader.ReadElementContentAsString().Split(':');
                            abilityProbabilities.Add(Convert.ToInt32(attack[0]));
                            abilityNames.Add(attack[1]);
                            abilityMods.Add(attack[2]);
                        }
                        if (abilityNames.Count > 0)
                        {
                            reader.ReadEndElement();
                        }

                        reader.ReadStartElement();
                        while (reader.Name == "reward")
                        {
                            string[] itemString = reader.ReadElementContentAsString().Split(':');
                            itemRewards.Add(Convert.ToInt32(itemString[0]));
                        }
                        if (itemRewards.Count > 0)
                        {
                            reader.ReadEndElement();
                        }

                        string win = reader.ReadElementContentAsString();
                        string lose = reader.ReadElementContentAsString();

                        encounters.Add(new Encounter(monsterName, monsterHealth, gold, itemRewards.ToArray(), announcement, win, lose, attackNames.ToArray(), attackDamages.ToArray(), attackProbabilities.ToArray(), abilityNames.ToArray(), abilityMods.ToArray(), abilityProbabilities.ToArray()));
                        break;
                    case "room":
                        reader.ReadStartElement();

                        string roomName = reader.ReadElementContentAsString();
                        string desc = reader.ReadElementContentAsString();
                        int encIndex = reader.ReadElementContentAsInt();
                        string[] keyStrings = reader.ReadElementContentAsString().Split(':');
                        string[] navStrings = reader.ReadElementContentAsString().Split(':');

                        int[] keyMetas = new int[]
                        {
                            Convert.ToInt32(navStrings[0]),
                            Convert.ToInt32(navStrings[1]),
                            Convert.ToInt32(navStrings[2]),
                            Convert.ToInt32(navStrings[3])
                        };

                        int[] navTable = new int[]
                        {
                            Convert.ToInt32(navStrings[0]),
                            Convert.ToInt32(navStrings[1]),
                            Convert.ToInt32(navStrings[2]),
                            Convert.ToInt32(navStrings[3])
                        };

                        Room room = new Room(roomName, desc, encIndex, navTable, keyMetas);
                        break;
                    case "item":
                        reader.ReadStartElement();
                        string itemname = reader.ReadElementContentAsString();
                        ItemTypes type = (ItemTypes)reader.ReadElementContentAsInt();
                        int itemmeta = reader.ReadElementContentAsInt();

                        Item item = new Item(itemname, type, itemmeta);
                        items.Add(item);
                        break;
                }
            }

            game.encounters = encounters.ToArray();
            game.rooms = rooms.ToArray();
            game.items = items.ToArray();
        }
    }
}