using System;
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
        [SerializeField] private LayerMask wallLayer;
        
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

            ChangeDirection();
            
            _isMoving = true;
        }

        public void BecomeKiller()
        {
            onKilled.Invoke(Number);

            enabled = false;

            gameObject.AddComponent<KillerCube>();
        }

        public void ForceDeath()
        {
            onKilled.Invoke(Number);
            
            Destroy(gameObject);
        }

        private void ChangeDirection()
        {
            _direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
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

        private void OnCollisionEnter(Collision other)
        {
            ChangeDirectionOnCollision(other);
        }

        private void ChangeDirectionOnCollision(Collision other)
        {
            if (LayerMaskUtils.CompareLayerMasks(wallLayer, other.gameObject.layer))
            {
                ChangeDirection();
            }
        }
        
        private void OnCollisionStay(Collision other)
        {
            ChangeDirectionOnCollision(other);
        }
    }
}