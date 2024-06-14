using UnityEngine;
using Sirenix.OdinInspector;
using Zenject;
using SaveSystem;

namespace GameSaveLoad
{

    public sealed class SaveLoadManager : MonoBehaviour
    {
        private ISaveLoader[] _saveLoaders;
        private IServiceResolver _gameDIContainer;
        private IGameRepository _gameRepository;

        [Inject]
        private void Construct(ISaveLoader[] saveLoaders, IServiceResolver gameDIContainer, IGameRepository gameRepository)
        { 
            _saveLoaders = saveLoaders;
            _gameDIContainer = gameDIContainer;
            _gameRepository = gameRepository;
        }

        [Button]
        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame(_gameRepository,_gameDIContainer);
            }

            _gameRepository.SaveState();
        }

        [Button]
        public void Load()
        {
            _gameRepository.LoadState();

            foreach (var saveLoader in _saveLoaders)
            { 
                saveLoader.LoadGame(_gameRepository,_gameDIContainer);
            }
        }
    }
}
