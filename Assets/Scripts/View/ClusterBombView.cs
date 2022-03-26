using Models;
using UnityEngine;
using Zenject;

namespace View
{
    public class ClusterBombView : BombView
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            _renderer.material.color = Color.red;
        }

        public override void Explode()
        {
            var sourcePosition = transform.position;
            ClusterExplode(sourcePosition + Vector3.forward * (_model.explodeRange / 2));
            ClusterExplode(sourcePosition + Vector3.back * (_model.explodeRange / 2));
            ClusterExplode(sourcePosition + Vector3.right * (_model.explodeRange / 2));
            ClusterExplode(sourcePosition + Vector3.left * (_model.explodeRange / 2));
            
            base.Explode();
        }

        private void ClusterExplode(Vector3 position)
        {
            var physicObjects = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
            
            foreach (var t in physicObjects)
            {
                if (Vector3.Distance(position, t.transform.position) <= _model.explodeRange)
                {
                    var unit = t.GetComponent<UnitView>();
                    if (unit != null)
                    {
                        unit.Hit(_model.damage);
                    }
                    t.AddExplosionForce(500, position, _model.explodeRange);
                }
            }
            
            Instantiate(_particlePrefab, position, Quaternion.identity);
        }
        
        public new class Factory : PlaceholderFactory<BombModel, ClusterBombView>
        {
        }
    }
}