using Controllers;
using Data;
using Models;
using UnityEngine;
using View;
using Zenject;

namespace ZenjectInstallers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject container;

        [SerializeField]
        private GameObject bomb;
        [SerializeField]
        private GameObject clusterBomb;
        [SerializeField]
        private GameObject unit;
        
        public override void InstallBindings()
        {
            var configMediator = new ConfigMediator();
            Container.Bind<ConfigMediator>().FromInstance(configMediator).AsSingle();
            
            Container.BindInterfacesAndSelfTo<BombSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClusterBombSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitSpawner>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitsController>().AsSingle();
            Container.BindInterfacesAndSelfTo<BombController>().AsSingle();
            
            Container.BindFactory<BombModel, BombView, BombView.Factory>().FromComponentInNewPrefab(bomb);
            Container.BindFactory<BombModel, ClusterBombView, ClusterBombView.Factory>().FromComponentInNewPrefab(clusterBomb);
            Container.BindFactory<UnitModel, UnitView, UnitView.Factory>().FromComponentInNewPrefab(unit);
        }
    }
}
