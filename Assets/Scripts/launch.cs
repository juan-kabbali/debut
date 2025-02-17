using UnityEngine;
using System.Collections;

public class launch : MonoBehaviour {

	private float movementSpeed=20.0f;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}

	void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Player")) {
			Debug.Log ("golpeado");
			other.gameObject.GetComponent<movement> ().setUpExtraSpeed (1.0f);
		}
	}
}
