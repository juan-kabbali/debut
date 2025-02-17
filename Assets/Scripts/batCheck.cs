using UnityEngine;
using System.Collections;

public class batCheck : MonoBehaviour {

	private GameObject boardManager;
	// Use this for initialization
	void Start () {
		boardManager = GameObject.Find ("BoardManager");
	}
	
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			other.gameObject.GetComponent<movement> ().setUpExtraSpeed (10.0f);
			boardManager.GetComponent<BoardManager> ().placePowerUps ();

			Destroy (this.gameObject);
		}
	}
}
