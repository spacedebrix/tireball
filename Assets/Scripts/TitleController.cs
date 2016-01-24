using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TitleController : MonoBehaviour {

	public GameObject highScoreText;
	public GameObject highScoresContainer;
	public InputField playerNameInput;

	private SortedList< long, string > highScores;
	private DatabaseAccessor databaseAccessor;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("GlobalData");

		if (go == null) {
			Application.LoadLevel ("init");
			return;
		}

		databaseAccessor = go.GetComponent<DatabaseAccessor> ();

		Debug.Log ("Found Data: " + databaseAccessor.GetInstanceID ());
		databaseAccessor.RegisterForScores (this);

		if (databaseAccessor.GetPlayerName () != null) {
			playerNameInput.text = databaseAccessor.GetPlayerName();
		}
			
	}

	public void TakeScores( SortedList< long, string > highScores ) {

		Vector3 location = new Vector3 ();
		int rank = 0;
		foreach (var score in highScores) {
			Debug.Log ( "Have score: " + score.Value + " " + score.Key );
			GameObject scoreObject = (GameObject)Instantiate( highScoreText, location, Quaternion.identity );
			Debug.Log ( "Score Instance: " + scoreObject.GetInstanceID() );
			
			scoreObject.GetComponent<Text>().text = "" + rank + ". " + score.Key + " " + score.Value;
			scoreObject.transform.SetParent( highScoresContainer.transform, false );
			location.y -= 30;
			++rank;
		}

	}

	public void StartGame( InputField name ) {
		Debug.Log ("Got name: " + name.text);
		if (name.text.Length == 0) {
			name.text = "Butt";
		}
		databaseAccessor.SetPlayerName ( name.text );
		Application.LoadLevel( "Main" );
	}

	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			Application.LoadLevel( "Main" );
		}
		*/
	}
}
