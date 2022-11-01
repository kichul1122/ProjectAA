using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AA
{
	public class AAPool<T> : ObjectPool<T> where T : Component
	{
		private readonly GameObject _prefab;

		public AAPool(string prefabPath)
		{
			_prefab = Managers.Resource.LoadPrefab(prefabPath);
		}

		public AAPool(GameObject prefab)
		{
			_prefab = prefab;
		}
		//protected abstract T CreateInstance();

		protected override T CreateInstance()
		{
			GameObject newGO = Managers.Resource.Instantiate(_prefab);

			return newGO.GetComponent<T>();
		}

		//protected override void OnBeforeRent(T instance)
		//{
		//	base.OnBeforeRent(instance);
		//}

		//protected override void OnBeforeReturn(T instance)
		//{
		//	base.OnBeforeReturn(instance);
		//}
	}

	/// <summary>
	/// Prefab To GameObject Pool
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AAAddressablesPool<T> : ObjectPool<T> where T : Component
	{
		private readonly GameObject _prefab;

		public AAAddressablesPool(GameObject prefab)
		{
			_prefab = prefab;
		}

		protected override T CreateInstance()
		{
			GameObject newGO = Managers.Resource.Instantiate(_prefab);

			return newGO.GetComponent<T>();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
