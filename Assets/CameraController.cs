using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float timeOut;

	private bool watchTarget;
	private bool hasTimedOut;
	private float timeOutStart;


	// Use this for initialization
	void Start () {
		watchTarget = false;
		hasTimedOut = false;
		timeOutStart = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (watchTarget) {
			if(hasTimedOut) {
				transform.LookAt (target.transform);
			}
			else {
				if( (Time.time - timeOutStart) >= timeOut ) {
					hasTimedOut = true;
				}
			}
		}
	}

	public void WatchTire() {
		watchTarget = true;
		timeOutStart = Time.time;
	}

}
