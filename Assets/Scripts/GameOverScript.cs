using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
	public UnityEngine.UI.Text LifeText;
	public PauseScript PauseScript;

	Animator anim;
	bool LoadMain = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (int.Parse(LifeText.text) <= 0) {
			anim.SetTrigger("GameOver");

			PauseScript.Paused = true;
		}
	}

	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		PauseScript.Paused = false;
	}

	public void Exit() {
        Cursor.visible = true;
		SceneManager.LoadScene(0);
	}
}
