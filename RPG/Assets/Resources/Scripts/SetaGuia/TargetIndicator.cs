using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    public Transform Target;
    public float HideDistance;

    public List<Transform> celulas;

    private void Update() {
        var dir = Target.position - transform.position;

        if (dir.magnitude < HideDistance)
        {
            SetChildrenActive(false);
        }
        else
        {
            SetChildrenActive(true);

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        
    }

    private void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    public void NextTarget(Transform state)
    {

        Transform novaPosicao = state;

        foreach (Transform celula in celulas)
        {
            if(celula.name.Equals(state.name))
            {
                Debug.Log(celula.name + " = " + state.name);

                if(celula.gameObject.activeSelf == false)
                {
                    Debug.Log(celula.name + " desativada");
                    Dialogue1 dialogue = celula.GetComponent<Dialogue1>();
                    novaPosicao = celulas[dialogue.indiceCheckpoint + 1].transform;


                }
            }
        }
        Target = novaPosicao;
    }
}
