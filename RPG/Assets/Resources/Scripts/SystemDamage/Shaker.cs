using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	public static Shaker instance;

	public float frameFreezeTime;

	private float power;
	private float duration;
	private bool shouldShake;
	private bool canFreeze;

	private Vector3 startPosition;

	private void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

		startPosition = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

		if (shouldShake)
		{
			if (canFreeze)
			{
				StartCoroutine(FrameFreeze());
			}

			if(duration > 0)
			{
				transform.localPosition = startPosition + (Random.insideUnitSphere * power) * Time.deltaTime;
				duration -= Time.deltaTime;
			}
			else
			{
				canFreeze = true;
				shouldShake = false;
				duration = 0;
				transform.localPosition = startPosition;
			}
		}

	}

	public void SetValues(float powerValue, float durationValue)
	{
		power = powerValue;
		duration = durationValue;
		canFreeze = true;
		shouldShake = true;
	}

	IEnumerator FrameFreeze()
	{
		canFreeze = false;
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(frameFreezeTime);
		Time.timeScale = 1;
	}
}
