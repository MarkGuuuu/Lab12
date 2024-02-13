using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace XMLDemo
{
    /// <summary>
    /// A simple class representing a State.
    /// </summary>
    public class State
    {
        public string   Name       { get; private set; }
        public string   Capital    { get; private set; }
        public int      Population { get; private set; }

        /// <summary>
        /// Create a new state with the given Name, Capital, and Population
        /// </summary>
        /// <param Name="_major"></param>
        public State(string name, string capital, int population)
        {
            Name = name;
            Capital = capital;
            Population = population;
        }

        /// <summary>
        /// Write this State to an existing XmlWriter
        /// </summary>
        /// <param Name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("State");
            // We use a shortcut to write an element with a single string
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("Capital", Capital);
            writer.WriteElementString("Population", Population.ToString());
            writer.WriteEndElement(); // Ends the State block
        }

        /// <summary>
        /// Human readable version of state.
        /// </summary>
        public override string ToString()
        {
            return $"State: {Name}, Capital: {Capital}, Population: {Population}\n";
        }

    }
}