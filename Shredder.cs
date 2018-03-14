using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	//Destroys the lasers that go offscreen for optimization
	void OnTriggerEnter2D(Collider2D collider){
		Destroy(collider.gameObject);
	}
}
