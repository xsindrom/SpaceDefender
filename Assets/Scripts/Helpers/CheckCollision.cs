using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour
{
    #region FIELDS
    public GameObject whatToTouch;
    private bool isTouching = false;
    #endregion
    #region PROPERTIES
    public bool IsTouching
    {
        get { return isTouching; }
        set { isTouching = value; }
    }
    #endregion
    #region STANDART_EVENTS
    void OnCollisionStay2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject == whatToTouch)
        {
            isTouching = true;
        }
    }
    void OnCollisionExit2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject == whatToTouch)
        {
            isTouching = false;
        }
    }
    #endregion
}
