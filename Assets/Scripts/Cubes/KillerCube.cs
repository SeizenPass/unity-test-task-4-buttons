using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Cubes
{
    public class KillerCube : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float speed;
        
        private List<TargetCube> _victims;
        private Transform _currentTarget;
        
        private bool _init;
        private bool _hunting;

        public void Initialize(float newSpeed, List<TargetCube> victims)
        {
            transform.localScale *= 2;
            speed = newSpeed;

            rigidbody = GetComponent<Rigidbody>();
            _victims = victims;

            _init = true;

            _hunting = ChooseTarget();
        }

        private bool ChooseTarget()
        {
            if (_victims.Count <= 0) return false;

            var randomIndex = Random.Range(0, _victims.Count);

            _currentTarget = _victims[randomIndex].transform;
            return _currentTarget;
        }

        private void FixedUpdate()
        {
            if (!_init || !_hunting) return;

            var dir = _currentTarget.position - rigidbody.position;
            dir.Normalize();

            rigidbody.MovePosition(rigidbody.position + dir * (speed * Time.fixedDeltaTime));
            var targetRotation = Quaternion.LookRotation(dir);
            var rigidbodyRotation = rigidbody.rotation;
            var angles = rigidbodyRotation.eulerAngles;
            angles.y = targetRotation.eulerAngles.y;
            rigidbodyRotation.eulerAngles = angles;
            rigidbody.rotation = rigidbodyRotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out TargetCube cube)) return;
            cube.ForceDeath();
            _hunting = ChooseTarget();
        }
    }
}