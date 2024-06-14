using Newtonsoft.Json;

namespace SaveSystem
{

    public sealed class GameRepository : IGameRepository
    {
        private GameState _gameState = new();

        private IGameStateLoader _gameStateLoader;

        public GameRepository(IGameStateLoader gameStateLoader)
        {
            _gameStateLoader = gameStateLoader;
        }

        public void LoadState()
        {
            _gameStateLoader.LoadState(_gameState);
        }

        public void SaveState()
        {
            _gameStateLoader.SaveState(_gameState);
        }

        public T GetData<T>()
        {
            var serializedData = _gameState.State[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public bool TryGetData<T>(out T value)
        {
            if (_gameState.State.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            _gameState.State[typeof(T).Name] = serializedData;
        }
    }
}
