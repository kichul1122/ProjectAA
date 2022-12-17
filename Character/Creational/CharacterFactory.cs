using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
	public class CharacterFactory
	{
		public static CharacterFactory Default = new();

		public async UniTask<Character> CreateAsync(string prefabPath, CharacterModel characterModel, UnityEngine.Component owner)
		{
			var newCharacterGO = await Managers.Resource.InstantiateAsync(prefabPath, owner);

			Character newCharacter = newCharacterGO.GetComponent<Character>();

			newCharacter.Construct();

			return newCharacter;
		}

		public Character Create(Character prefab)
		{
			Character newCharacter = Managers.Resource.Instantiate(prefab);

			return newCharacter;
		}

		public Character Create(Character prefab, Vector3 position)
		{
			Character newCharacter = Managers.Resource.Instantiate(prefab);
			newCharacter.Position = position;
			return newCharacter;
		}
	}
}
