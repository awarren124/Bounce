using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D colinfo) {
        if(colinfo.collider.tag == "Platform") {
            GetComponent<Animation>().Play("StarCollected");
            //Destroy(gameObject, GetComponent<Animation>()["StarCollected"].length);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {

        //If the ball goes past the trigger
        if(col.tag == "FallTrigger") {
            Destroy(gameObject);
        }
    }

    void collected(){
        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}
