using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	private float timeAlive;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive += Time.deltaTime;
//		print(timeAlive);
		if(timeAlive > 5)
			Destroy(gameObject);
	}

	void OnColliderEnter (Collision other)
	{
		if (other.gameObject.tag == "Breakable" || other.gameObject.tag == "Player" || other.gameObject.tag == "NPC") {
			Destroy(gameObject);
		} 
	}
}
