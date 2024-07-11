using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject bulletImpact;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject newImpact = Instantiate(bulletImpact, transform.position, transform.rotation);
		Destroy(newImpact, 1);
		Destroy(gameObject);
	}
}
