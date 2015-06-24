using UnityEngine;
using System.Collections.Generic;

public class DatabaseAccessor : MonoBehaviour {

	private SortedList< long, string > pretendDatabase;

	void Awake() {
		Debug.Log ("Awaking database: " + this.GetInstanceID ());
		DontDestroyOnLoad (this);

		pretendDatabase = new SortedList< long, string > ();
	}

	public SortedList< long, string > GetHighScores( ) {

		return pretendDatabase;

	}

	public void SaveHighScore( long score, string name ) {

		pretendDatabase.Add( score, name );

	}
}
