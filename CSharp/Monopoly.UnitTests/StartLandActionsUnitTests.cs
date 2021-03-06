﻿using System.Collections.Generic;
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
        private IDice _dice;

        [SetUp]
        public void Setup()
        {
            _dice = new Dice();
            _startLand = new StartLand();
            _player = new TestPlayer(0m);
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
        public void ShouldEarn_WhenStop()
        {
            //Act
            _squareActions.OnPlayerStop(
                _player,
                _startLand);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(StartLandPayment);
        }
        
        [Test]
        public void ShouldEarn_WhenPass()
        {
            //Act
            _squareActions.OnPlayerPass(_player, _startLand);
            
            //Assert
            _player.Patrimony.Cash.Should().Be(StartLandPayment);
        }
    }
}