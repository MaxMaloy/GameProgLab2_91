using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BallController : MonoBehaviour
{
	[SerializeField] private float moveForce = 5;
	[SerializeField] private float jumpForce = 2;
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

	void Jump()
    {
		if (Physics.Raycast(transform.position, -Vector3.up, distFromGround))
		{
			rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	void FixedUpd()
	{
		var direction = (GameManager.Instance.InputController.VerticalAxis * Vector3.forward + GameManager.Instance.InputController.HorisontalAxis * Vector3.right).normalized;
		if (Physics.Raycast(transform.position, -Vector3.up, 0.55f)) //if on ground
		{
			rigidbody.AddTorque(new Vector3(direction.z, 0, -direction.x) * moveForce);
        }
        else
        {
			rigidbody.AddForce(new Vector3(direction.z, 0, -direction.x) * moveForce);
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
			collider.gameObject.GetComponent<TriggerObject>().OnTriggered();
		}
	}

    private void OnDestroy()
    {
		GameManager.Instance.InputController.OnJump -= Jump;
		GameManager.Instance.OnFixedUpdate -= FixedUpd;
	}
}