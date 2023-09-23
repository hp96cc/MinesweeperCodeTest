using Minesweeper.Core.Interfaces;

namespace Minesweeper.Core
{
    public class Game
    {
        private Minefield Minefield;
        private Player Player;
        private IPlatform platform;
        private IMineService mineService;
        string[] chessboardXPositionLabels = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H"  };

        private Game() { }

        public Game(IPlatform platform, IMineService mineService) {
            this.platform = platform;
            this.mineService = mineService;
            CreateGame();
        }

        public void CreateGame()
        {
            Minefield = new Minefield(mineService);
            Player = new Player();   
        }

        public void StartGame()
        {
            Move(1, 1);
        }

        public void Move(int changedPositionX, int changedPositionY) {

            var newPosition = CaluclateNewPosition(changedPositionX, changedPositionY);
            var positionX = newPosition[0];
            var positionY = newPosition[1];

            if (DoesMoveCompleteGame(positionY))
            {
                Player.Move(positionX, positionY);
                ProcessGameWin();
            }

            if (!IsMoveValid(positionX, positionY))
            {
                ProcessInvalidMove();
            } 
            else
            {

                Player.Move(positionX, positionY);

                if (Minefield.DoesPositionContainMine(positionX, positionY))
                {
                    Player.LoseLife();
                    ProcessMineHit(positionX, positionY);
                }
                else
                {
                    ProcessSuccessfulMove(positionX, positionY);
                }

            }


        }

        private void ProcessSuccessfulMove(int positionX, int positionY)
        {
            platform.MoveSuccessful(GetTranslatedChessBoardPosition(positionX, positionY), Player.MoveCount, Player.AvailableLives);
            platform.Move();
        }

        private void ProcessMineHit(int positionX, int positionY)
        {
            if (Player.HasAvailableLives())
            {
                platform.PlayerHitMine(GetTranslatedChessBoardPosition(positionX, positionY), Player.MoveCount, Player.AvailableLives);
                platform.Move();
            }
            else
            {
                platform.GameOver(GetTranslatedChessBoardPosition(positionX, positionY));
            }
        }

        private void ProcessInvalidMove()
        {
            platform.InvalidMove();
            platform.Move();
        }

        private void ProcessGameWin()
        {
            platform.GameWin(Player.MoveCount);
        }

        private string GetTranslatedChessBoardPosition(int positionX, int positionY)
        {
            return chessboardXPositionLabels[positionX] + (positionY + 1).ToString();
        }

        private int[] CaluclateNewPosition(int changedPositionX, int changedPositionY)
        {
            return new int[] { Player.CurrentPositon[0] + changedPositionX, Player.CurrentPositon[1] + changedPositionY };
        }

        private bool IsMoveValid(int positionX, int positionY)
        {
            return Minefield.IsPositionValid(positionX, positionY);
        }

        private bool DoesMoveCompleteGame(int positionY)
        {
            return Minefield.DoesPositionClearTheMinefield(positionY);
        }

       
    }
}
