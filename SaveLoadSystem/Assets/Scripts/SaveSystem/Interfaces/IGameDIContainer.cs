namespace SaveSystem
{
    public interface IGameDIContainer
    {
        TService Resolve<TService>();
    }
}
