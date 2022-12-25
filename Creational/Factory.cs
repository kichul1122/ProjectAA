using UnityEngine;

namespace AA
{
	public class Factory<T> where T : Object
	{
		private readonly T _prefab;

		public Factory(string path, Component owner)
		{
			_prefab = Managers.Resource.LoadAsset<T>(path, owner);
		}

		public T Create()
		{
			return Managers.Resource.Instantiate(_prefab);
		}
	}
}
