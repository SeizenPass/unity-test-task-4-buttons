using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class TargetCube : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float movementSpeed;
        

        public UnityEvent<int> onKilled;
        
        private int _number;
        private bool _isMoving;
        private Vector3 _direction;

        public int Number => _number;

        public void Initialize(int number)
        {
            _number = number;
            
            var randomColor = Random.ColorHSV();
            SetColor(randomColor);
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            rigidbody.MovePosition(transform.position + _direction * (movementSpeed * Time.fixedDeltaTime));
        }

        public void StartMoving()
        {
            if (_isMoving) return;

            _direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            
            _isMoving = true;
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