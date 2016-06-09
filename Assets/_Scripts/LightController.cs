using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	[Tooltip ("Number of minutes per second passed, try 60")]
	public float minutesPerSeconds;

//	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
//		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		float angleThisFrame = Time.deltaTime / 360 * minutesPerSeconds;
		transform.RotateAround(transform.position, Vector3.forward, angleThisFrame);
	}
}
