using UnityEngine;

namespace AA
{
    public class CharacterRotator : MonoBehaviour
    {
        public Transform CachedTransform;

        private void Awake()
        {
            CachedTransform = transform;

        }
    }
}
