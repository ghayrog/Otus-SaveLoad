namespace SaveSystem
{
    public interface IServiceResolver
    {
        TService Resolve<TService>();
    }
}
