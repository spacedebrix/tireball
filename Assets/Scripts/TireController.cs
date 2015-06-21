using UnityEngine;
using System.Collections;

public class TireController : MonoBehaviour {

	public float initialVelocityMin;
	public float initialVelocityMax;
	public GameController gameController;

	public AudioClip impactSmall;
	public AudioClip impactMedium;
	public AudioClip impactBig;
	public AudioClip impactHuge;

	private Rigidbody myRigidBody;

	private AudioSource impactSmallSource;
	private AudioSource impactMediumSource;
	private AudioSource impactBigSource;
	private AudioSource impactHugeSource;

	private enum TireStage_e {
		TS_PREFLIGHT,
		TS_FLYING,
		TS_POSTFLIGHT
	};

	private TireStage_e myStage;

	AudioSource addAudioSource( AudioClip clip ) {
		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.clip = clip;
		return source;
	}

	void Start () {
		myStage = TireStage_e.TS_PREFLIGHT;
		myRigidBody = GetComponent<Rigidbody> ();
		myRigidBody.velocity = new Vector3 (0, 0, Random.Range (initialVelocityMin, initialVelocityMax));

		impactSmallSource = addAudioSource (impactSmall);
		impactMediumSource = addAudioSource (impactMedium);
		impactBigSource = addAudioSource (impactBig);
		impactHugeSource = addAudioSource (impactHuge);
	}

	void FixedUpdate () {
		myRigidBody.constraints = RigidbodyConstraints.FreezePositionX;
		if (myStage == TireStage_e.TS_FLYING) {
			gameController.UpdateFlying (myRigidBody.position, myRigidBody.velocity);
		}
	}

	void OnCollisionEnter( Collision collision ) {
		if (myStage == TireStage_e.TS_FLYING && collision.relativeVelocity.y > 0 ) {
			if (collision.collider.tag == "ground") {
				myStage = TireStage_e.TS_POSTFLIGHT;
				Debug.Log ("I hit the ground. :(");
				gameController.TireLanded();
				Debug.Log( "Velocity = " + collision.relativeVelocity.y );

				if( collision.relativeVelocity.y > 200 ) {
					impactHugeSource.Play();
				} else if( collision.relativeVelocity.y > 150 ){
					impactBigSource.Play ();
				} else if( collision.relativeVelocity.y > 75 ) {
					impactMediumSource.Play();
				} else {
					impactSmallSource.Play ();
				}
			}
		}
	}

	public void Batted() {
		if (myRigidBody.velocity.y > 1) {
			myStage = TireStage_e.TS_FLYING;
			Debug.Log ("I'm flying! :)");
		} else {
			gameController.TireLanded ();
		}
	}
}
