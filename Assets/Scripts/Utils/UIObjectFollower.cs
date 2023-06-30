using UnityEngine;

namespace Scripts
{
    public class UIObjectFollower : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Transform target;
        
        private void Update()
        {
            if (!gameCamera || !target) return;
            var worldPosition = target.position;
            var screenPosition = gameCamera.WorldToScreenPoint(worldPosition);
            screenPosition.x += offset.x;
            screenPosition.y += offset.y;
            var objectTransform = transform;
            screenPosition.z = objectTransform.position.z;

            objectTransform.position = screenPosition;
        }
    }
}