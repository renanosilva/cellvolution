using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class CutsceneController : MonoBehaviour {

	public PlayableDirector cutscene;

	public UnityEvent OnPlay;
	public UnityEvent OnStop;

	public void Play(){
		OnPlay.Invoke();
		cutscene.Play();
		Invoke("Stop", (float)cutscene.duration);
	}
	
	public void Stop(){
		OnStop.Invoke();

	}
}
