using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Test.Core
{
    public class EnergyPresenter : IInitializable, IDisposable
    {
        private readonly IEnergy _energy;
        private readonly EnergyView _energyView;
        private readonly EnergyViewConfig _energyViewConfig;
        private readonly CompositeDisposable _disposables = new();

        public EnergyPresenter(IEnergy energy, EnergyView energyView, EnergyViewConfig energyViewConfig)
        {
            _energy = energy ?? throw new ArgumentNullException(nameof(energy));
            _energyView = energyView ?? throw new ArgumentNullException(nameof(energyView));
            _energyViewConfig = energyViewConfig;
        }

        public void Initialize()
        {
            _energy.Count.Subscribe(OnEnergyValueChanged).AddTo(_disposables);
        }

        private void OnEnergyValueChanged(int value)
        {
            string energyText = value.ToString();
            Color energyTextColor = GetColorFromValue(value);

            _energyView.SetEnergyText(energyText, energyTextColor);
        }
        
        private Color GetColorFromValue(int value)
        {
            foreach (var entry in _energyViewConfig.SortedEnergyColors.Reverse())
            {
                if (value >= entry.Threshold)
                    return entry.Color;
            }

            throw new ArgumentException($"Invalid value for config! {_energyViewConfig.name}");
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}