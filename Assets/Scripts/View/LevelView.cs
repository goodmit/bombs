using Controllers.API;
using Models;
using UnityEngine;
using Zenject;

namespace View
{
    public class LevelView : MonoBehaviour
    {
        [Inject] private ILevelController _levelController;

        [SerializeField] private WallView _wallPrefab;
        [SerializeField] private LevelModel levelModel;
        [SerializeField] private bool needLoadLevel;
        
        private void Start()
        {
            var loadedLevel = _levelController.CurrentLevel;
            if (needLoadLevel && loadedLevel != null)
            {
                levelModel = loadedLevel;
            }
            transform.name = levelModel.id;
            for (var i = 0; i < levelModel.maxWalls; i++)
            {
                var x = Random.Range(-levelModel.sizeX, levelModel.sizeX);
                var z = Random.Range(-levelModel.sizeZ, levelModel.sizeZ);
                var pos = new Vector3(x, 1, z);
                var angle = Random.Range(0, 2) * 90;
                Instantiate(_wallPrefab, pos, Quaternion.AngleAxis(angle, Vector3.up), transform);
            }
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
#if UNITY_EDITOR
            if(UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            return;
#endif
            Application.Quit();
        }
    }
}