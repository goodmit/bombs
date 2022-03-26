using System;
using Models;
using UnityEngine;
using Zenject;

namespace View
{
    public class UnitView : MonoBehaviour
    {
        public event Action<UnitView> OnDestroy;

        private int _hp;
        private bool _activated;
        
        private UnitModel _model;

        [Inject]
        public void Construct(UnitModel model)
        {
            _model = model;
        }
        
        public void Activate(Vector3 pos)
        {
            transform.position = pos;
            _activated = true;
        }
        
        public void Hit(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                KillSelf();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_activated && collision.gameObject.GetComponent<UnitView>() || collision.gameObject.GetComponent<WallView>())
            {
                KillSelf();
            }
        }
        
        private void KillSelf()
        {
            OnDestroy?.Invoke(this);
            Destroy(gameObject);
        }
        
        public class Factory : PlaceholderFactory<UnitModel, UnitView>
        {
        }
    }
}