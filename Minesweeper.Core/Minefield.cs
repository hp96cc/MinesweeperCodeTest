using Minesweeper.Core.Interfaces;
using System.Diagnostics;

namespace Minesweeper.Core
{
    public  class Minefield
    {
        private const int minefieldX = 8;
        private const int minefieldY = 8;
        private const int mineCount = 10;
        private int[][] minefieldMines;
        private IMineService mineService;

        private Minefield() { }

        public Minefield(IMineService mineService) 
        {
            this.mineService = mineService;
            AddMinesToMinefield();
        }

        public bool DoesPositionContainMine(int positionX, int positionY)
        {
            int[] position = new int[2] { positionX, positionY };
            var matchedPositon = Array.Find(minefieldMines, x => x != null && x[0] == positionX && x[1] == positionY);
            return matchedPositon != null;
        }

        public bool IsPositionValid(int positionX, int positionY) {

            return  positionX >= 0 && 
                    positionX < minefieldX &&
                    positionY >= 0 &&
                    positionY < minefieldY;
        }

        public bool DoesPositionClearTheMinefield(int positionY)
        {
            return positionY >= minefieldY;
        }

        private void AddMinesToMinefield()
        {
            minefieldMines = mineService.GenerateMines(minefieldX, minefieldY, mineCount);
        }
    }
}
