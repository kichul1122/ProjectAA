using Drawing;
using UnityEngine;

namespace AA
{
	public class ColliderGizmos : MonoBehaviourGizmos
	{
		private Collider _collider;
		private Transform _cachedTransform;

		private System.Action _onDrawGizmos;

		private void Awake()
		{
			_cachedTransform = transform;
			_collider = GetComponent<Collider>();

			_onDrawGizmos = GetDrawGizmos();
		}


		public override void DrawGizmos()
		{
			using (Draw.InLocalSpace(_cachedTransform))
			{
				_onDrawGizmos?.Invoke();
			}
		}

		private System.Action GetDrawGizmos()
		{
			switch (_collider)
			{
				case SphereCollider sphereCollider:
					{
						return () => Draw.WireSphere(sphereCollider.center, sphereCollider.radius);
					}
				case CapsuleCollider capsuleCollider:
					{
						var capsule = capsuleCollider.GetCapsule();
						return () => Draw.WireCapsule(capsule.start, capsule.end, capsule.radius);
					}
			}

			return default;
		}
	}
}
