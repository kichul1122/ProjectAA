using UnityEngine;

namespace AA
{
	public class CharacterSetting : MonoBehaviour
	{
		public CharacterModel Model;

		public CharacterMove.Setting Move;

		public CharacterSetting Construct(CharacterModel characterModel)
		{
			Model = characterModel;

			Move = new CharacterMove.Setting();

			return this;
		}
	}
}
