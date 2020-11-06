using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class PlayerMobilityUnitTests
    {
        private const int SameDiceMax = 3;
        private const int UnfreezeAttempts = 4;

        private int[] _sameDices;
        private int[] _differentDices;
        private Mobility _mobility;

        [SetUp]
        public void Setup()
        {
            _sameDices = new[] {5, 5};
            _differentDices = new[] {5, 1};
            _mobility = new Mobility(SameDiceMax, UnfreezeAttempts);
        }
        
        [Test]
        public void ShouldMoveUntilFreeze()
        {
            //Act
            var result1 = _mobility.CanMove(_sameDices);
            var result2 = _mobility.CanMove(_sameDices);
            var result3 = _mobility.CanMove(_sameDices);

            //Assert
            result1.Should().Be(MovementResult.CanMoveSamePlayer);
            result2.Should().Be(MovementResult.CanMoveSamePlayer);
            result3.Should().Be(MovementResult.JustFrozen);
        }
        
        [Test]
        public void ShouldFreezeAndThenUnfreeze_WhenSameDiceCameFourTimes()
        {
            //Act
            var result1 = _mobility.CanMove(_sameDices);
            var result2 = _mobility.CanMove(_sameDices);
            var result3 = _mobility.CanMove(_sameDices);
            var result4 = _mobility.CanMove(_sameDices);

            //Assert
            result1.Should().Be(MovementResult.CanMoveSamePlayer);
            result2.Should().Be(MovementResult.CanMoveSamePlayer);
            result3.Should().Be(MovementResult.JustFrozen);
            result4.Should().Be(MovementResult.CanMove);
        }
        
        [Test]
        public void ShouldNotFreeze_WhenTwiceDifferentDiceAfterSameDices()
        {
            //Act
            var result1 = _mobility.CanMove(_sameDices);
            var result2 = _mobility.CanMove(_differentDices);
            var result3 = _mobility.CanMove(_differentDices);

            //Assert
            result1.Should().Be(MovementResult.CanMoveSamePlayer);
            result2.Should().Be(MovementResult.CanMove);
            result3.Should().Be(MovementResult.CanMove);
        }
        
        [Test]
        public void ShouldNotFreeze_WhenDifferentDiceAfterTwiceSameDices()
        {
            //Act
            var result1 = _mobility.CanMove(_sameDices);
            var result2 = _mobility.CanMove(_sameDices);
            var result3 = _mobility.CanMove(_differentDices);

            //Assert
            result1.Should().Be(MovementResult.CanMoveSamePlayer);
            result2.Should().Be(MovementResult.CanMoveSamePlayer);
            result3.Should().Be(MovementResult.CanMove);
        }
        
        [Test]
        public void ShouldNotFreeze_WhenTreeDifferentDices()
        {
            //Act
            var result1 = _mobility.CanMove(_differentDices);
            var result2 = _mobility.CanMove(_differentDices);
            var result3 = _mobility.CanMove(_differentDices);

            //Assert
            result1.Should().Be(MovementResult.CanMove);
            result2.Should().Be(MovementResult.CanMove);
            result3.Should().Be(MovementResult.CanMove);
        }
        
        [Test]
        public void ShouldNotUnfreeze_WhenFourDifferentAttempts()
        {
            //Arrange
            _mobility.CanMove(_sameDices);
            _mobility.CanMove(_sameDices);
            _mobility.CanMove(_sameDices);
            
            //Act
            var result1 = _mobility.CanMove(_differentDices);
            var result2 = _mobility.CanMove(_differentDices);
            var result3 = _mobility.CanMove(_differentDices);
            var result4 = _mobility.CanMove(_differentDices);

            //Assert
            result1.Should().Be(MovementResult.CannotMove);
            result2.Should().Be(MovementResult.CannotMove);
            result3.Should().Be(MovementResult.CannotMove);
            result4.Should().Be(MovementResult.ExpiredUnfreeze);
        }
    }
}