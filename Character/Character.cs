using UniRx;
using UnityEngine;

namespace AA
{
    public partial class Character : MonoBehaviour
    {
        public CharacterModel Model => _setting.Model;

        public int ObjectId;

        public Vector3 Position { get => _cachedTransform.position; set => _cachedTransform.position = value; }

        #region Character Components
        private Transform _cachedTransform;
        public Transform CachedTransform => _cachedTransform;
        private Renderer _renderer;

        private CharacterSetting _setting;
        #endregion

        private CompositeDisposable _disposables;

        public Character Construct(CharacterModel characterModel)
        {
            _setting = this.GetOrAddComponent<CharacterSetting>().Construct(characterModel);

            Construct();

            return this;
        }

        public Character Construct()
        {
            ObjectId = GetInstanceID();
            _cachedTransform = transform;
            _renderer = GetComponentInChildren<Renderer>();
            _disposables = new CompositeDisposable();

            return this;
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Die()
        {
            _onDieSubject.OnNext(this);
        }

        public void ChangeMaterial(Material material)
        {
            _renderer.sharedMaterial = material;
        }

        public void SetParent(Transform parentTransform)
        {
            _cachedTransform.SetParent(parentTransform);
        }
    }
}
