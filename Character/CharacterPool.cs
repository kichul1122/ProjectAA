using UniRx;
using UnityEngine;

namespace AA
{
	public class CharacterPool : AAPool<Character>
	{
		public CharacterPool(GameObject prefab) : base(prefab)
		{
		}

		protected override Character CreateInstance()
		{
			Character newCharacter = base.CreateInstance();

			newCharacter.Observable.OnDieAsObservable().Subscribe(_ => Despawn(_));

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
