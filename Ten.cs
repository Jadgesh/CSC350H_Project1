using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class Ten : Board
    {
        public Ten()
        {
            activeCardMax = 13;
            goalSum = 10;
            selectedCards = new bool[activeCardMax];

            ResetSelectedCardsArray(); // Sets all value of selectedCards to false;
        }

        private protected override void GetPlayerSelection()
        {
            do
            {

                DisplayCards();

                Console.WriteLine("Choose 2 cards that add up to 10 or 4 of a kind ( 10 + ): ");

                string input = Console.ReadLine();

                if (IsValidInput(input))
                    selectedCards[Int32.Parse(input)] = !selectedCards[Int32.Parse(input)];

                Console.Clear();

                // Handle case where user has selected two cards
                if (SelectedCardsCount == 2)
                {
                    // Make sure the cards arent greater than 

                }

                if (SelectedCardsCount == 4 && SelectionHasValidQuatert())
                    break;

            } while (true);
        }

        private protected override bool SelectionIsValidCombination()
        {
            if (IsValidSum() || SelectionHasValidQuatert())
                return true;

            return false;
        }

        private bool SelectionHasValidQuatert()
        {
            Card firstCard = null;
            int selectedCardCount = 0;
            for (int i = 0; i < inPlayCards.Count; i++)
            {
                if (selectedCards[i])
                {
                    selectedCardCount++;
                    if (firstCard == null)
                        firstCard = inPlayCards[i];
                    else
                    {
                        if (inPlayCards[i].Rank != firstCard.Rank)
                            return false;
                    }
                }

            }

            if (selectedCardCount != 4)
                return false;

            return true;
        }

        private protected override bool HasValidPair()
        {
            if (InPlayHasValidSum() || InPlayHasValidQuatert())
            {
                Console.Write("Valid Match");
                AwaitKeyPress();
                return true;
            }

            Console.Write("Invalid Match");
            AwaitKeyPress();

            return false;
        }

        private bool InPlayHasValidQuatert()
        {
            int rankCount;
            for (int i = 0; i < inPlayCards.Count - 3; i++)
            {
                rankCount = 0;
                if ((int)System.Enum.Parse(typeof(Rank), inPlayCards[i].Rank.ToString()) + 1 >= 10)
                {
                    for (int j = i; j < inPlayCards.Count; j++)
                    {
                        if (inPlayCards[i].Rank == inPlayCards[j].Rank)
                            rankCount++;
                    }

                    if (rankCount == 4)
                        return true;
                }
            }
            return false;
        }

    }
}
