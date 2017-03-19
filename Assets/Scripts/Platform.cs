using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public static bool expand = false;
    public static bool shrink = false;
    public static bool platformIsExpanded = false;
    public static bool isReversed = false;
    Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
	
    // Update is called once per frame
    void Update() {
        
        if(Input.touchCount > 0) {
            Vector2 touchPos = Input.GetTouch(0).position;
            if(isReversed){
                touchPos.x = Screen.width - Input.GetTouch(0).position.x;
            }
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touchPos);

            worldPoint.z = transform.position.z;
            worldPoint.y = transform.position.y;
            //rb.MovePosition(new Vector3(worldPoint.x / 2, worldPoint.y, worldPoint.z));
            rb.MovePosition(new Vector2(transform.position.x - (transform.position.x - worldPoint.x) / 2, transform.position.y));
        }

        if(expand){
            expandPlatform();
            platformIsExpanded = true;
            expand = false;
        }
        if(shrink){
            shrinkPlatform();
            platformIsExpanded = false;
            shrink = false;
        }
    }

    public void expandPlatform(){
        GetComponent<Animation>().Play("PlatformGrow");
    }
    public void shrinkPlatform(){
        GetComponent<Animation>().Play("PlatformShrink");
    }
}
