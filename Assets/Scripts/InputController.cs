using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum InputModeType
{
    Blocked,
    Gameplay,
    UI
}

public class InputController
{
    public Action OnJump;
    public float HorisontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }
    private EventSystem eventSystem;

    private InputModeType inputModeType;
    public InputModeType InputModeType
    {
        get => inputModeType;
        set 
        { 
            inputModeType = value;
            eventSystem.enabled = value == InputModeType.UI; //enables ui raycasts
        }
    }

    public InputController()
    {
        GameManager.Instance.OnUpdate += Update;
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        HorisontalAxis = 0;
        VerticalAxis = 0;
        if (InputModeType != InputModeType.Gameplay) return;

        HorisontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.UIController.ToggleMenuVisibility();
        }
    }

    ~InputController()
    {
        GameManager.Instance.OnUpdate -= Update;
    }
}
