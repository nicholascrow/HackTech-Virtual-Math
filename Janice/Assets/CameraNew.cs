using UnityEngine;
using System.Collections;

public class CameraNew : MonoBehaviour {
	public class LookProblem {
		
		string curHitTag; // Current object tag hit
		public int numberHit; // How many times the it has been hit
		public bool onProblem = false; // Whether on a problem or not
		public int correctAnswer;

		public LookProblem()
		{
			curHitTag = " ";
			numberHit = 0;
			correctAnswer = -1;
		}

		// Update is called once per frame
		public void doUpdate () {
            bool hitWall = false; //If it hit any walls yet
            string theTag = "Yo"; // For storing the tags
			// Create a ray that is basically where the camera is looking forward at
			Ray myRay = new Ray (Camera.main.transform.position,
			                     Camera.main.transform.forward);
			RaycastHit[] currentHit;
            currentHit = Physics.RaycastAll(myRay, Mathf.Infinity);
            // Looking at something
            if (currentHit != null) {
                for (int index = 0; index < currentHit.Length; index++)
                {
                    //print(currentHit[index].collider.gameObject);
                    // If there is no tag
                    if (currentHit[index].collider.gameObject.tag == null) 
                    {
                        theTag = "Nahthiswontwork";
                    }
                    else // Otherwise you have to check the tag
                    {
                        theTag = currentHit[index].collider.gameObject.tag;
                    }

                    // If the object hit starts with the proper tag
                    if (theTag.StartsWith("wall"))
                    {
                        print("Hey we are hitting a wall here");
                        if (curHitTag == theTag)
                        {
                            numberHit++; // Increment hit
                            hitWall = true;
                        }
                        else
                            curHitTag = theTag;
                    }
                }
                print("Done with this");
                if (hitWall == false) // If we are not looking at a wall
                {
                    numberHit = 0; // Set hit back to 0
                }
                if (numberHit >= 200) // If you've been on problem long enough
				{
					onProblem = true; // Set onProblem to true
                    if (theTag.Equals("wall1"))
                    {
                        GameObject.FindGameObjectWithTag(theTag)
                            .GetComponent<Candyspawn>().numToSpawn = 3;
                        correctAnswer = 1;
                    }
                    else if (theTag.Equals("wall2"))
                    {
                        GameObject.FindGameObjectWithTag(theTag).
                            GetComponent<Candyspawn>().numToSpawn = 7;
                        correctAnswer = 2;
                    }
                    else if (theTag.Equals("wall3"))
                    {
                        GameObject.FindGameObjectWithTag(theTag)
                            .GetComponent<Candyspawn>().numToSpawn = 11;
                        correctAnswer = 4;
                    }
                    else
                        correctAnswer = -1; // gg can't solve this

                    GameObject.FindGameObjectWithTag(theTag).tag = "NOTwall";
				}
			}	
		}
	}

    public GameObject[] candies;
    public LookProblem keepLooking = new LookProblem(); // Using the class to check 
	public static int currentScore; // How much score you have
	GameObject [] candiesIn; // To store the candies in there
	public int timeLeft = 200; // How long to display the good job/wrong answer message
	public int forInstruction = 100; // How long to show the instructions
	public bool congratulating = false;
	public bool wrongAnswer = false;
    public GUIText scoreText;
	
	// Use this for initialization
	void Start () {
		currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (forInstruction > 0) // This is how long instructions stay 
			forInstruction--;

		if (keepLooking.onProblem == false) {
			keepLooking.doUpdate ();
		}
		else
		{
			// If the user has submitted their answer
			if (GameObject.FindGameObjectWithTag("submitted") != null)
			{
				// Reset the tag to notSubmitted!
				GameObject.FindGameObjectWithTag("submitted").tag = "notSubmitted";
				// Check how many candies are in the box right now
				candiesIn = GameObject.FindGameObjectsWithTag ("InBox");
				if (Mathf.Approximately (candiesIn.GetLength(0), keepLooking.correctAnswer)) 
				{
					keepLooking.onProblem = false;
					incrementScore(); // Call to increment score
					wrongAnswer = false; // It's the right answer so make sure not to scold
					congratulating = true; // It's time to congratulate!
					timeLeft = 200;
				}
				else // Otherwise they got it wrong
				{
					congratulating = false; // You can't support them
					timeLeft = 200; // How much time left to do
					wrongAnswer = true;
				}
			}
			if (congratulating == true) // If you are celebrating
			{
				timeLeft--; // Decrement time
				if (timeLeft <= 0)
					congratulating = false; // Done congratulating
			}
			if (wrongAnswer == true) // If they said the wrong answer
			{
				timeLeft--; // Decrement time
				if (timeLeft <= 0)
					wrongAnswer = false; // Done scolding them
			}
		}	
	}
	
	// Call this to create the new label with score
	void OnGUI()
	{
        /*GameObject.FindGameObjectWithTag("ScoreTag").GetComponent<GUIText>().text =
            currentScore.ToString(); */
        GUI.Label(new Rect(10, 10, 200, 200), "Current score: "
                       + currentScore.ToString());
		if (keepLooking.onProblem == true) { // If you are on a problem right now
			GUI.Label(new Rect (10, 40, 150, 150), "Throw a candy at the cat, Rubik's cube, or door" + 
			          " to submit your answer!");
		}
		if (congratulating == true) { // If you are being congratulated on answering
			GUI.Label (new Rect (200, 0, 150, 150), "Great job on the problem!"); 
		}
		if (wrongAnswer == true) { // If you are being scolded on wrong answer
			GUI.Label (new Rect (200, 0, 150, 150), "Wrong answer! Try again..."); 
		}
		if (forInstruction > 0) { // The instructions in the very beginning
			GUI.Label (new Rect (10, 150, 200, 200), "Welcome to class!\n" + 
			           "Solve the fraction problems!");
		}
	}
	
	// Called when score needs to be incremented
	void incrementScore()
	{
		currentScore++;
	}
}
