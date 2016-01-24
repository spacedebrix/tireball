using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterNameController : MonoBehaviour {

	private DatabaseAccessor databaseAccessor;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("GlobalData");
		databaseAccessor = go.GetComponent<DatabaseAccessor> ();
		
		Debug.Log ("Found Data: " + databaseAccessor.GetInstanceID ());

		//GameObject input = gameObject.GetComponent<InputField>();
		//SubmitEvent se= new InputField.SubmitEvent();
		//se.AddListener(databaseAccessor.SetPlayerName);
		//input.onEndEdit = se;

	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SubmitName( string name ) {
		Debug.Log ("Got name " + name );
		databaseAccessor.SetPlayerName ( name );
		Application.LoadLevel ("Title");

	}
}
