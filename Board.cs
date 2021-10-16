using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    public abstract class Board
    {
        private protected int activeCardMax;
        private protected int goalSum;
        private protected Deck deck;
        private protected List<Card> inPlayCards;
        private protected bool[] selectedCards;
        private protected int score;

        private protected Board()
        {
            deck = new Deck();
            inPlayCards = new List<Card>();
            score = 0;
        }

        public void Play()
        {
            deck.Shuffle();

            do
            {
                TopUp();

                GetPlayerSelection();

                if (SelectionIsValidCombination())
                {
                    score++;
                    RemoveSelectedCards();
                }

                ResetSelectedCardsArray();

            } while (HasValidPair());
        }

        private protected int SelectedCardsCount
        {
            get
            {
                int tally = 0;
                for (int i = 0; i < selectedCards.Length; i++)
                {
                    if (selectedCards[i])
                        tally++;
                }
                return tally;
            }
        }
        private protected void TopUp()
        {
            while (inPlayCards.Count < activeCardMax && !deck.Empty)
            {
                inPlayCards.Add(deck.TakeTopCard());
            }
        }

        private protected void DisplayCards()
        {
            Console.Write("Current Cards on Table:\n");

            for (int i = 0; i < inPlayCards.Count; i++)
            {
                if (selectedCards[i])
                    Console.Write("*");

                Console.Write("[" + i + "] " + inPlayCards[i].Rank + " of " + inPlayCards[i].Suit + "\n");
            }
        }

        private protected abstract void GetPlayerSelection();

        private protected bool IsValidInput(string userInput)
        {
            try
            {
                int input = Int32.Parse(userInput);

                if (input >= 0 && input < activeCardMax)
                    return true;

                Console.WriteLine("Invalid Entry.");
                return false;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Entry.");
                return false;
            }
        }

        private protected bool IsValidSum()
        {
            int sum = 0;

            for (int i = 0; i < inPlayCards.Count; i++)
            {
                if (selectedCards[i])
                {
                    int cardValue = (int)System.Enum.Parse(typeof(Rank), inPlayCards[i].Rank.ToString()) + 1;

                    if (cardValue >= goalSum)
                        return false;

                    sum += cardValue;
                }
            }

            return (sum == goalSum);
        }

        private protected bool InPlayHasValidSum()
        {
            int sum = 0;

            for (int i = 0; i < inPlayCards.Count; i++)
            {
                sum = (int)System.Enum.Parse(typeof(Rank), inPlayCards[i].Rank.ToString()) + 1;
                for (int j = 0; j < inPlayCards.Count; j++)
                {
                    sum += (int)System.Enum.Parse(typeof(Rank), inPlayCards[j].Rank.ToString()) + 1;

                    if (sum == goalSum)
                        return true;
                }
            }
            return false;
        }

        private protected abstract bool SelectionIsValidCombination();

        private protected void RemoveSelectedCards()
        {
            for (int i = 0; i < selectedCards.Length; i++)
                if (selectedCards[i])
                    inPlayCards.RemoveAt(i);
        }

        private protected void ResetSelectedCardsArray()
        {
            for (int i = 0; i < selectedCards.Length; i++)
                if (selectedCards[i])
                    selectedCards[i] = false;
        }

        private protected abstract bool HasValidPair();

        private protected void AwaitKeyPress()
        {
            Console.WriteLine("Press a key to continue.");
            Console.ReadKey();
        }
    }
}
