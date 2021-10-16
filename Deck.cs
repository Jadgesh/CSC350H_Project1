using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class Deck
    {
        private List<Card> cards = new List<Card>();

        public Deck()
        {
            // Create a deck of 52 cards
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    cards.Add(new Card(rank, suit));
        }

        public bool Empty
        {
            get { return cards.Count == 0; }
        }

        public int Count
        {
            get { return cards.Count; }
        }

        public Card TakeTopCard()
        {
            if (!Empty)
            {
                Card topCard = cards[cards.Count - 1];
                cards.RemoveAt(cards.Count - 1);
                return topCard;
            }

            return null;
        }

        public void Shuffle()
        {
            int swapPos;
            Card temp;
            Random rand = new Random();

            for (int i = cards.Count - 1; i > 0; i--)
            {
                swapPos = rand.Next(i);
                temp = cards[swapPos];
                cards[swapPos] = cards[i];
                cards[i] = temp;
            }
        }
    }
}
