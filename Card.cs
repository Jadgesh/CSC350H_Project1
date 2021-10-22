namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    class Card
    {
        private Rank rank;
        private Suit suit;

        public Card(Rank rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        public Rank Rank
        {
            get { return rank; }
        }

        public Suit Suit
        {
            get { return suit; }
        }

        public int Value
        {
            get { return (int)System.Enum.Parse(typeof(Rank), Rank.ToString()) + 1; }
        }
    }
}
