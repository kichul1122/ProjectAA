using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AA
{
    public partial class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterModel _data;
        public CharacterModel Data => _data;

        public int ObjectId;

        public Vector3 Position { get => _cachedTransform.position; set => _cachedTransform.position = value; }

        #region Character Components
        private Transform _cachedTransform;
        private Renderer _renderer;
        #endregion

        private CompositeDisposable _disposables;

        public Character Construct(CharacterModel characterData)
        {
            _data = characterData;
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
