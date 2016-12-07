using UnityEngine;
using System.Collections;

public class ControllGunSystem : MonoBehaviour
{
    #region FIELDS
    [Header(StringHeadersInfo.DELTA_Angle_Header)]
    public float deltaAngle = 0.0f;
#if UNITY_STANDALONE_WIN
    [Header(StringHeadersInfo.KEY_NAME_Header)]
    public string rotateLeftKeyName;
    public string rotateRightKeyName;
#endif
    [Header(StringHeadersInfo.ANGLE_Limits_Header)]
    private float minAngle = 0.0f;
    private float maxAngle = 0.0f;
    #endregion
    #region PROPERTIES
    public float MinAngle
    {
        get { return minAngle; }
        set
        {
            if (value >= 0.0f && value <= 180.0f) { minAngle = value; }
        }
    }
    public float MaxAngle
    {
        get { return maxAngle; }
        set
        {
            if (value >= 0.0f && value <= 180.0f) { maxAngle = value; }
        }
    }
    #endregion
    private Rigidbody2D rbody;
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }
#if UNITY_ANDROID
    public void RotateLeftGun()
    {
        int rotationAngle = Mathf.FloorToInt(rbody.rotation);
        if (rotationAngle < maxAngle)
        {
            rbody.rotation += deltaAngle;
        }
    }
    public void RotateRightGun()
    {
        int rotationAngle = Mathf.FloorToInt(rbody.rotation);
        if (rotationAngle > minAngle)
        {
            rbody.rotation -= deltaAngle;
        }
    }
    public void RotateGun(float angleToSet)
    {
        if (angleToSet > minAngle && angleToSet < maxAngle)
        {
            rbody.rotation = angleToSet;
        }
    }
#endif

#if UNITY_STANDALONE_WIN
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
#endif
}
