using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    bool startDissapearing = false;
    float t = 0.0f;
    int bounceCount = 0;
    public static bool shrinkBalls = false;
    public static bool expandBalls = false;
    int bounceThresh = 1;

	// Use this for initialization
	void Start () {
        //When instantiated, make add random force left or right
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
	}

    // Update is called once per frame
    void Update() {

        //If the ball has hit the platform
        if (startDissapearing) {

            //Lerp the alpha to zero.
            Color color = GetComponent<SpriteRenderer>().material.color;
            color.a -= Mathf.Lerp(0.0f, 1.0f, t);
            GetComponent<SpriteRenderer>().material.color = color;
            t += 0.5f * Time.deltaTime;

            //When the ball is no longer visible, destory it
            if (color.a <= 0) {
                Destroy(gameObject);
                GameManager.numOfBalls--;
            }
        }

        if (shrinkBalls){
            shrinkBall();
            shrinkBalls = false;
        }
        if (expandBalls){
            expandBall();
            expandBalls = false;
        }

	}

    void shrinkBall() {
        GetComponent<Animation>().Play("BallShrink");
    }

    void expandBall()
    {
        GetComponent<Animation>().Play("BallGrow");
    }

    void OnTriggerEnter2D(Collider2D col) {

        //If the ball goes past the 
        if(col.tag == "FallTrigger"){
            GameManager.lostBall();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D colinfo){
        if(colinfo.collider.tag == "Platform"){
            //startDissapearing = true;
            bounceCount++;
            GameManager.bouncedBall();
            if(bounceCount == bounceThresh){
                startDissapearing = true;
            }
        }
    }
}
