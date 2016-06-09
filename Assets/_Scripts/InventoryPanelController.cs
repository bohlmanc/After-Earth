using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour {

	public string panelName;
	private GameObject currentPanel;
//	private Button button;
	private GameObject player;
	private List<Item> playerBag;
	private Dictionary<string,int> outputList;
	private bool isEnabled;


	// Use this for initialization
	void Start () {
		currentPanel = GameObject.Find("CurrentTab");
//		button = gameObject.GetComponent<Button>();
		player = GameObject.Find("Player") as GameObject;
		outputList = new Dictionary<string,int>();
		isEnabled = false;
	}

	void Update ()
	{
		if (enabled) {
			if (isEnabled) {
				print ("Inventory open");
				panelName = "All";
				Clicked ();
				isEnabled = false;
			}
		} else {
			isEnabled = true;
		}
	}
	
//	// Update is called once per frame
//	void Update ()
//	{
//		
//	}

	public void Clicked ()
	{
		currentPanel.GetComponent<Text> ().text = panelName + ":\n\n";
		playerBag = player.GetComponent<PlayerController> ().GetBag ();
		outputList.Clear ();
		currentPanel.GetComponent<Text> ().text = "";

		if (panelName == "All") {
			currentPanel.GetComponent<Text> ().text += "All:\n\n";
			for (int i = 0; i < playerBag.Count; i++) {
				if (outputList.ContainsKey (playerBag [i].GetName ())) {
					outputList [playerBag [i].GetName ()] += 1;
				} else {
					outputList [playerBag [i].GetName ()] = 1;
				}

//				currentPanel.GetComponent<Text>().text += ((Item) playerBag[i]).GetName()+"\n";
			}

			foreach (string i in outputList.Keys) {
				currentPanel.GetComponent<Text> ().text += i + " x" + outputList [i] + "\n";
			}

			// iterate through dictionary and print out the items the player has
		}
		if (panelName == "Weapons") {
			currentPanel.GetComponent<Text> ().text += "Weapons:\n\n";
			for (int i = 0; i < playerBag.Count; i++) {
				if (((Item)playerBag [i]).IsGun ()) {
					currentPanel.GetComponent<Text> ().text += ((Item)playerBag [i]).GetName () + "\n";
					if (outputList.ContainsKey (playerBag [i].GetName ())) {
						outputList [playerBag [i].GetName ()] += 1;
					} else {
						outputList [playerBag [i].GetName ()] = 1;
					}
				}
					
			}

			for (int i = 0; i < playerBag.Count; i++) {
				if (outputList.ContainsKey (playerBag [i].GetName ())) {
					outputList [playerBag [i].GetName ()] += 1;
				}
//				currentPanel.GetComponent<Text>().text += ((Item) playerBag[i]).GetName()+"\n";
			}

			foreach (string i in outputList.Keys) {
				currentPanel.GetComponent<Text> ().text += i + " x" + outputList [i] + "\n";
			}
		}
		if (panelName == "Junk") {
			currentPanel.GetComponent<Text> ().text += "Junk:\n\n";
			for (int i = 0; i < playerBag.Count; i++)	 {
//				if(((Item) playerBag[i].IsJunk()))
//					currentPanel.GetComponent<Text>().text += ((Item) playerBag[i]).GetName()+"\n";
			}
		}
		if (panelName == "Materials") {
			currentPanel.GetComponent<Text> ().text += "Materials:\n\n";
			for (int i = 0; i < playerBag.Count; i++) {
				if (((Item)playerBag [i]).IsMaterial ()) {
//					currentPanel.GetComponent<Text> ().text += ((Item)playerBag [i]).GetName () + "\n";
					if (outputList.ContainsKey (playerBag [i].GetName ())) {
						outputList [playerBag [i].GetName ()] += 1;
					} else {
						outputList [playerBag [i].GetName ()] = 1;
					}
				}
			}

//			for (int i = 0; i < playerBag.Count; i++) {
//				if (outputList.ContainsKey (playerBag [i].GetName())) {
//					outputList [playerBag [i].GetName()] += 1;
//				}
////				currentPanel.GetComponent<Text>().text += ((Item) playerBag[i]).GetName()+"\n";
//			}

			foreach (string i in outputList.Keys) {
				currentPanel.GetComponent<Text>().text += i+" x"+outputList[i]+"\n";
			}
		}
	}

}
