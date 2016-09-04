using UnityEngine;
using System.Collections;

public class ControllGunSystem : MonoBehaviour
{
    [Header(StringHeadersInfo.DELTA_Angle_Header)]
    public float deltaAngle = 0.0f;
    [Header(StringHeadersInfo.KEY_NAME_Header)]
    public string rotateLeftKeyName;
    public string rotateRightKeyName;
    [Header(StringHeadersInfo.ANGLE_Limits_Header)]
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
