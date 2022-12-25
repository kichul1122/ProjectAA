using UnityEngine;

namespace AA
{
	public class Weapon : MonoBehaviour
	{
		public ProjectilePivot projectilePivot;
		public Vector3 PivotPosition => projectilePivot.CachedTransform.position;
		AAPool<Projectile> _projectilePool;

		public Transform _projectileForwardTransform;
		private Transform _projectileParent;

		public void Construct(AAPool<Projectile> projectilePool, Transform projectileParent)
		{
			_projectilePool = projectilePool;
			_projectileParent = projectileParent;
		}

		public Weapon SetProjectileForward(Transform projectileForwardTransform)
		{
			_projectileForwardTransform = projectileForwardTransform;

			return this;
		}

		public void Equip()
		{
		}

		public void Launch()
		{
			Projectile spawnedProjectile = _projectilePool.Rent();
			spawnedProjectile.Construct(ReturnProjectile).SetParent(_projectileParent).SetPosition(projectilePivot.Position).SetForward(_projectileForwardTransform.forward);
		}

		public void UnEquip()
		{
		}

		private void Awake()
		{
			projectilePivot = GetComponentInChildren<ProjectilePivot>();
		}

		public void ReturnProjectile(Projectile projectile)
		{
			_projectilePool.Return(projectile);
		}
	}
}
