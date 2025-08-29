using System;
using UnityEngine;
using Zenject;

namespace Test.Core
{
    public class EnergyIncreaseTimer : MonoBehaviour
    {
        [SerializeField] private float _timeToAddEnergy = 10f;
        [SerializeField] private int _addEnergy = 10;
      
        private IEnergy _energy;
        private float _time;

        [Inject]
        public void Initialize(IEnergy energy)
        {
            _energy = energy ?? throw new ArgumentNullException(nameof(energy));
        }
        
        private void Update()
        {
            _time += Time.unscaledDeltaTime;

            if (_time >= _timeToAddEnergy)
            {
                _time = 0f;
                IncreaseEnergy();
            }
        }

        private void IncreaseEnergy()
        {
            _energy.Increase(_addEnergy);
        }
    }
}