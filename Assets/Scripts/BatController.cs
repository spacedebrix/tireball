using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour {

	public CameraController mainCamera;
	public Rigidbody tire;
	public Vector3 defaultScaler;
	public Vector3 swingSpeed;
	
	private Rigidbody myRigidBody;
	private bool hasCollided;

	void Start() {
		hasCollided = false;
		myRigidBody = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			myRigidBody.velocity = swingSpeed;
		}
	}

	void OnTriggerEnter( Collider other )
	{
		if (!hasCollided && other.tag == "tire") {
			Debug.Log( "Initial velocity = " + tire.velocity );
			mainCamera.WatchTire ();
			Vector3 scaler = defaultScaler * (myRigidBody.velocity.z / 10);
			Debug.Log( "scaler = " + scaler );
			float heightScaler = (1/(1.4f - tire.transform.position.y));
			Debug.Log( "tire height = " + tire.transform.position.y + ", scaler = " + heightScaler );
			scaler = Vector3.Scale(scaler, new Vector3( 1, heightScaler, 1) );
			Debug.Log( "final scaler = " + scaler );
			tire.velocity = Vector3.Scale (tire.velocity, scaler);

			Debug.Log( "Final velocity = " + tire.velocity );
		
			hasCollided = true;
		}

	}
}
