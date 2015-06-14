using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour {

	public CameraController mainCamera;
	public Rigidbody tire;
	public Vector3 defaultScaler;
	public Vector3 swingSpeed;

	private bool swinging;
	private float startZ;
	private Rigidbody myRigidBody;

	void Start() {
		swinging = false;
		startZ = transform.position.z;
		myRigidBody = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			myRigidBody.velocity = swingSpeed;
		}
	}

	void FixedUpdate() {
		if (swinging) {
			if( transform.position.z >= startZ + .5 ) {
				swinging = false;
			}
			else {
				Vector3 newPosition = transform.position;
				newPosition.z += 0.01f;
				transform.position = newPosition;
			}
		}
	}

	void OnTriggerEnter( Collider other )
	{
		if (other.tag == "tire") {
			mainCamera.WatchTire ();
			tire.velocity = Vector3.Scale (tire.velocity, defaultScaler) + myRigidBody.velocity;
		}

	}
}
