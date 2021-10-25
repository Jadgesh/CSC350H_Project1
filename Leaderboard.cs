using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    public static class Leaderboard
    {
        private const string FILE_NAME = "leaderboard.dat";
        private static string currentDirectory = null;
        private static string filePath = null;

        public static void Init()
        {
            // Check if a file has been found at the path specified
            // If there's not, create it.
            currentDirectory = Directory.GetCurrentDirectory();
            filePath = Path.Combine(currentDirectory, FILE_NAME);

            Console.WriteLine(filePath);

            if (!Directory.Exists(filePath))
            {
                Console.Write("File does not exist. Creating file now.");
                Stream myFile = File.Create(filePath);
                myFile.Close();
            }

        }

        public static List<Leaderboard_Data> Load()
        {
            List < Leaderboard_Data > lb = new List<Leaderboard_Data>();
            
            foreach(string line in File.ReadAllLines(filePath))
            {
                string[] dat = line.Split(',');

                Leaderboard_Data temp = new Leaderboard_Data(dat[0], dat[1], Int32.Parse(dat[2]));

                lb.Add(temp);
            }

            return lb;
        }

        public static void Save(Leaderboard_Data data)
        {
            File.AppendAllText(filePath, $"{data.GameType},{data.Name},{data.Score}\n");
        }
    }
}
