using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour{

	public List<GameObject> items_needed;

	public GameObject item;
	private Item itemI;

	public Recipe(GameObject i) {
//		items_needed = needed;
		item = i;
		itemI = item.GetComponent<ItemController>().MakeItem();
	}

	public bool CanCreate (List<Item> items)
	{
		for (int i = 0; i < items_needed.Count; i++) {
			bool valid = false;
			for (int j = 0; j < items.Count; j++) {
				if (items [j].ItemEquals (items_needed [i].GetComponent<ItemController>().MakeItem())) {
					valid = true;
				}
			}
			if (!valid) {
				return false;
			}
		}
		return true;
	}

	public bool CanCreate (List<GameObject> items)
	{
		for (int i = 0; i < items_needed.Count; i++) {
			bool valid = false;
			for (int j = 0; j < items.Count; j++) {
				if (items[j].GetComponent<ItemController>().MakeItem().ItemEquals (items_needed [i].GetComponent<ItemController>().MakeItem())) {
					valid = true;
				}
			}
			if (!valid) {
				return false;
			}
		}
		return true;
	}

	public Item Create (List<Item> items)
	{
		if (CanCreate (items)) {
			return itemI;
		}
		return null;
	}

	public Item Create (List<GameObject> items)
	{
		if (CanCreate (items)) {
			return itemI;
		}
		return null;
	}

	public string GetRecipeName ()
	{
		return itemI.GetName();
	}

}
