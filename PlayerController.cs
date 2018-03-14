using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject playerLaserPrefab;
	public float speed;
	public float laserSpeed;
	public float firingRate;
	public float health;
	public AudioClip fire;
	public AudioClip destroyed;
	private float padding = 1f;
	private float minX, maxX;
	private HealthKeeper healthKeeper;
	private Laser laser;

	// Use this for initialization
	void Start () {
		//Bounds the player's movement to the playspace
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftWall = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightWall = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		minX = leftWall.x + padding;
		maxX = rightWall.x - padding;
		healthKeeper = GameObject.Find("Health").GetComponent<HealthKeeper>();
	}

	void Fire () {
		GameObject laser = Instantiate(playerLaserPrefab, this.transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, laserSpeed, 0f);
		AudioSource.PlayClipAtPoint(fire, transform.position, 1.0f);
	}

	void Die(){
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(destroyed, transform.position, 1.0f);
		LevelManager lm = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		lm.LoadLevel("Win Screen");
	}

	// Update is called once per frame
	void Update () {
		//Time.deltaTime: this way speed doesn't depend on the framerate
		if(Input.GetKey(KeyCode.LeftArrow)){
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}

		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}

		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Laser projectile = collider.gameObject.GetComponent<Laser>();
		if(projectile){
			health -= projectile.GetDamage();
			projectile.Hit();
			if(health <= 0) {
				Die();
			}
		}
	}

	public float GetHealth(){
		return health;
	}
}
