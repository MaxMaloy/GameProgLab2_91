using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class BallController : MonoBehaviour
{
	//state
	private List<IBallParameters> ballParameters = new List<IBallParameters>() { new StoneBallParameters(), new WoodenBallParameters() };
	private IBallParameters currentBallParams = new WoodenBallParameters();
	[SerializeField] private Material woodenMaterial;
	[SerializeField] private Material stoneMaterial;
	//state

	[SerializeField] private float maxAngularVelocity = 15;
	private const float distFromGround = 1f;

	private new Rigidbody rigidbody;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.maxAngularVelocity = maxAngularVelocity;
		GameManager.Instance.OnFixedUpdate += FixedUpd;
	}

	private void Start()
	{
		GameManager.Instance.InputController.OnJump += Jump;
		var cameraController = Camera.main.GetComponent<CameraController>();
		cameraController.target = transform;
		cameraController.isFollowing = true;
	}

	public void SetBallType(BallType ballType)
    {
		currentBallParams = ballParameters.First(x => x.Type == ballType);
		GetComponent<Rigidbody>().mass = currentBallParams.Mass;

		switch (ballType)
        {
            case BallType.wooden:
				GetComponent<MeshRenderer>().material = woodenMaterial;
				break;
            case BallType.stone:
				GetComponent<MeshRenderer>().material = stoneMaterial;
				break;
        }
	}

	void Jump()
    {
		if (Physics.Raycast(transform.position, -Vector3.up, distFromGround))
		{
			rigidbody.AddForce(Vector3.up * currentBallParams.JumpForce, ForceMode.Impulse);
		}
	}

	void FixedUpd()
	{
		var direction = (GameManager.Instance.InputController.VerticalAxis * Vector3.forward + GameManager.Instance.InputController.HorisontalAxis * Vector3.right).normalized;
		if (Physics.Raycast(transform.position, -Vector3.up, 0.55f)) //if on ground
		{
			rigidbody.AddTorque(new Vector3(direction.z, 0, -direction.x) * currentBallParams.MoveForce);
        }
        else
        {
			rigidbody.AddForce(new Vector3(direction.z, 0, -direction.x) * currentBallParams.MoveForce);
		}

		if (!Physics.Raycast(transform.position, -Vector3.up, 100f)) //if falling
		{
			Debug.Log("Play falling anim and restart level");
			GameManager.Instance.OnFixedUpdate -= FixedUpd;
			Sequence sequence = DOTween.Sequence();
			sequence.InsertCallback(.5f, ()=>Camera.main.GetComponent<CameraController>().isFollowing = false);
			sequence.InsertCallback(3f, GameManager.Instance.SceneManager.ReloadScene);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Trigger")
		{
			collider.gameObject.GetComponent<TriggerObject>().OnTriggered(this);
		}
	}

    private void OnDestroy()
    {
		GameManager.Instance.InputController.OnJump -= Jump;
		GameManager.Instance.OnFixedUpdate -= FixedUpd;
	}
}