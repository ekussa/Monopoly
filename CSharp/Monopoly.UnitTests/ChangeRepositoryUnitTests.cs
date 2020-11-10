using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Monopoly.UnitTests
{
    [TestFixture]
    public class ChangeRepositoryUnitTests
    {
        private const int TotalCards = 30;
        private const int EarnTotalCards = 13;
        private const int SpendTotalCards = 14;
        private const int PrisonExitCards = 1;
        private const int Earn50FromEveryOneCards = 1;
        private const int GoToPrisonChangeCards = 1;

        [Test]
        public void ShouldTotalChangeCards()
        {
            //Arrange
            var hashSet = new HashSet<ChanceCard>();
            var changeRepository =
                new ChangeRepository(
                    new List<Player>());

            //Act
            for (var i = 0; i < TotalCards*3; i++)
            {
                var result = changeRepository.Next();
                if (!hashSet.Contains(result))
                    hashSet.Add(result);
            }

            //Assert
            hashSet.Count.Should().Be(TotalCards);
            var groups =
                hashSet.
                    GroupBy(_ => _.GetType()).
                    ToList();
            groups.Count.Should().Be(5);

            Assert(groups, typeof(EarnChangeCard), EarnTotalCards);
            Assert(groups, typeof(SpendChangeCard), SpendTotalCards);
            Assert(groups, typeof(PrisonExitCard), PrisonExitCards);
            Assert(groups, typeof(Earn50FromEveryOne), Earn50FromEveryOneCards);
            Assert(groups, typeof(GoToPrisonChangeCard), GoToPrisonChangeCards);
        }

        private static void Assert(List<IGrouping<Type, ChanceCard>> groups, Type type, int total)
        {
            groups.
                Find(_ => _.Key == type).
                ToList().
                Count.
                Should().
                Be(total);
        }
    }
}