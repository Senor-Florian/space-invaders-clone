using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay;
	private bool movingRight = true;
	private float minX, maxX;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftWall = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightWall = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		minX = leftWall.x;
		maxX = rightWall.x;
		SpawnUntilFull();
	}

	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if(!movingRight){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float formationLeftWall = transform.position.x - width*0.5f;
		float formationRighttWall = transform.position.x + width*0.5f;
		if(formationLeftWall < minX) {
			movingRight = true;
		}
		else if(formationRighttWall > maxX) {
			movingRight = false;
		}

		if(AllMembersDead()){
			SpawnUntilFull();	
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){  //So we don't even try creating an enemy if freePosition is null
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition; //This way the enemies are put into the formation

		}
		if(NextFreePosition()){	
			Invoke("SpawnUntilFull", spawnDelay); //Adds delay between creating new enemies
		}
	}

	void SpawnEnemies(){
		foreach(Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child; //This way the enemies are put into the formation
		}
	}

	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}

	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
}
