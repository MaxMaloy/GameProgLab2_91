using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTypeSwitcher : TriggerObject
{
    [SerializeField] private BallType type;
    public override void OnTriggered(BallController ballController)
    {
        ballController.SetBallType(type);
    }
}