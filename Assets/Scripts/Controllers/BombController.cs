using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers.API;
using Data;
using Models;
using UnityEngine;
using Utils;
using View;
using Zenject;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class BombController : IBombController
    {
        [Inject] private ILevelController _levelController;
        [Inject] private ConfigMediator _mediator;
        [Inject] private ISpawner<BombView, BombModel> _spawner;
        [Inject] private ISpawner<ClusterBombView, BombModel> _clusterSpawner;
        
        private int _bombsInAction;
        private LevelModel _level;
        private List<BombModel> _bombModels;

        public void Initialize()
        {
            _bombModels = _mediator.GetBombs();
            _level = _levelController.CurrentLevel;
            SpawnAllBombs();
        }

        private async Task SpawnAllBombs()
        {
            while (CanSpawn())
            {
                SpawnBomb();
                await Awaiters.WaitForSeconds(1.5f);
            }
        }
        
        private void SpawnBomb()
        {
            _bombsInAction++;
            var model = GetRandomBombModel();

            var bomb = model.bombType == "cluster" ? _clusterSpawner.Spawn(model) : _spawner.Spawn(model);
            
            var x = Random.Range(-_level.sizeX, _level.sizeX);
            var z = Random.Range(-_level.sizeZ, _level.sizeZ);
            var pos = new Vector3(x, _level.bombSpawnAltitude, z);
            bomb.Activate(pos);
            
            bomb.OnDestroy += OnBombDestroyHandler;
        }

        private void OnBombDestroyHandler(BombView bombView)
        {
            bombView.OnDestroy -= OnBombDestroyHandler;
            _bombsInAction--;
            if (CanSpawn())
            {
                SpawnBomb();
            }
        }
        
        private BombModel GetRandomBombModel()
        {
            var index = Random.Range(0, _bombModels.Count);
            return _bombModels[index];
        }
        
        private bool CanSpawn()
        {
            return _level.maxBombs > _bombsInAction;
        }
    }
}