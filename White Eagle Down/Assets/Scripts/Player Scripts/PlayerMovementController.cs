using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	private MovementScript playerMovementScript = null;
	#endregion

	#region Private Variable Declarations.
	private Vector2 m_movementVector2 = Vector2.zero;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		//Take input.
		m_movementVector2 = HandleMovementInput();

		//Pass it to the movement script.
		playerMovementScript.InputMovementDirection(m_movementVector2);
		playerMovementScript.MakeCreatureJump(CheckJumpInput());
	}

	private Vector2 HandleMovementInput() {
		//Initialise return variable.
		Vector2 localMovement = Vector2.zero;

		//Horizontal Plane Movement Input.
		if (Input.GetKey(KeyCode.D)) {
			localMovement.x += 1.0f;
		}
		if (Input.GetKey(KeyCode.A)) {
			localMovement.x -= 1.0f;
		}
		if (Input.GetKey(KeyCode.W)) {
			localMovement.y += 1.0f;
		}
		if (Input.GetKey(KeyCode.S)) {
			localMovement.y -= 1.0f;
		}

		//Check running input.
		if (Input.GetKey(KeyCode.LeftShift)) {
			playerMovementScript.MakeCreatureRun(true);
		} else {
			playerMovementScript.MakeCreatureRun(false);
		}

		//Normalize the input.
		localMovement.Normalize();

		//Return it.
		return localMovement;
	}

	private bool CheckJumpInput() {
		//Initialise Return Variable.
		bool localJumping = false;

		//Check if the player wants to jump.
		if (Input.GetKey(KeyCode.Space)) {
			localJumping = true;
		}

		//Return the result.
		return localJumping;
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	#endregion
}