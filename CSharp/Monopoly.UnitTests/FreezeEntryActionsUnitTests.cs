using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class FreezeEntryActionsUnitTests
    {
        private FreezeEntry _freezeEntry;
        private TestPlayer _player;
        private SquareActions _squareActions;
        private IDice _dice;

        [SetUp]
        public void Setup()
        {
            _dice = new Dice();
            _freezeEntry = new FreezeEntry();
            _player = new TestPlayer(0);
            _squareActions = new BoardCursor(
                new Board(),
                new PlayerCursor(
                    new List<Player>
                    {
                        _player,
                    },
                    _dice));
        }
        
        [Test]
        public void ShouldDebit_WhenStop()
        {
            //Act
            _squareActions.OnPlayerStop(
                _player,
                _freezeEntry);
            
            //Assert
        }
        
        [Test]
        public void ShouldNotDebit_WhenPass()
        {
            //Act
            _squareActions.OnPlayerPass(
                _player,
                _freezeEntry);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(0);
        }
    }
}