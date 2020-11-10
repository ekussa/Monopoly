using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class CommunityChestActionsUnitTests
    {
        private CommunityChest _communityChest;
        private TestPlayer _player;
        private SquareActions _boardCursor;

        [SetUp]
        public void Setup()
        {
            _communityChest = new CommunityChest();
            _player = new TestPlayer(0);
            _boardCursor = new BoardCursor(
                new Board(),
                new PlayerCursor(
                    new List<Player>
                    {
                        _player
                    },
                new Dice()));
        }
        
        [Test]
        public void ShouldDebit_WhenStop()
        {
            //Act
            _boardCursor.OnPlayerStop(
                _player,
                _communityChest);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(200);
        }
        
        [Test]
        public void ShouldNotDebit_WhenPass()
        {
            //Act
            _boardCursor.OnPlayerPass(
                _player,
                _communityChest);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(0);
        }
    }
}