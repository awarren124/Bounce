﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager {// : MonoBehaviour {
    Text scoreLabel;
    Text livesLabel;
    GameObject panel;
    GameObject lostBallX;
    GameObject titlePanel;
    GameObject pausePanel;
    Button pauseButton;
    public bool pauseInFrame = true;
    GameObject timerPanel;
    float sbTimer;
    Text starsLabel;
    GameObject shopPanel;
    // Use this for initialization

    public UIManager(Text sL, Text lL, GameObject p, GameObject i, GameObject tp, GameObject pp, Button pb, GameObject tip, Text stL, GameObject sp) {
        scoreLabel = sL;
        livesLabel = lL;
        panel = p;
        lostBallX = i;
        titlePanel = tp;
        pausePanel = pp;
        pauseButton = pb;
        timerPanel = tip;
        starsLabel = stL;
        shopPanel = sp;
    }

    public void updateLives(int lives) {
        livesLabel.text = "Lives: " + lives;
    }

    public void updateScore(int score) {
        scoreLabel.text = "Score: " + score;
    }

    public void showGameOverPanel(string gm) {
        foreach(Transform child in panel.transform) {
            if(child.CompareTag("High Score")) {
                child.gameObject.GetComponent<Text>().text = "High Score:\n" + PlayerPrefs.GetInt(gm);
            }
        }
        panel.GetComponentInChildren<Button>().interactable = true;
        panel.GetComponent<Animation>()["UIIn"].speed = 1;
        panel.GetComponent<Animation>()["UIIn"].time = 0.0F;
        panel.GetComponent<Animation>().Play("UIIn");

        hidePauseButton();
    }

    public void restart(bool isPauseMenu, bool showButton) {
        GameObject p = isPauseMenu ? pausePanel : panel;
        p.GetComponent<Animation>()["UIIn"].speed = -1;
        p.GetComponent<Animation>()["UIIn"].time = p.GetComponent<Animation>()["UIIn"].length;
        p.GetComponent<Animation>().Play("UIIn");
        if(showButton) {
            Debug.Log("restart(" + isPauseMenu + ", " + showButton + ")");
            showPauseButton();
        }

//        if(!isPauseMenu) {
            GameManager.updateUiSBTimer = false;
            timerPanel.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
//        }
    }

    public void showLostBallX(Vector2 pos) {
        pos.y += 2;
        GameObject xImg = GameObject.Instantiate(lostBallX, pos, Quaternion.identity);
        GameObject.Destroy(xImg, xImg.GetComponentInChildren<Animation>()["LostBallX"].length);
    }

    public void fadeOutTitle(bool showLabelsAndButton) {
        titlePanel.GetComponent<Animation>()["TitleOut"].speed = 1;
        titlePanel.GetComponent<Animation>()["TitleOut"].time = 0;
        titlePanel.GetComponent<Animation>().Play("TitleOut");

        if(showLabelsAndButton) {
            scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].speed = 1;
            scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].time = 0;
            scoreLabel.GetComponentInParent<Animation>().Play("LabelsIn");

            showPauseButton();
        }
    }

    public void menuIn(bool isPause, bool labels) {
        titlePanel.GetComponent<Animation>()["TitleOut"].speed = -1;
        titlePanel.GetComponent<Animation>()["TitleOut"].time = titlePanel.GetComponent<Animation>()["TitleOut"].length;
        titlePanel.GetComponent<Animation>().Play("TitleOut");

        if(labels) {
            scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].speed = -1;
            scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].time = scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].length;
            scoreLabel.GetComponentInParent<Animation>().Play("LabelsIn");
        }

        if(pauseInFrame)
            hidePauseButton();
    }

    public void showPauseMenu() {
        pausePanel.GetComponent<Animation>()["UIIn"].speed = 1;
        pausePanel.GetComponent<Animation>()["UIIn"].time = 0.0F;
        pausePanel.GetComponent<Animation>().Play("UIIn");
        GameManager.updateUiSBTimer = false;
        if(pauseInFrame)
            hidePauseButton();
    }

    public void hidePauseMenu(bool isRestart) {
        pausePanel.GetComponent<Animation>()["UIIn"].speed = -1;
        pausePanel.GetComponent<Animation>()["UIIn"].time = pausePanel.GetComponent<Animation>()["UIIn"].length;
        pausePanel.GetComponent<Animation>().Play("UIIn");
        GameManager.updateUiSBTimer = true;
        if(isRestart) {
            Debug.Log("hidePauseMenu");
            showPauseButton();
        }
    }

    public void showPauseButton() {
        pauseInFrame = true;
        pauseButton.GetComponentInParent<Animation>()["PauseIn"].speed = 1;
        pauseButton.GetComponentInParent<Animation>()["PauseIn"].time = 0.0F;
        pauseButton.GetComponentInParent<Animation>().Play("PauseIn");
    }

    public void hidePauseButton() {
        pauseInFrame = false;
        pauseButton.GetComponentInParent<Animation>()["PauseIn"].speed = -1;
        pauseButton.GetComponentInParent<Animation>()["PauseIn"].time = pauseButton.GetComponent<Animation>()["PauseIn"].length;
        pauseButton.GetComponentInParent<Animation>().Play("PauseIn");
    }

    public void startSpecialBallTimer(){
        sbTimer = 0.0F;
        GameManager.updateUiSBTimer = true;
    }

    public void updateSpecialBallTimer(float deltaTime){
        //if(Time.time - sbInitialTime >= 10.0F){
        sbTimer += deltaTime;
        if(sbTimer >= 10.0F){
            GameManager.updateUiSBTimer = false;
            timerPanel.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            return;
        }
        timerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(50, Screen.height - (sbTimer / 10.0F) * Screen.height);
    }

    public void updateStarsLabel(){
        starsLabel.text = "Stars: " + PlayerPrefs.GetInt("Stars");
    }

    public void showShop(){
        showStarsLabel();
        shopPanel.GetComponent<ShopManager>().shopIn();
    }
        
    public void showStarsLabel(){
        starsLabel.GetComponentInParent<Animation>()["StarLabelIn"].speed = 1;
        starsLabel.GetComponentInParent<Animation>()["StarLabelIn"].time = 0.0F;
        starsLabel.GetComponent<Animation>().Play("StarLabelIn");
    }

    public void hideStarsLabel(){
        starsLabel.GetComponentInParent<Animation>()["StarLabelIn"].speed = -1;
        starsLabel.GetComponentInParent<Animation>()["StarLabelIn"].time = starsLabel.GetComponent<Animation>()["StarLabelIn"].length;
        starsLabel.GetComponent<Animation>().Play("StarLabelIn");
    }

}
