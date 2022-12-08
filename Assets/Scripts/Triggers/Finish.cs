using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : TriggerObject
{
    [SerializeField] private string levelNameToLoad;
    public override void OnTriggered(BallController ballController)
    {
        Debug.Log("Finished");
        GameManager.Instance.SceneManager.LoadScene(levelNameToLoad);
        GetComponent<Collider>().enabled = false;
    }
}
