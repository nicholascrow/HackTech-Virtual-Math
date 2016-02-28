using UnityEngine;
using System.Collections;

public class SubmitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onCollisionEnter(Collision col)
	{
		// If the candy has collided with the "submit" button
		if (col.gameObject.tag.Equals ("NotInBox")) {
			gameObject.tag = "submitted"; // Set the tag to InBox
		}
	}
}
