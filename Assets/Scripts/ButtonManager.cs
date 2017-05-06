using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void retry() {
        GameManager.restart(false, false, true);
    }

    public void classic() {
        GameManager.startGame(GameManager.GameMode.Lives);
    }

    public void blind() {
        GameManager.startGame(GameManager.GameMode.Blind);
    }

    public void menu() {
        GameManager.goToMenu(false);
    }

    public void pause() {
        GameManager.ui.hidePauseMenu(false);
        if(GameManager.isPaused) {
            GameManager.ui.showPauseButton();
            //Time.timeScale = 1.0F;
            GameManager.play(false);
        } else {
            GameManager.pause();
        }
    }

    public void pauseRetry() {
        Time.timeScale = 1.0F;
        GameManager.restart(false, true, true);
    }

    public void pauseMenu() {
        Time.timeScale = 1.0F;
        GameManager.goToMenu(true);
    }

    public void shop(){
        GameManager.showShop();
    }

    public void tutorialStart(){
        GameManager.showTutorial();
    }

    public void tutorialEnd(){
        GameManager.tutorialEnd();
    }
}