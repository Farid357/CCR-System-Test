using System;
using UniRx;

namespace Test.Core
{
    public class Wallet : IWallet
    {
        private readonly ReactiveProperty<int> _coins = new();

        public Wallet(int startCoins)
        {
            _coins.Value = startCoins;
        }

        public IReadOnlyReactiveProperty<int> Coins => _coins;
        
        public void Put(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));
           
            _coins.Value += coins;
        }
    }
}