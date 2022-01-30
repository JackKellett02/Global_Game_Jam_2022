using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Items {
	Empty = 0,
	MatchBox = 1,
	Password = 2,
	Key = 3,
}

public class GameManagerScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	[Range(60.0f, 180.0f)]
	private float timeToDestination = 120.0f;

	[SerializeField]
	[Range(5.0f, 20.0f)]
	private float returnToMenuTime = 10.0f;
	#endregion

	#region Private Variable Declarations.
	private List<Items> playerInventory;
	private float timeLeft = 0.0f;
	private bool hasWon = false;
	private bool hasLost = false;
	private bool canLose = true;

	//Game Manager Instance.
	private static GameManagerScript m_instance = null;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {
		timeLeft = timeToDestination;

		//Initialise manager instance variables.
		m_instance = this.gameObject.GetComponent<GameManagerScript>();
		playerInventory = new List<Items>();
	}

	// Update is called once per frame
	void Update() {
		CheckLossState();

		if (hasWon) {
			canLose = false;
			hasWon = false;
			StartCoroutine("WinCooldown");
		}

		if (hasLost) {
			hasLost = false;
			StartCoroutine("LoseCooldown");
		}
	}

	private void CheckLossState() {
		if (timeLeft <= 0.0f) {
			LoseGame();
			timeLeft = 0.0f;
		} else {
			if (canLose) {
				timeLeft -= Time.deltaTime;
			}
		}
	}

	private void LoseGame() {
		Debug.Log("Player has lost game.");
		hasLost = true;
	}

	private IEnumerator WinCooldown() {
		UI_ManagerScript.ActiveWinText();
		yield return new WaitForSeconds(returnToMenuTime);
		SceneManager.LoadScene(0);
	}

	private IEnumerator LoseCooldown() {
		UI_ManagerScript.ActivateLossText();
		yield return new WaitForSeconds(returnToMenuTime);
		SceneManager.LoadScene(0);
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	public void AddItemToInventory(Items a_item) {
		Debug.Log(a_item + " added to inventory.");
		playerInventory.Add(a_item);
	}

	public List<Items> GetInventoryList() {
		return playerInventory;
	}

	public bool DoesPlayerHaveItem(Items a_item) {
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory[i] == a_item) {
				return true;
			}
		}

		//Will only get to this point if the item was not found in the players inventory.
		return false;
	}

	public void WinGame() {
		//To do:: Win Game Animation.
		Debug.Log("Player has won and has crashed the plane in time.");
		hasWon = true;
	}

	public float GetTimeLeft() {
		return timeLeft;
	}

	public static GameManagerScript GetGameManagerInstance() {
		return m_instance;
	}
	#endregion
}
