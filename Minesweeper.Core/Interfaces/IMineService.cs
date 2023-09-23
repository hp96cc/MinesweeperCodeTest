namespace Minesweeper.Core.Interfaces
{
    public interface IMineService
    {
        int[][] GenerateMines(int minefieldSizeX, int mineFieldSizeY, int mineCount);    
            
    }
}
