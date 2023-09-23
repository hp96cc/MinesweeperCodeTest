using Minesweeper.Core;
using Minesweeper.Core.Interfaces;
using Moq;

namespace Minesweeper.Tests
{
    public class MinefieldTests
    {
        private Minefield minefield;
        private Mock<IMineService> mineService;

        private int[][] expectedMines;

        [SetUp]
        public void Setup()
        {
            CreateExpectedMines();
            mineService = new Mock<IMineService>();
            mineService.Setup(x => x.GenerateMines(8, 8, 10)).Returns(expectedMines);
            minefield = new Minefield(mineService.Object);
        }

        private void CreateExpectedMines()
        {
            expectedMines = new int[10][];
            expectedMines[0] = new int[2] { 0, 0 };
            expectedMines[1] = new int[2] { 0, 1 };
            expectedMines[2] = new int[2] { 0, 2 };
            expectedMines[3] = new int[2] { 0, 3 };
            expectedMines[4] = new int[2] { 0, 4 };
            expectedMines[5] = new int[2] { 0, 5 };
            expectedMines[6] = new int[2] { 0, 6 };
            expectedMines[7] = new int[2] { 0, 7 };
            expectedMines[8] = new int[2] { 0, 8 };
            expectedMines[9] = new int[2] { 0, 9 };
        }

        [Test]
        public void Positionx0y0IsAMine()
        {
            var doesPositionContainMine = minefield.DoesPositionContainMine(0, 0);
            Assert.IsTrue(doesPositionContainMine);
        }

        [Test]
        public void Positionx2y2IsNotAMine()
        {
            var doesPositionContainMine = minefield.DoesPositionContainMine(2, 2);
            Assert.IsTrue(!doesPositionContainMine);
        }

        [Test]
        public void Positionx2y2IsDoesNotClearMinefield()
        {
            var doesPositionClearTheMinefield = minefield.DoesPositionClearTheMinefield(2);
            Assert.IsTrue(!doesPositionClearTheMinefield);
        }

        [Test]
        public void Positionx2y10IsDoesNotClearMinefield()
        {
            var doesPositionClearTheMinefield = minefield.DoesPositionClearTheMinefield(10);
            Assert.IsTrue(doesPositionClearTheMinefield);
        }

    }
}