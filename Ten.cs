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

            gameName = "Ten";

            MenuManager();
        }

        private protected override bool HasAltCombo()
        {
            for(int i = 0; i < inPlayCards.Count - 3; i++)
            {
                if(selectedCards[i] && inPlayCards[i].Value >= 10)
                {
                    int dupeCount = 1;
                    for(int j = i + 1; j < inPlayCards.Count; j++)
                    {
                        if (inPlayCards[i] == inPlayCards[j])
                            dupeCount++;
                    }

                    if (dupeCount == 4)
                        return true;
                }
            }
            return false;
        }

        private protected override bool HasAltCombo(List<Card> a)
        {
            for(int i = 0; i < inPlayCards.Count - 3; i++)
            {
                if(inPlayCards[i].Value >= 10)
                {
                    int dupeCount = 1;
                    for (int j = i + 1; j < inPlayCards.Count; j++)
                    {
                        if (inPlayCards[i] == inPlayCards[j])
                            dupeCount++;
                    }

                    if (dupeCount == 4)
                        return true;
                }
            }
            return false;
        }
    }
}
