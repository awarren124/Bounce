using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Ball ball;
    public SpecialBall specialBall;
    public GameObject wall;
    public GameObject ballFallTriggerPrefab;
    public Vector2 ballDropPoint;
    float ballTImer = 0.0f;
    float levelTimer = 0.0f;
    public static int numOfBalls = 0;
    public static int score = 0;
    public static int lives = 3;
    static int level = 1;
    static float ballTimerTrigger = 1.0f;
    static int numOfBallsTrigger = 3;
    public static bool spawnSpecial = true;
    double specialFreq = 0.995f;

    // Use this for initialization
    void Start() {

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
        ballTImer += Time.fixedDeltaTime;
        levelTimer += Time.fixedDeltaTime;

        if(Random.Range(0.0F, 1.0F) > specialFreq && spawnSpecial){
            SpecialBall specialball = Instantiate(specialBall, ballDropPoint, Quaternion.identity);
            spawnSpecial = false;
        }

        if(levelTimer > 4){
            level++;
            if(ballTimerTrigger > 0.3) {
                ballTimerTrigger -= 0.1f;
                //numOfBallsTrigger += 1;
            }
            levelTimer = 0;
        }

        if(ballTImer > ballTimerTrigger ) {//&& numOfBalls < numOfBallsTrigger) { //Every second if there are less than 3 balls

            //Make new ball
            Ball instantiatedBall = Instantiate(ball, ballDropPoint, Quaternion.identity);

            ballTImer = 0;

            numOfBalls++;
        }

    }

    public static void lostBall(){
        numOfBalls--;
        lives -= 1;
    }

    public static void bouncedBall(){
        score += 1;
        if(score == 6){
        }
    }

    public void gameOver(){
        score = 0;
        level = 1;

    }

}
