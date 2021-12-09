namespace Game.Player
{
    public class PlayerModel
    {
        private PlayerState state = new PlayerState();
        public float Velocity { get; set; } = 3f;



        public bool CheckRequirement(Direction direction,Color color)
        {
            return state.CheckMatch(direction, color);
        }

        public void Move(Move move)
        {
            switch (move)
            {
                case Game.Move.LEFT:
                    state.RotateSide();
                    break;
                case Game.Move.RIGHT:
                    state.RotateSide();
                    break;
                case Game.Move.BACK:
                    state.RotateBack();
                    break;
                case Game.Move.UP:
                    break;
                
            }
          
        }
    }
}