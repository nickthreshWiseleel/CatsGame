namespace Infrastructure
{
    public interface IFactory<T>
    {
        T Create();
    }
}