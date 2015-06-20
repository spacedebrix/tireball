using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour {

	public GameController gameController;
	public CameraController mainCamera;
	public TireController tire;
	public Vector3 defaultScaler;
	public Vector3 swingSpeed;
	public float missOffset;
	
	private Rigidbody myRigidBody;
	private Rigidbody tireRigidBody;
	private bool hasCollided;
	private bool hasMissed;


	void Start() {
		hasCollided = false;
		hasMissed = false;
		myRigidBody = GetComponent<Rigidbody> ();
		tireRigidBody = tire.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			myRigidBody.velocity = swingSpeed;
		}
	}

	void FixedUpdate() {
		if (!hasCollided && !hasMissed &&
		    ( myRigidBody.position.z > (tireRigidBody.position.z + missOffset)) ) {
			// Tell GameController we missed :(
			Debug.Log( "Missed" );
			gameController.PlayerMissed();
			hasMissed = true;
		}
	}



	void OnTriggerEnter( Collider other )
	{
		if (!hasCollided && other.tag == "tire") {
			Debug.Log( "Initial velocity = " + tireRigidBody.velocity );
			mainCamera.WatchTire ();
			Vector3 scaler = defaultScaler * (myRigidBody.velocity.z / 10);
			Debug.Log( "scaler = " + scaler );
			float heightScaler = (1/(1.4f - tireRigidBody.transform.position.y));
			Debug.Log( "tire height = " + tireRigidBody.transform.position.y + ", scaler = " + heightScaler );
			scaler = Vector3.Scale(scaler, new Vector3( 1, heightScaler, 1) );
			Debug.Log( "final scaler = " + scaler );
			tireRigidBody.velocity = Vector3.Scale (tireRigidBody.velocity, scaler);

			Debug.Log( "Final velocity = " + tireRigidBody.velocity );
		
			hasCollided = true;
			tire.Batted();
		}

	}
}
