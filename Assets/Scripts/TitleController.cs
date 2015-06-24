using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TitleController : MonoBehaviour {

	public GameObject highScoreText;

	private SortedList< long, string > highScores;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("GlobalData");
		DatabaseAccessor dba = go.GetComponent<DatabaseAccessor> ();
	
		GameObject goHs = GameObject.Find ("HighScores");

		Debug.Log ("Found Data: " + dba.GetInstanceID ());

		highScores = dba.GetHighScores ();

		Vector3 location = new Vector3 ();
		int rank = 0;
		foreach (var score in highScores) {
			Debug.Log ( "Have score: " + score.Value + " " + score.Key );
			GameObject scoreObject = (GameObject)Instantiate( highScoreText, location, Quaternion.identity );
			Debug.Log ( "Score Instance: " + scoreObject.GetInstanceID() );

			scoreObject.GetComponent<Text>().text = "" + rank + ". " + score.Key + " " + score.Value;
			scoreObject.transform.SetParent( goHs.transform, false );
			location.y -= 30;
			++rank;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			Application.LoadLevel( "Main" );
		}
	}
}
