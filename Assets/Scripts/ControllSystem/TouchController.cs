using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{

    public GameObject whatToControll;
    private ControllGunSystem controllGun;
    private ShootSystem controllShoot;
    bool rotateLeft = false;
    bool rotateRight = false;

    void Awake()
    {
        if (!whatToControll)
        {
            return;
        }
        controllGun = whatToControll.GetComponent<ControllGunSystem>();
        controllShoot = whatToControll.GetComponent<ShootSystem>();
    }

    public void RotateLeft()
    {
        rotateLeft = true;
    }
    public void RotateRight()
    {
        rotateRight = true;
    }
    public void StopRotateLeft()
    {
        rotateLeft = false;
    }
    public void StopRotateRight()
    {
        rotateRight = false;
    }
    public void Shoot()
    {
        controllShoot.Shoot();
    }

    void Update()
    {
        if (rotateLeft)
        {
            controllGun.RotateLeftGun();
        }
        if (rotateRight)
        {
            controllGun.RotateRightGun();
        }
    }
}
