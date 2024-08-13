using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLoader : MonoBehaviour
{
  public LevelLoader levelLoader;

  public void CarregarFade()
  {
    StartCoroutine(AtivarAnimacao());
  }

   IEnumerator AtivarAnimacao()
  {
    yield return new WaitForSeconds(4f);
    levelLoader.Transition("Creditos");
  }
}
