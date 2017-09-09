using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float health;
	public GameObject enemyLaserPrefab;
	public float laserSpeed;
	public float shotsPerSecond;
	public int score = 100;
	public AudioClip fire;
	public AudioClip destroyed;
	private ScoreController scoreController;

	// Use this for initialization
	void Start () {
		scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
	}

	// Update is called once per frame
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Laser laser = collider.gameObject.GetComponent<Laser>();
		if(laser){
			health -= laser.GetDamage();
			laser.Hit();
			if(health <= 0) {
				Death();
			}
		}
	}

	void Death(){
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(destroyed, transform.position, 1.0f);
		scoreController.Score(score);
	}

	void Fire(){
		GameObject laser = Instantiate(enemyLaserPrefab, this.transform.position + Vector3.down * 0.5f, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -laserSpeed, 0f);
		AudioSource.PlayClipAtPoint(fire, transform.position, 1.0f);
	}
}
