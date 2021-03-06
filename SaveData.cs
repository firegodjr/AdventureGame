﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RPG_Final
{
    class SaveData
    {
        Dictionary<string, bool> bools = new Dictionary<string, bool>();
        Dictionary<string, int> ints = new Dictionary<string, int>();
        Dictionary<string, double> doubles = new Dictionary<string, double>();
        Dictionary<string, string> strings = new Dictionary<string, string>();

        private const int BOOL = 0,
                          INT = 1,
                          DOUBLE = 2,
                          STRING = 3;

        public void AddSetting(string settingName, bool value)
        {
            bools.Add(settingName, value);
        }

        public void AddSetting(string settingName, int value)
        {
            ints.Add(settingName, value);
        }

        public void AddSetting(string settingName, double value)
        {
            doubles.Add(settingName, value);
        }

        public void AddSetting(string settingName, string value)
        {
            strings.Add(settingName, value);
        }

        public bool GetBoolValue(string settingName)
        {
            return GetValue<bool>(settingName);
        }

        public T GetValue<T>(string settingName)
        {
            T value = default(T);
            if (typeof(T) == typeof(bool))
            {
                if (bools.ContainsKey(settingName))
                {
                    value = (T)Convert.ChangeType(bools[settingName], typeof(T));
                }
                else
                {
                    Set(settingName, default(bool));
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (ints.ContainsKey(settingName))
                {
                    value = (T)Convert.ChangeType(ints[settingName], typeof(T));
                }
                else
                {
                    Set(settingName, default(int));
                }
            }
            else if (typeof(T) == typeof(double))
            {
                if (doubles.ContainsKey(settingName))
                {
                    value = (T)Convert.ChangeType(doubles[settingName], typeof(T));
                }
                else
                {
                    Set(settingName, default(double));
                }
            }
            else if(typeof(T) == typeof(string))
            {
                if(strings.ContainsKey(settingName))
                {
                    value = (T)Convert.ChangeType(strings[settingName], typeof(T));
                }
                else
                {
                    Set(settingName, "");
                }
            }

            return value;
        }

        public void Set(string settingName, bool value)
        {
            if (bools.ContainsKey(settingName))
            {
                bools[settingName] = value;
            }
            else
            {
                AddSetting(settingName, value);
            }
        }

        public void Set(string settingName, int value)
        {
            if (ints.ContainsKey(settingName))
            {
                ints[settingName] = value;
            }
            else
            {
                AddSetting(settingName, value);
            }
        }

        public void Set(string settingName, double value)
        {
            if (doubles.ContainsKey(settingName))
            {
                doubles[settingName] = value;
            }
            else
            {
                AddSetting(settingName, value);
            }
        }

        public void Set(string settingName, string value)
        {
            if (strings.ContainsKey(settingName))
            {
                strings[settingName] = value;
            }
            else
            {
                AddSetting(settingName, value);
            }
        }

        public void RemoveSetting(string settingName, Type type = null)
        {
            if (type == null)
            {
                bools.Remove(settingName);
                ints.Remove(settingName);
                doubles.Remove(settingName);
            }
            else if (type == typeof(bool))
            {
                bools.Remove(settingName);
            }
            else if (type == typeof(int))
            {
                ints.Remove(settingName);
            }
            else if (type == typeof(double))
            {
                doubles.Remove(settingName);
            }
        }

        public void SaveXML(string filepath)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(filepath, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement("savefile");

            writer.WriteStartElement("bools");
            foreach (KeyValuePair<string, bool> keyval in bools)
            {
                writer.WriteElementString(keyval.Key, keyval.Value.ToString());
            }
            writer.WriteEndElement();

            writer.WriteStartElement("ints");
            foreach (KeyValuePair<string, int> keyval in ints)
            {
                writer.WriteElementString(keyval.Key, keyval.Value.ToString());
            }
            writer.WriteEndElement();

            writer.WriteStartElement("doubles");
            foreach (KeyValuePair<string, double> keyval in doubles)
            {
                writer.WriteElementString(keyval.Key, keyval.Value.ToString());
            }
            writer.WriteEndElement();

            writer.WriteStartElement("strings");
            foreach(KeyValuePair<string, string> keyval in strings)
            {
                writer.WriteElementString(keyval.Key, keyval.Value.ToString());
            }
            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Close();
        }

        public void LoadXML(string filepath)
        {
            XmlReader reader;
            try
            {
                reader = XmlReader.Create(filepath);
            }
            catch
            {
                TextWriter.Write("No save data found.");
                return;
            }

            int readerstage = 0;

            reader.ReadStartElement();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "bools")
                        {
                            readerstage = BOOL;
                        }
                        else if (reader.Name == "ints")
                        {
                            readerstage = INT;
                        }
                        else if (reader.Name == "doubles")
                        {
                            readerstage = DOUBLE;
                        }
                        else if (reader.Name == "strings")
                        {
                            readerstage = STRING;
                        }
                        else if (reader.Name != "")
                        {
                            string name = reader.Name;
                            string value = reader.ReadElementContentAsString();
                            switch (readerstage)
                            {
                                case BOOL:
                                    if (!bools.ContainsKey(name))
                                    {
                                        bools.Add(name, Convert.ToBoolean(value));
                                    }
                                    break;
                                case INT:
                                    if (!ints.ContainsKey(name))
                                    {
                                        ints.Add(name, Convert.ToInt16(value));
                                    }
                                    break;
                                case DOUBLE:
                                    if (!doubles.ContainsKey(name))
                                    {
                                        doubles.Add(name, Convert.ToDouble(value));
                                    }
                                    break;
                                case STRING:
                                    if(!doubles.ContainsKey(name))
                                    {
                                        strings.Add(name, Convert.ToString(value));
                                    }
                                    break;
                            }

                            Console.WriteLine($"Loaded  setting '{name}' with value '{value}'");
                        }
                        break;
                    }
                }

            reader.Close();
        }
    }
}
