using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class CubeIndicatorSystem : MonoBehaviour
    {
        [SerializeField] private RectTransform indicatorPrefab;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private RectTransform indicatorContainer;
        
        

        private Dictionary<int, CubePair> _cubeMap;

        private bool Active => _cubeMap.Count > 0;

        private void Awake()
        {
            _cubeMap = new Dictionary<int, CubePair>();
        }

        public void RegisterCube(TargetCube cube)
        {
            var indicator = Instantiate(indicatorPrefab, indicatorContainer);
            indicator.GetComponent<TextMeshProUGUI>().text = cube.Number.ToString();
            _cubeMap[cube.Number] = new CubePair(cube, indicator);
            cube.onKilled.AddListener(EliminateCube);
        }

        private void EliminateCube(int number)
        {
            var pair = _cubeMap[number];
            pair.Cube.onKilled.RemoveListener(EliminateCube);
            Destroy(pair.Indicator.gameObject);
            
            _cubeMap.Remove(number);
        }

        private void Update()
        {
            if (!Active) return;
            RenderIndicators();
        }

        private void RenderIndicators()
        {
            foreach (var cubeKey in _cubeMap.Keys)
            {
                var pair = _cubeMap[cubeKey];

                var cube = pair.Cube;

                var worldPosition = cube.transform.position;
                var screenPosition = gameCamera.WorldToScreenPoint(worldPosition);

                var indicator = pair.Indicator;
                screenPosition.z = indicator.position.z;
                indicator.position = screenPosition;
            }
        }
        
        private struct CubePair
        {
            public readonly TargetCube Cube;
            public readonly RectTransform Indicator;

            public CubePair(TargetCube cube, RectTransform indicator)
            {
                Cube = cube;
                Indicator = indicator;
            }
        }
    }
}