﻿using System;
using System.Collections.Generic;
using System.IO;

namespace GuitarShopUI
{
    // 1. User reflecion for search \ Use dictionary
    // 2. Big nesting
    // 3. Guitar constructor has huge param list
    // 4. SRP heavily violated
    // 5. 
    class Program
    {
        static void Main()
        {
            List<Guitar> guitars = ReadData();

            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        Search(guitars);
                        break;
                    case "a":
                        Add(guitars);
                        break;
                }
            }
        }

        private static void Add(List<Guitar> guitars)
        {
            Console.Write("Enter guitar spec (ID Price Model Builder Type):");
            var line = Console.ReadLine();
            guitars.Add(MakeGuitar(line));
            File.AppendAllText("guitars.txt", Environment.NewLine + line);
        }

        private static Guitar MakeGuitar(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }

        private static void Search(List<Guitar> guitars)
        {
            Console.WriteLine("Enter search term:");

            var term = Console.ReadLine();

            foreach (var guitar in guitars)
            {
                if (guitar.Contains(term))
                    Console.WriteLine("Mathc found: {0}", guitar.ID);
            }
        }

        private static List<Guitar> ReadData()
        {
            var lines = File.ReadAllLines("guitars.txt");
            var guitars = new List<Guitar>();

            foreach (var line in lines)
            {
                guitars.Add(MakeGuitar(line));
            }

            return guitars;
        }
    }

    public class Guitar
    {
        public Guitar(string id, string name, string model, string builder, string type)
        {
            ID = id;
            Price = name;
            Model = model;
            Builder = builder;
            Type = type;
        }

        public string ID { get; set; }
        public string Price { get; set; }
        public string Model { get; set; }
        public string Builder { get; set; }
        public string Type { get; set; }

        public bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }
    }
}
