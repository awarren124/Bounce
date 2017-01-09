using UnityEngine;
using System.Collections;

public class SpecialBall : MonoBehaviour {

    Camera cam;
    bool active = false;
    float timer = 0.0F;
    string type;
    Color color;
    string[] types = { "reverse", "shrink balls", "expand platform" , "strobe"};
    Color[] colors = { Color.red, Color.blue, Color.yellow, Color.gray };

	// Use this for initialization
	void Start () {
        cam = Camera.main;

        //type = types[Random.Range(0, types.Length)];
        type = types[2];
        color = colors[System.Array.IndexOf(types, type)];

        GetComponent<SpriteRenderer>().color = color;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
    }
	
	// Update is called once per frame
	void Update () {
        if(active){
            timer += Time.deltaTime;
            if(type == "strobe") {
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
            grow();
        }
    }

    void grow(){
        if(type != "strobe") {
            GetComponent<SpriteRenderer>().sortingOrder = -100;
        }else{
            GetComponent<SpriteRenderer>().sortingOrder = 100;
        }
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Animation>().Play("SpecialBallGrow");
    }

    void growOver (){
        switch(type) {
            case "reverse":
                Platform.isReversed = true;
                break;
            case "shrink balls":
                Ball.shrinkBalls = true;
                break;
            case "expand platform":
                Platform.expand = true;
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
            case "reverse":
                Platform.isReversed = false;
                break;
            case "shrink balls":
                Ball.shrinkBalls = false;
                break;
            case "expand platform":
                Platform.expand = false;
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
