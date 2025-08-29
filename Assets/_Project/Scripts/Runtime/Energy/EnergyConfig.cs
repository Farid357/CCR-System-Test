using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.Core
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/EnergyConfig")]
    public class EnergyConfig : ScriptableObject
    {
        [Tooltip("List of energy thresholds and corresponding colors")]
        [SerializeField] private List<EnergyColorEntry> _energyColors = new();

        [field: SerializeField] public int MaxValue { get; private set; } = 1000;
        
        [field: SerializeField] public float TimeToAddEnergy { get; private set; } = 10f;
       
        [field: SerializeField] public int AddEnergyCount { get; private set; } = 10;
        
        public IReadOnlyList<EnergyColorEntry> EnergyColors => _energyColors;
        
        public IReadOnlyList<EnergyColorEntry> SortedEnergyColors => _energyColors.OrderBy(energyColor => energyColor.Threshold).ToList();
        
        [Serializable]
        public struct EnergyColorEntry
        {
            public int Threshold;
            public Color Color;    
        }
    }
}