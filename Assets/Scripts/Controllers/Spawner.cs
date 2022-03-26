using Controllers.API;

namespace Controllers
{
    public abstract class Spawner<V, M> : ISpawner<V, M>
    {
        public abstract V Spawn(M model);
    }
}