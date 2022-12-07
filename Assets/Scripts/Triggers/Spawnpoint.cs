using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnpoint : TriggerObject
{
    // Start is called before the first frame update
    void Start()
    {
        var ball = Instantiate(GameManager.Instance.ballController, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ball.gameObject, SceneManager.GetSceneByName(GameManager.Instance.SceneManager.currentScene));
    }
}
