using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecialBall : MonoBehaviour {

    public bool active = false;
    float timer = 0.0F;
    float timeThreshold;
    public Sprite plainSprite;
    public bool expanded = false;
    public bool expanding = false;

    enum BallType { Reverse, BallShrink, PlatformExpand, Strobe, Slow };
    BallType type;

    void Start() {

        //Adds random force left or right
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));

        //Plays color looping animation
        GetComponent<Animation>()["SpecialBallMystery"].time = Random.Range(0, GetComponent<Animation>()["SpecialBallMystery"].length);
        GetComponent<Animation>().Play("SpecialBallMystery");
    }

    void Update() {
        if(active) {
            if(!GameManager.isPaused)
                timer += Time.deltaTime;

            //Fluxuates alpha if it is the gray type
            if(type == BallType.Strobe) {
                float lerp = Mathf.PingPong(Time.time, 1.0F);
                float alpha = Mathf.Lerp(1.0F, 0.0F, lerp);
                Color color = GetComponent<SpriteRenderer>().material.color;
                color.a = alpha;
                GetComponent<SpriteRenderer>().material.color = color;
            }

            //Shrinks after certain amount of time
            if(timer > timeThreshold) {
                shrink();
            }
        }

        if(GameManager.gameOver) {
            if(active) {
                shrink();
            }else {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colinfo) {

        //Special ball collected by player
        if(colinfo.collider.tag == "Platform") {

            //Stops color switching animation and makes sprite the plain one with no question mark
            GetComponent<Animation>().Stop("SpecialBallMystery");
            GetComponent<SpriteRenderer>().sprite = plainSprite;

            //Changes the sprite color to match the type
            switch(type) {
                case BallType.Reverse:
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case BallType.BallShrink:
                    GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case BallType.PlatformExpand:
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                case BallType.Strobe:
                    GetComponent<SpriteRenderer>().color = Color.gray;
                    break;
                case BallType.Slow:
                    GetComponent<SpriteRenderer>().color = Color.cyan;
                    break;
            }

            grow();
        }
    }

    //Expands special ball
    void grow() {
        expanding = true;

        //Put it at the front if it is the strobe special ball but the back if it is one of the others
        if(type != BallType.Strobe) {
            GetComponent<SpriteRenderer>().sortingOrder = -100;
        } else {
            GetComponent<SpriteRenderer>().sortingOrder = 100;
        }
        GetComponent<Animation>().Play("SpecialBallGrow");

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    //Special ball done growing
    void growOver() {
        //Calls respective special ball methods
        switch(type) {
            case BallType.Reverse:
                Time.timeScale = 0.6F;
                Time.fixedDeltaTime = Time.timeScale * 0.02F;
                Platform.isReversed = true;
                break;
            case BallType.BallShrink:
                GameManager.shrinkBalls();
                break;
            case BallType.PlatformExpand:
                Platform.expand = true;
                break;
            case BallType.Slow:
                Time.timeScale = 0.5F;
                Time.fixedDeltaTime = Time.timeScale * 0.02F;
                break;
        }
        timeThreshold = 10.0F * Time.timeScale;
        expanding = false;
        expanded = true;
        active = true;
        GameManager.ui.startSpecialBallTimer(timeThreshold);
    }

    //Shrinks special ball
    public void shrink() {
        active = false;
        expanded = false;
        GetComponent<Animation>().Play("SpecialBallShrink");
        if(type == BallType.Reverse)
            Platform.isReversed = false;
    }

    //Called when special ball finishes shrinking and destroys it
    void shrinkOver() {
        //Special ball methods reverting to normal
        switch(type) {
            case BallType.Reverse:
                Time.timeScale = 1.0F;
                Time.fixedDeltaTime = Time.timeScale * 0.02F;
                break;
            case BallType.BallShrink:
                GameManager.expandBalls();
                break;
            case BallType.PlatformExpand:
                Platform.shrink = true;
                break;
            case BallType.Slow:
                Time.timeScale = 1.0F;
                Time.fixedDeltaTime = Time.timeScale * 0.02F;
                break;
        }
        GameManager.spawnSpecial = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        //Destroys self if player didn't bounce
        if(col.tag == "FallTrigger") {
            GameManager.spawnSpecial = true;
            Destroy(gameObject);
        }
    }

    //If destroying, revert back to normal if not already
    private void OnDestroy() {
        switch(type) {
            case BallType.Reverse:
                Platform.isReversed = false;
                break;
            case BallType.BallShrink:
                GameManager.expandBalls();
                break;
            case BallType.PlatformExpand:
                if(Platform.platformIsExpanded) {
                    Platform.shrink = true;
                }
                break;
            case BallType.Slow:
                Time.timeScale = 1.0F;
                Time.fixedDeltaTime = Time.timeScale * 0.02F;
                break;
        }
        GameManager.spawnSpecial = true;
    }

    //Sets type while looping through color changing animation
    void setColor(string color) {
        switch(color) {
            case "red":
                type = BallType.Reverse;
                break;
            case "blue":
                type = BallType.BallShrink;
                break;
            case "yellow":
                type = BallType.PlatformExpand;
                break;
            case "cyan":
                type = BallType.Slow;
                break;
            case "gray":
                type = BallType.Strobe;
                break;
            default:
                break;
        }
    }

}
