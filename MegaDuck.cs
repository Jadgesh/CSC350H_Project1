using System;
using System.Text;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class MegaDuck
    {
        private int selectedOption;

        public MegaDuck()
        {
            MenuManager();
        }

        private void MenuManager()
        {
            selectedOption = 0;

            do
            {
                // Display Menu
                DisplayMenu();

                // Get user input, if they press enter do something
                if (GetUserInput())
                {
                    if (selectedOption == 3)
                        break;

                    LoadLevel();
                }
            } while (true);
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("╔═════════════╗");
            Console.WriteLine("║Select a game║");
            Console.WriteLine("╠═════════════╣");
            Console.Write("║");

            if (selectedOption == 0)
                ToggleHighlight();

            Console.Write("Tens         ");
            Console.ResetColor();
            Console.Write("║\n║");

            if (selectedOption == 1)
                ToggleHighlight();

            Console.Write("Elevens      ");
            Console.ResetColor();
            Console.Write("║\n║");

            if (selectedOption == 2)
                ToggleHighlight();

            Console.Write("Thirteens    ");
            Console.ResetColor();
            Console.Write("║\n║");

            if (selectedOption == 3)
                ToggleHighlight();

            Console.Write("Quit         ");
            Console.ResetColor();
            Console.Write("║\n");

            Console.WriteLine("╚═════════════╝");
        }

        private static void ToggleHighlight()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }

        private bool GetUserInput()
        {
            ConsoleKeyInfo input = Console.ReadKey();

            // Case if user pressed enter
            if (input.Key == ConsoleKey.Enter)
                return true;

            // Cased if user pressed up or down arrow
            if (input.Key == ConsoleKey.UpArrow && selectedOption > 0)
                selectedOption--;
            else if (input.Key == ConsoleKey.DownArrow && selectedOption < 3)
                selectedOption++;

            // If we reach the end of this function then the user didn't
            // press enter
            return false;
        }

        private void LoadLevel()
        {
            switch (selectedOption)
            {
                case 0:
                    // Load the TENS game
                    Ten ten = new Ten();
                    break;
                case 1:
                    // Load the ELEVENTS game
                    Eleven eleven = new Eleven();
                    break;
                case 2:
                    // Load the Thirteen Game
                    Thirteen thirteen = new Thirteen();
                    break;
            }
        }
    }
}