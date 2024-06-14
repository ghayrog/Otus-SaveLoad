using Zenject;

namespace SaveSystem
{
    public sealed class ZenjectResolver : IServiceResolver
    {
        private DiContainer _diContainer;

        public ZenjectResolver(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public TService Resolve<TService>()
        {
            return _diContainer.Resolve<TService>();
        }
    }
}
