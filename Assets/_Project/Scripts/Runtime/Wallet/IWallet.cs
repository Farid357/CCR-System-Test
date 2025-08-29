using UniRx;

namespace Test.Core
{
    public interface IWallet
    {
        IReadOnlyReactiveProperty<int> Coins { get; }

        void Put(int coins);
    }
}