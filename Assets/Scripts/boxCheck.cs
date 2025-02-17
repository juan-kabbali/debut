using UnityEngine;
using System.Collections;

public class boxCheck : MonoBehaviour {

	private GameObject boardManager;
	// Use this for initialization
	void Start () {
		boardManager = GameObject.Find ("BoardManager");
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			other.gameObject.GetComponent<movement> ().powerUp = true;
			boardManager.GetComponent<BoardManager> ().placePowerUps ();

			Destroy (this.gameObject);
		}
	}
}
