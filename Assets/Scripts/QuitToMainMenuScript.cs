using UnityEngine;
using System.Collections;

public class QuitToMainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
