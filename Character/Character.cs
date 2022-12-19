using UniRx;
using UnityEngine;

namespace AA
{
    public class Character : MonoBehaviour
    {
        public int ObjectId;

        public Vector3 Position { get => _cachedTransform.position; set => _cachedTransform.position = value; }

        #region Character Components
        private Transform _cachedTransform;
        public Transform CachedTransform => _cachedTransform;
        private Renderer _renderer;

        public CharacterObservable Observable = new CharacterObservable();
        #endregion

        private CompositeDisposable _disposables;

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
            Observable.Dispose();
        }

        public void Die()
        {
            Observable.OnDieAsObserver().OnNext(this);
        }

        public void OnRemove()
        {
            Observable.OnRemoveAsObserver().OnNext(this);
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
    }
}
