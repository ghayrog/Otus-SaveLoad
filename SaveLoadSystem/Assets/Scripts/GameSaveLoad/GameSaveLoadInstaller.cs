using SaveSystem;
using UnityEngine;
using Zenject;

namespace GameSaveLoad
{
    public sealed class GameSaveLoadInstaller : MonoInstaller
    {
        [SerializeField]
        private ISaveLoader[] _saveLoaders;

        public override void InstallBindings()
        {
            Container.Bind<ISaveLoader>().To<UnitManagerSaveLoader>().AsSingle();
            Container.Bind<ISaveLoader>().To<ResourceServiceSaveLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameRepository>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EncryptedGameStateLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ZenjectResolver>().AsSingle().NonLazy();

        }
    }
}
