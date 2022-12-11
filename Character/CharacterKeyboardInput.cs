using MessagePipe;
using System;

namespace AA
{
	public class CharacterKeyboardInput : CharacterInput
	{
		private IDisposable _disposable;

		private void Start()
		{
			var directionPublisher = Managers.MessagePipe.GetSubscriber<InputManager.DirectionMsg>();

			var d = DisposableBag.CreateBuilder();

			directionPublisher.Subscribe(msg => Direction = msg.Value).AddTo(d);

			_disposable = d.Build();
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}
