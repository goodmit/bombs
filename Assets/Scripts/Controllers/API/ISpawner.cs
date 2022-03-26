namespace Controllers.API
{
    public interface ISpawner<T, M>
    {
        public T Spawn(M model);
    }
}