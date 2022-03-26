using Models;
using View;
using Zenject;

namespace Controllers
{
    public class UnitSpawner : Spawner<UnitView, UnitModel>
    {
        private readonly UnitView.Factory _factory;
        
        [Inject]
        public UnitSpawner(UnitView.Factory factory)
        {
            _factory = factory;
        }
        
        public override UnitView Spawn(UnitModel model)
        {
            return _factory.Create(model);
        }
    }
}