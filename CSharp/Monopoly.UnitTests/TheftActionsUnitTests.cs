using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class TheftActionsUnitTests
    {
        private const decimal StartMoney = 200m;
        
        private Theft _theft;
        private TestPlayer _player;
        private SquareActions _squareActions;
        private IDice _dice;

        [SetUp]
        public void Setup()
        {
            _dice = new Dice();
            _theft = new Theft();
            _player = new TestPlayer(StartMoney);
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
                _theft);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(0);
        }
        
        [Test]
        public void ShouldNotDebit_WhenPass()
        {
            //Act
            _squareActions.OnPlayerPass(
                _player,
                _theft);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(StartMoney);
        }
    }
}