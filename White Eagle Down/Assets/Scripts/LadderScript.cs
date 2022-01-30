using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	[Range(0.5f, 2.0f)]
	private float teleportCooldown = 1.0f;

	[SerializeField]
	private Transform ladderExitPoint = null;
	#endregion

	#region Private Variable Declarations.

	private static bool canTeleport = true;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && canTeleport) {
			canTeleport = false;
			GameObject playerGameObject = other.gameObject;
			playerGameObject.GetComponent<CharacterController>().enabled = false;
			ClimbLadder(playerGameObject);
			playerGameObject.GetComponent<CharacterController>().enabled = true;
			StartCoroutine("TeleportCooldown");
		}
	}

	private void ClimbLadder(GameObject playerGameObject) {
		//Move the player to the ladder exit point.
		playerGameObject.transform.position = ladderExitPoint.position;
		playerGameObject.transform.rotation = ladderExitPoint.rotation;
	}

	private IEnumerator TeleportCooldown() {
		yield return new WaitForSeconds(teleportCooldown);
		canTeleport = true;
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	#endregion
}
