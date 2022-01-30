using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	private string activationMessage = "Activating";

	[SerializeField]
	private Items requiredItem = Items.Password;
	#endregion

	#region Private Variable Declarations.
	private static int currentActivations = 0;
	private int requiredActivations = 2;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {
		currentActivations = 0;
	}

	// Update is called once per frame
	void Update() {
		if (currentActivations >= requiredActivations)
		{
			//To do:: PLAYER HAS WON STUFF.
			GameManagerScript.GetGameManagerInstance().WinGame();
			currentActivations = requiredActivations;
		}
	}

	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			if (GameManagerScript.GetGameManagerInstance().DoesPlayerHaveItem(requiredItem)) {
				UI_ManagerScript.AddMessageToQueue(activationMessage);
				currentActivations++;
			} else {
				UI_ManagerScript.AddMessageToQueue("You need to get: " + requiredItem);
			}
		}
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	#endregion
}
