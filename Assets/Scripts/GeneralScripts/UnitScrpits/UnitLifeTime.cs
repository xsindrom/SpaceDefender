using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    public int lifeCost;
    public DeathHandler deathHandler;
    private Animator animController;
    private MovementScript movement;
    private float scaleAmount;
    private static AudioSource meteorSoundExplodeInAir;
    private static AudioSource coinSound;
    private static AudioSource meteorSoundExplodeOnGround;
    void Awake()
    {
        MyExtensionMethods.InitAudio(ref meteorSoundExplodeInAir, StringNamesInfo.METEOR_IN_AIR_EXPLOSION_SOUND);
        MyExtensionMethods.InitAudio(ref meteorSoundExplodeOnGround, StringNamesInfo.METEOR_ON_GROUND_EXPLOSION_SOUND);
        MyExtensionMethods.InitAudio(ref coinSound, StringNamesInfo.COIN_SOUND);
    }
    void Start()
    {
        scaleAmount = transform.localScale.x;
        int score = lifeCost * PlayerStats.Current.Level / 10;
        deathHandler = new DeathHandler();
        deathHandler.AddAction(delegate { PlayerStats.Current.Score += score; });
        deathHandler.AddAction(delegate { PlayerStats.Current.IncreaseLevel(lifeCost / 10); });
        deathHandler.AddAction(delegate { if(scaleAmount == 10.0f) PlayerStats.Current.Money += 1; });
        deathHandler.AddAction(delegate {
            Canvas canvasChild = GetComponentInChildren<Canvas>(true);
            
            if (canvasChild)
            {
                canvasChild.gameObject.SetActive(true);
                Text lifeCostChild = canvasChild.GetComponentInChildren<Text>();
                if (lifeCostChild)
                {
                    lifeCostChild.text = score.ToString();
                }
            }
        });
        deathHandler.AddAction(delegate
        {
            if (meteorSoundExplodeInAir && SettingsScript.EffectVolume > 0.01f)
            {
                meteorSoundExplodeInAir.volume = SettingsScript.EffectVolume * transform.localScale.x / 100;
                meteorSoundExplodeInAir.Play();
            }
        });
        deathHandler.AddAction(delegate { if (transform.localScale.x == 10 && coinSound && SettingsScript.EffectVolume > 0.01f) coinSound.Play(); });
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
            if (meteorSoundExplodeOnGround && SettingsScript.EffectVolume > 0.01f)
            {
                meteorSoundExplodeOnGround.volume = SettingsScript.EffectVolume * transform.localScale.x / 100;
                meteorSoundExplodeOnGround.Play();
            }
            GameManager.Instance.Health -= lifeCost;
        }
    }
}
