using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	enum GameStage_e {
		GS_PLAYING,
		GS_ENDING,
		GS_ENDED
	};

	public Text scoreText;
	public Text gameOverText;
	public float endingTime;

	private long score;
	private GameStage_e myGameStage;
	
	void Start () {
		score = 0;
		myGameStage = GameStage_e.GS_PLAYING;
	}

	void Restart() {
		Application.LoadLevel( Application.loadedLevel );
	}

	void HandleInput() {
		if (Input.GetKeyDown (KeyCode.R)) {
			Restart ();
		}
		if (myGameStage == GameStage_e.GS_ENDED) {
			if( Input.GetKeyDown( KeyCode.Space ) ) {
				Restart ();
			}
		}
	}

	void Update () {
		HandleInput ();

		if (myGameStage == GameStage_e.GS_ENDING) {
			endingTime -= Time.deltaTime;

			if( 0 > endingTime ) {
				myGameStage = GameStage_e.GS_ENDED;
				gameOverText.enabled = true;
			}
		}
	}

	public void UpdateFlying( Vector3 position, Vector3 velocity ) {
		// Some horrible score calculation

		float velocityPoints = velocity.z * 10.0f;
		velocityPoints *= velocityPoints;
		score += Mathf.CeilToInt( velocityPoints );

		float positionPoints = position.y;
		position *= 10;
		position /= (100.0f / velocity.z);
		score += Mathf.CeilToInt( positionPoints );

		scoreText.text = "Score: " + score;
	}

	public void PlayerMissed() {
		myGameStage = GameStage_e.GS_ENDING;
	}

	public void TireLanded() {
		myGameStage = GameStage_e.GS_ENDING;
	}

}
