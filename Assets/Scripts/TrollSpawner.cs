using UnityEngine;
using System.Threading;

public class TrollSpawner : MonoBehaviour
{
	public GameObject Troll;
	public int SpawnSecond;

	bool spawn = true;

	// Use this for initialization
	void Start() {
		new Thread(() => {
			while (true) {
				try {
					Thread.Sleep(SpawnSecond * 1000);
				} catch (System.Exception) { } finally {
					spawn = true;
				}
			}
		}).Start();

		new Thread(() => {
			while (SpawnSecond > 3) {
				try {
					Thread.Sleep(60 * 1000);
				} finally {
					SpawnSecond--;
				}
			}
		}).Start();
	}

	// Update is called once per frame
	void Update() {
		if (spawn) {
			Instantiate(Troll, transform.position, transform.rotation);
			spawn = false;
		}
	}
}
