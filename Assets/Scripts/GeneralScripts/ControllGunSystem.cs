using UnityEngine;
using System.Collections;

public class ControllGunSystem : MonoBehaviour
{
    //--Contolls rotation of Gun
    public float deltaAngle = 0.0f;
    //--Key names;
    public string rotateLeftKeyName;
    public string rotateRightKeyName;
    //--Limits---
    public float minAngle = 0.0f;
    public float maxAngle = 0.0f;
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void RotateGun()
    {
        int rotationAngle = Mathf.FloorToInt(rbody.rotation);
        if (rotationAngle < maxAngle)
        {
            if (Input.GetKey(rotateLeftKeyName))
            {
                rbody.rotation += deltaAngle;
            }
        }
        if (rotationAngle > minAngle)
        {
            if (Input.GetKey(rotateRightKeyName))
            {
                rbody.rotation -= deltaAngle;
            }
        }
    }
    void Update()
    {
        RotateGun();
    }
   
}
