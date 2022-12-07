using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Action OnUpdate;
    public Action OnFixedUpdate;

    [SerializeField] UIController uiController;
    public BallController ballController;

    public static GameManager Instance;
    public InputController InputController { get; private set; }
    public SceneManager SceneManager { get; private set; }
    public UIController UIController { get => uiController; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            return;
        }

        InputController = new InputController();
        SceneManager = GetComponent<SceneManager>();
    }

    private void Start()
    {
        SceneManager.LoadStaringScene();
        //UIController.SetHudVisibility(true);
    }

    void Update()
    {
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
