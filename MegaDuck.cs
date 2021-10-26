using System;
using System.Collections.Generic;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    static class MegaDuck
    {
        private static int selectedOption;

        public static void Menu()
        {
            selectedOption = 0;

            do
            {
                // Display Menu
                DisplayMenu();

                // Get user input, if they press enter do something
                if (GetUserInput())
                {
                    if (selectedOption == 4)
                        break;

                    if (selectedOption == 3)
                        ShowLeaderboard();

                    LoadLevel();
                }
            } while (true);
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("SELECT A MODULE TO LOAD");

            if (selectedOption == 0)
                ToggleHighlight();
            Console.WriteLine(" Tens");
            Console.ResetColor();

            if (selectedOption == 1)
                ToggleHighlight();
            Console.WriteLine(" Elevens");
            Console.ResetColor();

            if (selectedOption == 2)
                ToggleHighlight();
            Console.WriteLine(" Thirteens");
            Console.ResetColor();

            if (selectedOption == 3)
                ToggleHighlight();
            Console.WriteLine(" Leaderboard");
            Console.ResetColor();

            if (selectedOption == 4)
                ToggleHighlight();
            Console.WriteLine(" Quit");
            Console.ResetColor();
        }

        private static void ToggleHighlight()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }

        private static bool GetUserInput()
        {
            ConsoleKeyInfo input = Console.ReadKey();

            // Case if user pressed enter
            if (input.Key == ConsoleKey.Enter)
                return true;

            // Case if user pressed up or down arrow
            if (input.Key == ConsoleKey.UpArrow && selectedOption > 0)
                selectedOption--;
            else if (input.Key == ConsoleKey.DownArrow && selectedOption < 4)
                selectedOption++;

            // If we reach the end of this function then the user didn't
            // press enter
            return false;
        }

        private static void LoadLevel()
        {
            switch (selectedOption)
            {
                case 0:
                    // Load the TENS game
                    Ten ten = new Ten();
                    break;
                case 1:
                    // Load the ELEVENS game
                    Eleven eleven = new Eleven();
                    break;
                case 2:
                    // Load the Thirteen Game
                    Thirteen thirteen = new Thirteen();
                    break;
            }
        }

        private static void ShowLeaderboard()
        {
            Console.Clear();
            List<Leaderboard_Data> ls = Leaderboard.Load();

            Console.WriteLine("   Game    ║ Tag ║ Score");
            Console.WriteLine("═══════════╩═════╩═══════");

            for (int i = 0; i < ls.Count; i++)
            {
                Console.WriteLine($"{ls[i].GameType}  {ls[i].Name}  {ls[i].Score}");
                Console.WriteLine("═══════════╩═════╩═══════");
            }

            Console.WriteLine("\n\n Press any key to return to the menu");
            Console.ReadKey();
        }
    }
}