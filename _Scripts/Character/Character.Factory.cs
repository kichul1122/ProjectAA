using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
    public partial class Character : MonoBehaviour
    {
        public class Factory
        {
            private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

            public async UniTask<Character> CreateAsync(string prefabPath, CharacterData characterData, UnityEngine.Component owner)
            {
                if (!_prefabs.ContainsKey(prefabPath))
                {
                    var characterPrefabGO = await Managers.Resource.LoadPrefabAsync(prefabPath, owner);

                    _prefabs.Add(prefabPath, characterPrefabGO);
                }

                var newCharacterGO = Managers.Resource.Instantiate(_prefabs[prefabPath]);
                Character newCharacter = newCharacterGO.GetComponent<Character>();

                newCharacter.Construct(characterData);

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
