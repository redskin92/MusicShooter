using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
	private int SpectrumCount = 512;

	float[] _Samples;
	float[] _SampleBuffer;

	public float _SmoothDownRate = 0;
	public float _Scalar = 1;

	public bool _DrawGizmos = false;

	[HideInInspector]
	private static float[] _FreqBands64;

	// Start is called before the first frame update
	void Start()
	{
		_FreqBands64 = new float[64];
		_Samples = new float[SpectrumCount];
		_SampleBuffer = new float[SpectrumCount];
	}

	void UpdateFreqBands64()
	{
		// 22050 / 512 = 43hz per sample
		// 10 - 60 hz
		// 60 - 250
		// 250 - 500
		// 500 - 2000
		// 2000 - 4000
		// 4000 - 6000
		// 6000 - 20000

		int count = 0;
		int sampleCount = 1;
		int power = 0;

		for (int i = 0; i < 64; i++)
		{
			float average = 0;

			if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
			{
				power++;
				sampleCount = (int)Mathf.Pow(2, power);
				if (power == 3)
					sampleCount -= 2;
			}

			for (int j = 0; j < sampleCount; j++)
			{
				average += _Samples[count] * (count + 1);
				count++;
			}

			average /= count;
			_FreqBands64[i] = average;
		}
	}

	// Update is called once per frame
	void Update()
	{
		AudioListener.GetSpectrumData(_SampleBuffer, 0, FFTWindow.Hamming);

		for (int i = 0; i < _Samples.Length; i++)
		{
			if (_SampleBuffer[i] > _Samples[i])
				_Samples[i] = _SampleBuffer[i];
			else
				_Samples[i] = Mathf.Lerp(_Samples[i], _SampleBuffer[i], Time.deltaTime * _SmoothDownRate);
		}

		UpdateFreqBands64();
	}

	public static float GetBandValue(int index)
	{
		return _FreqBands64[index];
	}

	private void OnDrawGizmos()
	{
		if (_DrawGizmos && Application.isPlaying)
		{
			float linearXPos0;
			float linearXPos1;

			for (int i = 1; i < 63; i++)
			{
				linearXPos0 = (float)(i - 1) / 63f;
				linearXPos1 = (float)(i) / 63f;

				Gizmos.color = Color.white;
				Gizmos.DrawLine(transform.position + new Vector3(linearXPos0 * 4, _FreqBands64[i - 1] * _Scalar, 0), transform.position + new Vector3(linearXPos1 * 4, _FreqBands64[i] * _Scalar, 0));
			}
		}
	}

}