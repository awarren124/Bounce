using UnityEngine;
using System.Collections;

public class SpecialBall : MonoBehaviour {

    Camera cam;
    bool active = false;
    float timer = 0.0F;
    Color color;

    enum BallType {Reverse, BallShrink, PlatformExpand, Strobe, Slow};
//    enum BallType {Reverse = Color.red, BallShrink = Color.blue, PlatformExpand = Color.yellow, Strobe = Color.gray};
    //BallType type;
    BallType type = (BallType)Random.Range(0, System.Enum.GetNames(typeof(BallType)).Length);
    //string[] types = { "reverse", "shrink balls", "expand platform" , "strobe"};
    //Color[] colors = { Color.red, Color.blue, Color.yellow, Color.gray };
    

	// Use this for initialization
	void Start () {
        type = BallType.BallShrink;
        cam = Camera.main;
        print(type);
        //type = BallType.Slow;
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

        //GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
    }
	
	// Update is called once per frame
	void Update () {
        //print("Kinematic: " + isKinematic);
        if(active){
            //print(timer);
            timer += Time.deltaTime;
            if(type == BallType.Strobe) {
                float lerp = Mathf.PingPong(Time.time, 1.0F) / 1.0F;
                float alpha = Mathf.Lerp(0.0F, 1.0F, lerp);
                Color color = GetComponent<SpriteRenderer>().material.color;
                color.a = alpha;
                GetComponent<SpriteRenderer>().material.color = color;
            }
            if(timer > 10){
                active = false;
                shrink();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colinfo){
        if(colinfo.collider.tag == "Platform"){
            print("collision");
            grow();
            //GetComponent<Rigidbody2D>().isKinematic = true;
            //GetComponent<Animation>().Play();
        }
    }

    void grow(){
        if(type != BallType.Strobe) {
            GetComponent<SpriteRenderer>().sortingOrder = -100;
        }else{
            GetComponent<SpriteRenderer>().sortingOrder = 100;
        }
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animation>().Play("SpecialBallGrow");
    }

    void growOver (){
        switch(type) {
            case BallType.Reverse:
                Platform.isReversed = true;
                break;
            case BallType.BallShrink:
                Ball.shrinkBalls = true;
                break;
            case BallType.PlatformExpand:
                Platform.expand = true;
                break;
            case BallType.Slow:
                Time.timeScale = 0.5F;
                break;
        }

        cam.backgroundColor = color;
        active = true;
    }

    void shrink(){
        cam.backgroundColor = Color.black;
        GetComponent<Animation>().Play("SpecialBallShrink");
    }

    void shrinkOver(){
        switch(type) {
            case BallType.Reverse:
                Platform.isReversed = false;
                break;
            case BallType.BallShrink:
                Ball.expandBalls = true;
                break;
            case BallType.PlatformExpand:
                Platform.shrink = true;
                break;
            case BallType.Slow:
                Time.timeScale = 1.0F;
                break;
        }
        GameManager.spawnSpecial = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "FallTrigger"){
            GameManager.spawnSpecial = true;
            Destroy(gameObject);
        }
    }

}
