namespace Minesweeper.Core.Interfaces
{
    public interface IPlatform
    {
        void NextMove();

        void MoveSuccessful(string chessPosition, int moveCount, int availableLives);

        void PlayerHitMine(string chessPosition, int moveCount, int availableLives);

        void GameWin(int score);

        void GameOver(string chessPosition);

        void InvalidMove();

    }
}
