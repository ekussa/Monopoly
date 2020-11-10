using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class CompanyActionsUnitTests
    {
        private const decimal Price = 100m;
        private const decimal StartMoney = 500m;
        private const decimal Multiplier = 50;
        
        private SquareActions _squareActions;
        private Company _company;
        private TestPlayer _ownerPlayer;
        private TestPlayer _visitorPlayer;
        private Mock<IDice> _dice;
        private int[] _diceTree;

        [SetUp]
        public void Setup()
        {
            _company = new Company("", Price, Multiplier);
            _visitorPlayer = new TestPlayer(StartMoney);
            _ownerPlayer = new TestPlayer(StartMoney);
            _ownerPlayer.Patrimony.Credit(_company);
            _dice = new Mock<IDice>(MockBehavior.Strict);
            _squareActions = new BoardCursor(
                new Board(),
                new PlayerCursor(
                    new List<Player>
                    {
                        _visitorPlayer,
                        _ownerPlayer,
                    },
                    _dice.Object));

            _diceTree = new[] {1, 2};
        }
        
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void ShouldPayRent_WhenStopOnLand(bool shouldSell, bool shouldBuy)
        {
            //Arrange
            _dice.Setup(_ => _.LastRoll()).Returns(_diceTree);
            _visitorPlayer.ShouldBuy = shouldBuy;
            _ownerPlayer.ShouldSell = shouldSell;
            
            //Act
            _squareActions.OnPlayerStop(
                _visitorPlayer,
                _company);
            
            //Assert
            var total = _diceTree.Sum() * Multiplier;
            _visitorPlayer.Patrimony.Cash.Should().Be(StartMoney - total);
            _visitorPlayer.Patrimony.Count.Should().Be(0);
            _ownerPlayer.Patrimony.Cash.Should().Be(StartMoney + total);
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
                _company);
            
            //Assert
            _visitorPlayer.Patrimony.Cash.Should().Be(StartMoney);
            _visitorPlayer.Patrimony.Count.Should().Be(0);
            _ownerPlayer.Patrimony.Cash.Should().Be(StartMoney);
            _ownerPlayer.Patrimony.Count.Should().Be(1);
        }
    }
}