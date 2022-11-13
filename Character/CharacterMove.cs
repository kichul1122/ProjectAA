using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AA
{
	public class CharacterMove : MonoBehaviour
	{
		[System.Serializable]
		public class Setting
		{
			public float JumpForce = 10f;
			public float MoveSpeed;
		}

		[SerializeField]
		private Setting _setting;

		private readonly ReactiveProperty<bool> _isGrounded = new ReactiveProperty<bool>(false);
		private readonly Subject<Unit> _jumpSubject = new Subject<Unit>();

		[SerializeField]
		private LayerMask _groundLayerMask;
		private Rigidbody _rigidbody;

		//private IReadOnlyReactiveProperty<bool> _jumpButton;

		private void Awake()
		{
			var setting = GetComponent<CharacterSetting>();

			Construct(setting != null ? setting.Move : _setting);
		}

		public void Construct(Setting setting)
		{
			_setting = setting;

			//var characterInput = GetComponent<CharacterInput>();
			//_jumpButton = characterInput.JumpButton;

			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Start()
		{
			_isGrounded.AddTo(this);
			_jumpSubject.AddTo(this);

			this.FixedUpdateAsObservable()
				.Subscribe(_ =>
				{
					_isGrounded.Value = Physics.SphereCast(origin: transform.position + Vector3.up * 0.04f,
						radius: 0.02f,
						direction: Vector3.down, hitInfo: out var _, maxDistance: 0.05f, _groundLayerMask);
				})
				.AddTo(this);

			//_jumpButton
			//	.Where(x => x && _isGrounded.Value)
			//	.ObserveOnMainThread(MainThreadDispatchType.FixedUpdate) //Input은 업데이트, 다음 FixedUpdate에서 실행
			//	.Subscribe(_ =>
			//	{
			//		Jump();
			//	})
			//	.AddTo(this);
		}

		private void Jump()
		{
			_rigidbody.AddForce(Vector3.up * _setting.JumpForce, ForceMode.VelocityChange);

			_jumpSubject.OnNext(Unit.Default);
		}
	}
}
