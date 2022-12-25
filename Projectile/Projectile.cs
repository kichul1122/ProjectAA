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
		private Setting _setting = new();

		private void Awake()
		{
			//this.FixedUpdateAsObservable().Subscribe(_ =>
			//{
			//	//Move(Time.fixedDeltaTime);
			//}).AddTo(this);

			this.OnCollisionEnterAsObservable().Subscribe(collision =>
			{
				var collisionCharacter = collision.gameObject.GetComponent<Character>();

				if (!collisionCharacter) return;
				if (collisionCharacter.ECharacter == _setting.OwnerECharacter) return;

				OnReturn();

			}).AddTo(this);

			_rigidbody = GetComponent<Rigidbody>();
		}

		public Projectile Construct(System.Action<Projectile> onReturn, Setting setting)
		{
			_onReturn = onReturn;
			_setting = setting;

			return this;
		}

		public void OnReturn()
		{
			_onReturn?.Invoke(this);
		}

		public Projectile SetVelocity(Vector3 velocity)
		{
			_rigidbody.velocity = velocity * _setting.MoveSpeed;

			return this;
		}

		[Serializable]
		public class Setting
		{
			public float MoveSpeed = 1f;
			public ECharacter OwnerECharacter = ECharacter.None;
		}
	}
}
