namespace Game
{
    public class ScoreController
    {
        public ScoreController()
        {
            Score = new Score();
        }

        public Score Score { get; set; }

        public int PointsForMatch(float z)
        {
            return (int)(25 * z / 100 )+ 25;
        }
    }
}