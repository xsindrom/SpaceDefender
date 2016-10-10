using UnityEngine;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    public int lifeCost;
    private DeathHandler deathHandler;
    private Animator animController;
    private MovementScript movement;
    private float scaleAmount;
    void Start()
    {
        deathHandler = new DeathHandler();
        deathHandler.AddAction(delegate { ScoreManager.Instance.AddScore(lifeCost, PlayerStats.Current.ScoreMultipler); });
        deathHandler.AddAction(delegate { PlayerStats.Current.IncreaseLevel(10); });
        animController = gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<MovementScript>();
        scaleAmount = transform.localScale.x;
        
    }
    IEnumerator Death(string animationName)
    {
        movement.canMove = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        animController.Play(animationName);
        yield return new WaitForSeconds(animController.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject.CompareTag(StringNamesInfo.BULLET_tag))
        {
            deathHandler.OnDeath();
            Destroy(collisionToDetect.gameObject);
            Destroy(gameObject);
        }
        if (collisionToDetect.gameObject.CompareTag(StringNamesInfo.GROUND_tag))
        {
            StartCoroutine(Death(StringNamesInfo.EXPLODE_animation_name));
            StartCoroutine(CameraShakingScript.Instance.ShakeCameraIEnumerator(scaleAmount));
            GameManager.Instance.Health -= lifeCost;
        }
    }
}
