using UnityEngine;
using System.Collections;

public class BulletMovementScript : MonoBehaviour
{
    public string nameFrom;
    public float speed;
    private GameObject from;
    private float angle;
    private Rigidbody2D rbody;

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

    private void Move()
    {
        rbody.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
    }

    void Update()
    {
        Move();
    }
  
}
