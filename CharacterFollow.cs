using MessagePipe;
using System;
using UnityEngine;

namespace AA
{
	public class CharacterFollow : MonoBehaviour
	{
		private ISubscriber<TargetTransformMsg> _subscriber;
		private IDisposable _disposable;

		private Transform _cachedTransform;
		private Transform _targetTransform;

		public struct TargetTransformMsg
		{
			public Transform Value;

			public TargetTransformMsg(Transform value)
			{
				Value = value;
			}
		}

		private void Awake()
		{
			_subscriber = Managers.MessagePipe.GetSubscriber<TargetTransformMsg>();

			_cachedTransform = transform;

			var d = DisposableBag.CreateBuilder();

			_subscriber.Subscribe(targetTransformEvent =>
			{
				_targetTransform = targetTransformEvent.Value;

			}).AddTo(d);

			_disposable = d.Build();
		}

		private void Update()
		{
			if (_targetTransform == null) return;

			_cachedTransform.position = _targetTransform.position;
		}

		private void OnDestroy()
		{
			_disposable?.Dispose();
		}
	}
}
