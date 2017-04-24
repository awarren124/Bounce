using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecialBall : MonoBehaviour {

    Camera cam;
    public bool active = false;
    float timer = 0.0F;
    public Sprite plainSprite;
    public bool expanded = false;
    public bool expanding = false;

    enum BallType { Reverse, BallShrink, PlatformExpand, Strobe, Slow };
    //    enum BallType {Reverse = Color.red, BallShrink = Color.blue , PlatformExpand = Color.yellow, Strobe = Color.gray};
    //BallType type;
    BallType type;
    //string[] types = { "reverse", "shrink balls", "expand platform" , "strobe"};
    //Color[] colors = { Color.red, Color.blue, Color.yellow, Color.gray };


    // Use this for initialization
    void Start() {
        type = (BallType)Random.Range(0, System.Enum.GetNames(typeof(BallType)).Length);
        type = BallType.Reverse;
        cam = Camera.main;
        /**/


        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
        GetComponent<Animation>()["SpecialBallMystery"].time = Random.Range(0, GetComponent<Animation>()["SpecialBallMystery"].length);
        GetComponent<Animation>().Play("SpecialBallMystery");
    }

    // Update is called once per frame
    void Update() {
        //print("Kinematic: " + isKinematic);
        if(active) {
            //print(timer);
            //print(GetComponent<SpriteRenderer>().material.color.a);

            if(!GameManager.isPaused)
                timer += Time.deltaTime;
            if(type == BallType.Strobe) {
                float lerp = Mathf.PingPong(Time.time, 1.0F);
                float alpha = Mathf.Lerp(1.0F, 0.0F, lerp);
                Color color = GetComponent<SpriteRenderer>().material.color;
                color.a = alpha;
                GetComponent<SpriteRenderer>().material.color = color;
            }
            if(timer > 10) {

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
        if(colinfo.collider.tag == "Platform") {
            GetComponent<Animation>().Stop("SpecialBallMystery");
            GetComponent<SpriteRenderer>().sprite = plainSprite;
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

    void grow() {
        expanding = true;
        if(type != BallType.Strobe) {
            GetComponent<SpriteRenderer>().sortingOrder = -100;
        } else {
            GetComponent<SpriteRenderer>().sortingOrder = 100;
            //GetComponent<Animation>().Play("StrobeSpecialBallGrow");
        }
        GetComponent<Animation>().Play("SpecialBallGrow");

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //GetComponent<SpriteRenderer>().sortingOrder = 100;

    }

    void growOver() {
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
        expanding = false;
        expanded = true;
        active = true;
        GameManager.ui.startSpecialBallTimer();
    }

    public void shrink() {
        active = false;
        expanded = false;
        cam.backgroundColor = Color.black;
        GetComponent<Animation>().Play("SpecialBallShrink");
        if(type == BallType.Reverse)
            Platform.isReversed = false;
    }

    void shrinkOver() {
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
        if(col.tag == "FallTrigger") {
            GameManager.spawnSpecial = true;
            Destroy(gameObject);
        }
    }

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
