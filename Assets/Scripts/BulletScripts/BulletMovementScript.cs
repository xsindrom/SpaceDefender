using UnityEngine;
using System.Collections;

public class BulletMovementScript : MonoBehaviour
{
    public string nameFrom;
    public float speed;
    private GameObject from;
    private float angle;
    private float timeToDestroy = 5.0f;
    void Start()
    {
        from = GameObject.Find(nameFrom);
        if (from == null)
        {
            return;
        }
        StartCoroutine(CalculateAngle());
        if (from.GetComponent<BuildGunScript>())
        {
            speed = (float)from.GetComponent<BuildGunScript>().GunStat.Powerfull;
        }
        StartCoroutine(DestroyThis());
    }
    
    void Update()
    {
        Move();
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
    IEnumerator CalculateAngle()
    {
        angle = from.GetComponent<Rigidbody2D>().rotation;
        transform.Rotate(new Vector3(0.0f, 0.0f, angle - 90.0f));
        yield return null;
    }
    private void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
