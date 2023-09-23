using Minesweeper.Core;

namespace Minesweeper.Tests
{
    public class PlayerTests
    {
        private Player player;

        [SetUp]
        public void Setup()
        {
            player = new Player();
        }

        [Test]
        public void OnInit_PlayerHasAvailableLives()
        {
            var hasAvailableLives = player.HasAvailableLives();
            Assert.IsTrue(hasAvailableLives);
        }

        [Test]
        public void OnInit_PlayerHasAMoveCountOfZero()
        {
            Assert.AreEqual(player.MoveCount, 0);
        }

        [Test]
        public void OnMove_PlayersMoveCountIncreasesByOne()
        {
            int expectedMoveCount = 1;
            player.Move(0, 0);
            Assert.AreEqual(player.MoveCount, expectedMoveCount);
        }

        [Test]
        public void OnMove_PlayersPositionEqualsThatOfMovePostion()
        {
            var position = new int[2] { 3, 3 };
            player.Move(position[0], position[1]);
            Assert.AreEqual(player.CurrentPositon, position);
        }


        [Test]
        public void OnLosing3Lives_PlayerHasNoAvailableLives()
        {
            for(int i=0; i < 3; i++)
            {
                player.LoseLife();
            }

            Assert.IsFalse(player.HasAvailableLives());

        }
    }
}