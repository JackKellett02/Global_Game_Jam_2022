using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManagerScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (SerializeFields).
	[SerializeField]
	[Range(1.0f, 4.0f)]
	private float notificationDisplayTime = 2.0f;

	[SerializeField]
	private GameObject notificationPanel = null;

	[SerializeField]
	private TextMeshProUGUI notificationText = null;

	[SerializeField]
	private GameObject inventoryPanel = null;

	[SerializeField]
	private TextMeshProUGUI inventoryText = null;

	[SerializeField]
	private GameObject timeLeftPanel = null;

	[SerializeField]
	private TextMeshProUGUI timeLeftText = null;

	[SerializeField]
	private GameObject lossText = null;

	[SerializeField]
	private GameObject winText = null;
	#endregion

	#region Private Variable Declarations.
	//Notification Variables.
	private static Queue<string> notificationQueue = new Queue<string>();
	private bool canDisplayNextNotification = true;

	private static bool hasLost = false;
	private static bool hasWon = false;
	#endregion

	#region Private Functions.
	// Start is called before the first frame update
	void Start() {
		notificationQueue = new Queue<string>();
		hasLost = false;
		hasWon = false;
		notificationPanel.SetActive(false);
		lossText.SetActive(false);
		winText.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		//Display Notifications if there are any.
		if (ThereAreNotificationsToDisplay() && canDisplayNextNotification) {
			canDisplayNextNotification = false;
			StartCoroutine("DisplayNotification");
		}

		//Update inventory UI.
		UpdateInventoryText();

		//UpdateTimeLeftText.
		UpdateTimeLeftText();

		if (hasWon) {
			winText.SetActive(true);
		}

		if (hasLost) {
			lossText.SetActive(true);
		}
	}


	private void UpdateTimeLeftText() {
		//Get the current time left.
		float localTimeLeft = GameManagerScript.GetGameManagerInstance().GetTimeLeft();

		//Update Text.
		string displayText = "Time left till plane reaches destination: " + localTimeLeft + " seconds";
		timeLeftText.text = displayText;
	}

	private void UpdateInventoryText() {
		//Get the current inventory.
		List<Items> inventory = GameManagerScript.GetGameManagerInstance().GetInventoryList();

		//Update the UI to acommodate.
		string textForInventory = "Inventory: ";
		if (inventory.Count <= 0) {
			textForInventory = textForInventory + "\n- empty.";
		} else {
			for (int i = 0; i < inventory.Count; i++) {
				textForInventory = textForInventory + "\n- " + inventory[i];
			}
		}

		//Update the text object.
		inventoryText.text = textForInventory;
	}

	private IEnumerator DisplayNotification() {
		//Get the next message.
		string nextMessage = notificationQueue.Dequeue();

		//Display it.
		notificationPanel.SetActive(true);
		notificationText.text = nextMessage;

		//Start timer.
		yield return new WaitForSeconds(notificationDisplayTime);

		//Take down notification panel.
		notificationPanel.SetActive(false);

		//Start cooldown.
		StartCoroutine("NotificationCooldown");
	}

	private IEnumerator NotificationCooldown() {
		yield return new WaitForSeconds(0.5f);
		canDisplayNextNotification = true;
	}

	private bool ThereAreNotificationsToDisplay() {
		//Check if the queue is empty.
		if (notificationQueue.Count > 0) {
			//There are messages in the queue!!!
			return true;
		}

		//Only gets to this point if there are no messages to display.
		return false;
	}
	#endregion

	#region Public Access Functions (Getters and Setters).

	public static void AddMessageToQueue(string a_message) {
		if (a_message.Length > 0) {
			notificationQueue.Enqueue(a_message);
		}
	}

	public static void ActivateLossText() {
		hasLost = true;
	}

	public static void ActiveWinText() {
		hasWon = true;
	}
	#endregion
}
