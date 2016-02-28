using UnityEngine;
using System.Collections;

public class NewCamera : MonoBehaviour {
	string curHitTag; // Current object tag hit
	public int numberHit; // How many times the it has been hit
	public bool onProblem = false; // Whether on a problem or not
	public int correctAnswer;
	public Camera c = new Camera();
	public int currentScore;
	GameObject [] candiesIn;
	public int timeLeft = 200; // How long to display the good job thing
	public bool congratulating = false;
	public GUI encourage;
		
		// Update is called once per frame
		public void doUpdate () {
			Ray myRay = new Ray (c.transform.position,
			                     c.transform.forward);
			RaycastHit currentHit;
			// Looking at something
			if (Physics.Raycast (myRay, out currentHit, Mathf.Infinity)) {
				string theTag;
				if (currentHit.collider.gameObject.tag == null) // If there is no tag
				{
					theTag = "Nahthiswontwork";
				}
				else // Otherwise you have to check the tag
				{
					theTag = currentHit.collider.gameObject.tag;
				}
				// If the object hit starts with the proper tag
				if (theTag.StartsWith("wall"))
				{
					if (curHitTag == theTag)
						numberHit++; // Increment hit
					else
					{
						numberHit = 0; // Set hit back to 0
						curHitTag = theTag;
					}
				}
				if (numberHit >= 200) // If you've been on problem long enough
				{
					onProblem = true; // Set onProblem to true
					if (theTag.Equals("wall1"))
						//currentHit.collider.gameObject.GetComponent<Script>().Method();
						correctAnswer = 1;
					else if (theTag.Equals ("wall2"))
						correctAnswer = 2;
					else if (theTag.Equals("wall3"))
						correctAnswer = 4;
					else
						correctAnswer = -1; // gg can't solve this
					
					currentHit.collider.gameObject.tag = "NOTwall";
				}
			}	
		}

	
	// Use this for initialization
	void Start () {
		currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (onProblem == false) {
			doUpdate ();
		}
		else
		{
			candiesIn = GameObject.FindGameObjectsWithTag ("InBox");
			if (Mathf.Approximately (candiesIn.GetLength(0), correctAnswer)) 
			{
				onProblem = false;
				incrementScore(); // Call to increment score
				congratulating = true;
				timeLeft = 200;
			}
			if (congratulating == true)
			{
				timeLeft--; // Decrement time
				if (timeLeft <= 0)
					congratulating = false; // Done congratulating
			}
		}	
		OnGUI (); // Update the score every frame
	}
	
	// Call this to create the new label with score
	void OnGUI()
	{
		GUI.Label (new Rect (0, 0, 100, 100), "Current score: " 
		           + currentScore.ToString ());
		if (congratulating == true) {
			GUI.Label (new Rect (150, 0, 100, 100), "Great job on the problem!"); 
		}
	}
	
	// Called when score needs to be incremented
	void incrementScore()
	{
		currentScore++;
	}
}
