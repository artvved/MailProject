namespace Game
{
    public class Score
    {
        public int PositionScore = 0;
        public int BonusScore = 0;

        public Score()
        {
            PositionScore = 0;
            BonusScore = 0;
        }

        public int TotalScore()
        {
            return PositionScore + BonusScore;
        }
    }
}