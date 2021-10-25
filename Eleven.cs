using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class Eleven : Board
    {
        public Eleven()
        {
            deck = new Deck();
            inPlayCards = new List<Card>();

            maxInPlayCards = 9;

            selectedCards = new bool[maxInPlayCards];
            ResetSelectedCards();

            highlightedCard = 0;
            targetSum = 11;

            gameName = "Eleven";

            MenuManager();
        }

        private protected override bool HasAltCombo()
        {
            int selectedCardCount = 0;

            bool hasK = false;
            bool hasQ = false;
            bool hasJ = false;

            // Check total amount of selected cards
            for (int i = 0; i < maxInPlayCards; i++)
            {
                if (selectedCards[i])
                    selectedCardCount++;
            }

            if (selectedCardCount != 3)
                return false;

            for (int i = 0; i < inPlayCards.Count; i++)
            {
                if (selectedCards[i])
                {
                    if (inPlayCards[i].Value == 11)
                        hasJ = true;

                    if (inPlayCards[i].Value == 12)
                        hasQ = true;

                    if (inPlayCards[i].Value == 13)
                        hasK = true;
                }
            }

            if (hasK && hasQ && hasJ)
                return true;

            return false;
        }

        private protected override bool HasAltCombo(List<Card> a)
        {
            bool hasK = false;
            bool hasQ = false;
            bool hasJ = false;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Value == 11)
                    hasJ = true;

                if (a[i].Value == 12)
                    hasQ = true;

                if (a[i].Value == 13)
                    hasK = true;
            }

            if (hasK && hasQ && hasJ)
                return true;

            return false;
        }
    }
}   