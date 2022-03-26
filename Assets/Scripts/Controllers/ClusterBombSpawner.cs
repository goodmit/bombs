using Models;
using View;
using Zenject;

namespace Controllers
{
    public class ClusterBombSpawner : Spawner<ClusterBombView, BombModel>
    {
        private readonly ClusterBombView.Factory _factory;

        [Inject]
        public ClusterBombSpawner(ClusterBombView.Factory factory)
        {
            _factory = factory;
        }

        public override ClusterBombView Spawn(BombModel model)
        {
            return _factory.Create(model);
        }
    }
}