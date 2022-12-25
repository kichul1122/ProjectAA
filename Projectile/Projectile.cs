using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AA
{
	public class Projectile : MonoBehaviour
	{
		private Subject<Projectile> _onRemoveSubject;

		private System.Action<Projectile> _onReturn;

		private Rigidbody _rigidbody;
		public Setting _setting = new();

		private Transform CachedTransform;

		private Transform _projectileForwardTransform;

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

			_rigidbody = GetComponent<Rigidbody>();
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

		public Projectile Construct(System.Action<Projectile> onReturn, Setting setting = default)
		{
			_onReturn = onReturn;

			if (setting != default)
			{
				_setting = setting;
			}

			_elapsedLifeTime = 0f;

			return this;
		}

		public void OnReturn()
		{
			_onReturn?.Invoke(this);
		}

		public Projectile SetPosition(Vector3 position)
		{
			CachedTransform.position = position;

			return this;
		}

		public Projectile SetForward(Vector3 forward)
		{
			CachedTransform.forward = forward;

			return this;
		}

		public Projectile SetParent(Transform projectileParent)
		{
			CachedTransform.SetParent(projectileParent);

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
