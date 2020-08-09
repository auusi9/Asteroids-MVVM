namespace Code.Infrastructure.Services
{
    public interface IAsteroidsPerLevelService : IGet<int, int>
    {
    }

    public interface IGet<in T, out T1>
    {
        T1 Get(T input);
    }
}