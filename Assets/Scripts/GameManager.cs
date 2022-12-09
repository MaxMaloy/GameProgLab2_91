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
    public GameSceneManager SceneManager { get; private set; }
    public UIController UIController { get => uiController; }
    public InterstitialAd Ads { get; private set; }

    private int livesCount = 3;
    public int LivesCount
    {
        get => livesCount;
        set
        {
            livesCount = Math.Clamp(value, 0, 3);
            UIController.SetLivesCount(value);
            if (livesCount == 0)
            {
                OnLoose();
            }
        }
    }

    private void OnLoose()
    {
        Debug.Log("You loose haha");
        UIController.SetLoosePopupVisibility(true);
    }

    private int coinsCount;
    public int CoinsCount { 
        get => coinsCount; 
        set 
        {
            coinsCount = value;
            if (coinsCount == 10)
            {
                LivesCount++;
                CoinsCount = 0;
            }
            UIController.SetCoinsCount(value);
        } 
    }

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
        Ads = GetComponent<InterstitialAd>();
        InputController = new InputController();
        SceneManager = GetComponent<GameSceneManager>();
        Ads.OnAdsShowComplete += SceneManager.ReloadScene;
    }

    private void Start()
    {
        SceneManager.LoadStaringScene();
        UIController.SetHudVisibility(true);
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
