using System;

namespace Test.Core
{
    public class Clicker
    {
        private readonly IWallet _wallet;

        public Clicker(IWallet wallet)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        }

        public void Click()
        {
            _wallet.Put(1);
        }
    }
}
