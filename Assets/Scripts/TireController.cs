using UnityEngine;
using System.Collections;

public class TireController : MonoBehaviour {

	public float initialVelocityMin;
	public float initialVelocityMax;
	public GameController gameController;

	private Rigidbody myRigidBody;

	private enum TireStage_e {
		TS_PREFLIGHT,
		TS_FLYING,
		TS_POSTFLIGHT
	};

	private TireStage_e myStage;

	// Use this for initialization
	void Start () {
		myStage = TireStage_e.TS_PREFLIGHT;
		myRigidBody = GetComponent<Rigidbody> ();
		myRigidBody.velocity = new Vector3 (0, 0, Random.Range (initialVelocityMin, initialVelocityMax));
	}

	void FixedUpdate () {
		myRigidBody.constraints = RigidbodyConstraints.FreezePositionX;
		if (myStage == TireStage_e.TS_FLYING) {
			gameController.UpdateFlying (myRigidBody.position, myRigidBody.velocity);
		}
	}

	void OnCollisionEnter( Collision collision ) {
		if (myStage == TireStage_e.TS_FLYING) {
			if (collision.collider.tag == "ground") {
				myStage = TireStage_e.TS_POSTFLIGHT;
				Debug.Log ("I hit the ground. :(");
				gameController.TireLanded();
			}
		}
	}

	public void Batted() {
		myStage = TireStage_e.TS_FLYING;
		Debug.Log ("I'm flying! :)");
	}
}
