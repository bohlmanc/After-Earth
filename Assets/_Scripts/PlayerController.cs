using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.EventSystems;

//namespace UnityStandardAssets.Characters.FirstPerson{
public class PlayerController : MonoBehaviour {

	private Camera cam;
	private GameObject closeObject;
	private GameObject gun;
	private List<Item> bag;
	private List<Recipe> recipes;
	private bool canCraft;
	private bool crafting;
	private bool typing;
	private bool inventoryOpen;
	private bool reloading;
	private bool isPrimary;
	private bool mapShowing;
	private bool objectClose;
	private int curClipPrim;
	private int clipMaxPrim;
	private int curClipSec;
	private int clipMaxSec;
	private float timeReloading;
	private float timeBetweenShots;

	public float gunRegX;
	public float gunZoomX;
	public int fovReg;
	public int fovZoom;
	public int health;
	public int max_health;
	public int ammoPrim;
	public int ammoSec;
	public int bulletSpeed;
	public Camera mapCam;
	public Canvas HUD;
	public Canvas inventoryHUD;
	public GameObject invHUD;
	public GameObject primGun;
	public GameObject secGun;
	public GameObject playerGun;
	public Image cursor;
	public Slider healthSlider;
	public Text gunInfo;
	public Text healthInfo;
	public Text pickupText;

	// Use this for initialization
	void Start () {
		inventoryOpen = false;
		typing = false;
		recipes = new List<Recipe>();
		bag = new List<Item>();
		objectClose = false;
		mapShowing = false;
		isPrimary = true;
		timeBetweenShots = (float) .1;
		health = max_health;
		canCraft = false;
		crafting = false;
		gun = primGun;
		clipMaxPrim = 30;
		curClipPrim = clipMaxPrim;
		clipMaxSec = 10;
		curClipSec = clipMaxSec;
		cam = GetComponentInChildren<Camera>();
		reloading = false;
		timeReloading = 0;
	}

	void PickupItem(GameObject obj) {
		Item newItem = MakeItem(obj);
		bag.Add(newItem);
		Destroy(obj);
		objectClose = false;
		SortInventory();
	}

	// TAG: FIX THE ITEM ID CREATOR LATER
	void RemoveItem (Item item)
	{
		for(int i = 0;i<bag.Count;i++) {
			if (((Item)bag[i]).ItemEquals(item)) {
				bag.RemoveAt(i);
				break;
			}
		}
	}

	void SortInventory() {
		bag.Sort((a1,a2) => a1.GetName().CompareTo(a2.GetName()));
	}

	/**
	void EquipWeapon (Item w)
	{
		if (w.IsPrimary ()) {
			GameObject newPrimGun = (GameObject)Instantiate (Resources.Load (w.GetName ()));
			newPrimGun.transform.position = primGun.transform.position;
			newPrimGun.transform.rotation = primGun.transform.rotation;
		} else if (w.IsSecondary ()) {
			GameObject newSecGun = (GameObject)Instantiate (Resources.Load (w.GetName ()));
			transform.transform.position = secGun.transform.position;
			transform.transform.rotation = secGun.transform.rotation;
			Item secondaryGunItem = MakeItem (secGun);
		} else {
			print("Can't equip a non-gun!");
		}
	}
	*/

	Item MakeItem (GameObject other)
	{
		Item newItem = new Item(other.GetComponent<ItemController>().GetName(),other.GetComponent<ItemController>().GetValue(),
			other.GetComponent<ItemController>().GetWeight(),other.GetComponent<ItemController>().GetRarity(),
			other.GetComponent<ItemController>().IsPrimary(),other.GetComponent<ItemController>().IsSecondary(),
			other.GetComponent<ItemController>().IsMaterial(),other.GetComponent<ItemController>().GetClipMax(),
			other.GetComponent<ItemController>().GetAmmoMax(),other.GetComponent<ItemController>().GetID());

		return newItem;
	}

	public List<Recipe> GetRecipes() {
		return recipes;
	}

	public List<Item> GetBag ()
	{
		return bag;
	} 

	void EnableCraftingText ()
	{
		pickupText.gameObject.SetActive (true);
		pickupText.text = "Press 'E' to open crafting window";

	}

	void DisableCraftingText ()
	{
		pickupText.gameObject.SetActive (false);
		pickupText.text = "";

	}

	void EnablePickupText (GameObject obj)
	{
		pickupText.gameObject.SetActive (true);
		if (obj.tag == "Harvestable") {
			pickupText.text = "Harvest 1 " + obj.gameObject.GetComponent<HarvestableObjectController> ().GetName () + "\n'E'";
		} else {
			pickupText.text = obj.gameObject.GetComponent<ItemController> ().GetName () + "\n'E'";
		}
	}

	void DisablePickupText() {
		pickupText.text = "";
		pickupText.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
//		inventoryHUD.transform.position = new Vector3(Screen.width/3,0);
//		invHUD.GetComponent<RectTransform>().rect.Set((Screen.width - Screen.width/3),Screen.height,Screen.width/3,0);
		bool typedThisFrame = false;

		if (Input.GetKeyDown (KeyCode.Return)) { // Enter pressed
			if (!typing) {
				typedThisFrame = true;
				DisableFPC ();
				Cursor.visible = true;
			}
		}

		if (!typing) {
			if (objectClose) {
				if (closeObject.tag == "Object") {
					EnablePickupText(closeObject);
					if (Input.GetKeyDown (KeyCode.E)) {
						PickupItem (closeObject);
						DisablePickupText();
					}
				} else if (closeObject.tag == "Harvestable") {
					EnablePickupText(closeObject);
					if (Input.GetKeyDown (KeyCode.E)) {
						List<Item> items = closeObject.GetComponent<HarvestableObjectController> ().Harvest (1);
						foreach (Item i in items) {
							bag.Add (i);
						}

						if (closeObject.GetComponent<HarvestableObjectController> ().IsEmpty ()) {
							closeObject = null;
							objectClose = false;
						}
						DisablePickupText();
					}
				} else if (closeObject.tag == "GunP" || closeObject.tag == "GunS") {
					EnablePickupText(closeObject);
					if (Input.GetKeyDown (KeyCode.E)) {
						PickupItem(closeObject);
						DisablePickupText();
					}
				}


			} else {
				pickupText.gameObject.SetActive (false);
			}

			if (Input.GetKeyDown (KeyCode.Tab)) {
				if (!primGun.gameObject.activeSelf) {
					secGun.gameObject.SetActive (false);

					primGun.gameObject.SetActive (true);
					gun = primGun;

				} else {
					primGun.gameObject.SetActive (false);
					secGun.gameObject.SetActive (true);
					gun = secGun;
				}
				isPrimary = !isPrimary;
			}

			// TAG SHOOTING
			if (Input.GetMouseButton (0)) {
				if (isPrimary) {
					timeBetweenShots += Time.deltaTime;
					if (timeBetweenShots > .05) {
						Shoot ();
						timeBetweenShots = 0;
					}
				}
			}

			// Toggle to show the map view
			if (Input.GetKeyDown (KeyCode.M)) {
				if (mapShowing) {
					mapCam.gameObject.SetActive (false);
					cam.gameObject.SetActive (true);
					mapShowing = false;
				} else {
					cam.gameObject.SetActive (false);
					mapCam.gameObject.SetActive (true);

					mapShowing = true;
				}

			}

			if (Input.GetMouseButtonDown (1)) {
				cam.fieldOfView = fovZoom;
				playerGun.transform.localPosition = new Vector3 (playerGun.transform.localPosition.x + gunZoomX,
					playerGun.transform.localPosition.y, playerGun.transform.localPosition.z);
			}
			if (Input.GetMouseButtonUp (1)) {
				cam.fieldOfView = fovReg;
				playerGun.transform.localPosition = new Vector3 (playerGun.transform.localPosition.x - gunZoomX,
					playerGun.transform.localPosition.y, playerGun.transform.localPosition.z);
			}

			if (Input.GetMouseButtonDown (0)) {
				if (!isPrimary) {
					Shoot ();
				}
			}
			if (canCraft) {
				EnableCraftingText();
				if (Input.GetKeyDown (KeyCode.E)) {
					crafting = true;
					DisableCraftingText();
				}
			}
//			if (crafting) {
//				if (!canCraft) {
//					crafting = false;
//				}
//				if (Input.GetKeyDown (KeyCode.F)) {
//					crafting = false;
//				}
//			}
			if (Input.GetKeyDown (KeyCode.R)) {
				if (isPrimary) {
					if (ammoPrim > 0) {
						reloading = true;
					}
				} else {
					if (ammoSec > 0) {
						reloading = true;
					}
				}

			}
			if (Input.GetKeyDown (KeyCode.Q)) {
				health -= 10;
				if (health < 0) {
					health = 0;
				}	
			}

			if (Input.GetKeyDown (KeyCode.H)) {
				health += 10;
				if (health > 100) {
					health = 100;
				}	
			}

			if (reloading) {
				Reload ();
			}

			if (gun.transform.childCount == 0) {
				gunInfo.text = "Fists";
			} else {
				

			}
			healthSlider.value = health;
			UpdateHUD ();

		} else {
			if (!typedThisFrame) {
				if (Input.GetKeyDown (KeyCode.Return)) {
					EnableFPC ();
				}
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
		}

		// Currently only closes on a subsequent click of I.. change to make it close with the escape key as well

		if (Input.GetKeyDown (KeyCode.I)) {
			if (inventoryOpen) {
				inventoryHUD.gameObject.SetActive (false);
				invHUD.transform.localPosition = new Vector2 (3 * Screen.width / 8, 0);
				HUD.gameObject.SetActive (true);
				EnableFPC ();
				Cursor.visible = false;
			} else {
				HUD.gameObject.SetActive (false);
				inventoryHUD.gameObject.SetActive (true);
				DisableFPC ();
				Cursor.visible = true;
			}
			inventoryOpen = !inventoryOpen;
		}
	}

	void DisableFPC() {
		
		GameObject.Find ("Player").GetComponent<FirstPersonController> ().enabled = false;
		Cursor.visible = true;
		HUD.GetComponent<CanvasGroup>().interactable = true;
		typing = true;
	}

	void EnableFPC ()
	{
		GameObject.Find ("Player").GetComponent<FirstPersonController> ().enabled = true;
		Cursor.visible = false;
		HUD.GetComponent<CanvasGroup>().interactable = false;
		typing = false;
	}

	void OpenInventory() {
		inventoryOpen = true;
		HUD.gameObject.SetActive (false);
		inventoryHUD.gameObject.SetActive (true);
	}

	void UpdateHUD ()
	{
		if (!reloading) {
			if (isPrimary) {
				gunInfo.text = gun.transform.GetChild (0).gameObject.name +"   |   "+secGun.transform.GetChild(0).gameObject.name+ "\n" + curClipPrim + " | " + ammoPrim;
				if (curClipPrim == 0) {
					gunInfo.text = gunInfo.text + "\n(R)eload";
				}
			} else {
				gunInfo.text = gun.transform.GetChild (0).gameObject.name+"   |   "+primGun.transform.GetChild(0).gameObject.name+ "\n" + curClipSec + " | " + ammoSec;
			
				if (curClipSec == 0) {
					gunInfo.text = gunInfo.text + "\n(R)eload";
				}
			}
		} else {
			gunInfo.text = "Reloading...";
		}

	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Workshop") {
			canCraft = true;
		}
		if (other.gameObject.tag == "Object" || other.gameObject.tag == "Harvestable"||other.gameObject.tag == "GunP"||other.gameObject.tag == "GunS") {
			objectClose = true;
			closeObject = other.gameObject;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Workshop") {
			canCraft = false;
		}
		if (other.gameObject.tag == "Object" || other.gameObject.tag == "Harvestable"||other.gameObject.tag == "GunP"||other.gameObject.tag == "GunS") {
			objectClose = false;
			closeObject = null;
		}

	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Bullet") {
			TakeDamage (other.gameObject.name);
		}
	}

	void TakeDamage (string type)
	{
		if (type == "Bullet") {
			health -= 16;
		}
	}

	bool CanShoot ()
	{
		if (isPrimary) {
			return curClipPrim > 0 && !reloading;
		}
		return curClipSec > 0 && !reloading;

	}

	void Reload ()
	{
		timeReloading += Time.deltaTime;
		if (isPrimary) {
			
			if (timeReloading > 1) {
				ammoPrim = ammoPrim - (clipMaxPrim - curClipPrim);
				curClipPrim = clipMaxPrim;
				UpdateHUD ();
				timeReloading = 0;
				reloading = false;
			}
		} else {
			if (timeReloading > 1) {
				ammoSec = ammoSec - (clipMaxSec - curClipSec);
				curClipSec = clipMaxSec;
				UpdateHUD ();
				timeReloading = 0;
				reloading = false;
			}
		}

	}

	void Shoot ()
	{
		if (CanShoot ()) {
			GameObject bullet = Instantiate (Resources.Load ("bulletTest")) as GameObject;
			bullet.GetComponent<Rigidbody> ().transform.position = primGun.gameObject.transform.position;
			bullet.GetComponent<Rigidbody> ().velocity = (cam.transform.forward) * bulletSpeed;
			bullet.gameObject.transform.RotateAround (primGun.transform.position, new Vector3 (0, 1, 0), 30);
			bullet.gameObject.transform.rotation = cam.transform.rotation;
			if (isPrimary) {
				curClipPrim--;
			} else {
				curClipSec--;
			}

		}
	}
}