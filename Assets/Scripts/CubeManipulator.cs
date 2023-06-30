using UnityEngine;

namespace Scripts
{
    public class CubeManipulator : MonoBehaviour
    {
        [SerializeField] private CubeSpawner cubeSpawner;

        public void MoveCubes()
        {
            var cubes = cubeSpawner.Cubes;

            foreach (var cube in cubes)
            {
                cube.StartMoving();
            }
        }
    }
}