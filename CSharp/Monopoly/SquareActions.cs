using System;
using System.Collections.Generic;

namespace Monopoly
{
    public abstract class SquareActions
    {
        private readonly Dictionary<Type, Action<Player, Square>> _actionsStop;
        private readonly Dictionary<Type, Action<Player, Square>> _actionsPass;

        protected SquareActions()
        {
            _actionsStop = new Dictionary<Type, Action<Player, Square>>
            {
                {typeof(StartLand), OnStartLandStop},
                {typeof(Land), OnLandStop},
                {typeof(Company), OnCompanyStop},
                {typeof(ChanceSquare), OnChanceSquareStop},
                {typeof(CommunityChest), OnCommunityChestStop},
                {typeof(FreeStop), OnNop},
                {typeof(FreezeEntry), OnFreezeEntryStop},
                {typeof(FreezeVisit), OnNop},
                {typeof(Theft), OnTheftStop},
            };
            _actionsPass = new Dictionary<Type, Action<Player, Square>>
            {
                {typeof(StartLand), OnStartLandPass},
                {typeof(Land), OnNop},
                {typeof(Company), OnNop},
                {typeof(ChanceSquare), OnNop},
                {typeof(CommunityChest), OnNop},
                {typeof(FreeStop), OnNop},
                {typeof(FreezeEntry), OnNop},
                {typeof(FreezeVisit), OnNop},
                {typeof(Theft), OnNop},
            };
        }

        private static void OnNop(Player player, Square square)
        {
        }

        protected abstract void OnStartLandStop(Player player, Square square);
        protected abstract void OnCommunityChestStop(Player player, Square square);
        protected abstract void OnStartLandPass(Player player, Square square);
        protected abstract void OnTheftStop(Player player, Square square);
        protected abstract void OnFreezeEntryStop(Player player, Square square);
        protected abstract void OnChanceSquareStop(Player player, Square square);
        protected abstract void OnLandStop(Player player, Square square);
        protected abstract void OnCompanyStop(Player player, Square square);


        public void OnPlayerStop(Player player, Square square)
        {
            Invoke(_actionsStop, player, square);
        }

        public void OnPlayerPass(Player player, Square square)
        {
            Invoke(_actionsPass, player, square);
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