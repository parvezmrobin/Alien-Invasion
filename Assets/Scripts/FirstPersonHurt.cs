using UnityEngine;

public class FirstPersonHurt : MonoBehaviour
{
	public GameObject Enemy;
	public UnityEngine.UI.Text LifeText;
	public UnityEngine.UI.Image HurtImage;
	public volatile int Damage = 0;
	public PauseScript PauseScript;
	public bool Alive { get; private set; }

	ScoreCalculatorScript ScoreCalculator;
	AudioSource AudioSource;
	Color HurtColor;
	System.Diagnostics.Stopwatch watch;

	void Start() {
		AudioSource = GetComponents<AudioSource>()[1];
		ScoreCalculator = GetComponent<ScoreCalculatorScript>();
		LifeText.text = 100.ToString();
		HurtColor = HurtImage.color;
		HurtImage.color = Color.clear;
		Alive = true;
		watch = new System.Diagnostics.Stopwatch();
		watch.Start();
	}

	// Update is called once per frame
	void Update() {
		if (!PauseScript.Paused) {
			HurtImage.color = Color.Lerp(HurtImage.color, Color.clear, .5f * Time.deltaTime);
			

			if (Damage > 0 && Alive) {
				HurtImage.color = HurtColor;
				AudioSource.Play();
				LifeText.text = (int.Parse(LifeText.text) - Damage).ToString();
				Damage = 0;
				
				if (int.Parse(LifeText.text) <= 0) {
					Alive = false;
					watch.Stop();
					ScoreCalculator.Terminate();
				}
			}
		}

	}

	
}
