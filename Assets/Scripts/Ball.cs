using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    bool startDissapearing = false;
    float t = 0.0f;
    int bounceCount = 0;
    public static bool shrinkBalls = false;

	// Use this for initialization
	void Start () {
        //When instantiated, make add random force left or right
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
	}
	
	// Update is called once per frame
	void Update () {

        //If the ball has hit the platform
        if(startDissapearing){

            //Lerp the alpha to zero.
            Color color = GetComponent<SpriteRenderer>().material.color;
            color.a -= Mathf.Lerp(0.0f, 1.0f, t);
            GetComponent<SpriteRenderer>().material.color = color;
            t += 0.5f * Time.deltaTime;

            //When the ball is no longer visible, destory it
            if(color.a <= 0){
                Destroy(gameObject);
                GameManager.numOfBalls--;
            }
        }

        if(shrinkBalls && transform.localScale.x > 0.5F){
            transform.localScale -= new Vector3(0.5F, 0.5F, 1) * Time.deltaTime;
        }

        if(!shrinkBalls && transform.localScale.x < 1){
            transform.localScale += new Vector3(0.5F, 0.5F, 1) * Time.deltaTime;
        }

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
            if(bounceCount == 3){
                startDissapearing = true;
            }
        }
    }
}
