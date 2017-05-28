using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    bool startDissapearing = false;
    float t = 0.0f;
    int bounceCount = 0;
    public static bool shrinkBalls = false;
    public static bool expandBalls = false;
    int bounceThresh = 1;
    public bool stopStrobing = false;
    float seenTimer = 0.0F;

    void Start() {
        //Adds random force left or right when spawned
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
    }

    void Update() {

        //If the ball has hit the platform
        if(startDissapearing) {
            //If the game mode is blind, the ball stops strobing
            stopStrobing = true;
            seenTimer += Time.deltaTime;
            //Turns completely opaque for 0.1 seconds
            if(seenTimer < 0.1F) {
                Color c = GetComponent<SpriteRenderer>().material.color;
                c.a = 1.0F;
                GetComponent<SpriteRenderer>().material.color = c;
            } else {
                //Lerp the alpha to zero.
                Color color = GetComponent<SpriteRenderer>().material.color;
                color.a -= Mathf.Lerp(0.0f, 1.0f, t);
                GetComponent<SpriteRenderer>().material.color = color;
                t += 0.5f * Time.deltaTime;

                //When the ball is no longer visible, destory it
                if(color.a <= 0) {
                    Destroy(gameObject);
                    GameManager.numOfBalls--;
                }
            }
        }

    }



    void OnTriggerEnter2D(Collider2D col) {
        //If the player doesn't bounce the ball
        if(col.tag == "FallTrigger") {
            GameManager.lostBall(gameObject.transform.position);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D colinfo) {
        //If the player bounces the ball
        if(colinfo.collider.tag == "Platform") {
            GameManager.bouncedBall();
            startDissapearing = true;
        }
    }

    //Plays animation on game over
    public void gameOver() {
        GetComponent<Animation>().Play("GameOver");
        Destroy(gameObject, GetComponent<Animation>().GetClip("GameOver").length);
    }

}
