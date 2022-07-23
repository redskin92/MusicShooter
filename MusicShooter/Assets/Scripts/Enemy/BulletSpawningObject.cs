using UnityEngine;

public class BulletSpawningObject : MonoBehaviour
{
	private float fireCooldownValue = 1f;

	private float fireCDTimer = 0f;
	private float previousValue;

	[SerializeField] private Projectile projectilePrefab;

	public void UpdateViaBandValue(float bandVal, float deltaTime)
	{
		fireCDTimer -= deltaTime;

		if (previousValue < bandVal && bandVal > 0.05f)
		{
			Fire();
		}

		previousValue = bandVal;
	}

	private void Fire()
	{
		if(fireCDTimer <= 0f)
		{
			// FIRE

			var projectile = Instantiate(projectilePrefab, transform);
			projectile.transform.forward = this.transform.forward;

			fireCDTimer = fireCooldownValue;
		}
	}
}
