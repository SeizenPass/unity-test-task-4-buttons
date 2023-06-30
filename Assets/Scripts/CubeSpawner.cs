using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class CubeSpawner : MonoBehaviour
    {
        [Header("Bindings")]
        [SerializeField] private TargetCube cubePrefab;

        [Header("Parameters")] 
        [Range(1, 50)]
        [SerializeField] private int maximumRandomRange = 30;
        [SerializeField] private Vector3 spawnOffset;
        [SerializeField] private float spawnRadius = 10f;

        public UnityEvent<TargetCube> onCubeCreated;
        
        private List<TargetCube> _cubes;

        private Vector3 InitialSpawnPosition => transform.position + spawnOffset;

        private void Awake()
        {
            _cubes = new List<TargetCube>();
        }

        public void SpawnCubes()
        {
            var randomInt = Random.Range(1, maximumRandomRange + 1);

            for (var i = 0; i <= randomInt; i++)
            {
                var number = i + 1;
                
                var pos = GetRandomSpawnPoint();
                var createdCube = Instantiate(cubePrefab, pos, cubePrefab.transform.rotation, null);
                
                createdCube.Initialize(number);
                
                _cubes.Add(createdCube);
                onCubeCreated.Invoke(createdCube);
            }
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return new Vector3
            {
                x = GetShiftedValue(InitialSpawnPosition.x),
                y = GetShiftedValue(InitialSpawnPosition.y),
                z = GetShiftedValue(InitialSpawnPosition.z)
            };
            
            float GetShiftedValue(float input)
            {
                return input + Random.Range(-spawnRadius, spawnRadius);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(InitialSpawnPosition, spawnRadius);
        }
    }
}