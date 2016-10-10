using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchController : MonoBehaviour
{

    public GameObject whatToControll;
    #region COMPONENTS_TO_CACHE
    private ControllGunSystem controllGun;
    private ShootSystem controllShoot;
    #endregion
    #region STATES
    bool rotateLeft = false;
    bool rotateRight = false;
    bool shootState = false;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        if (!whatToControll)
        {
            return;
        }
        #region CACHE_COMPONENTS
        controllGun = whatToControll.GetComponent<ControllGunSystem>();
        controllShoot = whatToControll.GetComponent<ShootSystem>();
        #endregion
    }
    void Update()
    {
        //if (rotateLeft)
        //{
        //    controllGun.RotateLeftGun();
        //}
        //if (rotateRight)
        //{
        //    controllGun.RotateRightGun();
        //}
        if (shootState)
        {
            controllShoot.TimerForActions.CompleteAction();
        }
    }
    void Start()
    {
        Slider rotateController = GameObject.Find("RotateController").GetComponent<Slider>();
        rotateController.value = 90.0f;
        rotateController.minValue = controllGun.MinAngle;
        rotateController.maxValue = controllGun.MaxAngle;
        rotateController.onValueChanged.AddListener(delegate { controllGun.RotateGun(rotateController.value); });
    }
    #endregion
    #region LOGIC
    #region ROTATION_EVENTS
    public void RotateLeft()
    {
        rotateLeft = true;
    }
    public void RotateRight()
    {
        rotateRight = true;
    }
    #endregion
    #region STOP_ROTATION
    public void StopRotateLeft()
    {
        rotateLeft = false;
    }
    public void StopRotateRight()
    {
        rotateRight = false;
    }
    #endregion
    #region SHOOT_EVENT
    //public void Shoot()
    //{
    //    controllShoot.Shoot();
    //}
    public void Shoot()
    {
        shootState = true;
    }
    public void StopShoot()
    {
        shootState = false;
    }
    #endregion
    #endregion
}
