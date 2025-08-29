using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.Core
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/EnergyConfig")]
    public class EnergyViewConfig : ScriptableObject
    {
        [Tooltip("List of energy thresholds and corresponding colors")]
        [SerializeField] private List<EnergyColorEntry> _energyColors = new();

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