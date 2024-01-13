using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectableSignController : MonoBehaviour
{
    [SerializeField] GameObject Sign;
    GameObject currentTarget;
    public void selectable(GameObject target)
    {
        if(currentTarget == target)
        {
            return;
        }
        currentTarget = target;
        Vector3 position = target.transform.position;
        Highlight(position);
    }

    public void Highlight(Vector3 position)
    {
        Sign.SetActive(true);
        Sign.transform.position = position; 
    }

    public void Hide()
    {
        currentTarget = null;
        Sign.SetActive(false);
    }
}
