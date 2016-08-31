using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

    public GameObject whatToTouch;
    private bool isTouching = false;
    public bool IsTouching
    {
        get { return isTouching; }
        set { isTouching = value; }
    }


    void OnCollisionStay2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject == whatToTouch)
        {
            isTouching = true;
        }
    }
    void OnCollisionExit2D(Collision2D collisionToDetect)
    {
        isTouching = false;
    }
}
