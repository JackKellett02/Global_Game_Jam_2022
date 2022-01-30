using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CargoBoxScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	private Items cargoBoxItem = Items.Empty;
	#endregion

	#region Private Variable Declarations.

	private bool itemTaken = false;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (itemTaken) {
			cargoBoxItem = Items.Empty;
			itemTaken = false;
		}
	}

	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			Items item = GetContainedItem();
			if (item == Items.Empty)
			{
				UI_ManagerScript.AddMessageToQueue("Cargo box is empty!!!");
			}
			else
			{
				UI_ManagerScript.AddMessageToQueue("Picking up " + item + " from the cargo box.");
				GameManagerScript.GetGameManagerInstance().AddItemToInventory(item);
			}
		}
	}


	private Items GetContainedItem() {
		itemTaken = true;
		return cargoBoxItem;
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	#endregion
}
