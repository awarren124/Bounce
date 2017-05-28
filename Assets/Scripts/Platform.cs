using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public static bool expand = false;
    public static bool shrink = false;
    public static bool platformIsExpanded = false;
    public static bool isReversed = false;
    Rigidbody2D rb;
    float sensitivity = 1.5F;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        //Platform only moves when the game is not paused
        if(!GameManager.isPaused){

            //If there is a finger on the screen move the platform to the x location
            if(Input.touchCount > 0) {
                Vector2 touchPos = Input.GetTouch(0).position;

                //Position is reversed when the red special ball is collected
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

    //Expands platorm when the yellow special ball is collected
    public void expandPlatform() {
        setDrawMode(PlayerPrefs.GetInt("ActiveSkin"));
        GetComponent<Animation>().Play("PlatformGrow");
    }

    //Shrings platorm when the yellow special ball expires
    public void shrinkPlatform() {
        GetComponent<Animation>().Play("PlatformShrink");
    }

    //Sets the SpriteRenderer.drawMode attribute so that the platform looks better when it expands for certain skins
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
