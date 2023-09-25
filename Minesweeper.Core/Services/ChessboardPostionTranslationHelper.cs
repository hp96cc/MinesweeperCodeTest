namespace Minesweeper.Core.Services
{
    public static class ChessboardPostionTranslationHelper
    {
        private static string[] chessboardXPositionLabels = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };

        public static string GeTranslatedChessBoardPosition(int positionX, int positionY)
        {
            return chessboardXPositionLabels[positionX] + (positionY + 1).ToString();
        }

    }
}
