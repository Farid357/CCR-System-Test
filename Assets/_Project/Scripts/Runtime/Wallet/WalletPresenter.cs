using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace Test.Core
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private readonly IWallet _wallet;
        private readonly WalletView _view;
        private readonly CoinsViewFactory _coinsViewFactory;
        private readonly CompositeDisposable _disposables = new();
      
        private int _wasCoins;

        public WalletPresenter(IWallet wallet, WalletView view, CoinsViewFactory coinsViewFactory)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _coinsViewFactory = coinsViewFactory ?? throw new ArgumentNullException(nameof(coinsViewFactory));
        }

        public void Initialize()
        {
            _wallet.Coins.Subscribe(OnCoinsChanged).AddTo(_disposables);
        }

        private async void OnCoinsChanged(int coins)
        {
            string coinsText = coins.ToString();

            if (coins > _wasCoins)
            {
                for (int i = 0; i < coins - _wasCoins; i++)
                {
                    if (i == coins - _wasCoins - 1)
                    {
                        await _coinsViewFactory.GetCoin().MoveTo(_view.CoinIcon, _coinsViewFactory.ObjectsPool);
                    }
                    else
                    {
                        _coinsViewFactory.GetCoin().MoveTo(_view.CoinIcon, _coinsViewFactory.ObjectsPool).Forget();
                    }
                }
            }

            _view.SetCoinsText(coinsText);
            _wasCoins = coins;
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}