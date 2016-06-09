using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	public GameObject allObjects;

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(allObjects);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
