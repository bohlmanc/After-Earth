using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {
	[Tooltip("Name of object")]
	public string itemName;

	[Tooltip("Weight (as float)")]
	public float weight;

	[Tooltip("Value (as float)")]
	public float value;

	[Tooltip("Rarity (0=common|1=uncommon|2=rare|3=epic|4=storied)")]
	public int rarity;

	[Tooltip("Is this a primary weapon?")]
	public bool gunP;

	[Tooltip("Is this a secondary weapon?")]
	public bool gunS;

	[Tooltip("Is this material?")]
	public bool material;

	[Tooltip("If a gun, how large is the clip?")]
	public int clipMax;

	[Tooltip("If a gun, how much ammo can the player carry of this type?")]
	public int ammoMax;

	[Tooltip("The ID of the item")]
	public int ID;

	public string GetName ()
	{
		return itemName;
	}

	public float GetWeight ()
	{
		return weight;
	}

	public float GetValue ()
	{
		return value;
	}

	public GameObject GetObject ()
	{
		return gameObject;
	}

	public int GetRarity ()
	{
		return rarity;
	}

	public bool IsPrimary() {
		return gunP;
	}

	public bool IsSecondary() {
		return gunS;
	}

	public bool IsMaterial() {
		return material;
	}

	public int GetClipMax() {
		return clipMax;
	}

	public int GetAmmoMax ()
	{
		return ammoMax;
	}

	public int GetID() {
		return ID;
	}

	public Item MakeItem ()
	{
		Item newItem = new Item(GetName(),GetValue(),GetWeight(),GetRarity(),IsPrimary(),IsSecondary(),IsMaterial(),GetClipMax(),GetAmmoMax(),GetID());

		return newItem;
	}

}

