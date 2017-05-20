using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public static bool expand = false;
    public static bool shrink = false;
    public static bool platformIsExpanded = false;
    public static bool isReversed = false;
    Rigidbody2D rb;
    float sensitivity = 1.5F;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {

        if(!GameManager.isPaused){

            if(Input.touchCount > 0) {
                Vector2 touchPos = Input.GetTouch(0).position;
                if(isReversed) {
                    touchPos.x = Screen.width - Input.GetTouch(0).position.x;
                }
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touchPos);
                worldPoint.z = transform.position.z;
                worldPoint.y = transform.position.y;
                rb.MovePosition(new Vector2(transform.position.x - (transform.position.x - worldPoint.x) / sensitivity, transform.position.y));
            }
       }
        if(expand) {
            expandPlatform();
            platformIsExpanded = true;
            expand = false;
        }
        if(shrink) {
            shrinkPlatform();
            platformIsExpanded = false;
            shrink = false;
        }
    }

    public void expandPlatform() {
        setDrawMode(PlayerPrefs.GetInt("ActiveSkin"));
        GetComponent<Animation>().Play("PlatformGrow");
    }
    public void shrinkPlatform() {
        GetComponent<Animation>().Play("PlatformShrink");
    }

    public void setDrawMode(int activeSkin) {
        switch(activeSkin) {
            case 0:
            case 1:
            case 3:
            case 5:
            case 6:
                GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
                break;
            case 2:
            case 4:
                GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
                GetComponent<SpriteRenderer>().tileMode = SpriteTileMode.Continuous;
                break;
            default:
                GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Simple;
                break;
        }
    }
}
