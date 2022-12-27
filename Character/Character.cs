using UnityEngine;

namespace AA
{
    public class Character : MonoBehaviour
    {
        public int ObjectId;

        private Renderer _renderer;

        public Transform CachedTransform;
        public Vector3 Position { get => CachedTransform.position; set => CachedTransform.position = value; }

        public CharacterObservable Observable = new CharacterObservable();

        public ECharacter ECharacter = ECharacter.None;

        public CharacterRotator Rotator;

        private void Awake()
        {
            CachedTransform = transform;
            _renderer = GetComponentInChildren<Renderer>();
            Rotator = GetComponentInChildren<CharacterRotator>();

            this.GetOrAddComponent<CharacterGizmos>();

        }

        public Character Construct()
        {
            ObjectId = GetInstanceID();

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
            CachedTransform.SetParent(parentTransform);

            return this;
        }

        public Character Teleport(Vector3 spawnPosition)
        {
            Position = spawnPosition;

            return this;
        }

        public void Equip(Weapon weapon)
        {
            var characterWeapon = GetComponent<CharacterWeapon>();
            if (!characterWeapon) return;

            characterWeapon.Equip(weapon);
        }
    }
}
