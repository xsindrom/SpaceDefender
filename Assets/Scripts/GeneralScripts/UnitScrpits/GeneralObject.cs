﻿using UnityEngine;
using System.Collections;

public class GeneralObject : MonoBehaviour
{
    private float gravityScale;
    private Vector2 position;
    
    [Header(StringHeadersInfo.GRAVITYSCALE_Header)]
    public float minGravityScale = 0.0f;
    public float maxGravityScale = 0.0f;
    [Header(StringHeadersInfo.POSITION_Header)]
    public float minXPosition = 0.0f;
    public float maxXPosition = 0.0f;
    public float minYPosition = 0.0f;
    public float maxYPosition = 0.0f;
    
    //--Components---
    private Rigidbody2D rbody;
    void Awake()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        SetGravityScale();
        SetPosition();
    }
    private void SetGravityScale()
    {
        gravityScale = Random.Range(minGravityScale, maxGravityScale);
        if (rbody)
        {
            rbody.gravityScale = gravityScale;
        }
    }
    private void SetPosition()
    {
        position = new Vector2(Random.Range(minXPosition, maxXPosition), Random.Range(minYPosition, maxYPosition));
        if (rbody)
        {
            rbody.position = position;
        }
    }
   
}
