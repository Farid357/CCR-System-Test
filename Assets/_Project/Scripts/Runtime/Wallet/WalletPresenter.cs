using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Test.Core
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private readonly IWallet _wallet;
        private readonly WalletView _view;
        private readonly CompositeDisposable _disposables = new();

        public WalletPresenter(IWallet wallet, WalletView view)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _view = view;
        }

        public void Initialize()
        {
            _wallet.Coins.Subscribe(OnCoinsChanged).AddTo(_disposables);
        }

        private void OnCoinsChanged(int coins)
        {
            string coinsText = coins.ToString();
            
            _view.SetCoinsText(coinsText);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}