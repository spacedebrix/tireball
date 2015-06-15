using UnityEngine;
using System.Collections;

public class TireController : MonoBehaviour {

	public float initialVelocityMin;
	public float initialVelocityMax;

	private Rigidbody myRigidBody;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		myRigidBody.velocity = new Vector3 (0, 0, Random.Range (initialVelocityMin, initialVelocityMax));
	}

	void FixedUpdate () {
		myRigidBody.constraints = RigidbodyConstraints.FreezePositionX;
	}
}
