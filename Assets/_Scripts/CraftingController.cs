using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CraftingController : MonoBehaviour {

	public GameObject player;
	private List<Recipe> recipes;
	private List<Item> items;
	private List<Text> recipesInPanel;

	// Use this for initialization
	void Start () {
		recipes = player.GetComponent<PlayerController>().GetRecipes();
		items = player.GetComponent<PlayerController>().GetBag();

		CreateRecipeText();
		CreateItemText();
	}

	void CreateRecipeText ()
	{
//		print (items);
		if (recipes != null) {
			for (int i = 0; i < recipes.Count; i++) {
				GameObject newRecipeText = new GameObject ("Recipe: " + recipes [i].GetRecipeName ());
				newRecipeText.transform.SetParent (transform.GetChild (0));

				Text t = newRecipeText.AddComponent<Text> ();
				t.text = recipes [i].GetRecipeName ();
				t.fontSize = 14;
			}
		} else {
			GameObject emptyText = new GameObject("NoRecipes");
			print(transform.GetChild(0));
			emptyText.transform.SetParent(transform.GetChild(0));
//			emptyText.transform.localPosition = emptyText.transform.localPosition;
			Text t = emptyText.AddComponent<Text>();
			t.text = "You have no recipes!";
			t.color = Color.black;
			t.fontSize = 14;
			Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

			t.font = ArialFont;
			t.material = ArialFont.material;
		}

	}

	void CreateItemText ()
	{
		if (items != null) {
			for (int i = 0; i < items.Count; i++) {
				GameObject newRecipeText = new GameObject("Items: "+items[i].GetName());
				newRecipeText.transform.SetParent(transform.GetChild(1));

				Text t = newRecipeText.AddComponent<Text>();
				t.text = items[i].GetName();
				t.fontSize = 14;
				t.color = Color.black;

				Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

				t.font = ArialFont;
				t.material = ArialFont.material;
			}
		}


		else {
			GameObject emptyText = new GameObject("NoItems");
			emptyText.transform.SetParent(transform.GetChild(1)	);
//			emptyText.transform.localPosition = emptyText.transform.localPosition;
			Text t = emptyText.AddComponent<Text>();
			t.text = "You have no items";
			t.fontSize = 14;
			t.color = Color.black;

			Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

			t.font = ArialFont;
			t.material = ArialFont.material;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}