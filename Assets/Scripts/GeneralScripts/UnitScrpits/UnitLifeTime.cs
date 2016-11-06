using UnityEngine;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    public int lifeCost;
    public DeathHandler deathHandler;
    private Animator animController;
    private MovementScript movement;
    private float scaleAmount;
    void Start()
    {
        scaleAmount = transform.localScale.x;
        deathHandler = new DeathHandler();
        deathHandler.AddAction(delegate { PlayerStats.Current.Score += lifeCost * PlayerStats.Current.ScoreMultipler / 10; });
        deathHandler.AddAction(delegate { PlayerStats.Current.IncreaseLevel(lifeCost); });
        deathHandler.AddAction(delegate { if(scaleAmount == 10.0f) PlayerStats.Current.Money += 1; });
        animController = gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<MovementScript>();
    }
    public IEnumerator Death(string animationName)
    {
        movement.canMove = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        animController.Play(animationName);
        yield return new WaitForSeconds(animController.GetCurrentAnimatorStateInfo(0).length);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject.CompareTag(StringNamesInfo.BULLET_tag))
        {
            deathHandler.OnDeath();
            Destroy(collisionToDetect.gameObject);
            StartCoroutine(Death(StringNamesInfo.EXPLODE_inAir_animation_name));
        }
        if (collisionToDetect.gameObject.CompareTag(StringNamesInfo.GROUND_tag))
        {
            StartCoroutine(Death(StringNamesInfo.EXPLODE_onGround_animation_name));
            StartCoroutine(CameraShakingScript.Instance.ShakeCameraIEnumerator(scaleAmount));
            GameManager.Instance.Health -= lifeCost;
        }
    }
}
