using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AA
{
	public class Projectile : MonoBehaviour
	{
		private System.Action<Projectile> _onReturn;

		public Setting _setting = new();

		private Transform CachedTransform;

		private float _elapsedLifeTime;

		private void Awake()
		{
			this.FixedUpdateAsObservable().Subscribe(_ =>
			{
				Move(Time.fixedDeltaTime);
				UpdateLifeTime();
			}).AddTo(this);

			this.OnCollisionEnterAsObservable().Subscribe(collision =>
			{
				var collisionCharacter = collision.gameObject.GetComponent<Character>();

				if (!collisionCharacter) return;
				if (collisionCharacter.ECharacter == _setting.OwnerECharacter) return;

				OnReturn();

			}).AddTo(this);

			CachedTransform = transform;

			this.UpdateAsObservable().Subscribe(_ =>
			{

			}).AddTo(this);
		}

		private void Move(float fixedDeltaTime)
		{
			CachedTransform.position += _setting.MoveSpeed * fixedDeltaTime * CachedTransform.forward;
		}

		private void UpdateLifeTime()
		{
			_elapsedLifeTime += Time.fixedDeltaTime;

			if (_elapsedLifeTime >= _setting.LifeTime)
			{
				_elapsedLifeTime = 0f;

				OnReturn();
			}
		}

		public Projectile Construct(System.Action<Projectile> onReturn)
		{
			_onReturn = onReturn;

			_elapsedLifeTime = 0f;

			return this;
		}

		public void OnReturn()
		{
			_onReturn?.Invoke(this);
		}

		public Projectile SetSetting(Setting setting)
		{
			_setting = setting;

			return this;
		}

		public Projectile SetStartPosition(Vector3 position)
		{
			CachedTransform.position = position;

			return this;
		}

		public Projectile SetStartForward(Vector3 forward)
		{
			CachedTransform.forward = forward;

			return this;
		}

		[Serializable]
		public class Setting
		{
			public float LifeTime = 3f;
			public float MoveSpeed = 1f;
			public ECharacter OwnerECharacter = ECharacter.None;
		}
	}
}
