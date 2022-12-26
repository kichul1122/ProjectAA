using UnityEngine;

namespace AA
{
    public class CharacterRotator : MonoBehaviour
    {
        public Vector3 Forward => CachedTransform.forward;
        public Transform CachedTransform;

        private void Awake()
        {
            CachedTransform = transform;

        }
    }
}
