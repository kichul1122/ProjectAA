using UniRx;
using UnityEngine;

namespace AA
{
	public class CharacterAnimator : MonoBehaviour
	{
		[SerializeField]
		private CharacterAnimancer _animancer;

		private CharacterInput _characterInput;

		private void Start()
		{
			_characterInput = GetComponent<CharacterInput>();
			_animancer = GetComponentInChildren<CharacterAnimancer>();

			Subscribes();
		}

		private void Subscribes()
		{
			var inputObservable = this.ObserveEveryValueChanged(_ => _characterInput.Direction);

			inputObservable.Where(value => Vector3.SqrMagnitude(value) >= 0f).Subscribe(_ =>
			{
				_animancer.Play(ECharacterState.Move);
			}).AddTo(this);

			inputObservable.Where(value => Vector3.SqrMagnitude(value) == 0f).Subscribe(_ =>
			{
				_animancer.Play(ECharacterState.Idle);
			}).AddTo(this);

		}

		public void Play(ECharacterState eCharacterState)
		{
			_animancer.Play(eCharacterState);
		}
	}
}
