using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLoader : MonoBehaviour
{
    public Animator anim;
    public LevelLoader levelLoader;

    public void CarregarFade()
    {
        StartCoroutine(AtivarAnimacao());
    }

    IEnumerator AtivarAnimacao()
    {
        anim.Play("Fade_Morte");
        yield return new WaitForSeconds(4f);
        CheckpointManager.instance.Load();
        levelLoader.Transition("Menu");
    }
}
