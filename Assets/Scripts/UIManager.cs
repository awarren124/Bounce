using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager {// : MonoBehaviour {
    Text scoreLabel;
    Text livesLabel;
    GameObject panel;
    CanvasRenderer[] panelChildren;
    CanvasRenderer panelRenderer;
    GameObject lostBallX;
    GameObject titlePanel;
    GameObject pausePanel;
    Button pauseButton;
    public bool pauseInFrame = true;
    // Use this for initialization

    public UIManager(Text sL, Text lL, GameObject p, GameObject i, GameObject tp, GameObject pp, Button pb) {
        scoreLabel = sL;
        livesLabel = lL;
        panel = p;
        lostBallX = i;
        titlePanel = tp;
        pausePanel = pp;
        pauseButton = pb;
    }

    public void updateLives(int lives) {
        livesLabel.text = "Lives: " + lives;
    }

    public void updateScore(int score) {
        scoreLabel.text = "Score: " + score;
    }

    public void showGameOverPanel() {

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
    }

    public void showLostBallX(Vector2 pos) {
        pos.y += 2;
        GameObject xImg = GameObject.Instantiate(lostBallX, pos, Quaternion.identity);
    }

    public void fadeOutTitle() {
        titlePanel.GetComponent<Animation>()["TitleOut"].speed = 1;
        titlePanel.GetComponent<Animation>()["TitleOut"].time = 0;
        titlePanel.GetComponent<Animation>().Play("TitleOut");

        scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].speed = 1;
        scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].time = 0;
        scoreLabel.GetComponentInParent<Animation>().Play("LabelsIn");

        Debug.Log("fadeouttitle");
        showPauseButton();
    }

    public void menuIn(bool isPause) {
        titlePanel.GetComponent<Animation>()["TitleOut"].speed = -1;
        titlePanel.GetComponent<Animation>()["TitleOut"].time = titlePanel.GetComponent<Animation>()["TitleOut"].length;
        titlePanel.GetComponent<Animation>().Play("TitleOut");

        scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].speed = -1;
        scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].time = scoreLabel.GetComponentInParent<Animation>()["LabelsIn"].length;
        scoreLabel.GetComponentInParent<Animation>().Play("LabelsIn");

        if(pauseInFrame)
            hidePauseButton();
    }

    public void showPauseMenu() {
        pausePanel.GetComponent<Animation>()["UIIn"].speed = 1;
        pausePanel.GetComponent<Animation>()["UIIn"].time = 0.0F;
        pausePanel.GetComponent<Animation>().Play("UIIn");

        if(pauseInFrame)
            hidePauseButton();
    }

    public void hidePauseMenu(bool isRestart) {
        pausePanel.GetComponent<Animation>()["UIIn"].speed = -1;
        pausePanel.GetComponent<Animation>()["UIIn"].time = pausePanel.GetComponent<Animation>()["UIIn"].length;
        pausePanel.GetComponent<Animation>().Play("UIIn");

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
}
