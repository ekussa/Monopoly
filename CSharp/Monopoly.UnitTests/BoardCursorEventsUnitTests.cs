using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class BoardCursorEventsUnitTests
    {
        private MockRepository _repositoryMock;
        private Mock<IPlayerCursor> _playerCursorMock;
        private Board _fivePlacesWithFreezeBoard;
        private Human _player1;
        private Human _player2;
        private FreezeEntry _freezeEntry;
        private FreeStop _freeStop;
        private int _freeOnPassExecuted;
        private int _freeOnStopExecuted;
        private int _freezeOnStopExecuted;
        private int _freezeOnPassExecuted;

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

            _freezeEntry = new FreezeEntry();
            _freeStop = new FreeStop();
            
            _fivePlacesWithFreezeBoard = new Board
            {
                _freeStop,
                _freeStop,
                _freezeEntry,
                _freeStop,
                _freeStop,
            };

            _freeOnPassExecuted = 0;
            _freeOnStopExecuted = 0;
            _freezeOnStopExecuted = 0;
            _freezeOnPassExecuted = 0;
        }

        [TestCase(1, 1, 0, 0)]
        [TestCase(2, 2, 1, 0)]
        [TestCase(3, 3, 0, 1)]
        [TestCase(4, 4, 0, 1)]
        [TestCase(5, 0, 0, 1)]
        [TestCase(6, 1, 0, 1)]
        [TestCase(7, 2, 1, 1)]
        [TestCase(8, 3, 0, 2)]
        [TestCase(9, 4, 0, 2)]
        [TestCase(10, 0, 0, 2)]
        public void ShouldCountFreezeStops_WhenPassAndStop(
            int dice,
            int expectedPosition,
            int freezeOnStopCountExpected,
            int freezeOnPassCountExpected
        )
        {
            //Arrange
            _playerCursorMock.
                Setup(_ => _.Next()).
                Returns(_player1.Move(dice));
            
            var boardCursor = new BoardCursor(
                _fivePlacesWithFreezeBoard,
                _playerCursorMock.Object);

            _freezeEntry.OnPass += FreezeOnPass;
            _freezeEntry.OnStop += FreezeOnStop;
            
            //Act
            boardCursor.NextTurn();

            //Assert
            boardCursor.Positions[_player1].Should().Be(expectedPosition);
            _freezeOnStopExecuted.Should().Be(freezeOnStopCountExpected);
            _freezeOnPassExecuted.Should().Be(freezeOnPassCountExpected);
        }
        
        [TestCase(1, 1, 1, 0)]
        [TestCase(2, 2, 0, 1)]
        [TestCase(3, 3, 1, 1)]
        [TestCase(4, 4, 1, 2)]
        [TestCase(5, 0, 1, 3)]
        [TestCase(6, 1, 1, 4)]
        [TestCase(7, 2, 0, 5)]
        [TestCase(8, 3, 1, 5)]
        [TestCase(9, 4, 1, 6)]
        [TestCase(10, 0, 1, 7)]
        public void ShouldCountFreeStops_WhenPassAndStop(
            int dice,
            int expectedPosition,
            int freeOnStopCountExpected,
            int freeOnPassCountExpected
        )
        {
            //Arrange
            _playerCursorMock.
                Setup(_ => _.Next()).
                Returns(_player1.Move(dice));
            
            var boardCursor = new BoardCursor(
                _fivePlacesWithFreezeBoard,
                _playerCursorMock.Object);

            _freeStop.OnStop += FreeOnStop;
            _freeStop.OnPass += FreeOnPass;
            
            //Act
            boardCursor.NextTurn();

            //Assert
            boardCursor.Positions[_player1].Should().Be(expectedPosition);
            _freeOnStopExecuted.Should().Be(freeOnStopCountExpected);
            _freeOnPassExecuted.Should().Be(freeOnPassCountExpected);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 0)]
        [TestCase(6, 1)]
        [TestCase(7, 2)]
        [TestCase(8, 3)]
        [TestCase(9, 4)]
        [TestCase(10, 0)]
        public void ShouldMoveTwoPlayers(int dice, int position)
        {
            //Arrange
            _playerCursorMock.
                SetupSequence(_ => _.Next()).
                Returns(_player1.Move(dice)).
                Returns(_player2.Move(dice));
            
            var boardCursor = new BoardCursor(
                _fivePlacesWithFreezeBoard,
                _playerCursorMock.Object);

            _freeStop.OnStop += FreeOnStop;
            _freeStop.OnPass += FreeOnPass;
            _freezeEntry.OnStop += FreeOnStop;
            _freezeEntry.OnPass += FreeOnPass;
            
            //Act
            boardCursor.NextTurn();
            boardCursor.NextTurn();

            //Assert
            boardCursor.Positions[_player1].Should().Be(position);
            boardCursor.Positions[_player2].Should().Be(position);
            _freeOnStopExecuted.Should().Be(2);
            _freeOnPassExecuted.Should().Be((dice-1)*2);
        }
        
        private void FreezeOnStop(object sender, Player e)
        {
            _freezeOnStopExecuted++;
        }

        private void FreezeOnPass(object sender, Player e)
        {
            _freezeOnPassExecuted++;
        }

        private void FreeOnStop(object sender, Player e)
        {
            _freeOnStopExecuted++;
        }
        
        private void FreeOnPass(object sender, Player e)
        {
            _freeOnPassExecuted++;
        }
    }
}