using UnityEngine;
using System.Collections;

public class CandyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void onCollisionEnter(Collision col)
	{
		// If the candy has collided with the basket
		if (col.gameObject.tag.Equals ("basket")) {
			gameObject.tag = "InBox"; // Set the tag to InBox
		}
	}

	void onCollisionExit (Collision col)
	{
		// If the candy is not currently in the basket
		if (col.gameObject.tag.Equals ("basket")) {
			gameObject.tag = "NopenotInBox"; // Set tag away from InBox
		}
	}

}
