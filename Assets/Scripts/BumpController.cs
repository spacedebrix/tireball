using UnityEngine;
using System.Collections;

public class BumpController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider other )
	{
		if (other.tag == "tire") {
			Rigidbody rb = other.GetComponent<Rigidbody> ();

			Vector3 newVelocity = rb.velocity;
			newVelocity.y = (-Physics.gravity.y) * (Physics.gravity.y / rb.velocity.z);
			rb.velocity = newVelocity;

			GetComponent<AudioSource>().Play ();
		}

	}

}
