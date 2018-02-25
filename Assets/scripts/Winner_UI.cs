using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner_UI : MonoBehaviour {

	public GameObject winnerUI;
	public bool colliding = false;

	// Use this for initialization
	void Start () {
		winnerUI.active = false;
		
	}

	void OnTriggerEnter2D(Collider2D trig) {
		if (trig.gameObject.name == "flag") {
			colliding = true;	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (colliding) {
			winnerUI.active = true;
		} else {
			winnerUI.active = false;
		}
		
	}
}
