using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class TargetCube : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        public UnityEvent<int> onKilled;
        
        private int _number;

        public int Number => _number;

        public void Initialize(int number)
        {
            _number = number;
            
            var randomColor = Random.ColorHSV();
            SetColor(randomColor);
        }

        private void SetColor(Color color)
        {
            var material = new Material(Shader.Find("Standard"))
            {
                color = color
            };
            meshRenderer.material = material;
        }

        private void OnDestroy()
        {
            onKilled.Invoke(_number);
        }
    }
}