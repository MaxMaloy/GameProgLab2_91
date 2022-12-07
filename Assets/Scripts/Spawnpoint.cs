using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : TriggerObject
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(GameManager.Instance.ballController, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
    }
}
