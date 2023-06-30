using UnityEngine;

namespace Scripts.Cubes
{
    public class CubeManipulator : MonoBehaviour
    {
        [SerializeField] private CubeSpawner cubeSpawner;
        
        [Header("Killer Mode")] 
        [SerializeField] private float killerSpeed = 15;

        

        public void MoveCubes()
        {
            var cubes = cubeSpawner.Cubes;

            foreach (var cube in cubes)
            {
                cube.StartMoving();
            }
        }

        public void EnterKillerCube()
        {
            var cubes = cubeSpawner.Cubes;
            if (cubes.Count <= 0) return;

            var randomIndex = Random.Range(0, cubes.Count);

            var rogueCube = cubes[randomIndex];
            
            rogueCube.BecomeKiller();
            var killer = rogueCube.GetComponent<KillerCube>();
            
            killer.Initialize(killerSpeed, cubes);
        }
    }
}