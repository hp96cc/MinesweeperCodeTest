using Minesweeper.Core.Interfaces;
using Minesweeper.Core.Services;
using System.Diagnostics;

namespace Minesweeper.Core
{
    public class Game
    {
        private Minefield Minefield;
        private Player Player;
        private IPlatform platform;
        private IMineService mineService;
      
        private Game() { }

        public Game(IPlatform platform, IMineService mineService) {
            this.platform = platform;
            this.mineService = mineService;
            CreateGame();
        }

        private void CreateGame()
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
                Debug.WriteLine(string.Format("Move to {0}", ChessboardPostionTranslationHelper.GeTranslatedChessBoardPosition(positionX, positionY)));


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
            platform.MoveSuccessful(ChessboardPostionTranslationHelper.GeTranslatedChessBoardPosition(positionX, positionY), Player.MoveCount, Player.AvailableLives);
            platform.NextMove();
        }

        private void ProcessMineHit(int positionX, int positionY)
        {
            if (Player.HasAvailableLives())
            {
                platform.PlayerHitMine(ChessboardPostionTranslationHelper.GeTranslatedChessBoardPosition(positionX, positionY), Player.MoveCount, Player.AvailableLives);
                platform.NextMove();
            }
            else
            {
                platform.GameOver(ChessboardPostionTranslationHelper.GeTranslatedChessBoardPosition(positionX, positionY));
            }
        }

        private void ProcessInvalidMove()
        {
            platform.InvalidMove();
            platform.NextMove();
        }

        private void ProcessGameWin()
        {
            platform.GameWin(Player.MoveCount);
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
