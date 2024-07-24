using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    public Transform Target;
    public float HideDistance;

    public List<GameObject> celulas;

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

        if(gameObject == null){
           foreach(GameObject celula in celulas ){
                if(celula.activeSelf == true){
                    NextTarget(celula.transform);
                }
           }
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
        Target = state;
    }
}
