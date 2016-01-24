using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DatabaseAccessor : MonoBehaviour {

	private SortedList< long, string > pretendDatabase;
	private TitleController titleController;
	private string playerName;

	void Awake() {
		Debug.Log ("Awaking database: " + this.GetInstanceID ());
		DontDestroyOnLoad (this);

		pretendDatabase = new SortedList< long, string > ();
	}

	public SortedList< long, string > GetHighScores( ) {
		return pretendDatabase;
	}

	public void SaveHighScore( long score ) {
		pretendDatabase.Add( score, playerName );
	}

	public void SetPlayerName( string name ) {
		playerName = name;
	}

	public string GetPlayerName() {
		return playerName;
	}

	public void RegisterForScores (TitleController title){
		titleController = title;
		Debug.Log ("Title Controller registered");
		StartCoroutine (GetFromWWW ());
		Debug.Log ("done");
	}

	IEnumerator GetFromWWW() {

		//pretendDatabase.Clear ();
		WWW www = new WWW ("http://www.spacedebrix.com");


		yield return www;
		//pretendDatabase.Add (1, www.text);


		if (titleController)
			titleController.TakeScores (pretendDatabase);
	}
}
