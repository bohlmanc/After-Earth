using UnityEngine;
using System.Collections;

public class HarvestableRespawnController : MonoBehaviour {

	public int respawnTime;
	private float timeElapsed;
	private bool respawn;
	public GameObject child;
	public GameObject prefab;
//	public GameObject itemNeeded;
//	private string prefabName;

	// Use this for initialization
	void Start () {
		respawn = false;
		timeElapsed = 0f;
		CreateChild();
//		prefabName = child.GetComponent<HarvestableObjectController>().GetName();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!respawn) {
			if (transform.childCount == 0) {
				respawn = true;
			}	
				
		} else {
			timeElapsed += Time.deltaTime;
			if (timeElapsed >= respawnTime) {
				CreateChild();
				timeElapsed = 0;
				respawn = false;
			}
		}
	}

	public int GetRespawnTime() {
		return respawnTime;
	}

	void CreateChild ()
	{
		child = (GameObject) Instantiate (prefab, transform.position, transform.rotation);
		child.transform.SetParent (gameObject.transform);
	}


}
