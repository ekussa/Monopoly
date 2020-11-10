using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class SquareActions
    {
        private const decimal StartLandCash = 200m;
        
        private readonly PlayerRepository _playerRepository;
        private readonly Dictionary<Type, Action<Player, Square>> _actionsStop;
        private readonly Dictionary<Type, Action<Player, Square>> _actionsPass;
        private readonly IDice _dice;

        public SquareActions(PlayerRepository playerRepository, IDice dice)
        {
            _dice = dice;
            _playerRepository = playerRepository;
            _actionsStop = new Dictionary<Type, Action<Player, Square>>
            {
                {typeof(StartLand), OnStartLandStop},
                {typeof(Land), OnPropertyStop},
                {typeof(Company), OnPropertyStop},
                {typeof(ChanceSquare), OnChanceSquareStop},
                {typeof(CommunityChest), OnCommunityChestStop},
                {typeof(FreeStop), OnFreeStopStop},
                {typeof(FreezeEntry), OnFreezeEntryStop},
                {typeof(FreezeVisit), OnFreezeVisitStop},
                {typeof(Theft), OnTheftStop},
            };
            _actionsPass = new Dictionary<Type, Action<Player, Square>>
            {
                {typeof(StartLand), OnStartLandPass},
                {typeof(Land), OnNopPass},
                {typeof(Company), OnNopPass},
                {typeof(ChanceSquare), OnNopPass},
                {typeof(CommunityChest), OnNopPass},
                {typeof(FreeStop), OnNopPass},
                {typeof(FreezeEntry), OnNopPass},
                {typeof(FreezeVisit), OnNopPass},
                {typeof(Theft), OnNopPass},
            };
        }

        private void OnNopPass(Player player, Square square)
        {
        }

        private void OnStartLandPass(Player player, Square square)
        {
            player.Patrimony.Credit(StartLandCash);
        }

        private void OnTheftStop(Player player, Square square)
        {
            throw new NotImplementedException();
        }

        private void OnFreezeVisitStop(Player player, Square square)
        {
            throw new NotImplementedException();
        }

        private void OnFreezeEntryStop(Player player, Square square)
        {
            throw new NotImplementedException();
        }

        private void OnFreeStopStop(Player player, Square square)
        {
            throw new NotImplementedException();
        }

        private void OnCommunityChestStop(Player player, Square square)
        {
            throw new NotImplementedException();
        }

        private void OnChanceSquareStop(Player player, Square square)
        {
            throw new NotImplementedException();
        }

        private void OnPropertyStop(Player player, Square square)
        {
            var property = (Property) square;
            var owner = _playerRepository.GetOwnerOf(property);
            if (owner.WouldLikeToSell(property) && player.WouldLikeToBuy(property))
            {
                player.Buy(property, player);
                return;
            }

            var transaction = CalculateSpending(property);
            player.
                Patrimony.
                Debit(transaction);
            
            owner.
                Patrimony.
                Credit(transaction);
        }

        private void OnStartLandStop(Player player, Square square)
        {
            player.Patrimony.Credit(StartLandCash);
        }

        public void OnPlayerStop(Player player, Square square)
        {
            Invoke(_actionsStop, player, square);
        }

        public void OnPlayerPass(Player player, Square square)
        {
            Invoke(_actionsPass, player, square);
        }

        private decimal CalculateSpending(Square square)
        {
            return square switch
            {
                Land land => land.GetRent,
                Company company => company.GetBill(_dice.LastRoll().Sum()),
                _ => 0
            };
        }

        private static void Invoke(
            IReadOnlyDictionary<Type, Action<Player, Square>> actionBase,
            Player player,
            Square square)
        {
            var type = square.GetType();
            actionBase[type].Invoke(player, square);
        }
    }
}