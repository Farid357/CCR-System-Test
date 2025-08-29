using System;
using UniRx;

namespace Test.Core
{
    public class Energy : IEnergy
    {
        private readonly ReactiveProperty<int> _count = new ReactiveProperty<int>();

        public Energy(int maxValue)
        {
            MaxValue = _count.Value = maxValue;
        }
        
        public IReadOnlyReactiveProperty<int> Count => _count;
       
        public int MaxValue { get; }
        
        public void Increase(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (_count.Value + count > MaxValue)
            {
                _count.Value = MaxValue;
                return;
            }
            
            _count.Value += count;
        }

        public void Decrease(int count)
        {
            if (_count.Value - count < 0)
            {
                _count.Value = 0;
                return;
            }
            
            _count.Value -= count;
        }
    }
}