using UnityEngine;
using System.Collections;

public class GeneralObject : MonoBehaviour
{
    private float gravityScale;
    private Vector2 position;
    private Vector2 startDirection;
    //--GravityScale---
    public float minGravityScale = 0.0f;
    public float maxGravityScale = 0.0f;
    //--Position---
    public float minXPosition = 0.0f;
    public float maxXPosition = 0.0f;
    public float minYPosition = 0.0f;
    public float maxYPosition = 0.0f;
    //--Direction vector---
    public float minXDir = 0.0f;
    public float maxXDir = 0.0f;
    public float minYDir = 0.0f;
    public float maxYDir = 0.0f;
    //--Components---
    private Rigidbody2D rbody;
    void Awake()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        SetGravityScale();
        SetPosition();
        SetStartDirection();
    }
    private void SetGravityScale()
    {
        gravityScale = Random.Range(minGravityScale, maxGravityScale);
        rbody.gravityScale = gravityScale;
    }
    private void SetPosition()
    {
        position = new Vector2(Random.Range(minXPosition, maxXPosition), Random.Range(minYPosition, maxYPosition));
        rbody.position = position;
    }
    private void SetStartDirection()
    {
        startDirection = new Vector2(Random.Range(minXDir, maxXDir), Random.Range(minYDir, maxYDir));
        rbody.AddForce(startDirection);
    }
}
