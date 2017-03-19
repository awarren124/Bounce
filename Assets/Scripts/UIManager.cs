using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager{// : MonoBehaviour {
    Text scoreLabel;
    Text livesLabel;
    GameObject panel;
    CanvasRenderer[] panelChildren;
    CanvasRenderer panelRenderer;
    GameObject lostBallX;
	// Use this for initialization

    public UIManager(Text sL, Text lL, GameObject p, GameObject i){
        scoreLabel = sL;
        livesLabel = lL;
        panel = p;
        lostBallX = i;

    }

    public void updateLives(int lives) {
        livesLabel.text = "Lives: " + lives;
    }

    public void updateScore(int score) {
        scoreLabel.text = "Score: " + score;
    }

    public void showGameOverPanel(){

        panel.GetComponentInChildren<Button>().interactable = true;
        panel.GetComponent<Animation>()["UIIn"].speed = 1;
        panel.GetComponent<Animation>()["UIIn"].time = 0.0F;
        panel.GetComponent<Animation>().Play("UIIn");
    }

    public void restart(){
        panel.GetComponent<Animation>()["UIIn"].speed = -1;
        panel.GetComponent<Animation>()["UIIn"].time = panel.GetComponent<Animation>()["UIIn"].length;
        panel.GetComponent<Animation>().Play("UIIn");
        panel.GetComponentInChildren<Button>().interactable = false;
        
    }

    public void showLostBallX(Vector2 pos)
    {
        Debug.Log(pos);
        pos.y += 2;
        Debug.Log(pos);
        GameObject xImg = GameObject.Instantiate(lostBallX, pos, Quaternion.identity);

    }
}
