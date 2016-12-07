using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchController : MonoBehaviour
{

    public GameObject whatToControll;
    private ControllGunSystem controllGun;
    private ShootSystem controllShoot;
    public Animator animationController;
    public AudioSource shootSound;
    public AudioSource deniedSound;
    public bool shootState = false;
    void Start()
    {
        MyExtensionMethods.InitAudio(ref shootSound, StringNamesInfo.FIRE_SOUND);
        MyExtensionMethods.InitAudio(ref deniedSound, StringNamesInfo.DENIED_SOUND);
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
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        controllShoot.TimerForActions.AddAction(delegate
        {
            if (shootSound && SettingsScript.EffectVolume > 0.01f)
            {
                if (controllShoot.AmmoStats.IsAbleToShoot())
                {
                    shootSound.Play();
                }
                else
                {
                    deniedSound.Play();
                }
            }
        });
    }
    void Update()
    {
        if (shootState)
        {
            controllShoot.TimerForActions.CompleteAction();
        }
    }
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
}
