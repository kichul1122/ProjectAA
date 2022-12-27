using UnityEngine;

namespace AA
{
    public class ProjectilePivot : MonoBehaviour
    {
        public Vector3 Forward => CachedTransform.forward;
        public Vector3 Position => CachedTransform.position;

        public Transform CachedTransform;

        private void Awake()
        {
            CachedTransform = transform;

        }
    }
}