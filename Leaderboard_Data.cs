namespace CSC350H_Project1_Jadgesh_Inderjeet
{
    public struct Leaderboard_Data
    {
        public string GameType;
        public string Name;
        public int Score;

        public Leaderboard_Data(string t, string n, int s)
        {
            GameType = t;
            Name = n;
            Score = s;
        }
    }
}
