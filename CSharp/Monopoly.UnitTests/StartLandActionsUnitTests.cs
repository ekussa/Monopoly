using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class StartLandActionsUnitTests
    {
        private const decimal StartLandPayment = 200m;

        private SquareActions _squareActions;
        private TestPlayer _player;
        private Square _startLand;

        [SetUp]
        public void Setup()
        {
            _startLand = new StartLand();
            _player = new TestPlayer(0m);
            _squareActions = new SquareActions(
                new PlayerRepository(
                    new List<Player>
                    {
                        _player,
                    }),
                new Dice());
        }
        
        [Test]
        public void ShouldEarn_WhenStopOnStartLand()
        {
            //Act
            _squareActions.OnPlayerStop(
                _player,
                _startLand);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(StartLandPayment);
        }
        
        [Test]
        public void ShouldEarn_WhenPassOnStartLand()
        {
            //Act
            _squareActions.OnPlayerPass(_player, _startLand);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(StartLandPayment);
        }
    }
}