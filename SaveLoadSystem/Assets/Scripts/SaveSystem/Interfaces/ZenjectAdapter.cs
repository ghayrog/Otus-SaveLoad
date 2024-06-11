using Zenject;

namespace SaveSystem
{
    public sealed class ZenjectAdapter : IGameDIContainer
    {
        private DiContainer _diContainer;

        public ZenjectAdapter(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public TService Resolve<TService>()
        {
            return _diContainer.Resolve<TService>();
        }
    }
}
