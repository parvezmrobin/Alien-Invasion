using UnityEngine;

public class FPShoot : MonoBehaviour {
	//public GameObject Bullet;
	public UnityEngine.UI.Text BulletText;
	public ParticleSystem ParticleSystem;
	public PauseScript PauseScript;
	public Transform From;
	public Transform To;

	int NumBullet;
	int ShootBullet;
	AudioSource[] AudioSources;
	Animator Animator;
	RaycastHit hit;
	float shootTime;

	// Use this for initialization
	void Start () {
		NumBullet = 100;
		BulletText.text = NumBullet.ToString();
		AudioSources = GetComponents<AudioSource>();
		Animator = GetComponent<Animator>();
		shootTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		shootTime += Time.deltaTime;

		if (!PauseScript.Paused) {
			if (Input.GetMouseButtonDown(0) && shootTime > 0) {
				if (NumBullet > 0) {
					ShootBullet = Random.Range(1, 5);
					if (NumBullet < ShootBullet)
						ShootBullet = NumBullet;
					NumBullet -= ShootBullet;
					AudioSources[1].Play();
					shootTime -= .33f;

					Vector3 diff = To.position - From.position;
					for(int i = 0; i<ShootBullet; i++) {
						if(Physics.Raycast(new Ray(From.position, diff), out hit)) {
							Debug.Log(hit.collider.tag);
							if(hit.collider.tag == "Enemy") {
								hit.collider.gameObject.GetComponent<EnemyScript>().Hit();
							}
						}
						
					}

					if (!ParticleSystem.isPlaying)
						ParticleSystem.Play();
				} else {
					AudioSources[2].Play();

				}
				Animator.SetBool("Shoot", true);
			} else if (Input.GetMouseButtonUp(0)) {
				Animator.SetBool("Shoot", false);
				if (ParticleSystem.isPlaying)
					ParticleSystem.Stop();

			}

			if (Input.GetKeyDown(KeyCode.R)) {
				Animator.SetTrigger("DoReload");
				AudioSources[0].Play();
				NumBullet = 100;
				shootTime = -2.5f;
			}

			BulletText.text = NumBullet.ToString();
		}
	}
}
