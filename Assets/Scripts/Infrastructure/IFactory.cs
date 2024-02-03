namespace Game.Infrastructure
{
    public interface IFactory<T>
    {
        T Create();
    }
}