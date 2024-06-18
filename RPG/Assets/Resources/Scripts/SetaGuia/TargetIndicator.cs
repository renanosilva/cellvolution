﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    public Transform Target;
    public float HideDistance;

    private GameObject celula4;
    private GameObject celula5;

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

        if(Target.gameObject.activeSelf == false){
            celula4 = GameObject.Find("Celula#4");
            NextTarget(celula4.transform);
            if(celula4 == null){
                celula5 = GameObject.Find("Celula#5");
                NextTarget(celula5.transform);
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
