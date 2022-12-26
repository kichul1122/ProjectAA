using UnityEngine;

namespace AA
{
	public class Weapon : MonoBehaviour
	{
		public ProjectilePivot projectilePivot;

		public Transform CachedTrasnform { get; private set; }

		public Vector3 PivotPosition => projectilePivot.CachedTransform.position;
		AAPool<Projectile> _projectilePool;

		public void Construct(AAPool<Projectile> projectilePool)
		{
			_projectilePool = projectilePool;
		}

		public void Equip()
		{
		}

		public void Launch(Vector3 startForward)
		{
			Projectile newProjectile = _projectilePool.Rent();
			newProjectile.Construct(ReturnProjectile)
				.SetStartPosition(projectilePivot.Position)
				.SetStartForward(startForward);
		}

		public void UnEquip()
		{
		}

		private void Awake()
		{
			projectilePivot = GetComponentInChildren<ProjectilePivot>();
			CachedTrasnform = transform;
		}

		public void ReturnProjectile(Projectile projectile)
		{
			_projectilePool.Return(projectile);
		}

		public void SetPivot(WeaponPivot weaponPivot)
		{
			CachedTrasnform.SetParent(weaponPivot.transform, false);
		}
	}
}
