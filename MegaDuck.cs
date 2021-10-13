using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class MegaDuck
    {
        private System.Timers.Timer mainTimer;
        private int length = 0;
        private int selectedGame = 0;
        public MegaDuck()
        {

        }

        public void PowerOn()
        {
            // Play Loading Screen Animation
            LoadingScreen();

            // Display menu
            DisplayMenu();

            // Load Game selected Game
            LoadGame();
        }

        private void LoadingScreen()
        {
            // Print Game Console's Logo
            Console.SetWindowSize(53, 8); // We're assuming the target platform is Windows
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("• ▌ ▄ ·. ▄▄▄ . ▄▄ •  ▄▄▄·     ·▄▄▄▄  ▄• ▄▌ ▄▄· ▄ •▄");
            Console.WriteLine("·██ ▐███▪▀▄.▀·▐█ ▀ ▪▐█ ▀█     ██▪ ██ █▪██▌▐█ ▌▪█▌▄▌▪");
            Console.WriteLine("▐█ ▌▐▌▐█·▐▀▀▪▄▄█ ▀█▄▄█▀▀█     ▐█· ▐█▌█▌▐█▌██ ▄▄▐▀▀▄·");
            Console.WriteLine("██ ██▌▐█▌▐█▄▄▌▐█▄▪▐█▐█ ▪▐▌    ██. ██ ▐█▄█▌▐███▌▐█.█▌");
            Console.Write("▀▀  █▪▀▀▀ ▀▀▀ ·▀▀▀▀  ▀  ▀     ▀▀▀▀▀•  ▀▀▀ ·▀▀▀ ·▀  ▀");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n                      Loading\n");

            Console.ForegroundColor = ConsoleColor.Green;

            mainTimer = new System.Timers.Timer();
            mainTimer.Interval = 500; // Trigger timer every 1.5 seconds
            mainTimer.Elapsed += AddToLoadingBar;
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;

            while(length < 52)
            {
                continue;
            }

            mainTimer.Dispose(); // Timer gets killed x.x
        }

        private void AddToLoadingBar(Object source, ElapsedEventArgs e)
        {
            Console.Write("█");
            length++;
        }

        private void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ResetColor(); // Reset foreground and background to default
                Console.SetWindowSize(38, 12); // Set window to fit menu only
                Console.Write("╔════════════════════════════════════╗\n");
                Console.Write("║");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" Mega Duck                          ");
                Console.ResetColor();
                Console.Write("║\n");
                Console.ResetColor();
                Console.Write("╠════════════════════════════════════╣\n");
                Console.Write("║ Select a game to play              ║\n");
                Console.Write("║                                    ║\n");
                Console.Write("║");

                if(selectedGame == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" Tens                               ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" Tens                               ");
                }

                Console.Write("║\n║");

                if(selectedGame == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" Elevens                            ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" Elevens                            ");
                }

                Console.Write("║\n║");

                if (selectedGame == 2)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" Thirteens                          ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" Thirteens                          ");
                }

                Console.Write("║\n");

                Console.Write("╠════════════════════════════════════╣\n");
                Console.Write("║");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" Use ↑ and ↓ to highlight a game    ");
                Console.ResetColor();
                Console.Write("║\n║");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" Use [Enter] to select a game       ");
                Console.ResetColor();
                Console.Write("║\n╚════════════════════════════════════╝");

                // Key Input Portion

                ConsoleKeyInfo input = Console.ReadKey();

                if (input.Key == ConsoleKey.UpArrow && selectedGame > 0)
                    selectedGame--;

                if (input.Key == ConsoleKey.DownArrow && selectedGame < 2)
                    selectedGame++;

                if (input.Key == ConsoleKey.Enter)
                    break;

                Console.Clear();
            }
        }

        private void LoadGame()
        {
            switch (selectedGame)
            {
                case 0:
                    Ten tenGame = new Ten();

                    tenGame.Play();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
}
