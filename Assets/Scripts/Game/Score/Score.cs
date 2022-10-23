namespace Game
{
    public class Score
    {
        public int PositionScore { get; set; }
        public int BonusScore  { get; set; }
        public int CoinsCount  { get; set; }

        public Score()
        {
            PositionScore = 0;
            BonusScore = 0;
            CoinsCount = 0;
        }

        public int TotalScore()
        {
            return PositionScore + BonusScore;
        }
    }
}