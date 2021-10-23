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
            deck = new Deck();
            inPlayCards = new List<Card>();

            maxInPlayCards = 13;

            selectedCards = new bool[maxInPlayCards];
            ResetSelectedCards();

            highlightedCard = 0;
            targetSum = 10;
        }

        private protected override bool HasAltCombo()
        {
            int selectedCardCount = 0;
            // Check total amount of selected cards
            for (int i = 0; i < maxInPlayCards; i++)
            {
                if (selectedCards[i])
                    selectedCardCount++;
            }

            if (selectedCardCount != 4)
                return false;

            return false;
        }

        private protected override bool HasAltCombo(List<Card> a)
        {
            int[] dupeCount = new int[4];

            for (int i = 0; i < 4; i++)
                dupeCount[i] = 0;

            foreach(Card card in a)
            {
                switch (card.Value)
                {
                    case 10:
                        dupeCount[0]++;
                        break;
                    case 11:
                        dupeCount[1]++;
                        break;
                    case 12:
                        dupeCount[2]++;
                        break;
                    case 13:
                        dupeCount[3]++;
                        break;
                }
            }

            for (int i = 0; i < 4; i++)
                if (dupeCount[i] == 4)
                    return true;
            return false;
        }
    }
}
