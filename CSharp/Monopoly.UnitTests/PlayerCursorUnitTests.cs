using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Moq;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    public class PlayerCursorUnitTests
    {
        private List<Player> _players;
        private MockRepository _repositoryMock;
        private Mock<IDice> _dice;
        private int[] _twoDiceSame;
        private int[] _twoDiceDifferent;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new MockRepository(MockBehavior.Strict);
            _dice = _repositoryMock.Create<IDice>();
            _players = new List<Player>
            {
                new Bank(),
                new Computer(Color.Aqua)
            };
            _twoDiceSame = new[] {5, 5};
            _twoDiceDifferent = new[] {2, 3};
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(5, 0)]
        [TestCase(6, 1)]
        public void ShouldCyclePlayers(int iterations, int expectedIndexPlayer)
        {
            //Arrange
            _dice.
                Setup(_ => _.Roll()).
                Returns(_twoDiceDifferent);

            var cursor = new PlayerMovement(_players, _dice.Object);
            
            //Act
            PlayerMove playerMove = null;
            for (var i = 0; i < iterations; i++)
                playerMove = cursor.Next();

            //Assert
            playerMove.Total.Should().Be(_twoDiceDifferent.Sum());
            playerMove.Player.Should().Be(_players[expectedIndexPlayer]);
        }
        
        [TestCase(1, 0, false)]
        [TestCase(2, 0, false)]
        [TestCase(3, 0, true)]
        [TestCase(4, 1, false)]
        [TestCase(5, 1, false)]
        [TestCase(6, 1, true)]
        public void ShouldFreezeTwoPlayers(
            int iterations,
            int expectedIndexPlayer,
            bool shouldFreeze)
        {
            //Arrange
            _dice.
                Setup(_ => _.Roll()).
                Returns(_twoDiceSame);

            var cursor = new PlayerMovement(_players, _dice.Object);
            
            //Act
            PlayerMove playerMove = null;
            for (var i = 0; i < iterations; i++)
                playerMove = cursor.Next();

            //Assert
            playerMove.Total.Should().Be(shouldFreeze ? 0 : _twoDiceSame.Sum());
            playerMove.Unfrozen.Should().Be(shouldFreeze);
            playerMove.Player.Should().Be(_players[expectedIndexPlayer]);
        }

        [TestCase(1, 0, false)]
        [TestCase(2, 0, false)]
        [TestCase(3, 0, true)]
        [TestCase(4, 1, false)]
        [TestCase(5, 1, false)]
        [TestCase(6, 1, true)]
        public void ShouldFreezelayerAndThenUnfreeze(
            int iterations,
            int expectedIndexPlayer,
            bool shouldFreeze)
        {
            //Arrange
            _dice.
                Setup(_ => _.Roll()).
                Returns(_twoDiceSame);

            var cursor = new PlayerMovement(_players, _dice.Object);
            
            //Act
            PlayerMove playerMove = null;
            for (var i = 0; i < iterations; i++)
                playerMove = cursor.Next();

            //Assert
            playerMove.Total.Should().Be(shouldFreeze ? 0 : _twoDiceSame.Sum());
            playerMove.Unfrozen.Should().Be(shouldFreeze);
            playerMove.Player.Should().Be(_players[expectedIndexPlayer]);
        }

        [Test]
        public void ShouldGetAllPlayers()
        {
            //Arrange
            var cursor = new PlayerMovement(_players, _dice.Object);
            
            //Act
            var result = cursor.GetAll();

            //Assert
            result.Count.Should().Be(2);

        }
    }
}