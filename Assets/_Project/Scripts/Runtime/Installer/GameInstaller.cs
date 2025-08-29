using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Test.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private WalletView _walletView;
        [SerializeField] private ClickerView _clickerView;

        public override void InstallBindings()
        {
            DOTween.Init();
            
            IWallet wallet = new Wallet(0);
            Clicker clicker = new Clicker(wallet);
            
            var walletPresenter = new WalletPresenter(wallet, _walletView);
            var clickerPresenter = new ClickerPresenter(clicker, _clickerView);
            
            Container.BindInterfacesTo<WalletPresenter>().FromInstance(walletPresenter).AsSingle();
            Container.BindInterfacesTo<ClickerPresenter>().FromInstance(clickerPresenter).AsSingle();

        }
    }
}