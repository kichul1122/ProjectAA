using UniRx.Toolkit;
using UnityEngine;

namespace AA
{
	public class AAPool<T> : ObjectPool<T> where T : Component
	{
		private readonly T _prefab;
		private Transform _parent;

		public AAPool(string prefabPath, Component parentComponent, Transform parentTransform)
		{
			_prefab = Managers.Resource.LoadPrefab<T>(prefabPath, parentComponent);
			SetParent(parentTransform);
		}

		public AAPool(T prefab, Transform parentTransform)
		{
			_prefab = prefab;
		}

		protected override T CreateInstance()
		{
			var newT = Managers.Resource.Instantiate(_prefab);

			if (_parent)
			{
				newT.transform.SetParent(_parent, false);
			}

			return newT;
		}

		public AAPool<T> SetParent(Transform parent)
		{
			_parent = parent;
			return this;
		}
	}
}
