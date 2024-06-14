namespace SaveSystem
{
    public interface IGameStateLoader
    {        
        void LoadState(GameState gameState);
        void SaveState(GameState gameState);
    }
}
