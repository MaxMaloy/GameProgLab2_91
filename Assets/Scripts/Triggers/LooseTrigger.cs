using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseTrigger : TriggerObject
{
    public override void OnTriggered(BallController ballController)
    {
		Debug.Log("Play falling anim and restart level");
		GameManager.Instance.LivesCount--;
		Sequence sequence = DOTween.Sequence();
		sequence.InsertCallback(.5f, () => Camera.main.GetComponent<CameraController>().isFollowing = false);
		if (GameManager.Instance.LivesCount > 0)
		{
			sequence.InsertCallback(3f, GameManager.Instance.SceneManager.ReloadScene);
		}
	}
}
