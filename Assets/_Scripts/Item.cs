using UnityEngine;
using System.Collections;

public class Item {

	private string itemName;
	private float value;
	private float weight;
	private int rarity;		
	private bool gunP;
	private bool gunS;
	private bool material;
	private int clipMax;
	private int ammoMax;
	private int ID;	// 0-4, increasing

	// Use this for initialization
	public Item (string n, float val, float wgt, int rare, bool p, bool s, bool m, int clipM, int ammoM, int id) {
		itemName = n;
		value = val;
		weight = wgt;
		rarity = rare;
		gunP = p;
		gunS = s;
		material = m;
		clipMax = clipM;
		ammoMax = ammoM;
		ID = id;
	}

	public string GetName ()
	{
		return itemName;
	}

	public float GetValue() {
		return value;
	}

	public float GetWeight() {
		return weight;
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

	public int GetAmmoMax() {
		return ammoMax;
	}

	public string Info() {
		string returnString = "";
		returnString += itemName;
		return returnString;
	}

	public bool IsGun() {
		return gunP || gunS;
	}

	public int GetID ()
	{
		return ID;
	}

	public bool ItemEquals(Item other) {
		return ID == other.GetID();
	}
//
//	public bool Use ()
//	{
//		if (Random.Range (0, (int)durability_factor * 10) == 0) {
//			durability--;
//			return true;
//		}
//		return false;
//	}
//
//	public void Fix() {
//		durability = max_durability;
//	}
}
