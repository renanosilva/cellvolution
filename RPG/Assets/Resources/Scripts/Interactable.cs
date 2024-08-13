using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

	public bool singleUse;

	public UnityEvent OnTrigger;
	public UnityEvent OnExit;

	private bool used;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player")){
			Debug.Log("Interagindo");
			if (used)
				return;

			if (singleUse)
				used = true;

			OnTrigger.Invoke();
		}
		}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("Saindo");
		if (singleUse)
			return;

		OnExit.Invoke();

	}
}
