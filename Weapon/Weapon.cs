using UnityEngine;

namespace AA
{
	public class Weapon : MonoBehaviour
	{
		public ProjectilePivot Pivot;
		public Vector3 PivotPosition => Pivot.CachedTransform.position;
		AAPool<Projectile> _projectilePool;

		public void Construct(AAPool<Projectile> projectilePool)
		{
			_projectilePool = projectilePool;
		}

		public void Equip()
		{
		}

		public void Launch()
		{
			Projectile spawnedProjectile = _projectilePool.Rent();
			spawnedProjectile.Construct(ReturnProjectile, new Projectile.Setting()).SetVelocity(Pivot.CachedTransform.forward);
		}

		public void UnEquip()
		{
		}

		private void Awake()
		{
			Pivot = GetComponentInChildren<ProjectilePivot>();
		}

		public void ReturnProjectile(Projectile projectile)
		{
			_projectilePool.Return(projectile);
		}
	}
}
