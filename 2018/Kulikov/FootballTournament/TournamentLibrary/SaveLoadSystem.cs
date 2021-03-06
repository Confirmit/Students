﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TournamentLibrary
{
    public static class SaveLoadSystem
    {
        private static BinaryFormatter _formatter = new BinaryFormatter();
        private static string _fileName = "tournamentProgress.dat";

        public static void Save(Tournament tournament)
        {
            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fileStream, tournament);
            }
        }

        public static Tournament Load(IDataManager printer)
        {
            Tournament tournament;

            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                tournament = (Tournament)_formatter.Deserialize(fileStream);
            }

            tournament.SetPrinter(printer);

            return tournament;
        }
    }
}
