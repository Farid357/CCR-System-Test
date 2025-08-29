using UniRx;

namespace Test.Core
{
    public interface IReadOnlyEnergy
    {
        IReadOnlyReactiveProperty<int> Count { get; }
    }
}