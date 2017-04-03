using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void retry(){
        GameManager.restart(false);
    }

    public void classic()
    {
        GameManager.startGame(GameManager.GameMode.Lives);
    }

    public void blind()
    {
        GameManager.startGame(GameManager.GameMode.Blind);
    }

    public void menu()
    {
        GameManager.goToMenu();
    }
}
