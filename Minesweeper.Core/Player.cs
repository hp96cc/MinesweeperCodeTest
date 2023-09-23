namespace Minesweeper.Core
{
    public  class Player
    {

        public int AvailableLives { get; private set; }

        public int[] CurrentPositon { get; private set; }

        public int MoveCount { get; private set; }

        public Player()
        {
            AvailableLives = 3;
            MoveCount = 0;
            CurrentPositon = new int[2] { -1, -1 };
        }

        public void LoseLife()
        {
            AvailableLives--;
        }

        public bool HasAvailableLives()
        {
            return AvailableLives > 0;
        }

        public void Move(int positionX, int positionY)
        {
            CurrentPositon = new int[2] { positionX, positionY };
            MoveCount++;
        }

        
    }
}
