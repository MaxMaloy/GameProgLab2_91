using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    private void Awake()
    {
        gameObject.tag = "Trigger";
    }

    public virtual void OnTriggered()
    {
        Debug.Log("Triggered");
    }
}
