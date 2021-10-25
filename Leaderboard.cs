using System;
using System.Collections.Generic;
using System.IO;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    public static class Leaderboard
    {
        private const string FILE_NAME = "leaderboard.dat";
        private static string currentDirectory;
        private static string filePath;

        // Static Constructors are invoked automatically before the first instance is created or when a static member is referenced
        static Leaderboard()
        {
            // Files are saved in what ever directory our program is being ran at.
            // We need to generate a path to where our Leaderboard.dat file should be at
            currentDirectory = Directory.GetCurrentDirectory();
            filePath = Path.Combine(currentDirectory, FILE_NAME);

            // Once we have our file's path, we can check if the file exists.
            // If the file doesn't exist, we'll create it and then close the file.
            if (!Directory.Exists(filePath))
            {
                Stream myFile = File.Create(filePath);
                myFile.Close();
            }

        }

        // Load method will open our Leaderboard.dat file and return the data as a list
        // We don't need to do error checking as this is a Static Class
        // and our constructor already validates the existance of Leaderboard.dat
        public static List<Leaderboard_Data> Load()
        {
            // Create a temporary List to hold the data
            List < Leaderboard_Data > lb = new List<Leaderboard_Data>();
            
            // Read in each line from our Leaderboard.dat file
            foreach(string line in File.ReadAllLines(filePath))
            {
                // As our data is saved in the following format
                // GameType,Name,Score
                // We need to delimit the line at every , to extract correct data
                string[] dat = line.Split(',');

                // Create a temporary Leaderboard_Data variable to hold the delimited data
                Leaderboard_Data temp = new Leaderboard_Data(dat[0], dat[1], Int32.Parse(dat[2]));

                // Add each line to our temporary list
                lb.Add(temp);
            }

            // Return the list of data
            return lb;
        }

        // Our Save Method offers two ways to invoke Save
        // One method takes a Leaderboard_Data parameter whereas the other takes 2 string and an int
        // The data is then appended to the end of our Leaderboard.dat file
        // No need for error checking as we know the file exists (as per our static constructor)
        public static void Save(Leaderboard_Data data)
        {
            File.AppendAllText(filePath, $"{data.GameType},{data.Name},{data.Score}\n");
        }

        public static void Save(string GameType, string Name, int Score)
        {
            File.AppendAllText(filePath, $"{GameType},{Name},{Score}\n");
        }
    }
}
