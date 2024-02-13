using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace XMLDemo
{
    /// <summary>
    /// Simple class that represents a nation, which is a collection of States.
    /// Authors: Daniel Kopta
    ///          Jim de St. Germain
    /// </summary>
    public class Nation
    {
        private List<State> _states; // backing property

        /// <summary>
        /// A property that allow access to our states variable
        /// </summary>
        public List<State> States
        {
            get { return _states; }
            set { _states = value; }
        }

        [JsonInclude]
        public String Founded { get; private set; } = "Not Founded";

        /// <summary>
        /// Default constructor. Initialize States list.
        /// </summary>
        public Nation( )
        {
            _states = new List<State>();
        }

        /// <summary>
        ///   Set Founded 
        /// </summary>
        public Nation(string founded) : this()
        {
            Founded = founded;
        }

        /// <summary>
        /// Adds a state to the Nation.
        /// </summary>
        /// <param Name="s">The State to add.</param>
        public void AddState(State s)
        {
            _states.Add(s);
        }

        /// <summary>
        /// Write an XML representation of this nation.
        /// </summary>
        /// <param Name="filename">The Name of the file where the XML representation will be saved.</param>
        public void WriteXml(string filename)
        {

            // We want some non-default settings for our XML writer.
            // Specifically, use indentation to make it more (human) readable.
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";

            // Create an XmlWriter inside this block, and automatically Dispose() it at the end.
            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Nation");

                // This adds an attribute to the Nation element
                writer.WriteAttributeString("Founded", "7/4/1776");

                writer.WriteStartElement("States");

                // write the states themselves
                foreach (State s in _states)
                    s.WriteXml(writer);

                writer.WriteEndElement(); // Ends the States block
                writer.WriteEndElement(); // Ends the Nation block
                writer.WriteEndDocument();

            }

        }

        /// <summary>
        /// Read and print the contents of an XML file represnting a Nation.
        /// TODO: Left as an exercise, extend this function so that it actually fills in the contents of a Nation object, and returns it.
        /// </summary>
        /// <param Name="filename">The Name of the file containing the XML to read.</param>
        public static void ReadXml(string filename)
        {
            // Create an XmlReader inside this block, and automatically Dispose() it at the end.
            using (XmlReader reader = XmlReader.Create(filename))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Nation":
                                // This is an example of reading an attribute on an element
                                Console.WriteLine("Nation founded: " + reader["Founded"]);
                                break; // no more direct info to read on Nation
                            case "States":
                                break; // no more direct info to read on States

                            case "State":
                                Console.WriteLine("Found state:");
                                break;

                            case "Name":
                                Console.Write("\tName = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;

                            case "Capital":
                                Console.Write("\tCapital = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;

                            case "Population":
                                Console.Write("\tPopulation = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;
                        }
                    }
                    else // If it's not a start element, it's probably an end element
                    {
                        if (reader.Name == "State")
                            Console.WriteLine("end of state");
                    }
                }
            }

        }

        /// <summary>
        /// Write a JSON representation of this nation.
        /// </summary>
        /// <param Name="filename"> The Name of the file where the JSON representation will be saved.</param>
        public void WriteJSON(string filename)
        {
            string output = JsonSerializer.Serialize(this);//FIXME: indext

            using (StreamWriter file = new StreamWriter(filename))
            {
                file.WriteLine(output);
            }

            Debug.WriteLine($"Wrote Json to File {filename}");
            Debug.WriteLine(output);
        }

        /// <summary>
        /// Read the contents of a JSON Nation file and return a newly created object from this data.
        /// </summary>
        /// <param Name="filename">The Name of the file containing the JSON to read.</param>
        public static Nation ReadJSON(string filename)
        {
            string json = File.ReadAllText(filename);

            return JsonSerializer.Deserialize<Nation>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string result = $"Nation Founded {Founded}\n";

            foreach (var state in _states)
            {
                result += "  " + state;
            }

            return result;
        }

    }
}