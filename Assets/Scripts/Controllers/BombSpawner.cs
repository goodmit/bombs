using Models;
using View;
using Zenject;

namespace Controllers
{
    public class BombSpawner : Spawner<BombView, BombModel>
    {
        private readonly BombView.Factory _factory;

        [Inject]
        public BombSpawner(BombView.Factory factory)
        {
            _factory = factory;
        }

        public override BombView Spawn(BombModel model)
        {
            return _factory.Create(model);
        }
    }
}