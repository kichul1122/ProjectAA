using Animancer;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
	public class CharacterAnimancer : SerializedMonoBehaviour
	{
		[SerializeField]
		private AnimancerComponent _animancer;

		[SerializeField]
		private Dictionary<ECharacterState, ClipTransition> _transition = new Dictionary<ECharacterState, ClipTransition>();

		private CharacterInput _characterInput;

		[Button]
		private void Start()
		{
			_characterInput = GetComponent<CharacterInput>();
			_animancer = GetComponentInChildren<AnimancerComponent>();

			Subscribes();
		}

		private void Subscribes()
		{
			var inputObservable = this.ObserveEveryValueChanged(_ => _characterInput.Direction);

			inputObservable.Where(value => Vector3.SqrMagnitude(value) >= 0f).Subscribe(_ =>
			{
				_animancer.Play(_transition[ECharacterState.Move]);
			}).AddTo(this);

			inputObservable.Where(value => Vector3.SqrMagnitude(value) == 0f).Subscribe(_ =>
			{
				_animancer.Play(_transition[ECharacterState.Idle]);
			}).AddTo(this);

		}

		[Button]
		public void Play(ECharacterState eCharacterState)
		{
			_animancer.Play(_transition[eCharacterState]);
		}
	}
}
