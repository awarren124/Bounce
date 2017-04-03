using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostBallXDestroyer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, gameObject.GetComponent<Animation>()["LostBallX"].time);

    }

}
