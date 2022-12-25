using UnityEngine;

namespace AA
{
    public class Character : MonoBehaviour
    {
        public int ObjectId;

        public Vector3 Position { get => _cachedTransform.position; set => _cachedTransform.position = value; }

        private Transform _cachedTransform;
        public Transform CachedTransform => _cachedTransform;

        private Renderer _renderer;

        public CharacterObservable Observable = new CharacterObservable();

        public ECharacter ECharacter = ECharacter.None;

        public Character Construct()
        {
            ObjectId = GetInstanceID();
            _cachedTransform = transform;
            _renderer = GetComponentInChildren<Renderer>();

            return this;
        }

        private void OnDestroy()
        {
            Observable.Dispose();
        }

        public void Die()
        {
            Observable.OnDieObserver().OnNext(this);
        }

        public void OnRemove()
        {
            Observable.OnRemoveObserver().OnNext(this);
        }

        public void ChangeMaterial(Material material)
        {
            _renderer.sharedMaterial = material;
        }

        public Character SetParent(Transform parentTransform)
        {
            _cachedTransform.SetParent(parentTransform);

            return this;
        }

        public Character Teleport(Vector3 spawnPosition)
        {
            Position = spawnPosition;

            return this;
        }
    }
}
