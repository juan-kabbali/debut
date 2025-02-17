using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public KeyCode up;
	public KeyCode down;
	public KeyCode right;
	public KeyCode left;
	public KeyCode shoot;

	public int myId;
	public bool powerUp = false;


	public float speed = 5.0F;

	private int direction;

	private GameObject boardManager;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 nextPosition;

	private float extraSpeedTime=5.0f;
	private float startTime;
	private float journeyLength=2.0f;

	private Vector3 dir0;
	private Vector3 dir1;
	private Vector3 dir2;
	private Vector3 dir3;

	private Vector3 rot0;
	private Vector3 rot1;
	private Vector3 rot2;
	private Vector3 rot3;


	//private SwipeController mySwipeController;

	// Use this for initialization
	void Start () {
		boardManager = GameObject.Find ("BoardManager");
		direction = -1;
		startPosition=this.transform.position;
		endPosition = startPosition;



		dir0 = new Vector3 (0, 0, 2);
		dir1 = new Vector3 (0, 0, -2);
		dir2 = new Vector3 (2, 0, 0);
		dir3 = new Vector3 (-2, 0, 0);

		rot0 = new Vector3 (0, 0, 0);
		rot1 = new Vector3 (0, 180, 0);
		rot2 = new Vector3 (0, 90, 0);
		rot3 = new Vector3 (0, 270,0 );


	}

	// Update is called once per frame
	void Update () {

		if (speed != 5) {
			verifySpeedTime ();
		}
		if (powerUp) {
			if (Input.GetKeyDown (shoot)) {
				boardManager.GetComponent<BoardManager> ().launchPower (this.gameObject.transform.GetChild (0).transform.position, this.transform.rotation);
				powerUp = false;
			}
		}

		if (Input.GetKeyDown(up))
		{
			direction = 0;
		}
		else if (Input.GetKeyDown(down))
		{
			direction = 1;
		}
		else if (Input.GetKeyDown(right))
		{
			direction = 2;
		}
		else if (Input.GetKeyDown(left))
		{
			direction = 3;

		}
			
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

		switch (direction)
		{
		case 0:verifyPosition (dir0, fracJourney, rot0);
			break;
		case 1: verifyPosition (dir1, fracJourney, rot1);
			break;
		case 2:verifyPosition (dir2, fracJourney, rot2);
			break;
		case 3:verifyPosition (dir3, fracJourney, rot3);
			break;
		}

	}



	private void moverse(float interpolant){
		if (endPosition.x < 0 || endPosition.x > 18) {
			endPosition.x = Mathf.Clamp (endPosition.x, 0, 18);
		}
		if (endPosition.z < 0 || endPosition.z > 18) {
			endPosition.z=Mathf.Clamp (endPosition.z, 0, 18);
		}
		transform.position = Vector3.Lerp(startPosition, endPosition, interpolant);
	}

	private void setNext(Vector3 vectorDirection, Vector3 rotat){
		startPosition = endPosition;
		endPosition= endPosition+vectorDirection;
		transform.eulerAngles = rotat;
		startTime = Time.time;
	}

	private void verifyPosition(Vector3 vectorDirection, float interpolant, Vector3 rotationDir){
		if (transform.position == endPosition)
		{
			setNext (vectorDirection, rotationDir);
		}
		else
		{
			moverse (interpolant);
		}
	}

	private void verifySpeedTime(){
		
		extraSpeedTime -= Time.deltaTime;
		if(extraSpeedTime < 0)
		{
			this.speed = 5.0f;
		}

	}

	public void setUpExtraSpeed(float newSpeed){
		this.speed=newSpeed;
		extraSpeedTime = 5.0f;
	}



}
