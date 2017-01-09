using UnityEngine;
using System.Collections;

public class TestTrigger : MonoBehaviour {

	// Use this for initialization
    void Start () {
        print(GetComponent<Collider2D>().isTrigger);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col){
        print(col.tag);
        
    }
}
