using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class with methods that buttons call
public class ButtonManager : MonoBehaviour {

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

    public void tutorialReset() {
        GameManager.ui.resetTutorialPosition();
    }

    public void endGameOverPanelIn() {
        if(GameManager.gameOver) {
            //If the ad counter is 4, show ad
            if(PlayerPrefs.GetInt("AdCounter") >= 4) {
                PlayerPrefs.SetInt("AdCounter", 0);
                GameManager.showAd();
            }

        }
    }

    public void showRewardAd() {
        GameManager.showRewardAd();
    }

    public void rate(){
        GameManager.rate();
    }

    public void credits() {
        GameManager.showCredits();
    }

    public void creditsDone() {
        GameManager.hideCredits();
    }
}