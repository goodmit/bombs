using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers.API;
using Data;
using Models;
using UnityEngine;
using Utils;
using View;
using Zenject;

namespace Controllers
{
    public class UnitsController : IUnitsController
    {
        [Inject] private ILevelController _levelController;
        [Inject] private ConfigMediator _mediator;
        [Inject] private ISpawner<UnitView, UnitModel> _spawner;
        
        private int _unitsCount;
        private LevelModel _level;
        private List<UnitModel> _unitModels;

        public void Initialize()
        {
            _unitModels = _mediator.GetUnits();
            _level = _levelController.CurrentLevel;
            SpawnUnits();
        }

        private async Task SpawnUnits()
        {
            while (CanSpawn())
            {
                SpawnUnit();
                await Awaiters.WaitForSeconds(Random.Range(0.1f, 0.5f));
            }
        }
        
        private void SpawnUnit()
        {
            _unitsCount++;
            var unit = _spawner.Spawn(GetRandomUnitModel());
            
            var x = Random.Range(-_level.sizeX, _level.sizeX);
            var z = Random.Range(-_level.sizeZ, _level.sizeZ);
            var pos = new Vector3(x, unit.transform.position.y, z);
            unit.Activate(pos);
            
            unit.OnDestroy += OnUnitDestroyHandler;
        }

        private void OnUnitDestroyHandler(UnitView unitView)
        {
            unitView.OnDestroy -= OnUnitDestroyHandler;
            _unitsCount--;
            if (CanSpawn())
            {
                SpawnUnit();
            }
        }
        
        private UnitModel GetRandomUnitModel()
        {
            var index = Random.Range(0, _unitModels.Count);
            return _unitModels[index];
        }
        
        private bool CanSpawn()
        {
            return _level.maxUnits > _unitsCount;
        }
    }
}