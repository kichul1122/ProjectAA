using Drawing;
using UnityEngine;

namespace AA
{
	public class WeaponPivot : MonoBehaviourGizmos
	{
		private Transform _cachedTransform;

		private void Awake()
		{
			_cachedTransform = transform;
		}

		public override void DrawGizmos()
		{
			Draw.SphereOutline(_cachedTransform.position, 0.1f);
		}

	}
}
