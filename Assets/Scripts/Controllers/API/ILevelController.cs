using Models;
using Zenject;

namespace Controllers.API
{
    public interface ILevelController : IInitializable
    {
        public LevelModel CurrentLevel { get; }
    }
}