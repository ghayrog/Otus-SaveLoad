namespace SaveSystem
{
    public interface ISaveLoader
    { 
        void SaveGame(IGameRepository gameRepository, IServiceResolver context);

        void LoadGame(IGameRepository gameRepository, IServiceResolver context);
    }
}
