using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

	public int power = 1;

	[Header("Shake Values")]
	public float powerValue = 50;
	public float duration = 0.5f;

	private Shaker shaker;


	private void Start(){

		shaker = GetComponent<Shaker>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		damageable damageable = other.GetComponent<damageable>();
		if(damageable != null && !damageable.CompareTag("Enime"))
		{
			damageable.TakeDamage(power, transform.position.x);
			 if(shaker != null){

				Shaker.instance.SetValues(powerValue, duration);
			}
		}

        if (damageable != null && damageable.CompareTag("Enime"))
        {
            damageable.TakeDamage(power, transform.position.x);
            if (shaker != null)
            {

                Shaker.instance.SetValues(powerValue, duration);
            }
        }

    }
}
