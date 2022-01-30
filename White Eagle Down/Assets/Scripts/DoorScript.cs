using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	private Items keyItem = Items.Key;
	#endregion

	#region Private Variable Declarations.

	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0))
		{
			if (GameManagerScript.GetGameManagerInstance().DoesPlayerHaveItem(keyItem))
			{
				UI_ManagerScript.AddMessageToQueue("Opening Door!!");
				gameObject.SetActive(false);
			}
			else
			{
				UI_ManagerScript.AddMessageToQueue("You need to get: " + keyItem);
			}
		}
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	#endregion
}
