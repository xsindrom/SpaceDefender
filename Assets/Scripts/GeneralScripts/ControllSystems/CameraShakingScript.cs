using UnityEngine;
using System.Collections;

public class CameraShakingScript : MonoBehaviour
{
    private Transform thisTransform = null;
    private Vector3 originalPosition;
    public float shakeAmount = 0.0f;
    public float shakeTime = 0.0f;
    public float shakeSpeed = 0.0f;
    
    private static CameraShakingScript instance;
    public static CameraShakingScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Camera.main.GetComponent<CameraShakingScript>();
                if (instance == null)
                {
                    Camera.main.gameObject.AddComponent<CameraShakingScript>();
                    instance = Camera.main.gameObject.GetComponent<CameraShakingScript>();
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        thisTransform = GetComponent<Transform>();
        originalPosition = thisTransform.localPosition;
    }
    public IEnumerator ShakeCameraIEnumerator(float shakeAmount)
    {
        this.shakeAmount = shakeAmount;
        float time = 0.0f;
        while (time < shakeTime)
        {
            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeAmount;
            thisTransform.localPosition = Vector3.Lerp(thisTransform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);
            yield return null;
            time += Time.deltaTime;
        }
        thisTransform.localPosition = originalPosition;
    }
}
