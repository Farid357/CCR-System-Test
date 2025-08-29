using System;
using Zenject;

namespace Test.Core
{
    public class ClickerPresenter : IInitializable, IDisposable
    {
        private readonly Clicker _clicker;
        private readonly ClickerView _view;

        public ClickerPresenter(Clicker clicker, ClickerView view)
        {
            _clicker = clicker ?? throw new ArgumentNullException(nameof(clicker));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Initialize()
        {
            _view.Button.onClick.AddListener(Click);
        }

        public void Click()
        {
            _view.PlayEffect();
            _view.PlayVFX();
            
            _clicker.Click();
        }

        public void Dispose()
        {
            _view.Button.onClick.RemoveListener(Click);
        }
    }
}