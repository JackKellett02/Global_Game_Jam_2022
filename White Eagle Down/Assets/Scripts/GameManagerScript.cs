using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items {
	Empty = 0,
	MatchBox = 1,
	Password = 2,
	Key = 3,
}

public class GameManagerScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).

	#endregion

	#region Private Variable Declarations.
	private List<Items> playerInventory;

	//Game Manager Instance.
	private static GameManagerScript m_instance = null;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start()
	{
		//Initialise manager instance variables.
		m_instance = this.gameObject.GetComponent<GameManagerScript>();
		playerInventory = new List<Items>();
	}

	// Update is called once per frame
	void Update() {

	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	public void AddItemToInventory(Items a_item)
	{
		playerInventory.Add(a_item);
	}

	public bool DoesPlayerHaveItem(Items a_item)
	{
		for (int i = 0; i < playerInventory.Count; i++)
		{
			if (playerInventory[i] == a_item)
			{
				return true;
			}
		}

		//Will only get to this point if the item was not found in the players inventory.
		return false;
	}

	public static GameManagerScript GetGameManagerInstance()
	{
		return m_instance;
	}
	#endregion
}
