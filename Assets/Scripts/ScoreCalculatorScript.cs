using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class ScoreCalculatorScript : MonoBehaviour {
	public Text PlayingTime;
	public Text FinalTimeText;
	public Text ScoreText;
	public Text EnemyKilledText;
	public PauseScript PauseScript;

	int Score = 0;
	Stopwatch watch;

	// Use this for initialization
	void Start () {
		watch = new Stopwatch();
		watch.Start();
	}
	
	// Update is called once per frame
	void Update () {
		PlayingTime.text = " Playing Time : " + TimeString() + " ";
		if (PauseScript.Paused && watch.IsRunning)
			watch.Stop();
		else if (!PauseScript.Paused && !watch.IsRunning)
			watch.Start();
	}

	public void Terminate() {
		UnityEngine.Debug.Log("Terminated");
		watch.Stop();
		FinalTimeText.text = " You Survived : " + TimeString();
		System.TimeSpan time = watch.Elapsed;

		ScoreText.text += "Enemy Killing Score : " + Score.ToString();
		ScoreText.text += "\n";
		ScoreText.text += "Survival Time Score : " + (int)time.TotalSeconds;
		ScoreText.text += "\n";
		Score += (int) time.TotalSeconds;
		ScoreText.text += "Final Score :         " + Score.ToString();
	}

	string TimeString() {
		string str = watch.Elapsed.ToString();
		str = str.Substring(0, str.Length - 6);
		return str;
	}

	public void IncreaseKill(string Name) {
		int EnemyScore = 0;
		if (Name == "Troll")
			EnemyScore = 10;
		else if (Name == "Goblin")
			EnemyScore = 5;
		Score += EnemyScore;

		string str = EnemyKilledText.text;
		str = str.Substring(15, str.Length - 16);
		EnemyKilledText.text = "Enemy Killed : " + (int.Parse(str) + 1).ToString() + " ";
	}
}
