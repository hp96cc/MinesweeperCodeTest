using Minesweeper.Core;
using Minesweeper.Core.Interfaces;

namespace Minesweeper.ConsoleApp
{
    internal class ConsolePlatform : IPlatform
    {

        Game game;

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Game started.");

            if (game != null) game = null;
            game = new Game(this, new MineService());
            game.StartGame();
        }

        public void Move()
        {
            Console.WriteLine("Make your next move");

            var keyPressed = Console.ReadKey();
            var vectorMove = ConvertArrowKeyToMoveVector(keyPressed.Key);

            if (vectorMove != null)
            {
                game.Move(vectorMove[0], vectorMove[1]);
            }
            else
            {
                DisplayInvalidKeyText();
            }

        }

        public void GameWin(int score)
        {
            Console.WriteLine(string.Format("Congratulations, you have won. Your score is: {0}", score));
            Console.WriteLine("Press any key to play again, or escape to quit.");
            
            var restartResult = Console.ReadKey();
            if (restartResult.Key != ConsoleKey.Escape)
            {
                StartGame();
            }
            else
            {
                Environment.Exit(0);
            }

        }

        public void GameOver(string chessPosition)
        {
            Console.WriteLine("Mine has been hit at position {0} and you have no availabvle lives. Game Over.", chessPosition);
        }

        public void InvalidMove()
        {
            Console.WriteLine("Invalid move. You cant move to that position. It is outwith the bounds of the minefield.");
        }

        public void MoveSuccessful(string chessPosition, int moveCount, int availableLives)
        {
            Console.WriteLine("Position {0} is safe. {1} total move(s). {2} lives remaining", chessPosition, moveCount, availableLives);
        }

        public void PlayerHitMine(string chessPosition, int moveCount, int availableLives)
        {
            Console.WriteLine("Mine has been hit at {0}. You lose one life. {1} total move(s). {2} lives remaining", chessPosition, moveCount, availableLives);
        }

        private void DisplayInvalidKeyText()
        {
            Console.WriteLine("Invalid key. That key is not usuable, please only use the arrow keys.");
        }

        private int[] ConvertArrowKeyToMoveVector(ConsoleKey consoleKey)
        {

            switch (consoleKey)
            {
                case (ConsoleKey.UpArrow):
                    return new int[2] { 0, 1 };

                case (ConsoleKey.DownArrow):
                    return new int[2] { 0, -1 };

                case (ConsoleKey.LeftArrow):
                    return new int[2] { -1, 0 };

                case (ConsoleKey.RightArrow):
                    return new int[2] { 1, 0 };

                default:
                    return null;
            }

        }
    }
}
