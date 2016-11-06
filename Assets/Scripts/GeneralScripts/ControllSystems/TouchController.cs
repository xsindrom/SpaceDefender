using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchController : MonoBehaviour
{

    public GameObject whatToControll;
    #region COMPONENTS_TO_CACHE
    private ControllGunSystem controllGun;
    private ShootSystem controllShoot;
    public Animator animationController; //----TEST

    #endregion
    #region STATES
    public bool shootState = false;
    #endregion
    #region STANDART_EVENTS
    void Start()
    {
        if (!whatToControll)
        {
            return;
        }
        #region CACHE_COMPONENTS
        controllGun = whatToControll.GetComponent<BuildGunScript>().ControllGunSystem;
        controllShoot = whatToControll.GetComponent<BuildGunScript>().ShootSystem;
        #endregion
        Slider rotateController = GameObject.Find(StringNamesInfo.ROTATEGUNCONTROLLER_name).GetComponent<Slider>();
        rotateController.value = 90.0f;
        rotateController.minValue = controllGun.MinAngle;
        rotateController.maxValue = controllGun.MaxAngle;
        rotateController.onValueChanged.AddListener(delegate { controllGun.RotateGun(rotateController.value); });
    }
    void Update()
    {
        if (shootState)
        {
            controllShoot.TimerForActions.CompleteAction();
        }
    }
    #endregion
    #region LOGIC
    #region SHOOT_EVENT
    public void Shoot()
    {
        shootState = true;
        animationController.SetBool("isShooting", shootState);
    }
    public void StopShoot()
    {
        shootState = false;
        animationController.SetBool("isShooting", shootState);
    }
    #endregion
    #endregion
}
