using System;
using UnityEngine;

namespace Scripts.Cubes
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;

        private float _bornTime, _lifetime;
        private Transform _targetCube;

        public Action OnCubeHit, OnDeath;

        public void Initialize(Vector3 direction, float force, float lifetime, Transform targetCube)
        {
            _bornTime = Time.time;
            _lifetime = lifetime;
            _targetCube = targetCube;
            
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        private void Update()
        {
            if (_bornTime + _lifetime < Time.time) Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform == _targetCube)
            {
                OnCubeHit?.Invoke();
            }
        }
    }
}