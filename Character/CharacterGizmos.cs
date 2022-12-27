using Drawing;
using System.Linq;
using Unity.Mathematics;
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

		private (float3 start, float3 end, float radius) _capsule;

		private void Awake()
		{
			_capsuleCollider = GetComponent<CapsuleCollider>();
			_cachedTransform = transform;
			_characterRotator = GetComponentInChildren<CharacterRotator>();

			_height = GetComponentsInChildren<Renderer>().Max(_ => _.bounds.max.y);

			_radius = _capsuleCollider.radius;
			_arcRadius = _radius + _arcRadiusOffset;

			_capsule = _capsuleCollider.GetCapsule();
		}

		public override void DrawGizmos()
		{
			using (Draw.InLocalSpace(_cachedTransform))
			{
				Draw.WireCapsule(_capsule.start, _capsule.end, _capsule.radius);
			}

			Draw.WireCylinder(_cachedTransform.position, _cachedTransform.position + Vector3.up * _height, _radius);

			Draw.ArrowheadArc(_cachedTransform.position, _characterRotator.Forward, _arcRadius);
		}
	}


}
