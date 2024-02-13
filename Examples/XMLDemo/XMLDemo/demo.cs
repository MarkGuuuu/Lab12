
///<summary>
///   Authors: Daniel Kopta
///            Jim de St. Germain
///   Date:    Spring 2020 (and previous years)
///   Copyright: CS 3500 - For instructional purposes only
///   
///   This code shows how to save and restore objects via XML and JSON
///   The XML serialization/deserialization is done manually
///   The JSON is done automatically via reflection
///</summary>

using System;

namespace XMLDemo
{
    class XMLDemo
    {
        /// <summary>
        ///   Choose which example to show.
        /// </summary>
        /// <param Name="args"></param>
        static void Main(string[] args)
        {
            ShowXML();
            //ShowJSON();

        }

        /// <summary>
        /// 1) Create an object using Code
        /// 2) Save that object to a file using XML format
        /// 3) Read the file to rebuild an object
        /// 
        /// Questions:  Where is the file saved? Why?
        /// </summary>
        static void ShowXML()
        {
            Nation US = new Nation();
            US.AddState(new State("Utah", "Salt Lake City", 3000000));
            US.AddState(new State("Wyoming", "Cheyenne", 12));

            // This will produce a file in bin/Debug/ (or Release if in Release mode)
            // Open that file and examine it for your own reference
            US.WriteXml("nation.xml");

            // This will read the file that was produced by the call above
            Nation.ReadXml("nation.xml");

            Console.ReadLine();
        }

        /// <summary>
        /// 1) Create an object using Code
        /// 2) Save that object to a file using JSON serialization
        /// 3) Read the file to rebuild an object
        /// 
        /// Answer to question in previous method:
        ///     The file in saved in the folder where the program is being run.
        ///     In this case: bin/Debug/ (or Release if in Release mode).
        /// </summary>
        static void ShowJSON()
        {
            Console.WriteLine("Creating a New Nation!");

            Nation US = new Nation("1776");
            US.AddState(new State("Utah", "Salt Lake City", 3000000));
            US.AddState(new State("Wyoming", "Cheyenne", 12));

            Console.WriteLine("Writing Nation to JSON file.");
            US.WriteJSON("nation.json");

            Console.WriteLine("---- Read a Nation in Via JSON ----");
            Nation new_nation = Nation.ReadJSON("nation.json");

            Console.WriteLine("---- Here is what I read ----");
            Console.WriteLine(new_nation);

            Console.WriteLine("---- Press Enter to End ----");
            Console.ReadLine();
        }

    }
}