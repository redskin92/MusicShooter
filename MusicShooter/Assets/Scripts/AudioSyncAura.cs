using UnityEngine;

public class AudioSyncAura : MonoBehaviour
{
	[SerializeField] private GameObject audioVisualizerObject;
	[SerializeField] private float auraRadius = 3;
	[SerializeField] private Vector3 scaleMultipler = Vector3.up;
	private AuraObject[] spectrumObjects;

	private Vector3 startingScale;

	private void Awake()
	{
		spectrumObjects = new AuraObject[64];


		float angleSpacing = (2f * Mathf.PI) / 64;

		startingScale = audioVisualizerObject.transform.localScale;

		for (int i = 0; i < 64; ++i)
		{
			var auraObj = Instantiate(audioVisualizerObject, transform);

			float angle = i * angleSpacing;
			float x = Mathf.Sin(angle) * auraRadius;
			float y = Mathf.Cos(angle) * auraRadius;

			auraObj.gameObject.transform.localPosition = new Vector3(x, y, 0);


			Vector2 dir = transform.position - auraObj.transform.position;
			float fuckinwork = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;


			auraObj.transform.rotation = Quaternion.AngleAxis(fuckinwork, Vector3.forward);

			spectrumObjects[i] = new AuraObject(auraObj);
		}

	}


	private void Update()
	{
		for(int i = 0; i < 64; ++i)
		{
			float bandValue = AudioSpectrum.GetBandValue(i);

			spectrumObjects[i].gameObject.transform.localScale = startingScale + (scaleMultipler * bandValue);
			spectrumObjects[i].UpdateColor(bandValue);
		}
	}


	private class AuraObject
	{
		public AuraObject(GameObject auraGO)
		{
			gameObject = auraGO;
			spriteRenderer = auraGO.GetComponent<SpriteRenderer>();
		}

		public void UpdateColor(float bandValue)
		{
			if (previousValue < bandValue && bandValue > 0.05f)
				stickTime = 0.05f;

			spriteRenderer.color = stickTime >= 0f ? Color.red : Color.white;
			previousValue = bandValue;

			if(stickTime >= 0f)
				stickTime -= Time.deltaTime;
		}

		public GameObject gameObject;

		private SpriteRenderer spriteRenderer;
		private float previousValue;

		private float stickTime;
	}

}
