namespace Models
{
    [System.Serializable]
    public class LevelModel
    {
        public string id;
        public int sizeX;
        public int sizeZ;
        public int maxUnits;
        public int maxBombs;
        public int maxWalls;
        public int bombSpawnAltitude;
    }
}