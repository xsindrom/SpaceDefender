using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    //--Contolls rotation of Gun
    public float deltaAngle = 0.0f;
    //--Limits---
    public float minAngle = 0.0f;
    public float maxAngle = 0.0f;
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void ControllGun()
    {
        if (Mathf.FloorToInt(rbody.rotation) < maxAngle && Input.GetKey(KeyCode.LeftArrow))
        {
            rbody.rotation += deltaAngle;
        }
        if (Mathf.FloorToInt(rbody.rotation) > minAngle && Input.GetKey(KeyCode.RightArrow))
        {
            rbody.rotation -= deltaAngle;
        }
    }
    void Update()
    {
        ControllGun();
    }
   
}
