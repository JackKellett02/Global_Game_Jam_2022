using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	[Range(1.0f, 30.0f)]
	private float speed = 10.0f;

	[SerializeField]
	[Range(1.1f, 3.0f)]
	private float runningSpeedMultiplier = 1.5f;

	[SerializeField]
	[Range(0.5f, 6.0f)]
	private float jumpHeight = 3.0f;

	[SerializeField]
	[Range(-19.62f, -4.905f)]
	private float gravity = -9.81f;

	[SerializeField]
	private LayerMask groundMask;

	[SerializeField]
	private float groundDistance = 0.4f;

	[SerializeField]
	private Transform groundCheck = null;

	[SerializeField]
	private CharacterController characterController = null;
	#endregion

	#region Private Variable Declarations.
	private Vector2 m_v2Movement = Vector2.zero;
	private bool isRunning = false;

	//Gravity variables.
	private Vector3 velocity = Vector3.zero;
	private bool isGrounded = true;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {
		velocity.y = -2.0f;
	}

	// Update is called once per frame
	void Update() {
		Move(m_v2Movement);
		HandleMovementDueToGravity();
	}

	private void Move(Vector2 a_movement) {
		//Calculate 3 dimensional movement.
		Vector3 movementV3 = transform.right * a_movement.x + transform.forward * a_movement.y;
		movementV3.Normalize();

		//Check if the creature is running.
		float localSpeed = speed;
		if (isRunning) {
			localSpeed *= runningSpeedMultiplier;
		}

		//Calculate the value of movement when taking speed into account.
		movementV3 *= localSpeed * Time.deltaTime;

		//Move the creature.
		characterController.Move(movementV3);
	}

	private void HandleMovementDueToGravity() {
		//Check if the creature is grounded.
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		if (isGrounded && velocity.y < 0.0f) {
			velocity.y = -2.0f;
		}

		//Update velocity.
		velocity.y += gravity * Time.deltaTime;

		//Move the creature by velocity.
		characterController.Move(velocity * Time.deltaTime);
	}
	#endregion

	#region Public Access Variables (Getters and Setters).

	public void InputMovementDirection(Vector2 a_v2Movement) {
		//Set the member movement vector to the argument vector2.
		m_v2Movement = a_v2Movement;

		//Ensure it is normalized.
		m_v2Movement.Normalize();
	}

	public void MakeCreatureJump(bool a_bJump) {
		if (a_bJump && isGrounded) {
			velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
		}
	}

	public void MakeCreatureRun(bool a_bRun) {
		isRunning = a_bRun;
	}
	#endregion
}
