using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Text scoreLabel;
    public Text livesLabel;
    public GameObject panel;
    public Ball ball;
    public SpecialBall specialBall;
    public GameObject wall;
    public GameObject ballFallTriggerPrefab;
    public Vector2 ballDropPoint;
    static float ballTimer = 0.0f;
    float levelTimer = 0.0f;
    public static int numOfBalls = 0;
    public static int score = 0;
    public static int lives = 3;
    static int level = 1;
    static float ballTimerTrigger = 1.0f;
    static int numOfBallsTrigger = 3;
    public static bool spawnSpecial = true;
    double specialFreq = 0.995f;
    static bool shrinkBall = false;
    public static UIManager ui;
    static bool gameOver = false;
//    public b
    // Use this for initialization
    void Start() {
        ui = new UIManager(scoreLabel, livesLabel, panel);
        Application.targetFrameRate = 60;

        //Level setup

        //Walls
        GameObject leftWall = Instantiate(wall,
                                          Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2)), //Center at edge of screen
                                          Quaternion.identity) as GameObject;
        leftWall.transform.position += new Vector3(0, 0, 1); //Bring to front
        leftWall.transform.localScale = new Vector2(1, Camera.main.orthographicSize * 2); //Makes wall stretch to screen height

        GameObject rightWall = Instantiate(wall,
                                           Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2)),
                                           Quaternion.identity) as GameObject;
        rightWall.transform.position += new Vector3(0, 0, 1);
        rightWall.transform.localScale = new Vector2(1, Camera.main.orthographicSize * 2);

        //Ball fall trigger
        GameObject fallTrigger = Instantiate(ballFallTriggerPrefab,
                                             new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 1.5f),
                                             Quaternion.identity) as GameObject;
        fallTrigger.transform.localScale = new Vector2(Camera.main.orthographicSize * 2 * Screen.width / Screen.height,
                                                       1);
    }
	
    // Update is called once per frame
    void FixedUpdate() {
        if (!gameOver)
        {
            ballTimer += Time.fixedDeltaTime;
            levelTimer += Time.fixedDeltaTime;

            if (Random.Range(0.0F, 1.0F) > specialFreq && spawnSpecial)
            {
                SpecialBall specialball = Instantiate(specialBall, ballDropPoint, Quaternion.identity);
                spawnSpecial = false;
            }

            if (levelTimer > 4)
            {
                level++;
                if (ballTimerTrigger > 0.3)
                {
                    ballTimerTrigger -= 0.1f;
                    //numOfBallsTrigger += 1;
                }
                levelTimer = 0;
            }

            if (ballTimer > ballTimerTrigger)
            {//&& numOfBalls < numOfBallsTrigger) { //Every second if there are less than 3 balls

                //Make new ball
                Ball instantiatedBall = Instantiate(ball, ballDropPoint, Quaternion.identity);
                if (shrinkBall)
                {
                    instantiatedBall.GetComponent<Animation>().Play("BallShrink");
                }
                ballTimer = 0;

                numOfBalls++;
            }

            if (lives < 0)
            {
                startGameOver();
            }
        }

    }

    public static void shrinkBalls(){
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for(int i = 0; i < balls.Length; i++) {
            balls[i].GetComponent<Animation>().Play("BallShrink");
        }
        shrinkBall = true; 
    }

    public static void expandBalls(){
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for(int i = 0; i < balls.Length; i++) {
            balls[i].GetComponent<Animation>().Play("BallGrow");
        }
        shrinkBall = false; 
    }

    public static void lostBall(){
        numOfBalls--;
        lives -= 1;
        ui.updateLives(lives);
    }

    public static void bouncedBall(){
        score += 1;
        ui.updateScore(score);
    }

    public void startGameOver(){
        gameOver = true;
//        Time.timeScale = 0.0F;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for(int i = 0; i < balls.Length; i++) {
            balls[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            balls[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        ui.showGameOverPanel();
    }

    public static void restart(){
        ui.restart();
        ballTimerTrigger = 1.0F;
        ballTimer = 0.0F;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for(int i = 0; i < balls.Length; i++){
            Destroy(balls[i]);
        }
        score = 0;
        lives = 3;
        ui.updateScore(score);
        ui.updateLives(lives);
        Time.timeScale = 1.0F;
        gameOver = false;
    }
}
