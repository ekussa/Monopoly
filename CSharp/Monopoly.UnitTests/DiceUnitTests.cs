using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class DiceUnitTests
    {
        [Test]
        public void ShouldRollDices()
        {
            //Arrange
            const int rolls = 1000;
            var dice = new Dice();
            var result = new List<int>(rolls);
            var lastResult = new List<int>(rolls);
            
            //Act
            for (var i = 0; i < rolls; i++)
            {
                result.Add(dice.Roll().Sum());
                lastResult.Add(dice.LastRoll().Sum());
            }
            
            //Assert
            result.Min().Should().Be(2);
            result.Max().Should().Be(12);
            Assert.That(result, Is.EquivalentTo(lastResult));
        }
    }
}