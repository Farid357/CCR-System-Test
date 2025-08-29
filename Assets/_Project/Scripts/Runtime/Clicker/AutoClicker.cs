using System;
using UnityEngine;
using Zenject;

namespace Test.Core
{
    public class AutoClicker : MonoBehaviour
    {
        [SerializeField] private ClickerConfig _config;
        
        private IReadOnlyEnergy _energy;
        private ClickerPresenter _clicker;
        private float _timer = 0f;

        [Inject]
        public void Initialize(IReadOnlyEnergy energy, ClickerPresenter clicker)
        {
            _energy = energy ?? throw new ArgumentNullException(nameof(energy));
            _clicker = clicker ?? throw new ArgumentNullException(nameof(clicker));
        }
        
        private void Update()
        {
            _timer += Time.unscaledDeltaTime;

            if (_timer >= _config.TimeToAutoClick)
            {
                _timer = 0f;

                if (_energy.Count.Value >= 1)
                    AutoClick();
            }
        }

        private void AutoClick()
        {
            _clicker.Click();
        }
    }
}