using UnityEngine;
using System.Collections;

public class portalCheck : MonoBehaviour {

	private GameObject boardManager;
	// Use this for initialization
	void Start () {
		boardManager = GameObject.Find ("BoardManager");
	}
	
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			int player = other.gameObject.GetComponent<movement> ().myId;
			boardManager.GetComponent<BoardManager> ().countPoints (player);

			Destroy (this.gameObject);
		}
	}
}
