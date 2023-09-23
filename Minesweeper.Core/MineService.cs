using Minesweeper.Core.Interfaces;
using System.Diagnostics;

namespace Minesweeper.Core
{
    public class MineService : IMineService
    {
        public int[][] GenerateMines(int minefieldSizeX, int mineFieldSizeY, int mineCount)
        {
            Debug.WriteLine("Creating minefield...");

            Random random = new Random();
            var mines = new int[mineCount][];

            for (int i = 0; i < mineCount; i++)
            {
                int randomX = random.Next(0, minefieldSizeX);
                int randomY = random.Next(0, mineFieldSizeY);

                var mineExists = DoesMineExist(mines, randomX, randomY);

                if (mineExists)
                {
                    i--;
                    Debug.WriteLine("Mine exists as position {0} {1}, trying again", randomX, randomY);
                    continue;
                }
                else
                {
                    mines[i] = new int[2] { randomX, randomY };
                    Debug.WriteLine("Mine at position {0} {1}", randomX, randomY);
                }
            }

            return mines;
        }

        private bool DoesMineExist(int[][] mines, int positionX, int positionY)
        {
            int[] position = new int[2] { positionX, positionY };
            var matchedPositon = Array.Find(mines, x => x != null && x[0] == positionX && x[1] == positionY);
            return matchedPositon != null;
        }


    }
}
