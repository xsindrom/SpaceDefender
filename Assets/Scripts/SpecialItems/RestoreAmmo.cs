using UnityEngine;
using System.Collections;

public class RestoreAmmo : MonoBehaviour {

    [SerializeField]
    private int ammoToRestore;

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
            Destroy(gameObject);
        }
    }
}
