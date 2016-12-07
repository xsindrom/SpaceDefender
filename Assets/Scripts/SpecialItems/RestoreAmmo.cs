using UnityEngine;
using System.Collections;

public class RestoreAmmo : MonoBehaviour {

    [SerializeField]
    private int ammoToRestore;
    public static AudioSource restoreAmmoSound;
    void Start()
    {
        MyExtensionMethods.InitAudio(ref restoreAmmoSound, StringNamesInfo.AMMO_SOUND);
    }
    private void SetAmmoToRestore()
    {
        ammoToRestore = Random.Range(0, GunStats.Instance.AmmoStats.AmmoSize);
    }
    void OnCollisionEnter2D(Collision2D colliderBullet)
    {
        if (colliderBullet.gameObject.CompareTag(StringNamesInfo.BULLET_tag))
        {
            SetAmmoToRestore();
            GunStats.Instance.AmmoStats.CurrentAmmo += ammoToRestore;
            if (restoreAmmoSound) restoreAmmoSound.Play();
            
            Destroy(gameObject);
        }
    }
}
