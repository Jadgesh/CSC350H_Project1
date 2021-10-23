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

        public void Play()
        {
            // Shuffle our deck
            deck.Shuffle();

            // Deal Cards
            DealCards();

            DisplayBoard();

            while (TableHasValidCombo())
            {
                GetPlayerInput();

                // If the selected cards they chose has a valid input do this
                if (SelectedCardsHasValidCombo())
                {
                    // Add points
                    // Remove Cards
                    RemoveSelectedCards();
                    // Reset selected cards
                    ResetSelectedCards();

                    // Move our "Cursor" to a valid card
                    RearrangeHighlightedCard();
                    // Draw New Cards
                    DealCards();
                }

                DisplayBoard();
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

        private protected void DealCards()
        {
            while (inPlayCards.Count < maxInPlayCards && !deck.Empty)
            {
                Card temp = deck.TakeTopCard();

                if(temp != null)
                    inPlayCards.Add(temp);
            }
        }

        private protected void RearrangeHighlightedCard()
        {
            if (inPlayCards.Count - 1 < highlightedCard)
                highlightedCard = inPlayCards.Count - 1;
        }
        private protected void DisplayBoard()
        {
            Console.Clear();

            DisplayBorder();

            DisplayCards();

        }

        private protected void DisplayLoseScreen()
        {
            Console.Clear();
            DisplayBoard();
            Console.SetCursorPosition(2, 2);
            Console.Write("YOU LOSE");
        }

        private protected void DisplayWinScreen()
        {
            Console.Clear();
            DisplayBoard();
            Console.SetCursorPosition(2, 2);
            Console.Write("YOU WIN!");
        }

        private protected void DisplayBorder()
        {
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

        private protected void DisplayCards()
        {
            int x, y;
            (x, y) = Console.GetCursorPosition();

            Console.BackgroundColor = ConsoleColor.White;

            int t = 2;
            int l = 2;

            // Starting position should be 2,2
            for(int i = 0; i < inPlayCards.Count; i++)
            {
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

        private protected string CardSAndR(int index, bool reverse)
        {
            string SuitSymbol = SuitToSymbol(inPlayCards[index].Suit);
            string ValueSymbol = ValueToSymbol(inPlayCards[index].Value);
            string filler = "  ";

            if (SuitSymbol.Length + ValueSymbol.Length + filler.Length < 5)
                filler += " ";

            if (reverse)
                return SuitSymbol + filler + ValueSymbol;

            return ValueSymbol + filler + SuitSymbol;
        }

        private protected string SuitToSymbol(Suit a)
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

        private protected string ValueToSymbol(int a)
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

        private protected void ChangeCardForeground(Suit a)
        {
            if (a == Suit.Clubs || a == Suit.Spades)
                Console.ForegroundColor = ConsoleColor.Black;
            else
                Console.ForegroundColor = ConsoleColor.Red;
        }

        private protected void GetPlayerInput() {
            ConsoleKeyInfo input = Console.ReadKey();

            //if(input.Key == ConsoleKey.F)

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
        }

        private protected void ResetSelectedCards()
        {
            for (int i = 0; i < maxInPlayCards; i++)
                selectedCards[i] = false;
        }

        private protected void RemoveSelectedCards()
        {
            for(int i = inPlayCards.Count; i > 0; i--)
            {
                if (selectedCards[i - 1])
                {
                    inPlayCards.RemoveAt(i - 1);
                }
            }
        }

        private protected bool TableHasValidCombo()
        {
            if (HasSumCombo(inPlayCards) || HasAltCombo(inPlayCards))
                return true;
            return false;
        }

        // Check if our Selected Cards has a valid Combo
        private protected bool SelectedCardsHasValidCombo()
        {
            if (HasSumCombo() || HasAltCombo())
                return true;
            return false;
        }

        // Checks if the cards we've selected add up to our target sum
        private protected bool HasSumCombo()
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

            if (sum == targetSum)
                return true;

            return false;
        }


        // Checks if cards on the table has atleast one pair that adds up to
        // the sum 
        private protected bool HasSumCombo(List<Card> a)
        {
            int sum = 0;
            for(int i = 0; i < inPlayCards.Count - 1; i++)
            {
                for(int j = i; j < inPlayCards.Count; j++)
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

    }
}