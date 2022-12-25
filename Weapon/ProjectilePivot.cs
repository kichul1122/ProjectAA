using UnityEngine;

namespace AA
{
    public class ProjectilePivot : MonoBehaviour
    {
        public Transform CachedTransform;

        private void Awake()
        {
            CachedTransform = transform;

        }
    }
}
