namespace Game
{
    public class ScoreController
    {
        public int PointsForMatch(float z)
        {
            return (int)(25 * z / 100 )+ 25;
        }
    }
}