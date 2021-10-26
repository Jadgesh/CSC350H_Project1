using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class Thirteen : Board
    {
        public Thirteen()
        {
            deck = new Deck();
            inPlayCards = new List<Card>();

            maxInPlayCards = 10;

            selectedCards = new bool[maxInPlayCards];
            ResetSelectedCards();

            highlightedCard = 0;
            targetSum = 13;

            gameName = "Thirteen";

            Play();
        }

        private protected override bool HasAltCombo()
        {
            int selectedCardCount = 0;
            // Check total amount of selected cards
            for(int i = 0; i < maxInPlayCards; i++)
            {
                if (selectedCards[i])
                    selectedCardCount++;
            }

            if (selectedCardCount != 1)
                return false;

            for(int i = 0; i < inPlayCards.Count; i++)
            {
                if (selectedCards[i])
                    if (inPlayCards[i].Value == 13)
                        return true;
            }
            return false;
        }

        private protected override bool HasAltCombo(List<Card> a)
        {
            for(int i = 0; i < a.Count; i++)
            {
                if (a[i].Value == 13)
                    return true;
            }
            return false;
        }

        private protected override void WriteAltCombo()
        {
            Console.SetCursorPosition(2, 11);
            Console.Write("Select a King");
        }
    }
}