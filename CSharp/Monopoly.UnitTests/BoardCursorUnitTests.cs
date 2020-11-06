using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class BoardCursorUnitTests
    {
        private MockRepository _repositoryMock;
        private Mock<IPlayerCursor> _playerCursorMock;
        private Board _fivePlacesBoard;
        private Board _fivePlacesWithFreezeBoard;
        private Human _player1;
        private Human _player2;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new MockRepository(MockBehavior.Strict);
            _playerCursorMock = _repositoryMock.Create<IPlayerCursor>();

            _player1 = new Human(Color.Aquamarine);
            _player2 = new Human(Color.Black);

            _playerCursorMock.
                Setup(_ => _.GetAll()).
                Returns(new List<Player>
                {
                    _player1,
                    _player2
                });

            _fivePlacesBoard = new Board
            {
                new StartLand(),
                new FreeStop(),
                new FreeStop(),
                new FreeStop(),
                new FreeStop()
            };
            
            _fivePlacesWithFreezeBoard = new Board
            {
                new StartLand(),
                new FreeStop(),
                new FreezeEntry(),
                new FreezeVisit(),
                new FreeStop()
            };
        }
        
        [TestCase(1,1)]
        [TestCase(2,2)]
        [TestCase(3,3)]
        [TestCase(4,4)]
        [TestCase(5,0)]
        [TestCase(6,1)]
        [TestCase(7,2)]
        [TestCase(8,3)]
        [TestCase(9,4)]
        [TestCase(10,0)]
        [TestCase(11, 1)]
        [TestCase(12,2)]
        public void ShouldMovePlayerOnBoard(int totalDice, int destinationIndex)
        {
            //Arrange
            _playerCursorMock.
                Setup(_ => _.Next()).
                Returns(new PlayerMove(_player1, totalDice, false));
            
            var boardCursor = new BoardCursor(
                _fivePlacesBoard,
                _playerCursorMock.Object);

            //Act
            boardCursor.NextTurn();

            //Assert
            boardCursor.Positions[_player1].Should().Be(destinationIndex);
        }

        [Test]
        public void ShouldFreeze_WhenThereIsAFreezeStop()
        {
            //Arrange
            _playerCursorMock.
                Setup(_ => _.Next()).
                Returns(new PlayerMove(_player1, 0, true));
            
            var boardCursor = new BoardCursor(
                _fivePlacesWithFreezeBoard,
                _playerCursorMock.Object);

            //Act
            boardCursor.NextTurn();

            //Assert
            boardCursor.Positions[_player1].Should().Be(3);
        }
        
        [Test]
        public void ShouldFreeze_WhenThereIsNoFreezeStop()
        {
            //Arrange
            _playerCursorMock.
                Setup(_ => _.Next()).
                Returns(new PlayerMove(_player1, 0, true));
            
            var boardCursor = new BoardCursor(
                _fivePlacesBoard,
                _playerCursorMock.Object);

            //Act
            boardCursor.NextTurn();

            //Assert
            boardCursor.Positions[_player1].Should().Be(0);
        }
    }
}