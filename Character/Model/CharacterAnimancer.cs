using Animancer;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace AA
{
	public class CharacterAnimancer : SerializedMonoBehaviour
	{
		[SerializeField]
		private AnimancerComponent _animancer;

		[SerializeField]
		private Dictionary<ECharacterState, ClipTransition> _transition = new Dictionary<ECharacterState, ClipTransition>();

		[Button]
		private void Awake()
		{
			_animancer = GetComponent<AnimancerComponent>();
		}

		[Button]
		public void Play(ECharacterState eCharacterState)
		{
			if (_transition.TryGetValue(eCharacterState, out ClipTransition transition))
			{
				_animancer.Play(transition);
			}
		}
	}
}
