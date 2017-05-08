using UnityEngine;

public class PauseScript : MonoBehaviour {
	public bool Paused;

	Animator anim;

	void Start () {
		Paused = false;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			Paused = true;
			anim.SetBool("IsPaused", true);
			Debug.Log("Escape Pressed.");
		}
	}

	public void ExitGame() {
        Cursor.visible = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void ResuemeGame() {
		Paused = false;
		anim.SetBool("IsPaused", false);
	}
}
