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
            var dice = new[] {5, 2};
            _dice.
                Setup(_ => _.RollTwice()).
                Returns(dice);

            var cursor = new PlayerCursor(_players, _dice.Object);
            
            //Act
            PlayerTurn playerTurn = null;
            for (var i = 0; i < iterations; i++)
                playerTurn = cursor.RollDice();

            //Assert
            playerTurn.Total.Should().Be(dice.Sum());
            playerTurn.Player.Should().Be(_players[expectedIndexPlayer]);
        }
        
        [TestCase(1, 0, false)]
        [TestCase(2, 0, false)]
        [TestCase(3, 0, true)]
        [TestCase(4, 1, false)]
        [TestCase(5, 1, false)]
        [TestCase(6, 1, true)]
        [TestCase(7, 0, false)]
        [TestCase(8, 0, false)]
        [TestCase(9, 0, true)]
        public void ShouldNotCyclePlayers_WhenSameNumberOnDice(
            int iterations,
            int expectedIndexPlayer,
            bool shouldGoToJail)
        {
            //Arrange
            var dice = new[] {5, 5};
            _dice.
                Setup(_ => _.RollTwice()).
                Returns(dice);

            var cursor = new PlayerCursor(_players, _dice.Object);
            
            //Act
            PlayerTurn playerTurn = null;
            for (var i = 0; i < iterations; i++)
                playerTurn = cursor.RollDice();

            //Assert
            playerTurn.Total.Should().Be(dice.Sum());
            playerTurn.ShouldGoToJail.Should().Be(shouldGoToJail);
            playerTurn.Player.Should().Be(_players[expectedIndexPlayer]);
        }
    }
}