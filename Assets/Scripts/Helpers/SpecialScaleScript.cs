using UnityEngine;
using System.Collections;

public class SpecialScaleScript : MonoBehaviour
{
    public Resolution idealResolution;
    private RectTransform transformer;
    void Awake()
    {
        transformer = transform.GetComponent<RectTransform>();
        idealResolution.width = 1920;
        idealResolution.height = 1080;
        transformer.localScale = new Vector2(1.0f, 1.0f);
    }
    void Start()
    {
        ScaleObject();
    }
    public void ScaleObject()
    {
        transformer.localScale = new Vector3((float)Screen.currentResolution.width / idealResolution.width, (float)Screen.currentResolution.height / idealResolution.height,1.0f);
    }
}
