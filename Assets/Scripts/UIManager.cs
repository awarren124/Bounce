using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager{// : MonoBehaviour {
    Text scoreLabel;
    Text livesLabel;
    GameObject panel;
    CanvasRenderer[] panelChildren;
    CanvasRenderer panelRenderer;
	// Use this for initialization

    public UIManager(Text sL, Text lL, GameObject p){
        scoreLabel = sL;
        livesLabel = lL;
        panel = p;
        panelRenderer = panel.GetComponent<CanvasRenderer>();
        panelChildren = panel.GetComponentsInChildren<CanvasRenderer>();
        /*panelRenderer.SetAlpha(0.0F);
        for(int i = 0; i < panelChildren.Length; i++){
            panelChildren[i].SetAlpha(0.0F);
        }*/
    }

    public void updateLives(int lives) {
        livesLabel.text = "Lives: " + lives;
    }

    public void updateScore(int score) {
        scoreLabel.text = "Score: " + score;
    }

    public void showGameOverPanel(){ 
        /*panelRenderer.SetAlpha(1.0F);
        for(int i = 0; i < panelChildren.Length; i++){
            panelChildren[i].SetAlpha(1.0F);
        }*/
        panel.GetComponent<Animation>().Play("UIIn");
    }

    public void restart(){
        panel.GetComponentInChildren<Button>().interactable = false;
        panelRenderer.SetAlpha(0.0F);
        for(int i = 0; i < panelChildren.Length; i++){
            panelChildren[i].SetAlpha(0.0F);
        }
    }
}
