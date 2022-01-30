using System.Collections;
using System.Collections.Generic;
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
	#endregion

	#region Public Access Functions (Getters and Setters).

	public Items GetContainedItem() {
		itemTaken = true;
		return cargoBoxItem;
	}
	#endregion
}
