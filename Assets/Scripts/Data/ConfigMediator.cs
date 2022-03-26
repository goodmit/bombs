using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class ConfigMediator
    {
        public readonly List<LevelModel> LevelsList = new List<LevelModel>();
        public readonly List<UnitModel> UnitsList = new List<UnitModel>();
        public readonly List<BombModel> BombList = new List<BombModel>();
        
        private readonly string _data;
        
        public ConfigMediator()
        {
            _data = Resources.Load<TextAsset>("ConfigData/config_data").text;
            LoadData();
        }

        public List<LevelModel> GetLevels() => LevelsList;
        public List<UnitModel> GetUnits() => UnitsList;
        public List<BombModel> GetBombs() => BombList;
        
        private void LoadData()
        {
            var deserializedLevels = JsonConvert.DeserializeObject<Levels>(_data);
            LevelsList.AddRange(deserializedLevels.levels);
            
            var deserializedUnits = JsonConvert.DeserializeObject<Units>(_data);
            UnitsList.AddRange(deserializedUnits.units);

            var deserializedBombs = JsonConvert.DeserializeObject<Bombs>(_data);
            BombList.AddRange(deserializedBombs.bombs);
        }

        private class Levels
        {
            public List<LevelModel> levels;
        }
        
        private class Units
        {
            public List<UnitModel> units;
        }
        
        private class Bombs
        {
            public List<BombModel> bombs;
        }
    }
}