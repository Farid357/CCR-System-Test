using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Test.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private WalletView _walletView;
        [SerializeField] private ClickerView _clickerView;
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private EnergyConfig _energyConfig;

        public override void InstallBindings()
        {
            DOTween.Init();
            
            IWallet wallet = new Wallet(0);
            IEnergy energy = new Energy(_energyConfig.MaxValue);
            Clicker clicker = new Clicker(wallet, energy);

            var walletPresenter = new WalletPresenter(wallet, _walletView);
            var energyPresenter = new EnergyPresenter(energy, _energyView, _energyConfig);
            var clickerPresenter = new ClickerPresenter(clicker, _clickerView);
            
            Container.BindInterfacesTo<WalletPresenter>().FromInstance(walletPresenter).AsSingle();
            Container.BindInterfacesTo<EnergyPresenter>().FromInstance(energyPresenter);

            Container.BindInterfacesAndSelfTo<Wallet>().FromInstance(wallet).AsSingle();
            Container.BindInterfacesAndSelfTo<Energy>().FromInstance(energy).AsSingle();
            Container.BindInterfacesAndSelfTo<ClickerPresenter>().FromInstance(clickerPresenter).AsSingle();

            Container.BindInstance(clicker).AsSingle();
        }
    }
}