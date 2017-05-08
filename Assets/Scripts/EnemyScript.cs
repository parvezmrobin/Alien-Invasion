using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	public string Name = "Troll";
	public Transform NavTarget;
	public FirstPersonHurt Victim;
	public PauseScript PauseScript;
	public int life = 10;
	public ScoreCalculatorScript ScoreCalculator;

	Animator anim;
	NavMeshAgent navMeshAgent;
	AudioSource Audio;
	float dist;
	float attackTimer;


	void Start () {
		anim = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		Audio = GetComponent<AudioSource>();
		attackTimer = .5f;
	}

	void Update () {
		if (life > 0 && !PauseScript.Paused) {
			navMeshAgent.SetDestination(NavTarget.position);
			
			dist = Hypotenuse(NavTarget.position.x - transform.position.x, NavTarget.position.z - transform.position.z);
			if (dist <= 2) {
				anim.SetBool("IsWalking", false);
				anim.SetBool("IsRunning", false);
			} else if (dist < 10) {
				anim.SetBool("IsWalking", true);
				anim.SetBool("IsRunning", false);
				navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, 2, .5f * Time.deltaTime);
			} else {
				anim.SetBool("IsRunning", true);
				anim.SetBool("IsWalking", false);
				navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, 5, .5f * Time.deltaTime);
			}


			if (dist < 2.5 && Victim.Alive) {
				attackTimer += Time.deltaTime;
				anim.SetBool("DoAttack", true);
				if (attackTimer > 1.5f) {
					attackTimer = 0;
					if (Name == "Troll")
						Victim.Damage = 5;
					else if (Name == "Goblin")
						Victim.Damage = 3;
				}
			} else {
				anim.SetBool("DoAttack", false);
				attackTimer = .5f;
			}
			//Debug.Log(attackTimer);
		} else {
			navMeshAgent.SetDestination(transform.position);
		}
	}

	float Hypotenuse(float x, float y) {
		return Mathf.Sqrt(x * x + y * y);
	}

	
	public void Hit() {
		if (life > 0 && !PauseScript.Paused) {
			anim.SetTrigger("Hit");
			Audio.Play();
			life--;
			attackTimer = 0f;

			if (life == 0) {
				anim.SetTrigger("Die");
				Destroy(gameObject, 10);
				ScoreCalculator.IncreaseKill(Name);
			}
		}
	}

	
}
