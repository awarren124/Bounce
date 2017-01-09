using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text scoreLabel;
    public Text livesLabel;
	// Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        scoreLabel.text = "Score: " + GameManager.score;
        livesLabel.text = "Lives: " + GameManager.lives;
	}
}
