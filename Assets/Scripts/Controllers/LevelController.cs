using System.Collections.Generic;
using System.Linq;
using Controllers.API;
using Data;
using Models;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class LevelController : ILevelController
    {
        //[SerializeField] private 
            
        [Inject] private ConfigMediator _mediator;
        

        private List<LevelModel> _levels;
        
        public LevelModel CurrentLevel { get; private set; }
        
        public void Initialize()
        {
            _levels = _mediator.GetLevels();
            
            // we need in just one level in this test case so I didn't realize selecting levels feature
            CurrentLevel = _levels.FirstOrDefault();
        }
    }
}