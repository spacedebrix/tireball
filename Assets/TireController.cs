using UnityEngine;
using System.Collections;

public class TireController : MonoBehaviour {

	public float initialVelocityMin;
	public float initialVelocityMax;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, Random.Range (initialVelocityMin, initialVelocityMax));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
