using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class DealFeatureUnitTests
    {
        private const decimal BuyerCash = 100m;
        private const decimal BrokenBuyerCash = 1m;
        private const decimal SellerCash = 0m;
        
        private Land _hundredProperty;
        private Land _twoHundredProperty;
        private Patrimony _seller;
        private Patrimony _buyer;

        [SetUp]
        public void Setup()
        {
            _hundredProperty = new Land("100", 100, Color.Aqua);
            _twoHundredProperty = new Land("200", 200, Color.Aqua);
            _seller = new Patrimony(SellerCash);
            _buyer = new Patrimony(BuyerCash);
        }
        
        [Test]
        public void ShouldExchangeCashPerProperty()
        {
            //Arrange
            _seller.Credit(_hundredProperty);
            
            //Act
            var result = _seller.Exchange(_hundredProperty, _buyer);
            
            //Assert
            result.Should().BeTrue();
            _seller.Cash.Should().Be(BuyerCash);
            _seller.Count.Should().Be(0);
            _buyer.Cash.Should().Be(SellerCash);
            _buyer.Count.Should().Be(1);
        }
        
        [Test]
        public void ShouldNotExchangeCashPerProperty_WhenThereIsNoFunds()
        {
            //Arrange
            _seller.Credit(_hundredProperty);
            _buyer.Cash = BrokenBuyerCash;
            
            //Act
            var result = _seller.Exchange(_hundredProperty, _buyer);
            
            //Assert
            result.Should().BeFalse();
            _seller.Cash.Should().Be(SellerCash);
            _seller.Count.Should().Be(1);
            _buyer.Cash.Should().Be(BrokenBuyerCash);
            _buyer.Count.Should().Be(0);
        }
        
        [Test]
        public void ShouldNotExchangeCashPerProperty_WhenPropertyDoNotBelongs()
        {
            //Arrange
            _seller.Credit(_hundredProperty);
            
            //Act
            var result = _seller.Exchange(_twoHundredProperty, _buyer);
            
            //Assert
            result.Should().BeFalse();
            _seller.Cash.Should().Be(SellerCash);
            _seller.Count.Should().Be(1);
            _buyer.Cash.Should().Be(BuyerCash);
            _buyer.Count.Should().Be(0);
        }
    }
}