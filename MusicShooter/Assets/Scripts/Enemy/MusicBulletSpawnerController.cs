using System.Collections.Generic;
using UnityEngine;

public class MusicBulletSpawnerController : MonoBehaviour
{
	[SerializeField] private BulletSpawningObject objectToSpawn;
	private BulletSpawningObject[] bulletSpawningObjects;

	private int numberOfBulletObjects = 64;
	[SerializeField] private float spawningRadius = 40;

	private void Awake()
	{
		bulletSpawningObjects = GetComponentsInChildren<BulletSpawningObject>();
		if (bulletSpawningObjects.Length != numberOfBulletObjects)
			BuildObjects();
	}

	private void Update()
	{
		for (int i = 0; i < numberOfBulletObjects; ++i)
		{
			bulletSpawningObjects[i].UpdateViaBandValue(AudioSpectrum.GetBandValue(i), Time.deltaTime);
		}
	}

	public void BuildObjects()
	{
		bulletSpawningObjects = GetComponentsInChildren<BulletSpawningObject>();

		foreach(var bulletObj in bulletSpawningObjects)
		{
			DestroyImmediate(bulletObj.gameObject);
		}

		bulletSpawningObjects = new BulletSpawningObject[numberOfBulletObjects];


		float angleSpacing = (2f * Mathf.PI) / numberOfBulletObjects;

		for (int i = 0; i < numberOfBulletObjects; ++i)
		{
			var bulletSpawner = Instantiate(objectToSpawn, transform);
			bulletSpawningObjects[i] = bulletSpawner;

			float angle = i * angleSpacing;
			float x = Mathf.Sin(angle) * spawningRadius;
			float y = Mathf.Cos(angle) * spawningRadius;

			bulletSpawner.gameObject.transform.localPosition = new Vector3(x, y, 0);


			Vector2 dir = bulletSpawner.transform.position - transform.position;
			float fuckinwork = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;

			bulletSpawner.transform.rotation = Quaternion.AngleAxis(fuckinwork, Vector3.forward);
		}
	}


	private class AudioBandObject
	{
		bool fired;

	}
}
