using Drawing;
using UnityEngine;

namespace AA
{
	public class ProjectilePivot : MonoBehaviourGizmos
	{
		public Vector3 Forward => CachedTransform.forward;
		public Vector3 Position => CachedTransform.position;

		public Transform CachedTransform;

		private void Awake()
		{
			CachedTransform = transform;

		}

		public override void DrawGizmos()
		{
			Draw.SphereOutline(CachedTransform.position, 0.1f);
		}
	}
}
