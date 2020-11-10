using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class LandActionsUnitTests
    {
        private const decimal Price = 100m;
        private const decimal StartMoney = 100m;
        private const decimal Rent = 1m;
        
        private Land _land;
        private TestPlayer _ownerPlayer;
        private TestPlayer _visitorPlayer;
        private SquareActions _squareActions;

        [SetUp]
        public void Setup()
        {
            _land = new Land("", Price, Color.Aqua)
            {
                RentPrice = new RentPrice(Rent,Rent,Rent,Rent,Rent,Rent)
            };
            _visitorPlayer = new TestPlayer(StartMoney);
            _ownerPlayer = new TestPlayer(StartMoney);
            _ownerPlayer.Patrimony.Credit(_land);
            _squareActions = new BoardCursor(
                new Board(),
                new PlayerCursor(
                    new List<Player>
                    {
                        _visitorPlayer,
                        _ownerPlayer,
                    },
                    new Dice()));
        }
        
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void ShouldPayRent_WhenStopOnLand(bool shouldSell, bool shouldBuy)
        {
            //Arrange
            _visitorPlayer.ShouldBuy = shouldBuy;
            _ownerPlayer.ShouldSell = shouldSell;
            
            //Act
            _squareActions.OnPlayerStop(
                _visitorPlayer,
                _land);
            
            //Assert
            _visitorPlayer.Patrimony.Cash.Should().Be(StartMoney - Rent);
            _visitorPlayer.Patrimony.Count.Should().Be(0);
            _ownerPlayer.Patrimony.Cash.Should().Be(StartMoney + Rent);
            _ownerPlayer.Patrimony.Count.Should().Be(1);
        }

        [Test]
        public void ShouldSell_WhenStopOnLand()
        {
            //Arrange
            _visitorPlayer.ShouldBuy = true;
            _ownerPlayer.ShouldSell = true;

            //Act
            _squareActions.OnPlayerStop(
                _visitorPlayer,
                _land);
            
            //Assert
            _visitorPlayer.Patrimony.Cash.Should().Be(StartMoney);
            _visitorPlayer.Patrimony.Count.Should().Be(0);
            _ownerPlayer.Patrimony.Cash.Should().Be(StartMoney);
            _ownerPlayer.Patrimony.Count.Should().Be(1);
        }
    }
}