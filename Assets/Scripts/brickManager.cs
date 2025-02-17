using UnityEngine;
using System.Collections;

public class brickManager : MonoBehaviour {

	private bool occupied;
	private int owner=0;
	private Color normalColor;

	public bool Occupied
	{
		get { return occupied; }
	}

	public int Owner
	{
		get { return owner; }
	}
	void Start () {
		//owner = 0;
		normalColor = new Color (0.5f, 0.5f, 0.5f);
	}



	void OnTriggerEnter(Collider other){

		this.occupied = true;
		if (other.CompareTag ("Player")) {
			this.owner = other.gameObject.GetComponent<movement> ().myId;
			Debug.Log ("dueño "+owner);
			switch (owner) {
			case 1:
				this.GetComponent<Renderer> ().material.color = Color.red;
				Debug.Log ("Cambiando a rojo");
				break;
			case 2:
				this.GetComponent<Renderer> ().material.color = Color.blue;
				Debug.Log ("cambiando a azul ");
				break;
			}
		}
	}

	void OnTriggerExit (Collider other){
		this.occupied = false;
	}

	public void backToNormal(){
		owner = 0;
		this.GetComponent<Renderer> ().material.color = normalColor;
	}


}
