using UniRx.Toolkit;
using UnityEngine;

namespace AA
{
	public class AAPool<T> : ObjectPool<T> where T : Component
	{
		private readonly T _prefab;

		public AAPool(string prefabPath, Component parent)
		{
			_prefab = Managers.Resource.LoadAsset<T>(prefabPath, parent);
		}

		public AAPool(T prefab)
		{
			_prefab = prefab;
		}

		protected override T CreateInstance()
		{
			return Managers.Resource.Instantiate(_prefab);
		}
	}
}
