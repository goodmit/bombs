using System;
using Models;
using UnityEngine;
using Zenject;

namespace View
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BombView : MonoBehaviour
    {
        public event Action<BombView> OnDestroy;

        protected BombModel _model;
        [SerializeField]
        protected ParticleSystem _particlePrefab;

        [Inject]
        public void Construct(BombModel model)
        {
            _model = model;
        }

        private bool _activated;
        protected MeshRenderer _renderer;
        
        private void Awake()
        {
            //_particle = GetComponentInChildren<ParticleSystem>();
            _renderer = GetComponent<MeshRenderer>();
        }

        protected virtual void OnEnable()
        {
            _renderer.material.color = GetColor(_model.bombType);
        }

        private Color GetColor(string bombType)
        {
            return bombType switch
            {
                "forced" => Color.yellow,
                _ => Color.black
            };
        }
        
        public void Activate(Vector3 pos)
        {
            transform.position = pos;
            _renderer.enabled = true;
            _activated = true;
        }
        
        public virtual void Explode()
        {
            var physicObjects = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
            
            foreach (var t in physicObjects)
            {
                if (Vector3.Distance(transform.position, t.transform.position) <= _model.explodeRange)
                {
                    var unit = t.GetComponent<UnitView>();
                    if (unit != null)
                    {
                        Debug.Log($"hit on {unit.name}");
                        unit.Hit(_model.damage);
                    }
                    t.AddExplosionForce(500, transform.position, _model.explodeRange);
                }
            }
            OnDestroy?.Invoke(this);

            Instantiate(_particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_activated)
            {
                Explode();
            }
        }
        
        public class Factory : PlaceholderFactory<BombModel, BombView>
        {
        }
    }
}