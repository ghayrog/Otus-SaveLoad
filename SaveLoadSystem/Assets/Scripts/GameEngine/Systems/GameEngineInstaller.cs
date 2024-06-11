using Zenject;
using UnityEngine;

namespace GameEngine
{
    public sealed class GameEngineInstaller : MonoInstaller
    {
        [SerializeField]
        UnitPrefabManager _unitPrefabManager;

        [SerializeField]
        UnitManager _unitManager;

        [SerializeField]
        ResourceService _resourceService;

        public override void InstallBindings()
        {
            Container.Bind<ResourceService>().FromInstance(_resourceService).AsSingle().NonLazy();
            Container.Bind<UnitManager>().FromInstance(_unitManager).AsSingle().NonLazy();
            Container.Bind<UnitPrefabManager>().FromInstance(_unitPrefabManager).AsSingle().NonLazy();

            InitializeGameEngine();
        }

        private void InitializeGameEngine()
        {
            _unitPrefabManager.InitializePrefabDictionary();
            _resourceService.SetResources(FindObjectsOfType<Resource>());
            _unitManager.SetupUnits(FindObjectsOfType<Unit>());
        }
    }
}