using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
	public partial class Character : MonoBehaviour
	{
		public class Pool : AAAddressablesPool<Character>
		{
			public Pool(GameObject prefab) : base(prefab)
			{
			}

			protected override Character CreateInstance()
			{
				Character newCharacter = base.CreateInstance();

				newCharacter.OnDieAsObservable().Subscribe(_ => Despawn(_));

				return newCharacter;
			}

			public Character Spawn()
			{
				return Rent();
			}

			public Character Spawn(Vector3 position)
			{
				var character = Rent();

				character.Position = position;

				return character;
			}

			public void Despawn(Character character)
			{
				Return(character);
			}
		}
	}
}
