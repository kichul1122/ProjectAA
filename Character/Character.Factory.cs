using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
    public partial class Character : MonoBehaviour
    {
        public static Factory DefaultFactory = new();

        public class Factory
        {
            public async UniTask<Character> CreateAsync(string prefabPath, CharacterModel characterModel, UnityEngine.Component owner)
            {
                var characterPrefabGO = await Managers.Resource.LoadPrefabAsync(prefabPath, owner);

                var newCharacterGO = Managers.Resource.Instantiate(characterPrefabGO);

                Character newCharacter = newCharacterGO.GetComponent<Character>();

                newCharacter.Construct(characterModel);

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
}
