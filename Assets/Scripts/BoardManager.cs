using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

	private GameObject[,] bricksList;
	private GameObject playerOne;
	private GameObject playerTwo;
	private int p1Points;
	private int p2Points;

	private int puCounter=0;

	public GameObject brickPrefab;
	public GameObject playerOnePrefab;
	public GameObject playerTwoPrefab;
	public GameObject portalPrefab;
	public GameObject bateryPrefab;
	public GameObject boxPrefab;
	public GameObject powerPrefab;

	public Text p1Text;
	public Text p2Text;

	void Start () {
		bricksList=new GameObject[10,10];
		fillBoard ();
		setPlayers ();
		placePortal ();
		p1Points = 0;
		p2Points = 0;
		placePowerUps ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void fillBoard(){
		for (int i = 0; i < bricksList.GetLength(0); i++) 
		{
			for (int j = 0; j < bricksList.GetLength (1); j++) 
			{

				bricksList [i, j] = Instantiate (brickPrefab, new Vector3 (j * 2, 0, i * 2), Quaternion.identity) as GameObject;
				bricksList [i, j].name = ("BoardBrick" + i + "" + j);
				//NetworkServer.Spawn (bricks [i, j]);

			}
		}
	}

	void setPlayers(){
		playerOne = Instantiate (playerOnePrefab, new Vector3 (0, 1.79f, 0), Quaternion.Euler(0,90,0) )as GameObject;
		playerTwo = Instantiate (playerTwoPrefab, new Vector3 (18, 1.79f, 18), Quaternion.Euler(0,-90,0)) as GameObject;
	}

	void placePortal(){
		int fila = Random.Range (0, 10);
		int col = Random.Range (0, 10);
		bool ocupado = bricksList [fila, col].GetComponent<brickManager> ().Occupied;

		if (!ocupado) {
			GameObject portal = Instantiate (portalPrefab, new Vector3 (fila * 2, 0.35f, col * 2), Quaternion.identity) as GameObject;
		} else {
			Debug.Log ("occupied tile " + fila*2 + "-" + col*2);
			placePortal ();
		}
	}

	public void launchPower(Vector3 posicion, Quaternion rotacion){
		GameObject powerUp = Instantiate (powerPrefab, posicion,rotacion) as GameObject;
		Destroy (powerUp, 2.0f);
	}

	public void placePowerUps(){
		int fila = Random.Range (0, 10);
		int col = Random.Range (0, 10);
		bool ocupado = bricksList [fila, col].GetComponent<brickManager> ().Occupied;

		if (!ocupado) {
			if (puCounter == 0) {
				GameObject powerUp = Instantiate (bateryPrefab, new Vector3 (fila * 2, 0.94f, col * 2), Quaternion.identity) as GameObject;
				puCounter++;
			} else {
				GameObject powerUp = Instantiate (boxPrefab, new Vector3 (fila * 2, 0.09f, col * 2), Quaternion.identity) as GameObject;
				puCounter--;
			}
		} else {
			Debug.Log ("occupied tile " + fila*2 + "-" + col*2);
			placePowerUps ();
		}
	}


	public void countPoints(int playerId){
		int points = 0;
		for (int i = 0; i < bricksList.GetLength(0); i++) 
		{
			for (int j = 0; j < bricksList.GetLength (1); j++) 
			{
				
				int brickOwner = bricksList [i, j].GetComponent<brickManager> ().Owner;

				if (brickOwner == playerId) {
					points++;
					bricksList [i, j].GetComponent<brickManager> ().backToNormal ();
				}

			}
		}

		if (playerId == 1) {
			p1Points += points;
			p1Text.text = p1Points.ToString ();
		} else {
			p2Points += points;
			p2Text.text = p2Points.ToString ();
		}
		placePortal ();
	}


}
