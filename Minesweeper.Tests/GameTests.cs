using Minesweeper.Core;
using Minesweeper.Core.Interfaces;
using Moq;

namespace Minesweeper.Tests
{
    public class GameTests
    {
        private Game game;
        private Mock<IPlatform> platform;
        private Mock<IMineService> mineService;
        private int[][] expectedMines;

        [SetUp]
        public void Setup()
        {
            CreateExpectedMines();
            platform = new Mock<IPlatform>();
            mineService = new Mock<IMineService>();
            mineService.Setup(x => x.GenerateMines(8, 8, 10)).Returns(expectedMines);
            game = new Game(platform.Object, mineService.Object);

        }

        private void CreateExpectedMines()
        {
            expectedMines = new int[10][];
            expectedMines[0] = new int[2] { 1, 0 };
            expectedMines[1] = new int[2] { 1, 1 };
            expectedMines[2] = new int[2] { 1, 2 };
            expectedMines[3] = new int[2] { 1, 3 };
            expectedMines[4] = new int[2] { 1, 4 };
            expectedMines[5] = new int[2] { 1, 5 };
            expectedMines[6] = new int[2] { 1, 6 };
            expectedMines[7] = new int[2] { 1, 7 };
            expectedMines[8] = new int[2] { 1, 8 };
            expectedMines[9] = new int[2] { 1, 9 };
        }

        [Test]
        public void OnGameStart_SuccessfulMove()
        {
            game.StartGame();
            platform.Verify(v => v.MoveSuccessful("A1", 1, 3));
            platform.Verify(v => v.NextMove());
        }

        [Test]
        public void OnMoveToX1Y1_SuccessfulMoveToB1()
        {
            game.StartGame();
            game.Move(0, 1);
            platform.Verify(v => v.MoveSuccessful("A2", 2, 3));
            platform.Verify(v => v.NextMove());
        }


        [Test]
        public void OnMoveToX1Y2_HitMineOnB2AndLoseLife()
        {
            game.StartGame();
            game.Move(0, 1);
            game.Move(1, 0);
            platform.Verify(v => v.PlayerHitMine("B2", 3, 2));
            platform.Verify(v => v.NextMove());
        }

        [Test]
        public void After3HitMines_GameOver()
        {
            game.StartGame();
            game.Move(1, 0);
            game.Move(0, 1);
            game.Move(0, 1);

            platform.Verify(v => v.GameOver("B3"));
        }


        [Test]
        public void After9Moves_PlayerWinsGame()
        {
            game.StartGame();
            platform.Verify(v => v.NextMove());

            for (int i = 0; i < 8; i++)
            {
                game.Move(0, 1);
            }

            platform.Verify(v => v.GameWin(9));
        }

        [Test]
        public void PlayerMovesOffBoard_InvalidMoveMessageShown()
        {
            game.StartGame();
            game.Move(-1, 0);

            platform.Verify(v => v.InvalidMove());
        }

    }
}