using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 startDirection;
    private Rigidbody2D rbody;
    [SerializeField]
    private float speed = 0.0f;
    [Header(StringHeadersInfo.SPEED_Limits_Header)]
    public float minSpeed = 0.0f;
    public float maxSpeed = 0.0f;
    [Header(StringHeadersInfo.DIRECTION_Header)]
    public float minXDir = 0.0f;
    public float maxXDir = 0.0f;
    public float minYDir = 0.0f;
    public float maxYDir = 0.0f;
    public bool canMove;
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        SetStartDirection();
        SetSpeed();
        canMove = true;
    }
    private void SetSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed + PlayerStats.Current.Level * 2);
    }
    private void SetStartDirection()
    {
        startDirection = new Vector2(Random.Range(minXDir, maxXDir), Random.Range(minYDir, maxYDir));
    }
    private void Move()
    {
        if (canMove)
        {
            rbody.velocity = startDirection * speed;
        }
    }
    void FixedUpdate()
    {
        Move();
    }
}
