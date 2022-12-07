using System;
using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	[SerializeField] private float moveForce = 5;
	[SerializeField] private float jumpForce = 2;
	[SerializeField] private float maxAngularVelocity = 15;
	private bool needsTorque = true;
	private const float DistFromGround = 1f;

	private Vector3 camerasDirection;
	private Vector3 action;
	private new Rigidbody rigidbody;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.maxAngularVelocity = maxAngularVelocity;
	}

	private void Start()
	{
		GameManager.Instance.InputController.OnJump += OnJump;
		Camera.main.GetComponent<CameraController>().target = transform;
	}

	void OnJump()
    {
		if (Physics.Raycast(transform.position, -Vector3.up, DistFromGround))
		{
			rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	void FixedUpdate()
	{
		var direction = (GameManager.Instance.InputController.VerticalAxis * Vector3.forward + GameManager.Instance.InputController.HorisontalAxis * Vector3.right).normalized;
		rigidbody.AddTorque(new Vector3(direction.z, 0, -direction.x) * moveForce);
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
		GameManager.Instance.InputController.OnJump -= OnJump;
	}
}