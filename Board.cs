using System;
using System.Collections.Generic;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    public abstract class Board
    {
        private protected Deck deck;
        private protected List<Card> inPlayCards;

        private protected bool[] selectedCards;

        private protected int targetSum;
        private protected int highlightedCard;
        private protected int maxInPlayCards;

        private protected string gameName;

        private int score;

        // Plays Game Loop
        private protected void Play()
        {
            DisplayBorder();

            HowToPlay();

            score = 0;

            bool ff = false;
            // Shuffle our deck
            deck.Shuffle();

            // Deal Cards
            DealCards();

            Console.Clear();

            DisplayBoard();

            DisplayGameName();

            DisplayCards();

            DisplayScore();
            TimeSpan timer = TimeSpan.FromSeconds(10);
            DateTime timerStart = DateTime.Now;
            int timeRemaining = 0;
            int lastTime = timeRemaining;
            // While there's valid matches still on the table
            // 
            while (TableHasValidCombo() && DateTime.Now - timerStart < timer)
            {
                timeRemaining = 10 - (int)Math.Round(((DateTime.Now - timerStart).TotalSeconds));

                if(timeRemaining != lastTime)
                {
                    DisplayTimeRemaining(timeRemaining);
                    lastTime = timeRemaining;
                }

                if (Console.KeyAvailable)
                {
                    ff = GetPlayerInput();

                    if (!ff)
                        DisplayCards();

                }

                if (ff)
                    break;

                if (SelectedCardsHasValidCombo())
                {
                    score++;

                    RemoveSelectedCards();
                    ResetSelectedCards();
                    RearrangeHighlightedCard();
                    DealCards();

                    DisplayCards();
                    DisplayScore();

                    // Reset Timer
                    timerStart = DateTime.Now;
                }
            }

            if(deck.Empty && inPlayCards.Count == 0)
            {
                // You won
                DisplayWinScreen();
            }
            else
            {
                DisplayLoseScreen();
                // You lost
            }
        }

        // Adds cards to our inPlayCards list 
        private void DealCards()
        {
            while (inPlayCards.Count < maxInPlayCards && !deck.Empty)
            {
                Card temp = deck.TakeTopCard();

                if(temp != null)
                    inPlayCards.Add(temp);
            }
        }

        // If our highlight card position is nolong a valid card
        // Reset our HIghlighted card position
        private void RearrangeHighlightedCard()
        {
            if (inPlayCards.Count - 1 < highlightedCard)
                highlightedCard = inPlayCards.Count - 1;
        }

        private static void AwaitKeyPress()
        {
            Console.ReadKey(true);
        }
        // Display our game board to our screen
        private void DisplayBoard()
        {
            Console.Clear();

            DisplayBorder();

            DisplayGameName();

            DisplayCards();

            DisplayScore();
        }

        private void DisplayTimeRemaining(int time)
        {
            Console.SetCursorPosition(21, 10);
            Console.Write(String.Format("Time: {0,2}", time));
        }

        private void DisplayGameName()
        {
            int x, y;
            (x, y) = Console.GetCursorPosition();

            Console.SetCursorPosition(2, 1);
            Console.Write(gameName);

            Console.SetCursorPosition(x, y);
        }

        private void DisplayScore()
        {
            int x, y;
            (x, y) = Console.GetCursorPosition();

            Console.SetCursorPosition(21, 11);

            Console.Write($"Score: {score}");
            Console.SetCursorPosition(x, y);
        }

        // Prints Losing screen
        private void DisplayLoseScreen()
        {
            Console.Clear();
            DisplayBorder();
            Console.SetCursorPosition(12, 4);
            Console.Write("You lose!");
            Console.SetCursorPosition(12, 6);
            Console.Write($"Score: {score}");
            Console.SetCursorPosition(2, 10);
            Console.Write("Enter your name: ");
            Leaderboard.Save(gameName, Console.ReadLine(), score);
        }

        // Prints Winning Screen
        private void DisplayWinScreen()
        {
            Console.Clear();
            DisplayBorder();
            Console.SetCursorPosition(12, 4);
            Console.Write("You win!");
            Console.SetCursorPosition(12, 6);
            Console.Write($"Score: {score}");
            Console.SetCursorPosition(2, 10);
            Console.Write("Enter your name: ");
            Leaderboard.Save(gameName, Console.ReadLine(), score);
        }

        // Prints the border of our game Window 
        private void DisplayBorder()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("╔");

            for (int i = 0; i < 31; i++)
                Console.Write("═");

            Console.Write("╗\n");

            for(int h = 0; h < 13; h++)
            {
                Console.Write("║");

                for (int w = 0; w < 31; w++)
                    Console.Write(" ");

                Console.Write("║\n");
            }

            Console.Write("╚");

            for (int i = 0; i < 31; i++)
                Console.Write("═");

            Console.Write("╝\n");
        }

        // Prints a visual representation of each card
        private void DisplayCards()
        {
            // Save our current Cursor Position so we can reset postion later
            int x, y;
            (x, y) = Console.GetCursorPosition();

            // We always want to start at 2,2
            int t = 2;
            int l = 2;

            // Starting position should be 2,2
            for(int i = 0; i < maxInPlayCards; i++)
            {
                if(i >= inPlayCards.Count)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(l, t);
                    Console.Write($"     ");
                    Console.SetCursorPosition(l, t + 1);
                    Console.Write($"     ");
                    Console.SetCursorPosition(l, t + 2);
                    Console.Write($"     ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;

                    if (highlightedCard == i)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    if (selectedCards[i])
                        Console.BackgroundColor = ConsoleColor.Yellow;

                    if (selectedCards[i] && highlightedCard == i)
                        Console.BackgroundColor = ConsoleColor.Green;


                    ChangeCardForeground(inPlayCards[i].Suit);
                    Console.SetCursorPosition(l, t);
                    Console.Write(CardSAndR(i, false));
                    Console.SetCursorPosition(l, t + 1);
                    Console.Write($"     ");
                    Console.SetCursorPosition(l, t + 2);
                    Console.Write(CardSAndR(i, true));
                }

                l += 6;

                // Update Row
                if ((i + 1) % 5 == 0)
                {
                    t += 4;
                    l = 2;
                }

                // Reset Background color
                Console.BackgroundColor = ConsoleColor.White;
            }

            // Reset cursor postion
            Console.SetCursorPosition(x, y);

            // Reset color
            Console.ResetColor();
        }

        // Take an index and a bool to specify if we're printing reversed or not
        private string CardSAndR(int index, bool reverse)
        {
            string SuitSymbol = SuitToSymbol(inPlayCards[index].Suit);
            string ValueSymbol = ValueToSymbol(inPlayCards[index].Value);
            string filler = "  ";

            // If our string length isn't long enough, add spaces to filler
            if (SuitSymbol.Length + ValueSymbol.Length + filler.Length < 5)
                filler += " ";

            // If we are requesting the reverse version return this
            if (reverse)
                return SuitSymbol + filler + ValueSymbol;

            return ValueSymbol + filler + SuitSymbol;
        }

        // Returns a string representation of our Suit
        private string SuitToSymbol(Suit a)
        {
            switch (a)
            {
                case Suit.Clubs:
                    return "♣";
                case Suit.Diamonds:
                    return "♦";
                case Suit.Hearts:
                    return "♥";
                case Suit.Spades:
                    return "♠";
            }

            return " ";
        }

        // Returns a string representation of our Rank
        private string ValueToSymbol(int a)
        {
            switch (a)
            {
                case 1:
                    return "A";
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";
            }
            return a.ToString();
        }

        // CHanging the foreground color depending on which suit
        // we're currently printing
        private void ChangeCardForeground(Suit a)
        {
            if (a == Suit.Clubs || a == Suit.Spades)
                Console.ForegroundColor = ConsoleColor.Black;
            else
                Console.ForegroundColor = ConsoleColor.Red;
        }

        // Gets the player input moves and moves our "highlighter"
        // Also selects cards if the user presses enter
        // if the player presses the F key they forfeit
        // function returns true then
        private bool GetPlayerInput() {

            ConsoleKeyInfo input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.F)
                return true;

            if (input.Key == ConsoleKey.LeftArrow && highlightedCard > 0)
                highlightedCard--;

            if (input.Key == ConsoleKey.RightArrow && highlightedCard + 1 < inPlayCards.Count)
                highlightedCard++;

            if (input.Key == ConsoleKey.UpArrow && highlightedCard - 5 >= 0)
                highlightedCard -= 5;

            if (input.Key == ConsoleKey.DownArrow && highlightedCard + 5 < inPlayCards.Count)
                highlightedCard += 5;

            if (input.Key == ConsoleKey.Enter)
                selectedCards[highlightedCard] = !selectedCards[highlightedCard];

            return false;
        }

        // Loops through our selectedCards array and set every element to false
        private protected void ResetSelectedCards()
        {
            for (int i = 0; i < selectedCards.Length; i++)
                selectedCards[i] = false;
        }

        // Loops through our inPlayCards and remove the cards we've selected
        // Loops through array backwards to avoid elements being shifted
        private void RemoveSelectedCards()
        {
            for(int i = inPlayCards.Count; i > 0; i--)
            {
                if (selectedCards[i - 1])
                {
                    inPlayCards.RemoveAt(i - 1);
                }
            }
        }

        // Checks if the table has any valid combos by checking
        // our SumCombo case and our AltCombo case
        private bool TableHasValidCombo()
        {
            if (HasSumCombo(inPlayCards) || HasAltCombo(inPlayCards))
                return true;
            return false;
        }

        // Check if our Selected Cards has a valid Combo
        private bool SelectedCardsHasValidCombo()
        {
            if (HasSumCombo() || HasAltCombo())
                return true;
            return false;
        }

        // Checks if the cards we've selected add up to our target sum
        private bool HasSumCombo()
        {
            int totalSelectedCards = 0; // Keeps track of the amount of cards we selected
            int sum = 0;
            for(int i = 0; i < inPlayCards.Count; i++)
            {
                if (selectedCards[i])
                {
                    sum += inPlayCards[i].Value;
                    totalSelectedCards++;
                }
            }

            // We have a valid SumCombo only if we selected 2 cards
            if (totalSelectedCards != 2)
                return false;

            // We have a valid sum
            if (sum == targetSum)
                return true;

            return false;
        }

        // Checks if cards on the table has atleast one pair that adds up to
        // the sum 
        private bool HasSumCombo(List<Card> a)
        {
            int sum = 0;
            for(int i = 0; i < inPlayCards.Count - 1; i++)
            {
                for(int j = i + 1; j < inPlayCards.Count; j++)
                {
                    sum = inPlayCards[i].Value + inPlayCards[j].Value;

                    if (sum == targetSum)
                        return true;
                }
            }
            return false;
        }

        private protected abstract bool HasAltCombo();

        private protected abstract bool HasAltCombo(List<Card> a);

        private protected abstract void WriteAltCombo();
        private void HowToPlay()
        {
            Console.SetCursorPosition(2, 1);
            Console.Write($"How to play {gameName}");
            Console.SetCursorPosition(2, 3);
            Console.Write("You can move with arrow keys");
            Console.SetCursorPosition(2, 4);
            Console.Write("You can toggle select a card");
            Console.SetCursorPosition(2, 5);
            Console.Write("with [Enter]");

            Console.SetCursorPosition(2, 7);
            Console.Write("The Goal:");
            Console.SetCursorPosition(2, 8);
            Console.Write("Select two cards that add");
            Console.SetCursorPosition(2, 9);
            Console.Write($"up to {targetSum}");

            WriteAltCombo();

            Console.ReadKey(true);
        }
    }
}