using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Scripts.Cubes
{
    public class CubeShooter : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Vector3 bulletSpawnOffset;
        [SerializeField] private CubeSpawner cubeSpawner;
        [SerializeField] private float shootingCooldown = 4f;

        [Header("Bullet Parameters")]
        [SerializeField] private float bulletForce;
        [SerializeField] private float bulletLifetime = 4f;

        [Header("Events")] 
        public UnityEvent<int> onTargetShoot;
        public UnityEvent onHit;

        private Vector3 BulletSpawnPosition => transform.position + bulletSpawnOffset;

        private List<TargetCube> Cubes => cubeSpawner.Cubes;

        private bool _shoot;
        private bool _bulletAlive;
        private float _lastShootTime;
        
        public void StartShooting()
        {
            _shoot = true;
        }

        private void Update()
        {
            if (!_shoot) return;
            if (_lastShootTime + shootingCooldown > Time.time) return;
            
            ShootRandomCube();
        }

        private void ShootRandomCube()
        {
            if (Cubes.Count <= 0)
            {
                _shoot = false;
                return;
            }
            int randomIndex = Random.Range(0, Cubes.Count);

            var target = Cubes[randomIndex];

            var dir = target.transform.position - BulletSpawnPosition;
            dir.Normalize();
            
            var bullet = Instantiate(bulletPrefab, BulletSpawnPosition, bulletPrefab.transform.rotation, null);
            bullet.Initialize(dir, bulletForce, bulletLifetime, target.transform);

            bullet.OnCubeHit += () =>
            {
                _shoot = false;
                onHit.Invoke();
            };

            _lastShootTime = Time.time;
            
            onTargetShoot.Invoke(target.Number);
        }
        

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(BulletSpawnPosition, 0.1f);
        }
    }
}