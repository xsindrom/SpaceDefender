using UnityEngine;
using System.Collections;

public class BulletMovementScript : MonoBehaviour
{
    #region FIELDS_TO_DISPLAY
    public string nameFrom;
    public float speed;
    #endregion
    #region FIELDS_TO_CACHE
    private GameObject from;
    private Rigidbody2D rbody;
    #endregion
    private float angle;

    #region STANDART_EVENTS
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        from = GameObject.Find(nameFrom);
        if (from == null)
        {
            return;
        }
        angle = from.GetComponent<Rigidbody2D>().rotation * Mathf.Deg2Rad;
        if (from.GetComponent<BuildGunScript>())
        {
            speed = (float)from.GetComponent<BuildGunScript>().GunStat.Powerfull;
        }
    }
    void Update()
    {
        Move();
    }
    #endregion
    #region LOGIC
    private void Move()
    {
        rbody.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
    }
    #endregion
}
