using UnityEngine;

namespace AA
{
	public class Factory<T> where T : Component
	{
		private readonly T _prefab;

		public Factory(string path, Component owner)
		{
			_prefab = Managers.Resource.LoadPrefab<T>(path, owner);
		}

		public T Create()
		{
			return Managers.Resource.Instantiate(_prefab);
		}
	}
}
