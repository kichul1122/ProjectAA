using MessagePipe;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AA
{
	public class InputManager : ManagerMonobehaviour
	{
		private IPublisher<HorizontalMsg> _horizontalPublisher;
		private IPublisher<VerticalMsg> _verticalPublisher;
		private IPublisher<DirectionMsg> _directionPublisher;

		private readonly ReactiveProperty<bool> _jumpButton = new ReactiveProperty<bool>(false);
		public IReadOnlyReactiveProperty<bool> JumpButton => _jumpButton;

		private void Start()
		{
			_horizontalPublisher = Managers.MessagePipe.GetPublisher<HorizontalMsg>();
			_verticalPublisher = Managers.MessagePipe.GetPublisher<VerticalMsg>();
			_directionPublisher = Managers.MessagePipe.GetPublisher<DirectionMsg>();

			this.UpdateAsObservable().Select(_ => Horizontal)
				.Subscribe(value => _horizontalPublisher.Publish(new HorizontalMsg(value))).AddTo(this);

			this.UpdateAsObservable().Select(_ => Vertical)
				.Subscribe(value => _verticalPublisher.Publish(new VerticalMsg(value))).AddTo(this);

			this.UpdateAsObservable().Select(_ => Direction)
				.Subscribe(value => _directionPublisher.Publish(new DirectionMsg(value))).AddTo(this);

		}

		public Vector3 Direction => new Vector3(Horizontal, 0f, Vertical);

		public float Horizontal => UnityEngine.Input.GetAxisRaw(AAString.Horizontal);
		public float Vertical => UnityEngine.Input.GetAxisRaw(AAString.Vertical);

		public bool Jump => UnityEngine.Input.GetButton(AAString.Jump);

		public readonly struct HorizontalMsg
		{
			public float Value { get; }

			public HorizontalMsg(float value)
			{
				Value = value;
			}
		}

		public readonly struct VerticalMsg
		{
			public float Value { get; }

			public VerticalMsg(float value)
			{
				Value = value;
			}
		}

		public readonly struct DirectionMsg
		{
			public Vector3 Value { get; }

			public DirectionMsg(Vector3 value)
			{
				Value = value;
			}
		}
	}
}
