using System;

namespace Test.Core
{
    public class Clicker
    {
        private readonly IWallet _wallet;
        private readonly IEnergy _energy;

        public Clicker(IWallet wallet, IEnergy energy)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _energy = energy ?? throw new ArgumentNullException(nameof(energy));
        }

        public void Click()
        {
            _wallet.Put(1);
            _energy.Decrease(1);
        }
    }
}
