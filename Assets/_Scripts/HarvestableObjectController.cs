using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarvestableObjectController : MonoBehaviour {

	
	private Item material;

	private int quantity;

	public string matName;
	public string prefabName;
	public int maxQuantity;
	public int timeToHarvest;
	public float value;
	public float weight;
	public int rarity;	
	public int ID;

	private float time;

	// Use this for initialization
	void Start () {
		time = 0f;
		quantity = Random.Range(maxQuantity/2,maxQuantity);
		material = new Item(matName,value,weight,rarity,false,false,true,0,0,ID);
	}
	
	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		if (timeToHarvest <= time) {
			time = 0;
			if (quantity < maxQuantity) {
				quantity += 1;
//				print("Adding quantity");
			}
		}
		if (IsEmpty()) {
			Destroy(gameObject);
		}
	}

	public bool IsEmpty ()
	{
		return quantity == 0;
	}

	public List<Item> Harvest (int amount)
	{
		List<Item> mats = new List<Item> ();
		if (amount >= quantity) {
			for (int i = 0; i < quantity; i++) {
				mats.Add(material);
				quantity--;
			}
		} else {
			for (int i = 0; i < amount; i++) {
				mats.Add(material);
				quantity--;
			}
		}

		return mats;
	}

	public string GetName() {
		return matName;
	}

	public void SetQuantity(int newQ) {
		quantity = newQ;
	}
}
