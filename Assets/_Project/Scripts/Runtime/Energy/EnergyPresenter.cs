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
        private readonly EnergyConfig _energyConfig;
        private readonly CompositeDisposable _disposables = new();

        public EnergyPresenter(IEnergy energy, EnergyView energyView, EnergyConfig energyConfig)
        {
            _energy = energy ?? throw new ArgumentNullException(nameof(energy));
            _energyView = energyView ?? throw new ArgumentNullException(nameof(energyView));
            _energyConfig = energyConfig;
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
            foreach (var entry in _energyConfig.SortedEnergyColors.Reverse())
            {
                if (value >= entry.Threshold)
                    return entry.Color;
            }

            throw new ArgumentException($"Invalid value for config! {_energyConfig.name}");
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}