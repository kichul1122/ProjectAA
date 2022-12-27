using Drawing;
using System.Linq;
using UnityEngine;

namespace AA
{
	public class CharacterGizmos : MonoBehaviourGizmos
	{
		private CapsuleCollider _capsuleCollider;
		private Transform _cachedTransform;
		private CharacterRotator _characterRotator;

		private float _radius;
		private float _arcRadius;
		private float _arcRadiusOffset = 0.1f;

		private float _height;

		private void Awake()
		{
			_capsuleCollider = GetComponent<CapsuleCollider>();
			_cachedTransform = transform;
			_characterRotator = GetComponentInChildren<CharacterRotator>();

			_height = GetComponentsInChildren<Renderer>().Max(_ => _.bounds.max.y);

			_radius = _capsuleCollider.radius;
			_arcRadius = _radius + _arcRadiusOffset;
		}

		public override void DrawGizmos()
		{
			using (Draw.InLocalSpace(_cachedTransform))
			{
				var capsule = _capsuleCollider.GetCapsule();
				Draw.WireCapsule(capsule.start, capsule.end, capsule.radius, Color.black);
			}

			Draw.WireCylinder(_cachedTransform.position, _cachedTransform.position + Vector3.up * _height, _radius, Color.black);

			Draw.ArrowheadArc(_cachedTransform.position, _characterRotator.Forward, _arcRadius, Color.black);
		}
	}


}
