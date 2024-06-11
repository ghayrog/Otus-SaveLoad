namespace SaveSystem
{
    public interface ISaveLoader
    { 
        void SaveGame(IGameRepository gameRepository, IGameDIContainer context);

        void LoadGame(IGameRepository gameRepository, IGameDIContainer context);
    }
}
